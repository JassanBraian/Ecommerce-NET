using System.Collections.Generic;
using XCommerce.Servicio.Core.Provincia.DTOs;

namespace XCommerce.Servicio.Core.Provincia
{
    public interface IProvinciaServicio
    {
        long Insertar(ProvinciaDto dto);

        void Modificar(ProvinciaDto dto);

        void Eliminar(long empleadoId);

        // ===================================================== //

        IEnumerable<ProvinciaDto> Obtener(string cadenaBuscar);

        ProvinciaDto ObtenerPorId(long entidadId);

        ProvinciaDto ObtenerPorDescripcion(string descripcion);
    }
}
