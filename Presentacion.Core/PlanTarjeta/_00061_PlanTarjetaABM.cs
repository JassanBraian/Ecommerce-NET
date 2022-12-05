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
using XCommerce.Servicio.Core.PlanTarjeta;
using XCommerce.Servicio.Core.PlanTarjeta.DTOs;
using XCommerce.Servicio.Core.Tarjeta;
using XCommerce.Servicio.Core.Tarjeta.DTOs;

namespace Presentacion.Core.PlanTarjeta
{
    public partial class _00061_PlanTarjetaABM : FormularioAbm
    {
        private readonly IPlanTarjeta _PlanTarjetaServicio;
        private readonly ITarjetaServicio _TarjetaServicio;

        public _00061_PlanTarjetaABM(TipoOperacion tipoOperacion, long? entidadId = null)
            : base(tipoOperacion, entidadId)
        {
            InitializeComponent();

            _PlanTarjetaServicio = new PlanTarjetaServicio();
            _TarjetaServicio = new TarjetaServicio();

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
            AgregarControlesObligatorios(nudAlicuota, "Alicuota");

            Inicializador(entidadId);
        }

        public override void Inicializador(long? entidadId)
        {
            if (entidadId.HasValue) return;

            // Asignando un Evento
            txtDescripcion.KeyPress += Validacion.NoSimbolos;
            nudAlicuota.KeyPress += Validacion.NoSimbolos;
            nudAlicuota.KeyPress += Validacion.NoLetras;
            //nudAlicuota.KeyPress += Validacion.NoPuntos;
            


            CargarComboBox(cmbTarjeta, _TarjetaServicio.Obtener(string.Empty), "Descripcion", "Id");

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

            txtDescripcion.KeyPress += Validacion.NoSimbolos;
            txtDescripcion.KeyPress += Validacion.NoNumeros;

            CargarComboBox(cmbTarjeta, _TarjetaServicio.Obtener(string.Empty), "Descripcion", "Id");

            var planTarjeta = _PlanTarjetaServicio.ObtenerPorId(entidadId.Value);

            // Datos Personales
            txtDescripcion.Text = planTarjeta.Descripcion;
        }

        public override bool EjecutarComandoNuevo()
        {
            if (!VerificarDatosObligatorios())
            {
                MessageBox.Show(@"Por favor ingrese los campos Obligatorios.", @"Atención", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return false;
            }

            var PlanTarjetaNueva = new PlanTarjetaDto
            {
                Descripcion = txtDescripcion.Text,
                Alicuota= nudAlicuota.Value,
                TarjetaId = ((TarjetaDto)cmbTarjeta.SelectedItem).Id

            };

            _PlanTarjetaServicio.Insertar(PlanTarjetaNueva);

            CargarComboBox(cmbTarjeta, _TarjetaServicio.Obtener(string.Empty), "Descripcion", "Id");

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

            var PlantarjetaModificar = new PlanTarjetaDto
            {
                Id = EntidadId.Value,
                Descripcion = txtDescripcion.Text,
                Alicuota=nudAlicuota.Value,
                TarjetaId = ((TarjetaDto)cmbTarjeta.SelectedItem).Id
            };

            _PlanTarjetaServicio.Modificar(PlantarjetaModificar);

            return true;
        }

        public override bool EjecutarComandoEliminar()
        {
            if (EntidadId == null) return false;

            _PlanTarjetaServicio.Eliminar(EntidadId.Value);

            return true;
        }
    }
}
