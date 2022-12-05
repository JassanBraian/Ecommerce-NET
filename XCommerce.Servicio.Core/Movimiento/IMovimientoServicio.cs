using Servicios.Core.Movimiento.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCommerce.AccesoDatos;

namespace Servicios.Core.Movimiento
{
    public interface IMovimientoServicio
    {
        long Insertar(MovimientoDto movimientoDto);

        IEnumerable<MovimientoDto> Obtener(string cadenaBuscar);

        IEnumerable<MovimientoDto> ObtenerSegunCajaId(long cajaId);

        IEnumerable<MovimientoDto> ObtenerCtaCte(string cadenaBuscar);
    }
}
