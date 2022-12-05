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
using XCommerce.Servicio.Core.BajaArticulo;
using XCommerce.Servicio.Core.BajaArticulo.DTOs;

namespace Presentacion.Core.ArticuloBaja
{
    public partial class _00029_BajaArticuloConsulta : FormularioConsulta
    {

        private readonly IBajaArticuloServicio _BajaArtServicio;

        public _00029_BajaArticuloConsulta()
            : this(new BajaArticuloServicio())
        {
            InitializeComponent();

            
        }



        public _00029_BajaArticuloConsulta(IBajaArticuloServicio BajaArtServicio)
        {
            _BajaArtServicio = BajaArtServicio;
        }

        public override void FormatearGrilla(DataGridView grilla)
        {
            base.FormatearGrilla(grilla);


            grilla.Columns["Fecha"].Visible = true;
            grilla.Columns["Fecha"].Width = 100;
            grilla.Columns["Fecha"].HeaderText = @"Fecha";
            grilla.Columns["Fecha"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            grilla.Columns["Cantidad"].Visible = true;
            grilla.Columns["Cantidad"].Width = 150;
            grilla.Columns["Cantidad"].HeaderText = @"Cantidad";
            grilla.Columns["Cantidad"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            grilla.Columns["Observacion"].Visible = true;
            grilla.Columns["Observacion"].Width = 100;
            grilla.Columns["Observacion"].HeaderText = @"Observacion";
            grilla.Columns["Observacion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grilla.Columns["Observacion"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            grilla.Columns["EstaEliminadoStr"].Visible = true;
            grilla.Columns["EstaEliminadoStr"].Width = 100;
            grilla.Columns["EstaEliminadoStr"].HeaderText = @"Eliminado";
            grilla.Columns["EstaEliminadoStr"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grilla.Columns["EstaEliminadoStr"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }
        public override void ActualizarDatos(DataGridView grilla, string cadenaBuscar)
        {
            grilla.DataSource = _BajaArtServicio.Obtener(cadenaBuscar);
        }
        public override void EjecutarNuevo()
        {
            var fBajaArticuloABM = new _00030_ABM_BajaArticulo(TipoOperacion.Nuevo);
            fBajaArticuloABM.ShowDialog();

            ActualizarSegunOperacion(fBajaArticuloABM.RealizoAlgunaOperacion);
        }

     
        public override void EjecutarEliminar()
        {

            base.EjecutarEliminar();

            if (!PuedeEjecutarComando) return;

            if (!((BajaArticuloDto)EntidadSeleccionada).EstaEliminado)
            {

                var fBajaArticuloABM = new _00030_ABM_BajaArticulo(TipoOperacion.Eliminar, EntidadId);

                fBajaArticuloABM.ShowDialog();

                ActualizarSegunOperacion(fBajaArticuloABM.RealizoAlgunaOperacion);
            }
            else
            {
                MessageBox.Show(@"El Articulo esta Eliminado", @"Atención", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
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
