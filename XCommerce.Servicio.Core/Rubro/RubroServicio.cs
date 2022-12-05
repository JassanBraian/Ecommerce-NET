using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCommerce.AccesoDatos;
using XCommerce.Servicio.Core.Rubro.DTOs;

namespace XCommerce.Servicio.Core.Rubro
{
    public class RubroServicio : IRubroServicio
    {
        public void Eliminar(long RubroId)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var RubroEliminar = context.Rubros
                    .FirstOrDefault(x => x.Id == RubroId);

                if (RubroEliminar == null)
                    throw new Exception("Ocurrio un error al Obtener el Rubro");

                RubroEliminar.EstaEliminado = true;

                context.SaveChanges();
            }
        }

        public long Insertar(RubroDto dto)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var RubroNuevo = new AccesoDatos.Rubro
                {
                    Descripcion = dto.Descripcion
                };

                context.Rubros.Add(RubroNuevo);

                context.SaveChanges();

                return RubroNuevo.Id;
            }
        }

        public void Modificar(RubroDto dto)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var RubroModificar = context.Rubros
                    .FirstOrDefault(x => x.Id == dto.Id);

                if (RubroModificar == null)
                    throw new Exception("Ocurrio un error al Obtener el rubro");

                RubroModificar.Descripcion = dto.Descripcion;

                context.SaveChanges();
            }
        }

        public IEnumerable<RubroDto> Obtener(string cadenaBuscar)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                return context.Rubros
                    .AsNoTracking()
                    .Where(x => x.Descripcion.Contains(cadenaBuscar))
                    .Select(x => new RubroDto
                    {
                        Id = x.Id,
                        Descripcion = x.Descripcion,
                        EstaEliminado = x.EstaEliminado
                    }).ToList();
            }
        }

        public RubroDto ObtenerPorDescripcion(string descripcion)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                return context.Rubros
                    .AsNoTracking()
                    .Select(x => new RubroDto
                    {
                        Id = x.Id,
                        Descripcion = x.Descripcion,
                        EstaEliminado = x.EstaEliminado
                    }).FirstOrDefault(x => x.Descripcion == descripcion);
            }
        }

        public RubroDto ObtenerPorId(long entidadId)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                return context.Rubros
                    .AsNoTracking()
                    .Select(x => new RubroDto
                    {
                        Id = x.Id,
                        Descripcion = x.Descripcion,
                        EstaEliminado = x.EstaEliminado
                    }).FirstOrDefault(x => x.Id == entidadId);
            }
        }
    }
}
