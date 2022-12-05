using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCommerce.Servicio.Core.Rubro.DTOs;

namespace XCommerce.Servicio.Core.Rubro
{
    public interface IRubroServicio
    {
        long Insertar(RubroDto dto);

        void Modificar(RubroDto dto);

        void Eliminar(long BancoId);

        // ===================================================== //

        IEnumerable<RubroDto> Obtener(string cadenaBuscar);

        RubroDto ObtenerPorId(long entidadId);

        RubroDto ObtenerPorDescripcion(string descripcion);

    }
}
