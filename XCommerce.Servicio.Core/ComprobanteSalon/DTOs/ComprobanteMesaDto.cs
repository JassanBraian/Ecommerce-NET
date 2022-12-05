namespace XCommerce.Servicio.Core.Comprobante.DTOs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using XCommerce.AccesoDatos;
    using XCommerce.Servicio.Core.Base;
    using static XCommerce.Servicio.Core.Comprobante.Descuento;

    public class ComprobanteMesaDto :BaseDto
    {
        public ComprobanteMesaDto()
        {
            if (Items == null)
                Items = new List<DetalleComprobanteSalonDto>();
        }

        public long MesaId { get; set; }

        public DateTime Fecha { get; set; }

        public int Numero { get; set; }

        public string NumeroStr => "001" + " - " + Numero.ToString("00000000");

        public long? MozoId { get; set; }

        public string ApellidoMozo { get; set; }

        public string NombreMozo { get; set; }

        public string ApyNomMozo => $"{ApellidoMozo} {NombreMozo}";

        public int Legajo { get; set; }

        public long ClienteId { get; set; }

        public string ClienteStr { get; set; }

        public long UsuarioId { get; set; }

        public decimal SubTotal => Items.Sum(x => x.SubTotalLinea);

        public decimal Descuento { get; set; }

        public decimal Total => SubTotal - Calcular(Descuento, SubTotal);

        public EstadoComprobanteSalon Estado { get; set; }

        public List<DetalleComprobanteSalonDto> Items { get; set; }

    }
}
