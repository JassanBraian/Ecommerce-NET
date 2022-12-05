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
using XCommerce.Servicio.Core.Cliente.DTOs;

namespace Presentacion.Core.Cliente
{
    public partial class _00067_BuscarCtaCte : Form
    {
        private readonly IClienteServicio _ClienteServicio;

        protected long? clienteId;

        protected object clienteSeleccionado;

        protected long _comprobanteId;
        
        public _00067_BuscarCtaCte()
            : this(new ClienteServicio())
        {
            InitializeComponent();


        }

        public _00067_BuscarCtaCte(IClienteServicio _clienteServicio)
        {
            _ClienteServicio = _clienteServicio;
        }
        
        private void _00067_BuscarCtaCte_Load(object sender, EventArgs e)
        {
            EjecutarLoadFormulario();
            FormatearGrilla(dgvgrillaBuscarCtaCte);
        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            if (clienteSeleccionado == null)
            {
                MessageBox.Show(@"Por favor seleccione un registro.");
                return;
            }
            
            
                _ClienteServicio.EstablecerClienteCtaCte(((ClienteDto)clienteSeleccionado).Id);

                this.Close();
            
        }       
       
        public void FormatearGrilla(DataGridView grilla)
        {
            for (var i = 0; i < dgvgrillaBuscarCtaCte.ColumnCount; i++)
            {
                dgvgrillaBuscarCtaCte.Columns[i].Visible = false;
            }

            grilla.Columns["ApyNom"].Visible = true;
            grilla.Columns["ApyNom"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grilla.Columns["ApyNom"].HeaderText = @"Apellido y Nombre";
            grilla.Columns["ApyNom"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            grilla.Columns["MontoMaximoCtaCte"].Visible = true;
            grilla.Columns["MontoMaximoCtaCte"].Width = 150;
            grilla.Columns["MontoMaximoCtaCte"].HeaderText = @"Monto Cta Cte";
            grilla.Columns["MontoMaximoCtaCte"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            grilla.Columns["SaldoCtaCte"].Visible = true;
            grilla.Columns["SaldoCtaCte"].Width = 150;
            grilla.Columns["SaldoCtaCte"].HeaderText = @"Saldo Cta Cte";
            grilla.Columns["SaldoCtaCte"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;


        }
        public void EjecutarLoadFormulario()
        {
            ActualizarDatos(dgvgrillaBuscarCtaCte, string.Empty);
        }

        public void ActualizarDatos(DataGridView grilla, string cadenaBuscar)
        {
            

            grilla.DataSource = _ClienteServicio.Obtener(cadenaBuscar);

        }

        private void dgvgrillaBuscarCtaCte_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvgrillaBuscarCtaCte.RowCount > 0)
            {
                clienteId = (long)dgvgrillaBuscarCtaCte["Id", e.RowIndex].Value;
                clienteSeleccionado = dgvgrillaBuscarCtaCte.Rows[e.RowIndex].DataBoundItem;

            }
            else
            {
                clienteId = null;
                clienteSeleccionado = null;
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            dgvgrillaBuscarCtaCte.DataSource = _ClienteServicio.Obtener(txtBuscar.Text);
        }

        private void dgvgrillaBuscarCtaCte_DoubleClick(object sender, EventArgs e)
        {
            if (clienteSeleccionado == null)
            {
                MessageBox.Show(@"Por favor seleccione un registro.");
                return;
            }


            _ClienteServicio.EstablecerClienteCtaCte(((ClienteDto)clienteSeleccionado).Id);

            this.Close();
        }
    }
}
