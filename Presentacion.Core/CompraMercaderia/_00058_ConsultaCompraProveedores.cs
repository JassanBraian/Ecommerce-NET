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
using XCommerce.Servicio.Core.Proveedor;

namespace Presentacion.Core.CompraMercaderia
{
    public partial class _00058_ConsultaCompraProveedores : Form
    {
        private readonly ICompraServicio _CompraServicio;

        public _00058_ConsultaCompraProveedores()
            : this(new CompraServicio())
        {
            InitializeComponent();
        }

        public _00058_ConsultaCompraProveedores(ICompraServicio _compraServicio)
        {
            _CompraServicio = _compraServicio;
        }

        public void FormatearGrilla(DataGridView grilla)
        {

            for (var i = 0; i < dgvGrillaPedidosCompra.ColumnCount; i++)
            {
                dgvGrillaPedidosCompra.Columns[i].Visible = false;
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
        }

        public void EjecutarLoadFormulario()
        {
            ActualizarDatos(dgvGrillaPedidosCompra, string.Empty);
        }

       


        public void ActualizarDatos(DataGridView grilla, string cadenaBuscar)
        {
            grilla.DataSource = _CompraServicio.ObtenerComprobanteCompra(cadenaBuscar);
        }



        private void _00058_ConsultaCompraProveedores_Load(object sender, EventArgs e)
        {
            EjecutarLoadFormulario();
            FormatearGrilla(dgvGrillaPedidosCompra);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            ActualizarDatos(dgvGrillaPedidosCompra, txtBuscar.Text);
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
