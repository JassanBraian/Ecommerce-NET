using System.Windows.Forms;
using Presentacion.FormularioBase;
using Presentacion.Helpers;
using XCommerce.Servicio.Core.Cliente;
using XCommerce.Servicio.Core.Cliente.DTOs;

namespace Presentacion.Core.Cliente
{
    public partial class _00003_Clientes : FormularioConsulta
    {
        private readonly IClienteServicio _empleadoServicio;

        public _00003_Clientes()
            : this(new ClienteServicio())
        {
            InitializeComponent();
        }

        public _00003_Clientes(IClienteServicio empleadoServicio)
        {
            _empleadoServicio = empleadoServicio;
        }

        public override void FormatearGrilla(DataGridView grilla)
        {
            base.FormatearGrilla(grilla);

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

            grilla.Columns["MontoMaximoCtaCte"].Visible = true;
            grilla.Columns["MontoMaximoCtaCte"].Width = 150;
            grilla.Columns["MontoMaximoCtaCte"].HeaderText = @"Monto Cta Cte";
            grilla.Columns["MontoMaximoCtaCte"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            grilla.Columns["SaldoCtaCte"].Visible = true;
            grilla.Columns["SaldoCtaCte"].Width = 150;
            grilla.Columns["SaldoCtaCte"].HeaderText = @"Saldo Cta Cte";
            grilla.Columns["SaldoCtaCte"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

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
            var fClienteAbm = new _00004_ABM_Cliente(TipoOperacion.Nuevo);
            fClienteAbm.ShowDialog();

            ActualizarSegunOperacion(fClienteAbm.RealizoAlgunaOperacion);
        }

        public override void EjecutarModificar()
        {
            base.EjecutarModificar();

            if (!PuedeEjecutarComando) return;

            if (!((ClienteDto)EntidadSeleccionada).EstaEliminado)
            {

                var fClienteAbm = new _00004_ABM_Cliente(TipoOperacion.Modificar, EntidadId);
                fClienteAbm.ShowDialog();

                ActualizarSegunOperacion(fClienteAbm.RealizoAlgunaOperacion);
            }
            else
            {
                MessageBox.Show(@"El empleado se encuetra Modificado", @"Atención", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        public override void EjecutarEliminar()
        {

            base.EjecutarEliminar();

            if (!PuedeEjecutarComando) return;


            if (!((ClienteDto)EntidadSeleccionada).EstaEliminado)
            {
                

                var fClienteAbm = new _00004_ABM_Cliente(TipoOperacion.Eliminar, EntidadId);

                fClienteAbm.ShowDialog();

                ActualizarSegunOperacion(fClienteAbm.RealizoAlgunaOperacion);
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
