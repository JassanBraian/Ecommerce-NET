using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCommerce.AccesoDatos;
using XCommerce.Servicio.Core.Marca.DTOs;

namespace XCommerce.Servicio.Core.Marca
{
    public class MarcaServicio : IMarcaServicio
    {
        public void Eliminar(long marcaId)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var MarcaEliminar = context.Marcas
                    .FirstOrDefault(x => x.Id == marcaId);

                if (MarcaEliminar == null)
                    throw new Exception("Ocurrio un error al Obtener lA Marca");

                MarcaEliminar.EstaEliminado = true;

                context.SaveChanges();
            }
        }

        public long Insertar(MarcaDto dto)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var MarcaNueva = new AccesoDatos.Marca
                {
                    Descripcion = dto.Descripcion
                };



                context.Marcas.Add(MarcaNueva);

                context.SaveChanges();

                return MarcaNueva.Id;
            }
        }

        public void Modificar(MarcaDto dto)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var MarcaModificar = context.Marcas
                    .FirstOrDefault(x => x.Id == dto.Id);

                if (MarcaModificar == null)
                    throw new Exception("Ocurrio un error al Obtener la Marca");

                MarcaModificar.Descripcion = dto.Descripcion;

                context.SaveChanges();
            }
        }

        public IEnumerable<MarcaDto> Obtener(string cadenaBuscar)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                return context.Marcas
                    .AsNoTracking()
                    .Where(x => x.Descripcion.Contains(cadenaBuscar))
                    .Select(x => new MarcaDto
                    {
                        Id = x.Id,
                        Descripcion = x.Descripcion,
                        EstaEliminado = x.EstaEliminado
                    }).ToList();
            }
        }

        public MarcaDto ObtenerPorDescripcion(string descripcion)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                return context.Marcas
                    .AsNoTracking()
                    .Select(x => new MarcaDto
                    {
                        Id = x.Id,
                        Descripcion = x.Descripcion,
                        EstaEliminado = x.EstaEliminado
                    }).FirstOrDefault(x => x.Descripcion == descripcion);
            }
        }

        public MarcaDto ObtenerPorId(long entidadId)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                return context.Marcas
                    .AsNoTracking()
                    .Select(x => new MarcaDto
                    {
                        Id = x.Id,
                        Descripcion = x.Descripcion,
                        EstaEliminado = x.EstaEliminado
                    }).FirstOrDefault(x => x.Id == entidadId);
            }
        }
    }
}
