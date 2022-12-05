using Presentacion.FormularioBase;
using Presentacion.Helpers;
using System.Windows.Forms;
using XCommerce.Servicio.Core.Tarjeta;
using XCommerce.Servicio.Core.Tarjeta.DTOs;

namespace Presentacion.Core.Tarjeta
{
    public partial class _00060_TarjetaConsulta : FormularioConsulta
    {
        private ITarjetaServicio _tarjetaServicio;

        public _00060_TarjetaConsulta()
            : this(new TarjetaServicio())
        {
            InitializeComponent();
        }

        public _00060_TarjetaConsulta(ITarjetaServicio tarjetaServicio)
        {
            _tarjetaServicio = tarjetaServicio;
        }
        public override void FormatearGrilla(DataGridView grilla)
        {
            base.FormatearGrilla(grilla);
            grilla.Columns["Descripcion"].Visible = true;
            grilla.Columns["Descripcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grilla.Columns["Descripcion"].HeaderText = @"Tarjeta";
            grilla.Columns["Descripcion"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            grilla.Columns["EstaEliminadoStr"].Visible = true;

            grilla.Columns["EstaEliminadoStr"].HeaderText = @"Eliminado";
            grilla.Columns["EstaEliminadoStr"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grilla.Columns["EstaEliminadoStr"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

        }
        public override void ActualizarDatos(DataGridView grilla, string cadenaBuscar)
        {
            grilla.DataSource = _tarjetaServicio.Obtener(cadenaBuscar);
        }
        public override void EjecutarNuevo()
        {
            var fNuevaTarjeta = new _00059_TarjetaABM(TipoOperacion.Nuevo);
            fNuevaTarjeta.ShowDialog();

            ActualizarSegunOperacion(fNuevaTarjeta.RealizoAlgunaOperacion);
        }
        public override void EjecutarModificar()
        {

            if (!((TarjetaDto)EntidadSeleccionada).EstaEliminado)
            {
                base.EjecutarModificar();

                if (!PuedeEjecutarComando) return;

                var fModificarTarjeta = new _00059_TarjetaABM(TipoOperacion.Modificar, EntidadId);

                fModificarTarjeta.ShowDialog();


                ActualizarSegunOperacion(fModificarTarjeta.RealizoAlgunaOperacion);
            }
            else
            {
                MessageBox.Show(@"La tarjeta se encuetra Elimnada", @"Atención", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }

            
        }
        public override void EjecutarEliminar()
        {
            if (!((TarjetaDto)EntidadSeleccionada).EstaEliminado)
            {
                base.EjecutarEliminar();

                if (!PuedeEjecutarComando) return;

                var fEliminarTarjeta = new _00059_TarjetaABM(TipoOperacion.Eliminar, EntidadId);

                fEliminarTarjeta.ShowDialog();

                ActualizarSegunOperacion(fEliminarTarjeta.RealizoAlgunaOperacion);
            }
            else
            {
                MessageBox.Show(@"La tarjeta se encuetra Elimnada", @"Atención", MessageBoxButtons.OK,
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
