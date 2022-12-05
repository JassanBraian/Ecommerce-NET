using System;
using XCommerce.Servicio.Core.Base;

namespace XCommerce.Servicio.Core.Empleado.DTOs
{
    public class EmpleadoDto : BaseDto
    {
        public int Legajo { get; set; }

        public string Apellido { get; set; }

        public string Nombre { get; set; }

        public string ApyNom => $"{Apellido} {Nombre}";

        public string Dni { get; set; }

        public string Telefono { get; set; }

        public string Celular { get; set; }

        public string Email { get; set; }

        public string Cuil { get; set; }

        public DateTime FechaNacimiento { get; set; }

        public DateTime FechaIngreso { get; set; }

        public byte[] Foto { get; set; }


        public long TipoEmpleadoId { get; set; }

        public string TipoEmpleadoDes { get; set; }

        // =========================================== //
        // ========      Datos del Domicilio    ====== //
        // =========================================== //

        public string Calle { get; set; }

        public int Numero { get; set; }

        public string Piso { get; set; }

        public string Dpto { get; set; }

        public string Casa { get; set; }

        public string Lote { get; set; }

        public string Mza { get; set; }

        public string Barrio { get; set; }

        public long LocalidadId { get; set; }

        public long ProvinciaId { get; set; }

        // =========================================== //
    }
}
