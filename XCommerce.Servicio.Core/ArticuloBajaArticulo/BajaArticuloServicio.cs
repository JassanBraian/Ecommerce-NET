using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCommerce.AccesoDatos;
using XCommerce.Servicio.Core.BajaArticulo.DTOs;

namespace XCommerce.Servicio.Core.BajaArticulo
{
    public class BajaArticuloServicio : IBajaArticuloServicio
    {
        public void Eliminar(long entidadId)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var bajaArticuloEliminar = context.BajaArticulos
                    .FirstOrDefault(x => x.Id == entidadId);

                if (bajaArticuloEliminar == null)
                    throw new Exception("Ocurrio un error al Obtener la Baja de Articulo");

                bajaArticuloEliminar.EstaEliminado = true;

                context.SaveChanges();
            }
        }

        public long Insertar(BajaArticuloDto articuloBajadto)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var bajaArticuloNuevo = new AccesoDatos.BajaArticulo
                {
                    Observacion = articuloBajadto.Observacion,
                    EstaEliminado = articuloBajadto.EstaEliminado,
                    Fecha = articuloBajadto.Fecha,
                    Cantidad = articuloBajadto.Cantidad,
                    MotivoBajaId = articuloBajadto.MotivoBajaId,
                    ArticuloId = (int)articuloBajadto.ArticuloId

                };

                context.BajaArticulos.Add(bajaArticuloNuevo);

                context.SaveChanges();

                return bajaArticuloNuevo.Id;
            }
        }

        public void Modificar(BajaArticuloDto dto)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var bajaArticuloModificar = context.BajaArticulos
                    .FirstOrDefault(x => x.Id == dto.Id);

                if (bajaArticuloModificar == null)
                    throw new Exception("Ocurrio un error al Obtener el Articulo de Baja");

                bajaArticuloModificar.Fecha = dto.Fecha;
                bajaArticuloModificar.Cantidad = dto.Cantidad;
                bajaArticuloModificar.Observacion = dto.Observacion;
                bajaArticuloModificar.MotivoBajaId = dto.MotivoBajaId;
                bajaArticuloModificar.ArticuloId = dto.ArticuloId;

                context.SaveChanges();
            }
        }

        public IEnumerable<BajaArticuloDto> Obtener(string cadenaBuscar)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                return context.BajaArticulos
                    .AsNoTracking()
                    .Where(x => x.Observacion.Contains(cadenaBuscar))
                    .Select(x => new BajaArticuloDto
                    {
                        Id = x.Id,
                        Fecha = x.Fecha,
                        Cantidad = x.Cantidad,
                        Observacion =x.Observacion,
                        MotivoBajaId =x.MotivoBajaId,
                        ArticuloId = x.ArticuloId,
                        EstaEliminado =x.EstaEliminado

                    }).ToList();
            }
        }

        public IEnumerable<BajaArticuloDto> ObtenerBajaArticuloExistente(string cadenaBuscar)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                return context.BajaArticulos
                    .AsNoTracking()
                    .Where(x => x.Observacion.Contains(cadenaBuscar) && x.EstaEliminado == false)
                    .Select(x => new BajaArticuloDto
                    {

                        Id = x.Id,
                        Fecha = x.Fecha,
                        Cantidad = x.Cantidad,
                        Observacion = x.Observacion,
                        MotivoBajaId = x.MotivoBajaId,
                        ArticuloId = x.ArticuloId,
                        EstaEliminado = x.EstaEliminado
                    }).ToList();
            }
        }

        public BajaArticuloDto ObtenerPorId(long entidadId)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                return context.BajaArticulos
                    .AsNoTracking()
                    .Select(x => new BajaArticuloDto
                    {
                        Id = x.Id,
                        Observacion = x.Observacion
                    }).FirstOrDefault(x => x.Id == entidadId);
            }
        }
    }
}
