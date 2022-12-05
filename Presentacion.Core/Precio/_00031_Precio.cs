using Presentacion.FormularioBase;
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
using XCommerce.Servicio.Core.ListaPrecio;
using XCommerce.Servicio.Core.Precio;
using XCommerce.Servicio.Core.Precio.DTOs;

namespace Presentacion.Core.Precio
{
    public partial class _00031_Precio : FormularioConsulta
    {
        private readonly IPrecioServicio _precioServicio;
        private readonly IListaPrecioServicio _listaPrecioServicio;

        public _00031_Precio()
            : this(new PrecioServicio(), new ListaPrecioServicio())
        {
            InitializeComponent();
        }

        public _00031_Precio(IPrecioServicio precioServicio, IListaPrecioServicio listaPrecioServicio)
        {
            InitializeComponent();

            _precioServicio = precioServicio;
            _listaPrecioServicio = listaPrecioServicio;

            
        }

        public override void FormatearGrilla(DataGridView grilla)
        {
            base.FormatearGrilla(grilla);
            

            grilla.Columns["Articulo"].Visible = true;
            grilla.Columns["Articulo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grilla.Columns["Articulo"].HeaderText = @"Articulo";
            grilla.Columns["Articulo"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;            
            
            grilla.Columns["PrecioPublico"].Visible = true;
            grilla.Columns["PrecioPublico"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grilla.Columns["PrecioPublico"].HeaderText = @"Precio Publico";
            grilla.Columns["PrecioPublico"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    
            
            grilla.Columns["ListaPrecio"].Visible = true;
            grilla.Columns["ListaPrecio"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grilla.Columns["ListaPrecio"].HeaderText = @"Lista de Precio";
            grilla.Columns["ListaPrecio"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            
            grilla.Columns["FechaActualizacion"].Visible = true;
            grilla.Columns["FechaActualizacion"].Width = 100;
            grilla.Columns["FechaActualizacion"].HeaderText = @"Fecha de Actualizacion";
            grilla.Columns["FechaActualizacion"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grilla.Columns["FechaActualizacion"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }
        public override void ActualizarDatos(DataGridView grilla, string cadenaBuscar)
        {
            var listaSeleccionada = cmbListaPrecio.Text;
            
            if (listaSeleccionada != " - Todas las Listas de Precios - ")
            {
                grilla.DataSource = _precioServicio.ObtenerPorListaPrecioDescrip(cadenaBuscar, listaSeleccionada);
            }
            else grilla.DataSource = _precioServicio.Obtener(cadenaBuscar);
        }

        public override void EjecutarNuevo()
        {
            var fPrecioABM = new _00032_ABM_Precio(TipoOperacion.Nuevo);
            fPrecioABM.ShowDialog();

            ActualizarSegunOperacion(fPrecioABM.RealizoAlgunaOperacion);

        }

        public override void EjecutarModificar()
        {
            if (EntidadId == null) return;

            var precio = _precioServicio.ObtenerPorId(EntidadId);

            if (precio.ListaPrecioId == 1)
            {
                MessageBox.Show("No se puede Modificar un precio de la Lista Costo");
                return;
            }


            base.EjecutarModificar();

            if (!PuedeEjecutarComando) return;

            var fPrecioABM = new _00032_ABM_Precio(TipoOperacion.Modificar, EntidadId);

            fPrecioABM.ShowDialog();

            ActualizarSegunOperacion(fPrecioABM.RealizoAlgunaOperacion);



        }

        //-----------------------------------------------------------------------//
        private void ActualizarSegunOperacion(bool realizoAlgunaOperacion)
        {
            if (realizoAlgunaOperacion)
            {
                ActualizarDatos(dgvGrilla, string.Empty);
            }
        }

        private void _00031_Precio_Activated(object sender, EventArgs e)
        {
            ActualizarDatos(dgvGrilla, string.Empty);
        }

        private void cmbListaPrecio_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarDatos(dgvGrilla, txtBuscar.Text);
        }

        private void _00031_Precio_Load(object sender, EventArgs e)
        {
            cmbListaPrecio.Items.Clear();

            cmbListaPrecio.Items.Add(" - Todas las Listas de Precios - ");

            cmbListaPrecio.Items.AddRange(_listaPrecioServicio.Obtener(string.Empty).ToArray());

            cmbListaPrecio.DisplayMember = "Descripcion";

            cmbListaPrecio.SelectedIndex = 0;
        }
    }
}