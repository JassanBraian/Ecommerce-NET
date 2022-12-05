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
using XCommerce.Servicio.Core.Producto;
using XCommerce.Servicio.Core.Producto.DTOs;

namespace Presentacion.Core.Delivery
{
    public partial class _00056_BuscarProductoDelivery : Form
    {
        private readonly IDeliveryServicio _deliveryServicio;

        protected long? productoId;

        protected object productoSeleccionado;

        protected long _comprobanteId;

        private readonly IProductoServicio _productoServicio;

        public _00056_BuscarProductoDelivery(long comprobanteId)
            :this(new ProductoServicio(), new DeliveryServicio())
        {
            InitializeComponent();
            _comprobanteId = comprobanteId;
        }

        public _00056_BuscarProductoDelivery(IProductoServicio productoServicio , IDeliveryServicio deliveryServicio)
        {
            _productoServicio = productoServicio;

            _deliveryServicio = deliveryServicio;
        }
        public void FormatearGrilla(DataGridView grilla)
        {
            for (var i = 0; i < dgvgrillaBuscarProducto.ColumnCount; i++)
            {
                dgvgrillaBuscarProducto.Columns[i].Visible = false;
            }

            grilla.Columns["Codigo"].HeaderText = @"Codigo Producto";
            grilla.Columns["Codigo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grilla.Columns["Codigo"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grilla.Columns["Codigo"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grilla.Columns["Codigo"].Visible = true;

            grilla.Columns["CodigoBarra"].HeaderText = @"Codigo Barra";
            grilla.Columns["CodigoBarra"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grilla.Columns["CodigoBarra"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grilla.Columns["CodigoBarra"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grilla.Columns["CodigoBarra"].Visible = true;

            grilla.Columns["Precio"].HeaderText = @"Precio";
            grilla.Columns["Precio"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grilla.Columns["Precio"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grilla.Columns["Precio"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grilla.Columns["Precio"].Visible = true;

            grilla.Columns["Descripcion"].HeaderText = @"Descripcion";
            grilla.Columns["Descripcion"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grilla.Columns["Descripcion"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grilla.Columns["Descripcion"].Visible = true;
        }

        public void ActualizarDatos(DataGridView grilla, string cadenaBuscar)
        {
            grilla.DataSource = _productoServicio.ObtenerParaDelivery(cadenaBuscar);
        }

        public void EjecutarLoadFormulario()
        {
            ActualizarDatos(dgvgrillaBuscarProducto, string.Empty);
        }


        private void _00056_BuscarProductoDelivery_Load(object sender, EventArgs e)
        {
            EjecutarLoadFormulario();
            FormatearGrilla(dgvgrillaBuscarProducto);
        }

        private void BtnSeleccionar_Click(object sender, EventArgs e)
        {
            // Cumple la misma funcion que Doble Clik sobre el producto

            if (productoSeleccionado == null)
            {
                MessageBox.Show(@"Por favor seleccione un registro.");
                return;
            }

            _productoServicio.AsignarProducSelecId((long)productoId);

            this.Close();
        }

        private void DgvgrillaBuscarProducto_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvgrillaBuscarProducto.RowCount > 0)
            {
                productoId = (long?)dgvgrillaBuscarProducto["Id", e.RowIndex].Value;
                productoSeleccionado = dgvgrillaBuscarProducto.Rows[e.RowIndex].DataBoundItem;
            }
            else
            {
                productoId = null;
                productoSeleccionado = null;
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            ActualizarDatos(dgvgrillaBuscarProducto, txtBuscar.Text);
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

        private void dgvgrillaBuscarProducto_DoubleClick(object sender, EventArgs e)
        {
            // Cumple la misma funcion que Seleccionar

            if (productoId == null) return;

            _productoServicio.AsignarProducSelecId((long)productoId);

            this.Close();
        }
    }
}
