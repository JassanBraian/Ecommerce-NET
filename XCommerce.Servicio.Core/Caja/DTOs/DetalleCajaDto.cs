using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCommerce.Servicio.Core.Caja.DTOs
{
   public class DetalleCajaDto
    {
        public long Id { get; set; }

        public long CajaId { get; set; }

        public decimal Monto { get; set; }

        public int TipoPago { get; set; }
    }
}
