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
using XCommerce.Servicio.Core.Articulo;
using XCommerce.Servicio.Core.Articulo.DTOs;

namespace Presentacion.Core.Articulo
{
    public partial class _00010_Articulos : FormularioConsulta

    {

        private readonly IArticuloServicio _articuloServicio;

        public _00010_Articulos()
             : this(new ArticuloServicio())
        {
            InitializeComponent();
        }

        public _00010_Articulos(IArticuloServicio articuloServicio)
        {
            _articuloServicio = articuloServicio;
        }

        public override void FormatearGrilla(DataGridView grilla)
        {
            base.FormatearGrilla(grilla);

            grilla.Columns["Descripcion"].Visible = true;
            grilla.Columns["Descripcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grilla.Columns["Descripcion"].HeaderText = @"Producto";
            grilla.Columns["Descripcion"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            grilla.Columns["Codigo"].Visible = true;
            grilla.Columns["Codigo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grilla.Columns["Codigo"].HeaderText = @"Codigo";
            grilla.Columns["Codigo"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            grilla.Columns["CodigoBarra"].Visible = true;
            grilla.Columns["CodigoBarra"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grilla.Columns["CodigoBarra"].HeaderText = @"CodigoBarra";
            grilla.Columns["CodigoBarra"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            grilla.Columns["PrecioCosto"].Visible = true;
            grilla.Columns["PrecioCosto"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grilla.Columns["PrecioCosto"].HeaderText = @"Precio Costo";
            grilla.Columns["PrecioCosto"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            grilla.Columns["Stock"].Visible = true;
            grilla.Columns["Stock"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grilla.Columns["Stock"].HeaderText = @"Stock";
            grilla.Columns["Stock"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            grilla.Columns["Iva"].Visible = true;
            grilla.Columns["Iva"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grilla.Columns["Iva"].HeaderText = @"Iva";
            grilla.Columns["Iva"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            
            grilla.Columns["EstaEliminadoStr"].Visible = true;
            grilla.Columns["EstaEliminadoStr"].Width = 100;
            grilla.Columns["EstaEliminadoStr"].HeaderText = @"Eliminada";
            grilla.Columns["EstaEliminadoStr"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grilla.Columns["EstaEliminadoStr"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        public override void ActualizarDatos(DataGridView grilla, string cadenaBuscar)
        {
            grilla.DataSource = _articuloServicio.Obtener(cadenaBuscar);
        }

        public override void EjecutarNuevo()
        {
            var fArticuloABM = new _00011_ABM_Articulos(TipoOperacion.Nuevo);
            fArticuloABM.ShowDialog();

            ActualizarSegunOperacion(fArticuloABM.RealizoAlgunaOperacion);

        }

        public override void EjecutarModificar()
        {
            base.EjecutarModificar();

            if (!PuedeEjecutarComando) return;

            if (!((ArticuloDto)EntidadSeleccionada).EstaEliminado)
            {
                var fArticuloABM = new _00011_ABM_Articulos(TipoOperacion.Modificar, EntidadId);
                fArticuloABM.ShowDialog();

                ActualizarSegunOperacion(fArticuloABM.RealizoAlgunaOperacion);
            }
            else
            {
                MessageBox.Show(@"El Articulo esta Eliminado.", @"Atención", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }

        }

        public override void EjecutarEliminar()
        {

          

            if (!((ArticuloDto)EntidadSeleccionada).EstaEliminado)
            {

                base.EjecutarEliminar();

                if (!PuedeEjecutarComando) return;

                var fArticuloABM = new _00011_ABM_Articulos(TipoOperacion.Eliminar, EntidadId);

                fArticuloABM.ShowDialog();

                ActualizarSegunOperacion(fArticuloABM.RealizoAlgunaOperacion);
            }
            else
            {
                MessageBox.Show(@"El Articulo esta Eliminado.", @"Atención", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }

        }

        //----------------------------------------------------------------------//

        private void ActualizarSegunOperacion(bool realizoAlgunaOperacion)
        {
            if (realizoAlgunaOperacion)
            {
                ActualizarDatos(dgvGrilla, string.Empty);
            }
        }
    }
}
