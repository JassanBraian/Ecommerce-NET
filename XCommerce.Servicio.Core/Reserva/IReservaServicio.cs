using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCommerce.Servicio.Core.Reserva.DTOs;

namespace XCommerce.Servicio.Core.Reserva
{
    public interface IReservaServicio
    {
        long Insertar(ReservaDto dto);

        void Modificar(ReservaDto dto);

        void Eliminar(long reservaId);

        //---------------------------------------------------------//
        IEnumerable<ReservaDto> Obtener(string cadenaBuscar);

        IEnumerable<ReservaDto> ObtenerDeCajaActual(string cadenaBuscar);

        IEnumerable<ReservaDto> ObtenerActuales(string cadenaBuscar);

        ReservaDto ObtenerPorId(long entidadId);

        bool CancelarReserva(long _mesaId);

        ReservaDto ObtenerPorMesaId(long entidadId);

    }
}
