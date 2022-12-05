using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCommerce.Servicio.Core.Delivery
{
 public   class DescuentoDelivery
    {

        public static decimal Calcular(decimal porcentaje, decimal valor)
        {
            return (valor * (porcentaje / 100m));
        }
    }
}
