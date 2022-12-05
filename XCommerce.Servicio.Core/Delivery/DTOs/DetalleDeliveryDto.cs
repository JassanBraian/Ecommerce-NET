using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCommerce.Servicio.Core.Base;

namespace XCommerce.Servicio.Core.Delivery.DTOs
{
   public class DetalleDeliveryDto : BaseDto
    {

        public string Codigo { get; set; }

        public long ComprobanteId { get; set; }

        public string Descripcion { get; set; }

        public decimal PrecioUnitario { get; set; }

        public decimal Cantidad { get; set; }

        public decimal SubTotalLinea => PrecioUnitario * Cantidad;

        public long ArticuloId { get; set; }
    }
}
