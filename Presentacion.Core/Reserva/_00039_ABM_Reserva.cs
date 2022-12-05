using Presentacion.Core.Cliente;
using Presentacion.Core.MotivoReserva;
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
using XCommerce.AccesoDatos;
using XCommerce.Servicio.Core.Cliente;
using XCommerce.Servicio.Core.Mesa;
using XCommerce.Servicio.Core.Mesa.DTOs;
using XCommerce.Servicio.Core.MotivoReserva;
using XCommerce.Servicio.Core.MotivoReserva.DTOs;
using XCommerce.Servicio.Core.Reserva;
using XCommerce.Servicio.Core.Reserva.DTOs;
using XCommerce.Servicio.Seguridad.Usuario;

namespace Presentacion.Core.Reserva
{
    public partial class _00039_ABM_Reserva  : FormularioAbm
    {
        private readonly IReservaServicio _reservaServicio;
        private readonly IMesaServicio _mesaServicio;
        private readonly IMotivoReservaServicio _motivoReservaServicio;
        private readonly IClienteServicio _clienteServicio;
        private readonly IUsuarioServicio _usuarioServicio;

        private long clienteId;

        public _00039_ABM_Reserva(TipoOperacion tipoOperacion, long? entidadId = null)
             : base(tipoOperacion, entidadId)
        {
            InitializeComponent();

            _reservaServicio = new ReservaServicio();
            _clienteServicio = new ClienteServicio();
            _mesaServicio = new MesaServicio();
            _motivoReservaServicio = new MotivoReservaServicio();
            _usuarioServicio = new UsuarioServicio();

            if (tipoOperacion == TipoOperacion.Eliminar || tipoOperacion == TipoOperacion.Modificar)
            {
                CargarDatos(entidadId);
            }

            if (tipoOperacion == TipoOperacion.Eliminar)
            {
                DesactivarControles(this);
            }

            AsignarEventoEnterLeave(this);

            AgregarControlesObligatorios(nudSeña, "Senia");
            AgregarControlesObligatorios(txtCliente, "ClienteId");
            //AgregarControlesObligatorios(cmbUsuario, "UsuarioId");
            AgregarControlesObligatorios(cmbMotivoReserva, "MotivoReserva");
            AgregarControlesObligatorios(cmbMesa, "MesaId");
            AgregarControlesObligatorios(cmbEstadoReserva, "EstadoReserva");

            Inicializador(entidadId);

        }
        public override void Inicializador(long? entidadId)
        {
            if (entidadId.HasValue) return;

            nudSeña.KeyPress += Validacion.NoLetras;
            nudSeña.KeyPress += Validacion.NoSimbolos;

            txtCliente.KeyPress += Validacion.NoLetras;
            txtCliente.KeyPress += Validacion.NoSimbolos;

            //CargarComboBox(cmbCliente, _clienteServicio.Obtener(string.Empty), "Apellido", "Id");
            // CargarComboBox(cmbUsuario, _usuarioServicio.Obtener(string.Empty), "ApyNom", "Id");
            CargarComboBox(cmbMesa, _mesaServicio.ObtenerMesasVigentesSinReserva(string.Empty), "Numero", "Id");
            //CargarComboBox(cmbEstadoReserva, _reservaServicio.Obtener(string.Empty), "EstadoReserva", "Id");
            cmbEstadoReserva.DataSource = Enum.GetValues(typeof(EstadoReserva));
            CargarComboBox(cmbMotivoReserva, _motivoReservaServicio.Obtener(string.Empty), "Descripcion", "Id");

        }

        public override void CargarDatos(long? entidadId)
        {
            nudSeña.KeyPress += Validacion.NoLetras;
            nudSeña.KeyPress += Validacion.NoSimbolos;

            txtCliente.KeyPress += Validacion.NoLetras;
            txtCliente.KeyPress += Validacion.NoSimbolos;

            txtCliente.Enabled = false;
            cmbMesa.Enabled = false;

            if (!entidadId.HasValue)
            {
                MessageBox.Show(@"Ocurrio un Error Grave", @"Error Grave", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                this.Close();
            }

            if (TipoOperacion == TipoOperacion.Eliminar)
            {
                btnLimpiar.Enabled = false;
            }

            CargarComboBox(cmbMotivoReserva, _motivoReservaServicio.Obtener(string.Empty), "Descripcion", "Id");
            CargarComboBox(cmbMesa, _mesaServicio.Obtener(string.Empty), "Numero", "Id");
            cmbEstadoReserva.DataSource = Enum.GetValues(typeof(EstadoReserva));
            // CargarComboBox(cmbUsuario, _usuarioServicio.Obtener(string.Empty), "Descripcion", "Id");
            //CargarComboBox(cmbCliente, _clienteServicio.Obtener(string.Empty), "Apellido", "Id");

            var reserva = _reservaServicio.ObtenerPorId(entidadId.Value);

            var cliente = _clienteServicio.ObtenerPorId(reserva.ClienteId);

            //cmbCliente.SelectedValue = reserva.ClienteId;
            // cmbUsuario.SelectedValue = reserva.UsuarioId;
            txtCliente.Text = cliente.Dni;
            cmbMotivoReserva.SelectedValue = reserva.MotivoReservaId;
            cmbMesa.SelectedValue = reserva.MesaId;
            cmbEstadoReserva.SelectedItem = reserva.EstadoReserva;
            nudSeña.Value = reserva.Senia;
            //dtpFecha.Value = reserva.Fecha;
        }

        public override bool EjecutarComandoNuevo()
        {
            if (!VerificarDatosObligatorios())
            {
                MessageBox.Show(@"Por favor ingrese los campos Obligatorios.", @"Atención", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }

            if (_clienteServicio.ExisteClienteDni(txtCliente.Text))
            {
                clienteId = _clienteServicio.ObtenerIdCliente(txtCliente.Text);
            }
            else
            {
                MessageBox.Show(@"No hay Clientes cargados con ese Dni.", @"Atención", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                return false;
            }


            var nuevaReserva = new ReservaDto
            {
                Senia = nudSeña.Value,
                ClienteId = clienteId,
                UsuarioId = Validacion.UsuarioLogeado,
                MotivoReservaId = ((MotivoReservaDto)cmbMotivoReserva.SelectedItem).Id,
                EstadoReserva = (EstadoReserva)cmbEstadoReserva.SelectedItem,
                Fecha = DateTime.Now,
                //Fecha = dtpFecha.Value,
                MesaId = ((MesaDto)cmbMesa.SelectedItem).Id,
                EstaEliminado = false,


            };

            _reservaServicio.Insertar(nuevaReserva);

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

            var reservaParaModifica = new ReservaDto
            {
                Id = EntidadId.Value,
                Senia = nudSeña.Value,
                ClienteId = _clienteServicio.ObtenerIdCliente(txtCliente.Text),
                UsuarioId = Validacion.UsuarioLogeado,
                MotivoReservaId = ((MotivoReservaDto)cmbMotivoReserva.SelectedItem).Id,
                //Fecha = dtpFecha.Value,
                Fecha = DateTime.Now,
                EstadoReserva = (EstadoReserva)cmbEstadoReserva.SelectedItem,
                MesaId = ((MesaDto)cmbMesa.SelectedItem).Id,

            };

            _reservaServicio.Modificar(reservaParaModifica);

            return true;
        }

        public override bool EjecutarComandoEliminar()
        {
            if (EntidadId == null) return false;


            _reservaServicio.Eliminar(EntidadId.Value);

            return true;
        }

      

    
        private void btnNuevoMotivoReserva_Click_1(object sender, EventArgs e)
        {
            var fNuevoMotivoReserva = new _00041_ABM_MotivoReserva(TipoOperacion.Nuevo);
            fNuevoMotivoReserva.ShowDialog();
        }

        private void btnBuscarClientes_Click_1(object sender, EventArgs e)
        {
            var fBuscarClientes = new _00003_Clientes();

            fBuscarClientes.ShowDialog();
        }
    }
}
