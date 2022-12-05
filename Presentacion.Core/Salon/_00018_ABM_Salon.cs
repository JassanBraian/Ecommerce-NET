namespace Presentacion.Core.Salon
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using Presentacion.Core.ListaPrecio;
    using Presentacion.FormularioBase;
    using Presentacion.Helpers;
    using XCommerce.Servicio.Core.ListaPrecio;
    using XCommerce.Servicio.Core.ListaPrecio.DTOs;
    using XCommerce.Servicio.Core.Salon;
    using XCommerce.Servicio.Core.Salon.DTOs;

    public partial class _00018_ABM_Salon : FormularioAbm
    {
        private readonly ISalonServicio _SalonServicio;
        private readonly IListaPrecioServicio _ListaPrecioServico;


        /////////////////////// Constructor////////////////////////////////////////////////


        public _00018_ABM_Salon(TipoOperacion tipoOperacion, long? entidadId = null)
            : base(tipoOperacion, entidadId)
        {
            InitializeComponent();


            _SalonServicio = new SalonServicio();
             _ListaPrecioServico = new ListaPrecioServicio();


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
            AgregarControlesObligatorios(cmbListaPrecio, "ListaPrecio");

            Inicializador(entidadId);
        }
        public override void Inicializador(long? entidadId)
        {
            if (entidadId.HasValue) return;
           
            // Asignando un Evento
            txtDescripcion.KeyPress += Validacion.NoSimbolos;
            txtDescripcion.KeyPress += Validacion.NoNumeros;

            txtDescripcion.Focus();
            CargarComboBox(cmbListaPrecio, _ListaPrecioServico.Obtener(string.Empty), "Descripcion", "Id");

        }

        /////////////////////// Constructor////////////////////////////////////////////////


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



            var salon = _SalonServicio.ObtenerPorId(entidadId.Value);

            // Datos Personales
            txtDescripcion.Text = salon.Descripcion;
            CargarComboBox(cmbListaPrecio, _ListaPrecioServico.Obtener(string.Empty), "Descripcion", "Id");

        }

        public override bool EjecutarComandoNuevo()
        {
            if (!VerificarDatosObligatorios())
            {
                MessageBox.Show(@"Por favor ingrese los campos Obligatorios.", @"Atención", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }

            var nuevoSalon = new SalonDto
            {
                Descripcion = txtDescripcion.Text,
                ListaPrecioId = ((ListaPrecioDto)cmbListaPrecio.SelectedItem).Id,
                
                
            };

            _SalonServicio.Insertar(nuevoSalon);

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

            var salonParaModificar = new SalonDto
            {
                Id = EntidadId.Value,
                Descripcion = txtDescripcion.Text,
                ListaPrecioId = ((ListaPrecioDto)cmbListaPrecio.SelectedItem).Id
            };

            _SalonServicio.Modificar(salonParaModificar);

            return true;
        }

        public override bool EjecutarComandoEliminar()
        {
            if (EntidadId == null) return false;

            _SalonServicio.Eliminar(EntidadId.Value);

            return true;
        }

        private void BtnNuevaListaPrecio_Click(object sender, EventArgs e)
        {
            var flistaPrecioABM = new _00020_ABM_ListaPrecio(TipoOperacion.Nuevo);
            flistaPrecioABM.ShowDialog();

            if (flistaPrecioABM.RealizoAlgunaOperacion)
            {
                CargarComboBox(cmbListaPrecio, _ListaPrecioServico.Obtener(string.Empty), "Descripcion", "Id");
            }
        }
    }

}

