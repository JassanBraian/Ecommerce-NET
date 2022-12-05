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
using XCommerce.Servicio.Core.CondicionIva;
using XCommerce.Servicio.Core.CondicionIva.DTOs;

namespace Presentacion.Core.CondicionIva
{
    public partial class _00035_CondicionIvaConsulta : FormularioConsulta
    {
        private readonly ICondicionIvaServicio _CondicionIvaServicio;


        public _00035_CondicionIvaConsulta()
            : this(new CondicionIvaServicio())
        {
            InitializeComponent();
        }


        public _00035_CondicionIvaConsulta(ICondicionIvaServicio CondicionIvaServicio)
        {
            _CondicionIvaServicio = CondicionIvaServicio;

        }

        public override void FormatearGrilla(DataGridView grilla)
        {
            base.FormatearGrilla(grilla);
            grilla.Columns["Descripcion"].Visible = true;
            grilla.Columns["Descripcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grilla.Columns["Descripcion"].HeaderText = @"Condicion IVA";
            grilla.Columns["Descripcion"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            grilla.Columns["EstaEliminadoStr"].Visible = true;

            grilla.Columns["EstaEliminadoStr"].HeaderText = @"Eliminado";
            grilla.Columns["EstaEliminadoStr"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grilla.Columns["EstaEliminadoStr"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

        }
        public override void ActualizarDatos(DataGridView grilla, string cadenaBuscar)
        {
            grilla.DataSource = _CondicionIvaServicio.Obtener(cadenaBuscar);
        }

        public override void EjecutarNuevo()
        {
            var fCondicionIvaConsultaABM = new _00036_ABM_CondicionIva(TipoOperacion.Nuevo);
            fCondicionIvaConsultaABM.ShowDialog();

            ActualizarSegunOperacion(fCondicionIvaConsultaABM.RealizoAlgunaOperacion);
        }

       
        public override void EjecutarModificar()
        {

            if (!((CondicionIvaDto)EntidadSeleccionada).EstaEliminado)
            {
                base.EjecutarModificar();

                if (!PuedeEjecutarComando) return;

                var fCondicionIvaConsultaABM = new _00036_ABM_CondicionIva(TipoOperacion.Modificar, EntidadId);

                fCondicionIvaConsultaABM.ShowDialog();


                ActualizarSegunOperacion(fCondicionIvaConsultaABM.RealizoAlgunaOperacion);
            }
            else
            {
                MessageBox.Show(@"La Condicion Iva  se encuetra Elimnado", @"Atención", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }


        }
        public override void EjecutarEliminar()
        {
            if (!((CondicionIvaDto)EntidadSeleccionada).EstaEliminado)
            {
                base.EjecutarEliminar();

                if (!PuedeEjecutarComando) return;

                var fCondicionIvaConsultaABM = new _00036_ABM_CondicionIva(TipoOperacion.Eliminar, EntidadId);

                fCondicionIvaConsultaABM.ShowDialog();

                ActualizarSegunOperacion(fCondicionIvaConsultaABM.RealizoAlgunaOperacion);
            }
            else
            {
                MessageBox.Show(@"La Condicion Iva  se encuetra Elimnado", @"Atención", MessageBoxButtons.OK,
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
