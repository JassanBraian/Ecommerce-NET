using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCommerce.Servicio.Core.FormaDePago.DTOs;

namespace XCommerce.Servicio.Core.FormaDePago
{
    public interface IFormaDePago
    {
        long Insertar(FormaDePagoEfectivoDto dto);

        IEnumerable<FormaDePagoGeneralDto> Obtener(string cadenaBuscar);

        FormaDePagoGeneralDto ObtenerPorId(long entidadId);
    }
}
