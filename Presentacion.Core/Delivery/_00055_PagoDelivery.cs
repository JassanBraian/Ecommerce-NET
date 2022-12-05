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
using XCommerce.Servicio.Core.Delivery;
using XCommerce.Servicio.Core.Delivery.DTOs;
using XCommerce.Servicio.Core.PlanTarjeta;
using XCommerce.Servicio.Core.PlanTarjeta.DTOs;
using XCommerce.Servicio.Core.Tarjeta;
using XCommerce.Servicio.Core.Tarjeta.DTOs;

namespace Presentacion.Core.Delivery
{
    public partial class _00055_PagoDelivery : Form
    {
        private readonly IDeliveryServicio _deliveryServicio;
        private DeliveryDto _comprobante = new DeliveryDto();
        private readonly ITarjetaServicio _TarjetaServicio = new TarjetaServicio();
        private readonly IPlanTarjeta _PlanTarjetaServicio = new PlanTarjetaServicio();
        private readonly IBancoServicio _BancoServicio = new BancoServicio();
        private readonly IClienteServicio _clienteServicio = new ClienteServicio();

        private TipoFormaPago _tipoFormaPago;

        public _00055_PagoDelivery(DeliveryDto comprobante)
        {          

            _deliveryServicio = new DeliveryServicio();
            _comprobante = comprobante;

            _tipoFormaPago = TipoFormaPago.Efectivo;

            InitializeComponent();

            BloquearPanelSegunOperacion(_tipoFormaPago);

            //muestra los totales 
            CargarDatosGeneral();

            nudDescuento.KeyPress += Validacion.NoLetras;
            nudDescuento.KeyPress += Validacion.NoSimbolos;
            nudDescuento.KeyPress += Validacion.NoNumeros;

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
                        txtCupon.KeyPress += Validacion.NoLetras;
                        txtCupon.KeyPress += Validacion.NoSimbolos;
                        txtNumeroTarjeta.KeyPress += Validacion.NoSimbolos;
                        txtNumeroTarjeta.KeyPress += Validacion.NoLetras;
                        txtNumeroTicket.KeyPress += Validacion.NoLetras;
                        txtNumeroTicket.KeyPress += Validacion.NoSimbolos; 
                        CargarDatosTarjeta();
                        break;
                    }
                case TipoFormaPago.CuentaCorriente:
                    {
                        pnlCheque.Enabled = false;
                        pnlCtaCte.Enabled = true;
                        pnlTarjeta.Enabled = false;
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
                        break;
                    }
                default:
                    this.Close();
                    break;
            }
            
        }
        private void CargarDatosGeneral()
        {
            nudSubTotal.Value = _comprobante.SubTotal;
            nudTotal.Value = _comprobante.Total;
            nudDescuento.Value = _comprobante.Descuento;

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

        private void CargarComboBox(ComboBox cmb, object datos, string propiedadMostrar,
          string propiedadDevolver)
        {
            cmb.DataSource = datos;
            cmb.DisplayMember = propiedadMostrar;
            cmb.ValueMember = propiedadDevolver;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                DialogResult result = MessageBox.Show("Esta seguro que desea Salir?", "Salir", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    this.Close();
                }
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btnNuevaTarjeta_Click(object sender, EventArgs e)
        {
            var fNuevaTarjeta = new _00059_TarjetaABM(TipoOperacion.Nuevo);
            fNuevaTarjeta.ShowDialog();

            CargarDatosTarjeta();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNuevoPlanTarjeta_Click_1(object sender, EventArgs e)
        {
            var fNuevoPlanTarjeta = new _00061_PlanTarjetaABM(TipoOperacion.Nuevo);
            fNuevoPlanTarjeta.ShowDialog();
            CargarDatosTarjeta();
        }

        private void btnBanco_Click_1(object sender, EventArgs e)
        {
            var fNuevoPlanTarjeta = new _00061_PlanTarjetaABM(TipoOperacion.Nuevo);
            fNuevoPlanTarjeta.ShowDialog();
            CargarDatosTarjeta();
        }

        private void cmbTarjeta_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            var tarjetaSelecId = ((TarjetaDto)cmbTarjeta.SelectedItem).Id;

            CargarComboBox(cmbPlanTarjeta, _PlanTarjetaServicio.ObtenerSegunTarjetaId(tarjetaSelecId), "Descripcion", "Id");
        }

        private void btnPagar_Click(object sender, EventArgs e)
        {
            // Validaciones //

            switch (_tipoFormaPago)
            {
                case TipoFormaPago.Tarjeta:
                    {
                        if (string.IsNullOrEmpty(txtNumeroTicket.Text))
                        {
                            MessageBox.Show("Por favor ingrese un codigo en Numero Ticket");
                            return;
                        }

                        if (string.IsNullOrEmpty(txtCupon.Text))
                        {
                            MessageBox.Show("Por favor ingrese un codigo en Numero Cupon");
                            return;
                        }

                        if (string.IsNullOrEmpty(txtNumeroTarjeta.Text))
                        {
                            MessageBox.Show("Por favor ingrese un codigo Numero Tarjeta");
                            return;
                        }

                        break;
                    }
                case TipoFormaPago.CuentaCorriente:
                    {

                        if (string.IsNullOrEmpty(txtCuil.Text))
                        {
                            MessageBox.Show("Por favor ingrese un codigo en Cuil");
                            return;
                        }

                        var cliente = _clienteServicio.ObtenerClientePorCuil(txtCuil.Text);

                        _comprobante.Clienteid = cliente.Id;

                        if (_clienteServicio.HaySaldoSuficiente(_comprobante.Clienteid, nudTotal.Value))
                        {
                            // Continua sin realizar operacion

                        }
                        else
                        {
                            MessageBox.Show("El cliente No posee Saldo Suficiente");
                            return;
                        }

                        break;
                    }
                case TipoFormaPago.Cheque:
                    {
                        if (string.IsNullOrEmpty(txtnumeroCheque.Text))
                        {
                            MessageBox.Show("Por favor ingrese un codigo en Numero Cheque");
                            return;
                        }
                        if (string.IsNullOrEmpty(txtEmisor.Text))
                        {
                            MessageBox.Show("Por favor ingrese un Emisor");
                            return;
                        }

                        break;
                    }
                case TipoFormaPago.Efectivo:
                    {
                        // Continua sin realizar operacion

                        break;
                    }
                default:
                    MessageBox.Show("Ocurrio un Error");
                    break;
            }

            // Persistencia //

            _deliveryServicio.Pagar(nudDescuento.Value, nudSubTotal.Value, nudTotal.Value, _comprobante.Id,
                _tipoFormaPago, (TipoComprobante)cmbTipoComprobante.SelectedItem);

            //_deliveryServicio.Total(_comprobante.Id, nudDescuento.Value);

            switch (_tipoFormaPago)
            {
                case TipoFormaPago.Tarjeta:
                    {
                        _deliveryServicio.FormaPagoTarjeta(
                            ((TarjetaDto)cmbTarjeta.SelectedItem).Id, ((PlanTarjetaDto)cmbPlanTarjeta.SelectedItem).Id,
                            txtNumeroTicket.Text, txtCupon.Text, txtNumeroTarjeta.Text);

                        break;
                    }
                case TipoFormaPago.CuentaCorriente:
                    {
                        _deliveryServicio.FormaPagoCtaCte(_comprobante.Clienteid);

                        _clienteServicio.AumentarSaldoCtaCte(_comprobante.Clienteid, _comprobante.Total);

                        break;
                    }
                case TipoFormaPago.Cheque:
                    {
                        _deliveryServicio.FormaPagoCheque(((BancoDto)cmbBanco.SelectedItem).Id, txtEmisor.Text,
                            txtnumeroCheque.Text, Convert.ToInt32(nudDias.Value), dateTimeFechaEmision.Value);

                        break;
                    }
                case TipoFormaPago.Efectivo:
                    {
                        _deliveryServicio.FormaPagoEfectivo();

                        break;
                    }
                default:
                    MessageBox.Show("Ocurrio un Error");
                    break;
            }

            MessageBox.Show("Se relizó la venta correctamente. Muchas Gracias");

            this.Close();

            var comprobanteId = _deliveryServicio.CrearComprobante();

        }

        private void nudDescuento_ValueChanged_1(object sender, EventArgs e)
        {
            nudTotal.Value = _comprobante.SubTotal - (_comprobante.SubTotal * nudDescuento.Value / 100);

            var comprobante = _deliveryServicio.ObtenerUltimoComprobante();

            comprobante.Descuento = nudDescuento.Value;
        }

        private void btnBuscar_Click_1(object sender, EventArgs e)
        {
            var fBuscarCtaCte = new _00067_BuscarCtaCte();
            fBuscarCtaCte.ShowDialog();

            var cliente = _clienteServicio.ObtenerPorId(_clienteServicio.ObtenerClienteCtaCte());

            if (cliente == null) return;

            txtCuil.Text = cliente.Cuil;
            txtNombre.Text = cliente.ApyNom;
        }

        private void cmbFormaPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            var tipoFormaPago = (TipoFormaPago)cmbFormaPago.SelectedItem;

            BloquearPanelSegunOperacion(tipoFormaPago);

            _tipoFormaPago = tipoFormaPago;
        }

        private void _00055_PagoDelivery_Load(object sender, EventArgs e)
        {
            CargarCmbFormaPago();

            CargarCmbTipoComprobante();
        }

        private void CargarCmbTipoComprobante()
        {
            cmbTipoComprobante.Items.Clear();

            cmbTipoComprobante.Items.Add(TipoComprobante.A);
            cmbTipoComprobante.Items.Add(TipoComprobante.B);
            cmbTipoComprobante.Items.Add(TipoComprobante.C);
            cmbTipoComprobante.Items.Add(TipoComprobante.X);

            cmbTipoComprobante.SelectedIndex = 0;
        }

        private void CargarCmbFormaPago()
        {
            cmbFormaPago.Items.Clear();

            cmbFormaPago.Items.Add(TipoFormaPago.Efectivo);
            cmbFormaPago.Items.Add(TipoFormaPago.CuentaCorriente);
            cmbFormaPago.Items.Add(TipoFormaPago.Tarjeta);
            cmbFormaPago.Items.Add(TipoFormaPago.Cheque);

            cmbFormaPago.SelectedIndex = 0;
        }
    }
    
}
