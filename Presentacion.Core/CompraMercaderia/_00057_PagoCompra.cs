using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XCommerce.Servicio.Core.ComprobanteCompra;
using XCommerce.Servicio.Core.ComprobanteCompra.DTOs;

namespace Presentacion.Core.CompraMercaderia
{
    public partial class _00057_PagoCompra : Form
    {
        private ComprobanteCompraDto _comprobante = new ComprobanteCompraDto();
        private readonly ICompraServicio _compraServicio;

        public _00057_PagoCompra(ComprobanteCompraDto comprobanteDto)
        {
            _compraServicio = new CompraServicio();
            _comprobante = comprobanteDto;
            InitializeComponent();
            CargarDatos();
        }

        private void btnPagarFactura_Click(object sender, EventArgs e)
        {
            _compraServicio.Pagar(nudDescuento.Value, nudSubTotal.Value, nudTotal.Value, _comprobante.Id);

            _compraServicio.Total(_comprobante.Id, nudDescuento.Value);

            //  _comprobanteMesaServicio.IngresarFormaPago(_comprobante);

            MessageBox.Show("Se relizó la venta Correctamente. Muchas Gracias");

            var comprobante = _compraServicio.CrearComprobante();

            this.Close();
        }
        

        private void CargarDatos()
        {
            nudSubTotal.Value = _comprobante.SubTotal;
            nudTotal.Value = _comprobante.Total;
        }

        private void btnPagarFactura_Click_1(object sender, EventArgs e)
        {
            _compraServicio.Pagar(nudDescuento.Value, nudSubTotal.Value, nudTotal.Value, _comprobante.Id);

            _compraServicio.Total(_comprobante.Id, nudDescuento.Value);

            //  _comprobanteMesaServicio.IngresarFormaPago(_comprobante);

            MessageBox.Show("Ha sido pagado");

            this.Close();
        }
        
        
        private void nudDescuento_ValueChanged(object sender, EventArgs e)
        {
            nudTotal.Value = _comprobante.SubTotal - (_comprobante.SubTotal * nudDescuento.Value / 100);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            

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
