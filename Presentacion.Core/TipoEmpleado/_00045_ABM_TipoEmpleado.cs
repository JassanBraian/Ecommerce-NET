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
using XCommerce.Servicio.Core.TipoEmpleado;
using XCommerce.Servicio.Core.TipoEmpleado.DTOs;

namespace Presentacion.Core.TipoEmpleado
{
    public partial class _00045_ABM_TipoEmpleado : FormularioAbm
    {
        private readonly ITipoEmpleadoSericio _TipoEmpleadoServicio;

        public _00045_ABM_TipoEmpleado(TipoOperacion tipoOperacion, long? entidadId = null)
            : base(tipoOperacion, entidadId)
        {
            InitializeComponent();

            _TipoEmpleadoServicio = new TipoEmpleadoServicio();

            if (entidadId != null)
            {
                var tipoEmpleado = _TipoEmpleadoServicio.ObtenerPorId((long)entidadId);

                if (tipoEmpleado.Descripcion == "Cajero" || tipoEmpleado.Descripcion == "Cadete")
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

            var tipoempleado = _TipoEmpleadoServicio.ObtenerPorId(entidadId.Value);

            // Datos Personales
            txtDescripcion.Text = tipoempleado.Descripcion;
        }
        public override bool EjecutarComandoNuevo()
        {
            if (!VerificarDatosObligatorios())
            {
                MessageBox.Show(@"Por favor ingrese los campos Obligatorios.", @"Atención", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return false;
            }

            if (_TipoEmpleadoServicio.ExisteTipoEmpleadoDescripcion(txtDescripcion.Text))
            {
                MessageBox.Show(@"Ya existe un Tipo de Empleado con la Descripcion ingresada", @"Atención", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }

            var nuevoTipoEmpleado = new TipoEmpleadoDto
            {
                Descripcion = txtDescripcion.Text,
            };

            _TipoEmpleadoServicio.Insertar(nuevoTipoEmpleado);

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

            if (_TipoEmpleadoServicio.ExisteTipoEmpleadoDescripcion(txtDescripcion.Text))
            {
                MessageBox.Show(@"Ya existe un Tipo de Empleado con la Descripcion ingresada", @"Atención", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }

            var TipoEmpleadoParaModificar = new TipoEmpleadoDto
            {
                Id = EntidadId.Value,
                Descripcion = txtDescripcion.Text
            };

            _TipoEmpleadoServicio.Modificar(TipoEmpleadoParaModificar);

            return true;
        }

        public override bool EjecutarComandoEliminar()
        {
            if (EntidadId == null) return false;

            _TipoEmpleadoServicio.Eliminar(EntidadId.Value);

            return true;
        }
    }
}
