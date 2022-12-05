namespace XCommerce.Servicio.Core.Mesa.DTOs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using XCommerce.AccesoDatos;
    using XCommerce.Servicio.Core.Base;

    public class MesaDto : BaseDto
    {
        public int Numero { get; set; }

        public string Descripcion { get; set; }

        public long SalonId { get; set; }

        public string Salon { get; set; }

        public EstadoMesa EstadoMesa { get; set; }
    }
}
