using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCommerce.AccesoDatos;
using XCommerce.Servicio.Core.Base;
using static XCommerce.Servicio.Core.Kiosco.Descuento;


namespace XCommerce.Servicio.Core.Kiosco.DTOs
{
    public class kioscoDto : BaseDto
    {
        public kioscoDto()
        {
            if (Items == null)
                Items = new List<DetalleKioscoDto>();
        }

        public int Numero { get; set; }

        public string NumeroStr => "002" + " - " + Numero.ToString("00000000");

        public DateTime Fecha { get; set; }

        public decimal SubTotal => Items.Sum(x => x.SubTotalLinea);

        public decimal Descuento { get; set; }

        public decimal Total => SubTotal - Calcular(Descuento, SubTotal);

        public long UsuarioId { get; set; }

        public long Clienteid { get; set; }

        public string ClienteStr { get; set; }

        public TipoComprobante TipoComprobante { get; set; }

        public List<DetalleKioscoDto> Items { get; set; }
    }
}
