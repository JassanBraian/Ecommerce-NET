using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCommerce.Servicio.Core.Base;

namespace XCommerce.Servicio.Core.MotivoReserva.DTOs
{
    public class MotivoReservaDto : BaseDto
    {

      //  public long MotivoReservaId { get; set; }

        public string MotivoReserva { get; set; }

        public string Descripcion { get; set; }
    }
}
