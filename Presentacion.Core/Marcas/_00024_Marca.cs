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
using XCommerce.Servicio.Core.Marca;
using XCommerce.Servicio.Core.Marca.DTOs;

namespace Presentacion.Core.Marcas
{
    public partial class _00024_Marca : FormularioConsulta
    {
        private readonly IMarcaServicio _MarcaServicio;

        public _00024_Marca()
            : this(new MarcaServicio())
        {
            InitializeComponent();
        }

        public _00024_Marca(IMarcaServicio MarcaServicio)
        {
            _MarcaServicio = MarcaServicio;
        }

        public override void FormatearGrilla(DataGridView grilla)
        {
            base.FormatearGrilla(grilla);
            grilla.Columns["Descripcion"].Visible = true;
            grilla.Columns["Descripcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grilla.Columns["Descripcion"].HeaderText = @"Marca";
            grilla.Columns["Descripcion"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            grilla.Columns["EstaEliminadoStr"].Visible = true;

            grilla.Columns["EstaEliminadoStr"].HeaderText = @"Eliminado";
            grilla.Columns["EstaEliminadoStr"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grilla.Columns["EstaEliminadoStr"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

        }
        public override void ActualizarDatos(DataGridView grilla, string cadenaBuscar)
        {
            grilla.DataSource = _MarcaServicio.Obtener(cadenaBuscar);
        }

        public override void EjecutarNuevo()
        {
            var fMarcaABM = new _00023_ABM_Marca(TipoOperacion.Nuevo);
            fMarcaABM.ShowDialog();

            ActualizarSegunOperacion(fMarcaABM.RealizoAlgunaOperacion);
        }

        public override void EjecutarModificar()
        {

            if (!((MarcaDto)EntidadSeleccionada).EstaEliminado)
            {
                base.EjecutarModificar();

                if (!PuedeEjecutarComando) return;

                var fMarcaABM = new _00023_ABM_Marca(TipoOperacion.Modificar, EntidadId);

                fMarcaABM.ShowDialog();


                ActualizarSegunOperacion(fMarcaABM.RealizoAlgunaOperacion);
            }
            else
            {
                MessageBox.Show(@"la marca  se encuetra Modificada", @"Atención", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }


        }
        public override void EjecutarEliminar()
        {
            if (!((MarcaDto)EntidadSeleccionada).EstaEliminado)
            {
                base.EjecutarEliminar();

                if (!PuedeEjecutarComando) return;

                var fMarcaABM = new _00023_ABM_Marca(TipoOperacion.Eliminar, EntidadId);

                fMarcaABM.ShowDialog();

                ActualizarSegunOperacion(fMarcaABM.RealizoAlgunaOperacion);
            }
            else
            {
                MessageBox.Show(@"La marca se encuetra Elimnado", @"Atención", MessageBoxButtons.OK,
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
