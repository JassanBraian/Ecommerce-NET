namespace XCommerce.Servicio.Core.Comprobante
{
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
    using XCommerce.Servicio.Core.Comprobante.DTOs;
    using XCommerce.Servicio.Core.Empleado.DTOs;
    using XCommerce.Servicio.Core.Producto;
    using XCommerce.Servicio.Core.Producto.DTOs;
    using static XCommerce.AccesoDatos.ComprobanteSalon;


    public class ComprobanteSalonServicio : IComprobanteSalonServicio
    {
        IArticuloServicio _ArticuloServicio = new ArticuloServicio();
        ICajaServicio _CajaServicio = new CajaServicio();
        IProductoServicio _productoServicio = new ProductoServicio();
        IClienteServicio _clienteServicio = new ClienteServicio();

        public void Generar(long mesaId, long usuarioId, int comensales, long? mozoId )
        {
            using (var context = new ModeloXCommerceContainer())
            {
                if (context.Comprobantes.OfType<ComprobanteSalon>()
                    .Any(x => x.MesaId == mesaId && x.EstadoSalon == EstadoComprobanteSalon.EnProceso))
                    return;  

                var clienteConsumidorFinal = context.Personas
                    .OfType<Cliente>()
                    .FirstOrDefault(x => x.Dni == "99999999"); // Dni por defecto para CF

                if (clienteConsumidorFinal == null)
                    throw new Exception("Ocurrio un error al obtener el Consumidor Final");

                var mesa = context.Mesas.FirstOrDefault(x => x.Id == mesaId);

                if (mesa == null)
                    throw new Exception("Ocurrio un error al obtener la mesa");

                mesa.EstadoMesa = EstadoMesa.Abierta;

                var nuevoComprobante = new ComprobanteSalon
                {
                    MesaId = mesaId,
                    ClienteId = clienteConsumidorFinal.Id,
                    Comensal = comensales.ToString(),
                    Descuento = 0m,
                    EstadoSalon = EstadoComprobanteSalon.EnProceso, 
                    Fecha = DateTime.Now,
                    MozoId = mozoId,
                    Numero = 0,
                    SubTotal = 0m,
                    Total = 0m,
                    TipoComprobante = TipoComprobante.X,
                    UsuarioId = usuarioId,
                    ReservaId=0,
                    MotivoReservaId=0,
                    DetalleComprobantes = new List<DetalleComprobante>()
                    
                    
                };

                context.Comprobantes.Add(nuevoComprobante);

                context.SaveChanges();
            }
        }
        public void AgregarItem(long mesaId, decimal cantidad, ProductoDto dto)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var comprobante = context.Comprobantes
                    .OfType<ComprobanteSalon>()
                    .Include(x=>x.DetalleComprobantes)
                    .FirstOrDefault(x => x.MesaId == mesaId
                                        && x.EstadoSalon == EstadoComprobanteSalon.EnProceso);

                if (comprobante == null)
                    throw new Exception("Ocurrio un error al obtener el comprobante");

                var producto = context.Articulos.FirstOrDefault(x => x.Codigo == dto.Codigo);

                var item = comprobante.DetalleComprobantes
                    .FirstOrDefault(x => x.Codigo == dto.Codigo);

                if(item == null)
                {
                    if (_productoServicio.PermiteStockNegativo(producto.Id, cantidad) == false)
                    {
                        MessageBox.Show("No se puede vender debido a que no permite tener Stock Negativo");
                        return;
                    }

                    if (_productoServicio.SuperaLimiteVenta(dto.Id, cantidad))
                    {
                        MessageBox.Show("No se puede exceder el limite de venta de este Articulo por venta");
                        return;
                    }

                    if (_productoServicio.CumpleStockMinimo(producto.Id, cantidad) == false)
                    {
                        MessageBox.Show("No se puede vender debido al Stock Minimo establecido");
                        return;
                    }


                    comprobante.DetalleComprobantes.Add(new DetalleComprobante
                    {
                        ArticuloId = dto.Id,
                        Cantidad = cantidad,
                        Codigo = dto.Codigo,
                        Descripcion = dto.Descripcion,
                        PrecioUnitario = dto.Precio,
                        SubTotal = dto.Precio * cantidad
                    });
                }
                else
                {
                    var nuevaCantidad = item.Cantidad + cantidad;

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

                    item.Cantidad += cantidad;
                    item.SubTotal = item.Cantidad * item.PrecioUnitario;
                }


                if (dto.DescuentaStock)
                {
                    _ArticuloServicio.DescontarStock(dto.Id, cantidad);
                }

                


                context.SaveChanges();
            }
        }

        public void ObtenerMozoNuevo(EmpleadoDto dto, long comprobanteId)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var comprobanteSalon =
                    context.Comprobantes.OfType<ComprobanteSalon>().FirstOrDefault(x => x.Id == comprobanteId);

                comprobanteSalon.MozoId = dto.Id;
                context.SaveChanges();


            }
        }

        public ComprobanteMesaDto ObtenerComprobanteMesa(long mesaId)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                return context.Comprobantes.OfType<ComprobanteSalon>()
                 .AsNoTracking()
                 .Include(x => x.Mozo)
                 .Where(x => x.MesaId == mesaId && x.EstadoSalon == EstadoComprobanteSalon.EnProceso) // falta esto!
                 .Select(x => new ComprobanteMesaDto
                 {
                     Id = x.Id,
                     ClienteId = x.ClienteId,
                     Legajo = x.MozoId.HasValue ? x.Mozo.Legajo : 0,
                     UsuarioId = x.UsuarioId,
                     MozoId = x.MozoId,
                     MesaId = x.MesaId,
                     Fecha = x.Fecha,
                     ApellidoMozo = x.MozoId.HasValue ? x.Mozo.Apellido : "NO",
                     NombreMozo = x.MozoId.HasValue ? x.Mozo.Nombre : "ASIGNADO",
                     Descuento = x.Descuento,
                     Items = x.DetalleComprobantes.Select(d => new DetalleComprobanteSalonDto
                     {
                         Id = d.Id,
                         CodigoProducto = d.Codigo,
                         DescripcionProducto = d.Descripcion,
                         Cantidad = d.Cantidad,
                         PrecioUnitario = d.PrecioUnitario,
                         ProductoId = d.ArticuloId,
                         ComprobanteId = d.ComprobanteId,
                     }).ToList()
                 }).FirstOrDefault();


            }
        }

        public void ObtenerProductoNuevo(ProductoDto productoSeleccionado, long comprobanteId)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var comprobanteSalon =
                    context.Comprobantes.OfType<ComprobanteSalon>().FirstOrDefault(x => x.Id == comprobanteId);

               // comprobanteSalon = productoSeleccionado.Id;

                
                context.SaveChanges();


            }
        }



        public void PagarComprobante(ComprobanteMesaDto comprobante, TipoFormaPago tipoFormaPago, TipoComprobante tipoComprobante)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var comprobanteSalon =
                    context.Comprobantes.OfType<ComprobanteSalon>().First(x => x.Id == comprobante.Id);

                comprobanteSalon.EstadoSalon = EstadoComprobanteSalon.Facturado;
                comprobanteSalon.Fecha = comprobante.Fecha;
                comprobanteSalon.Numero = GenerarNumeroFactura();
                comprobanteSalon.TipoComprobante = tipoComprobante;

                var ultimacaja = context.Cajas.FirstOrDefault(x => x.Estado == EstadoCaja.Abierta);

                if (comprobanteSalon.Total == 0) return;

                if(tipoFormaPago != TipoFormaPago.CuentaCorriente)
                {
                    var nuevoMovimiento = new Movimiento()
                    {
                        Fecha = DateTime.Now,
                        CajaId = ultimacaja.Id,
                        ComprobanteId = comprobante.Id,
                        Descripcion = "Cobro en Salon",
                        Monto = comprobante.Total,
                        TipoMovimento = TipoMovimiento.Ingreso,
                        UsuarioId = Validacion.UsuarioLogeado
                    };
                    context.Movimientos.Add(nuevoMovimiento);

                    var cajaAbierta = _CajaServicio.ObtenerCajaAbierta();

                    _CajaServicio.AumentarMontoSistema(cajaAbierta.Id, comprobante.Total);
                }
                else
                {
                    var nuevoMovimiento = new Movimiento()
                    {
                        Fecha = DateTime.Now,
                        CajaId = ultimacaja.Id,
                        ComprobanteId = comprobante.Id,
                        Descripcion = "Pago en Bar con CtaCte",
                        Monto = comprobante.Total,
                        TipoMovimento = TipoMovimiento.Egreso,
                        UsuarioId = Validacion.UsuarioLogeado

                    };
                    context.Movimientos.Add(nuevoMovimiento);
                    
                    _clienteServicio.AumentarSaldoCtaCte(comprobanteSalon.ClienteId, comprobante.Total);
                }
                
                context.SaveChanges();
            }

        }

        public void Total(long comprobanteId, decimal descuento)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                //var comprobante = context.Comprobantes.Find(comprobanteId);
                var comprobante = context.Comprobantes.FirstOrDefault(x => x.Id == comprobanteId);

                comprobante.Descuento = descuento;
                comprobante.Total = comprobante.SubTotal - (comprobante.SubTotal * descuento / 100m);

                context.SaveChanges();
            }
        }



        public void CerrarMesa(long mesaId)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                //Cambiar el Estado de la Mesa
                var mesaSeleccionada = context.Mesas
                    .FirstOrDefault(x => x.Id == mesaId);

                if (mesaSeleccionada == null)
                    throw new Exception("Ocurrio un error al obtener la Mesa");

                mesaSeleccionada.EstadoMesa = EstadoMesa.Cerrada;

                context.SaveChanges();

            }
        }

        public void FormaPagoTarjeta(long tarjetaId, long planTarjetaId, string numeroTicket, string cupon, string numeroTarjeta, ComprobanteMesaDto comprobante)
        {
            using (var context = new ModeloXCommerceContainer())
            {

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

        public void FormaPagoCheque(long bancoId, string enteEmisor, string numero, int dias, DateTime dateTime, ComprobanteMesaDto comprobante)
        {
            using (var context = new ModeloXCommerceContainer())
            {

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

        public void FormaPagoCtaCte(long clienteId, ComprobanteMesaDto comprobante)
        {
            using (var context = new ModeloXCommerceContainer())
            {

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

        public void FormaPagoEfectivo(ComprobanteMesaDto comprobante)
        {
            using (var context = new ModeloXCommerceContainer())
            {

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

        public void DescontarMontoSenia(long comprobanteId, decimal senia)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var comprobante = context.Comprobantes.OfType<ComprobanteSalon>().FirstOrDefault(x => x.Id == comprobanteId);
                
                comprobante.Total -= senia;

                context.SaveChanges();
            }

        }

        public void AsignarCliente(long comprobanteId, long clienteId)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var comprobante = context.Comprobantes.OfType<ComprobanteSalon>().FirstOrDefault(x => x.Id == comprobanteId);

                comprobante.ClienteId = clienteId;

                context.SaveChanges();
            }
        }

        public void EliminarItem(long? itemId, long comprobanteId)
        {
            if (itemId == null)
            {
                MessageBox.Show("Ocurrio un error. Producto null");
                return;
            }

            using (var context = new ModeloXCommerceContainer())
            {
                var comprobante = context.Comprobantes
                    .OfType<ComprobanteSalon>()
                    .Include(x => x.DetalleComprobantes)
                    .FirstOrDefault(x => x.Id == comprobanteId
                                        && x.EstadoSalon == EstadoComprobanteSalon.EnProceso);

                if (comprobante == null)
                    throw new Exception("Ocurrio un error al obtener el comprobante");

                var item = comprobante.DetalleComprobantes
                    .FirstOrDefault(x => x.Id == itemId);

                if (item == null)
                {
                    MessageBox.Show("Ocurrio un error. El item no se encuentra en la grilla");
                    return;
                }

                context.DetalleComprobantes.Remove(item);

                context.SaveChanges();
            }
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

        public void ActualizarMontos(long comprobanteId, decimal subtotal, decimal descuento)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var comprobante = context.Comprobantes.Find(comprobanteId);

                comprobante.SubTotal = subtotal;
                comprobante.Descuento = descuento;
                comprobante.Total = comprobante.SubTotal - (comprobante.SubTotal * descuento / 100m);

                context.SaveChanges();
            }
        }

        public IEnumerable<ComprobanteMesaDto> Obtener(string cadenaBuscar)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                return context.Comprobantes.OfType<ComprobanteSalon>()
                    .AsNoTracking()
                    //.Where(x => x.Descripcion.Contains(cadenaBuscar))
                    .Select(x => new ComprobanteMesaDto
                    {
                        Id = x.Id,
                        ClienteId = x.ClienteId,
                        ClienteStr = context.Personas.FirstOrDefault(p => p.Id == x.ClienteId).Cuil,
                        UsuarioId = x.UsuarioId,
                        Descuento = x.Descuento,
                        Fecha = x.Fecha,
                        Numero = x.Numero,
                        MozoId = x.MozoId,
                        ApellidoMozo = context.Personas.FirstOrDefault(p => p.Id == x.MozoId).Apellido,
                        NombreMozo = context.Personas.FirstOrDefault(p => p.Id == x.MozoId).Nombre,
                        Items = x.DetalleComprobantes.Select(d => new DetalleComprobanteSalonDto
                        {
                            Id = d.Id,
                            CodigoProducto = d.Codigo,
                            DescripcionProducto = d.Descripcion,
                            Cantidad = d.Cantidad,
                            PrecioUnitario = d.PrecioUnitario,
                            ProductoId = d.ArticuloId,
                            ComprobanteId = d.ComprobanteId,

                        }).ToList()

                    }).ToList();
            }
        }

        public void CerrarComprobanteSinFacturar(ComprobanteMesaDto comprobante)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var comprobanteSalon =
                    context.Comprobantes.OfType<ComprobanteSalon>().First(x => x.Id == comprobante.Id);

                comprobanteSalon.EstadoSalon = EstadoComprobanteSalon.Facturado;
                comprobanteSalon.Fecha = comprobante.Fecha;
                comprobante.EstaEliminado = true;

                context.SaveChanges();
            }
        }
    }
}
