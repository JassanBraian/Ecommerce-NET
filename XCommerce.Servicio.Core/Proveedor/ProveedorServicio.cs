using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCommerce.AccesoDatos;
using XCommerce.Servicio.Core.Proveedor.DTOs;

namespace XCommerce.Servicio.Core.Proveedor
{
    public class ProveedorServicio : IProveedorServicio
    {
        public void Eliminar(long proveedorId)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var proveedorEliminar = context.Proveedores.FirstOrDefault(x => x.Id == proveedorId);

                if (proveedorEliminar == null)
                    throw new Exception("No se encontro el proveedor");

                proveedorEliminar.EstaEliminado = true;


                context.SaveChanges();
            }
        }

        public long Insertar(ProveedorDto dto)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var nuevoProveedor = new AccesoDatos.Proveedor
                {
                    Descripcion=dto.Descripcion,
                    RazonSocial = dto.RazonSocial,
                    Email = dto.Email,
                    Contacto = dto.Contacto,
                    Telefono = dto.Telefono,
                    CondicionIvaId = dto.CondicionIvaId,
                    



                };

                context.Proveedores.Add(nuevoProveedor);

                context.SaveChanges();

                return nuevoProveedor.Id;
            }
        }

        public void Modificar(ProveedorDto dto)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var proveedorModificar = context.Proveedores
                    .FirstOrDefault(x => x.Id == dto.Id);

                if (proveedorModificar == null)
                    throw new Exception("No se encontro el proveedor");

                proveedorModificar.Descripcion = dto.Descripcion;
                proveedorModificar.RazonSocial = dto.RazonSocial;
                proveedorModificar.Email = dto.Email;
                proveedorModificar.Contacto = dto.Contacto;
                proveedorModificar.Telefono = dto.Telefono;
                proveedorModificar.CondicionIvaId = dto.CondicionIvaId;

                context.SaveChanges();
            }
        }

        public IEnumerable<ProveedorDto> Obtener(string cadenaBuscar)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                return context.Proveedores.
                    Where(x => x.RazonSocial.Contains(cadenaBuscar))
                    .Select(x => new ProveedorDto
                    {
                        Id = x.Id,
                        Descripcion=x.Descripcion,
                        RazonSocial = x.RazonSocial,
                        Email = x.Email,
                        Telefono = x.Telefono,
                        CondicionIvaId = x.CondicionIvaId,
                        Contacto = x.Contacto,
                        EstaEliminado = x.EstaEliminado

                    }).ToList();
            }
        }

        public ProveedorDto ObtenerPorId(long entidadId)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                return context.Proveedores
                    .AsNoTracking()
                    .Select(x => new ProveedorDto
                    {
                        Id = x.Id,
                        Descripcion = x.Descripcion,
                        RazonSocial = x.RazonSocial,
                        EstaEliminado = x.EstaEliminado,
                        CondicionIvaId = x.CondicionIvaId,
                        Telefono = x.Telefono,
                        Email = x.Email,
                        Contacto = x.Contacto


                    }).FirstOrDefault(x => x.Id == entidadId);
            }
        }
    }
}
