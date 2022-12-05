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
using XCommerce.Servicio.Core.Banco;
using XCommerce.Servicio.Core.Banco.DTOs;

namespace Presentacion.Core.Banco
{
    public partial class _00022_ABM_Banco : FormularioAbm
    {
        private readonly IBancoServicio _BancoServicio;


        public _00022_ABM_Banco(TipoOperacion tipoOperacion, long? entidadId = null)
            : base(tipoOperacion, entidadId)
        {
            InitializeComponent();

            _BancoServicio = new BancoServicio();

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

            var banco = _BancoServicio.ObtenerPorId(entidadId.Value);

            // Datos Personales
            txtDescripcion.Text = banco.Descripcion;
        }
        public override bool EjecutarComandoNuevo()
        {
            if (!VerificarDatosObligatorios())
            {
                MessageBox.Show(@"Por favor ingrese los campos Obligatorios.", @"Atención", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return false;
            }

            var nuevoBanco = new BancoDto
            {
                Descripcion = txtDescripcion.Text,
            };

            _BancoServicio.Insertar(nuevoBanco);

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

            var BancoParaModificar = new BancoDto
            {
                Id = EntidadId.Value,
                Descripcion = txtDescripcion.Text
            };

            _BancoServicio.Modificar(BancoParaModificar);

            return true;
        }

        public override bool EjecutarComandoEliminar()
        {
            if (EntidadId == null) return false;

            _BancoServicio.Eliminar(EntidadId.Value);

            return true;
        }

    }
}
