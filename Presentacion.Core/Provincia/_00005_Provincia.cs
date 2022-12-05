using Presentacion.FormularioBase;
using Presentacion.Helpers;
using System.Windows.Forms;
using XCommerce.Servicio.Core.Provincia;
using XCommerce.Servicio.Core.Provincia.DTOs;

namespace Presentacion.Core.Provincia
{
    public partial class _00005_Provincia : FormularioConsulta
    {
        private readonly IProvinciaServicio _provinciaServicio;

        public _00005_Provincia()
            : this(new ProvinciaServicio())
        {
            InitializeComponent();

        }

        public _00005_Provincia(IProvinciaServicio provinciaServicio)
        {
            _provinciaServicio = provinciaServicio;
        }

        

        public override void FormatearGrilla(DataGridView grilla)
        {
            base.FormatearGrilla(grilla);

            grilla.Columns["Descripcion"].Visible = true;
            grilla.Columns["Descripcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grilla.Columns["Descripcion"].HeaderText = @"Provincia";
            grilla.Columns["Descripcion"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            grilla.Columns["EstaEliminadoStr"].Visible = true;
            grilla.Columns["EstaEliminadoStr"].Width = 100;
            grilla.Columns["EstaEliminadoStr"].HeaderText = @"Eliminado";
            grilla.Columns["EstaEliminadoStr"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grilla.Columns["EstaEliminadoStr"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        public override void ActualizarDatos(DataGridView grilla, string cadenaBuscar)
        {
            grilla.DataSource = _provinciaServicio.Obtener(cadenaBuscar);
        }

        public override void EjecutarNuevo()
        {
            var fEmpleadoAbm = new _00006_Provincia_ABM(TipoOperacion.Nuevo);
            fEmpleadoAbm.ShowDialog();

            ActualizarSegunOperacion(fEmpleadoAbm.RealizoAlgunaOperacion);
        }

        public override void EjecutarModificar()
        {

            if (!((ProvinciaDto)EntidadSeleccionada).EstaEliminado)
            {
                base.EjecutarModificar();

                if (!PuedeEjecutarComando) return;

                var fEmpleadoAbm = new _00006_Provincia_ABM(TipoOperacion.Modificar, EntidadId);
                
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
            if (!((ProvinciaDto)EntidadSeleccionada).EstaEliminado)
            {
                base.EjecutarEliminar();

                if (!PuedeEjecutarComando) return;

                var fEmpleadoAbm = new _00006_Provincia_ABM(TipoOperacion.Eliminar, EntidadId);

                fEmpleadoAbm.ShowDialog();

                ActualizarSegunOperacion(fEmpleadoAbm.RealizoAlgunaOperacion);
            }
            else
            {
                MessageBox.Show(@"La Provincia se encuetra Elimnado", @"Atención", MessageBoxButtons.OK,
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
