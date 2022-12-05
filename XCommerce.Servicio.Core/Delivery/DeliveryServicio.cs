using Presentacion.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XCommerce.AccesoDatos;
using XCommerce.Servicio.Core.Articulo;
using XCommerce.Servicio.Core.Caja;
using XCommerce.Servicio.Core.Caja.DTOs;
using XCommerce.Servicio.Core.Cliente;
using XCommerce.Servicio.Core.Delivery.DTOs;
using XCommerce.Servicio.Core.Empleado;
using XCommerce.Servicio.Core.Empleado.DTOs;
using XCommerce.Servicio.Core.Producto;

namespace XCommerce.Servicio.Core.Delivery
{
    public class DeliveryServicio : IDeliveryServicio
    {
        IArticuloServicio _ArticuloServicio = new ArticuloServicio();
        ICajaServicio _CajaServicio = new CajaServicio();
        IEmpleadoServicio _empleadoServicio = new EmpleadoServicio();
        IClienteServicio _clienteServicio = new ClienteServicio();
        IProductoServicio _productoServicio = new ProductoServicio();

        public static List<DeliveryDto>  comprantesdelivery = new List<DeliveryDto>();


        public void AgregarItem(string codigo, decimal cantidad, long comprobanteId)
        {
            var comprobante = ObtenerUltimoComprobante();

            using (var context = new ModeloXCommerceContainer())
            {
                var producto = context.Articulos.FirstOrDefault(x => x.Codigo == codigo);

                var listaPrecioDelivery = context.ListaPrecios.FirstOrDefault(x => x.Descripcion == "Delivery");

                var precio = context.Precios.FirstOrDefault(x => x.ArticuloId == producto.Id && x.ListaPrecioId == listaPrecioDelivery.Id);


                var itemIngresado = comprobante.Items.FirstOrDefault(x => x.Codigo == codigo);
                if (itemIngresado == null)
                {

                    if (_productoServicio.PermiteStockNegativo(producto.Id, cantidad) == false)
                    {
                        MessageBox.Show("No se puede vender debido a que no permite tener Stock Negativo");
                        return;
                    }

                    if (_productoServicio.SuperaLimiteVenta(producto.Id, cantidad))
                    {
                        MessageBox.Show("No se puede exceder el limite de venta de este Articulo por venta");
                        return;
                    }

                    if (_productoServicio.CumpleStockMinimo(producto.Id, cantidad) == false)
                    {
                        MessageBox.Show("No se puede vender debido al Stock Minimo establecido");
                        return;
                    }

                    DetalleDeliveryDto item = new DetalleDeliveryDto
                    {
                        Id = producto.Id,
                        Cantidad = cantidad,
                        ComprobanteId = comprobanteId,
                        Codigo = producto.Codigo,
                        PrecioUnitario = precio.PrecioPublico,
                        ArticuloId = producto.Id,
                        Descripcion = producto.Descripcion

                    };

                    comprobante.Items.Add(item);

                }
                else
                {
                    var nuevaCantidad = itemIngresado.Cantidad + cantidad;

                    if (_productoServicio.PermiteStockNegativo(producto.Id, nuevaCantidad) == false)
                    {
                        MessageBox.Show("No se puede vender debido a que no permite tener Stock Negativo");
                        return;
                    }

                    if (_productoServicio.SuperaLimiteVenta(producto.Id, nuevaCantidad))
                    {
                        MessageBox.Show("No se puede exceder el limite de venta de este Articulo por venta");
                        return;

                    }

                    if (_productoServicio.CumpleStockMinimo(producto.Id, nuevaCantidad) == false)
                    {
                        MessageBox.Show("No se puede vender debido al Stock Minimo establecido");
                        return;
                    }

                    itemIngresado.Cantidad += cantidad;
                }
            }
        }

        public long CrearComprobante()
        {
            var comprobante = new DeliveryDto();

            comprobante.Clienteid = _clienteServicio.ObtenerClientePorCuil("99999999999").Id;     // ANDA PERO HAY QUE CORREGIR, DEBE CREAR CON CONSUMIDOR FINAL Y LUEGO ELEGIR EL CLIENTE EN PANTALLA
            comprobante.Descuento = 0m;
            comprobante.Fecha = DateTime.Now;
            comprobante.Numero = 1;
            // comprobante.SubTotal = 0m;
            // comprobante.Total = comprobante.SubTotal - comprobante.SubTotal * comprobante.Descuento;
            comprobante.UsuarioId = Validacion.UsuarioLogeado;
            comprobante.Items = new List<DetalleDeliveryDto>();
            comprobante.EstadoDelivery = EstadoComprobanteSalon.EnProceso;
            comprobante.TipoComprobante = TipoComprobante.A;
            comprobante.CadeteId = 2;

            comprantesdelivery.Add(comprobante);

            return comprobante.Id;

        }

        public IEnumerable<DeliveryDto> Obtener(string cadenaBuscar)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                return context.Comprobantes.OfType<ComprobanteDelivery>()
                    .AsNoTracking()
                    //.Where(x => x.Descripcion.Contains(cadenaBuscar))
                    .Select(x => new DeliveryDto
                    {
                        Id = x.Id,
                        Clienteid = x.ClienteId,
                        ClienteStr = context.Personas.FirstOrDefault(p => p.Id == x.ClienteId).Cuil,
                        UsuarioId = x.UsuarioId,
                        Descuento = x.Descuento,
                        Fecha = x.Fecha,
                        Numero = x.Numero,
                        EstadoDelivery = x.EstadoDelivery,
                        TipoComprobante = x.TipoComprobante,
                        CadeteId = x.CadeteId,
                        CadeteStr = context.Personas.FirstOrDefault(p => p.Id == x.CadeteId).Cuil,
                        Items = x.DetalleComprobantes.Select(d => new DetalleDeliveryDto
                        {
                            Id = d.Id,
                            Codigo = d.Codigo,
                            Descripcion = d.Descripcion,
                            Cantidad = d.Cantidad,
                            PrecioUnitario = d.PrecioUnitario,
                            ArticuloId = d.ArticuloId,
                            ComprobanteId = d.ComprobanteId,

                        }).ToList()

                    }).ToList();
            }
        }

        public DeliveryDto ObtenerUltimoComprobante()
        {
            return comprantesdelivery.Last();
        }

        public void Pagar(decimal descuento, decimal subTotal, decimal total, long comprobanteId, TipoFormaPago formaPago, TipoComprobante tipoComprobante)
        {
            var comprobante = ObtenerUltimoComprobante();

            using (var context = new ModeloXCommerceContainer())
            {
                var comprobantePersistencia = new AccesoDatos.ComprobanteDelivery();

                comprobantePersistencia.ClienteId = comprobante.Clienteid;
                comprobantePersistencia.Descuento = descuento;
                comprobantePersistencia.Fecha = comprobante.Fecha;
                comprobantePersistencia.Numero = GenerarNumeroFactura();
                comprobantePersistencia.SubTotal = subTotal;
                comprobantePersistencia.Total = total;
                comprobantePersistencia.TipoComprobante = tipoComprobante;
                comprobantePersistencia.UsuarioId = Validacion.UsuarioLogeado;
                comprobantePersistencia.CadeteId = comprobante.CadeteId;
                comprobantePersistencia.EstadoDelivery = EstadoComprobanteSalon.Facturado;

                comprobantePersistencia.DetalleComprobantes = new List<DetalleComprobante>();

                context.Comprobantes.Add(comprobantePersistencia);

                context.SaveChanges();

                // Cargo el Id creado por la DB al hacer persistencia, en el comprobante static de la List y creo otro para actualizarlo
                comprobante.Id = comprobantePersistencia.Id;
                comprantesdelivery.Add(comprobante);

                foreach (var item in comprobante.Items)
                {
                    var detallePersistencia = new AccesoDatos.DetalleComprobante
                    {
                        ArticuloId = item.Id,
                        SubTotal = item.SubTotalLinea,
                        Cantidad = item.Cantidad,
                        Codigo = item.Codigo,
                        ComprobanteId = comprobante.Id,
                        Descripcion = item.Descripcion,
                        PrecioUnitario = item.PrecioUnitario
                    };

                    context.DetalleComprobantes.Add(detallePersistencia);

                    var articulo = _ArticuloServicio.ObtenerPorId(item.Id);

                    if (articulo.DescuentaStock)
                    {
                        _ArticuloServicio.DescontarStock(item.Id, item.Cantidad);
                    }
                }
                // movimiento de delivery para que salga en la pantalla de movimiento y la fomra de pago

                var ultimacaja = context.Cajas.FirstOrDefault(x => x.Estado == EstadoCaja.Abierta);
                
                var cajaAbierta = _CajaServicio.ObtenerCajaAbierta();

                if (formaPago != TipoFormaPago.CuentaCorriente)
                {
                    var nuevoMovimiento = new Movimiento()
                    {
                        Fecha = DateTime.Now,
                        CajaId = ultimacaja.Id,
                        ComprobanteId = comprobante.Id,
                        Descripcion = "Cobro en Delivery",
                        Monto = comprobante.Total,
                        TipoMovimento = TipoMovimiento.Ingreso,
                        UsuarioId = Validacion.UsuarioLogeado
                    };
                    context.Movimientos.Add(nuevoMovimiento);

                    _CajaServicio.AumentarMontoSistema(cajaAbierta.Id, comprobante.Total);
                }
                else
                {
                    var nuevoMovimiento = new Movimiento()
                    {
                        Fecha = DateTime.Now,
                        CajaId = ultimacaja.Id,
                        ComprobanteId = comprobante.Id,
                        Descripcion = "Pago en Delivery con CtaCte",
                        Monto = comprobante.Total,
                        TipoMovimento = TipoMovimiento.Egreso,
                        UsuarioId = Validacion.UsuarioLogeado

                    };
                    context.Movimientos.Add(nuevoMovimiento);
                }

                context.SaveChanges();
            }
        }

        public void Total(long comprobanteId, decimal descuento)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var comprobante = context.Comprobantes.Find(comprobanteId);

                comprobante.Descuento = descuento;
                comprobante.Total = comprobante.SubTotal - (comprobante.SubTotal * descuento / 100m);

                context.SaveChanges();
            }
        }

        public void FormaPagoTarjeta(long tarjetaId, long planTarjetaId, string numeroTicket, string cupon, string numeroTarjeta)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var comprobante = ObtenerUltimoComprobante();

                var formaPago = new FormaPagoTarjeta()
                {

                    ComprobanteId = comprobante.Id,
                    TipoFormaPago = TipoFormaPago.Tarjeta,
                    Monto = comprobante.Total,
                    PlanTarjetaId = planTarjetaId,
                    NumeroTarjeta = numeroTarjeta,
                    Cupon = cupon,
                    Numero = numeroTicket,
                };

                context.FormasPagos.Add(formaPago);

                context.SaveChanges();
            }
        }

        public void FormaPagoCheque(long bancoId, string enteEmisor, string numero, int dias, DateTime dateTime)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var comprobante = ObtenerUltimoComprobante();

                var formaPago = new AccesoDatos.FormaPagoCheque()
                {
                    ComprobanteId = comprobante.Id,
                    Monto = comprobante.Total,
                    TipoFormaPago = TipoFormaPago.Efectivo,
                    BancoId = bancoId,
                    Dias = dias,
                    EnteEmisor = enteEmisor,
                    Numero = numero,
                    FechaEmision = dateTime,

                };

                context.FormasPagos.Add(formaPago);

                context.SaveChanges();
            }
        }

        public void FormaPagoCtaCte(long clienteId)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var comprobante = ObtenerUltimoComprobante();

                var formaPago = new AccesoDatos.FormaPagoCtaCte()
                {
                    ComprobanteId = comprobante.Id,
                    Monto = comprobante.Total,
                    TipoFormaPago = TipoFormaPago.Efectivo,
                    ClienteId = clienteId
                };

                context.FormasPagos.Add(formaPago);

                context.SaveChanges();
            }
        }

        public void FormaPagoEfectivo()
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var comprobante = ObtenerUltimoComprobante();

                var formaPago = new FormaPagoEfectivo()
                {
                    ComprobanteId = comprobante.Id,
                    Monto = comprobante.Total,
                    TipoFormaPago = TipoFormaPago.Efectivo,

                };

                context.FormasPagos.Add(formaPago);

                context.SaveChanges();
            }
        }

        public void ObtenerCadeteNuevo(EmpleadoDto dto, long comprobanteId)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var comprobanteDelivery = ObtenerUltimoComprobante();

                comprobanteDelivery.CadeteId = dto.Id;
                context.SaveChanges();
            }
        }

        public void EliminarItem(long? itemId, long comprobanteId)
        {
            if (itemId == null)
            {
                MessageBox.Show("Ocurrio un error");
                return;
            }

            var comprobante = ObtenerUltimoComprobante();

            var item = comprobante.Items.FirstOrDefault(x => x.Id == itemId);
            if (item == null)
            {
                MessageBox.Show("Ocurrio un error. El item no se encuentra en la grilla");
                return;
            }

            comprobante.Items.Remove(item);
        }

        public int GenerarNumeroFactura()
        {
            using (var context = new ModeloXCommerceContainer())
            {
                if (context.Comprobantes.Any())
                {
                    return context.Comprobantes.Max(x => x.Numero) + 1;
                }
                else
                {
                    return 1;
                }
            }
        }
    }
}