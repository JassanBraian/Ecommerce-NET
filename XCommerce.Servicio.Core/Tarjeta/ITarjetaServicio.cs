using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCommerce.Servicio.Core.Tarjeta.DTOs;

namespace XCommerce.Servicio.Core.Tarjeta
{
    public interface ITarjetaServicio
    {
        long Insertar(TarjetaDto dto);

        void Modificar(TarjetaDto dto);

        void Eliminar(long tarjetaId);

        // ===================================================== //

        IEnumerable<TarjetaDto> Obtener(string cadenaBuscar);

        TarjetaDto ObtenerPorId(long entidadId);

        TarjetaDto ObtenerPorDescripcion(string descripcion);

        bool ExisteTarjeta(string descripcion);
    }
}
