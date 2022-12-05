using Presentacion.Helpers;
using Servicios.Core.Movimiento;
using Servicios.Core.Movimiento.DTOs;
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
using XCommerce.Servicio.Core.ComprobanteCompra;
using XCommerce.Servicio.Core.ComprobanteCompra.DTOs;
using XCommerce.Servicio.Core.Producto;

namespace XCommerce.Servicio.Core.ComprobanteCompra
{
    public class CompraServicio : ICompraServicio
    {
        IArticuloServicio _ArticuloServicio = new ArticuloServicio();
        ICajaServicio _CajaServicio = new CajaServicio();
        IProductoServicio _productoServicio = new ProductoServicio();
        IClienteServicio _clienteServicio = new ClienteServicio();
        IMovimientoServicio _movimientoServicio = new MovimientoServicio();

        public static List<ComprobanteCompraDto> comprobantes = new List<ComprobanteCompraDto>();

        public void AgregarItem(string codigo, decimal cantidad, long comprobanteId)
        {
            var comprobante = ObtenerUltimoComprobante();

            using (var context = new ModeloXCommerceContainer())
            {
                var producto = context.Articulos.FirstOrDefault(x => x.Codigo == codigo);

                var precio = context.Precios.FirstOrDefault(x => x.ArticuloId == producto.Id && x.ListaPrecioId == 1);
                
                var itemIngresado = comprobante.Items.FirstOrDefault(x => x.Codigo == codigo);
                if (itemIngresado == null)
                {
                    if (_productoServicio.CumpleStockMaximo(producto.Id, cantidad) == false)
                    {
                        MessageBox.Show("No se puede superar el limite Maximo de Stock");
                        return;
                    }

                    DetalleCompraDto item = new DetalleCompraDto
                    {
                        Id = producto.Id,
                        Cantidad = cantidad,
                        ComprobanteId = comprobanteId,
                        Codigo = producto.Codigo,
                        PrecioUnitario = precio.PrecioCosto,
                        ArticuloId = producto.Id,
                        Descripcion = producto.Descripcion
                    };

                    comprobante.Items.Add(item);
                }
                else
                {
                    var nuevaCantidad = itemIngresado.Cantidad + cantidad;

                    if (_productoServicio.CumpleStockMaximo(producto.Id, nuevaCantidad) == false)
                    {
                        MessageBox.Show("No se puede exceder el limite de venta de este Articulo por venta");
                        return;

                    }

                    itemIngresado.Cantidad += cantidad;
                }
            }
        }

        public long CrearComprobante()
        {
            var comprobante = new ComprobanteCompraDto();

            comprobante.Id = 1;
            comprobante.Clienteid = _clienteServicio.ObtenerClientePorCuil("99999999999").Id;      // ANDA PERO HAY QUE CORREGIR, DEBE CREAR CON CONSUMIDOR FINAL Y LUEGO ELEGIR EL CLIENTE EN PANTALLA
            comprobante.Descuento = 0m;
            comprobante.Fecha = DateTime.Now;
            comprobante.Numero = 1;
            // comprobante.SubTotal = 0m;
            // comprobante.Total = comprobante.SubTotal - comprobante.SubTotal * comprobante.Descuento;
            comprobante.UsuarioId = Validacion.UsuarioLogeado;
            comprobante.Items = new List<DetalleCompraDto>();
            comprobante.TipoComprobante = TipoComprobante.B;
            comprobante.ProveedorId = 1;  // Anda pero se debe tomar desde pantalla
            

            comprobantes.Add(comprobante);

            return comprobante.Id;
        }


        public ComprobanteCompraDto ObtenerUltimoComprobante()
        {
            return comprobantes.Last();
        }

        public void Pagar(decimal descuento, decimal subTotal, decimal total, long comprobanteId)
        {
            var comprobante = ObtenerUltimoComprobante();

            using (var context = new ModeloXCommerceContainer())
            {
                var comprobantePersistencia = new AccesoDatos.ComprobanteCompra();

                comprobantePersistencia.Id = comprobante.Id;
                comprobantePersistencia.ClienteId = comprobante.Clienteid;
                comprobantePersistencia.Descuento = descuento;
                comprobantePersistencia.Fecha = comprobante.Fecha;
                comprobantePersistencia.Numero = GenerarNumeroFactura(); 
                comprobantePersistencia.SubTotal = subTotal;
                comprobantePersistencia.Total = total;
                comprobantePersistencia.TipoComprobante = TipoComprobante.X;
                comprobantePersistencia.UsuarioId = Validacion.UsuarioLogeado;
                comprobantePersistencia.ProveedorId = comprobante.ProveedorId;

                comprobantePersistencia.DetalleComprobantes = new List<DetalleComprobante>();

                context.Comprobantes.Add(comprobantePersistencia);

                foreach (var item in comprobante.Items)
                {
                    var detallePersistencia = new AccesoDatos.DetalleComprobante
                    {
                        ArticuloId = item.Id,
                        SubTotal = item.SubTotalLinea,
                        Cantidad = item.Cantidad,
                        Codigo = item.Codigo,
                        ComprobanteId = item.ComprobanteId,
                        Descripcion = item.Descripcion,
                        PrecioUnitario = item.PrecioUnitario,
                    };

                    context.DetalleComprobantes.Add(detallePersistencia);


                    _ArticuloServicio.AumentarStock(item.Id, item.Cantidad);
                }
                // movimiento de compra mercaderia  para que salga en la pantalla de movimiento y la fomra de pago

                var ultimacaja = context.Cajas.FirstOrDefault(x => x.Estado == EstadoCaja.Abierta);

                var nuevoMovimiento = new MovimientoDto()
                {
                    Fecha = DateTime.Now,
                    CajaId = ultimacaja.Id,
                    ComprobanteId = comprobante.Id,
                    Descripcion = "Pago de Mercaderia",
                    Monto = comprobante.Total,
                    TipoMovimiento = TipoMovimiento.Egreso,
                    UsuarioId = Validacion.UsuarioLogeado
                };

                var formaPago = new FormaPagoEfectivo()
                {
                    ComprobanteId = comprobante.Id,
                    Monto = comprobante.Total,
                    TipoFormaPago = TipoFormaPago.Efectivo,
                };

                var cajaAbierta = _CajaServicio.ObtenerCajaAbierta();
                _CajaServicio.DescontarMontoSistema(cajaAbierta.Id, comprobante.Total);

                _movimientoServicio.Insertar(nuevoMovimiento);
                context.FormasPagos.Add(formaPago);
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


        public IEnumerable<ComprobanteCompraDto> ObtenerComprobanteCompra(string cadenaBuscar)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                return context.Comprobantes.OfType<AccesoDatos.ComprobanteCompra>()
                    .AsNoTracking()
                    //.Where(x => x.Descripcion.Contains(cadenaBuscar))
                    .Select(x => new ComprobanteCompraDto
                    {
                       
                        Id = x.Id,
                        UsuarioId = x.UsuarioId,
                        Descuento = x.Descuento,
                        Fecha = x.Fecha,
                        Numero = x.Numero,
                        ProveedorId = 1,
                        ProveedorStr = context.Proveedores.FirstOrDefault(p => p.Id == x.ProveedorId).Descripcion,
                        TipoComprobante = x.TipoComprobante,
                        Items = x.DetalleComprobantes.Select(d => new DetalleCompraDto
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

        public void ActualizarPrecioCostoItem(long? productoId, long comprobanteId, decimal nuevoPrecio)
        {
            if (productoId == null)
            {
                MessageBox.Show("Ocurrio un error");
                return;
            }

            var comprobante = ObtenerUltimoComprobante();

            var item = comprobante.Items.FirstOrDefault(x => x.ArticuloId == productoId);
            if (item == null)
            {
                MessageBox.Show("Ocurrio un error. El item no se encuentra en la grilla");
                return;
            }

            item.PrecioUnitario = nuevoPrecio;
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
