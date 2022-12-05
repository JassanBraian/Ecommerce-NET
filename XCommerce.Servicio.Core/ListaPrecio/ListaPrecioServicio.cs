using System;
using System.Collections.Generic;
using System.Linq;
using XCommerce.AccesoDatos;
using XCommerce.Servicio.Core.ListaPrecio.DTOs;

namespace XCommerce.Servicio.Core.ListaPrecio
{
    public class ListaPrecioServicio : IListaPrecioServicio
    {
        public void Eliminar(long listaPrecioId)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var listaEliminar = context.ListaPrecios
                    .FirstOrDefault(x => x.Id == listaPrecioId);

                if (listaEliminar == null)
                    throw new Exception("Ocurrio un error al Obtener la Lista de Precios");

                listaEliminar.EstaEliminado = true;

                context.SaveChanges();
            }
        }

        public bool ExisteListaDesripcion(string descripcion)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var lista = context.ListaPrecios
                    .FirstOrDefault(x => x.Descripcion == descripcion);

                if (lista == null) return false;
                else return true;

            }
        }

        public long Insertar(ListaPrecioDto listaPrecioDto)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var listaNueva = new AccesoDatos.ListaPrecio
                {
                    Descripcion = listaPrecioDto.Descripcion,
                    Rentabilidad = listaPrecioDto.Rentabilidad
                };

                context.ListaPrecios.Add(listaNueva);

                context.SaveChanges();

                return listaNueva.Id;
            }
        }

        public void Modificar(ListaPrecioDto listaPrecioDto)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var listaModificar = context.ListaPrecios
                    .FirstOrDefault(x => x.Id == listaPrecioDto.Id);

                if (listaModificar == null)
                    throw new Exception("Ocurrio un error al Obtener la Lista de Precios");

                listaModificar.Descripcion = listaPrecioDto.Descripcion;
                listaModificar.Rentabilidad = listaPrecioDto.Rentabilidad;

                context.SaveChanges();
            }
        }

        public IEnumerable<ListaPrecioDto> Obtener(string cadenaBuscar)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                return context.ListaPrecios
                    .AsNoTracking()
                    .Where(x => x.Descripcion.Contains(cadenaBuscar) && x.Id != 1 && x.EstaEliminado==false)
                    .Select(x => new ListaPrecioDto
                    {
                        Id = x.Id,
                        Descripcion = x.Descripcion,
                        Rentabilidad = x.Rentabilidad,
                        EstaEliminado = x.EstaEliminado
                    }).ToList();
            }
        }


        public ListaPrecioDto ObtenerPorDescripcion(string descripcion)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                return context.ListaPrecios
                    .AsNoTracking()
                    .Select(x => new ListaPrecioDto
                    {
                        Id = x.Id,
                        Descripcion = x.Descripcion,
                        Rentabilidad = x.Rentabilidad,
                        EstaEliminado = x.EstaEliminado
                    }).FirstOrDefault(x => x.Descripcion == descripcion);
            }
        }

        public ListaPrecioDto ObtenerPorId(long listaPrecioId)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                return context.ListaPrecios
                    .AsNoTracking()
                    .Select(x => new ListaPrecioDto
                    {
                        Id = x.Id,
                        Descripcion = x.Descripcion,
                        Rentabilidad = x.Rentabilidad,
                        EstaEliminado = x.EstaEliminado
                    }).FirstOrDefault(x => x.Id == listaPrecioId);
            }
        }

    }
}
