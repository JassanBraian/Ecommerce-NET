using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCommerce.AccesoDatos;
using XCommerce.Servicio.Core.Base;

namespace XCommerce.Servicio.Core.FormaDePago.DTOs
{
    public class FormaDePagoChequeDto : BaseDto
    {
        public long ComprobanteId { get; set; }

        public TipoFormaPago TipoFormaDePago { get; set; }

        public decimal Monto { get; set; }

        public long BancoId { get; set; }

        public string EnteEmisor { get; set; }

        public string Numero { get; set; }

        public long Dias { get; set; }

        public DateTime FechaEmision { get; set; }


    }
}
