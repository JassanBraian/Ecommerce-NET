using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCommerce.Servicio.Core.ComprobanteCtaCte.DTOs;

namespace XCommerce.Servicio.Core.ComprobanteCtaCte
{
    public interface IComprobanteCtaCteServicio
    {
        long Insertar(ComprobanteCtaCteDto dto);

        int GenerarNumeroFactura();

        IEnumerable<ComprobanteCtaCteDto> ObtenerComprobanteCtaCte(string cadenaBuscar);
    }
}
