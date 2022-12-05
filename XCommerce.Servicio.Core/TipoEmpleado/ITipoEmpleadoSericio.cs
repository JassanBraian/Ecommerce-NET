using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCommerce.Servicio.Core.TipoEmpleado.DTOs;

namespace XCommerce.Servicio.Core.TipoEmpleado
{
    public interface ITipoEmpleadoSericio
    {
        long Insertar(TipoEmpleadoDto tipoEmpleadoDto);

        void Modificar(TipoEmpleadoDto tipoEmpleadoDto);

        void Eliminar(long TipoEmpleadoId);

        // ===================================================== //

        IEnumerable<TipoEmpleadoDto> Obtener(string cadenaBuscar);

        TipoEmpleadoDto ObtenerPorId(long entidadId);

        TipoEmpleadoDto ObtenerPorDescripcion(string descripcion);

        bool ExisteTipoEmpleadoDescripcion(string descripcion);
    }
}
