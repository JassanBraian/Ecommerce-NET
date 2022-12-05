using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCommerce.Servicio.Core.Base;

namespace XCommerce.Servicio.Core.Precio.DTOs
{
    public class PrecioDto : BaseDto
    {

        public long ArticuloId { get; set; }

        public string Articulo { get; set; }

        public decimal PrecioCosto { get; set; }

        public decimal PrecioPublico { get; set; }

        public DateTime FechaActualizacion { get; set; }

        public long ListaPrecioId { get; set; }

        public string ListaPrecio { get; set; }



        public bool ActivarHoraVenta { get; set; }

        public DateTime HoraVenta { get; set; }

    }
}
