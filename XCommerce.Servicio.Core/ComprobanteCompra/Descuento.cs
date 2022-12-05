using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCommerce.Servicio.Core.ComprobanteCompra
{
    public static class Descuento
    {
        public static decimal Calcular(decimal porcentaje, decimal valor)
        {
            return (valor * (porcentaje / 100m));
        }
    }


}
