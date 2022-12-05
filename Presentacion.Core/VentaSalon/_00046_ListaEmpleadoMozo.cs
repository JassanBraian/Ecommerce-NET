using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XCommerce.AccesoDatos;
using XCommerce.Servicio.Core.Comprobante;
using XCommerce.Servicio.Core.Empleado;
using XCommerce.Servicio.Core.Empleado.DTOs;

namespace Presentacion.Core.VentaSalon
{
    public partial class _00046_ListaEmpleadoMozo : Form
    {
        private readonly IEmpleadoServicio _EmpleadoSericio;

        private readonly IComprobanteSalonServicio _ComprobanteSalonServicio;

        protected long? mozoId;

        protected object mozoSeleccionado;

        protected long _comprobanteId;


        public _00046_ListaEmpleadoMozo(long idComprobante)
            : this(new EmpleadoServicio(), new ComprobanteSalonServicio())
        {
            InitializeComponent();
            _comprobanteId = idComprobante;
        }

        public _00046_ListaEmpleadoMozo(IEmpleadoServicio empleadoServicio, IComprobanteSalonServicio comprobanteSalonServicio)
        {

            _EmpleadoSericio = empleadoServicio;
            _ComprobanteSalonServicio = comprobanteSalonServicio;
        }

        public  void FormatearGrilla(DataGridView grilla)
        {

            for (var i = 0; i < dgvgrillaMozo.ColumnCount; i++)
            {
                dgvgrillaMozo.Columns[i].Visible = false;
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
            grilla.DataSource = _EmpleadoSericio.ObtenerMozos(cadenaBuscar);
        }

        

        public void EjecutarLoadFormulario()
        {
            ActualizarDatos(dgvgrillaMozo, string.Empty);
        }

        private void _00046_ListaEmpleadoMozo_Load(object sender, EventArgs e)
        {
            EjecutarLoadFormulario();
            FormatearGrilla(dgvgrillaMozo);
        }

        private void dgvGrillaMozos_RowEnter(object sender, DataGridViewCellEventArgs e)
        {


            if (dgvgrillaMozo.RowCount > 0)
            {
                mozoId = (long?)dgvgrillaMozo["Id", e.RowIndex].Value;
                mozoSeleccionado = dgvgrillaMozo.Rows[e.RowIndex].DataBoundItem;



            }
            else
            {
                mozoId = null;
                mozoSeleccionado = null;
            }


        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (mozoSeleccionado == null)
            {
                MessageBox.Show(@"Por favor seleccione un registro.");
                return;
            }

                _ComprobanteSalonServicio.ObtenerMozoNuevo(((EmpleadoDto)mozoSeleccionado), _comprobanteId);

                this.Close();
            
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            ActualizarDatos(dgvgrillaMozo, txtBuscar.Text);
        }

        private void dgvgrillaMozo_DoubleClick(object sender, EventArgs e)
        {
            if (mozoSeleccionado == null)
            {
                MessageBox.Show(@"Por favor seleccione un registro.");
                return;
            }

            _ComprobanteSalonServicio.ObtenerMozoNuevo(((EmpleadoDto)mozoSeleccionado), _comprobanteId);

            this.Close();
        }
    }
}
