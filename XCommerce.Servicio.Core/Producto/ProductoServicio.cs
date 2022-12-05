namespace XCommerce.Servicio.Core.Producto
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using XCommerce.AccesoDatos;
    using XCommerce.Servicio.Core.ListaPrecio;
    using XCommerce.Servicio.Core.Producto.DTOs;

    public class ProductoServicio : IProductoServicio
    {
        private static long ProductoSeleccionadoId;
        

        public IEnumerable<ProductoDto> ObtenerParaMesa(string cadenaBuscar, long mesaId)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                return context.Articulos
                   .AsNoTracking()
                       .Include("x=>x.Precios")
                     .Include("Precios.ListaPrecio")
                    .Include("Precios.ListaPrecio.Salones")
                     .Include("Precios.ListaPrecio.Salones.Mesas")
                   .Where(x => x.EstaEliminado == false && x.EstaDiscontinuado == false &&
                    context.Precios.Any(l => l.ListaPrecio.Salon.Any(s => s.Mesas.Any(m => m.Id == mesaId))
                                                                               && l.ArticuloId == x.Id))
                    .Select(x => new ProductoDto
                    {
                        Id = x.Id,
                        Descripcion = x.Descripcion,
                        Codigo = x.Codigo,
                        CodigoBarra = x.CodigoBarra,
                        DescuentaStock = x.DescuentaStock,
                        Precio = context.Precios
                             .FirstOrDefault(l => l.ListaPrecio.Salon.Any(s => s.Mesas.Any(m => m.Id == mesaId))
                                             && l.ArticuloId == x.Id
                                             && l.FechaActualizacion == context.Precios
                                                     .Where(l2 => l2.ListaPrecio.Salon.Any(
                                                                 s2 => s2.Mesas.Any(m2 => m2.Id == mesaId))
                                                             && l2.ArticuloId == x.Id)
                                                     .Max(max => max.FechaActualizacion)).PrecioPublico,                        
                        EstaEliminado = x.EstaEliminado
                    }).ToList();
                    

             
            }
        }

       
        public ProductoDto ObtenerPorCodigoParaMesa(long mesaId, string codigo)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                return context.Articulos
                    .AsNoTracking()
                    .Include("x=>x.Precios")
                    .Include("Precios.ListaPrecio")
                    .Include("Precios.ListaPrecio.Salones")
                    .Include("Precios.ListaPrecio.Salones.Mesas")
                    .Select(x => new ProductoDto
                    {
                        Id = x.Id,
                        Descripcion = x.Descripcion,
                        EstaEliminado = x.EstaEliminado,
                        Codigo = x.Codigo,
                        CodigoBarra = x.CodigoBarra,
                        DescuentaStock = x.DescuentaStock,
                        Precio = context.Precios
                            .FirstOrDefault(l => l.ListaPrecio.Salon.Any(s => s.Mesas.Any(m => m.Id == mesaId))
                                            && l.ArticuloId == x.Id
                                            && l.FechaActualizacion == context.Precios
                                                    .Where(l2 => l2.ListaPrecio.Salon.Any(
                                                                s2 => s2.Mesas.Any(m2 => m2.Id == mesaId))
                                                            && l2.ArticuloId == x.Id)
                                                    .Max(max => max.FechaActualizacion)).PrecioPublico,
                        EstaDiscontinuado = x.EstaDiscontinuado
                    }).FirstOrDefault(x => x.Codigo == codigo
                                            || x.CodigoBarra == codigo);
            }
        }

        // este se obtiene cuando se seleciona en el buscar producto 
        public IEnumerable<ProductoDto> ObtenerParaKiosco(string cadenaBuscar)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                return context.Articulos
                   .AsNoTracking()
                    .Include("Precios.ListaPrecio")
                    .Include("Precios.ListaPrecio.Descripcion")
                    //.Where(x => x.Descripcion.Contains(cadenaBuscar))
                    .Where(x => x.EstaEliminado == false && x.EstaDiscontinuado == false &&
                        context.Precios.Any(l => l.ListaPrecio.Descripcion == "Kiosco" && l.ArticuloId == x.Id))


                    .Select(x => new ProductoDto
                    {
                        Id = x.Id,
                        Descripcion = x.Descripcion,
                        Codigo = x.Codigo,
                        CodigoBarra = x.CodigoBarra,
                        DescuentaStock = x.DescuentaStock,
                        Precio = context.Precios
                            .FirstOrDefault(l => l.ListaPrecio.Descripcion == "Kiosco"
                                            && l.ArticuloId == x.Id
                                            && l.FechaActualizacion == context.Precios
                                            .Where(l2 => l2.ListaPrecio.Descripcion == "Kiosco" && l2.ArticuloId == x.Id)
                                                    .Max(max => max.FechaActualizacion)).PrecioPublico,
                        EstaDiscontinuado=x.EstaDiscontinuado,
                        EstaEliminado = x.EstaEliminado
                    }).ToList();

            }
        }


        public IEnumerable<ProductoDto> ObtenerParaDelivery(string cadenaBuscar)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                return context.Articulos
                   .AsNoTracking()
                    .Include("Precios.ListaPrecio")
                    .Include("Precios.ListaPrecio.Descripcion")
                    //.Where(x => x.Descripcion.Contains(cadenaBuscar))
                    .Where(x => x.EstaEliminado == false && x.EstaDiscontinuado == false &&
                        context.Precios.Any(l => l.ListaPrecio.Descripcion == "Delivery" && l.ArticuloId == x.Id))


                    .Select(x => new ProductoDto
                    {
                        Id = x.Id,
                        Descripcion = x.Descripcion,
                        Codigo = x.Codigo,
                        CodigoBarra = x.CodigoBarra,
                        DescuentaStock = x.DescuentaStock,
                        Precio = context.Precios
                            .FirstOrDefault(l => l.ListaPrecio.Descripcion == "Delivery"
                                            && l.ArticuloId == x.Id
                                            && l.FechaActualizacion == context.Precios
                                            .Where(l2 => l2.ListaPrecio.Descripcion == "Delivery" && l2.ArticuloId == x.Id)
                                                    .Max(max => max.FechaActualizacion)).PrecioPublico,
                        EstaDiscontinuado = x.EstaDiscontinuado,
                        EstaEliminado = x.EstaEliminado
                    }).ToList();

            }
        }
        public ProductoDto ObtenerPorCodigoParaCompra(string codigo)    // funciona correctamente, trae el ultimo precio
        {
            using (var context = new ModeloXCommerceContainer())
            {
                return context.Articulos
                    .AsNoTracking()
                    .Include("x=>x.Precios")
                    .Include("Precios.ListaPrecio")
                    .Where(x => x.EstaEliminado == false && x.EstaDiscontinuado == false &&
                        context.Precios.Any(l => l.ListaPrecio.Descripcion == "Costo" && l.ArticuloId == x.Id))
                    .Select(x => new ProductoDto
                    {
                        Id = x.Id,
                        Descripcion = x.Descripcion,
                        EstaEliminado = x.EstaEliminado,
                        Codigo = x.Codigo,
                        CodigoBarra = x.CodigoBarra,
                        DescuentaStock = x.DescuentaStock,
                        Precio = context.Precios
                            .FirstOrDefault(l => l.ListaPrecio.Descripcion == "Costo"
                                            && l.ArticuloId == x.Id
                                            && l.FechaActualizacion == context.Precios
                                            .Where(l2 => l2.ListaPrecio.Descripcion == "Costo" && l2.ArticuloId == x.Id)
                                                    .Max(max => max.FechaActualizacion)).PrecioCosto,
                        EstaDiscontinuado = x.EstaDiscontinuado,
                    }).FirstOrDefault(x => x.Codigo == codigo
                                            || x.CodigoBarra == codigo);


            }
        }
        public IEnumerable<ProductoDto> ObtenerParaCompra(string cadenaBuscar)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                return context.Articulos
                   .AsNoTracking()
                    .Include("Precios.ListaPrecio")
                    .Include("Precios.ListaPrecio.Descripcion")
                      .Where(x => x.EstaEliminado == false && x.EstaDiscontinuado == false &&
                        context.Precios.Any(l => l.ListaPrecio.Descripcion == "Costo" && l.ArticuloId == x.Id))
                    .Select(x => new ProductoDto
                    {
                        Id = x.Id,
                        Descripcion = x.Descripcion,
                        Codigo = x.Codigo,
                        CodigoBarra = x.CodigoBarra,
                        DescuentaStock = x.DescuentaStock,
                        EstaEliminado = x.EstaEliminado,
                        Precio = context.Precios
                            .FirstOrDefault(l => l.ListaPrecio.Descripcion == "Costo"
                                            && l.ArticuloId == x.Id
                                            && l.FechaActualizacion == context.Precios
                                            .Where(l2 => l2.ListaPrecio.Descripcion == "Costo" && l2.ArticuloId == x.Id)
                                                    .Max(max => max.FechaActualizacion)).PrecioCosto,
                        EstaDiscontinuado = x.EstaDiscontinuado,
                    }).ToList();

            }
        }


        // este es de kiosco,delivery y Bar uno generico 
        public ProductoDto ObtenerPorCodigo(string codigo, string listaPrecio)  // error, devuelve el primer precio en vez del ultimo
        {
            using (var context = new ModeloXCommerceContainer())
            {
                return context.Articulos
                    .AsNoTracking()
                    .Include("x=>x.Precios")
                    .Include("Precios.ListaPrecio")
                    .Select(x => new ProductoDto
                    {
                        Id = x.Id,
                        Descripcion = x.Descripcion,
                        EstaEliminado = x.EstaEliminado,
                        Codigo = x.Codigo,
                        CodigoBarra = x.CodigoBarra,
                        DescuentaStock = x.DescuentaStock,
                        Precio = context.Precios
                            .FirstOrDefault(p => p.ListaPrecio.Descripcion == listaPrecio
                                            && p.ArticuloId == x.Id
                                            && p.FechaActualizacion == context.Precios
                                                    .Where(p2 => p2.ListaPrecio.Descripcion == listaPrecio
                                                            && p2.ArticuloId == x.Id)
                                                    .Max(max => max.FechaActualizacion)).PrecioPublico,
                        EstaDiscontinuado = x.EstaDiscontinuado,
                        HoraVentaMax = context.Precios
                            .FirstOrDefault(p => p.ListaPrecio.Descripcion == listaPrecio
                                            && p.ArticuloId == x.Id
                                            && p.FechaActualizacion == context.Precios
                                                    .Where(p2 => p2.ListaPrecio.Descripcion == listaPrecio
                                                            && p2.ArticuloId == x.Id)
                                                    .Max(max => max.FechaActualizacion)).HoraVenta,
                        Stock = context.Articulos.FirstOrDefault(a => a.Codigo == codigo).Stock,

                    }).FirstOrDefault(x => x.Codigo == codigo
                                            || x.CodigoBarra == codigo);
            }
        }

        public bool TienePrecioParaSalon(string codigo, long salonId, long mesaId)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var articulo = context.Articulos.FirstOrDefault(a => a.Codigo == codigo);

                if (articulo == null)
                {
                    return false;
                }

                var salon = context.Salones.FirstOrDefault(b => b.Id == salonId);

                if (salon == null)
                {
                    return false;
                }

                return context.Precios.Any(l => l.ListaPrecio.Salon.Any(s => s.Mesas.Any(m => m.Id == mesaId)) && l.ArticuloId == articulo.Id);

                
                
            }
        }

        public bool TienePrecioParaGeneral(string codigo, string listaPrecioDescripcion)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var articulo = context.Articulos.FirstOrDefault(a => a.Codigo == codigo);

                if (articulo == null)
                {
                    return false;
                }
                                
                return context.Precios.Any(p => p.ListaPrecio.Descripcion == listaPrecioDescripcion && p.ArticuloId == articulo.Id);
                
            }
        }

        public bool ExisteProducto(string codigo)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var articulo = context.Articulos.FirstOrDefault(a => a.Codigo == codigo);

                if (articulo == null)
                {
                    return false;
                }
                else return true;
                
            }
        }

        public bool SuperaLimiteVenta(long articuloid, decimal cantidadvendida)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var articulo = context.Articulos.Find(articuloid);

                if (articulo.ActivarLimiteVenta == true)
                {
                    if (articulo.LimiteVenta >= cantidadvendida)
                    {
                        return false;
                    }
                    else return true;
                }
                else return false;
            }
        }

        public bool CumpleStockMaximo(long productoId, decimal stockIngresante)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var producto = context.Articulos.Find(productoId);

                var nuevoStock = producto.Stock + stockIngresante;

                if (nuevoStock > producto.StockMaximo) return false;
                else return true;

            }
        }


        public bool CumpleStockMinimo(long productoId, decimal stockEgresante)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var producto = context.Articulos.Find(productoId);

                var nuevoStock = producto.Stock - stockEgresante;

                if (nuevoStock < producto.StockMinimo) return false;
                else return true;

            }
        }

        public bool PermiteStockNegativo(long productoId, decimal cantidadVendida)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var producto = context.Articulos.Find(productoId);

                if (producto.PermiteStockNegativo == false)
                {
                    var nuevoStock = producto.Stock - cantidadVendida;

                    if (nuevoStock >= 0) return true;
                    else return false;
                }
                else return true;
            }
        }

        public void AsignarProducSelecId(long productoId)
        {
            ProductoSeleccionadoId = productoId;
        }

        public long ObtenerProducSelecId()
        {
            return ProductoSeleccionadoId;
        }

        public void LimpiarProducSelecId()
        {
            ProductoSeleccionadoId = 0;
        }
    }
}
