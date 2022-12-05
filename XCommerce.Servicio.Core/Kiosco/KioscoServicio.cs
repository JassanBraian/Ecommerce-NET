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
using XCommerce.Servicio.Core.Cliente.DTOs;
using XCommerce.Servicio.Core.FormaDePago;
using XCommerce.Servicio.Core.FormaDePago.DTOs;
using XCommerce.Servicio.Core.Kiosco.DTOs;
using XCommerce.Servicio.Core.PlanTarjeta;
using XCommerce.Servicio.Core.Producto;
using XCommerce.Servicio.Core.Tarjeta;

namespace XCommerce.Servicio.Core.Kiosco
{
    public class KioscoServicio : IkioscoServicio
    {
        IArticuloServicio _ArticuloServicio = new ArticuloServicio();
        ICajaServicio _CajaServicio = new CajaServicio();
        IClienteServicio _clienteServicio = new ClienteServicio();
        IFormaDePago _formaPagoServicio = new FormaDePagoServicio();
        IProductoServicio _productoServicio = new ProductoServicio();        

        public static List<kioscoDto> comprobantes = new List<kioscoDto>();

        public void AgregarItem(string codigo, decimal cantidad, long comprobanteId)
        {
            var comprobante = ObtenerUltimoComprobante();

            using (var context = new ModeloXCommerceContainer())
            {
                var producto = context.Articulos.FirstOrDefault(x => x.Codigo == codigo);

                var listaPrecioKiosco = context.ListaPrecios.FirstOrDefault(x => x.Descripcion == "Kiosco");

                var precio = context.Precios.FirstOrDefault(x => x.ArticuloId == producto.Id && x.ListaPrecioId == listaPrecioKiosco.Id);

                var itemIngresado = comprobante.Items.FirstOrDefault(x => x.Codigo == codigo);                
                if(itemIngresado == null)
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

                    DetalleKioscoDto item = new DetalleKioscoDto
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
            var comprobante = new kioscoDto();

            comprobante.Clienteid = _clienteServicio.ObtenerClientePorCuil("99999999999").Id;      
            comprobante.Descuento = comprobante.Descuento;
            comprobante.Fecha = DateTime.Now;
            comprobante.Numero = 1;
            comprobante.UsuarioId = Validacion.UsuarioLogeado;
            comprobante.Items = new List<DetalleKioscoDto>();
            comprobante.TipoComprobante = TipoComprobante.B;

            comprobantes.Add(comprobante);

            return comprobante.Id;

        }

        public kioscoDto ObtenerUltimoComprobante()
        {
            return comprobantes.Last();
        }
        

        public void Pagar(decimal descuento, decimal subTotal, decimal total, long comprobanteId, TipoFormaPago formaPago, TipoComprobante tipoComprobante)
        {
            var comprobante = ObtenerUltimoComprobante();
            using (var context = new ModeloXCommerceContainer())
            {

                var comprobantePersistencia = new AccesoDatos.ComprobanteFactura();

                comprobantePersistencia.ClienteId = comprobante.Clienteid;
                comprobantePersistencia.Descuento = descuento;
                comprobantePersistencia.Fecha = comprobante.Fecha;
                comprobantePersistencia.Numero = GenerarNumeroFactura();
                comprobantePersistencia.SubTotal = subTotal;
                comprobantePersistencia.Total = total;
                comprobantePersistencia.UsuarioId = Validacion.UsuarioLogeado;
                comprobantePersistencia.TipoComprobante = tipoComprobante;
                comprobantePersistencia.DetalleComprobantes = new List<DetalleComprobante>();

                context.Comprobantes.Add(comprobantePersistencia);

                context.SaveChanges();

                // Cargo el Id creado por la DB al hacer persistencia, en el comprobante static de la List y creo otro para actualizarlo
                comprobante.Id = comprobantePersistencia.Id;
                comprobante.Descuento = comprobantePersistencia.Descuento;

                comprobantes.Add(comprobante);

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

                    var articulo =_ArticuloServicio.ObtenerPorId(item.Id);

                    if (articulo.DescuentaStock)
                    {
                        _ArticuloServicio.DescontarStock(item.Id, item.Cantidad);
                    }

                }

                // movimiento de kiosco para que salga en la pantalla de movimiento y la fomra de pago

                var ultimacaja = context.Cajas.FirstOrDefault(x => x.Estado == EstadoCaja.Abierta);
                
                var cajaAbierta = _CajaServicio.ObtenerCajaAbierta();

                

                if (formaPago != TipoFormaPago.CuentaCorriente)
                {
                    var nuevoMovimiento = new Movimiento()
                    {
                        Fecha = DateTime.Now,
                        CajaId = ultimacaja.Id,
                        ComprobanteId = comprobante.Id,
                        Descripcion = "Cobro en Kiosco",
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
                        Descripcion = "Pago en Kiosco con CtaCte",
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
                comprobante.Total = comprobante.SubTotal - (comprobante.SubTotal * descuento / 100);

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

        public void FormaPagoCtaCte(long clienteId)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var comprobante = ObtenerUltimoComprobante();

                var formaPago = new AccesoDatos.FormaPagoCtaCte()
                {
                    ComprobanteId = comprobante.Id,
                    Monto = comprobante.Total,
                    TipoFormaPago = TipoFormaPago.CuentaCorriente,
                    ClienteId = clienteId,

                };
                context.FormasPagos.Add(formaPago);

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
                    TipoFormaPago = TipoFormaPago.Cheque,
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
        public void ObtenerClienteCtaCte(long clienteid, long comprobanteId)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var comprobante = ObtenerUltimoComprobante();

                comprobante.Clienteid = clienteid;

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

        public IEnumerable<kioscoDto> Obtener(string cadenaBuscar)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                return context.Comprobantes.OfType<ComprobanteFactura>()
                    .AsNoTracking()
                    //.Where(x => x.Descripcion.Contains(cadenaBuscar))
                    .Select(x => new kioscoDto
                    {
                        Id = x.Id,
                        Clienteid = x.ClienteId,
                        ClienteStr = context.Personas.FirstOrDefault(p => p.Id == x.ClienteId).Cuil,
                        UsuarioId = x.UsuarioId,
                        Descuento = x.Descuento,
                        Fecha = x.Fecha,
                        Numero = x.Numero,
                        TipoComprobante = x.TipoComprobante,
                        Items = x.DetalleComprobantes.Select(d => new DetalleKioscoDto
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
    }
}
