using System.Windows.Forms;
using Presentacion.FormularioBase;
using Presentacion.Helpers;
using XCommerce.Servicio.Core.Empleado;
using XCommerce.Servicio.Core.Empleado.DTOs;

namespace Presentacion.Core.Empleado
{
    public partial class _00001_Empleados : FormularioConsulta
    {
        private readonly IEmpleadoServicio _empleadoServicio;

        public _00001_Empleados()
            : this(new EmpleadoServicio())
        {
            InitializeComponent();
        }

        public _00001_Empleados(IEmpleadoServicio empleadoServicio)
        {
            _empleadoServicio = empleadoServicio;
        }

        public override void FormatearGrilla(DataGridView grilla)
        {
            base.FormatearGrilla(grilla);

            grilla.Columns["Legajo"].Visible = true;
            grilla.Columns["Legajo"].Width = 100;
            grilla.Columns["Legajo"].HeaderText = @"Legajo";
            grilla.Columns["Legajo"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            grilla.Columns["ApyNom"].Visible = true;
            grilla.Columns["ApyNom"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grilla.Columns["ApyNom"].HeaderText = @"Apellido y Nombre";
            grilla.Columns["ApyNom"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            grilla.Columns["Dni"].Visible = true;
            grilla.Columns["Dni"].Width = 100;
            grilla.Columns["Dni"].HeaderText = @"DNI";
            grilla.Columns["Dni"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            grilla.Columns["Celular"].Visible = true;
            grilla.Columns["Celular"].Width = 150;
            grilla.Columns["Celular"].HeaderText = @"Celular";
            grilla.Columns["Celular"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            grilla.Columns["EstaEliminadoStr"].Visible = true;
            grilla.Columns["EstaEliminadoStr"].Width = 100;
            grilla.Columns["EstaEliminadoStr"].HeaderText = @"Eliminado";
            grilla.Columns["EstaEliminadoStr"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grilla.Columns["EstaEliminadoStr"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        public override void ActualizarDatos(DataGridView grilla, string cadenaBuscar)
        {
            grilla.DataSource = _empleadoServicio.Obtener(cadenaBuscar);
        }

        public override void EjecutarNuevo()
        {
            var fEmpleadoAbm = new _00002_ABM_Empleados(TipoOperacion.Nuevo);
            fEmpleadoAbm.ShowDialog();

            ActualizarSegunOperacion(fEmpleadoAbm.RealizoAlgunaOperacion);
        }

        public override void EjecutarModificar()
        {
            if (!((EmpleadoDto) EntidadSeleccionada).EstaEliminado)
            {
                base.EjecutarModificar();

                if (!PuedeEjecutarComando) return;

                var fEmpleadoAbm = new _00002_ABM_Empleados(TipoOperacion.Modificar, EntidadId);
                fEmpleadoAbm.ShowDialog();

                ActualizarSegunOperacion(fEmpleadoAbm.RealizoAlgunaOperacion);

            }
            else
            {
                MessageBox.Show(@"El empleado se encuetra Elimnado", @"Atención", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        public override void EjecutarEliminar()
        {
            base.EjecutarEliminar();

            if (!PuedeEjecutarComando) return;

            if (!((EmpleadoDto) EntidadSeleccionada).EstaEliminado)
            {
               

                var fEmpleadoAbm = new _00002_ABM_Empleados(TipoOperacion.Eliminar, EntidadId);

                fEmpleadoAbm.ShowDialog();

                ActualizarSegunOperacion(fEmpleadoAbm.RealizoAlgunaOperacion);
            }
            else
            {
                MessageBox.Show(@"El empleado se encuetra Elimnado", @"Atención", MessageBoxButtons.OK,
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
