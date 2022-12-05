using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCommerce.Servicio.Core.Salon.DTOs;

namespace XCommerce.Servicio.Core.Salon
{
    public interface ISalonServicio
    {
        long Insertar(SalonDto dto);

        void Modificar(SalonDto dto);

        void Eliminar(long salonIdId);

        // ===================================================== //

        IEnumerable<SalonDto> Obtener(string cadenaBuscar);

        SalonDto ObtenerPorId(long entidadId);

        IEnumerable<SalonDto> ObtenerSalonesExistente(string cadenaBuscar);

    }
}
