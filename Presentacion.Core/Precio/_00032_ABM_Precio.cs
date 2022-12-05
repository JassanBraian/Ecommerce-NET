using Presentacion.Core.Articulo;
using Presentacion.Core.ListaPrecio;
using Presentacion.FormularioBase;
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
using XCommerce.Servicio.Core.Articulo.DTOs;
using XCommerce.Servicio.Core.ListaPrecio;
using XCommerce.Servicio.Core.ListaPrecio.DTOs;
using XCommerce.Servicio.Core.Precio;
using XCommerce.Servicio.Core.Precio.DTOs;

namespace Presentacion.Core.Precio
{
    public partial class _00032_ABM_Precio  : FormularioAbm
    {
        private readonly IArticuloServicio _articuloServicio;
        private readonly IListaPrecioServicio _listaPrecioServicio;
        private readonly IPrecioServicio _precioServicio;

        public _00032_ABM_Precio(TipoOperacion tipoOperacion, long? entidadId = null)
             : base(tipoOperacion, entidadId)
        {
            InitializeComponent();

            _articuloServicio = new ArticuloServicio();
            _listaPrecioServicio = new ListaPrecioServicio();
            _precioServicio = new PrecioServicio();

            if (tipoOperacion == TipoOperacion.Modificar || tipoOperacion == TipoOperacion.Eliminar)
            {
                CargarDatos(entidadId);

                cmbArticulo.Enabled = false;
                cmbListaPrecios.Enabled = false;
            }



            AsignarEventoEnterLeave(this);

            AgregarControlesObligatorios(nudPrecioPublico, "PrecioPublico");
            AgregarControlesObligatorios(nudPrecioCosto, "PrecioCosto");
            AgregarControlesObligatorios(cmbArticulo, "Articulo");
            AgregarControlesObligatorios(cmbListaPrecios, "ListaPrecio");



            Inicializador(entidadId);

            nudPrecioPublico.KeyPress += Validacion.NoSimbolos;
            nudPrecioPublico.KeyPress += Validacion.NoLetras;

            nudPrecioCosto.KeyPress += Validacion.NoSimbolos;
            nudPrecioCosto.KeyPress += Validacion.NoLetras;

            //dtpHoraVenta.Enabled = false;
        }
        public override void Inicializador(long? entidadId)
        {
            if (entidadId.HasValue) return;

            nudPrecioPublico.KeyPress += Validacion.NoSimbolos;
            nudPrecioPublico.KeyPress += Validacion.NoLetras;

            nudPrecioCosto.KeyPress += Validacion.NoSimbolos;
            nudPrecioCosto.KeyPress += Validacion.NoLetras;

            CargarComboBox(cmbArticulo, _articuloServicio.Obtener(string.Empty), "Descripcion", "Id");
            CargarComboBox(cmbListaPrecios, _listaPrecioServicio.Obtener(string.Empty), "Descripcion", "Id");


        }
        public override void CargarDatos(long? entidadId)
        {
            if (!entidadId.HasValue)
            {
                MessageBox.Show(@"Ocurrio un Error Grave", @"Error Grave", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                this.Close();
            }



            var precio = _precioServicio.ObtenerPorId(entidadId.Value);

            //nud
            nudPrecioPublico.Value = precio.PrecioPublico;


            //combo box
            CargarComboBox(cmbArticulo, _articuloServicio.Obtener(string.Empty), "Descripcion", "Id");
            CargarComboBox(cmbListaPrecios, _listaPrecioServicio.Obtener(string.Empty), "Descripcion", "Id");

            cmbArticulo.SelectedValue = precio.ArticuloId;
            cmbListaPrecios.SelectedValue = precio.ListaPrecioId;

            //fechas

            dtpFechaActualizacion.Value = precio.FechaActualizacion;
          //  dtpHoraVenta.Value = precio.HoraVenta;

         //   checkActivarHoraVenta.Checked = precio.ActivarHoraVenta;
        }
       
        public override bool EjecutarComandoNuevo()
        {

            if (_precioServicio.PrecioYaExiste(((ArticuloDto)cmbArticulo.SelectedItem).Id, ((ListaPrecioDto)cmbListaPrecios.SelectedItem).Id))
            {
                MessageBox.Show(@"Ya existe este precio cargado.", @"Atención", MessageBoxButtons.OK,
                  MessageBoxIcon.Error);
                return false;
            }

            if (!VerificarDatosObligatorios())
            {
                MessageBox.Show(@"Por favor ingrese los campos Obligatorios.", @"Atención", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }

            if (nudPrecioPublico.Value <= 0)
            {
                MessageBox.Show(@"Por favor Calcule el Precio Publico.", @"Atención", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }

            var nuevoPrecio = new PrecioDto
            {

                ListaPrecioId = ((ListaPrecioDto)cmbListaPrecios.SelectedItem).Id,
                ArticuloId = ((ArticuloDto)cmbArticulo.SelectedItem).Id,
               // ActivarHoraVenta = checkActivarHoraVenta.Checked,
                HoraVenta = DateTime.Now,
                PrecioPublico = nudPrecioPublico.Value,//* ((ListaPrecioDto)cmbListaPrecios.SelectedItem).Rentabilidad,//
                FechaActualizacion = DateTime.Now,

            };

            _precioServicio.Insertar(nuevoPrecio);

            this.Close();

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

            if (nudPrecioPublico.Value <= 0)
            {
                MessageBox.Show(@"Por favor Calcule el Precio Publico.", @"Atención", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }            

            var precioModificar = new PrecioDto
            {
                ListaPrecioId = ((ListaPrecioDto)cmbListaPrecios.SelectedItem).Id,
                ArticuloId = ((ArticuloDto)cmbArticulo.SelectedItem).Id,
               // ActivarHoraVenta = checkActivarHoraVenta.Checked,
                //HoraVenta = dtpHoraVenta.Value,
                HoraVenta = DateTime.Now,
                PrecioPublico = nudPrecioPublico.Value,
                FechaActualizacion = DateTime.Now,
                EstaEliminado = false
            };
            // En vez de Actualizar y que pise la entidad anterior, Inserta una nueva para tener un Historial de precios
            _precioServicio.Insertar(precioModificar);         

            return true;
        }

        private void BtnNuevoArticulo_Click(object sender, EventArgs e)
        {
            var fArticuloABM = new _00011_ABM_Articulos(TipoOperacion.Nuevo);
            fArticuloABM.ShowDialog();

            if (fArticuloABM.RealizoAlgunaOperacion)
            {
                CargarComboBox(cmbArticulo, _articuloServicio.Obtener(string.Empty), "Descripcion", "Id");
            }
        }
    

        private void BtnNuevaListaPrecio_Click(object sender, EventArgs e)
        {

            var flistaPrecioABM = new _00020_ABM_ListaPrecio(TipoOperacion.Nuevo);
            flistaPrecioABM.ShowDialog();

            if (flistaPrecioABM.RealizoAlgunaOperacion)
            {
                CargarComboBox(cmbListaPrecios, _listaPrecioServicio.Obtener(string.Empty), "Descripcion", "Id");
            }
        }

        /*private void CheckActivarHoraVenta_CheckedChanged_1(object sender, EventArgs e)
        {
            dtpHoraVenta.ShowUpDown = true;

            if (checkActivarHoraVenta.Checked)
            {
                dtpHoraVenta.Enabled = true;
            }
            else
            {
                dtpHoraVenta.Enabled = false;
            }


        }
        */

     

        private void CmbArticulo_SelectedIndexChanged(object sender, EventArgs e)
        {
            var articuloSeleccionado =_articuloServicio.ObtenerPorId(((ArticuloDto)cmbArticulo.SelectedItem).Id);

            nudPrecioCosto.Value = articuloSeleccionado.PrecioCosto;
        }

        private void BtnCalcular_Click(object sender, EventArgs e)
        {
            var precioCosto = nudPrecioCosto.Value;

            var listaPrecioSeleccionada = _listaPrecioServicio.ObtenerPorId(((ListaPrecioDto)cmbListaPrecios.SelectedItem).Id);
            var rentabilidadMonto = listaPrecioSeleccionada.Rentabilidad * precioCosto / 100;

            var articuloSeleccionado = _articuloServicio.ObtenerPorId(((ArticuloDto)cmbArticulo.SelectedItem).Id);
            var ivaMonto = Convert.ToDecimal(articuloSeleccionado.Iva) * (precioCosto + rentabilidadMonto) / 100;

            nudPrecioPublico.Value = precioCosto + rentabilidadMonto + ivaMonto;


        }
    }
}
