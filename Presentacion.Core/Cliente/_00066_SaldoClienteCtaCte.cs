using Presentacion.Helpers;
using Servicios.Core.Movimiento;
using Servicios.Core.Movimiento.DTOs;
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
using XCommerce.Servicio.Core.Caja;
using XCommerce.Servicio.Core.Caja.DTOs;
using XCommerce.Servicio.Core.Cliente;
using XCommerce.Servicio.Core.ComprobanteCtaCte;
using XCommerce.Servicio.Core.ComprobanteCtaCte.DTOs;
using XCommerce.Servicio.Core.FormaDePago;
using XCommerce.Servicio.Core.FormaDePago.DTOs;
using XCommerce.Servicio.Core.Kiosco.DTOs;

namespace Presentacion.Core.Cliente
{
    public partial class _00066_SaldoClienteCtaCte : Form
    {
        IClienteServicio _clienteServicio = new ClienteServicio();
        ICajaServicio _cajaServicio = new CajaServicio();
        IMovimientoServicio _movimientoServicio = new MovimientoServicio();
        IComprobanteCtaCteServicio _comprobanteCtaCteServicio = new ComprobanteCtaCteServicio();
        IFormaDePago _formaPagoServicio = new FormaDePagoServicio();

        public _00066_SaldoClienteCtaCte()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            var fBuscarCtaCte = new _00067_BuscarCtaCte();
            fBuscarCtaCte.ShowDialog();

            var cliente = _clienteServicio.ObtenerPorId(_clienteServicio.ObtenerClienteCtaCte());

            if (cliente == null) return;

            txtCuil.Text = cliente.Cuil;
            txtNombre.Text = cliente.ApyNom;
            nudSaldoActual.Value = cliente.SaldoCtaCte;
            nudSaldoMax.Value = cliente.MontoMaximoCtaCte;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCuil.Text))
            {
                MessageBox.Show("Por favor ingrese un codigo en Cuil");
                return;
            }

            var cliente = _clienteServicio.ObtenerClientePorCuil(txtCuil.Text);

            var caja = _cajaServicio.ObtenerCajaAbierta();
            if(caja == null)
            {
                MessageBox.Show("Debe Abrir Caja antes de realizar esta Operacion");
                return;
            }

            _clienteServicio.PagarSaldoCtaCte(cliente.Id, nudCargarSaldo.Value);
            
            _cajaServicio.AumentarMontoSistema(caja.Id, nudCargarSaldo.Value);

            var comprobanteNuevo = new ComprobanteCtaCteDto()  // trabajo con dto de comprobante kiosco para poder conseguir el comprobanteId para Movimiento
            {
                Numero = _comprobanteCtaCteServicio.GenerarNumeroFactura(),
                Fecha = DateTime.Now,
                SubTotal = nudCargarSaldo.Value,
                Descuento = 0,
                Total = nudCargarSaldo.Value,
                UsuarioId = Validacion.UsuarioLogeado,
                Clienteid = cliente.Id,
                TipoComprobante = TipoComprobante.A
            };

            var comprobanteNuevoId = _comprobanteCtaCteServicio.Insertar(comprobanteNuevo);
            
            var nuevoMovimiento = new MovimientoDto()
            {
                Fecha = DateTime.Now,
                CajaId = _cajaServicio.ObtenerCajaAbierta().Id,
                ComprobanteId = comprobanteNuevoId,
                Descripcion = "Pago de Saldo CtaCte",
                Monto = nudCargarSaldo.Value,
                TipoMovimiento = TipoMovimiento.Ingreso,
                UsuarioId = Validacion.UsuarioLogeado
            };
            _movimientoServicio.Insertar(nuevoMovimiento);

            var nuevaFormaPago = new FormaDePagoEfectivoDto()
            {
                ComprobanteId = comprobanteNuevoId,
                Monto = nudCargarSaldo.Value,
                TipoFormaDePago = TipoFormaPago.Efectivo,
            };
            _formaPagoServicio.Insertar(nuevaFormaPago);

            MessageBox.Show("Se ha realizado la carga Correctamente");

            this.Close();
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
    }
}
