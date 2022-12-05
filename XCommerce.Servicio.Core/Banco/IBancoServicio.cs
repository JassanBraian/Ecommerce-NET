using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCommerce.Servicio.Core.Banco.DTOs;

namespace XCommerce.Servicio.Core.Banco
{
    public interface IBancoServicio
    {

        long Insertar(BancoDto dto);

        void Modificar(BancoDto dto);

        void Eliminar(long BancoId);

        // ===================================================== //

        IEnumerable<BancoDto> Obtener(string cadenaBuscar);

        BancoDto ObtenerPorId(long entidadId);
    }
}
