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
using XCommerce.Servicio.Core.Cliente;
using XCommerce.Servicio.Core.ComprobanteCtaCte;

namespace Presentacion.Core.Cliente
{
    public partial class _00075_ComprobCtaCteConsulta : FormularioConsulta
    {
        IClienteServicio _clienteServicio = new ClienteServicio();
        IComprobanteCtaCteServicio _comprobanteCtaCteServicio = new ComprobanteCtaCteServicio();
        public _00075_ComprobCtaCteConsulta()
        {
            InitializeComponent();

            btnNuevo.Enabled = false;
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
        }

        public override void ActualizarDatos(DataGridView grilla, string cadenaBuscar)
        {
            grilla.DataSource = _comprobanteCtaCteServicio.ObtenerComprobanteCtaCte(cadenaBuscar);
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

            grilla.Columns["Total"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grilla.Columns["Total"].HeaderText = @"Total";
            grilla.Columns["Total"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grilla.Columns["Total"].Visible = true;


        }
    }
}
