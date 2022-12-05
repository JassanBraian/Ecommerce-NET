using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCommerce.Servicio.Core.CondicionIva.DTOs;

namespace XCommerce.Servicio.Core.CondicionIva
{
    public interface ICondicionIvaServicio
    {
        long Insertar(CondicionIvaDto dto);

        void Modificar(CondicionIvaDto dto);

        void Eliminar(long entidadId);

        // ===================================================== //

        IEnumerable<CondicionIvaDto> Obtener(string cadenaBuscar);

        CondicionIvaDto ObtenerPorId(long entidadId);

        CondicionIvaDto ObtenerPorDescripcion(string descripcion);
    }
}
