using System.Drawing;
using System.Windows.Forms;
using Presentacion.Core.Localidad;
using Presentacion.Core.Provincia;
using Presentacion.FormularioBase;
using Presentacion.Helpers;
using XCommerce.Servicio.Core.Cliente;
using XCommerce.Servicio.Core.Cliente.DTOs;
using XCommerce.Servicio.Core.Localidad;
using XCommerce.Servicio.Core.Localidad.DTOs;
using XCommerce.Servicio.Core.Provincia;
using XCommerce.Servicio.Core.Provincia.DTOs;
using static Presentacion.Helpers.ImagenDb;

namespace Presentacion.Core.Cliente
{
    public partial class _00004_ABM_Cliente : FormularioAbm
    {
        private readonly IClienteServicio _clienteServicio;
        private readonly IProvinciaServicio _provinciaServicio;
        private readonly ILocalidadServicio _localidadServicio;

        public _00004_ABM_Cliente(TipoOperacion tipoOperacion, long? entidadId = null)
            : base(tipoOperacion, entidadId)
        {
            InitializeComponent();

            _clienteServicio = new ClienteServicio();
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

            AgregarControlesObligatorios(txtApellido, "Apellido");
            AgregarControlesObligatorios(txtNombre, "Nombre");
            AgregarControlesObligatorios(txtDni, "DNI");
            AgregarControlesObligatorios(txtCuil, "CUIL");
            AgregarControlesObligatorios(txtEmail, "E-Mail");
            AgregarControlesObligatorios(txtCalle, "Calle");

            nudCtaCte.Enabled = false;

            Inicializador(entidadId);
        }

        public override void Inicializador(long? entidadId)
        {
            if (entidadId.HasValue) return;

            CargarComboBox(cmbProvincia, _provinciaServicio.Obtener(string.Empty), "Descripcion", "Id");



            if (cmbProvincia.Items.Count > 0)
            {
                var provincia = (ProvinciaDto)cmbProvincia.Items[0];

                CargarComboBox(cmbLocalidad, _localidadServicio.ObtenerPorProvincia(provincia.Id, string.Empty), "Descripcion", "Id");
            }

            // Asignando un Evento
            txtApellido.KeyPress += Validacion.NoSimbolos;
            txtApellido.KeyPress += Validacion.NoNumeros;

            txtNombre.KeyPress += Validacion.NoSimbolos;
            txtNombre.KeyPress += Validacion.NoNumeros;

            txtDni.KeyPress += Validacion.NoSimbolos;
            txtDni.KeyPress += Validacion.NoLetras;

            txtCuil.KeyPress += Validacion.NoSimbolos;
            txtCuil.KeyPress += Validacion.NoLetras;

            txtTelefono.KeyPress += Validacion.NoSimbolos;
            txtTelefono.KeyPress += Validacion.NoLetras;

            txtCelular.KeyPress += Validacion.NoSimbolos;
            txtCelular.KeyPress += Validacion.NoLetras;


            nudCtaCte.KeyPress += Validacion.NoSimbolos;
            nudCtaCte.KeyPress += Validacion.NoLetras;
           


            imgFotoCliente.Image = Constantes.Imagen.Usuario;

            txtApellido.Focus();

            if (nudCtaCte.Value > 0)
            {
                chbCtaCte.Checked = true;
                nudCtaCte.Enabled = true;
            }
            else
            {
                chbCtaCte.Checked = false;
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

            var cliente = _clienteServicio.ObtenerPorId(entidadId.Value);

            // Datos Personales
            txtApellido.Text = cliente.Apellido;
            txtNombre.Text = cliente.Nombre;
            txtDni.Text = cliente.Dni;
            txtTelefono.Text = cliente.Telefono;
            txtCelular.Text = cliente.Celular;
            txtEmail.Text = cliente.Email;
            txtCuil.Text = cliente.Cuil;
            dtpFechaNacimiento.Value = cliente.FechaNacimiento;
            imgFotoCliente.Image = Convertir_Bytes_Imagen(cliente.Foto);
            nudCtaCte.Value = cliente.MontoMaximoCtaCte;

            // Datos Direccion
            txtCalle.Text = cliente.Calle;
            txtNumero.Text = cliente.Numero.ToString();
            txtPiso.Text = cliente.Piso;
            txtDepartamento.Text = cliente.Dpto;
            txtCasa.Text = cliente.Casa;
            txtLote.Text = cliente.Lote;
            txtManzana.Text = cliente.Mza;
            txtBarrio.Text = cliente.Barrio;

            if(nudCtaCte.Value > 0)
            {
                chbCtaCte.Checked = true;
                nudCtaCte.Enabled = true;
            }
            else
            {
                chbCtaCte.Checked = false;
            }

            CargarComboBox(cmbProvincia, _provinciaServicio.Obtener(string.Empty), "Descripcion", "Id");

            cmbProvincia.SelectedItem = cliente.ProvinciaId;

            if (cmbProvincia.Items.Count > 0)
            {
                CargarComboBox(cmbLocalidad, _localidadServicio.ObtenerPorProvincia(cliente.ProvinciaId, string.Empty), "Descripcion", "Id");
            }

            // Asignando un Evento
            txtApellido.KeyPress += Validacion.NoSimbolos;
            txtApellido.KeyPress += Validacion.NoNumeros;

            txtNombre.KeyPress += Validacion.NoSimbolos;
            txtNombre.KeyPress += Validacion.NoNumeros;

            txtDni.KeyPress += Validacion.NoSimbolos;
            txtDni.KeyPress += Validacion.NoLetras;

            txtCuil.KeyPress += Validacion.NoSimbolos;
            txtCuil.KeyPress += Validacion.NoLetras;

            txtTelefono.KeyPress += Validacion.NoSimbolos;
            txtTelefono.KeyPress += Validacion.NoLetras;

            txtCelular.KeyPress += Validacion.NoSimbolos;
            txtCelular.KeyPress += Validacion.NoLetras;

            if(Convertir_Bytes_Imagen(cliente.Foto) != Constantes.Imagen.Usuario)
            {
                imgFotoCliente.Image = Convertir_Bytes_Imagen(cliente.Foto);
            }            

            txtApellido.Focus();
        }

        public override bool EjecutarComandoNuevo()
        {
            if (!VerificarDatosObligatorios())
            {
                MessageBox.Show(@"Por favor ingrese los campos Obligatorios.", @"Atención", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }

            if(txtCuil.Text == "99999999999" || txtDni.Text == "99999999")
            {
                MessageBox.Show(@"Cuil o Dni invalido.", @"Atención", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }

            if (_clienteServicio.ExisteCuilCargado(txtCuil.Text))
            {
                MessageBox.Show(@"Ya existe un cliente cargado con el Cuil ingresado.", @"Atención", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }

            var nuevoCliente = new ClienteDto
            {
                Apellido = txtApellido.Text,
                Nombre = txtNombre.Text,
                Barrio = txtBarrio.Text,
                Calle = txtCalle.Text,
                Casa = txtCasa.Text,
                Celular = txtCelular.Text,
                Cuil = txtCuil.Text,
                Dni = txtDni.Text,
                Dpto = txtDepartamento.Text,
                Email = txtEmail.Text,
                FechaNacimiento = dtpFechaNacimiento.Value,
                Lote = txtLote.Text,
                Mza = txtManzana.Text,
                Numero = int.TryParse(txtNumero.Text, out var numero) ? numero : 0,
                Piso = txtPiso.Text,
                Telefono = txtTelefono.Text,
                LocalidadId = ((LocalidadDto)cmbLocalidad.SelectedItem).Id,
                Foto = Convertir_Imagen_Bytes(imgFotoCliente.Image),
                EstaEliminado = false,
                MontoMaximoCtaCte = nudCtaCte.Value,
                SaldoCtaCte = 0,
        };

            _clienteServicio.Insertar(nuevoCliente);

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

            if (txtCuil.Text == "99999999999" || txtDni.Text == "99999999")
            {
                MessageBox.Show(@"Cuil o Dni invalido.", @"Atención", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }

            if (_clienteServicio.ExisteCuilCargado(txtCuil.Text))
            {
                MessageBox.Show(@"Ya existe un cliente cargado con el Cuil ingresado.", @"Atención", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }

            var clienteParaModificar = new ClienteDto
            {
                Id = EntidadId.Value,
                Apellido = txtApellido.Text,
                Nombre = txtNombre.Text,
                Barrio = txtBarrio.Text,
                Calle = txtCalle.Text,
                Casa = txtCasa.Text,
                Celular = txtCelular.Text,
                Cuil = txtCuil.Text,
                Dni = txtDni.Text,
                Dpto = txtDepartamento.Text,
                Email = txtEmail.Text,
                FechaNacimiento = dtpFechaNacimiento.Value,
                Lote = txtLote.Text,
                Mza = txtManzana.Text,
                Numero = int.TryParse(txtNumero.Text, out var numero) ? numero : 0,
                Piso = txtPiso.Text,
                Telefono = txtTelefono.Text,
                LocalidadId = ((LocalidadDto)cmbLocalidad.SelectedItem).Id,
                Foto = Convertir_Imagen_Bytes(imgFotoCliente.Image),
                EstaEliminado = false,
                MontoMaximoCtaCte = nudCtaCte.Value,
            };

            _clienteServicio.Modificar(clienteParaModificar);

           
            return true;

        }

        public override bool EjecutarComandoEliminar()
        {
            if (EntidadId == null) return false;

            _clienteServicio.Eliminar(EntidadId.Value);

            return true;
        }
        
        private void CmbProvincia_SelectionChangeCommitted(object sender, System.EventArgs e)
        {
            if (cmbProvincia.Items.Count > 0)
            {
                CargarComboBox(cmbLocalidad,
                    _localidadServicio.ObtenerPorProvincia(((ProvinciaDto)cmbProvincia.SelectedItem).Id, string.Empty),
                    "Descripcion",
                    "Id");
            }
        }

        private void BtnAgregarImagen_Click(object sender, System.EventArgs e)
        {
            if (archivo.ShowDialog() == DialogResult.OK)
            {

                // Pregunta si Selecciono un Archivo
                if (!string.IsNullOrEmpty(archivo.FileName))
                {
                    imgFotoCliente.Image = Image.FromFile(archivo.FileName);
                }
                else
                {
                    imgFotoCliente.Image = Constantes.Imagen.Usuario;
                }
            }
            else
            {
                imgFotoCliente.Image = Presentacion.Constantes.Imagen.Usuario;
            }
        }

        private void BtnNuevaProvincia_Click(object sender, System.EventArgs e)
        {
            var fNuevaProvincia = new _00006_Provincia_ABM(TipoOperacion.Nuevo);
            fNuevaProvincia.ShowDialog();

            if (!fNuevaProvincia.RealizoAlgunaOperacion) return;

            CargarComboBox(cmbProvincia, _provinciaServicio.Obtener(string.Empty), "Descripcion", "Id");

            if (cmbProvincia.Items.Count > 0)
            {
                CargarComboBox(cmbLocalidad,
                    _localidadServicio.ObtenerPorProvincia(((ProvinciaDto)cmbProvincia.SelectedItem).Id, string.Empty),
                    "Descripcion", "Id");
            }
        }

        private void BtnLocalidad_Click(object sender, System.EventArgs e)
        {
            var fNuevaLocalidad = new _00008_Localidad_ABM(TipoOperacion.Nuevo);
            fNuevaLocalidad.ShowDialog();

            if (!fNuevaLocalidad.RealizoAlgunaOperacion) return;

            if (cmbProvincia.Items.Count > 0)
            {
                CargarComboBox(cmbLocalidad,
                    _localidadServicio.ObtenerPorProvincia(((ProvinciaDto)cmbProvincia.SelectedItem).Id, string.Empty),
                    "Descripcion", "Id");
            }
        }

        private void chbCtaCte_CheckedChanged(object sender, System.EventArgs e)
        {
            if (chbCtaCte.Checked == true)
            {
                nudCtaCte.Enabled = true;
            }
            else
            {
                nudCtaCte.Value = 0;
                nudCtaCte.Enabled = false;
            }
        }
    }
}
