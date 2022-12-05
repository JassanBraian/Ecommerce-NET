using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XCommerce.Servicio.Core.Kiosco;
using XCommerce.Servicio.Core.Producto;
using XCommerce.Servicio.Core.Producto.DTOs;

namespace Presentacion.Core.Kiosco
{
    public partial class _00051_BuscarProducto : Form
    {
        private readonly IProductoServicio _productoServicio;
        private readonly IkioscoServicio _kioscoServicio;

        protected long? productoId;

        protected object productoSeleccionado;

        protected long _comprobanteId;


        public _00051_BuscarProducto(long comprobanteId)
            : this(new ProductoServicio(), new KioscoServicio())
        {
            InitializeComponent();

            _comprobanteId = comprobanteId;
        }

        public _00051_BuscarProducto(IProductoServicio productoServicio, IkioscoServicio kioscoServicio)
        {
            _productoServicio = productoServicio;

            _kioscoServicio = kioscoServicio;

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
            grilla.DataSource = _productoServicio.ObtenerParaKiosco(cadenaBuscar);
        }

        public void EjecutarLoadFormulario()
        {
            ActualizarDatos(dgvgrillaBuscarProducto, string.Empty);
        }

        private void _00051_BuscarProducto_Load(object sender, EventArgs e)
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

       

        private void dgvgrillaBuscarProducto_RowEnter(object sender, DataGridViewCellEventArgs e)
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

        private void dgvgrillaBuscarProducto_DoubleClick(object sender, EventArgs e)
        {
            // Cumple la misma funcion que Seleccionar

            if (productoId == null) return;

            _productoServicio.AsignarProducSelecId((long)productoId);

            this.Close();
        }
    }
}
