using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XCommerce.Servicio.Core.Delivery;
using XCommerce.Servicio.Core.Empleado;
using XCommerce.Servicio.Core.Empleado.DTOs;

namespace Presentacion.Core.Delivery
{
    public partial class _00063_BuscarCadete : Form
    {
        private readonly IEmpleadoServicio _EmpleadoSericio;

        private readonly IDeliveryServicio _deliveryServicio;

        protected long? empleadoId;

        protected object empleadoSeleccionado;

        protected long _comprobanteId;

        public _00063_BuscarCadete(long idComprobante)
            : this(new EmpleadoServicio(), new DeliveryServicio())
        {
            InitializeComponent();
            _comprobanteId = idComprobante;
        }

        public _00063_BuscarCadete(IEmpleadoServicio empleadoServicio, IDeliveryServicio deliveryServicio)
        {
            _EmpleadoSericio = empleadoServicio;
            _deliveryServicio = deliveryServicio;
        }

        public void FormatearGrilla(DataGridView grilla)
        {

            for (var i = 0; i < dgvgrillaBuscarDate.ColumnCount; i++)
            {
                dgvgrillaBuscarDate.Columns[i].Visible = false;
            }

            grilla.Columns["ApyNom"].Visible = true;
            grilla.Columns["ApyNom"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grilla.Columns["ApyNom"].HeaderText = @"Apellido y Nombre";
            grilla.Columns["ApyNom"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            grilla.Columns["Legajo"].Visible = true;
            grilla.Columns["Legajo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grilla.Columns["Legajo"].HeaderText = @"Legajo";
            grilla.Columns["Legajo"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            grilla.Columns["TipoEmpleadoDes"].Visible = true;
            grilla.Columns["TipoEmpleadoDes"].HeaderText = @"Tipo de empleado";
            grilla.Columns["TipoEmpleadoDes"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grilla.Columns["TipoEmpleadoDes"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

        }

        public void ActualizarDatos(DataGridView grilla, string cadenaBuscar)
        {
            grilla.DataSource = _EmpleadoSericio.ObtenerCadetes(cadenaBuscar);
        }

        public void EjecutarLoadFormulario()
        {
            ActualizarDatos(dgvgrillaBuscarDate, string.Empty);
        }

        private void _00063_BuscarCadete_Load(object sender, EventArgs e)
        {
            EjecutarLoadFormulario();
            FormatearGrilla(dgvgrillaBuscarDate);
        }

        private void dgvgrillaMozo_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvgrillaBuscarDate.RowCount > 0)
            {
                empleadoId = (long?)dgvgrillaBuscarDate["Id", e.RowIndex].Value;
                empleadoSeleccionado = dgvgrillaBuscarDate.Rows[e.RowIndex].DataBoundItem;



            }
            else
            {
                empleadoId = null;
                empleadoSeleccionado = null;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (empleadoSeleccionado == null)
            {
                MessageBox.Show(@"Por favor seleccione un registro.");
                return;
            }
            
            
                _deliveryServicio.ObtenerCadeteNuevo(((EmpleadoDto)empleadoSeleccionado), _comprobanteId);

                this.Close();
            
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            ActualizarDatos(dgvgrillaBuscarDate, txtBuscar.Text);
        }

        private void dgvgrillaBuscarDate_DoubleClick(object sender, EventArgs e)
        {

            if (empleadoSeleccionado == null)
            {
                MessageBox.Show(@"Por favor seleccione un registro.");
                return;
            }


            _deliveryServicio.ObtenerCadeteNuevo(((EmpleadoDto)empleadoSeleccionado), _comprobanteId);

            this.Close();

        }
    }
}
