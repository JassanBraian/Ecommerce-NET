namespace XCommerce.Servicio.Core.Articulo.DTOs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using XCommerce.Servicio.Core.Base;

    public class ArticuloDto : BaseDto
    {
        public string Descripcion { get; set; }

        public decimal PrecioCosto { get; set; }

        public string Codigo { get; set; }

        public string CodigoBarra { get; set; }

        public string Abreviatura { get; set; }
        
        public string Detalle { get; set; }

        public byte[] Foto { get; set; }

        public bool ActivarLimiteVenta { get; set; }

        public decimal LimiteVenta { get; set; }

        public bool PermiteStockNegativo { get; set; }

        public bool EstaDiscontinuado { get; set; }

        public decimal StockMaximo { get; set; }

        public decimal StockMinimo { get; set; }

        public bool DescuentaStock { get; set; }


        public long MarcaId { get; set; }

        public long RubroId { get; set; }

        public decimal Stock { get; set; }

        public string Iva { get; set; }

        

        //// ========== Datos de Baja de Articulo ============= // 

        //public DateTime Fecha { get; set; }
        //
        //public decimal Cantidad { get; set; }
        //
        //public string Observacion { get; set; }
        //
        //public long MotivoBajaId { get; set; }
        //
        //public long ArticuloId { get; set; }
    }
}
