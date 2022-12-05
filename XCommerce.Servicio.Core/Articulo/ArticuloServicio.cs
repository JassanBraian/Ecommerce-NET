using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCommerce.AccesoDatos;
using XCommerce.Servicio.Core.Articulo.DTOs;
using XCommerce.Servicio.Core.Empleado.DTOs;

namespace XCommerce.Servicio.Core.Articulo
{
    public class ArticuloServicio : IArticuloServicio
    {
        public void Eliminar(long articuloId)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var articuloEliminar = context.Articulos
                    .FirstOrDefault(x => x.Id == articuloId);

                if (articuloEliminar == null)
                    throw new Exception("No se encontro el Articulo");

                articuloEliminar.EstaEliminado = true;

                context.SaveChanges();
            }
        }

        public long Insertar(ArticuloDto dto)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var nuevoArticulo = new AccesoDatos.Articulo
                {
                    Codigo = dto.Codigo,
                    CodigoBarra = dto.CodigoBarra,
                    Abreviatura = dto.Abreviatura,
                    Descripcion = dto.Descripcion,
                    Detalle = dto.Detalle,
                    Foto = dto.Foto,
                    ActivarLimiteVenta = dto.ActivarLimiteVenta,
                    LimiteVenta = dto.LimiteVenta,
                    EstaDiscontinuado = dto.EstaDiscontinuado,
                    StockMaximo = dto.StockMaximo,
                    StockMinimo = dto.StockMinimo,
                    DescuentaStock = dto.DescuentaStock,
                    MarcaId = dto.MarcaId,
                    RubroId = dto.RubroId,
                    Stock = dto.Stock,
                    PermiteStockNegativo = dto.PermiteStockNegativo,
                    EstaEliminado = dto.EstaEliminado,
                    Iva = dto.Iva,
                    
                    
                    //BajaArticulos = new BajaArticulo
                    //{
                    //    Fecha = dto.Fecha,
                    //    Cantidad = dto.Cantidad,
                    //    Observacion = dto.Observacion,
                    //    MotivoBajaId = dto.MotivoBajaId,
                    //    ArticuloId = dto.ArticuloId
                    //}

                };
                context.Articulos.Add(nuevoArticulo);

                var nuevoPrecio = new AccesoDatos.Precio
                {
                    PrecioCosto = dto.PrecioCosto,
                    FechaActualizacion = DateTime.Now,
                    ActivarHoraVenta = false,
                    ListaPrecioId = 1,  
                    ArticuloId = nuevoArticulo.Id,
                    HoraVenta = DateTime.Now,
                    PrecioPublico = 0
                };
                context.Precios.Add(nuevoPrecio);

                context.SaveChanges();

                return nuevoArticulo.Id;
            }
        }

        public void Modificar(ArticuloDto dto)
        {
            using(var context = new ModeloXCommerceContainer())
            {
                // Modif articulo
                var articuloModificar = context.Articulos
                    .FirstOrDefault(x => x.Id == dto.Id);

                if (articuloModificar == null)
                    throw new Exception("No se encontro el Articulo");

                articuloModificar.Codigo = dto.Codigo;
                articuloModificar.CodigoBarra = dto.CodigoBarra;
                articuloModificar.Abreviatura = dto.Abreviatura;
                articuloModificar.Descripcion = dto.Descripcion;
                articuloModificar.Detalle = dto.Detalle;
                articuloModificar.Foto = dto.Foto;
                articuloModificar.ActivarLimiteVenta = dto.ActivarLimiteVenta;
                articuloModificar.LimiteVenta = dto.LimiteVenta;
                articuloModificar.EstaDiscontinuado = dto.EstaDiscontinuado;
                articuloModificar.StockMaximo = dto.StockMaximo;
                articuloModificar.StockMinimo = dto.StockMinimo;
                articuloModificar.DescuentaStock = dto.DescuentaStock;
                articuloModificar.MarcaId = dto.MarcaId;
                articuloModificar.RubroId = dto.RubroId;
                articuloModificar.Stock = dto.Stock;
                articuloModificar.Iva = dto.Iva;

                // Modif Precio
                var precioModificar = context.Precios
                    .FirstOrDefault(x => x.ArticuloId == dto.Id);

                if (precioModificar == null)
                    throw new Exception("No se encontro el Precio");

                precioModificar.PrecioCosto = dto.PrecioCosto;

                context.SaveChanges();
            }
        }

        public IEnumerable<ArticuloDto> Obtener(string cadenaBuscar)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                return context.Articulos
                    .Where(x => x.Descripcion.Contains(cadenaBuscar)
                            || x.Detalle.Contains(cadenaBuscar)
                            || x.Abreviatura.Contains(cadenaBuscar)
                            || x.Codigo == cadenaBuscar
                            || x.CodigoBarra == cadenaBuscar)
                    .Select(x => new ArticuloDto
                    {
                        Id = x.Id,
                        Codigo = x.Codigo,
                        CodigoBarra = x.CodigoBarra,
                        Abreviatura = x.Abreviatura,
                        Descripcion = x.Descripcion,
                        Detalle = x.Detalle,
                        Foto = x.Foto,
                        ActivarLimiteVenta = x.ActivarLimiteVenta,
                        LimiteVenta = x.LimiteVenta,
                        EstaDiscontinuado = x.EstaDiscontinuado,
                        StockMaximo = x.StockMaximo,
                        StockMinimo = x.StockMinimo,
                        DescuentaStock = x.DescuentaStock,
                        MarcaId = x.MarcaId,
                        RubroId = x.RubroId,
                        EstaEliminado = x.EstaEliminado,
                        Stock = x.Stock,
                        Iva = x.Iva,
                        PrecioCosto = context.Precios.FirstOrDefault(p => p.ArticuloId == x.Id 
                            && p.ListaPrecio.Id == 1).PrecioCosto,

                    }).ToList();
            }
        }

        public ArticuloDto ObtenerPorId(long entidadId)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                return context.Articulos
                    .Select(x => new ArticuloDto
                    {
                        Id = x.Id,
                        Codigo = x.Codigo,
                        CodigoBarra = x.CodigoBarra,
                        Abreviatura = x.Abreviatura,
                        Descripcion = x.Descripcion,
                        Detalle = x.Detalle,
                        Foto = x.Foto,
                        ActivarLimiteVenta = x.ActivarLimiteVenta,
                        LimiteVenta = x.LimiteVenta,
                        EstaDiscontinuado = x.EstaDiscontinuado,
                        StockMaximo = x.StockMaximo,
                        StockMinimo = x.StockMinimo,
                        DescuentaStock = x.DescuentaStock,
                        MarcaId = x.MarcaId,
                        RubroId = x.RubroId,
                        EstaEliminado = x.EstaEliminado,
                        Stock = x.Stock,
                        Iva = x.Iva,
                        PrecioCosto = context.Precios.FirstOrDefault(p => p.ArticuloId == x.Id
                            && p.ListaPrecio.Id == 1).PrecioCosto,

                    }).FirstOrDefault(x => x.Id == entidadId);
            }
        }

        public long ObtenerSiguienteNumero()
        {
            using (var context = new ModeloXCommerceContainer())
            {
                if (context.Articulos.Any())
                {
                    return context.Articulos.Max(x => x.Id) + 1;
                }
                else
                {
                    return 1;
                }
            }
        }

        public bool CodigoYaExiste(string codigo)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                return context.Articulos.Any(x => x.Codigo == codigo);
            }
        }

        public bool CodigoBarraYaExiste(string codigoBarra)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                return context.Articulos.Any(x => x.CodigoBarra == codigoBarra);
            }
        }

        public void DescontarStock(long articuloid, decimal cantidadEliminar)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var articuloDescontar = context.Articulos
                    .FirstOrDefault(x => x.Id == articuloid);

                if (articuloDescontar == null)
                    throw new Exception("No se encontro el Articulo");

                articuloDescontar.Stock = articuloDescontar.Stock - cantidadEliminar;
              

                context.SaveChanges();
            }
        }

        public void AumentarStock(long articuloid, decimal cantidadAumentar)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var articuloAumentar = context.Articulos
                    .FirstOrDefault(x => x.Id == articuloid);

                if (articuloAumentar == null)
                    throw new Exception("No se encontro el Articulo");

                articuloAumentar.Stock = articuloAumentar.Stock + cantidadAumentar;


                context.SaveChanges();
            }
        }

        public void Discontinuar(long articuloid)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var articuloModificar = context.Articulos
                    .FirstOrDefault(x => x.Id == articuloid);

                if (articuloModificar == null)
                    throw new Exception("No se encontro el Articulo");

                articuloModificar.EstaDiscontinuado = true;

                context.SaveChanges();

            }
        }

        public ArticuloDto ObtenerPorCodigo(string codigo)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                return context.Articulos
                    .Select(x => new ArticuloDto
                    {
                        Id = x.Id,
                        Codigo = x.Codigo,
                        CodigoBarra = x.CodigoBarra,
                        Abreviatura = x.Abreviatura,
                        Descripcion = x.Descripcion,
                        Detalle = x.Detalle,
                        Foto = x.Foto,
                        ActivarLimiteVenta = x.ActivarLimiteVenta,
                        LimiteVenta = x.LimiteVenta,
                        EstaDiscontinuado = x.EstaDiscontinuado,
                        StockMaximo = x.StockMaximo,
                        StockMinimo = x.StockMinimo,
                        DescuentaStock = x.DescuentaStock,
                        MarcaId = x.MarcaId,
                        RubroId = x.RubroId,
                        EstaEliminado = x.EstaEliminado,
                        Stock = x.Stock,
                        Iva = x.Iva,
                        PrecioCosto = context.Precios.FirstOrDefault(p => p.ArticuloId == x.Id
                            && p.ListaPrecio.Id == 1).PrecioCosto,

                    }).FirstOrDefault(x => x.Codigo == codigo);
            }
        }

        
    }
}
