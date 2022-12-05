using Presentacion.Core.Cliente;
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
using XCommerce.Servicio.Core.Cliente;
using XCommerce.Servicio.Core.Cliente.DTOs;
using XCommerce.Servicio.Core.Delivery;

namespace Presentacion.Core.Delivery
{
    public partial class _00068_ClienteDelivery : Form
    {

        private readonly IClienteServicio _ClienteServicio;


        private readonly IDeliveryServicio _DeliveryServicio ;

        protected long? clienteId;

        protected object clienteSeleccionado;


        protected long _comprobanteId;


        public _00068_ClienteDelivery()
            : this(new ClienteServicio(), new DeliveryServicio())
        {
            InitializeComponent();

            _comprobanteId = _DeliveryServicio.ObtenerUltimoComprobante().Id;
        }
        

        public _00068_ClienteDelivery(IClienteServicio _clienteServicio, IDeliveryServicio _deliveryServicio)
        {
            _ClienteServicio = _clienteServicio;
            _DeliveryServicio = _deliveryServicio;
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
             
              grilla.Columns["Telefono"].Visible = true;
              grilla.Columns["Telefono"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
              grilla.Columns["Telefono"].HeaderText = @"Telefono";
              grilla.Columns["Telefono"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
             
              grilla.Columns["Celular"].Visible = true;
              grilla.Columns["Celular"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
              grilla.Columns["Celular"].HeaderText = @"Celular";
              grilla.Columns["Celular"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
             
              grilla.Columns["Calle"].Visible = true;
              grilla.Columns["Calle"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
              grilla.Columns["Calle"].HeaderText = @"Calle";
              grilla.Columns["Calle"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
             
              grilla.Columns["Numero"].Visible = true;
              grilla.Columns["Numero"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
              grilla.Columns["Numero"].HeaderText = @"Numero";
              grilla.Columns["Numero"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
             
              grilla.Columns["Dpto"].Visible = true;
              grilla.Columns["Dpto"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
              grilla.Columns["Dpto"].HeaderText = @"Dpto";
              grilla.Columns["Dpto"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
             
              grilla.Columns["Piso"].Visible = true;
              grilla.Columns["Piso"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
              grilla.Columns["Piso"].HeaderText = @"Piso";
              grilla.Columns["Piso"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;





        }
        public void EjecutarLoadFormulario()
        {
            ActualizarDatos(dgvgrillaBuscarCtaCte, string.Empty);
        }


        private void _00068_ClienteDelivery_Load(object sender, EventArgs e)
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
                _ClienteServicio.EstablecerClienteCtaCte(((ClienteDto)clienteSeleccionado).Id);

                this.Close();
            
        }

        private void btnNuevoCliente_Click(object sender, EventArgs e)
        {
            var fClientesABM = new _00004_ABM_Cliente(TipoOperacion.Nuevo);
            fClientesABM.ShowDialog();
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
            _ClienteServicio.EstablecerClienteCtaCte(((ClienteDto)clienteSeleccionado).Id);

            this.Close();
        }
    }
    
}
