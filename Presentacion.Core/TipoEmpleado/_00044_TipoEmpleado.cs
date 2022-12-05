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
using XCommerce.Servicio.Core.TipoEmpleado;
using XCommerce.Servicio.Core.TipoEmpleado.DTOs;

namespace Presentacion.Core.TipoEmpleado
{
    public partial class _00044_TipoEmpleado : FormularioConsulta
    {
        private readonly ITipoEmpleadoSericio _TipoEmpleadoServicio;

        public _00044_TipoEmpleado()
            : this(new TipoEmpleadoServicio())
        {
            InitializeComponent();
        }
        public _00044_TipoEmpleado(ITipoEmpleadoSericio TipoEmpleadoSericio)
        {
            _TipoEmpleadoServicio = TipoEmpleadoSericio;
        }

        public override void FormatearGrilla(DataGridView grilla)
        {
            base.FormatearGrilla(grilla);
            grilla.Columns["Descripcion"].Visible = true;
            grilla.Columns["Descripcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grilla.Columns["Descripcion"].HeaderText = @"Banco";
            grilla.Columns["Descripcion"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            grilla.Columns["EstaEliminadoStr"].Visible = true;

            grilla.Columns["EstaEliminadoStr"].HeaderText = @"Eliminado";
            grilla.Columns["EstaEliminadoStr"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grilla.Columns["EstaEliminadoStr"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

        }

        public override void ActualizarDatos(DataGridView grilla, string cadenaBuscar)
        {
            grilla.DataSource = _TipoEmpleadoServicio.Obtener(cadenaBuscar);
        }

        public override void EjecutarNuevo()
        {
            var fTipoEmpleadoABM = new _00045_ABM_TipoEmpleado(TipoOperacion.Nuevo);
            fTipoEmpleadoABM.ShowDialog();

            ActualizarSegunOperacion(fTipoEmpleadoABM.RealizoAlgunaOperacion);
        }
        public override void EjecutarModificar()
        {

            if (!((TipoEmpleadoDto)EntidadSeleccionada).EstaEliminado)
            {
                base.EjecutarModificar();

                if (!PuedeEjecutarComando) return;

                var fTipoEmpleadoABM = new _00045_ABM_TipoEmpleado(TipoOperacion.Modificar, EntidadId);

                fTipoEmpleadoABM.ShowDialog();


                ActualizarSegunOperacion(fTipoEmpleadoABM.RealizoAlgunaOperacion);
            }
            else
            {
                MessageBox.Show(@"El tipo empleado se encuetra Elimnado", @"Atención", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }


        }
        public override void EjecutarEliminar()
        {
            if (!((TipoEmpleadoDto)EntidadSeleccionada).EstaEliminado)
            {
                base.EjecutarEliminar();

                if (!PuedeEjecutarComando) return;

                var fTipoEmpleadoABM = new _00045_ABM_TipoEmpleado(TipoOperacion.Eliminar, EntidadId);

                fTipoEmpleadoABM.ShowDialog();

                ActualizarSegunOperacion(fTipoEmpleadoABM.RealizoAlgunaOperacion);
            }
            else
            {
                MessageBox.Show(@"El tipo empleado  se encuetra Elimnado", @"Atención", MessageBoxButtons.OK,
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
