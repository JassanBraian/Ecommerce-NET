namespace Presentacion.Core.Salon
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
    using XCommerce.Servicio.Core.Salon;
    using XCommerce.Servicio.Core.Salon.DTOs;

    public partial class _00017_Salon : FormularioConsulta
    {

        private readonly ISalonServicio _SalonServicio;

        public _00017_Salon()
            :this(new SalonServicio())
        {
            InitializeComponent();
        }

        public _00017_Salon(ISalonServicio SalonServicio)
        {
            _SalonServicio = SalonServicio;
        }

        public override void FormatearGrilla(DataGridView grilla)
        {
            base.FormatearGrilla(grilla);

            grilla.Columns["Descripcion"].Visible = true;
            grilla.Columns["Descripcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grilla.Columns["Descripcion"].HeaderText = @"Salon";
            grilla.Columns["Descripcion"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            grilla.Columns["ListaPrecioDescripcion"].Visible = true;
            grilla.Columns["ListaPrecioDescripcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grilla.Columns["ListaPrecioDescripcion"].HeaderText = @"Lista de precio";
            grilla.Columns["ListaPrecioDescripcion"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            grilla.Columns["EstaEliminadoStr"].Visible = true;
            grilla.Columns["EstaEliminadoStr"].Width = 100;
            grilla.Columns["EstaEliminadoStr"].HeaderText = @"Eliminado";
            grilla.Columns["EstaEliminadoStr"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grilla.Columns["EstaEliminadoStr"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }
        public override void ActualizarDatos(DataGridView grilla, string cadenaBuscar)
        {
            grilla.DataSource = _SalonServicio.Obtener(cadenaBuscar);
        }
        public override void EjecutarNuevo()
        {
            var fSalonABM = new _00018_ABM_Salon(TipoOperacion.Nuevo);
            fSalonABM.ShowDialog();

            ActualizarSegunOperacion(fSalonABM.RealizoAlgunaOperacion);
        }

        public override void EjecutarModificar()
        {
            if (!((SalonDto)EntidadSeleccionada).EstaEliminado)
            {
                base.EjecutarModificar();

                if (!PuedeEjecutarComando) return;

                var fSalonABM = new _00018_ABM_Salon(TipoOperacion.Modificar, EntidadId);
                fSalonABM.ShowDialog();

                ActualizarSegunOperacion(fSalonABM.RealizoAlgunaOperacion);
            }
            else
            {
                MessageBox.Show(@"El salon se encuetra Elimnado", @"Atención", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }
        public override void EjecutarEliminar()
        {

            base.EjecutarEliminar();
            if (!PuedeEjecutarComando) return;

            if (!((SalonDto)EntidadSeleccionada).EstaEliminado)
            {
                

                var fSalonABM = new _00018_ABM_Salon(TipoOperacion.Eliminar, EntidadId);

                fSalonABM.ShowDialog();

                ActualizarSegunOperacion(fSalonABM.RealizoAlgunaOperacion);
            }
            else
            {
                MessageBox.Show(@"El salon se encuetra Elimnado", @"Atención", MessageBoxButtons.OK,
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
