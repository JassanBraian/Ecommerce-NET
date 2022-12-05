namespace Presentacion.Core.Mesa.Control
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using XCommerce.AccesoDatos;
    using Presentacion.Core.VentaSalon;
    using System.Drawing.Drawing2D;
    using XCommerce.Servicio.Core.Comprobante;
    using Presentacion.Helpers;
    using XCommerce.Servicio.Core.Reserva;
    using XCommerce.Servicio.Core.Mesa;

    public partial class ctrolMesa : UserControl
    {
        IMesaServicio _mesaServicio = new MesaServicio();

        private long _mesaId;

        public long MesaId
        {
            get
            {
                return _mesaId;
            }

            set
            {
                _mesaId = value;
            }
        }

        private int _numeroMesa;
        public int NumeroMesa
        {
            set
            {
                _numeroMesa = value;
                lblNumeroMesa.Text = value.ToString();
            }
        }

        private decimal _precioConsumidoMesa;
        public decimal PrecioConsumido
        {
            set
            {
                _precioConsumidoMesa = value;
                lblPrecio.Text = value.ToString("C");
            }
        }


        private EstadoMesa _estadoMesa;
        public EstadoMesa Estado
        {
            set
            {
                lblNumeroMesa.ForeColor = Color.White;
                lblPrecio.ForeColor = Color.White;
                menuCerrarMesa.Visible = false;
                menuAbrirMesa.Visible = false;
                menuCancelarReserva.Visible = false;

                _estadoMesa = value;

                switch (value)
                {
                    case EstadoMesa.Cerrada:
                        this.BackColor = Color.Red;
                        menuAbrirMesa.Visible = true;
                        break;
                    case EstadoMesa.Abierta:
                        this.BackColor = Color.Green;
                        menuCerrarMesa.Visible = true;
                        break;
                    case EstadoMesa.FueraServicio:
                        this.BackColor = Color.Blue;
                        break;
                    case EstadoMesa.Reservada:
                        this.BackColor = Color.Yellow;
                        lblNumeroMesa.ForeColor = Color.Black;
                        lblPrecio.ForeColor = Color.Black;
                        menuAbrirMesa.Visible = true;
                        menuCancelarReserva.Visible = true;
                        break;
                    default:
                        this.BackColor = Color.White;
                        break;
                }
            }
        }

        private readonly IComprobanteSalonServicio _comprobanteSalonServicio;
        private readonly IReservaServicio _reservarServicio = new ReservaServicio();

        //consultar como funciona
        public ctrolMesa()
            : this(new ComprobanteSalonServicio())
        {
            InitializeComponent();
            RedondearCtrol();
        }

        public ctrolMesa(IComprobanteSalonServicio comprobanteSalonServicio)
        {
            _comprobanteSalonServicio = comprobanteSalonServicio;
        }

        private void MenuAbrirMesa_Click(object sender, EventArgs e)
        {
            if (_estadoMesa == EstadoMesa.Abierta || _estadoMesa == EstadoMesa.FueraServicio) return;

            if(_estadoMesa == EstadoMesa.Reservada)
            {
                var respuesta = MessageBox.Show("Está seguro que desea Abrir la mesa Reservada?",
                    "Advertencia", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (respuesta != DialogResult.Yes) return;

                _reservarServicio.CancelarReserva(this.MesaId);
            }

            _comprobanteSalonServicio.Generar(_mesaId, Validacion.UsuarioLogeado, 1,null);

            _mesaServicio.AsignarMesaEjecutada(_mesaId);

            if (_estadoMesa == EstadoMesa.Reservada)
            {
                var comprobante = _comprobanteSalonServicio.ObtenerComprobanteMesa(_mesaId);

                var reserva = _reservarServicio.ObtenerPorMesaId(_mesaId);

                

                _comprobanteSalonServicio.DescontarMontoSenia(comprobante.Id, reserva.Senia);
            }

            Estado = EstadoMesa.Abierta;

            var fComprobanteVenta = new _00015_ComprobanteMesa(_mesaId, _numeroMesa);
            fComprobanteVenta.ShowDialog();
        }

        private void LblNumeroMesa_DoubleClick(object sender, EventArgs e)
        {
            if (_estadoMesa != EstadoMesa.Abierta) return;

            _mesaServicio.AsignarMesaEjecutada(_mesaId);

            var fComprobanteVenta = new _00015_ComprobanteMesa(_mesaId, _numeroMesa);
            fComprobanteVenta.ShowDialog();
        }

        private void RedondearCtrol()
        {
            var gp = new GraphicsPath();
            gp.AddEllipse(0, 0, this.Width - 3, this.Height - 3);
            var rg = new Region(gp);
            this.Region = rg;
        }

        private void menuCerrarMesa_Click(object sender, EventArgs e)
        {
            var comprobanteMesa = _comprobanteSalonServicio.ObtenerComprobanteMesa(_mesaId);

            _mesaServicio.AsignarMesaEjecutada(_mesaId);

            if (comprobanteMesa.Total == 0)
            {
                var respuesta = MessageBox.Show("Desea Cerrar la Mesa sin haber consumido?", "Advertencia",
                      MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (respuesta == DialogResult.Yes)
                {
                    _comprobanteSalonServicio.CerrarComprobanteSinFacturar(comprobanteMesa);
                } 
            }
            else
            {
                var fCobroMesa = new _00048_CobroMesa(comprobanteMesa);
                fCobroMesa.ShowDialog();
            }

            if (_comprobanteSalonServicio.ObtenerComprobanteMesa(_mesaId) == null)
            {
                _comprobanteSalonServicio.CerrarMesa(_mesaId);

                this.Estado = EstadoMesa.Cerrada;
            }
            else
            {
                this.Estado = EstadoMesa.Abierta;
            }
        }

        private void menuCancelarReserva_Click(object sender, EventArgs e)
        {
            var respuesta = MessageBox.Show("Está seguro de Cancelar la Reserva?", "Advertencia", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            if (respuesta != DialogResult.Yes) return;

            _reservarServicio.CancelarReserva(this.MesaId);

            this.Estado = EstadoMesa.Cerrada;
        }
    }
}
