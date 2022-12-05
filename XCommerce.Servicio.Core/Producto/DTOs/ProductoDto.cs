namespace XCommerce.Servicio.Core.Producto.DTOs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using XCommerce.Servicio.Core.Base;

    public class ProductoDto : BaseDto
    {
        public string Codigo { get; set; }

        public string CodigoBarra { get; set; }

        public string Descripcion { get; set; }

        public decimal Precio { get; set; }

        public bool DescuentaStock { get; set; }

        public bool EstaDiscontinuado { get; set; }

        public DateTime HoraVentaMax { get; set; }

        public decimal Stock { get; set; }

        public decimal LimiteVentaCantidad { get; set; }

        public bool PermiteStockNegativo { get; set; }

    }
}
