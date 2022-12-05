using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCommerce.AccesoDatos;
using XCommerce.Servicio.Core.Precio.DTOs;
using System.Data.Entity;
using XCommerce.Servicio.Core.Articulo.DTOs;
using XCommerce.Servicio.Core.ListaPrecio.DTOs;
using XCommerce.Servicio.Core.ListaPrecio;
using XCommerce.Servicio.Core.Articulo;
using System.Windows.Forms;

namespace XCommerce.Servicio.Core.Precio
{
    public class PrecioServicio : IPrecioServicio
    {
        IListaPrecioServicio _listaPrecioServicio = new ListaPrecioServicio();
        IArticuloServicio _articuloServicio = new ArticuloServicio();

        public void ActualizarPrecioPublico(PrecioDto precio, decimal precioCostoNuevo)
        {
            var rentabilidad = _listaPrecioServicio.ObtenerPorId(precio.ListaPrecioId).Rentabilidad;

            var precioPublicoNuevo = precioCostoNuevo * rentabilidad / 100 + precioCostoNuevo;

            using (var context = new ModeloXCommerceContainer())
            {
                var precioModifica = context.Precios
                    .FirstOrDefault(x => x.Id == precio.Id);

                if (precioModifica == null)
                    throw new Exception(@"Ocurrio un error");

                precioModifica.PrecioPublico = precioPublicoNuevo;
                
                context.SaveChanges();
            }
        }

        
        public long Insertar(PrecioDto dto)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var nuevoPrecio = new AccesoDatos.Precio
                {
                    PrecioPublico = dto.PrecioPublico,
                    PrecioCosto = dto.PrecioCosto,
                    ArticuloId = (int)dto.ArticuloId,
                    ListaPrecioId = dto.ListaPrecioId,
                    ActivarHoraVenta = dto.ActivarHoraVenta,
                    HoraVenta = dto.HoraVenta,
                    FechaActualizacion = dto.FechaActualizacion
                };

                context.Precios.Add(nuevoPrecio);

                context.SaveChanges();

                return nuevoPrecio.Id;
            }
        }

        public void Modificar(PrecioDto dto)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var precioModifica = context.Precios
                    .FirstOrDefault(x => x.Id == dto.Id);

                if (precioModifica == null)
                    throw new Exception(@"Ocurrio un error");

                precioModifica.PrecioPublico = dto.PrecioPublico;
                precioModifica.PrecioCosto = dto.PrecioCosto;
                precioModifica.ArticuloId = (int)dto.ArticuloId;
                precioModifica.ListaPrecioId = dto.ListaPrecioId;
                precioModifica.ActivarHoraVenta = dto.ActivarHoraVenta;
                precioModifica.HoraVenta = dto.HoraVenta;
                precioModifica.FechaActualizacion = dto.FechaActualizacion;


                context.SaveChanges();
            }
        }

        public IEnumerable<PrecioDto> Obtener(string cadenaBuscar)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                return context.Precios
                    .Include(x => x.Articulo)
                    .AsNoTracking()
                    .Where(x => x.Articulo.Descripcion.Contains(cadenaBuscar))
                    .Select(x => new PrecioDto
                    {
                        Id = x.Id,
                        PrecioPublico = x.PrecioPublico,
                        PrecioCosto = x.PrecioCosto,
                        ArticuloId = x.ArticuloId,
                        ListaPrecioId = x.ListaPrecioId,
                        ListaPrecio = x.ListaPrecio.Descripcion,
                        Articulo = x.Articulo.Descripcion,
                        HoraVenta = x.HoraVenta,
                        ActivarHoraVenta = x.ActivarHoraVenta,
                        FechaActualizacion = x.FechaActualizacion

                    }).ToList();
            }
        }

        public IEnumerable<PrecioDto> ObtenerPorListaPrecioDescrip(string cadenaBuscar, string listaPrecioDescrip)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                return context.Precios
                    .Include(x => x.Articulo)
                    .AsNoTracking()
                    .Where(x => x.Articulo.Descripcion.Contains(cadenaBuscar)
                        && x.ListaPrecio.Descripcion == listaPrecioDescrip)
                    .Select(x => new PrecioDto
                    {
                        Id = x.Id,
                        PrecioPublico = x.PrecioPublico,
                        PrecioCosto = x.PrecioCosto,
                        ArticuloId = x.ArticuloId,
                        ListaPrecioId = x.ListaPrecioId,
                        ListaPrecio = x.ListaPrecio.Descripcion,
                        Articulo = x.Articulo.Descripcion,
                        HoraVenta = x.HoraVenta,
                        ActivarHoraVenta = x.ActivarHoraVenta,
                        FechaActualizacion = x.FechaActualizacion

                    }).ToList();
            }
        }

        public IEnumerable<PrecioDto> ObtenerSegunArticulo(long articuloId)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                return context.Precios
                    .Include(x => x.Articulo)
                    .AsNoTracking()
                    .Where(x => x.Articulo.Id == articuloId)
                    .Select(x => new PrecioDto
                    {
                        Id = x.Id,
                        PrecioPublico = x.PrecioPublico,
                        PrecioCosto = x.PrecioCosto,
                        ArticuloId = x.ArticuloId,
                        ListaPrecioId = x.ListaPrecioId,
                        ListaPrecio = x.ListaPrecio.Descripcion,
                        Articulo = x.Articulo.Descripcion,
                        HoraVenta = x.HoraVenta,
                        ActivarHoraVenta = x.ActivarHoraVenta,
                        FechaActualizacion = x.FechaActualizacion

                    }).ToList();
            }
        }

        public PrecioDto ObtenerPorId(long? entidadId)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                return context.Precios
                    .Include(x => x.Articulo)
                    .AsNoTracking()
                    .Select(x => new PrecioDto
                    {
                        Id = x.Id,
                        PrecioPublico = x.PrecioPublico,
                        PrecioCosto = x.PrecioCosto,
                        ArticuloId = x.ArticuloId,
                        ListaPrecioId = x.ListaPrecioId,
                        ListaPrecio = x.ListaPrecio.Descripcion,
                        Articulo = x.Articulo.Descripcion,
                        HoraVenta = x.HoraVenta,
                        ActivarHoraVenta = x.ActivarHoraVenta,
                        FechaActualizacion = x.FechaActualizacion

                    }).FirstOrDefault(x => x.Id == entidadId);
            }
        }

        public bool PrecioYaExiste(long ArticuloId, long ListaPrecioId)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                return context.Precios.Any(x => x.ArticuloId == ArticuloId && x.ListaPrecioId == ListaPrecioId);

            }
        }

        public void ActualizarRentabilidad(PrecioDto precio, decimal rentabilidadNueva)
        {
            var articulo = _articuloServicio.ObtenerPorId(precio.ArticuloId);

            var precioPublicoNuevo = articulo.PrecioCosto * rentabilidadNueva / 100 + articulo.PrecioCosto;

            using (var context = new ModeloXCommerceContainer())
            {
                var precioModifica = context.Precios
                    .FirstOrDefault(x => x.Id == precio.Id);

                if (precioModifica == null)
                    throw new Exception(@"Ocurrio un error");

                precioModifica.PrecioPublico = precioPublicoNuevo;

                context.SaveChanges();
            }
        }

        public IEnumerable<PrecioDto> ObtenerSegunListaPrecio(long listaPrecioId)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                return context.Precios
                    .Include(x => x.Articulo)
                    .AsNoTracking()
                    .Where(x => x.ListaPrecioId == listaPrecioId)
                    .Select(x => new PrecioDto
                    {
                        Id = x.Id,
                        PrecioPublico = x.PrecioPublico,
                        PrecioCosto = x.PrecioCosto,
                        ArticuloId = x.ArticuloId,
                        ListaPrecioId = x.ListaPrecioId,
                        ListaPrecio = x.ListaPrecio.Descripcion,
                        Articulo = x.Articulo.Descripcion,
                        HoraVenta = x.HoraVenta,
                        ActivarHoraVenta = x.ActivarHoraVenta,
                        FechaActualizacion = x.FechaActualizacion

                    }).ToList();
            }
        }

        public PrecioDto ObtenerPorArticuloYListaCosto(long articuloId)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                return context.Precios
                    .Include(x => x.Articulo)
                    .AsNoTracking()
                    .Select(x => new PrecioDto
                    {
                        Id = x.Id,
                        PrecioPublico = x.PrecioPublico,
                        PrecioCosto = x.PrecioCosto,
                        ArticuloId = x.ArticuloId,
                        ListaPrecioId = x.ListaPrecioId,
                        ListaPrecio = x.ListaPrecio.Descripcion,
                        Articulo = x.Articulo.Descripcion,
                        HoraVenta = x.HoraVenta,
                        ActivarHoraVenta = x.ActivarHoraVenta,
                        FechaActualizacion = x.FechaActualizacion

                    }).FirstOrDefault(x => x.ArticuloId == articuloId && x.ListaPrecioId == 1);
            }
        }
    }
}
