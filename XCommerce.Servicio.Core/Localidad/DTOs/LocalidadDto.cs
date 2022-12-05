using XCommerce.Servicio.Core.Base;

namespace XCommerce.Servicio.Core.Localidad.DTOs
{
    public class LocalidadDto : BaseDto
    {
        public long ProvinciaId { get; set; }

        public string Provincia { get; set; }

        public string Descripcion { get; set; }
    }
}
