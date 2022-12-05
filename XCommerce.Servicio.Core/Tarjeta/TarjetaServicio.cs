using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCommerce.AccesoDatos;
using XCommerce.Servicio.Core.Tarjeta.DTOs;

namespace XCommerce.Servicio.Core.Tarjeta
{
    public class TarjetaServicio : ITarjetaServicio
    {
        public void Eliminar(long tarjetaId)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var TarjetaEliminar = context.TarjetaSet
                    .FirstOrDefault(x => x.Id == tarjetaId);

                if (TarjetaEliminar == null)
                    throw new Exception("Ocurrio un error al Obtener la Tarjeta");

                TarjetaEliminar.EstaEliminado = true;

                context.SaveChanges();
            }
        }

        public long Insertar(TarjetaDto dto)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var TarjetaNueva = new AccesoDatos.Tarjeta
                {
                    Descripcion = dto.Descripcion,
                    EstaEliminado = dto.EstaEliminado
                };

                context.TarjetaSet.Add(TarjetaNueva);

                context.SaveChanges();

                return TarjetaNueva.Id;
            }
        }

        public void Modificar(TarjetaDto dto)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var tarjetaModif = context.TarjetaSet
                    .FirstOrDefault(x => x.Id == dto.Id);

                if (tarjetaModif == null)
                    throw new Exception("Ocurrio un error al Obtener la Tarjeta");

                tarjetaModif.Descripcion = dto.Descripcion;
                tarjetaModif.EstaEliminado = dto.EstaEliminado;

                context.SaveChanges();
            }
        }

        public IEnumerable<TarjetaDto> Obtener(string cadenaBuscar)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                return context.TarjetaSet
                    .AsNoTracking()
                    .Where(x => x.Descripcion.Contains(cadenaBuscar))
                    .Select(x => new TarjetaDto
                    {
                        Id = x.Id,
                        Descripcion = x.Descripcion,
                        EstaEliminado = x.EstaEliminado
                    }).ToList();
            }
        }

        public TarjetaDto ObtenerPorDescripcion(string descripcion)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                return context.TarjetaSet
                    .AsNoTracking()
                    .Select(x => new TarjetaDto
                    {
                        Id = x.Id,
                        Descripcion = x.Descripcion,
                        EstaEliminado = x.EstaEliminado
                    }).FirstOrDefault(x => x.Descripcion == descripcion);
            }
        }

        public TarjetaDto ObtenerPorId(long entidadId)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                return context.TarjetaSet
                    .AsNoTracking()
                    .Select(x => new TarjetaDto
                    {
                        Id = x.Id,
                        Descripcion = x.Descripcion,
                        EstaEliminado = x.EstaEliminado
                    }).FirstOrDefault(x => x.Id == entidadId);
            }
        }
        public bool ExisteTarjeta(string descripcion)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var tarjeta = context.TarjetaSet.FirstOrDefault(a => a.Descripcion == descripcion);

                if (tarjeta == null)
                {
                    return false;
                }
                else return true;

            }
        }
    }
}
