namespace XCommerce.Servicio.Core.Salon.DTOs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using XCommerce.Servicio.Core.Base;

    public class SalonDto : BaseDto
    {
        public string Descripcion { get; set; }


        //esto es de lista precio 
        public long ListaPrecioId { get; set; }


        public string ListaPrecioDescripcion { get; set; }

    }
}
