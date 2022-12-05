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
using XCommerce.Servicio.Core.CondicionIva;
using XCommerce.Servicio.Core.CondicionIva.DTOs;

namespace Presentacion.Core.CondicionIva
{
    public partial class _00036_ABM_CondicionIva : FormularioAbm
    {
        private readonly ICondicionIvaServicio _CondicionIvaServicio;


        public _00036_ABM_CondicionIva(TipoOperacion tipoOperacion, long? entidadId = null)
            : base(tipoOperacion, entidadId)
        {
            InitializeComponent();


            _CondicionIvaServicio = new CondicionIvaServicio();
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

            var condicionIva = _CondicionIvaServicio.ObtenerPorId(entidadId.Value);

            // Datos Personales
            txtDescripcion.Text = condicionIva.Descripcion;
        }

        public override bool EjecutarComandoNuevo()
        {
            if (!VerificarDatosObligatorios())
            {
                MessageBox.Show(@"Por favor ingrese los campos Obligatorios.", @"Atención", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return false;
            }

            var NuevaCondicionIvaServicio = new CondicionIvaDto
            {
                Descripcion = txtDescripcion.Text,
            };

            _CondicionIvaServicio.Insertar(NuevaCondicionIvaServicio);

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

            var CondicionIvaServicioParaModificar = new CondicionIvaDto
            {
                Id = EntidadId.Value,
                Descripcion = txtDescripcion.Text
            };

            _CondicionIvaServicio.Modificar(CondicionIvaServicioParaModificar);

            return true;
        }
        public override bool EjecutarComandoEliminar()
        {
            if (EntidadId == null) return false;

            _CondicionIvaServicio.Eliminar(EntidadId.Value);

            return true;
        }
    }
}
