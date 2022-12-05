using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Presentacion.Core.Salon;
using Presentacion.FormularioBase;
using Presentacion.Helpers;
using XCommerce.AccesoDatos;
using XCommerce.Servicio.Core.Mesa;
using XCommerce.Servicio.Core.Mesa.DTOs;
using XCommerce.Servicio.Core.Salon;
using XCommerce.Servicio.Core.Salon.DTOs;

namespace Presentacion.Core.Mesa
{
    

    public partial class _00014_ABM_Mesas : FormularioAbm
    {
        private readonly IMesaServicio _mesaServicio;
        private readonly ISalonServicio _salonServicio;


        public _00014_ABM_Mesas(TipoOperacion tipoOperacion, long? entidadId = null)
            : base(tipoOperacion, entidadId)
        {
            InitializeComponent();

            _salonServicio = new SalonServicio();
            _mesaServicio = new MesaServicio();

            if (tipoOperacion == TipoOperacion.Eliminar || tipoOperacion == TipoOperacion.Modificar)
            {
                CargarDatos(entidadId);
            }

            if (tipoOperacion == TipoOperacion.Eliminar)
            {
                DesactivarControles(this);
            }

            if(tipoOperacion == TipoOperacion.Nuevo)
            {
                nudNumeroMesa.Value = _mesaServicio.ObtenerNuevoNumeroMesa();

                nudNumeroMesa.Enabled = false;

                txtDescipcion.Text = "Mesa ";

                txtDescipcion.Focus();
            }

            AsignarEventoEnterLeave(this);

            AgregarControlesObligatorios(txtDescipcion, "Descripción");
            AgregarControlesObligatorios(cmbSalon, "Descripción");
            AgregarControlesObligatorios(nudNumeroMesa, "Numero");

            Inicializador(entidadId);
        }
        public override void Inicializador(long? entidadId)
        {
            if (entidadId.HasValue) return;

            CargarComboBox(cmbSalon, _salonServicio.ObtenerSalonesExistente(string.Empty), "Descripcion", "Id");

            // Asignando un Evento
            txtDescipcion.KeyPress += Validacion.NoSimbolos;
            txtDescipcion.KeyPress += Validacion.NoNumeros;

            nudNumeroMesa.KeyPress += Validacion.NoLetras;
            nudNumeroMesa.KeyPress += Validacion.NoSimbolos;

            nudNumeroMesa.Focus();
        }
        public override bool EjecutarComandoNuevo()
        {
            if (!VerificarDatosObligatorios())
            {
                MessageBox.Show(@"Por favor ingrese los campos Obligatorios.", @"Atención", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }

            if (_mesaServicio.ExisteMesaConNumero((int)nudNumeroMesa.Value))
            {
                MessageBox.Show(@"El numero de mesa que desea ingresar ya existe.", @"Atención", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                return false;
            }

            var nuevaMesa = new MesaDto
            {
                Numero = (int)nudNumeroMesa.Value,
                Descripcion = txtDescipcion.Text,
                SalonId = ((SalonDto)cmbSalon.SelectedItem).Id,
                EstadoMesa = EstadoMesa.Cerrada,
                EstaEliminado = false,

            };

            _mesaServicio.Insertar(nuevaMesa);

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


            var MesaParaModificar = new MesaDto
            {
                Id = EntidadId.Value,
                Numero = (int)nudNumeroMesa.Value,
                Descripcion = txtDescipcion.Text,
                SalonId = ((SalonDto)cmbSalon.SelectedItem).Id
            };

            _mesaServicio.Modificar(MesaParaModificar);

            return true;
        }
        public override bool EjecutarComandoEliminar()
        {
            if (EntidadId == null) return false;

            _mesaServicio.Eliminar(EntidadId.Value);

            return true;
        }

        private void BtnAgregarNuevoSalon_Click(object sender, EventArgs e)
        {
            var fSalonABM = new _00018_ABM_Salon(TipoOperacion.Nuevo);
            fSalonABM.ShowDialog();

            if (fSalonABM.RealizoAlgunaOperacion)
            {
                CargarComboBox(cmbSalon, _salonServicio.Obtener(string.Empty), "Descripcion", "Id");
            }
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

            CargarComboBox(cmbSalon, _salonServicio.Obtener(string.Empty), "Descripcion", "Id");

            var mesa = _mesaServicio.ObtenerPorId(entidadId.Value);

            txtDescipcion.Text = mesa.Descripcion;
            nudNumeroMesa.Value = mesa.Numero;
            cmbSalon.SelectedValue = mesa.SalonId;

        }
    }
}
