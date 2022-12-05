using Presentacion.Core.Articulo;
using Presentacion.Core.Cliente;
using Presentacion.Core.Empleado;
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
using XCommerce.AccesoDatos;
using XCommerce.Servicio.Core.Articulo;
using XCommerce.Servicio.Core.Cliente;
using XCommerce.Servicio.Core.Delivery;
using XCommerce.Servicio.Core.Delivery.DTOs;
using XCommerce.Servicio.Core.Empleado;
using XCommerce.Servicio.Core.Producto;

namespace Presentacion.Core.Delivery
{
    public partial class _00053_Delivery : Form
    {
        private readonly IProductoServicio _productoServicio;
        private readonly IDeliveryServicio _deliveryServicio;
        private readonly IEmpleadoServicio _empleadoServicio;
        private readonly IArticuloServicio _articuloServicio = new ArticuloServicio();
        private readonly IClienteServicio _clienteServicio = new ClienteServicio();
        private readonly long _comprobanteId;
        private long? ItemSelecId;


        private DeliveryDto comprobanteDelivery;


        public _00053_Delivery()
        {
            InitializeComponent();

            _deliveryServicio = new DeliveryServicio();
            _productoServicio = new ProductoServicio();
            _empleadoServicio = new EmpleadoServicio();

            var comprobanteId = _deliveryServicio.CrearComprobante();
            _comprobanteId = comprobanteId;

            ActualizarGrilla();

           ObtenerComprobanteDelivery(_comprobanteId);

           txtCodigo.KeyPress += Validacion.NoLetras;

        }
        public _00053_Delivery(long deliveryId)
        {
            InitializeComponent();
        }



        private void ActualizarGrilla()
        {
            var comprobante = _deliveryServicio.ObtenerUltimoComprobante();

            dgvGrilla.DataSource = comprobante.Items.ToList();

            FormatearGrilla(dgvGrilla);
        }
        private void FormatearGrilla(DataGridView dgvGrilla)
        {
            for (var i = 0; i < dgvGrilla.ColumnCount; i++)
            {
                dgvGrilla.Columns[i].Visible = false;
            }


            dgvGrilla.Columns["Codigo"].Visible = true;
            dgvGrilla.Columns["Codigo"].Width = 100;
            dgvGrilla.Columns["Codigo"].HeaderText = @"Codigo del Producto";
            dgvGrilla.Columns["Codigo"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvGrilla.Columns["Descripcion"].Visible = true;
            dgvGrilla.Columns["Descripcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvGrilla.Columns["Descripcion"].HeaderText = @"Descripcion del Producto";
            dgvGrilla.Columns["Descripcion"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvGrilla.Columns["PrecioUnitario"].Visible = true;
            dgvGrilla.Columns["PrecioUnitario"].Width = 100;
            dgvGrilla.Columns["PrecioUnitario"].HeaderText = @"Precio Unitario";
            dgvGrilla.Columns["PrecioUnitario"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvGrilla.Columns["Cantidad"].Visible = true;
            dgvGrilla.Columns["Cantidad"].Width = 150;
            dgvGrilla.Columns["Cantidad"].HeaderText = @"Cantidad";
            dgvGrilla.Columns["Cantidad"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvGrilla.Columns["SubTotalLinea"].Visible = true;
            dgvGrilla.Columns["SubTotalLinea"].Width = 100;
            dgvGrilla.Columns["SubTotalLinea"].HeaderText = @"SubTotal";
            dgvGrilla.Columns["SubTotalLinea"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvGrilla.Columns["SubTotalLinea"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;


        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCodigo.Text))
            {
                MessageBox.Show("Por favor ingrese un codigo");
                LimpiarTxt();
                return;
            }

            if (_productoServicio.ExisteProducto(txtCodigo.Text) == false)
            {
                MessageBox.Show("El Producto Ingresado No Existe");
                LimpiarTxt();
                return;

            }
            if (_productoServicio.TienePrecioParaGeneral(txtCodigo.Text, "Delivery") == false)
            {
                MessageBox.Show("El Producto Ingresado No Posee Precio");
                LimpiarTxt();
                return;
            }
            var producto = _productoServicio.ObtenerPorCodigo(txtCodigo.Text, "Delivery");

            if (producto == null)
            {
                MessageBox.Show("Por favor ingrese un Codigo Valido");
                LimpiarTxt();
                return;
            }
            else
            {
                if (producto.EstaDiscontinuado)
                {
                    MessageBox.Show("El Producto está Discontinuado");
                    LimpiarTxt();
                    return;
                }
                txtDescripcionProducto.Text = producto.Descripcion;
                txtPrecioUnitarioProducto.Text = producto.Precio.ToString();
                //ObtenerComprobanteMesa(_mesaId);
            }
            _deliveryServicio.AgregarItem(txtCodigo.Text, nudCantidad.Value, _comprobanteId);

            ActualizarGrilla();

            ObtenerComprobanteDelivery(_comprobanteId);

            LimpiarTxt();
        }

        private void ObtenerComprobanteDelivery(long comprobanteid)
        {
            comprobanteDelivery = _deliveryServicio.ObtenerUltimoComprobante();
            
            if (comprobanteDelivery == null)
            {
                MessageBox.Show("Ocurrio un Error");
                this.Close();
            }

            var cadete = _empleadoServicio.ObtenerPorId(comprobanteDelivery.CadeteId);

            txtLegajo.Text = cadete.Legajo.ToString();

            txtNombreCadeta.Text = cadete.ApyNom.ToString();
            

            nudSubTotal.Value = comprobanteDelivery.SubTotal;
            nudDescuento.Value = comprobanteDelivery.Descuento;
            nudTotal.Value = comprobanteDelivery.Total;

            dgvGrilla.DataSource = comprobanteDelivery.Items.ToList();

            FormatearGrilla(dgvGrilla);
        }

        private void _00053_Delivery_Activated(object sender, EventArgs e)
        {
            ObtenerComprobanteDelivery(_comprobanteId);
        }

        

        private void TxtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((char)Keys.Enter == e.KeyChar)
            {
                if (string.IsNullOrEmpty(txtCodigo.Text))
                {
                    MessageBox.Show("Por favor ingrese un codigo");
                    LimpiarTxt();
                    return;
                }
                if (_productoServicio.ExisteProducto(txtCodigo.Text)==false)
                {
                    MessageBox.Show("El Producto Ingresado No Existe");
                    LimpiarTxt();
                    return;

                }
                if (_productoServicio.TienePrecioParaGeneral(txtCodigo.Text, "Delivery") == false)
                {
                    MessageBox.Show("El Producto Ingresado No Posee Precio");
                    LimpiarTxt();
                    return;
                }

                var producto = _productoServicio.ObtenerPorCodigo(txtCodigo.Text ,"Delivery");
                
                if (producto == null)
                {
                    MessageBox.Show("Por favor ingrese un Codigo Valido");
                    LimpiarTxt();
                    return;
                }
                else
                {
                    if (producto.EstaDiscontinuado)
                    {
                        MessageBox.Show("El Producto está Discontinuado");
                        LimpiarTxt();
                        return;
                    }
                    txtDescripcionProducto.Text = producto.Descripcion;
                    txtPrecioUnitarioProducto.Text = producto.Precio.ToString();
                    //ObtenerComprobanteMesa(_mesaId);

                    btnAgregar.Focus();
                }
            }


        }


        private void LimpiarTxt()
        {
            txtCodigo.Clear();
            txtDescripcionProducto.Clear();
            txtPrecioUnitarioProducto.Clear();
            nudCantidad.Value = 1;
            txtCodigo.Focus();
        }

        private void _00053_Delivery_Load(object sender, EventArgs e)
        {
        }

        private void CargarComboBox(ComboBox cmb, object datos, string propiedadMostrar,
           string propiedadDevolver)
        {
            cmb.DataSource = datos;
            cmb.DisplayMember = propiedadMostrar;
            cmb.ValueMember = propiedadDevolver;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            var fBuscarCadete = new _00063_BuscarCadete(_comprobanteId);
            fBuscarCadete.ShowDialog();
        }

        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            var fBuscarCliente = new _00068_ClienteDelivery();
            fBuscarCliente.ShowDialog();

            var cliente = _clienteServicio.ObtenerPorId(_clienteServicio.ObtenerClienteCtaCte());

            if (cliente == null) return;

            txtClienteNombre.Text = cliente.ApyNom;

            txtClienteCuil.Text = cliente.Cuil;
        }


        private void dgvGrilla_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            ItemSelecId = (long?)dgvGrilla["Id", e.RowIndex].Value;
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


            if (keyData == Keys.F1)
            {
                if (nudTotal.Value == 0)
                {
                    MessageBox.Show("No realizo ninguna venta vuelva pronto");
                    return true ;
                }

                var comprobante = _deliveryServicio.ObtenerUltimoComprobante();

                var fPagoDelivery = new _00055_PagoDelivery(comprobante);
                fPagoDelivery.ShowDialog();
            }
            if (keyData == Keys.S)
            {
                var fBuscarProductoDelivery = new _00056_BuscarProductoDelivery(_comprobanteId);
                fBuscarProductoDelivery.ShowDialog();
            }
            if (keyData == Keys.R)
            {
                if (ItemSelecId == null)
                {
                    MessageBox.Show("Por favor seleccione el Producto de la lista que desea Eliminar");
                    return true;
                }

                _deliveryServicio.EliminarItem(ItemSelecId, _comprobanteId);

                ActualizarGrilla();

                ObtenerComprobanteDelivery(_comprobanteId);

                LimpiarTxt();

            }
            if (keyData == Keys.N)
            {
                var fNuevoProducto = new _00011_ABM_Articulos(TipoOperacion.Nuevo);
                fNuevoProducto.ShowDialog();
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btnNuevoProducto_Click_1(object sender, EventArgs e)
        {
            var fNuevoProducto = new _00011_ABM_Articulos(TipoOperacion.Nuevo);
            fNuevoProducto.ShowDialog();
        }

        private void btnEliminarProducto_Click_1(object sender, EventArgs e)
        {
            if (ItemSelecId == null)
            {
                MessageBox.Show("Por favor seleccione el Producto de la lista que desea Eliminar");
                return;
            }

            _deliveryServicio.EliminarItem(ItemSelecId, _comprobanteId);

            ActualizarGrilla();

            ObtenerComprobanteDelivery(_comprobanteId);

            LimpiarTxt();
        }

        private void btnBuscarProducto_Click_1(object sender, EventArgs e)
        {
            var fBuscarProductoDelivery = new _00056_BuscarProductoDelivery(_comprobanteId);
            fBuscarProductoDelivery.ShowDialog();

            var articulo = _articuloServicio.ObtenerPorId(_productoServicio.ObtenerProducSelecId());

            if (articulo == null)
            {
                return;
            }
            else
            {
                _productoServicio.LimpiarProducSelecId();

                var producto = _productoServicio.ObtenerPorCodigo(articulo.Codigo, "Delivery");

                txtCodigo.Text = producto.Codigo;
                txtDescripcionProducto.Text = producto.Descripcion;
                nudCantidad.Value = 1;
                txtPrecioUnitarioProducto.Text = producto.Precio.ToString();
            }
        }

        private void btnNuevoCliente_Click_1(object sender, EventArgs e)
        {
            var fClientesABM = new _00004_ABM_Cliente(TipoOperacion.Nuevo);
            fClientesABM.ShowDialog();
        }

        private void btnNuevoCadete_Click(object sender, EventArgs e)
        {
            var fCadeteNuevo = new _00002_ABM_Empleados(TipoOperacion.Nuevo);
            fCadeteNuevo.ShowDialog();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void nudDescuento_ValueChanged(object sender, EventArgs e)
        {
            var comprobante = _deliveryServicio.ObtenerUltimoComprobante();

            comprobante.Descuento = nudDescuento.Value;

            ObtenerComprobanteDelivery(_comprobanteId);
        }

        private void cmbRealizarPago_Click(object sender, EventArgs e)
        {
            if (nudTotal.Value == 0)
            {
                MessageBox.Show("No realizo ninguna venta vuelva pronto");
                return;
            }

            var comprobante = _deliveryServicio.ObtenerUltimoComprobante();
            
            var fPagoDelivery = new _00055_PagoDelivery(comprobante);
            fPagoDelivery.ShowDialog();

        }
    }
}
