using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCommerce.Servicio.Core.Base;

namespace XCommerce.Servicio.Core.Proveedor.DTOs
{
    public class ProveedorDto : BaseDto
    {
        public string Descripcion { get; set; }

        public string RazonSocial { get; set; }

        public string Telefono { get; set; }

        public string Email { get; set; }

        public string Contacto { get; set; }

        public long CondicionIvaId { get; set; }
    }
}
