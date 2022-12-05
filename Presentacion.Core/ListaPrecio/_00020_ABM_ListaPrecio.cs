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
using XCommerce.Servicio.Core.ListaPrecio;
using XCommerce.Servicio.Core.ListaPrecio.DTOs;
using XCommerce.Servicio.Core.Precio;

namespace Presentacion.Core.ListaPrecio
{
    public partial class _00020_ABM_ListaPrecio : FormularioAbm
    {
        private readonly IListaPrecioServicio _ListaPrecioServico;
        private readonly IPrecioServicio _precioServicio;

        /////////////////////// Constructor////////////////////////////////////////////////


        public _00020_ABM_ListaPrecio(TipoOperacion tipoOperacion, long? entidadId = null)
            : base(tipoOperacion, entidadId)
        {
            InitializeComponent();


            _ListaPrecioServico = new ListaPrecioServicio();
            _precioServicio = new PrecioServicio();

            if(entidadId != null)
            {
                var listaPrecio = _ListaPrecioServico.ObtenerPorId((long)entidadId);

                if(listaPrecio.Descripcion == "Kiosco" || listaPrecio.Descripcion == "Delivery" || listaPrecio.Descripcion == "Bar")
                {
                    txtDescripcion.Enabled = false;
                }
            }

            if (tipoOperacion == TipoOperacion.Eliminar || tipoOperacion == TipoOperacion.Modificar)
            {
                CargarDatos(entidadId);
            }

            if (tipoOperacion == TipoOperacion.Eliminar)
            {
                DesactivarControles(this);
            }

            AsignarEventoEnterLeave(this);

            AgregarControlesObligatorios(txtDescripcion, "Descripción");
            AgregarControlesObligatorios(txtRentabilidad, "Rentabilidad");

            Inicializador(entidadId);
        }
        public override void Inicializador(long? entidadId)
        {
            if (entidadId.HasValue) return;

            // Asignando un Evento
            txtDescripcion.KeyPress += Validacion.NoSimbolos;

            txtRentabilidad.KeyPress += Validacion.NoSimbolos;
            txtRentabilidad.KeyPress += Validacion.NoLetras;
            txtDescripcion.Focus();

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

            var listaprecio = _ListaPrecioServico.ObtenerPorId(entidadId.Value);


            // Datos Personales
            txtDescripcion.Text = listaprecio.Descripcion;
            txtRentabilidad.Text = listaprecio.Rentabilidad.ToString();
        }

        public override bool EjecutarComandoNuevo()
        {
            if (!VerificarDatosObligatorios())
            {
                MessageBox.Show(@"Por favor ingrese los campos Obligatorios.", @"Atención", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return false;
            }

            if (_ListaPrecioServico.ExisteListaDesripcion(txtDescripcion.Text))
            {

                MessageBox.Show(@"Ya existe una Lista de Precio con esa Descripcion.", @"Atención", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return false;
            }

            var ListaPrecioNueva = new ListaPrecioDto
            {
                Descripcion = txtDescripcion.Text,
                Rentabilidad= Convert.ToDecimal(txtRentabilidad.Text)
               
            };

            _ListaPrecioServico.Insertar(ListaPrecioNueva);


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

       

            var ListaPrecioParaModificar = new ListaPrecioDto
            {
                Id = EntidadId.Value,
                Descripcion = txtDescripcion.Text,
                Rentabilidad = decimal.Parse(txtRentabilidad.Text),
                EstaEliminado = false



            };

            _ListaPrecioServico.Modificar(ListaPrecioParaModificar);

            ActualizarPreciosPublicos();


            return true;
        }

        private void ActualizarPreciosPublicos()
        {
            var preciosPublicos = _precioServicio.ObtenerSegunListaPrecio(EntidadId.Value);

            foreach (var precioPublico in preciosPublicos)
            {
                _precioServicio.ActualizarRentabilidad(precioPublico, Convert.ToDecimal(txtRentabilidad.Text));
            }
        }

        public override bool EjecutarComandoEliminar()
        {

           
            
            
                var listaPrecio = _ListaPrecioServico.ObtenerPorId((long)EntidadId);

                if(listaPrecio.Descripcion == "Kiosco" || listaPrecio.Descripcion == "Delivery" || listaPrecio.Descripcion == "Bar")
                {
                         MessageBox.Show(@"Por favor no eliminar esta lista de precio.", @"Atención", MessageBoxButtons.OK,
                      MessageBoxIcon.Error);
                         return false;
                 }
            


            if (EntidadId == null) return false;

            _ListaPrecioServico.Eliminar(EntidadId.Value);

            return true;
        }



    }
}
