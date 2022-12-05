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
using XCommerce.Servicio.Core.PlanTarjeta;
using XCommerce.Servicio.Core.PlanTarjeta.DTOs;

namespace Presentacion.Core.PlanTarjeta
{
    public partial class _00062_PlanTarjetaConsulta : FormularioConsulta
    {
        private readonly IPlanTarjeta _PlanTarjetaServicio;

        public _00062_PlanTarjetaConsulta()
            : this(new PlanTarjetaServicio())
        {
            InitializeComponent();
        }
        public _00062_PlanTarjetaConsulta(IPlanTarjeta _planTarjetaServicio)
        {
            _PlanTarjetaServicio = _planTarjetaServicio;
        }

        public override void FormatearGrilla(DataGridView grilla)
        {
            base.FormatearGrilla(grilla);
            grilla.Columns["Descripcion"].Visible = true;
            grilla.Columns["Descripcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grilla.Columns["Descripcion"].HeaderText = @"Tarjeta";
            grilla.Columns["Descripcion"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            grilla.Columns["Alicuota"].Visible = true;
            grilla.Columns["Alicuota"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grilla.Columns["Alicuota"].HeaderText = @"Alicuota";
            grilla.Columns["Alicuota"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;




            grilla.Columns["EstaEliminadoStr"].Visible = true;

            grilla.Columns["EstaEliminadoStr"].HeaderText = @"Eliminado";
            grilla.Columns["EstaEliminadoStr"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grilla.Columns["EstaEliminadoStr"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

        }

        public override void ActualizarDatos(DataGridView grilla, string cadenaBuscar)
        {
            grilla.DataSource = _PlanTarjetaServicio.Obtener(cadenaBuscar);
        }
        public override void EjecutarNuevo()
        {
            var fNuevoPlanTarjeta = new _00061_PlanTarjetaABM(TipoOperacion.Nuevo);
            fNuevoPlanTarjeta.ShowDialog();

            ActualizarSegunOperacion(fNuevoPlanTarjeta.RealizoAlgunaOperacion);
        }
        public override void EjecutarModificar()
        {

            if (!((PlanTarjetaDto)EntidadSeleccionada).EstaEliminado)
            {
                base.EjecutarModificar();

                if (!PuedeEjecutarComando) return;

                var fModificarPlanTarjeta = new _00061_PlanTarjetaABM(TipoOperacion.Modificar, EntidadId);

                fModificarPlanTarjeta.ShowDialog();


                ActualizarSegunOperacion(fModificarPlanTarjeta.RealizoAlgunaOperacion);
            }
            else
            {
                MessageBox.Show(@"El plan  se encuetra Elimnado", @"Atención", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }


        }
        public override void EjecutarEliminar()
        {
            if (!((PlanTarjetaDto)EntidadSeleccionada).EstaEliminado)
            {
                base.EjecutarEliminar();

                if (!PuedeEjecutarComando) return;

                var fEliminarTarjetaPlan = new _00061_PlanTarjetaABM(TipoOperacion.Eliminar, EntidadId);

                fEliminarTarjetaPlan.ShowDialog();

                ActualizarSegunOperacion(fEliminarTarjetaPlan.RealizoAlgunaOperacion);
            }
            else
            {
                MessageBox.Show(@"El plan  se encuetra Elimnado", @"Atención", MessageBoxButtons.OK,
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
