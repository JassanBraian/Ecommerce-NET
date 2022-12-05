using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCommerce.AccesoDatos;
using XCommerce.Servicio.Core.Base;
using static XCommerce.Servicio.Core.Delivery.DescuentoDelivery;

namespace XCommerce.Servicio.Core.Delivery.DTOs
{
     public class DeliveryDto : BaseDto
     {

        public DeliveryDto()
        {
            if (Items == null)
                Items = new List<DetalleDeliveryDto>();
        }

        public int Numero { get; set; }

        public string NumeroStr => "003" + " - " + Numero.ToString("00000000");

        public DateTime Fecha { get; set; }

        public decimal SubTotal => Items.Sum(x => x.SubTotalLinea);

        public decimal Descuento { get; set; }

        public decimal Total => SubTotal - Calcular(Descuento, SubTotal);

        public long UsuarioId { get; set; }

        public long Clienteid { get; set; }

        public string ClienteStr { get; set; }

        public TipoComprobante TipoComprobante { get; set; }

        public List<DetalleDeliveryDto> Items { get; set; }

        public EstadoComprobanteSalon EstadoDelivery { get; set; }

        public long CadeteId { get; set; }

        public string CadeteStr { get; set; }
    }
}
