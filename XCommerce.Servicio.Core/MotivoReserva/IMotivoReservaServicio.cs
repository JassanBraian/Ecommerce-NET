﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCommerce.Servicio.Core.MotivoReserva.DTOs;

namespace XCommerce.Servicio.Core.MotivoReserva
{
    public interface IMotivoReservaServicio
    {

        long Insertar(MotivoReservaDto dto);

        void Modificar(MotivoReservaDto dto);

        void Eliminar(long motivoreservaId);

        //==============================================================//

        IEnumerable<MotivoReservaDto> Obtener(string cadenaBuscar);

        MotivoReservaDto ObtenerPorId(long entidadId);
    }
}
