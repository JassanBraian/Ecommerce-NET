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
using XCommerce.Servicio.Core.Proveedor;
using XCommerce.Servicio.Core.Proveedor.DTOs;

namespace Presentacion.Core.Proveedor
{
    public partial class _00033_Proveedor : FormularioConsulta
    {
        private readonly IProveedorServicio _ProveedorServicio;

        public _00033_Proveedor()
            : this(new ProveedorServicio())
        {
            InitializeComponent();
        }

        public _00033_Proveedor(IProveedorServicio ProveedorServicio)
        {
            _ProveedorServicio = ProveedorServicio;
        }

        public override void FormatearGrilla(DataGridView grilla)
        {
            base.FormatearGrilla(grilla);

            grilla.Columns["RazonSocial"].Visible = true;
            grilla.Columns["RazonSocial"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grilla.Columns["RazonSocial"].HeaderText = @"Razon Social";
            grilla.Columns["RazonSocial"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            grilla.Columns["Descripcion"].Visible = true;
            grilla.Columns["Descripcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grilla.Columns["Descripcion"].HeaderText = @"Descripcion";
            grilla.Columns["Descripcion"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            grilla.Columns["Contacto"].Visible = true;
            grilla.Columns["Contacto"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grilla.Columns["Contacto"].HeaderText = @"Contacto";
            grilla.Columns["Contacto"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            grilla.Columns["Telefono"].Visible = true;
            grilla.Columns["Telefono"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grilla.Columns["Telefono"].HeaderText = @"Telefono";
            grilla.Columns["Telefono"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            grilla.Columns["Email"].Visible = true;
            grilla.Columns["Email"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grilla.Columns["Email"].HeaderText = @"Email";
            grilla.Columns["Email"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;



            grilla.Columns["EstaEliminadoStr"].Visible = true;

            grilla.Columns["EstaEliminadoStr"].HeaderText = @"Eliminado";
            grilla.Columns["EstaEliminadoStr"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grilla.Columns["EstaEliminadoStr"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

        }
        public override void ActualizarDatos(DataGridView grilla, string cadenaBuscar)
        {
            grilla.DataSource = _ProveedorServicio.Obtener(cadenaBuscar);
        }

        public override void EjecutarNuevo()
        {
            var fProveedorABM = new _00034_ABM_Proveedor(TipoOperacion.Nuevo);
            fProveedorABM.ShowDialog();

            ActualizarSegunOperacion(fProveedorABM.RealizoAlgunaOperacion);
        }

        public override void EjecutarModificar()
        {

            if (!((ProveedorDto)EntidadSeleccionada).EstaEliminado)
            {
                base.EjecutarModificar();

                if (!PuedeEjecutarComando) return;

                var fProveedorABM = new _00034_ABM_Proveedor(TipoOperacion.Modificar, EntidadId);

                fProveedorABM.ShowDialog();


                ActualizarSegunOperacion(fProveedorABM.RealizoAlgunaOperacion);
            }
            else
            {
                MessageBox.Show(@"El Proveedor  se encuetra Elimnado", @"Atención", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }


        }
        public override void EjecutarEliminar()
        {
            if (!((ProveedorDto)EntidadSeleccionada).EstaEliminado)
            {
                base.EjecutarEliminar();

                if (!PuedeEjecutarComando) return;

                var fProveedorABM = new _00034_ABM_Proveedor(TipoOperacion.Eliminar, EntidadId);

                fProveedorABM.ShowDialog();

                ActualizarSegunOperacion(fProveedorABM.RealizoAlgunaOperacion);
            }
            else
            {
                MessageBox.Show(@"El Proveedor se encuetra Elimnado", @"Atención", MessageBoxButtons.OK,
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
