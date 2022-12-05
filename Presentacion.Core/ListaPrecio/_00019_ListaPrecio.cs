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
using XCommerce.Servicio.Core.ListaPrecio.DTOs;

namespace Presentacion.Core.ListaPrecio
{
    public partial class _00019_ListaPrecio : FormularioConsulta
    {
        private readonly IListaPrecioServicio _listaPrecioServicio;


        public _00019_ListaPrecio()
            : this(new ListaPrecioServicio())
        {
            InitializeComponent();
        }

        public _00019_ListaPrecio(IListaPrecioServicio listaprecioServicio)
        {
            _listaPrecioServicio = listaprecioServicio;
        }


        public override void FormatearGrilla(DataGridView grilla)
        {
            base.FormatearGrilla(grilla);

            grilla.Columns["Descripcion"].Visible = true;
            grilla.Columns["Descripcion"].Width = 100;
            grilla.Columns["Descripcion"].HeaderText = @"Descripcion";
            grilla.Columns["Descripcion"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            
            grilla.Columns["Rentabilidad"].Visible = true;
            grilla.Columns["Rentabilidad"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grilla.Columns["Rentabilidad"].HeaderText = @"Rentabilidad";
            grilla.Columns["Rentabilidad"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;


            grilla.Columns["EstaEliminadoStr"].Visible = true;
            grilla.Columns["EstaEliminadoStr"].Width = 100;
            grilla.Columns["EstaEliminadoStr"].HeaderText = @"Eliminado";
            grilla.Columns["EstaEliminadoStr"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grilla.Columns["EstaEliminadoStr"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }


        public override void ActualizarDatos(DataGridView grilla, string cadenaBuscar)
        {
            grilla.DataSource = _listaPrecioServicio.Obtener(cadenaBuscar);
        }

        public override void EjecutarNuevo()
        {
            var fListaPrecioAbm = new _00020_ABM_ListaPrecio(TipoOperacion.Nuevo);
            fListaPrecioAbm.ShowDialog();

            ActualizarSegunOperacion(fListaPrecioAbm.RealizoAlgunaOperacion);
        }

        public override void EjecutarModificar()
        {
            base.EjecutarModificar();

            if (!PuedeEjecutarComando) return;

            if (!((ListaPrecioDto)EntidadSeleccionada).EstaEliminado)
            {


                var fListaPrecioAbm = new _00020_ABM_ListaPrecio(TipoOperacion.Modificar, EntidadId);
                fListaPrecioAbm.ShowDialog();

                ActualizarSegunOperacion(fListaPrecioAbm.RealizoAlgunaOperacion);
            }
            else
            {
                MessageBox.Show(@"La Lista de Precios se encuetra Modificada", @"Atención", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }

        }

        public override void EjecutarEliminar()
        {
          

            if (EntidadSeleccionada != null)
            {
                if (!((ListaPrecioDto)EntidadSeleccionada).EstaEliminado)
                {
                    base.EjecutarEliminar();

                    if (!PuedeEjecutarComando) return;

                    var fListaPrecioAbm = new _00020_ABM_ListaPrecio(TipoOperacion.Eliminar, EntidadId);

                    fListaPrecioAbm.ShowDialog();

                    ActualizarSegunOperacion(fListaPrecioAbm.RealizoAlgunaOperacion);
                }
                else
                {
                    MessageBox.Show(@"La Lista de Precios se encuetra Elimnada", @"Atención", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
           
        }

        // ======================================================================================= //

        private void ActualizarSegunOperacion(bool realizoAlgunaOperacion)
        {
            if (realizoAlgunaOperacion)
            {
                ActualizarDatos(dgvGrilla, string.Empty);
            }
        }
    }
}

