using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCommerce.AccesoDatos;
using XCommerce.Servicio.Core.MotivoReserva.DTOs;

namespace XCommerce.Servicio.Core.MotivoReserva
{
    public class MotivoReservaServicio : IMotivoReservaServicio
    {
        public void Eliminar(long motivoreservaId)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var _motivoreservaEliminar = context.MotivoReservas
                    .FirstOrDefault(x => x.Id == motivoreservaId);

                if (_motivoreservaEliminar == null)
                    throw new Exception(@"Ocurrio un error, no se encontro el Motivo de Reserva.");

                _motivoreservaEliminar.EstaEliminado = true;

                context.SaveChanges();
            }
        }

        public long Insertar(MotivoReservaDto motivoreservaDto)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var motivoreservaNueva = new AccesoDatos.MotivoReserva
                {
                    Descripcion = motivoreservaDto.Descripcion
                };

                context.MotivoReservas.Add(motivoreservaNueva);

                context.SaveChanges();

                return motivoreservaNueva.Id;
            }
        }

        public void Modificar(MotivoReservaDto motivoreservaDto)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var motivoreservaModificar = context.MotivoReservas
                    .FirstOrDefault(x => x.Id == motivoreservaDto.Id);

                if (motivoreservaModificar == null)
                    throw new Exception(@"Ocurrio un error, no se encontro el Motivo de Reserva.");

                motivoreservaModificar.Descripcion = motivoreservaDto.Descripcion;

                context.SaveChanges();
            }
        }

        public IEnumerable<MotivoReservaDto> Obtener(string cadenaBuscar)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                return context.MotivoReservas
                    .AsNoTracking()
                    .Where(x => x.Descripcion.Contains(cadenaBuscar))
                    .Select(x => new MotivoReservaDto
                    {
                        Id = x.Id,
                        Descripcion = x.Descripcion,
                        EstaEliminado = x.EstaEliminado
                    }).ToList();
            }
        }

        public MotivoReservaDto ObtenerPorId(long motivoreservaId)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                return context.MotivoReservas
                    .AsNoTracking()
                    .Select(x => new MotivoReservaDto
                    {
                        Id = x.Id,
                        Descripcion = x.Descripcion,
                        EstaEliminado = x.EstaEliminado
                    }).FirstOrDefault(x => x.Id == motivoreservaId);
            }
        }
    }
}
