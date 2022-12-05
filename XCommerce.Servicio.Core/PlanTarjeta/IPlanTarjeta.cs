using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCommerce.Servicio.Core.PlanTarjeta.DTOs;

namespace XCommerce.Servicio.Core.PlanTarjeta
{
    public interface IPlanTarjeta
    {
        long Insertar(PlanTarjetaDto dto);

        void Modificar(PlanTarjetaDto dto);

        void Eliminar(long planTarjetaId);

        // ===================================================== //

        IEnumerable<PlanTarjetaDto> Obtener(string cadenaBuscar);

        PlanTarjetaDto ObtenerPorId(long entidadId);

        PlanTarjetaDto ObtenerPorDescripcion(string descripcion);

        IEnumerable<PlanTarjetaDto> ObtenerSegunTarjetaId(long tarjetaId);
    }
}
