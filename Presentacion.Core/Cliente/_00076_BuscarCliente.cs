using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XCommerce.Servicio.Core.Cliente;

namespace Presentacion.Core.Cliente
{
    public partial class _00076_BuscarCliente : Form
    {
        private readonly IClienteServicio _clienteServicio = new ClienteServicio();
        protected long? clienteId;
        protected object clienteSeleccionado;

        public _00076_BuscarCliente()
        {
            InitializeComponent();
        }
        public void FormatearGrilla(DataGridView grilla)
        {

            for (var i = 0; i < dgvgrillaBuscarProducto.ColumnCount; i++)
            {
                dgvgrillaBuscarProducto.Columns[i].Visible = false;
            }

            grilla.Columns["ApyNom"].Visible = true;
            grilla.Columns["ApyNom"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grilla.Columns["ApyNom"].HeaderText = @"Apellido y Nombre";
            grilla.Columns["ApyNom"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            grilla.Columns["Dni"].Visible = true;
            grilla.Columns["Dni"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grilla.Columns["Dni"].HeaderText = @"DNI";
            grilla.Columns["Dni"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            grilla.Columns["Celular"].Visible = true;
            grilla.Columns["Celular"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grilla.Columns["Celular"].HeaderText = @"Celular";
            grilla.Columns["Celular"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            grilla.Columns["MontoMaximoCtaCte"].Visible = true;
            grilla.Columns["MontoMaximoCtaCte"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grilla.Columns["MontoMaximoCtaCte"].HeaderText = @"Monto Cta Cte";
            grilla.Columns["MontoMaximoCtaCte"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            grilla.Columns["SaldoCtaCte"].Visible = true;
            grilla.Columns["SaldoCtaCte"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grilla.Columns["SaldoCtaCte"].HeaderText = @"Saldo Cta Cte";
            grilla.Columns["EstaEliminadoStr"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grilla.Columns["SaldoCtaCte"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

        }

        public void ActualizarDatos(DataGridView grilla, string cadenaBuscar)
        {
            grilla.DataSource = _clienteServicio.Obtener(cadenaBuscar);
        }

        public void EjecutarLoadFormulario()
        {
            ActualizarDatos(dgvgrillaBuscarProducto, string.Empty);
        }

        private void _00076_BuscarCliente_Load(object sender, EventArgs e)
        {
            EjecutarLoadFormulario();
            FormatearGrilla(dgvgrillaBuscarProducto);
        }

        private void BtnSeleccionar_Click(object sender, EventArgs e)
        {
            // Cumple la misma funcion que Doble Clik sobre el producto

            if (clienteSeleccionado == null)
            {
                MessageBox.Show(@"Por favor seleccione un registro.");
                return;
            }

            _clienteServicio.AsignarEntidadSelecId((long)clienteId);

            this.Close();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                DialogResult result = MessageBox.Show("Esta seguro que desea Salir?", "Salir", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    this.Close();
                }
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        /// <summary>
        /// ////////////////////////////////////////////////////////////
        /// </summary>


        private void dgvgrillaBuscarProducto_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvgrillaBuscarProducto.RowCount > 0)
            {
                clienteId = (long?)dgvgrillaBuscarProducto["Id", e.RowIndex].Value;
                clienteSeleccionado = dgvgrillaBuscarProducto.Rows[e.RowIndex].DataBoundItem;
            }
            else
            {
                clienteId = null;
                clienteSeleccionado = null;
            }
        }

        private void btnBuscar_Click_1(object sender, EventArgs e)
        {
            ActualizarDatos(dgvgrillaBuscarProducto, txtBuscar.Text);
        }

        private void dgvgrillaBuscarProducto_DoubleClick(object sender, EventArgs e)
        {
            // Cumple la misma funcion que Seleccionar

            if (clienteId == null) return;

            _clienteServicio.AsignarEntidadSelecId((long)clienteId);

            this.Close();
        }
    }
}
