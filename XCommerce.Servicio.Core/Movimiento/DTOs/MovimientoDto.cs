using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCommerce.AccesoDatos;
using XCommerce.Servicio.Core.Base;

namespace Servicios.Core.Movimiento.DTOs
{
    public class MovimientoDto : BaseDto
    {
        public long CajaId { get; set; }

        public long ComprobanteId { get; set; }

        public TipoMovimiento TipoMovimiento { get; set; }

        public long UsuarioId { get; set; }

        public decimal Monto { get; set; }

        public DateTime Fecha { get; set; }

        public string Descripcion { get; set; }

        public TipoFormaPago TipoFormaPago { get; set; }
    }
}
