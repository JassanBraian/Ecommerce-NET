using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCommerce.AccesoDatos;
using XCommerce.Servicio.Core.Base;

namespace XCommerce.Servicio.Core.FormaDePago.DTOs
{
    public class FormaDePagoCtaCteDto : BaseDto
    {

        public long ComprobanteId { get; set; }

        public TipoFormaPago TipoFormaDePago { get; set; }

        public decimal Monto { get; set; }

        public long ClienteId { get; set; }
    }
}
