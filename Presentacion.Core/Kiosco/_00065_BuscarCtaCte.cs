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
using XCommerce.Servicio.Core.Kiosco;

namespace Presentacion.Core.Kiosco
{
    public partial class _00065_BuscarCtaCte : Form
    {

        private readonly IClienteServicio _ClienteServicio;

        private readonly IkioscoServicio _KioscoServicio;

        protected long? clienteId;

        protected object clienteSeleccionado;

        
        protected long _comprobanteId;


        

        public _00065_BuscarCtaCte()
            :this(new ClienteServicio(), new KioscoServicio())
        {
           

            InitializeComponent();

            _comprobanteId = _KioscoServicio.ObtenerUltimoComprobante().Id;
        }

        public _00065_BuscarCtaCte(IClienteServicio _clienteServicio, IkioscoServicio _kioscoServicio)
        {
            _ClienteServicio = _clienteServicio;
            _KioscoServicio = _kioscoServicio;
        }

        public  void FormatearGrilla(DataGridView grilla)
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

        private void _00065_BuscarCtaCte_Load(object sender, EventArgs e)
        {
            EjecutarLoadFormulario();
            FormatearGrilla(dgvgrillaBuscarCtaCte);

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

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            if (clienteSeleccionado == null)
            {
                MessageBox.Show(@"Por favor seleccione un registro.");
                return;
            }
                _KioscoServicio.ObtenerClienteCtaCte(((ClienteDto)clienteSeleccionado).Id, _comprobanteId);
                this.Close();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            ActualizarDatos(dgvgrillaBuscarCtaCte, txtBuscar.Text);
        }

        private void dgvgrillaBuscarCtaCte_DoubleClick(object sender, EventArgs e)
        {
            if (clienteSeleccionado == null)
            {
                MessageBox.Show(@"Por favor seleccione un registro.");
                return;
            }
            _KioscoServicio.ObtenerClienteCtaCte(((ClienteDto)clienteSeleccionado).Id, _comprobanteId);
            this.Close();
        }
    }
}
