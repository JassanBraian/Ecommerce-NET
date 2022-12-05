using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCommerce.Servicio.Core.Base;

namespace XCommerce.Servicio.Core.BajaArticulo.DTOs
{
    public class BajaArticuloDto : BaseDto
    {

        public DateTime Fecha { get; set; }

        public long Cantidad { get; set; }

        public string Observacion { get; set; }

        public long MotivoBajaId { get; set; }

        public long ArticuloId { get; set; }
    }
}
