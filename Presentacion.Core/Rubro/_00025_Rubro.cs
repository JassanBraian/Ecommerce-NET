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
using XCommerce.Servicio.Core.Rubro;
using XCommerce.Servicio.Core.Rubro.DTOs;

namespace Presentacion.Core.Rubro
{
    public partial class _00025_Rubro : FormularioConsulta
    {
        private readonly IRubroServicio _RubroServicio;

        public _00025_Rubro()
            : this(new RubroServicio())
        {
            InitializeComponent();
        }

        public _00025_Rubro(IRubroServicio rubroServicio)
        {
            _RubroServicio = rubroServicio;
        }

        public override void FormatearGrilla(DataGridView grilla)
        {
            base.FormatearGrilla(grilla);
            grilla.Columns["Descripcion"].Visible = true;
            grilla.Columns["Descripcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grilla.Columns["Descripcion"].HeaderText = @"Rubro";
            grilla.Columns["Descripcion"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            grilla.Columns["EstaEliminadoStr"].Visible = true;

            grilla.Columns["EstaEliminadoStr"].HeaderText = @"Eliminado";
            grilla.Columns["EstaEliminadoStr"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grilla.Columns["EstaEliminadoStr"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

        }

        public override void ActualizarDatos(DataGridView grilla, string cadenaBuscar)
        {
            grilla.DataSource = _RubroServicio.Obtener(cadenaBuscar);
        }

        public override void EjecutarNuevo()
        {
            var fRubroABM = new _00026_ABM_Rubro(TipoOperacion.Nuevo);
            fRubroABM.ShowDialog();

            ActualizarSegunOperacion(fRubroABM.RealizoAlgunaOperacion);
        }

        public override void EjecutarModificar()
        {

            if (!((RubroDto)EntidadSeleccionada).EstaEliminado)
            {
                base.EjecutarModificar();

                if (!PuedeEjecutarComando) return;

                var fRubroABM = new _00026_ABM_Rubro(TipoOperacion.Modificar, EntidadId);

                fRubroABM.ShowDialog();


                ActualizarSegunOperacion(fRubroABM.RealizoAlgunaOperacion);
            }
            else
            {
                MessageBox.Show(@"El Rubro  se encuetra Elimnado", @"Atención", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }


        }
        public override void EjecutarEliminar()
        {
            if (!((RubroDto)EntidadSeleccionada).EstaEliminado)
            {
                base.EjecutarEliminar();

                if (!PuedeEjecutarComando) return;

                var fRubroABM = new _00026_ABM_Rubro(TipoOperacion.Eliminar, EntidadId);

                fRubroABM.ShowDialog();

                ActualizarSegunOperacion(fRubroABM.RealizoAlgunaOperacion);
            }
            else
            {
                MessageBox.Show(@"El Rubro se encuetra Elimnado", @"Atención", MessageBoxButtons.OK,
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
