namespace Presentacion.Core.Mesa
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using Presentacion.FormularioBase;
    using Presentacion.Helpers;
    using XCommerce.Servicio.Core.Mesa;
    using XCommerce.Servicio.Core.Mesa.DTOs;

    public partial class _00013_Mesas : FormularioConsulta
    {
        private readonly IMesaServicio _mesaServicio;

        public _00013_Mesas()
             : this(new MesaServicio())
        {
            InitializeComponent();
        }


        public _00013_Mesas(IMesaServicio mesaServicio)
        {

            _mesaServicio = mesaServicio;
        }
        public override void FormatearGrilla(DataGridView grilla)
        {
            base.FormatearGrilla(grilla);

            grilla.Columns["Salon"].Visible = true;
            grilla.Columns["Salon"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grilla.Columns["Salon"].HeaderText = @"Salon";
            grilla.Columns["Salon"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            grilla.Columns["Numero"].Visible = true;
            grilla.Columns["Numero"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grilla.Columns["Numero"].HeaderText = @"Nº Mesa";
            grilla.Columns["Numero"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            grilla.Columns["Descripcion"].Visible = true;
            grilla.Columns["Descripcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grilla.Columns["Descripcion"].HeaderText = @"Mesa";
            grilla.Columns["Descripcion"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            grilla.Columns["EstaEliminadoStr"].Visible = true;
            grilla.Columns["EstaEliminadoStr"].Width = 100;
            grilla.Columns["EstaEliminadoStr"].HeaderText = @"Eliminado";
            grilla.Columns["EstaEliminadoStr"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grilla.Columns["EstaEliminadoStr"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        public override void ActualizarDatos(DataGridView grilla, string cadenaBuscar)
        {
            grilla.DataSource = _mesaServicio.Obtener(cadenaBuscar);
        }
        public override void EjecutarNuevo()
        {
            var fMesaAbm = new _00014_ABM_Mesas(TipoOperacion.Nuevo);
            fMesaAbm.ShowDialog();

            ActualizarSegunOperacion(fMesaAbm.RealizoAlgunaOperacion);
        }
        public override void EjecutarModificar()
        {
            if (EntidadSeleccionada != null)
            {
                if (!((MesaDto)EntidadSeleccionada).EstaEliminado)
                {
                    base.EjecutarModificar();

                    if (!PuedeEjecutarComando) return;

                    var fEmpleadoAbm = new _00014_ABM_Mesas(TipoOperacion.Modificar, EntidadId);
                    fEmpleadoAbm.ShowDialog();

                    ActualizarSegunOperacion(fEmpleadoAbm.RealizoAlgunaOperacion);
                }
                else
                {
                    MessageBox.Show(@"La Mesa esta Eliminada.", @"Atención", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
        }
        public override void EjecutarEliminar()
        {
            if (EntidadSeleccionada != null)
            {
                if (!((MesaDto)EntidadSeleccionada).EstaEliminado)
                {
                    base.EjecutarEliminar();

                    if (!PuedeEjecutarComando) return;

                    var fEmpleadoAbm = new _00014_ABM_Mesas(TipoOperacion.Eliminar, EntidadId);

                    fEmpleadoAbm.ShowDialog();

                    ActualizarSegunOperacion(fEmpleadoAbm.RealizoAlgunaOperacion);
                }
                else
                {
                    MessageBox.Show(@"La Mesa esta Eliminada.", @"Atención", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
        }
        private void ActualizarSegunOperacion(bool realizoAlgunaOperacion)
        {
            if (realizoAlgunaOperacion)
            {
                ActualizarDatos(dgvGrilla, string.Empty);
            }
        }
    }
}
