using System;
using System.Collections.Generic;
using System.Linq;
using XCommerce.AccesoDatos;
using XCommerce.Servicio.Core.ListaPrecio.DTOs;
using XCommerce.Servicio.Core.Salon.DTOs;

namespace XCommerce.Servicio.Core.Salon
{
    public class SalonServicio : ISalonServicio

    {
        public void Eliminar(long salonId)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var SalonEliminar = context.Salones
                    .FirstOrDefault(x => x.Id == salonId);

                if (SalonEliminar == null)
                    throw new Exception("Ocurrio un error al Obtener el salon");

                SalonEliminar.EstaEliminado = true;

                context.SaveChanges();
            }
        }

        public long Insertar(SalonDto dto)
        {
            using (var context = new ModeloXCommerceContainer())
            {

                var SalonNuevo = new AccesoDatos.Salon
                {
                    Descripcion = dto.Descripcion,
                    ListaPrecioId = dto.ListaPrecioId,
                    
                };

                context.Salones.Add(SalonNuevo);

                context.SaveChanges();

                return SalonNuevo.Id;
            }
        }

        public void Modificar(SalonDto dto)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var SalonModificar = context.Salones
                    .FirstOrDefault(x => x.Id == dto.Id);

                if (SalonModificar == null)
                    throw new Exception("Ocurrio un error al Obtener el salon");

                SalonModificar.Descripcion = dto.Descripcion;
                SalonModificar.ListaPrecioId = dto.ListaPrecioId;

                context.SaveChanges();
            }
        }

        public IEnumerable<SalonDto> Obtener(string cadenaBuscar)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                return context.Salones
                    .AsNoTracking()
                    .Where(x => x.Descripcion.Contains(cadenaBuscar))
                    .Select(x => new SalonDto
                    {
                        Id = x.Id,
                        Descripcion = x.Descripcion,
                        ListaPrecioId = x.ListaPrecioId,
                        ListaPrecioDescripcion = x.ListaPrecio.Descripcion,
                        EstaEliminado = x.EstaEliminado
                    }).ToList();
            }
        }

        public SalonDto ObtenerPorId(long entidadId)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                return context.Salones
                    .AsNoTracking()
                    .Select(x => new SalonDto
                    {
                        Id = x.Id,
                        Descripcion = x.Descripcion,
                        ListaPrecioId = x.ListaPrecioId,
                        EstaEliminado = x.EstaEliminado
                    }).FirstOrDefault(x => x.Id == entidadId);
            }
        }

        public IEnumerable<SalonDto> ObtenerSalonesExistente(string cadenaBuscar)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                return context.Salones
                    .AsNoTracking()
                    .Where(x => x.Descripcion.Contains(cadenaBuscar) && x.EstaEliminado == false)
                    .Select(x => new SalonDto
                    {
                        Id = x.Id,
                        Descripcion = x.Descripcion,
                        ListaPrecioId = x.ListaPrecioId,
                        ListaPrecioDescripcion = x.ListaPrecio.Descripcion,
                        EstaEliminado = x.EstaEliminado
                    }).ToList();
            }
        }
    }
}
