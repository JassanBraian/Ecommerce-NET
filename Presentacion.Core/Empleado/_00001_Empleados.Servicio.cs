using System.Windows.Forms;
using Presentacion.FormularioBase.helpers;

namespace Presentacion.Core.Empleado
{
    public partial class _00001_Empleados
    {
        public override void FormatearGrilla(DataGridView grilla)
        {
            base.FormatearGrilla(grilla);

            grilla.Columns["Legajo"].Visible = true;
            grilla.Columns["Legajo"].Width = 100;
            grilla.Columns["Legajo"].HeaderText = @"Legajo";

            grilla.Columns["ApyNom"].Visible = true;
            grilla.Columns["ApyNom"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grilla.Columns["ApyNom"].HeaderText = @"Apellido y Nombre";

            grilla.Columns["Dni"].Visible = true;
            grilla.Columns["Dni"].Width = 100;
            grilla.Columns["Dni"].HeaderText = @"DNI";

            grilla.Columns["Celular"].Visible = true;
            grilla.Columns["Celular"].Width = 150;
            grilla.Columns["Celular"].HeaderText = @"Celular";
        }

        public override void ActualizarDatos(DataGridView grilla, string cadenaBuscar)
        {
            grilla.DataSource = _empleadoServicio.Obtener(cadenaBuscar);
        }

        public override void EjecutarEliminar()
        {
            base.EjecutarEliminar();
            if (PuedeEjecutarComando)
            {
                var fEmpleadoAbm = new _00002_ABM_Empleados(TipoOperacion.Eliminar, EntidadId);
                fEmpleadoAbm.ShowDialog();
                ActualizarSegunOperacion(fEmpleadoAbm.RealizoAlgunaOperacion);
            }
        }

        private void ActualizarSegunOperacion(bool realizoAlgunaOperacion)
        {
            if (realizoAlgunaOperacion)
            {
                ActualizarDatos(dgvGrilla, string.Empty);
            }
        }

        public override void EjecutarModificar()
        {
            base.EjecutarModificar();
            if (PuedeEjecutarComando)
            {
                var fEmpleadoAbm = new _00002_ABM_Empleados(TipoOperacion.Modificar, EntidadId);
                fEmpleadoAbm.ShowDialog();
            }
        }

        public override void EjecutarNuevo()
        {
            var fEmpleadoAbm = new _00002_ABM_Empleados(TipoOperacion.Nuevo);
            fEmpleadoAbm.ShowDialog();
        }
    }
}
