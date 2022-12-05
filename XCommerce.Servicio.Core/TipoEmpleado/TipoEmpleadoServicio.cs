using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCommerce.AccesoDatos;
using XCommerce.Servicio.Core.TipoEmpleado.DTOs;

namespace XCommerce.Servicio.Core.TipoEmpleado
{
    public class TipoEmpleadoServicio : ITipoEmpleadoSericio
    {
        public void Eliminar(long TipoEmpleadoId)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var TipoEmpleadoEliminar = context.TipoEmpleados
                    .FirstOrDefault(x => x.Id == TipoEmpleadoId);

                if (TipoEmpleadoEliminar == null)
                    throw new Exception("Ocurrio un error al Obtener el tipo de empleado");

                TipoEmpleadoEliminar.EstaEliminado = true;

                context.SaveChanges();
            }
        }

        public bool ExisteTipoEmpleadoDescripcion(string descripcion)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var TipoEmpleado = context.TipoEmpleados
                    .FirstOrDefault(x => x.Descripcion == descripcion);

                if (TipoEmpleado == null) return false;
                else return true;

            }
        }

        public long Insertar(TipoEmpleadoDto tipoEmpleadoDto)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var TipoEmpleadoNuevo = new AccesoDatos.TipoEmpleado
                {
                    Descripcion = tipoEmpleadoDto.Descripcion
                };

                context.TipoEmpleados.Add(TipoEmpleadoNuevo);

                context.SaveChanges();

                return TipoEmpleadoNuevo.Id;
            }
        }

        public void Modificar(TipoEmpleadoDto tipoEmpleadoDto)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var TipoEmpleadoModificar = context.TipoEmpleados
                    .FirstOrDefault(x => x.Id == tipoEmpleadoDto.Id);

                if (TipoEmpleadoModificar == null)
                    throw new Exception("Ocurrio un error al Obtener el tipo empleado");

                TipoEmpleadoModificar.Descripcion = tipoEmpleadoDto.Descripcion;

                context.SaveChanges();
            }
        }

        public IEnumerable<TipoEmpleadoDto> Obtener(string cadenaBuscar)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                return context.TipoEmpleados
                    .AsNoTracking()
                    .Where(x => x.Descripcion.Contains(cadenaBuscar))
                    .Select(x => new TipoEmpleadoDto
                    {
                        Id = x.Id,
                        Descripcion = x.Descripcion,
                        EstaEliminado = x.EstaEliminado
                    }).ToList();
            }
        }

        public TipoEmpleadoDto ObtenerPorDescripcion(string descripcion)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                return context.TipoEmpleados
                    .AsNoTracking()
                    .Select(x => new TipoEmpleadoDto
                    {
                        Id = x.Id,
                        Descripcion = x.Descripcion,
                        EstaEliminado = x.EstaEliminado
                    }).FirstOrDefault(x => x.Descripcion == descripcion);
            }
        }

        public TipoEmpleadoDto ObtenerPorId(long entidadId)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                return context.TipoEmpleados
                    .AsNoTracking()
                    .Select(x => new TipoEmpleadoDto
                    {
                        Id = x.Id,
                        Descripcion = x.Descripcion,
                        EstaEliminado = x.EstaEliminado
                    }).FirstOrDefault(x => x.Id == entidadId);
            }
        }
    }
}
