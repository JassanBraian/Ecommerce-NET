using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XCommerce.Servicio.Core.Mesa;
using XCommerce.Servicio.Core.Mesa.DTOs;
using XCommerce.Servicio.Core.Salon;
using XCommerce.Servicio.Core.Salon.DTOs;

namespace Presentacion.Core.VentaSalon
{
    public partial class _00078_UnirMesas : Form
    {
        private readonly IMesaServicio _mesaServicio = new MesaServicio();

        private readonly ISalonServicio _salonServicio = new SalonServicio();

        protected long? mesaId;

        protected object mesaSeleccionada;

        public _00078_UnirMesas()
        {
            InitializeComponent();
        }

        private void _00078_UnirMesas_Load(object sender, EventArgs e)
        {
            ActualizarDatos(dgvgrilla, string.Empty);

            FormatearGrilla(dgvgrilla);

            CargarComboBox(cmbSalon.ComboBox, _salonServicio.ObtenerSalonesExistente(string.Empty), "Descripcion", "Id");
        }

        public void FormatearGrilla(DataGridView grilla)
        {
            for (var i = 0; i < dgvgrilla.ColumnCount; i++)
            {
                dgvgrilla.Columns[i].Visible = false;
            }

            grilla.Columns["Salon"].Visible = true;
            grilla.Columns["Salon"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grilla.Columns["Salon"].HeaderText = @"Salon";
            grilla.Columns["Salon"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            grilla.Columns["Numero"].Visible = true;
            grilla.Columns["Numero"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grilla.Columns["Numero"].HeaderText = @"Nº Mesa";
            grilla.Columns["Numero"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            grilla.Columns["Descripcion"].Visible = true;
            grilla.Columns["Descripcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grilla.Columns["Descripcion"].HeaderText = @"Mesa";
            grilla.Columns["Descripcion"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            grilla.Columns["EstaEliminadoStr"].Visible = true;
            grilla.Columns["EstaEliminadoStr"].Width = 100;
            grilla.Columns["EstaEliminadoStr"].HeaderText = @"Eliminado";
            grilla.Columns["EstaEliminadoStr"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grilla.Columns["EstaEliminadoStr"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

        }

        public void ActualizarDatos(DataGridView grilla, string cadenaBuscar)
        {
            grilla.DataSource = _mesaServicio.ObtenerMesasAUnir().ToList();
        }

        public virtual void CargarComboBox(ComboBox cmb, object datos, string propiedadMostrar,
            string propiedadDevolver)
        {
            cmb.DataSource = datos;
            cmb.DisplayMember = propiedadMostrar;
            cmb.ValueMember = propiedadDevolver;
        }

        private void cmbSalon_SelectedIndexChanged(object sender, EventArgs e)
        {            
            CargarComboBox(cmbMesa.ComboBox, _mesaServicio.ObtenerVigentesPorSalon(
                ((SalonDto)cmbSalon.ComboBox.SelectedItem).Id, string.Empty), "Numero", "Id");
        }

        private void btnAgregarMesa_Click(object sender, EventArgs e)
        {
            _mesaServicio.AgregarMesaAMesasAUnir((MesaDto)cmbMesa.ComboBox.SelectedItem);

            ActualizarDatos(dgvgrilla, string.Empty);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            var respuesta = MessageBox.Show("Desea Salir sin Guardar la Union de las Mesas?", "Advertencia",
                MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (respuesta != DialogResult.Yes) return;

            _mesaServicio.LimpiarMesasAUnir();

            this.Close();
        }

        private void btnEliminarMesa_Click(object sender, EventArgs e)
        {
            _mesaServicio.EliminarMesaDeMesasAUnir((MesaDto)mesaSeleccionada);

            ActualizarDatos(dgvgrilla, string.Empty);
        }

        private void dgvgrilla_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvgrilla.RowCount > 0)
            {
                mesaId = (long?)dgvgrilla["Id", e.RowIndex].Value;
                mesaSeleccionada = dgvgrilla.Rows[e.RowIndex].DataBoundItem;
            }
            else
            {
                mesaId = null;
                mesaSeleccionada = null;
            }
        }

        private void btnUnirMesas_Click(object sender, EventArgs e)
        {
            _mesaServicio.UnirMesas();
        }
    }
}
