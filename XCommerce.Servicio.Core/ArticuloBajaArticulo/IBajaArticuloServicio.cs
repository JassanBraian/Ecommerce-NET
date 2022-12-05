using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCommerce.Servicio.Core.BajaArticulo.DTOs;

namespace XCommerce.Servicio.Core.BajaArticulo
{
    public interface IBajaArticuloServicio
    {
        long Insertar(BajaArticuloDto dto);

        void Modificar(BajaArticuloDto dto);

        void Eliminar(long entidadId);

        // ===================================================== //

        IEnumerable<BajaArticuloDto> Obtener(string cadenaBuscar);

        BajaArticuloDto ObtenerPorId(long entidadId);


        IEnumerable<BajaArticuloDto> ObtenerBajaArticuloExistente(string cadenaBuscar);
    }
}
