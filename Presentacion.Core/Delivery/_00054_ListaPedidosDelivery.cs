using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XCommerce.Servicio.Core.Delivery;
using XCommerce.Servicio.Core.Delivery.DTOs;

namespace Presentacion.Core.Delivery
{
    public partial class _00054_ListaPedidosDelivery : Form
    {

        private readonly IDeliveryServicio _DeliveryServicio;

        protected long? pedidoId;

        protected object pedidoSeleccionado;

        protected object _pedidoSeleccionado;

        public _00054_ListaPedidosDelivery()
            : this(new DeliveryServicio())
        {
            InitializeComponent();
            btnNuevo.Focus();
        }

        public _00054_ListaPedidosDelivery(IDeliveryServicio deliveryServicio)
        {

            _DeliveryServicio = deliveryServicio;

        }

        public void FormatearGrilla(DataGridView grilla)
        {

            for (var i = 0; i < dgvGrillaPedidos.ColumnCount; i++)
            {
                dgvGrillaPedidos.Columns[i].Visible = false;
            }

            grilla.Columns["Numero"].Visible = true;
            grilla.Columns["Numero"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grilla.Columns["Numero"].HeaderText = @"Numero Factura";
            grilla.Columns["Numero"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            grilla.Columns["Fecha"].Visible = true;
            grilla.Columns["Fecha"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grilla.Columns["Fecha"].HeaderText = @"Fecha";
            grilla.Columns["Fecha"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            grilla.Columns["SubTotal"].Visible = true;
            grilla.Columns["SubTotal"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grilla.Columns["SubTotal"].HeaderText = @"SubTotal";
            grilla.Columns["SubTotal"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            grilla.Columns["Descuento"].Visible = true;
            grilla.Columns["Descuento"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grilla.Columns["Descuento"].HeaderText = @"Descuento";
            grilla.Columns["Descuento"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            grilla.Columns["Total"].Visible = true;
            grilla.Columns["Total"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grilla.Columns["Total"].HeaderText = @"Total";
            grilla.Columns["Total"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            grilla.Columns["TipoComprobante"].Visible = true;
            grilla.Columns["TipoComprobante"].HeaderText = @"Tipo Comprobante";
            grilla.Columns["TipoComprobante"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grilla.Columns["TipoComprobante"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            grilla.Columns["EstadoDelivery"].Visible = true;
            grilla.Columns["EstadoDelivery"].HeaderText = @"Estado";
            grilla.Columns["EstadoDelivery"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grilla.Columns["EstadoDelivery"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        public void ActualizarDatos(DataGridView grilla, string cadenaBuscar)
        {
            grilla.DataSource = _DeliveryServicio.Obtener(cadenaBuscar);
        }

        public void EjecutarLoadFormulario()
        {
            ActualizarDatos(dgvGrillaPedidos, string.Empty);
        }        

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            var nuevoPedido = new _00053_Delivery();
            nuevoPedido.ShowDialog();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (_pedidoSeleccionado == null)
            {
                MessageBox.Show(@"Por favor seleccione un registro.");

            }
            else
            {
                //_ComprobanteSalonServicio.ObtenerMozoNuevo(((EmpleadoDto)mozoSeleccionado), _comprobanteId);
                // pasar el pedido seleccionado a la pantalla de edicion
                var fEditarDelivery = new _00053_Delivery(((DeliveryDto)pedidoSeleccionado).Id);
                fEditarDelivery.ShowDialog();
                
            }
        }

        private void _00053_ListaPedidosDelivery_Load(object sender, EventArgs e)
        {
            EjecutarLoadFormulario();
            FormatearGrilla(dgvGrillaPedidos);
        }

        private void dgvGrillaPedidos_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvGrillaPedidos.RowCount > 0)
            {
                pedidoId = (long?)dgvGrillaPedidos["Id", e.RowIndex].Value;
                pedidoSeleccionado = dgvGrillaPedidos.Rows[e.RowIndex].DataBoundItem;



            }
            else
            {
                pedidoId = null;
                pedidoSeleccionado = null;
            }
        }

        private void _00054_ListaPedidosDelivery_Activated(object sender, EventArgs e)
        {
            EjecutarLoadFormulario();
            FormatearGrilla(dgvGrillaPedidos);
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
