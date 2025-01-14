﻿using Presentacion.FormularioBase;
using Presentacion.Helpers;
using System.Windows.Forms;
using Presentacion.Core.Provincia;
using XCommerce.Servicio.Core.Localidad;
using XCommerce.Servicio.Core.Localidad.DTOs;
using XCommerce.Servicio.Core.Provincia;
using XCommerce.Servicio.Core.Provincia.DTOs;

namespace Presentacion.Core.Localidad
{
    public partial class _00008_Localidad_ABM : FormularioAbm
    {
        private readonly IProvinciaServicio _provinciaServicio;
        private readonly ILocalidadServicio _localidadServicio;

        public _00008_Localidad_ABM(TipoOperacion tipoOperacion, long? entidadId = null)
            : base(tipoOperacion, entidadId)
        {
            InitializeComponent();

            _provinciaServicio = new ProvinciaServicio();
            _localidadServicio = new LocalidadServicio();

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

            CargarComboBox(cmbProvincia, _provinciaServicio.Obtener(string.Empty), "Descripcion", "Id");

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

            if (TipoOperacion == TipoOperacion.Eliminar)
            {
                btnLimpiar.Enabled = false;
            }

            CargarComboBox(cmbProvincia, _provinciaServicio.Obtener(string.Empty), "Descripcion", "Id");

            var localidad = _localidadServicio.ObtenerPorId(entidadId.Value);

            // Datos Personales
            txtDescripcion.Text = localidad.Descripcion;
        }

        public override bool EjecutarComandoNuevo()
        {
            if (!VerificarDatosObligatorios())
            {
                MessageBox.Show(@"Por favor ingrese los campos Obligatorios.", @"Atención", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }

            var nuevaLocalidad = new LocalidadDto
            {
                Descripcion = txtDescripcion.Text,
                ProvinciaId = ((ProvinciaDto)cmbProvincia.SelectedItem).Id
            };

            _localidadServicio.Insertar(nuevaLocalidad);

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

            var localidadParaModificar = new LocalidadDto
            {
                Id = EntidadId.Value,
                Descripcion = txtDescripcion.Text,
                ProvinciaId = ((ProvinciaDto)cmbProvincia.SelectedItem).Id
            };

            _localidadServicio.Modificar(localidadParaModificar);

            return true;
        }

        public override bool EjecutarComandoEliminar()
        {
            if (EntidadId == null) return false;

            _localidadServicio.Eliminar(EntidadId.Value);

            return true;
        }

        private void BtnNuevaProvincia_Click(object sender, System.EventArgs e)
        {
            var fNuevaProvincia = new _00006_Provincia_ABM(TipoOperacion.Nuevo);
            fNuevaProvincia.ShowDialog();

            if (fNuevaProvincia.RealizoAlgunaOperacion)
            {
                CargarComboBox(cmbProvincia, _provinciaServicio.Obtener(string.Empty), "Descripcion", "Id");
            }
        }
    }
}
