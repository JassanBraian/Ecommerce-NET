using Presentacion.Core.Rubro;
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
using XCommerce.Servicio.Core.MotivoBaja;
using XCommerce.Servicio.Core.MotivoBaja.DTOs;


namespace Presentacion.Core.MotivoBaja
{
    public partial class _00027_MotivoBaja : FormularioConsulta
    {
        private readonly IMotivoBajaServicio _MotivoBajaServicio;

        public _00027_MotivoBaja()
            : this(new MotivoBajaServicio())
        {
            InitializeComponent();
        }

        public _00027_MotivoBaja(IMotivoBajaServicio MotivoBajaServicio)
        {
            _MotivoBajaServicio = MotivoBajaServicio;
        }

        public override void FormatearGrilla(DataGridView grilla)
        {
            base.FormatearGrilla(grilla);
            grilla.Columns["Descripcion"].Visible = true;
            grilla.Columns["Descripcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grilla.Columns["Descripcion"].HeaderText = @"Motivo Baja";
            grilla.Columns["Descripcion"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            grilla.Columns["EstaEliminadoStr"].Visible = true;

            grilla.Columns["EstaEliminadoStr"].HeaderText = @"Eliminado";
            grilla.Columns["EstaEliminadoStr"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grilla.Columns["EstaEliminadoStr"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

        }

        public override void ActualizarDatos(DataGridView grilla, string cadenaBuscar)
        {
            grilla.DataSource = _MotivoBajaServicio.Obtener(cadenaBuscar);
        }

        public override void EjecutarNuevo()
        {
            var fMotivoBajaABM = new _00028_ABM_MotivoBaja(TipoOperacion.Nuevo);
            fMotivoBajaABM.ShowDialog();

            ActualizarSegunOperacion(fMotivoBajaABM.RealizoAlgunaOperacion);
        }

        public override void EjecutarModificar()
        {
            base.EjecutarModificar();

            if (!PuedeEjecutarComando) return;

            if (!((MotivoBajaDto)EntidadSeleccionada).EstaEliminado)
            {
               

                var fMotivoBajaABM = new _00028_ABM_MotivoBaja(TipoOperacion.Modificar, EntidadId);

                fMotivoBajaABM.ShowDialog();


                ActualizarSegunOperacion(fMotivoBajaABM.RealizoAlgunaOperacion);
            }
            else
            {
                MessageBox.Show(@"El Motivo Baja  se encuetra Elimnado", @"Atención", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }


        }


        public override void EjecutarEliminar()
        {
            base.EjecutarEliminar();

            if (!PuedeEjecutarComando) return;


            if (!((MotivoBajaDto)EntidadSeleccionada).EstaEliminado)
            {
                
                var fMotivoBajaABM = new _00028_ABM_MotivoBaja(TipoOperacion.Eliminar, EntidadId);

                fMotivoBajaABM.ShowDialog();

                ActualizarSegunOperacion(fMotivoBajaABM.RealizoAlgunaOperacion);
            }
            else
            {
                MessageBox.Show(@"El Motivo de baja se encuetra Elimnado", @"Atención", MessageBoxButtons.OK,
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
