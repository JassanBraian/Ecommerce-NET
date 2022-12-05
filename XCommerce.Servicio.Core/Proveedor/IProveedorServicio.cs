using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCommerce.Servicio.Core.Proveedor.DTOs;

namespace XCommerce.Servicio.Core.Proveedor
{
    public interface IProveedorServicio
    {
        long Insertar(ProveedorDto dto);

        void Modificar(ProveedorDto dto);

        void Eliminar(long BancoId);

        // ===================================================== //

        IEnumerable<ProveedorDto> Obtener(string cadenaBuscar);

        ProveedorDto ObtenerPorId(long entidadId);
    }
}
