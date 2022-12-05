using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCommerce.AccesoDatos;
using XCommerce.Servicio.Core.Banco.DTOs;

namespace XCommerce.Servicio.Core.Banco
{
    public class BancoServicio : IBancoServicio
    {
        public void Eliminar(long BancoId)
        {

            using (var context = new ModeloXCommerceContainer())
            {
                var BancoEliminar = context.Bancos
                    .FirstOrDefault(x => x.Id == BancoId);

                if (BancoEliminar == null)
                    throw new Exception("Ocurrio un error al Obtener el banco");

                BancoEliminar.EstaEliminado = true;

                context.SaveChanges();
            }

        }

        public long Insertar(BancoDto Bancodto)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var BancoNuevo = new AccesoDatos.Banco
                {
                    Descripcion = Bancodto.Descripcion
                };

                context.Bancos.Add(BancoNuevo);

                context.SaveChanges();

                return BancoNuevo.Id;
            }
        }

        public void Modificar(BancoDto Bancodto)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var BancoModificar = context.Bancos
                    .FirstOrDefault(x => x.Id == Bancodto.Id);

                if (BancoModificar == null)
                    throw new Exception("Ocurrio un error al Obtener el Banco");

                BancoModificar.Descripcion = Bancodto.Descripcion;

                context.SaveChanges();
            }

        }

        public IEnumerable<BancoDto> Obtener(string cadenaBuscar)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                return context.Bancos
                    .AsNoTracking()
                    .Where(x => x.Descripcion.Contains(cadenaBuscar))
                    .Select(x => new BancoDto
                    {
                        Id = x.Id,
                        Descripcion = x.Descripcion,
                        EstaEliminado = x.EstaEliminado
                    }).ToList();
            }
        }

        public BancoDto ObtenerPorId(long entidadId)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                return context.Bancos
                    .AsNoTracking()
                    .Select(x => new BancoDto
                    {
                        Id = x.Id,
                        Descripcion = x.Descripcion,
                        EstaEliminado = x.EstaEliminado
                    }).FirstOrDefault(x => x.Id == entidadId);
            }
        }
    }
}
