using Presentacion.Core.CondicionIva;
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
using XCommerce.Servicio.Core.Proveedor;
using XCommerce.Servicio.Core.Proveedor.DTOs;

namespace Presentacion.Core.Proveedor
{
    public partial class _00034_ABM_Proveedor : FormularioAbm
    {

        private readonly IProveedorServicio _ProveedorServicio;
        private readonly ICondicionIvaServicio _condicionIvaServicio;


        public _00034_ABM_Proveedor(TipoOperacion tipoOperacion, long? entidadId = null)
            : base(tipoOperacion, entidadId)
        {
            InitializeComponent();

            _ProveedorServicio = new ProveedorServicio();
            _condicionIvaServicio = new CondicionIvaServicio();

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
            AgregarControlesObligatorios(txtRazonSocial, "Razon Social");
            AgregarControlesObligatorios(txtEmail, "Email");
            AgregarControlesObligatorios(txtContacto, "Contacto");
            AgregarControlesObligatorios(txtTel, "Telefon");
            AgregarControlesObligatorios(cmbCondicionIva, "CondicionIvaId");

            Inicializador(entidadId);
        }
        public override void Inicializador(long? entidadId)
        {
            if (entidadId.HasValue) return;

            CargarComboBox(cmbCondicionIva, _condicionIvaServicio.Obtener(string.Empty), "Descripcion", "Id");

            if (cmbCondicionIva.Items.Count > 0)
            {
                var condicionIva = (CondicionIvaDto)cmbCondicionIva.Items[0];
            }

            // Asignando un Evento
            txtDescripcion.KeyPress += Validacion.NoSimbolos;
            txtDescripcion.KeyPress += Validacion.NoNumeros;

            txtRazonSocial.KeyPress += Validacion.NoSimbolos;

            txtTel.KeyPress += Validacion.NoSimbolos;
            txtTel.KeyPress += Validacion.NoLetras;


        }
        public override void CargarDatos(long? entidadId)
        {
            txtTel.KeyPress += Validacion.NoSimbolos;
            txtTel.KeyPress += Validacion.NoLetras;
            txtDescripcion.KeyPress += Validacion.NoSimbolos;
            txtDescripcion.KeyPress += Validacion.NoNumeros;

            if (!entidadId.HasValue)
            {
                MessageBox.Show(@"Ocurrio un Error Grave", @"Error Grave", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                this.Close();
            }

            

            if (TipoOperacion == TipoOperacion.Eliminar)
            {
                btnLimpiar.Enabled = false;
            }

            var proveedor = _ProveedorServicio.ObtenerPorId(entidadId.Value);

            // Datos Personales
            txtDescripcion.Text = proveedor.Descripcion;
            txtRazonSocial.Text = proveedor.RazonSocial;
            txtTel.Text = proveedor.Telefono;
            txtEmail.Text = proveedor.Email;
            txtContacto.Text = proveedor.Contacto;
            var condicion = proveedor.CondicionIvaId;


            // CargarComboBox(cmbCondicionIva, _condicionIvaServicio.ObtenerPorIdcombo(provedoor.CondicionIvaId), "Descripcion", "Id");
            CargarComboBox(cmbCondicionIva, _condicionIvaServicio.Obtener(string.Empty), "Descripcion", "Id");

            cmbCondicionIva.SelectedValue = proveedor.CondicionIvaId;
        }
        public override bool EjecutarComandoNuevo()
        {
            if (!VerificarDatosObligatorios())
            {
                MessageBox.Show(@"Por favor ingrese los campos Obligatorios.", @"Atención", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return false;
            }

            var nuevoProveedor = new ProveedorDto
            {
                Descripcion = txtDescripcion.Text,
                RazonSocial = txtRazonSocial.Text,
                Telefono = txtTel.Text,
                Email = txtEmail.Text,
                Contacto = txtContacto.Text,
                CondicionIvaId = ((CondicionIvaDto)cmbCondicionIva.SelectedItem).Id,
            };

            _ProveedorServicio.Insertar(nuevoProveedor);

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

            var ProveedorParaModificar = new ProveedorDto
            {
                Id = EntidadId.Value,
                Descripcion = txtDescripcion.Text,
                RazonSocial = txtRazonSocial.Text,
                Contacto = txtContacto.Text,
                Telefono = txtTel.Text,
                Email = txtEmail.Text,
                CondicionIvaId = ((CondicionIvaDto)cmbCondicionIva.SelectedItem).Id
            };

            _ProveedorServicio.Modificar(ProveedorParaModificar);

            return true;
        }

        public override bool EjecutarComandoEliminar()
        {
            if (EntidadId == null) return false;

            _ProveedorServicio.Eliminar(EntidadId.Value);

            return true;
        }

        private void BtnNuevaCondicionIva_Click(object sender, EventArgs e)
        {
            var fCondicionIvaConsultaABM = new _00036_ABM_CondicionIva(TipoOperacion.Nuevo);
            fCondicionIvaConsultaABM.ShowDialog();

            if (fCondicionIvaConsultaABM.RealizoAlgunaOperacion)
            {
                CargarComboBox(cmbCondicionIva, _condicionIvaServicio.Obtener(string.Empty), "Descripcion", "Id");
            }
        }

       
    }
}

 
