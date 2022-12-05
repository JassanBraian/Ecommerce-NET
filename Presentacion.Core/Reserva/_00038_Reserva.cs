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
using XCommerce.Servicio.Core.Reserva;
using XCommerce.Servicio.Core.Reserva.DTOs;

namespace Presentacion.Core.Reserva
{
    public partial class _00038_Reserva : FormularioConsulta
    {

        private readonly IReservaServicio _reservaServicio;

        public _00038_Reserva()
             : this(new ReservaServicio())
        {
            InitializeComponent();
        }
        public _00038_Reserva(IReservaServicio reservaServicio)
        {
            _reservaServicio = reservaServicio;

        }

        public override void FormatearGrilla(DataGridView grilla)
        {
            base.FormatearGrilla(grilla);

            grilla.Columns["Mesa"].Visible = true;
            grilla.Columns["Mesa"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grilla.Columns["Mesa"].HeaderText = @"Numero de Mesa";
            grilla.Columns["Mesa"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            grilla.Columns["EstadoReserva"].Visible = true;
            grilla.Columns["EstadoReserva"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grilla.Columns["EstadoReserva"].HeaderText = @"Estado de Reserva";
            grilla.Columns["EstadoReserva"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            grilla.Columns["EstaEliminadoStr"].Visible = true;
            grilla.Columns["EstaEliminadoStr"].Width = 100;
            grilla.Columns["EstaEliminadoStr"].HeaderText = @"Eliminada";
            grilla.Columns["EstaEliminadoStr"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grilla.Columns["EstaEliminadoStr"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }
        public override void ActualizarDatos(DataGridView grilla, string cadenaBuscar)
        {
            if (cmbFiltroActual.Text == "- Reservas Actuales -")
            {
                grilla.DataSource = _reservaServicio.ObtenerActuales(cadenaBuscar);
            }
            else if(cmbFiltroActual.Text == "- Reservas del Dia -")
            {
                grilla.DataSource = _reservaServicio.ObtenerDeCajaActual(cadenaBuscar);
            }
            else
            {
                grilla.DataSource = _reservaServicio.Obtener(cadenaBuscar);
            }
        }
        //------------------------------------------------------------------------------------//

        public override void EjecutarNuevo()
        {
            var fReservaAbm = new _00039_ABM_Reserva(TipoOperacion.Nuevo);
            fReservaAbm.ShowDialog();

            ActualizarSegunOperacion(fReservaAbm.RealizoAlgunaOperacion);

        }

        public override void EjecutarModificar()
        {
            base.EjecutarModificar();

            if (!PuedeEjecutarComando) return;

            if (!((ReservaDto)EntidadSeleccionada).EstaEliminado)
            {
                var fReservaAbm = new _00039_ABM_Reserva(TipoOperacion.Modificar, EntidadId);

                fReservaAbm.ShowDialog();

                ActualizarSegunOperacion(fReservaAbm.RealizoAlgunaOperacion);
            }
            else
            {
                MessageBox.Show(@"La reserva se encuetra Eliminada", @"Atención", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }


        }

        public override void EjecutarEliminar()
        {
            base.EjecutarEliminar();
            if (!PuedeEjecutarComando) return;

            if (!((ReservaDto)EntidadSeleccionada).EstaEliminado)
            {
                var fReservaAbm = new _00039_ABM_Reserva(TipoOperacion.Eliminar, EntidadId);

                fReservaAbm.ShowDialog();

                ActualizarSegunOperacion(fReservaAbm.RealizoAlgunaOperacion);
            }
            else
            {
                MessageBox.Show(@"La reserva se encunetra Eliminada", @"Atención", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }


        }

        //***************************************************************************************//
        private void ActualizarSegunOperacion(bool realizoAlgunaOperacion)
        {
            if (realizoAlgunaOperacion)
            {
                ActualizarDatos(dgvGrilla, string.Empty);
            }
        }

        private void cmbFiltroActual_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarDatos(dgvGrilla, txtBuscar.Text);
        }

        private void _00038_Reserva_Load(object sender, EventArgs e)
        {
            cmbFiltroActual.Items.Clear();
            cmbFiltroActual.Items.Add("- Reservas Actuales -");
            cmbFiltroActual.Items.Add("- Reservas del Dia -");
            cmbFiltroActual.Items.Add("- Todas las Reservas -");
            cmbFiltroActual.SelectedIndex = 0;
        }
    }
}
