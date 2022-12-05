using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCommerce.Servicio.Core.ListaPrecio.DTOs;

namespace XCommerce.Servicio.Core.ListaPrecio
{
    public interface IListaPrecioServicio

    {
        long Insertar(ListaPrecioDto dto);

        void Modificar(ListaPrecioDto dto);

        void Eliminar(long listaPrecioId);

        // ===================================================== //

        IEnumerable<ListaPrecioDto> Obtener(string cadenaBuscar);

        ListaPrecioDto ObtenerPorId(long entidadId);

        ListaPrecioDto ObtenerPorDescripcion(string descripcion);

        bool ExisteListaDesripcion(string descripcion);
    }
}
