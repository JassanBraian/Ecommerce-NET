using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCommerce.AccesoDatos;
using XCommerce.Servicio.Core.Base;

namespace XCommerce.Servicio.Core.FormaDePago.DTOs
{
    public class FormaDePagoTarjetaDto : BaseDto
    {

        public long ComprobanteId { get; set; }

        public TipoFormaPago TipoFormaDePago { get; set; }

        public decimal Monto { get; set; }

        public long PlanTarjetaId { get; set; }

        public string Cupon { get; set; }   

        public string Numero { get; set; }

        public string NumeroTarjeta { get; set; }
    }
}
