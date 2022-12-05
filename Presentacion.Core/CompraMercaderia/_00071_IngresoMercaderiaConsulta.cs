using Presentacion.FormularioBase;
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

namespace Presentacion.Core.CompraMercaderia
{
    public partial class _00071_IngresoMercaderiaConsulta : FormularioConsulta
    {
        ICompraServicio _compraServicio = new CompraServicio();
        public _00071_IngresoMercaderiaConsulta()
        {
            InitializeComponent();

            btnNuevo.Enabled = false;
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
        }

        public override void ActualizarDatos(DataGridView grilla, string cadenaBuscar)
        {
            grilla.DataSource = _compraServicio.ObtenerComprobanteCompra(cadenaBuscar);
        }

        public override void FormatearGrilla(DataGridView grilla)
        {
            base.FormatearGrilla(grilla);

            grilla.Columns["NumeroStr"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grilla.Columns["NumeroStr"].HeaderText = @"Numero Factura";
            grilla.Columns["NumeroStr"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grilla.Columns["NumeroStr"].Visible = true;

            grilla.Columns["Fecha"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grilla.Columns["Fecha"].HeaderText = @"Fecha";
            grilla.Columns["Fecha"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grilla.Columns["Fecha"].Visible = true;

            grilla.Columns["SubTotal"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grilla.Columns["SubTotal"].HeaderText = @"SubTotal";
            grilla.Columns["SubTotal"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grilla.Columns["SubTotal"].Visible = true;

            grilla.Columns["Descuento"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grilla.Columns["Descuento"].HeaderText = @"Descuento %";
            grilla.Columns["Descuento"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grilla.Columns["Descuento"].Visible = true;

            grilla.Columns["Total"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grilla.Columns["Total"].HeaderText = @"Total";
            grilla.Columns["Total"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grilla.Columns["Total"].Visible = true;

            grilla.Columns["TipoComprobante"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grilla.Columns["TipoComprobante"].HeaderText = @"TipoComprobante";
            grilla.Columns["TipoComprobante"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grilla.Columns["TipoComprobante"].Visible = true;

            grilla.Columns["ProveedorStr"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grilla.Columns["ProveedorStr"].HeaderText = @"Proveedor";
            grilla.Columns["ProveedorStr"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grilla.Columns["ProveedorStr"].Visible = true;


        }
    }
}
