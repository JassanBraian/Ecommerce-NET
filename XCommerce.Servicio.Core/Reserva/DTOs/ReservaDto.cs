using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCommerce.AccesoDatos;
using XCommerce.Servicio.Core.Base;

namespace XCommerce.Servicio.Core.Reserva.DTOs
{
    public class ReservaDto : BaseDto
    {
        public string Mesa { get; set; }

        public decimal Senia { get; set; }

        public EstadoReserva EstadoReserva { get; set; }

        public long MesaId { get; set; }

        public long ClienteId { get; set; }

        public string ClienteDni { get; set; }

        public string ClienteApellido { get; set; }

        public long UsuarioId { get; set; }

        public long MotivoReservaId { get; set; }

        public DateTime Fecha { get; set; }

    }
}
