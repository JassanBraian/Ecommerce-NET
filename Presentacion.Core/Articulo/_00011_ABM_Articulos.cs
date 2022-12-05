namespace Presentacion.Core.Articulo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using Presentacion.Core.Marcas;
    using Presentacion.Core.Rubro;
    using Presentacion.FormularioBase;
    using Presentacion.Helpers;
    using XCommerce.Servicio.Core.Articulo;
    using XCommerce.Servicio.Core.Articulo.DTOs;
    using XCommerce.Servicio.Core.ListaPrecio;
    using XCommerce.Servicio.Core.Marca;
    using XCommerce.Servicio.Core.Marca.DTOs;
    using XCommerce.Servicio.Core.Precio;
    using XCommerce.Servicio.Core.Precio.DTOs;
    using XCommerce.Servicio.Core.Rubro;
    using XCommerce.Servicio.Core.Rubro.DTOs;
    using static Presentacion.Helpers.ImagenDb;

    public partial class _00011_ABM_Articulos : FormularioAbm
    {
        private readonly IArticuloServicio _articuloServicio;
        private readonly IRubroServicio _rubroServicio;
        private readonly IMarcaServicio _marcaServicio;
        private readonly IPrecioServicio _precioServicio;
        private readonly IListaPrecioServicio _ListaPrecioServico;
        //private decimal _precioProductoModif;

        public _00011_ABM_Articulos(TipoOperacion tipoOperacion, long? entidadId = null)
            : base(tipoOperacion, entidadId)
        {
            InitializeComponent();

            _articuloServicio = new ArticuloServicio();
            _rubroServicio = new RubroServicio();
            _marcaServicio = new MarcaServicio();
            _precioServicio = new PrecioServicio();
            _ListaPrecioServico = new ListaPrecioServicio();

            if (tipoOperacion == TipoOperacion.Eliminar || tipoOperacion == TipoOperacion.Modificar)
            {
                CargarDatos(entidadId);
            }

            if (tipoOperacion == TipoOperacion.Eliminar)
            {
                DesactivarControles(this);
            }

            AsignarEventoEnterLeave(this);
            AgregarControlesObligatorios(nudCantidadLimiteVenta, "LimiteVenta");
            AgregarControlesObligatorios(txtStock, "Stock");
            AgregarControlesObligatorios(txtCodigo, "Codigo");
            AgregarControlesObligatorios(txtCodigoBarra, "CodigoBarra");
            AgregarControlesObligatorios(txtAbreviatura, "Abreviatura");
            AgregarControlesObligatorios(txtDetalle, "Detalle");
            AgregarControlesObligatorios(txtDescripcion, "Descripcion");
            AgregarControlesObligatorios(cmbRubro, "RubroId");
            AgregarControlesObligatorios(cmbMarca, "MarcaId");
            AgregarControlesObligatorios(nudPrecioCosto, "PrecioCosto");

            Inicializador(entidadId);

            if (tipoOperacion == TipoOperacion.Nuevo)
            {
                cmbIva.SelectedIndex = 0;
            }

            imgFotoArticulo.Image = Constantes.Imagen.Usuario;

        }
        public override void Inicializador(long? entidadId)
        {
            if (entidadId.HasValue) return;

            txtCodigo.KeyPress += Validacion.NoLetras;
            txtCodigo.KeyPress += Validacion.NoSimbolos;

            txtCodigoBarra.KeyPress += Validacion.NoLetras;
            txtCodigoBarra.KeyPress += Validacion.NoSimbolos;

            txtStock.KeyPress += Validacion.NoLetras;
            txtStock.KeyPress += Validacion.NoSimbolos;

            nudStockMaximo.KeyPress += Validacion.NoSimbolos;
            nudStockMaximo.KeyPress += Validacion.NoLetras;

            nudStockMinimo.KeyPress += Validacion.NoSimbolos;
            nudStockMinimo.KeyPress += Validacion.NoLetras;

            nudPrecioCosto.KeyPress += Validacion.NoSimbolos;
            nudPrecioCosto.KeyPress += Validacion.NoLetras;
            
            nudCantidadLimiteVenta.KeyPress += Validacion.NoSimbolos;
            nudCantidadLimiteVenta.KeyPress += Validacion.NoLetras;

            CargarComboBox(cmbMarca, _marcaServicio.Obtener(string.Empty), "Descripcion", "Id");
            CargarComboBox(cmbRubro, _rubroServicio.Obtener(string.Empty), "Descripcion", "Id");

            cmbIva.Items.Clear();
            cmbIva.Items.Add("21");
            cmbIva.Items.Add("10,5");
            cmbIva.Items.Add("0");

            txtCodigo.Focus();

            imgFotoArticulo.Image = Constantes.Imagen.Usuario;


        }
        public override void CargarDatos(long? entidadId)
        {
            if (!entidadId.HasValue)
            {
                MessageBox.Show(@"Ocurrio un Error Grave", @"Error Grave", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                this.Close();
            }

            if (TipoOperacion == TipoOperacion.Eliminar)
            {
                btnLimpiar.Enabled = false;
            }

            var articulo = _articuloServicio.ObtenerPorId(entidadId.Value);

            // Guardo el precio antiguo para verificar si ha sido modificado, si lo fue Inserto nuevo Precio
            //_precioProductoModif = articulo.PrecioCosto;

            //nud
            nudStockMaximo.Value = articulo.StockMaximo;
            nudStockMinimo.Value = articulo.StockMinimo;
            nudCantidadLimiteVenta.Value = articulo.LimiteVenta;
            nudPrecioCosto.Value = articulo.PrecioCosto;
            

            //textBox
            txtDescripcion.Text = articulo.Descripcion;
            txtCodigo.Text = articulo.Codigo;
            txtCodigoBarra.Text = articulo.CodigoBarra;
            txtAbreviatura.Text = articulo.Abreviatura;
            txtDetalle.Text = articulo.Detalle;
            imgFotoArticulo.Image = Convertir_Bytes_Imagen(articulo.Foto);
         
            txtStock.Text = articulo.Stock.ToString();
            //check box
            checkActivarLimiteVenta.Checked = articulo.ActivarLimiteVenta;
            checkPermiteStockNegativo.Checked = articulo.PermiteStockNegativo;
            checkDiscontinuar.Checked = articulo.EstaDiscontinuado;
            checkDescuentaStock.Checked = articulo.DescuentaStock;

            //combo box
            CargarComboBox(cmbMarca, _marcaServicio.Obtener(string.Empty), "Descripcion", "Id");
            CargarComboBox(cmbRubro, _rubroServicio.Obtener(string.Empty), "Descripcion", "Id");

            cmbIva.Items.Clear();
            cmbIva.Items.Add("21");
            cmbIva.Items.Add("10,5");
            cmbIva.Items.Add("0");

            cmbMarca.SelectedValue = articulo.MarcaId;
            cmbRubro.SelectedValue = articulo.RubroId;

            switch (articulo.Iva.ToString())
            {
                case "21":

                    cmbIva.SelectedIndex = 0;
                    break;

                case "10,5":

                    cmbIva.SelectedIndex = 1;
                    break;

                case "0":

                    cmbIva.SelectedIndex = 2;
                    break;

            }

            txtCodigo.KeyPress += Validacion.NoLetras;
            txtCodigo.KeyPress += Validacion.NoSimbolos;

            txtCodigoBarra.KeyPress += Validacion.NoLetras;
            txtCodigoBarra.KeyPress += Validacion.NoSimbolos;

            txtStock.KeyPress += Validacion.NoLetras;
            txtStock.KeyPress += Validacion.NoSimbolos;

            nudStockMaximo.KeyPress += Validacion.NoSimbolos;
            nudStockMaximo.KeyPress += Validacion.NoLetras;

            nudPrecioCosto.KeyPress += Validacion.NoSimbolos;
            nudPrecioCosto.KeyPress += Validacion.NoLetras;


            nudStockMinimo.KeyPress += Validacion.NoSimbolos;
            nudStockMinimo.KeyPress += Validacion.NoLetras;

            nudCantidadLimiteVenta.KeyPress += Validacion.NoSimbolos;
            nudCantidadLimiteVenta.KeyPress += Validacion.NoLetras;
        }
        public override bool EjecutarComandoNuevo()
        {
            if (!VerificarDatosObligatorios())
            {
                MessageBox.Show(@"Por favor ingrese los campos Obligatorios.", @"Atención", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }

            if (nudStockMaximo.Value < nudStockMinimo.Value)
            {
                MessageBox.Show(@"El Stock Minimo no puede ser mayor al Stock Maximo.", @"Atención", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                return false;
            }


            if (_articuloServicio.CodigoYaExiste(txtCodigo.Text))
            {
                MessageBox.Show(@"Ya existe otro articulo con este codigo.", @"Atención", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                return false;
            }

            if (_articuloServicio.CodigoBarraYaExiste(txtCodigoBarra.Text))
            {
                MessageBox.Show(@"Ya existe otro articulo con este codigo de barra.", @"Atención", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                return false;
            }

            if(Convert.ToDecimal(txtStock.Text) > nudStockMaximo.Value)
            {
                MessageBox.Show(@"No se puede almarcenar un Stock superior al Maximo Stock.", @"Atención", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                return false;
            }


            if (Convert.ToDecimal(txtStock.Text) < nudStockMinimo.Value)
            {
                MessageBox.Show(@"No se puede almarcenar un Stock inferior al Minimo Stock.", @"Atención", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                return false;
            }

            if (nudPrecioCosto.Value <= 0)
            {
                MessageBox.Show(@"El Precio Costo debe ser mayor a 0", @"Atención", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }

            var nuevoArticulo = new ArticuloDto
            {
                // Id = EntidadId.Value,
                Descripcion = txtDescripcion.Text,
                Codigo = txtCodigo.Text,
                CodigoBarra = txtCodigoBarra.Text,
                Abreviatura = txtAbreviatura.Text,
                Detalle = txtDetalle.Text,
                Foto = Convertir_Imagen_Bytes(imgFotoArticulo.Image),
                Stock = decimal.TryParse(txtStock.Text, out var numero) ? numero : 0m,
                MarcaId = ((MarcaDto)cmbMarca.SelectedItem).Id,
                RubroId = ((RubroDto)cmbRubro.SelectedItem).Id,
                StockMaximo = nudStockMaximo.Value,
                StockMinimo = nudStockMinimo.Value,
                LimiteVenta = nudCantidadLimiteVenta.Value,
                ActivarLimiteVenta = checkActivarLimiteVenta.Checked,
                PermiteStockNegativo = checkPermiteStockNegativo.Checked,
                EstaDiscontinuado = checkDiscontinuar.Checked,
                DescuentaStock = checkDescuentaStock.Checked,
                PrecioCosto = nudPrecioCosto.Value,
                EstaEliminado = false,
                Iva = cmbIva.SelectedItem.ToString(),

            };

            _articuloServicio.Insertar(nuevoArticulo);

            return true;
        }

        public override bool EjecutarComandoModificar()
        {
            if (!VerificarDatosObligatorios())
            {
                MessageBox.Show(@"Por favor ingrese los campos Obligatorios.", @"Atención", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }


            if (nudStockMaximo.Value < nudStockMinimo.Value)
            {
                MessageBox.Show(@"El Stock Minimo no puede ser mayor al Stock Maximo.", @"Atención", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                return false;
            }


            if (Convert.ToDecimal(txtStock.Text) > nudStockMaximo.Value)
            {
                MessageBox.Show(@"No se puede almarcenar un Stock superior al Maximo Stock.", @"Atención", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                return false;
            }

            if (Convert.ToDecimal(txtStock.Text) < nudStockMinimo.Value)
            {
                MessageBox.Show(@"No se puede almarcenar un Stock inferior al Minimo Stock.", @"Atención", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                return false;
            }

            if (nudPrecioCosto.Value <= 0)
            {
                MessageBox.Show(@"El Precio Costo debe ser mayor a 0", @"Atención", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }

            var articuloParaModificar = new ArticuloDto
            {
                Id = EntidadId.Value,
                Descripcion = txtDescripcion.Text,
                Codigo = txtCodigo.Text,
                CodigoBarra = txtCodigoBarra.Text,
                Abreviatura = txtAbreviatura.Text,
                Detalle = txtDetalle.Text,
                Foto = Convertir_Imagen_Bytes(imgFotoArticulo.Image),
                Stock = decimal.Parse(txtStock.Text),
                MarcaId = ((MarcaDto)cmbMarca.SelectedItem).Id,
                RubroId = ((RubroDto)cmbRubro.SelectedItem).Id,
                StockMaximo = nudStockMaximo.Value,
                StockMinimo = nudStockMinimo.Value,
                LimiteVenta = nudCantidadLimiteVenta.Value,
                ActivarLimiteVenta = checkActivarLimiteVenta.Checked,
                PermiteStockNegativo = checkPermiteStockNegativo.Checked,
                EstaDiscontinuado = checkDiscontinuar.Checked,
                DescuentaStock = checkDescuentaStock.Checked,
                PrecioCosto = nudPrecioCosto.Value,
                EstaEliminado = false,
                Iva = cmbIva.SelectedItem.ToString()
            };

            _articuloServicio.Modificar(articuloParaModificar);

            // Inserto nuevo Precio si fue Modificado
            var precioAnterior = _precioServicio.ObtenerPorArticuloYListaCosto(EntidadId.Value);
            if(precioAnterior.PrecioCosto != nudPrecioCosto.Value)
            {
                var nuevoPrecio = new PrecioDto
                {
                    PrecioCosto = nudPrecioCosto.Value,
                    ListaPrecioId = precioAnterior.ListaPrecioId,
                    ListaPrecio = precioAnterior.ListaPrecio,
                    ArticuloId = precioAnterior.ArticuloId,
                    // ActivarHoraVenta = checkActivarHoraVenta.Checked,
                    //HoraVenta = dtpHoraVenta.Value,
                    HoraVenta = DateTime.Now,
                    PrecioPublico = precioAnterior.PrecioPublico,
                    FechaActualizacion = DateTime.Now,
                    EstaEliminado = false
                    
                };

                _precioServicio.Insertar(nuevoPrecio);
            }

            return true;
        }

        private void ActualizarPreciosPublicos()
        {
            var preciosPublicos = _precioServicio.ObtenerSegunArticulo(EntidadId.Value);

            foreach (var precioPublico in preciosPublicos)
            {
                _precioServicio.ActualizarPrecioPublico(precioPublico, nudPrecioCosto.Value);
            }
        }

        public override bool EjecutarComandoEliminar()
        {
            if (EntidadId == null) return false;

            _articuloServicio.Eliminar(EntidadId.Value);

            return true;
        }

        public override void EjecutarComando()
        {
            base.EjecutarComando();

        }


        private void Button1_Click(object sender, EventArgs e)
        {
            var fRubroABM = new _00026_ABM_Rubro(TipoOperacion.Nuevo);
            fRubroABM.ShowDialog();

            if (fRubroABM.RealizoAlgunaOperacion)
            {
                CargarComboBox(cmbRubro, _rubroServicio.Obtener(string.Empty), "Descripcion", "Id");
            }

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            var fMarcaABM = new _00023_ABM_Marca(TipoOperacion.Nuevo);
            fMarcaABM.ShowDialog();

            if (fMarcaABM.RealizoAlgunaOperacion)
            {
                CargarComboBox(cmbMarca, _marcaServicio.Obtener(string.Empty), "Descripcion", "Id");
            }

        }

        private void CheckActivarLimiteVenta_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkActivarLimiteVenta.Checked)
            {
                nudCantidadLimiteVenta.Enabled = true;
            }
            else
            {
                nudCantidadLimiteVenta.Enabled = false;
            }
        }

        private void BtnAgregarImagen_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                // Pregunta si Selecciono un Archivo
                if (!string.IsNullOrEmpty(openFileDialog1.FileName))
                {
                    imgFotoArticulo.Image = Image.FromFile(openFileDialog1.FileName);
                }
                else
                {
                    imgFotoArticulo.Image = Constantes.Imagen.Usuario;
                }
            }
            else
            {
                imgFotoArticulo.Image = Presentacion.Constantes.Imagen.Usuario;
            }
        }

        private void _00011_ABM_Articulos_Load(object sender, EventArgs e)
        {
        }
    }
}
