namespace XCommerce
{
    using System.Windows.Forms;
    using Presentacion.Core.Localidad;
    using Presentacion.Core.Provincia;
    using Presentacion.Helpers;
    using Presentacion.Seguridad.Usuarios;
    using Presentacion.Core.VentaSalon;
    using Presentacion.Core.Salon;
    using Presentacion.Core.Cliente;
    using Presentacion.Core.Empleado;
    using Presentacion.FormularioBase;
    using Presentacion.Core.Mesa;
    using Presentacion.Core.ListaPrecio;
    using Presentacion.Core.Banco;
    using Presentacion.Core.Marcas;
    using Presentacion.Core.Rubro;
    using Presentacion.Core.MotivoBaja;
    using Presentacion.Core.ArticuloBaja;
    using Presentacion.Core.Articulo;
    using Presentacion.Core.Precio;
    using Presentacion.Core.Proveedor;
    using Presentacion.Core.CondicionIva;
    using Presentacion.Core.Reserva;
    using Presentacion.Core.MotivoReserva;
    using Presentacion.Core.Caja;
    using Presentacion.Core.TipoEmpleado;
    using Presentacion.Core.Movimiento;
    using Presentacion.Core.Kiosco;
    using Presentacion.Core.Delivery;
    using Presentacion.Core.CompraMercaderia;
    using XCommerce.Servicio.Core.Caja;
    using XCommerce.Servicio.Core.Caja.DTOs;
    using Presentacion.Core.Tarjeta;
    using Presentacion.Core.PlanTarjeta;
    using Presentacion.Core.FormaPago;
    using System;

    public partial class Principal : Form
    {
        ICajaServicio _cajaServicio = new CajaServicio();

        public Principal()
        {
            InitializeComponent();
        }

        private void ConsultaDeEmpleadosToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            var fEmpleados = new _00001_Empleados();
            fEmpleados.Show();
        }

        private void ConsultaToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            var fProvincia = new _00005_Provincia();
            fProvincia.ShowDialog();
        }

        private void NuevaProvinciaToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            var fNuevaProvincia = new _00006_Provincia_ABM(TipoOperacion.Nuevo);
            fNuevaProvincia.ShowDialog();
        }

        private void ConsultaToolStripMenuItem1_Click(object sender, System.EventArgs e)
        {
            var fLocalidad = new _00007_Localidad();
            fLocalidad.ShowDialog();
        }

        private void NuevaLocalidadToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            var fNuevaLocalidad = new _00008_Localidad_ABM(TipoOperacion.Nuevo);
            fNuevaLocalidad.ShowDialog();
        }

        private void NuevoEmpleadoToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            var fNuevoEmpleado = new _00002_ABM_Empleados(TipoOperacion.Nuevo);
            fNuevoEmpleado.ShowDialog();
        }

        private void UsuariosToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            var fUsuarios = new _00009_Usuarios();
            fUsuarios.ShowDialog();
        }

        private void VentaEnSalonToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            if (_cajaServicio.ObtenerEstadoCaja() == false)
            {
                MessageBox.Show("no se encuentra ninguna caja abierta porfavor abrir caja ");

                return;
            }
            var fVentaSalon = new _00012_VentaSalon();
            fVentaSalon.ShowDialog();
        }

        private void ConsultaToolStripMenuItem2_Click(object sender, System.EventArgs e)
        {
            var fSalon = new _00017_Salon();
            fSalon.ShowDialog();
        }

        private void NuevoSalonToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            var fSalonABM = new _00018_ABM_Salon(TipoOperacion.Nuevo);
            fSalonABM.ShowDialog();
        }

        private void ConsultaDeClienteToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            var fClientes = new _00003_Clientes();
            fClientes.ShowDialog();
        }

        private void NuevoClienteToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            var fClientesABM = new _00004_ABM_Cliente(TipoOperacion.Nuevo);
            fClientesABM.ShowDialog();
        }

        private void ConsultaToolStripMenuItem3_Click(object sender, System.EventArgs e)
        {
            var fMesa = new _00013_Mesas();
            fMesa.ShowDialog();
        }

        private void NuevaMesaToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            var fMesaABM = new _00014_ABM_Mesas(TipoOperacion.Nuevo);
            fMesaABM.ShowDialog();
        }

      
    
        private void ConsultaToolStripMenuItem5_Click(object sender, System.EventArgs e)
        {
            var fBanco = new _00021_Banco();
            fBanco.ShowDialog();
        }

        private void NuevoBancoToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            var fBancoABM = new _00022_ABM_Banco(TipoOperacion.Nuevo);
            fBancoABM.ShowDialog();
        }

        private void ConsultaToolStripMenuItem6_Click(object sender, System.EventArgs e)
        {
            var flistaPrecio = new _00019_ListaPrecio();
            flistaPrecio.ShowDialog();
        }

        private void NuevaListaToolStripMenuItem1_Click(object sender, System.EventArgs e)
        {
            var flistaPrecioABM = new _00020_ABM_ListaPrecio(TipoOperacion.Nuevo);
            flistaPrecioABM.ShowDialog();
        }

        private void ConsultaToolStripMenuItem4_Click(object sender, System.EventArgs e)
        {
            var fMarca = new _00024_Marca();
            fMarca.ShowDialog();
        }

        private void NuevaMarcaToolStripMenuItem_Click(object sender, System.EventArgs e)
        {

            var fMarcaABM = new _00023_ABM_Marca(TipoOperacion.Nuevo);
            fMarcaABM.ShowDialog();
        }

        private void ConsultaToolStripMenuItem7_Click(object sender, System.EventArgs e)
        {
            var fRubro = new _00025_Rubro();
            fRubro.ShowDialog();
        }

        private void NuevoRubroToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            var fRubroABM = new _00026_ABM_Rubro(TipoOperacion.Nuevo);
            fRubroABM.ShowDialog();
        }

        private void ConsultaToolStripMenuItem8_Click(object sender, System.EventArgs e)
        {
            var fMotivoBaja = new _00027_MotivoBaja();
            fMotivoBaja.ShowDialog();
        }

        private void NuevoMotivoToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            var fMotivoBajaABM = new _00028_ABM_MotivoBaja(TipoOperacion.Nuevo);
            fMotivoBajaABM.ShowDialog();
        }

        private void ConsultaToolStripMenuItem9_Click(object sender, System.EventArgs e)
        {
            var fBajaArticulo = new _00029_BajaArticuloConsulta();
            fBajaArticulo.ShowDialog();
        }

        private void NuevaBajaToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            var fBajaArticuloABM = new _00030_ABM_BajaArticulo(TipoOperacion.Nuevo);
            fBajaArticuloABM.ShowDialog();
        }

        private void ConsultaToolStripMenuItem10_Click(object sender, System.EventArgs e)
        {
            var fArticulo = new _00010_Articulos();
            fArticulo.ShowDialog();
        }

        private void NuevoArticuloToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            var fArticuloABM = new _00011_ABM_Articulos(TipoOperacion.Nuevo);
            fArticuloABM.ShowDialog();
        }

        private void ConsultaToolStripMenuItem11_Click(object sender, System.EventArgs e)
        {
            var fPrecio = new _00031_Precio();
            fPrecio.ShowDialog();
        }

        private void NuevoPrecioToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            var fPrecioABM = new _00032_ABM_Precio(TipoOperacion.Nuevo);
            fPrecioABM.ShowDialog();
        }

        private void ConsultaToolStripMenuItem12_Click(object sender, System.EventArgs e)
        {
            var fProveedor = new _00033_Proveedor();
            fProveedor.ShowDialog();
        }

        private void NuevoProveedorToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            var fProveedorABM = new _00034_ABM_Proveedor(TipoOperacion.Nuevo);
            fProveedorABM.ShowDialog();
        }

        private void ConsultaToolStripMenuItem13_Click(object sender, System.EventArgs e)
        {
            var fCondicionIvaConsulta = new _00035_CondicionIvaConsulta();
            fCondicionIvaConsulta.ShowDialog();
        }

        private void NuevaCondicionIvaToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            var fCondicionIvaConsultaABM = new _00036_ABM_CondicionIva(TipoOperacion.Nuevo);
            fCondicionIvaConsultaABM.ShowDialog();
        }

        private void ConsultaToolStripMenuItem14_Click(object sender, System.EventArgs e)
        {
            var fReserva = new _00038_Reserva();
            fReserva.ShowDialog();
        }

        private void NuevaReservaToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            var fReservaABM = new _00039_ABM_Reserva(TipoOperacion.Nuevo);
            fReservaABM.ShowDialog();
        }

        private void ConsultaToolStripMenuItem15_Click(object sender, System.EventArgs e)
        {
            var fMotivoReserva = new _00040_MotivoReserva();
            fMotivoReserva.ShowDialog();
        }

        private void NuevoMotivoReservaToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            var fMotivoReservaABM = new _00041_ABM_MotivoReserva(TipoOperacion.Nuevo);
            fMotivoReservaABM.ShowDialog();
        }

        

        private void ConsultaCajaToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            var fCajaConsulta = new _00043_ConsultaCaja();
            fCajaConsulta.ShowDialog();
        }

        private void CajaToolStripMenuItem1_Click(object sender, System.EventArgs e)
        {
            if (_cajaServicio.ObtenerEstadoCaja())
            {
                MessageBox.Show("No puede Abrir Caja debido a que ya se encuentra Abierta");

                return;
            }

            var fCaja = new _00042_AbrirCaja();
            fCaja.ShowDialog();
        }

        private void ConsultaTipoEmpleadoToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            var fTipoEmpleado = new _00044_TipoEmpleado();
            fTipoEmpleado.ShowDialog();
        }

        private void NuevoTipoEmpleadoToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
          var fTipoEmpleadoABM = new _00045_ABM_TipoEmpleado(TipoOperacion.Nuevo);
            fTipoEmpleadoABM.ShowDialog();
        }

        private void ConsultaMovimientoToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            var fMovimientoConsulta = new _00049_MovimientoConsulta();
            fMovimientoConsulta.ShowDialog();
        }

        private void VentaEnKioscoToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            if (_cajaServicio.ObtenerEstadoCaja() == false)
            {
                MessageBox.Show("La Caja se encuentra Cerrada");

                return;
            }
          
            var fkiosco = new _00050_Kiosco();
            fkiosco.ShowDialog();
        }

        private void VentaEnDeliveryToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            if (_cajaServicio.ObtenerEstadoCaja() == false)
            {
                MessageBox.Show("La Caja se encuentra Cerrada");

                return;
            }
            var fDelivery = new _00054_ListaPedidosDelivery();
            fDelivery.ShowDialog();
        }

        private void IngresoDeMercaderíaToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            if (_cajaServicio.ObtenerEstadoCaja() == false)
            {
                MessageBox.Show("La Caja se encuentra Cerrada");

                return;
            }

            var fIngresoMercaderia = new _00055_IngresoMercaderia();
            fIngresoMercaderia.ShowDialog();
        }

        private void ConsultaProveedoresToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            var fConsultaProveedores = new _00058_ConsultaCompraProveedores();
            fConsultaProveedores.ShowDialog();
        }

        private void ConsultaTarjetaToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            var fConsultaTarjeta = new _00060_TarjetaConsulta();
            fConsultaTarjeta.ShowDialog();
        }

        private void NuevaTarjetaToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            var fNuevaTarjeta = new _00059_TarjetaABM(TipoOperacion.Nuevo);
            fNuevaTarjeta.ShowDialog();
        }

        private void ConsultaPlanTarjetaToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            var fConsultaPlanTarjeta = new _00062_PlanTarjetaConsulta();
            fConsultaPlanTarjeta.ShowDialog();
        }

        private void NuevoPlanTarjetaToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            var fNuevoPlanTarjeta = new _00061_PlanTarjetaABM(TipoOperacion.Nuevo);
            fNuevoPlanTarjeta.ShowDialog();
        }

        private void ConsultaToolStripMenuItem16_Click(object sender, System.EventArgs e)
        {
            var fConsultaFormaDePago = new _00064_ConsultaFormaPago();
            fConsultaFormaDePago.ShowDialog();
        }

        private void saldoCtaCteToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            var fSaldoCliente = new _00066_SaldoClienteCtaCte();
            fSaldoCliente.ShowDialog();
        }


        

        

        private void Picmoto_Click(object sender, System.EventArgs e)
        {
            if (_cajaServicio.ObtenerEstadoCaja() == false)
            {
                MessageBox.Show("La Caja se encuentra Cerrada");

                return;
            }
            var fDelivery = new _00054_ListaPedidosDelivery();
            fDelivery.ShowDialog();
        }

        private void pictureBar_Click_1(object sender, System.EventArgs e)
        {
            if (_cajaServicio.ObtenerEstadoCaja() == false)
            {
                MessageBox.Show("La Caja se encuentra Cerrada");

                return;
            }

            var fBar = new _00012_VentaSalon();
            fBar.ShowDialog();
        }

        private void picKiosco_Click(object sender, System.EventArgs e)
        {
            if (_cajaServicio.ObtenerEstadoCaja() == false)
            {
                MessageBox.Show("La Caja se encuentra Cerrada");

                return;
            }
            var fkiosco = new _00050_Kiosco();
            fkiosco.ShowDialog();
        }

        private void consultaToolStripMenuItem17_Click(object sender, System.EventArgs e)
        {
            var fMovimientoConsulta = new _00049_MovimientoConsulta();
            fMovimientoConsulta.ShowDialog();
        }

        private void cerrarCajaToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            if (!_cajaServicio.ObtenerEstadoCaja())
            {
                MessageBox.Show("No puede Cerrar Caja debido a que no fue Abierta");
                return;
            }

            var fCerrarCaja = new _00069_CerrarCaja();
            fCerrarCaja.ShowDialog();
        }

        private void cambiarColorToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            if (this.BackColor == System.Drawing.Color.CornflowerBlue)
            {
                this.BackColor = System.Drawing.Color.SteelBlue;
            }
            else
            {
                this.BackColor = System.Drawing.Color.CornflowerBlue;
            }            
        }

        private void movimientosToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            if (!_cajaServicio.ObtenerEstadoCaja())
            {
                MessageBox.Show("No es posible mostrar los movimientos debido a que la Caja se encuentra Cerrada");
                return;
            }

            var fMovimientosCaja = new _00070_MovimientosCaja(_cajaServicio.ObtenerCajaAbierta().Id);
            fMovimientosCaja.ShowDialog();
        }

        private void Principal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F8)
            {
                if (_cajaServicio.ObtenerEstadoCaja() == false)
                {
                    MessageBox.Show("La Caja se encuentra Cerrada");

                    return;
                }

                var fIngresoMercaderia = new _00055_IngresoMercaderia();
                fIngresoMercaderia.ShowDialog();
            }

            if (e.KeyCode == Keys.F9)
            {
                if (_cajaServicio.ObtenerEstadoCaja() == false)
                {
                    MessageBox.Show("La Caja se encuentra Cerrada");

                    return;
                }

                var fBar = new _00012_VentaSalon();
                fBar.ShowDialog();
            }

            if (e.KeyCode == Keys.F10)
            {
                if (_cajaServicio.ObtenerEstadoCaja() == false)
                {
                    MessageBox.Show("La Caja se encuentra Cerrada");

                    return;
                }
                var fkiosco = new _00050_Kiosco();
                fkiosco.ShowDialog();
            }

            if (e.KeyCode == Keys.F11)
            {
                if (_cajaServicio.ObtenerEstadoCaja() == false)
                {
                    MessageBox.Show("La Caja se encuentra Cerrada");

                    return;
                }
                var fDelivery = new _00054_ListaPedidosDelivery();
                fDelivery.ShowDialog();
            }
        }

        private void ingresoMercaderiaConsultaToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            var fIngrMercaderiaConsulta = new _00071_IngresoMercaderiaConsulta();
            fIngrMercaderiaConsulta.ShowDialog();
        }

        private void ventaBarConsultaToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            var fVentaBarConsulta = new _00074_ComprobSalonConsulta();
            fVentaBarConsulta.ShowDialog();
        }

        private void ventaKioscoConsultaToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            var fVentaKioscoConsulta = new _00073_ComprobKioscoConsulta();
            fVentaKioscoConsulta.ShowDialog();
        }

        private void ventaDeliveryConsultaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fVentaDeliveryConsulta = new _00072_ComprobDeliveryConsulta();
            fVentaDeliveryConsulta.ShowDialog();
        }

        private void comprobanteCtaCteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fComprobCtaCteConsulta = new _00075_ComprobCtaCteConsulta();
            fComprobCtaCteConsulta.ShowDialog();
        }

        private void consultaDelDiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!_cajaServicio.ObtenerEstadoCaja())
            {
                MessageBox.Show("No es posible mostrar los movimientos debido a que la Caja se encuentra Cerrada");
                return;
            }

            var fMovimientosCaja = new _00070_MovimientosCaja(_cajaServicio.ObtenerCajaAbierta().Id);
            fMovimientosCaja.ShowDialog();
        }

        private void consultaDeCtaCteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fMovimientosCtaCte = new _00077_MovimientoCtaCte();
            fMovimientosCtaCte.ShowDialog();

        }
    }
}
