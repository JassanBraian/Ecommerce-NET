using Presentacion.Core.Articulo;
using Presentacion.Core.Proveedor;
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
using XCommerce.Servicio.Core.Articulo;
using XCommerce.Servicio.Core.ComprobanteCompra;
using XCommerce.Servicio.Core.ComprobanteCompra.DTOs;
using XCommerce.Servicio.Core.Precio;
using XCommerce.Servicio.Core.Producto;
using XCommerce.Servicio.Core.Proveedor;
using XCommerce.Servicio.Core.Proveedor.DTOs;

namespace Presentacion.Core.CompraMercaderia
{
    public partial class _00055_IngresoMercaderia : Form
    {
        private readonly IArticuloServicio _articuloServicio;
        private readonly ICompraServicio _compraServicio;
        private readonly long _comprobanteId;
        private ComprobanteCompraDto _CompraDto;
        private readonly IProductoServicio _productoServicio;
        private readonly IProveedorServicio _proveedorServicio;
        private readonly IPrecioServicio _precioServicio;
        private long? ItemSelecId;
        private object ItemSelecCodigoProducto;

        public _00055_IngresoMercaderia()
        {
            InitializeComponent();

            _compraServicio = new CompraServicio();
            _productoServicio = new ProductoServicio();
            _proveedorServicio = new ProveedorServicio();
            _articuloServicio = new ArticuloServicio();
            _precioServicio = new PrecioServicio();

            var comprobanteId = _compraServicio.CrearComprobante();
            _comprobanteId = comprobanteId;

            ActualizarGrilla();

            ObtenerComprobanteCompra(_comprobanteId);

            txtCodigo.Focus();
        }

        private void ActualizarGrilla()
        {
            var comprobante = _compraServicio.ObtenerUltimoComprobante();

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
        

        private void ObtenerComprobanteCompra(long comprobanteid)
        {
            _CompraDto = _compraServicio.ObtenerUltimoComprobante();

            if (_CompraDto == null)
            {
                MessageBox.Show("Ocurrio un Error");
                this.Close();
            }


            nudSubTotal.Value = _CompraDto.SubTotal;
            nudDescuento.Value = _CompraDto.Descuento;
            nudTotal.Value = _CompraDto.Total;

            dgvGrilla.DataSource = _CompraDto.Items.ToList();

            FormatearGrilla(dgvGrilla);
        }
        

        private void btnAgregar_Click_1(object sender, EventArgs e)
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
            
            var producto = _productoServicio.ObtenerPorCodigoParaCompra(txtCodigo.Text);

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
                txtDescripcion.Text = producto.Descripcion;
                txtPrecioUnitario.Text = producto.Precio.ToString();
            }
            _compraServicio.AgregarItem(txtCodigo.Text, nudCantidad.Value, _comprobanteId);

            ActualizarGrilla();

            ObtenerComprobanteCompra(_comprobanteId);

            LimpiarTxt();



        }

        private void btnPagar_Click_1(object sender, EventArgs e)
        {
            var comprobante = _compraServicio.ObtenerUltimoComprobante();

            if (((ProveedorDto)cmbProveedor.SelectedItem) == null)
            {
                MessageBox.Show("Por favor ingrese un proveedor");
                return;
            }

            if (nudTotal.Value == 0)
            {
                MessageBox.Show("No realizo ninguna compra vuelva pronto");
                return;
            }

            comprobante.ProveedorId = ((ProveedorDto)cmbProveedor.SelectedItem).Id;

            var fPagarCompra = new _00057_PagoCompra(comprobante);
            fPagarCompra.ShowDialog();

           // this.Close();
        }

        private void _00055_IngresoMercaderia_Activated(object sender, EventArgs e)
        {
            ObtenerComprobanteCompra(_comprobanteId);

            CargarComboBox(cmbProveedor, _proveedorServicio.Obtener(string.Empty), "Descripcion", "Id");
        }

        private void CargarComboBox(ComboBox cmb, object datos, string propiedadMostrar,
            string propiedadDevolver)
        {
            cmb.DataSource = datos;
            cmb.DisplayMember = propiedadMostrar;
            cmb.ValueMember = propiedadDevolver;
        }

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((char)Keys.Enter == e.KeyChar)
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


                var producto = _productoServicio.ObtenerPorCodigoParaCompra(txtCodigo.Text);


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

                    txtDescripcion.Text = producto.Descripcion;
                    txtPrecioUnitario.Text = producto.Precio.ToString();
                    //ObtenerComprobanteMesa(_mesaId);
                }

                btnAgregar.Focus();
            }
            
        }

        private void LimpiarTxt()
        {
            txtCodigo.Clear();
            txtDescripcion.Clear();
            txtPrecioUnitario.Clear();
            nudCantidad.Value = 1;
            txtCodigo.Focus();
        }

        private void dgvGrilla_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            ItemSelecId = (long?)dgvGrilla["Id", e.RowIndex].Value;
            ItemSelecCodigoProducto = dgvGrilla["Codigo", e.RowIndex].Value;
        }

        private void btnActualizarPrecio_Click(object sender, EventArgs e)
        {
            if (ItemSelecId == null)
            {
                MessageBox.Show("Por favor seleccione el Producto de la lista que desea Modificar");
                return;
            }

            var fModifArticulo = new _00011_ABM_Articulos(TipoOperacion.Modificar, ItemSelecId);
            fModifArticulo.ShowDialog();

            var producto = _productoServicio.ObtenerPorCodigoParaCompra(ItemSelecCodigoProducto.ToString());

            _compraServicio.ActualizarPrecioCostoItem(ItemSelecId, _comprobanteId, producto.Precio);

            ObtenerComprobanteCompra(_comprobanteId);

            LimpiarTxt();
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
                var comprobante = _compraServicio.ObtenerUltimoComprobante();

                if (((ProveedorDto)cmbProveedor.SelectedItem) == null)
                {
                    MessageBox.Show("Por favor ingrese un proveedor");
                    return true;
                }

                if (nudTotal.Value == 0)
                {
                    MessageBox.Show("No realizo ninguna compra vuelva pronto");
                    return true;
                }

                comprobante.ProveedorId = ((ProveedorDto)cmbProveedor.SelectedItem).Id;

                var fPagarCompra = new _00057_PagoCompra(comprobante);
                fPagarCompra.ShowDialog();
            }
            if (keyData == Keys.S)
            {
                var fBuscarProducto = new _00056_BuscarProducto(_comprobanteId);
                fBuscarProducto.ShowDialog();

                var articulo = _articuloServicio.ObtenerPorId(_productoServicio.ObtenerProducSelecId());

                if (articulo == null)
                {
                    return true ;
                }
                else
                {
                    _productoServicio.LimpiarProducSelecId();

                    var producto = _productoServicio.ObtenerPorCodigoParaCompra(articulo.Codigo);

                    txtCodigo.Text = producto.Codigo;
                    txtDescripcion.Text = producto.Descripcion;
                    nudCantidad.Value = 1;
                    txtPrecioUnitario.Text = producto.Precio.ToString();
                }
            }
            if (keyData == Keys.R)
            {
                if (ItemSelecId == null)
                {
                    MessageBox.Show("Por favor seleccione el Producto de la lista que desea Eliminar");
                    return true;
                }

                _compraServicio.EliminarItem(ItemSelecId, _comprobanteId);

                ActualizarGrilla();

                ObtenerComprobanteCompra(_comprobanteId);

                LimpiarTxt();
            }
            if (keyData == Keys.N)
            {
                var fNuevoProducto = new _00011_ABM_Articulos(TipoOperacion.Nuevo);
                fNuevoProducto.ShowDialog();
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btnBuscarProducto_Click(object sender, EventArgs e)
        {
            var fBuscarProducto = new _00056_BuscarProducto(_comprobanteId);
            fBuscarProducto.ShowDialog();

            var articulo = _articuloServicio.ObtenerPorId(_productoServicio.ObtenerProducSelecId());

            if (articulo == null)
            {
                return;
            }
            else
            {
                _productoServicio.LimpiarProducSelecId();

                var producto = _productoServicio.ObtenerPorCodigoParaCompra(articulo.Codigo);

                txtCodigo.Text = producto.Codigo;
                txtDescripcion.Text = producto.Descripcion;
                nudCantidad.Value = 1;
                txtPrecioUnitario.Text = producto.Precio.ToString();
            }
        }

        private void btnNuevoProveedor_Click(object sender, EventArgs e)
        {
            var fProveedorABM = new _00034_ABM_Proveedor(TipoOperacion.Nuevo);
            fProveedorABM.ShowDialog();
        }

        private void btnActualizarPrecio_Click_1(object sender, EventArgs e)
        {
            if (ItemSelecId == null)
            {
                MessageBox.Show("Por favor seleccione el Producto de la lista que desea Modificar");
                return;
            }

            var fModifArticulo = new _00011_ABM_Articulos(TipoOperacion.Modificar, ItemSelecId);
            fModifArticulo.ShowDialog();

            var producto = _productoServicio.ObtenerPorCodigoParaCompra(ItemSelecCodigoProducto.ToString());

            _compraServicio.ActualizarPrecioCostoItem(ItemSelecId, _comprobanteId, producto.Precio);

            ObtenerComprobanteCompra(_comprobanteId);

            LimpiarTxt();
        }

        private void btnEliminarProducto_Click_1(object sender, EventArgs e)
        {
            if (ItemSelecId == null)
            {
                MessageBox.Show("Por favor seleccione el Producto de la lista que desea Eliminar");
                return;
            }

            _compraServicio.EliminarItem(ItemSelecId, _comprobanteId);

            ActualizarGrilla();

            ObtenerComprobanteCompra(_comprobanteId);

            LimpiarTxt();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            var fNuevoProducto = new _00011_ABM_Articulos(TipoOperacion.Nuevo);
            fNuevoProducto.ShowDialog();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
