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
using XCommerce.Servicio.Core.MotivoReserva;
using XCommerce.Servicio.Core.MotivoReserva.DTOs;

namespace Presentacion.Core.MotivoReserva
{
    public partial class _00040_MotivoReserva : FormularioConsulta
    {
        private readonly IMotivoReservaServicio _motivoreservaServicio;

        public _00040_MotivoReserva()
             : this(new MotivoReservaServicio())
        {
            InitializeComponent();
        }
        public _00040_MotivoReserva(IMotivoReservaServicio motivoreservaServicio)
        {
            _motivoreservaServicio = motivoreservaServicio;
        }

        public override void FormatearGrilla(DataGridView grilla)
        {
            base.FormatearGrilla(grilla);

            grilla.Columns["Descripcion"].Visible = true;
            grilla.Columns["Descripcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grilla.Columns["Descripcion"].HeaderText = @"Motivo de Reserva";
            grilla.Columns["Descripcion"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            grilla.Columns["EstaEliminadoStr"].Visible = true;
            grilla.Columns["EstaEliminadoStr"].Width = 100;
            grilla.Columns["EstaEliminadoStr"].HeaderText = @"Eliminado";
            grilla.Columns["EstaEliminadoStr"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grilla.Columns["EstaEliminadoStr"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        public override void ActualizarDatos(DataGridView grilla, string cadenaBuscar)
        {
            grilla.DataSource = _motivoreservaServicio.Obtener(cadenaBuscar);
        }

        public override void EjecutarNuevo()
        {
            var fEmpleadoAbm = new _00041_ABM_MotivoReserva(TipoOperacion.Nuevo);
            fEmpleadoAbm.ShowDialog();

            ActualizarSegunOperacion(fEmpleadoAbm.RealizoAlgunaOperacion);
        }

        public override void EjecutarModificar()
        {
            if (EntidadSeleccionada != null)
            {
                if (!((MotivoReservaDto)EntidadSeleccionada).EstaEliminado)
                {
                    base.EjecutarModificar();

                    if (!PuedeEjecutarComando) return;

                    var fEmpleadoAbm = new _00041_ABM_MotivoReserva(TipoOperacion.Modificar, EntidadId);
                    fEmpleadoAbm.ShowDialog();

                    ActualizarSegunOperacion(fEmpleadoAbm.RealizoAlgunaOperacion);
                }
                else
                {
                    MessageBox.Show(@"El Motivo de la Reserva esta Eliminado.", @"Atención", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }

            else
            {
                MessageBox.Show(@"No hay ningun Motivo de Reserva seleccionado", @"MODIFCAR", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
            }
        }

        public override void EjecutarEliminar()
        {
            if (EntidadSeleccionada != null)
            {
                if (!((MotivoReservaDto)EntidadSeleccionada).EstaEliminado)
                {
                    base.EjecutarEliminar();

                    if (!PuedeEjecutarComando) return;

                    var fEmpleadoAbm = new _00041_ABM_MotivoReserva(TipoOperacion.Eliminar, EntidadId);

                    fEmpleadoAbm.ShowDialog();

                    ActualizarSegunOperacion(fEmpleadoAbm.RealizoAlgunaOperacion);
                }
                else
                {
                    MessageBox.Show(@"El Motivo de la Reserva esta Eliminado.", @"Atención", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }

            else
            {
                MessageBox.Show(@"No hay ningun Motivo de Reserva seleccionado", @"ELIMINAR", MessageBoxButtons.OK,
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
