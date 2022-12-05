using Presentacion.Core.VentaSalon;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XCommerce.Servicio.Core.Kiosco;
using XCommerce.Servicio.Core.Kiosco.DTOs;
using XCommerce.Servicio.Core.Producto;
using Presentacion.Helpers;
using XCommerce.AccesoDatos;
using XCommerce.Servicio.Core.Tarjeta;
using Presentacion.Core.Articulo;
using XCommerce.Servicio.Core.Articulo;
using Presentacion.Core.Cliente;
using XCommerce.Servicio.Core.Cliente;
using Presentacion.Core.Precio;

namespace Presentacion.Core.Kiosco
{
    public partial class _00050_Kiosco : Form
    {
        private readonly IArticuloServicio _articuloServicio = new ArticuloServicio();
        private readonly IClienteServicio _clienteServicio = new ClienteServicio();
        private readonly IkioscoServicio _kioscoServicio = new KioscoServicio();
        private readonly long _comprobanteId;
        private kioscoDto _KioscoDto;
        private readonly IProductoServicio _productoServicio = new ProductoServicio();
        private long? ItemSelecId;

        public _00050_Kiosco()
        {
            InitializeComponent();

            var comprobanteId = _kioscoServicio.CrearComprobante();
            _comprobanteId = comprobanteId;

            ActualizarGrilla();

            ObtenerComprobanteKiosco(_comprobanteId);

            txtCodigo.KeyPress += Validacion.NoSimbolos;
            txtCodigo.KeyPress += Validacion.NoLetras;


        }

        private void ActualizarGrilla()
        {
            var comprobante = _kioscoServicio.ObtenerUltimoComprobante();

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

        private void btnAgregar_Click(object sender, EventArgs e)
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
            if (_productoServicio.TienePrecioParaGeneral(txtCodigo.Text, "Kiosco") == false)
            {
                MessageBox.Show("El Producto Ingresado No Posee Precio");
                LimpiarTxt();
                return;
            }

            var producto = _productoServicio.ObtenerPorCodigo(txtCodigo.Text, "Kiosco");

            if (producto == null)
            {
                MessageBox.Show("Por favor ingrese un Codigo Valido");
                LimpiarTxt();
                return;
            }

            if (producto.EstaDiscontinuado)
            {
                MessageBox.Show("El Producto está Discontinuado");
                LimpiarTxt();
                return;
            }

            /* if(producto.HoraVentaMax < DateTime.Now)
             {
                  MessageBox.Show("El Producto no se puede vender a esta hora");
                  LimpiarTxt();
                  return;
             }
             */


            /*if (_productoServicio.LimiteVenta(producto.Id))
            {
                MessageBox.Show("No se puede superar el limite de venta");
                LimpiarTxt();
                return;
            }*/



            txtDescripcionProducto.Text = producto.Descripcion;
            txtPrecioUnitarioProducto.Text = producto.Precio.ToString();

            _kioscoServicio.AgregarItem(txtCodigo.Text, nudCantidad.Value, _comprobanteId);

            ActualizarGrilla();

            ObtenerComprobanteKiosco(_comprobanteId);

            LimpiarTxt();
        }


        private void ObtenerComprobanteKiosco(long comprobanteid)
        {
            _KioscoDto = _kioscoServicio.ObtenerUltimoComprobante();

            if (_KioscoDto == null)
            {
                MessageBox.Show("Ocurrio un Error");
                this.Close();
            }

            var cliente = _clienteServicio.ObtenerPorId(_KioscoDto.Clienteid);

            txtClienteCuil.Text = cliente.Cuil;
            txtClienteNombre.Text = cliente.ApyNom;

            nudSubTotal.Value = _KioscoDto.SubTotal;
            nudDescuento.Value = _KioscoDto.Descuento;
            nudTotal.Value = _KioscoDto.Total;

           

            dgvGrilla.DataSource = _KioscoDto.Items.ToList();

            FormatearGrilla(dgvGrilla);
        }

        private void _00050_Kiosco_Activated(object sender, EventArgs e)
        {
            ObtenerComprobanteKiosco(_comprobanteId);

            
        }

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((char)Keys.Enter == e.KeyChar)
            {
                
                ValidarKiosco();
            }
           
        }


        private void ValidarKiosco()
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
            if (_productoServicio.TienePrecioParaGeneral(txtCodigo.Text, "Kiosco") == false)
            {
                MessageBox.Show("El Producto Ingresado No Posee Precio");
                LimpiarTxt();
                return;
            }

            

            var producto = _productoServicio.ObtenerPorCodigo(txtCodigo.Text, "Kiosco");


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
        private void LimpiarTxt()
        {
            txtCodigo.Clear();
            txtDescripcionProducto.Clear();
            txtPrecioUnitarioProducto.Clear();
            nudCantidad.Value = 1;
            txtCodigo.Focus();
        }

        private void _00050_Kiosco_Load(object sender, EventArgs e)
        {
            
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
                    return true;
                }
                else
                {
                    var comprobante = _kioscoServicio.ObtenerUltimoComprobante();

                    var fPagarKiosco = new _00052_PagoKiosco(comprobante);
                    fPagarKiosco.ShowDialog();
                }
            }
            if (keyData == Keys.S)
            {
                var fBuscarProducto = new _00051_BuscarProducto(_comprobanteId);
                fBuscarProducto.ShowDialog();
            }
            if (keyData == Keys.R)
            {
                if (ItemSelecId == null)
                {
                    MessageBox.Show("Por favor seleccione el Producto de la lista que desea Eliminar");
                    return true;
                }

                _kioscoServicio.EliminarItem(ItemSelecId, _comprobanteId);

                ActualizarGrilla();

                ObtenerComprobanteKiosco(_comprobanteId);

                LimpiarTxt();
            }
            if (keyData == Keys.N)
            {
                var fNuevoProducto = new _00011_ABM_Articulos(TipoOperacion.Nuevo);
                fNuevoProducto.ShowDialog();
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void nudDescuento_ValueChanged(object sender, EventArgs e)
        {
            var comprobante = _kioscoServicio.ObtenerUltimoComprobante();

            comprobante.Descuento = nudDescuento.Value;

            ObtenerComprobanteKiosco(_comprobanteId);
        }

        private void btnEliminarProducto_Click_1(object sender, EventArgs e)
        {
            if (ItemSelecId == null)
            {
                MessageBox.Show("Por favor seleccione el Producto de la lista que desea Eliminar");
                return;
            }

            _kioscoServicio.EliminarItem(ItemSelecId, _comprobanteId);

            ActualizarGrilla();

            ObtenerComprobanteKiosco(_comprobanteId);

            LimpiarTxt();
        }

        private void btnNuevoProducto_Click_1(object sender, EventArgs e)
        {
            var fNuevoProducto = new _00011_ABM_Articulos(TipoOperacion.Nuevo);
            fNuevoProducto.ShowDialog();
        }

        private void btnBuscarProducto_Click_1(object sender, EventArgs e)
        {
            var fBuscarProducto = new _00051_BuscarProducto(_comprobanteId);
            fBuscarProducto.ShowDialog();

            var articulo = _articuloServicio.ObtenerPorId(_productoServicio.ObtenerProducSelecId());

            if (articulo == null)
            {
                return;
            }
            else
            {
                _productoServicio.LimpiarProducSelecId();

                var producto = _productoServicio.ObtenerPorCodigo(articulo.Codigo, "Kiosco");

                txtCodigo.Text = producto.Codigo;
                txtDescripcionProducto.Text = producto.Descripcion;
                nudCantidad.Value = 1;
                txtPrecioUnitarioProducto.Text = producto.Precio.ToString();
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            var fClienteBuscar = new _00076_BuscarCliente();
            fClienteBuscar.ShowDialog();

            var cliente = _clienteServicio.ObtenerPorId(_clienteServicio.ObtenerEntidadSelecId());

            if (cliente == null) return;

            _clienteServicio.LimpiarEntidadSelecId();
            
            txtClienteCuil.Text = cliente.Cuil;
            txtClienteNombre.Text = cliente.ApyNom;

            _KioscoDto.Clienteid = cliente.Id;

            txtCodigo.Focus();
        }

        private void NuevoPrecio_Click(object sender, EventArgs e)
        {
            var fPrecioABM = new _00032_ABM_Precio(TipoOperacion.Nuevo);
            fPrecioABM.ShowDialog();
        }

        private void btnNuevoCliente_Click(object sender, EventArgs e)
        {
            var fNuevoCliente = new _00004_ABM_Cliente(TipoOperacion.Nuevo);
            fNuevoCliente.ShowDialog();
        }

        private void btnRealizarPago_Click(object sender, EventArgs e)
        {
            if (nudTotal.Value == 0)
            {
                MessageBox.Show("No realizo ninguna venta vuelva pronto");
                return;
            }

            var comprobante = _kioscoServicio.ObtenerUltimoComprobante();

            var fPagarKiosco = new _00052_PagoKiosco(comprobante);
            fPagarKiosco.ShowDialog();
        }
    }
}
