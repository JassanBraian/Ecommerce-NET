using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCommerce.Servicio.Core.Base;

namespace XCommerce.Servicio.Core.PlanTarjeta.DTOs
{
    public class PlanTarjetaDto : BaseDto
    {
        public string Descripcion { get; set; }

        public decimal Alicuota { get; set; }

        public long TarjetaId { get; set; }
    }
}
