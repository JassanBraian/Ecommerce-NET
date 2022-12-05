namespace Presentacion.Core.Empleado
{
    using System.Drawing;
    using System.Windows.Forms;
    using Presentacion.Core.Localidad;
    using Presentacion.Core.Provincia;
    using Presentacion.Core.TipoEmpleado;
    using Presentacion.FormularioBase;
    using Presentacion.Helpers;
    using XCommerce.Servicio.Core.Empleado;
    using XCommerce.Servicio.Core.Empleado.DTOs;
    using XCommerce.Servicio.Core.Localidad;
    using XCommerce.Servicio.Core.Localidad.DTOs;
    using XCommerce.Servicio.Core.Provincia;
    using XCommerce.Servicio.Core.Provincia.DTOs;
    using XCommerce.Servicio.Core.TipoEmpleado;
    using XCommerce.Servicio.Core.TipoEmpleado.DTOs;
    using static Presentacion.Helpers.ImagenDb;

    public sealed partial class _00002_ABM_Empleados : FormularioAbm
    {
        private readonly IEmpleadoServicio _empleadoServicio;
        private readonly IProvinciaServicio _provinciaServicio;
        private readonly ILocalidadServicio _localidadServicio;
        private readonly ITipoEmpleadoSericio _tipoEmpleadoServicio;

        public _00002_ABM_Empleados(TipoOperacion tipoOperacion, long? entidadId = null)
            : base(tipoOperacion, entidadId)
        {
            InitializeComponent();

            _empleadoServicio = new EmpleadoServicio();
            _provinciaServicio = new ProvinciaServicio();
            _localidadServicio = new LocalidadServicio();
            _tipoEmpleadoServicio = new TipoEmpleadoServicio();

            if (tipoOperacion == TipoOperacion.Eliminar || tipoOperacion == TipoOperacion.Modificar)
            {
                CargarDatos(entidadId);
            }

            if (tipoOperacion == TipoOperacion.Eliminar)
            {
                DesactivarControles(this);
            }

            AsignarEventoEnterLeave(this);

            AgregarControlesObligatorios(nudLegajo, "Legajo");
            AgregarControlesObligatorios(txtApellido, "Apellido");
            AgregarControlesObligatorios(txtNombre, "Nombre");
            AgregarControlesObligatorios(txtDni, "DNI");
            AgregarControlesObligatorios(txtCuil, "CUIL");
            AgregarControlesObligatorios(txtEmail, "E-Mail");
            AgregarControlesObligatorios(txtCalle, "Calle");

            Inicializador(entidadId);
        }

        public override void Inicializador(long? entidadId)
        {
            if (entidadId.HasValue) return;

            CargarComboBox(cmbTipoEmpleado, _tipoEmpleadoServicio.Obtener(string.Empty), "Descripcion", "Id");

            CargarComboBox(cmbProvincia, _provinciaServicio.Obtener(string.Empty), "Descripcion", "Id");

            if (cmbProvincia.Items.Count > 0)
            {
                var provincia = (ProvinciaDto)cmbProvincia.Items[0];

                CargarComboBox(cmbLocalidad, _localidadServicio.ObtenerPorProvincia(provincia.Id, string.Empty), "Descripcion", "Id");
            }

            nudLegajo.Minimum = 1;
            nudLegajo.Maximum = 99999999;
            nudLegajo.Value = _empleadoServicio.ObtenerSiguienteLegajo();

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

            imgFotoEmpleado.Image = Constantes.Imagen.Usuario;

            txtApellido.Focus();
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

            var empleado = _empleadoServicio.ObtenerPorId(entidadId.Value);

            // Datos Personales
            nudLegajo.Minimum = 1;
            nudLegajo.Maximum = 9999999999;
            nudLegajo.Value = empleado.Legajo;

            txtApellido.Text = empleado.Apellido;
            txtNombre.Text = empleado.Nombre;
            txtDni.Text = empleado.Dni;
            txtTelefono.Text = empleado.Telefono;
            txtCelular.Text = empleado.Celular;
            txtEmail.Text = empleado.Email;
            txtCuil.Text = empleado.Cuil;
            dtpFechaNacimiento.Value = empleado.FechaNacimiento;
            imgFotoEmpleado.Image = Convertir_Bytes_Imagen(empleado.Foto);

            // Datos Direccion
            txtCalle.Text = empleado.Calle;
            txtNumero.Text = empleado.Numero.ToString();
            txtPiso.Text = empleado.Piso;
            txtDepartamento.Text = empleado.Dpto;
            txtCasa.Text = empleado.Casa;
            txtLote.Text = empleado.Lote;
            txtManzana.Text = empleado.Mza;
            txtBarrio.Text = empleado.Barrio;

            CargarComboBox(cmbTipoEmpleado, _tipoEmpleadoServicio.Obtener(string.Empty), "Descripcion", "Id");

            CargarComboBox(cmbProvincia, _provinciaServicio.Obtener(string.Empty), "Descripcion", "Id");

            cmbProvincia.SelectedItem = empleado.ProvinciaId;

            if (cmbProvincia.Items.Count > 0)
            {
                CargarComboBox(cmbLocalidad, _localidadServicio.ObtenerPorProvincia(empleado.ProvinciaId, string.Empty), "Descripcion", "Id");
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


            if (Convertir_Bytes_Imagen(empleado.Foto) != Constantes.Imagen.Usuario)
            {
                imgFotoEmpleado.Image = Convertir_Bytes_Imagen(empleado.Foto);
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

            var nuevoEmpleado = new EmpleadoDto
            {
                Apellido = txtApellido.Text,
                Nombre = txtNombre.Text,
                Legajo = (int)nudLegajo.Value,
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
                //ProvinciaId = ((ProvinciaDto)cmbProvincia.SelectedItem).Id,
                TipoEmpleadoId = ((TipoEmpleadoDto)cmbTipoEmpleado.SelectedItem).Id,
                Foto = Convertir_Imagen_Bytes(imgFotoEmpleado.Image),
                EstaEliminado = false,
                FechaIngreso = dtpFechaIngreso.Value,

            };

            _empleadoServicio.Insertar(nuevoEmpleado);

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

            var empleadoParaModificar = new EmpleadoDto
            {
                Id = EntidadId.Value,
                Apellido = txtApellido.Text,
                Nombre = txtNombre.Text,
                Legajo = (int)nudLegajo.Value,
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
                //ProvinciaId = ((ProvinciaDto)cmbProvincia.SelectedItem).Id,
                TipoEmpleadoId = ((TipoEmpleadoDto)cmbTipoEmpleado.SelectedItem).Id,
                Foto = Convertir_Imagen_Bytes(imgFotoEmpleado.Image),
                EstaEliminado = false,
                FechaIngreso = dtpFechaIngreso.Value
            };

            _empleadoServicio.Modificar(empleadoParaModificar);

            return true;
        }

        public override bool EjecutarComandoEliminar()
        {
            if (EntidadId == null) return false;

            _empleadoServicio.Eliminar(EntidadId.Value);

            return true;
        }

        public override void EjecutarComando()
        {
            base.EjecutarComando();

            if (TipoOperacion == TipoOperacion.Nuevo)
                nudLegajo.Value = _empleadoServicio.ObtenerSiguienteLegajo();
        }

        private void cmbProvincia_SelectionChangeCommitted(object sender, System.EventArgs e)
        {
            if (cmbProvincia.Items.Count > 0)
            {
                CargarComboBox(cmbLocalidad,
                    _localidadServicio.ObtenerPorProvincia(((ProvinciaDto)cmbProvincia.SelectedItem).Id, string.Empty),
                    "Descripcion",
                    "Id");
            }
        }

        private void btnAgregarImagen_Click(object sender, System.EventArgs e)
        {
            if (archivo.ShowDialog() == DialogResult.OK)
            {

                // Pregunta si Selecciono un Archivo
                if (!string.IsNullOrEmpty(archivo.FileName))
                {
                    imgFotoEmpleado.Image = Image.FromFile(archivo.FileName);
                }
                else
                {
                    imgFotoEmpleado.Image = Constantes.Imagen.Usuario;
                }
            }
            else
            {
                imgFotoEmpleado.Image = Presentacion.Constantes.Imagen.Usuario;
            }
        }

        private void btnNuevaProvincia_Click(object sender, System.EventArgs e)
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

        private void btnLocalidad_Click(object sender, System.EventArgs e)
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

      

        private void btnTipoEmpleado_Click_1(object sender, System.EventArgs e)
        {
            var fTipoEmpleadoABM = new _00045_ABM_TipoEmpleado(TipoOperacion.Nuevo);
            fTipoEmpleadoABM.ShowDialog();

            if (!fTipoEmpleadoABM.RealizoAlgunaOperacion) return;

            CargarComboBox(cmbTipoEmpleado, _tipoEmpleadoServicio.Obtener(string.Empty), "Descripcion", "Id");
        }
    }
}