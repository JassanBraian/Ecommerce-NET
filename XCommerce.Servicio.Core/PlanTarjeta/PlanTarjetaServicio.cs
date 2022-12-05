using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCommerce.AccesoDatos;
using XCommerce.Servicio.Core.PlanTarjeta.DTOs;

namespace XCommerce.Servicio.Core.PlanTarjeta
{
    public class PlanTarjetaServicio : IPlanTarjeta
    {
        public void Eliminar(long planTarjetaId)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var planTarjetaEliminar = context.PlanesTarjetas
                    .FirstOrDefault(x => x.Id == planTarjetaId);

                if (planTarjetaEliminar == null)
                    throw new Exception("Ocurrio un error al Obtener el Plan Tarjeta");

                planTarjetaEliminar.EstaEliminado = true;

                context.SaveChanges();
            }
        }

        public long Insertar(PlanTarjetaDto dto)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var planTarjetaNueva = new AccesoDatos.PlanTarjeta
                {
                    Descripcion = dto.Descripcion,
                    Alicuota = dto.Alicuota,
                    TarjetaId = dto.TarjetaId,
                    EstaEliminado = dto.EstaEliminado
                };

                context.PlanesTarjetas.Add(planTarjetaNueva);

                context.SaveChanges();

                return planTarjetaNueva.Id;
            }
        }

        public void Modificar(PlanTarjetaDto dto)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var planTarjetaModif = context.PlanesTarjetas
                    .FirstOrDefault(x => x.Id == dto.Id);

                if (planTarjetaModif == null)
                    throw new Exception("Ocurrio un error al Obtener el Plan Tarjeta");

                planTarjetaModif.Descripcion = dto.Descripcion;
                planTarjetaModif.Alicuota = dto.Alicuota;
                planTarjetaModif.TarjetaId = dto.TarjetaId;
                planTarjetaModif.EstaEliminado = dto.EstaEliminado;


                context.SaveChanges();
            }
        }

        public IEnumerable<PlanTarjetaDto> Obtener(string cadenaBuscar)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                return context.PlanesTarjetas
                    .AsNoTracking()
                    .Where(x => x.Descripcion.Contains(cadenaBuscar))
                    .Select(x => new PlanTarjetaDto
                    {
                        Id = x.Id,
                        Descripcion = x.Descripcion,
                        Alicuota = x.Alicuota,
                        TarjetaId = x.TarjetaId,
                        EstaEliminado = x.EstaEliminado
                    }).ToList();
            }
        }

        public PlanTarjetaDto ObtenerPorDescripcion(string descripcion)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                return context.PlanesTarjetas
                    .AsNoTracking()
                    .Select(x => new PlanTarjetaDto
                    {
                        Id = x.Id,
                        Descripcion = x.Descripcion,
                        Alicuota = x.Alicuota,
                        TarjetaId = x.TarjetaId,
                        EstaEliminado = x.EstaEliminado
                    }).FirstOrDefault(x => x.Descripcion == descripcion);
            }
        }

        public PlanTarjetaDto ObtenerPorId(long entidadId)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                return context.PlanesTarjetas
                    .AsNoTracking()
                    .Select(x => new PlanTarjetaDto
                    {
                        Id = x.Id,
                        Descripcion = x.Descripcion,
                        Alicuota = x.Alicuota,
                        TarjetaId = x.TarjetaId,
                        EstaEliminado = x.EstaEliminado
                    }).FirstOrDefault(x => x.Id == entidadId);
            }
        }

        public IEnumerable<PlanTarjetaDto> ObtenerSegunTarjetaId(long tarjetaId)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                return context.PlanesTarjetas
                    .AsNoTracking()
                    .Where(x => x.TarjetaId==(tarjetaId))
                    .Select(x => new PlanTarjetaDto
                    {
                        Id = x.Id,
                        Descripcion = x.Descripcion,
                        Alicuota = x.Alicuota,
                        TarjetaId = x.TarjetaId,
                        EstaEliminado = x.EstaEliminado
                    }).ToList();
            }
        }
    }
}
