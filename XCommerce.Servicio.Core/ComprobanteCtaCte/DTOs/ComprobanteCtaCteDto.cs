using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCommerce.AccesoDatos;
using XCommerce.Servicio.Core.Base;

namespace XCommerce.Servicio.Core.ComprobanteCtaCte.DTOs
{
    public class ComprobanteCtaCteDto : BaseDto
    {
        public int Numero { get; set; }

        public string NumeroStr => "005" + " - " + Numero.ToString("00000000");

        public DateTime Fecha { get; set; }

        public decimal SubTotal { get; set; }

        public decimal Descuento { get; set; }

        public decimal Total { get; set; }

        public long UsuarioId { get; set; }

        public long Clienteid { get; set; }

        public TipoComprobante TipoComprobante { get; set; }
    }
}
