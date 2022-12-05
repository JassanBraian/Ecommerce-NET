using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCommerce.Servicio.Core.MotivoBaja.DTOs;

namespace XCommerce.Servicio.Core.MotivoBaja
{
    public interface IMotivoBajaServicio
    {
        long Insertar(MotivoBajaDto dto);

        void Modificar(MotivoBajaDto dto);

        void Eliminar(long BancoId);

        // ===================================================== //

        IEnumerable<MotivoBajaDto> Obtener(string cadenaBuscar);

        MotivoBajaDto ObtenerPorId(long entidadId);

    }
}
