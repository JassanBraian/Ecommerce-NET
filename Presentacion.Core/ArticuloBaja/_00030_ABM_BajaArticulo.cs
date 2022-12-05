using Presentacion.Core.Articulo;
using Presentacion.Core.MotivoBaja;
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
using XCommerce.Servicio.Core.BajaArticulo;
using XCommerce.Servicio.Core.BajaArticulo.DTOs;
using XCommerce.Servicio.Core.MotivoBaja;
using XCommerce.Servicio.Core.MotivoBaja.DTOs;

namespace Presentacion.Core.ArticuloBaja
{
    public partial class _00030_ABM_BajaArticulo : FormularioAbm
    {

        private readonly IBajaArticuloServicio _BajaArticuloServicio;
        private readonly IArticuloServicio    _articuloServicio;
        private readonly IMotivoBajaServicio  _MotivoBajaServicio;

        /////////////////////// Constructor////////////////////////////////////////////////


        public _00030_ABM_BajaArticulo(TipoOperacion tipoOperacion, long? entidadId = null)
            : base(tipoOperacion, entidadId)
        {
            InitializeComponent();

            _articuloServicio = new ArticuloServicio();
            _MotivoBajaServicio = new MotivoBajaServicio();
            _BajaArticuloServicio = new BajaArticuloServicio();

            if (tipoOperacion == TipoOperacion.Eliminar || tipoOperacion == TipoOperacion.Modificar)
            {
                CargarDatos(entidadId);
            }

            if (tipoOperacion == TipoOperacion.Eliminar)
            {
                DesactivarControles(this);
            }

            AsignarEventoEnterLeave(this);

            AgregarControlesObligatorios(txtObservacion, "Observacion");

            AgregarControlesObligatorios(dtpFecha, "Fecha");

            AgregarControlesObligatorios(txtCantidad, "Cantidad");

            AgregarControlesObligatorios(cmbMotivoBaja, "MotivoBaja");

            AgregarControlesObligatorios(cmbArticulo, "Descripcion");

            txtCantidad.KeyPress += Validacion.NoLetras;
            txtCantidad.KeyPress += Validacion.NoSimbolos;

            txtObservacion.KeyPress += Validacion.NoSimbolos;
            txtObservacion.KeyPress += Validacion.NoNumeros;

            dtpFecha.KeyPress += Validacion.NoLetras;

            Inicializador(entidadId);

        }
        public override void Inicializador(long? entidadId)
        {
            if (entidadId.HasValue) return;

            CargarComboBox(cmbMotivoBaja, _MotivoBajaServicio.Obtener(string.Empty), "Descripcion", "Id");


            CargarComboBox(cmbArticulo, _articuloServicio.Obtener(string.Empty), "Descripcion", "Id");

            if (cmbMotivoBaja.Items.Count > 0)
            {
                var condicionIva = (MotivoBajaDto)cmbMotivoBaja.Items[0];
            }

            // Asignando un Evento
            txtObservacion.KeyPress += Validacion.NoSimbolos;
            txtObservacion.KeyPress += Validacion.NoNumeros;

            dtpFecha.KeyPress += Validacion.NoLetras;

            txtCantidad.KeyPress += Validacion.NoLetras;
            txtCantidad.KeyPress += Validacion.NoSimbolos;

            dtpFecha.Focus();

        }

        /////////////////////// Constructor////////////////////////////////////////////////


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



            var bajaArticulo = _BajaArticuloServicio.ObtenerPorId(entidadId.Value);

            CargarComboBox(cmbMotivoBaja, _MotivoBajaServicio.Obtener(string.Empty), "Descripcion", "Id");

            CargarComboBox(cmbArticulo, _articuloServicio.Obtener(string.Empty), "Descripcion", "Id");

            txtCantidad.Text = bajaArticulo.Cantidad.ToString();
            txtObservacion.Text = bajaArticulo.Observacion;

        }

        public override bool EjecutarComandoNuevo()
        {
            if (!VerificarDatosObligatorios())
            {
                MessageBox.Show(@"Por favor ingrese los campos Obligatorios.", @"Atención", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }

            var nuevaBajaArt = new BajaArticuloDto
            {
                Observacion = txtObservacion.Text,
                ArticuloId = ((ArticuloDto)cmbArticulo.SelectedItem).Id,
                Fecha = dtpFecha.Value,
                Cantidad = int.Parse(txtCantidad.Text),
                EstaEliminado = false,
                MotivoBajaId = ((MotivoBajaDto)cmbMotivoBaja.SelectedItem).Id

            };

            _BajaArticuloServicio.Insertar(nuevaBajaArt);
            _articuloServicio.DescontarStock(((ArticuloDto)cmbArticulo.SelectedItem).Id, int.Parse(txtCantidad.Text));


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

            var bajaArtParaModificar = new BajaArticuloDto
            {
                Id = EntidadId.Value,
                Observacion = txtObservacion.Text,
                
            };

            


            _BajaArticuloServicio.Modificar(bajaArtParaModificar);

            return true;
        }

        public override bool EjecutarComandoEliminar()
        {
            if (EntidadId == null) return false;

            _BajaArticuloServicio.Eliminar(EntidadId.Value);

            return true;
        }

        private void BtnMotivoBaja_Click(object sender, EventArgs e)
        {
            var fMotivoBajaABM = new _00028_ABM_MotivoBaja(TipoOperacion.Nuevo);
            fMotivoBajaABM.ShowDialog();

            if (fMotivoBajaABM.RealizoAlgunaOperacion)
            {
                CargarComboBox(cmbMotivoBaja, _MotivoBajaServicio.Obtener(string.Empty), "Descripcion", "Id");
            }
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
    }
}
