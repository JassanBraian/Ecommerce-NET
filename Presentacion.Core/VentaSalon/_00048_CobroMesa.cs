using Presentacion.Core.Banco;
using Presentacion.Core.Cliente;
using Presentacion.Core.PlanTarjeta;
using Presentacion.Core.Tarjeta;
using Presentacion.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XCommerce.AccesoDatos;
using XCommerce.Servicio.Core.Banco;
using XCommerce.Servicio.Core.Banco.DTOs;
using XCommerce.Servicio.Core.Cliente;
using XCommerce.Servicio.Core.Comprobante;
using XCommerce.Servicio.Core.Comprobante.DTOs;
using XCommerce.Servicio.Core.PlanTarjeta;
using XCommerce.Servicio.Core.PlanTarjeta.DTOs;
using XCommerce.Servicio.Core.Tarjeta;
using XCommerce.Servicio.Core.Tarjeta.DTOs;

namespace Presentacion.Core.VentaSalon
{
    public partial class _00048_CobroMesa : Form
    {
        private ComprobanteMesaDto _comprobante = new ComprobanteMesaDto();
        private readonly IComprobanteSalonServicio _comprobanteSalonServicio;
        private readonly ITarjetaServicio _TarjetaServicio = new TarjetaServicio();
        private readonly IPlanTarjeta _PlanTarjetaServicio = new PlanTarjetaServicio();
        private readonly IBancoServicio _BancoServicio = new BancoServicio();
        private readonly IClienteServicio _clienteServicio = new ClienteServicio();

        private TipoFormaPago _tipoFormaPago;

        public _00048_CobroMesa(ComprobanteMesaDto comprobante)
        {
            _comprobanteSalonServicio = new ComprobanteSalonServicio();
            _comprobante = comprobante;
            

            InitializeComponent();


            //muestra los totales 
            CargarDatosGeneral();

        }

        private void BloquearPanelSegunOperacion(TipoFormaPago tipoFormaDePago)
        {
            switch (tipoFormaDePago)
            {


                case TipoFormaPago.Tarjeta:
                    {
                        pnlCheque.Enabled = false;
                        pnlCtaCte.Enabled = false;
                        pnlTarjeta.Enabled = true;
                        pnlGeneral.Enabled = true;

                        txtCupon.KeyPress += Validacion.NoLetras;
                        txtCupon.KeyPress += Validacion.NoSimbolos;
                        txtNumeroTarjeta.KeyPress += Validacion.NoSimbolos;

                        CargarDatosTarjeta();
                        break;
                    }
                case TipoFormaPago.CuentaCorriente:
                    {
                        pnlCheque.Enabled = false;
                        pnlCtaCte.Enabled = true;
                        pnlTarjeta.Enabled = false;
                        pnlGeneral.Enabled = true;
                        txtCuil.KeyPress += Validacion.NoLetras;
                        txtCuil.KeyPress += Validacion.NoSimbolos;


                        CargarDatosCtaCte();

                        break;
                    }
                case TipoFormaPago.Cheque:
                    {
                        pnlCheque.Enabled = true;
                        pnlCtaCte.Enabled = false;
                        pnlTarjeta.Enabled = false;
                        pnlGeneral.Enabled = true;
                        txtEmisor.KeyPress += Validacion.NoSimbolos;
                        txtEmisor.KeyPress += Validacion.NoNumeros;
                        txtnumeroCheque.KeyPress += Validacion.NoSimbolos;
                        txtnumeroCheque.KeyPress += Validacion.NoLetras;
                        CargarDatosCheque();
                        break;
                    }
                case TipoFormaPago.Efectivo:
                    {
                        pnlCheque.Enabled = false;
                        pnlCtaCte.Enabled = false;
                        pnlTarjeta.Enabled = false;
                        pnlGeneral.Enabled = true;
                        break;
                    }
                default:
                    this.Close();
                    break;
            }
        }

        private void CargarDatosCheque()
        {
            CargarDatosGeneral();

            CargarComboBox(cmbBanco, _BancoServicio.Obtener(string.Empty), "Descripcion", "Id");
        }

        private void CargarDatosCtaCte()
        {
            CargarDatosGeneral();
        }

        private void CargarDatosTarjeta()
        {
            CargarDatosGeneral();
            CargarComboBox(cmbTarjeta, _TarjetaServicio.Obtener(string.Empty), "Descripcion", "Id");
            CargarComboBox(cmbPlanTarjeta, _PlanTarjetaServicio.Obtener(string.Empty), "Descripcion", "Id");
        }

        private void CargarDatosGeneral()
        {
            nudSubTotal.Value = _comprobante.SubTotal;
            nudTotal.Value = _comprobante.Total;
            nudDescuento.Value = _comprobante.Descuento;
        }

        private void nudDescuento_ValueChanged(object sender, EventArgs e)
        {
            _comprobanteSalonServicio.ActualizarMontos(_comprobante.Id, _comprobante.SubTotal, _comprobante.Descuento);

            nudTotal.Value = _comprobante.SubTotal - (_comprobante.SubTotal * nudDescuento.Value / 100);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CargarComboBox(ComboBox cmb, object datos, string propiedadMostrar,
           string propiedadDevolver)
        {
            cmb.DataSource = datos;
            cmb.DisplayMember = propiedadMostrar;
            cmb.ValueMember = propiedadDevolver;
        }

        private void cmbTarjeta_SelectedIndexChanged(object sender, EventArgs e)
        {
            var tarjetaSelecId = ((TarjetaDto)cmbTarjeta.SelectedItem).Id;

            CargarComboBox(cmbPlanTarjeta, _PlanTarjetaServicio.ObtenerSegunTarjetaId(tarjetaSelecId), "Descripcion", "Id");
        }

        private void btnNuevaTarjeta_Click(object sender, EventArgs e)
        {
            var fNuevaTarjeta = new _00059_TarjetaABM(TipoOperacion.Nuevo);
            fNuevaTarjeta.ShowDialog();

            CargarDatosTarjeta();
        }

        private void btnNuevoPlanTarjeta_Click(object sender, EventArgs e)
        {
            var fNuevoPlanTarjeta = new _00061_PlanTarjetaABM(TipoOperacion.Nuevo);
            fNuevoPlanTarjeta.ShowDialog();
            CargarDatosTarjeta();
        }

        private void btnBanco_Click(object sender, EventArgs e)
        {
            var fBancoABM = new _00022_ABM_Banco(TipoOperacion.Nuevo);
            fBancoABM.ShowDialog();
            CargarDatosCheque();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            var fBuscarCtaCte = new _00067_BuscarCtaCte();
            fBuscarCtaCte.ShowDialog();

            var cliente = _clienteServicio.ObtenerPorId(_clienteServicio.ObtenerClienteCtaCte());

            if (cliente == null) return;

            txtCuil.Text = cliente.Cuil;
            txtNombre.Text = cliente.ApyNom;
        }


        private void _00048_CobroMesa_Load(object sender, EventArgs e)
        {
            CargarComboFormaPago();

            CargarComboTipoComprobante();
        }

        private void CargarComboTipoComprobante()
        {
            cmbTipoComprobante.Items.Clear();

            cmbTipoComprobante.Items.Add(TipoComprobante.A);
            cmbTipoComprobante.Items.Add(TipoComprobante.B);
            cmbTipoComprobante.Items.Add(TipoComprobante.C);
            cmbTipoComprobante.Items.Add(TipoComprobante.X);

            cmbTipoComprobante.SelectedIndex = 0;
        }

        private void CargarComboFormaPago()
        {
            cmbFormaPago.Items.Clear();

            cmbFormaPago.Items.Add(TipoFormaPago.Efectivo);
            cmbFormaPago.Items.Add(TipoFormaPago.CuentaCorriente);
            cmbFormaPago.Items.Add(TipoFormaPago.Tarjeta);
            cmbFormaPago.Items.Add(TipoFormaPago.Cheque);

            cmbFormaPago.SelectedIndex = 0;
        }

        private void cmbFormaPago_SelectedIndexChanged_2(object sender, EventArgs e)
        {
            var tipoFormaPago = (TipoFormaPago)cmbFormaPago.SelectedItem;

            BloquearPanelSegunOperacion(tipoFormaPago);

            _tipoFormaPago = tipoFormaPago;
        }

        private void btnPagar_Click(object sender, EventArgs e)
        {
            switch (_tipoFormaPago)
            {
                case TipoFormaPago.Tarjeta:
                    {
                        _comprobanteSalonServicio.FormaPagoTarjeta(
                            ((TarjetaDto)cmbTarjeta.SelectedItem).Id, ((PlanTarjetaDto)cmbPlanTarjeta.SelectedItem).Id,
                            txtNumeroTicket.Text, txtCupon.Text, txtNumeroTarjeta.Text, _comprobante);
                        break;
                    }
                case TipoFormaPago.CuentaCorriente:
                    {
                        if (string.IsNullOrEmpty(txtCuil.Text))
                        {
                            MessageBox.Show("Por favor ingrese un codigo en Cuil");
                            return;
                        }

                        var clienteSeleccionado = _clienteServicio.ObtenerClientePorCuil(txtCuil.Text);
                        // Persistencia
                        _comprobanteSalonServicio.AsignarCliente(_comprobante.Id, clienteSeleccionado.Id);
                        // Actualizo la variable interna
                        _comprobante = _comprobanteSalonServicio.ObtenerComprobanteMesa(_comprobante.MesaId);

                        if (_clienteServicio.HaySaldoSuficiente(clienteSeleccionado.Id, _comprobante.Total) == false)
                        {
                            MessageBox.Show("El cliente No posee Saldo Suficiente para realizar la operación");
                            return;
                        }

                        _clienteServicio.AumentarSaldoCtaCte(_comprobante.ClienteId, _comprobante.Total);

                        _comprobanteSalonServicio.FormaPagoCtaCte(clienteSeleccionado.Id, _comprobante);

                        break;
                    }
                case TipoFormaPago.Cheque:
                    {
                        _comprobanteSalonServicio.FormaPagoCheque(((BancoDto)cmbBanco.SelectedItem).Id, txtEmisor.Text,
                            txtnumeroCheque.Text, Convert.ToInt32(nudDias.Value), dateTimeFechaEmision.Value, _comprobante);
                        break;
                    }
                case TipoFormaPago.Efectivo:
                    {
                        _comprobanteSalonServicio.FormaPagoEfectivo(_comprobante);
                        break;
                    }
                default:
                    MessageBox.Show("Ocurrio un Error");
                    break;
            }



            _comprobanteSalonServicio.ActualizarMontos(_comprobante.Id, nudSubTotal.Value, nudDescuento.Value);

            _comprobanteSalonServicio.PagarComprobante(_comprobante, _tipoFormaPago, (TipoComprobante)cmbTipoComprobante.SelectedItem);

            MessageBox.Show("Ha sido Pagado");

            this.Close();
        }

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
