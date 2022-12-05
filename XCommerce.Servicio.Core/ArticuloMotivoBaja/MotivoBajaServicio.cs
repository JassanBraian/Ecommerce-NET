using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCommerce.AccesoDatos;
using XCommerce.Servicio.Core.MotivoBaja.DTOs;

namespace XCommerce.Servicio.Core.MotivoBaja
{
    public class MotivoBajaServicio : IMotivoBajaServicio
    {
        public void Eliminar(long MotivoBajaId)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var MotivoBajaEliminar = context.MotivosBajas
                    .FirstOrDefault(x => x.Id == MotivoBajaId);

                if (MotivoBajaEliminar == null)
                    throw new Exception("Ocurrio un error al Obtener el Motivo de Baja");


                MotivoBajaEliminar.EstaEliminado = true;

                context.SaveChanges();
            }
        }

        public long Insertar(MotivoBajaDto dto)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var MotivoBajaNueva = new AccesoDatos.MotivoBaja
                {
                    Descripcion = dto.Descripcion
                };

                context.MotivosBajas.Add(MotivoBajaNueva);

                context.SaveChanges();

                return MotivoBajaNueva.Id;
            }
        }

        public void Modificar(MotivoBajaDto dto)
        {

            using (var context = new ModeloXCommerceContainer())
            {
                var MotivoBajaModificar = context.MotivosBajas
                    .FirstOrDefault(x => x.Id == dto.Id);

                if (MotivoBajaModificar == null)
                    throw new Exception("Ocurrio un error al Obtener el Motivo de Baja");

                MotivoBajaModificar.Descripcion = dto.Descripcion;

                context.SaveChanges();
            }
        }

        public IEnumerable<MotivoBajaDto> Obtener(string cadenaBuscar)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                return context.MotivosBajas
                    .AsNoTracking()
                    .Where(x => x.Descripcion.Contains(cadenaBuscar))
                    .Select(x => new MotivoBajaDto
                    {
                        Id = x.Id,
                        Descripcion = x.Descripcion,
                        EstaEliminado = x.EstaEliminado
                    }).ToList();
            }
        }

        public MotivoBajaDto ObtenerPorId(long entidadId)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                return context.MotivosBajas
                    .AsNoTracking()
                    .Select(x => new MotivoBajaDto
                    {
                        Id = x.Id,
                        Descripcion = x.Descripcion,
                        EstaEliminado = x.EstaEliminado

                    }).FirstOrDefault(x => x.Id == entidadId);
            }
        }
    }
}
