namespace Presentacion.Core.VentaSalon
{
    using Presentacion.Core.Mesa;
    using Presentacion.Core.Mesa.Control;
    using Presentacion.Core.Reserva;
    using Presentacion.Core.Salon;
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
    using XCommerce.Servicio.Core.Comprobante;
    using XCommerce.Servicio.Core.Mesa;
    using XCommerce.Servicio.Core.Salon;

    public partial class _00012_VentaSalon : FormularioBase.FormularioBase
    {
        private readonly ISalonServicio _salonServicio;
        private readonly IMesaServicio _mesaServicio;
        private readonly IComprobanteSalonServicio _comprobanteSalonServicio;

        private TabControl contenedorPagina = new TabControl();
        
        public _00012_VentaSalon()
        {
            InitializeComponent();

            _salonServicio = new SalonServicio();
            _mesaServicio = new MesaServicio();
            _comprobanteSalonServicio = new ComprobanteSalonServicio();

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {

            this.Close();

        }

        private void _00012_VentaSalon_Load(object sender, EventArgs e)
        {
            CrearControles();
        }

        private void CrearControles()
        {
            //var contenedorPagina = new TabControl();
            var contadorTabIndex = 0;
    
            foreach (var salon in _salonServicio.Obtener(string.Empty)
                .Where(x=>!x.EstaEliminado))
            {
                var listaDeMesas = _mesaServicio.ObtenerPorSalon(salon.Id, string.Empty);

                var flowPanel = new FlowLayoutPanel
                {
                    Name = $"flowPanel{salon.Id}",
                    Dock = DockStyle.Fill,
                };

                foreach (var mesa in listaDeMesas.Where(x=>!x.EstaEliminado))
                {
                    var controlMesa = new ctrolMesa
                    {
                        MesaId = mesa.Id,
                        NumeroMesa = mesa.Numero,
                        Estado = mesa.EstadoMesa,
                        PrecioConsumido = 0m
                    };

                    if(mesa.EstadoMesa == XCommerce.AccesoDatos.EstadoMesa.Abierta)
                    {
                        controlMesa.PrecioConsumido = _comprobanteSalonServicio.ObtenerComprobanteMesa(mesa.Id).Total;
                    }

                    flowPanel.Controls.Add(controlMesa);
                }

                var pagina = new TabPage
                {
                    Location = new Point(4, 22),
                    Name = $"Pagina{salon.Id}",
                    Padding = new Padding(3),
                    Size = new Size(854, 357),
                    TabIndex = contadorTabIndex,
                    Text = $"{salon.Descripcion}",
                    UseVisualStyleBackColor = true
                };

                pagina.Controls.Add(flowPanel);

                contenedorPagina.Controls.Add(pagina);

                contadorTabIndex++;
            }            

            contenedorPagina.Dock = DockStyle.Fill;
            contenedorPagina.Location = new Point(0, 66);
            contenedorPagina.Name = "Contenedor";
            contenedorPagina.SelectedIndex = 0;
            contenedorPagina.Size = new Size(862,383);
            contenedorPagina.TabIndex = 9;
            contenedorPagina.ResumeLayout(false);

            this.Controls.Add(contenedorPagina);
            this.Controls.SetChildIndex(contenedorPagina, 0);
            contenedorPagina.ResumeLayout(false);
        }

       private void _00012_VentaSalon_Activated(object sender, EventArgs e)
       {
            var _mesaId = _mesaServicio.ObtenerMesaEjecutada();

            if (_mesaId != null)
            {
                ActualizarMesaModif((long)_mesaId);

                _mesaServicio.BorrarMesaAsignada();
            }
        }

        private void ActualizarMesaModif(long mesaId)
        {
            var mesaModif = _mesaServicio.ObtenerPorId(mesaId);

            //if (mesaModif.EstadoMesa != EstadoMesa.Abierta) return;

            var comprobante = _comprobanteSalonServicio.ObtenerComprobanteMesa(mesaId);

            //if (comprobante == null) return;

            foreach (var pagina in contenedorPagina.Controls)
            {
                if (pagina is TabPage)
                {
                    foreach (var salon in ((TabPage)pagina).Controls)
                    {
                        if (salon is FlowLayoutPanel)
                        {
                            foreach (var mesa in ((FlowLayoutPanel)salon).Controls)
                            {
                                if (((ctrolMesa)mesa).MesaId == mesaModif.Id)
                                {
                                    if (comprobante == null)
                                    {
                                        ((ctrolMesa)mesa).PrecioConsumido = 0;
                                    }
                                    else ((ctrolMesa)mesa).PrecioConsumido = comprobante.Total;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void btnNuevoSalon_Click(object sender, EventArgs e)
        {
            var fSalonABM = new _00018_ABM_Salon(TipoOperacion.Nuevo);
            fSalonABM.ShowDialog();


            ActualizarPantalla();

        }

        private void ActualizarPantalla()
        {
            contenedorPagina.Controls.Clear();

            CrearControles();
        }

        private void btnNuevaMesa_Click(object sender, EventArgs e)
        {
            var fMesaABM = new _00014_ABM_Mesas(TipoOperacion.Nuevo);
            fMesaABM.ShowDialog();

            

            ActualizarPantalla();

        }

        private void btnNuevaReserva_Click(object sender, EventArgs e)
        {
            var fNuevaReserva = new _00039_ABM_Reserva(TipoOperacion.Nuevo);
            fNuevaReserva.ShowDialog();

            ActualizarPantalla();
        }

        private void btnReservas_Click(object sender, EventArgs e)
        {
            var fReservasConsulta = new _00038_Reserva();
            fReservasConsulta.ShowDialog();

            ActualizarPantalla();
        }

        private void btnUnirMesas_Click(object sender, EventArgs e)
        {
            var fUnirMesas = new _00078_UnirMesas();
            fUnirMesas.ShowDialog();
        }
    }
}
