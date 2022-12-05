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
using XCommerce.Servicio.Core.Marca;
using XCommerce.Servicio.Core.Marca.DTOs;

namespace Presentacion.Core.Marcas
{
    public partial class _00023_ABM_Marca : FormularioAbm
    {


        private readonly IMarcaServicio _MarcaServicio;


        public _00023_ABM_Marca(TipoOperacion tipoOperacion, long? entidadId = null)
            : base(tipoOperacion, entidadId)
        {
            InitializeComponent();

            _MarcaServicio = new MarcaServicio();

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
          //  txtDescripcion.KeyPress += Validacion.NoSimbolos;
          //  txtDescripcion.KeyPress += Validacion.NoNumeros;

           // txtDescripcion.Focus();
        }
        public override void CargarDatos(long? entidadId)
        {
            if (!entidadId.HasValue)
            {
                MessageBox.Show(@"Ocurrio un Error Grave", @"Error Grave", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                this.Close();
            }

           // txtDescripcion.KeyPress += Validacion.NoSimbolos;
           // txtDescripcion.KeyPress += Validacion.NoNumeros;

            if (TipoOperacion == TipoOperacion.Eliminar)
            {
                btnLimpiar.Enabled = false;
            }

            var marca = _MarcaServicio.ObtenerPorId(entidadId.Value);

            // Datos Personales
            txtDescripcion.Text = marca.Descripcion;
        }
        public override bool EjecutarComandoNuevo()
        {
            if (!VerificarDatosObligatorios())
            {
                MessageBox.Show(@"Por favor ingrese los campos Obligatorios.", @"Atención", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return false;
            }

            var nuevaMarca = new MarcaDto
            {
                Descripcion = txtDescripcion.Text,
            };


            _MarcaServicio.Insertar(nuevaMarca);

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

            var MarcaParaModificar = new MarcaDto
            {
                Id = EntidadId.Value,
                Descripcion = txtDescripcion.Text
            };

            _MarcaServicio.Modificar(MarcaParaModificar);

            return true;
        }

        public override bool EjecutarComandoEliminar()
        {
            if (EntidadId == null) return false;

            _MarcaServicio.Eliminar(EntidadId.Value);
            

            return true;
        }

        
    }
}
