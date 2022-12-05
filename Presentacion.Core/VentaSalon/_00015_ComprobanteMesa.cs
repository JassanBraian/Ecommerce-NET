namespace Presentacion.Core.VentaSalon
{
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
    using XCommerce.Servicio.Core.Articulo;
    using XCommerce.Servicio.Core.Comprobante;
    using XCommerce.Servicio.Core.Comprobante.DTOs;
    using XCommerce.Servicio.Core.Mesa;
    using XCommerce.Servicio.Core.Producto;

    public partial class _00015_ComprobanteMesa : FormularioBase.FormularioBase
    {
        private readonly long _mesaId;
        private readonly IComprobanteSalonServicio _comprobanteSalonServicio;
        private readonly IProductoServicio _productoServicio;
        private readonly IMesaServicio _mesaServicio;
        private ComprobanteMesaDto _comprobanteMesaDto;
        private long? ItemSelecId;
        private decimal? ItemSelecCantidad;
        private long? ItemSelecProductoId;
        IArticuloServicio _articuloServicio = new ArticuloServicio();


        public _00015_ComprobanteMesa()
            : this(new ComprobanteSalonServicio(), new ProductoServicio() , new MesaServicio())
        {
            InitializeComponent();
            
        }

        public _00015_ComprobanteMesa(IComprobanteSalonServicio comprobanteSalonServicio, IProductoServicio productoServicio, IMesaServicio mesaServicio)
        {
            _comprobanteSalonServicio = comprobanteSalonServicio;
            _productoServicio = productoServicio;
            _mesaServicio = mesaServicio;
        }

        public _00015_ComprobanteMesa(long mesaId, int numeroMesa)
            : this()
        {
            this.Text = $@"Venta - Mesa {numeroMesa}";
            _mesaId = mesaId;

            ObtenerComprobanteMesa(mesaId);
        }

        private void ObtenerComprobanteMesa(long mesaId)
        {
            _comprobanteMesaDto = _comprobanteSalonServicio.ObtenerComprobanteMesa(mesaId);

            if(_comprobanteMesaDto == null)
            {
                MessageBox.Show("Ocurrio un Error");
                this.Close();
            }

            txtLegajo.Text = _comprobanteMesaDto.Legajo.ToString();
            txtMozo.Text = _comprobanteMesaDto.ApyNomMozo;

            nudSubTotal.Value = _comprobanteMesaDto.SubTotal;
            nudDescuento.Value = _comprobanteMesaDto.Descuento;
            nudTotal.Value = _comprobanteMesaDto.Total;

            dgvGrilla.DataSource = _comprobanteMesaDto.Items.ToList();

            FormatearGrillaVenta();

            txtCodigo.Focus();
        }

        private void FormatearGrillaVenta()
        {
            for (var i = 0; i < dgvGrilla.ColumnCount; i++)
            {
                dgvGrilla.Columns[i].Visible = false;
            }

            
            dgvGrilla.Columns["CodigoProducto"].HeaderText = @"Codigo Producto";
            dgvGrilla.Columns["CodigoProducto"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvGrilla.Columns["CodigoProducto"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvGrilla.Columns["CodigoProducto"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvGrilla.Columns["CodigoProducto"].Visible = true;

           dgvGrilla.Columns["DescripcionProducto"].HeaderText = @"Descripcion Producto";
            dgvGrilla.Columns["DescripcionProducto"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvGrilla.Columns["DescripcionProducto"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
           dgvGrilla.Columns["DescripcionProducto"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
           dgvGrilla.Columns["DescripcionProducto"].Visible = true;
           
           dgvGrilla.Columns["Cantidad"].HeaderText = @"Cantidad";
            dgvGrilla.Columns["Cantidad"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvGrilla.Columns["Cantidad"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
           dgvGrilla.Columns["Cantidad"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
           dgvGrilla.Columns["Cantidad"].Visible = true;
           
           
           dgvGrilla.Columns["PrecioUnitario"].HeaderText = @"Precio Unitario";
            dgvGrilla.Columns["PrecioUnitario"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvGrilla.Columns["PrecioUnitario"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
           dgvGrilla.Columns["PrecioUnitario"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
           dgvGrilla.Columns["PrecioUnitario"].Visible = true;
           
           dgvGrilla.Columns["SubTotallinea"].HeaderText = @"SubTotal";
           dgvGrilla.Columns["SubTotallinea"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
           dgvGrilla.Columns["SubTotallinea"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvGrilla.Columns["SubTotallinea"].Visible = true;
            

        }

        private void txtCodigoProducto_KeyPress(object sender, KeyPressEventArgs e)
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
                var mesa = _mesaServicio.ObtenerPorId(_mesaId);
                if (_productoServicio.TienePrecioParaSalon(txtCodigo.Text, mesa.SalonId, _mesaId) == false)
                {
                    MessageBox.Show("El producto no tiene precio para este salon");
                    LimpiarTxt();
                    return;
                }
                var producto = _productoServicio.ObtenerPorCodigoParaMesa(_mesaId, txtCodigo.Text);
           
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

        // bt buscar de mozo
        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            var comprobante = _comprobanteSalonServicio.ObtenerComprobanteMesa(_mesaId);

            var fBuscarEmpleadoMozo = new _00046_ListaEmpleadoMozo(comprobante.Id);
            fBuscarEmpleadoMozo.ShowDialog();
        }

        private void _00015_ComprobanteMesa_Activated(object sender, EventArgs e)
        {
            _comprobanteMesaDto = _comprobanteSalonServicio.ObtenerComprobanteMesa(_mesaId);

            //txtLegajo.Text = _comprobanteMesaDto.Legajo.ToString();
            //txtMozo.Text = _comprobanteMesaDto.ApyNomMozo;

            ObtenerComprobanteMesa(_mesaId);
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


            var mesa = _mesaServicio.ObtenerPorId(_mesaId);
            if (_productoServicio.TienePrecioParaSalon(txtCodigo.Text, mesa.SalonId, _mesaId) == false)
            {
                MessageBox.Show("El producto no tiene precio para este salon");
                LimpiarTxt();
                return;
            }

            var producto = _productoServicio.ObtenerPorCodigoParaMesa(_mesaId, txtCodigo.Text);

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

            txtDescripcionProducto.Text = producto.Descripcion;
            txtPrecioUnitarioProducto.Text = producto.Precio.ToString();
            //ObtenerComprobanteMesa(_mesaId);

            _comprobanteSalonServicio.AgregarItem(_mesaId, nudCantidad.Value, producto);

            LimpiarTxt();

            ObtenerComprobanteMesa(_mesaId);

            // Persistencia de Subtotal
            _comprobanteSalonServicio.ActualizarMontos(_comprobanteMesaDto.Id, nudSubTotal.Value, nudDescuento.Value);

        }
        private void LimpiarTxt()
        {
            txtCodigo.Clear();
            txtDescripcionProducto.Clear();
            txtPrecioUnitarioProducto.Clear();
            nudCantidad.Value = 1;
            txtCodigo.Focus();
        }



        private void DgvGrilla_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            ItemSelecId = (long?)dgvGrilla["Id", e.RowIndex].Value;
            ItemSelecCantidad = (decimal?)dgvGrilla["Cantidad", e.RowIndex].Value;
            ItemSelecProductoId = (long?)dgvGrilla["ProductoId", e.RowIndex].Value;
        }

        private void NudDescuento_ValueChanged(object sender, EventArgs e)
        {
           _comprobanteSalonServicio.ActualizarMontos(_comprobanteMesaDto.Id, nudSubTotal.Value, nudDescuento.Value);

            ObtenerComprobanteMesa(_mesaId);

            this.Focus();
        }

        private void btnEliminarProducto_Click_1(object sender, EventArgs e)
        {
            if (ItemSelecId == null)
            {
                MessageBox.Show("Por favor seleccione el Producto de la lista que desea Eliminar");
                return;
            }

            if (ItemSelecCantidad == null)
            {
                MessageBox.Show("Por favor seleccione el Producto de la lista que desea Eliminar");
                return;
            }

            if (ItemSelecProductoId == null)
            {
                MessageBox.Show("Por favor seleccione el Producto de la lista que desea Eliminar");
                return;
            }

            _comprobanteSalonServicio.EliminarItem(ItemSelecId, _comprobanteSalonServicio.ObtenerComprobanteMesa(_mesaId).Id);

            _articuloServicio.AumentarStock((long)ItemSelecProductoId, (decimal)ItemSelecCantidad);

            ObtenerComprobanteMesa(_mesaId);

            LimpiarTxt();

            // Persistencia de Subtotal
            _comprobanteSalonServicio.ActualizarMontos(_comprobanteMesaDto.Id, nudSubTotal.Value, nudDescuento.Value);

        }

        private void btnBuscarProducto_Click_1(object sender, EventArgs e)
        {
            var comprobante = _comprobanteSalonServicio.ObtenerComprobanteMesa(_mesaId);

            var fbuscarProducto = new _00047_BuscarProducto(_mesaId);
            fbuscarProducto.ShowDialog();

            var articulo = _articuloServicio.ObtenerPorId(_productoServicio.ObtenerProducSelecId());

            if (articulo == null) return;

            _productoServicio.LimpiarProducSelecId();

            var producto = _productoServicio.ObtenerPorCodigoParaMesa(_mesaId, articulo.Codigo);

            txtCodigo.Text = producto.Codigo;
            txtDescripcionProducto.Text = producto.Descripcion;
            nudCantidad.Value = 1;
            txtPrecioUnitarioProducto.Text = producto.Precio.ToString();
        }

        private void btnNuevoMozo_Click(object sender, EventArgs e)
        {
            var fNuevoMozo = new _00002_ABM_Empleados(TipoOperacion.Nuevo);
            fNuevoMozo.ShowDialog();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
