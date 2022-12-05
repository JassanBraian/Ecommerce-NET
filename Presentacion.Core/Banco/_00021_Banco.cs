using Presentacion.FormularioBase;
using Presentacion.Helpers;
using System.Windows.Forms;
using XCommerce.Servicio.Core.Banco;
using XCommerce.Servicio.Core.Banco.DTOs;

namespace Presentacion.Core.Banco
{
    public partial class _00021_Banco : FormularioConsulta
    {
        private readonly IBancoServicio _BancoServicio;

        public _00021_Banco()
            :this(new BancoServicio())
        {
            InitializeComponent();
        }

        public _00021_Banco(IBancoServicio BancoServicio)
        {
            _BancoServicio = BancoServicio;
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
            grilla.DataSource = _BancoServicio.Obtener(cadenaBuscar);
        }

        public override void EjecutarNuevo()
        {
            var fBancoABM = new _00022_ABM_Banco(TipoOperacion.Nuevo);
            fBancoABM.ShowDialog();

            ActualizarSegunOperacion(fBancoABM.RealizoAlgunaOperacion);
        }

        public override void EjecutarModificar()
        {

            if (!((BancoDto)EntidadSeleccionada).EstaEliminado)
            {
                base.EjecutarModificar();

                if (!PuedeEjecutarComando) return;

                var fBancoABM = new _00022_ABM_Banco(TipoOperacion.Modificar, EntidadId);

                fBancoABM.ShowDialog();


                ActualizarSegunOperacion(fBancoABM.RealizoAlgunaOperacion);
            }
            else
            {
                MessageBox.Show(@"El Banco se encuetra Elimnado", @"Atención", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }


        }
        public override void EjecutarEliminar()
        {
            if (!((BancoDto)EntidadSeleccionada).EstaEliminado)
            {
                base.EjecutarEliminar();

                if (!PuedeEjecutarComando) return;

                var fBancoABM = new _00022_ABM_Banco(TipoOperacion.Eliminar, EntidadId);

                fBancoABM.ShowDialog();

                ActualizarSegunOperacion(fBancoABM.RealizoAlgunaOperacion);
            }
            else
            {
                MessageBox.Show(@"El Banco se encuetra Elimnado", @"Atención", MessageBoxButtons.OK,
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
