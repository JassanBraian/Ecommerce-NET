using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCommerce.Servicio.Seguridad.Usuario.DTOs
{
    public class UsuarioDto
    {
        public long Id { get; set; }

        public long PersonaId { get; set; }

        public string ApellidoPersona { get; set; }

        public string NombrePersona { get; set; }

        public string ApyNom => $"{ApellidoPersona} {NombrePersona}";

        public string Nombre { get; set; }

        public bool EstaBloqueado { get; set; }

        public string EstaBloqueadoStr => EstaBloqueado ? "SI" : "NO";
    }
}
