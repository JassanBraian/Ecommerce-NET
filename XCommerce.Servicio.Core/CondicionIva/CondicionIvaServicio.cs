using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCommerce.AccesoDatos;
using XCommerce.Servicio.Core.CondicionIva.DTOs;

namespace XCommerce.Servicio.Core.CondicionIva
{
    public class CondicionIvaServicio : ICondicionIvaServicio
    {
        public void Eliminar(long entidadId)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var CondicionIvaEliminar = context.CondicionIvas
                    .FirstOrDefault(x => x.Id == entidadId);

                if (CondicionIvaEliminar == null)
                    throw new Exception("Ocurrio un error al Obtener la Condicion de Iva");

                CondicionIvaEliminar.EstaELiminado = true;

                context.SaveChanges();
            }
        }

        public long Insertar(CondicionIvaDto dto)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var entidadNueva = new AccesoDatos.CondicionIva
                {
                    Descripcion = dto.Descripcion
                };

                context.CondicionIvas.Add(entidadNueva);

                context.SaveChanges();

                return entidadNueva.Id;
            }
        }

        public void Modificar(CondicionIvaDto dto)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var CondicionIvaModificar = context.CondicionIvas
                    .FirstOrDefault(x => x.Id == dto.Id);

                if (CondicionIvaModificar == null)
                    throw new Exception("Ocurrio un error al Obtener la Condicion de IVA");

                CondicionIvaModificar.Descripcion = dto.Descripcion;

                context.SaveChanges();
            }
        }

        public IEnumerable<CondicionIvaDto> Obtener(string cadenaBuscar)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                return context.CondicionIvas
                    .AsNoTracking()
                    .Where(x => x.Descripcion.Contains(cadenaBuscar))
                    .Select(x => new CondicionIvaDto
                    {
                        Id = x.Id,
                        Descripcion = x.Descripcion,
                        EstaEliminado = x.EstaELiminado
                    }).ToList();
            }
        }

        public CondicionIvaDto ObtenerPorId(long entidadId)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                return context.CondicionIvas
                    .AsNoTracking()
                    .Select(x => new CondicionIvaDto
                    {
                        Id = x.Id,
                        Descripcion = x.Descripcion,
                        EstaEliminado = x.EstaELiminado,
                    }).FirstOrDefault(x => x.Id == entidadId);
            }
        }

        public CondicionIvaDto ObtenerPorDescripcion(string descripcion)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                return context.CondicionIvas
                    .AsNoTracking()
                    .Select(x => new CondicionIvaDto
                    {
                        Id = x.Id,
                        Descripcion = x.Descripcion,
                        EstaEliminado = x.EstaELiminado,
                    }).FirstOrDefault(x => x.Descripcion == descripcion);
            }
        }

    }
}
