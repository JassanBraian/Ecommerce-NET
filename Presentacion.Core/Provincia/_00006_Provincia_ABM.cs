using Presentacion.FormularioBase;
using Presentacion.Helpers;
using System.Windows.Forms;
using XCommerce.Servicio.Core.Provincia;
using XCommerce.Servicio.Core.Provincia.DTOs;

namespace Presentacion.Core.Provincia
{
    public partial class _00006_Provincia_ABM : FormularioAbm
    {
        private readonly IProvinciaServicio _provinciaServicio;

        public _00006_Provincia_ABM(TipoOperacion tipoOperacion, long?  entidadId = null)
            : base(tipoOperacion, entidadId)
        {
            InitializeComponent();

            _provinciaServicio = new ProvinciaServicio();

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

            Inicializador(entidadId);
        }
        public override void Inicializador(long? entidadId)
        {
            if (entidadId.HasValue) return;

            // Asignando un Evento
            txtDescripcion.KeyPress += Validacion.NoSimbolos;
            txtDescripcion.KeyPress += Validacion.NoNumeros;

            txtDescripcion.Focus();
        }


        public override void CargarDatos(long? entidadId)
        {
            if (!entidadId.HasValue)
            {
                MessageBox.Show(@"Ocurrio un Error Grave", @"Error Grave", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                this.Close();
            }

            txtDescripcion.KeyPress += Validacion.NoSimbolos;
            txtDescripcion.KeyPress += Validacion.NoNumeros;

            if (TipoOperacion == TipoOperacion.Eliminar)
            {
                btnLimpiar.Enabled = false;
            }

            var provincia = _provinciaServicio.ObtenerPorId(entidadId.Value);

            // Datos Personales
            txtDescripcion.Text = provincia.Descripcion;
        }

        public override bool EjecutarComandoNuevo()
        {
            if (!VerificarDatosObligatorios())
            {
                MessageBox.Show(@"Por favor ingrese los campos Obligatorios.", @"Atención", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return false;
            }

            var nuevaProvincia = new ProvinciaDto
            {
                Descripcion = txtDescripcion.Text,
            };

            _provinciaServicio.Insertar(nuevaProvincia);

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

            var provinciaParaModificar = new ProvinciaDto
            {
                Id = EntidadId.Value,
                Descripcion = txtDescripcion.Text
            };

            _provinciaServicio.Modificar(provinciaParaModificar);

            return true;
        }
     
        public override bool EjecutarComandoEliminar()
        {
            if (EntidadId == null) return false;

            _provinciaServicio.Eliminar(EntidadId.Value);

            return true;
        }
    }
}
