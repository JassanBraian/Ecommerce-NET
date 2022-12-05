using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCommerce.AccesoDatos;
using XCommerce.Servicio.Core.ComprobanteCtaCte.DTOs;

namespace XCommerce.Servicio.Core.ComprobanteCtaCte
{
    public class ComprobanteCtaCteServicio : IComprobanteCtaCteServicio
    {
        public int GenerarNumeroFactura()
        {
            using (var context = new ModeloXCommerceContainer())
            {
                if (context.Comprobantes.Any())
                {
                    return context.Comprobantes.Max(x => x.Numero) + 1;
                }
                else
                {
                    return 1;
                }
            }
        }

        public long Insertar(ComprobanteCtaCteDto dto)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var comprobante = new AccesoDatos.ComprobanteCtaCte()
                {
                    Numero = GenerarNumeroFactura(),
                    Fecha = dto.Fecha,
                    SubTotal = dto.SubTotal,
                    Descuento = dto.Descuento,
                    Total = dto.Total,
                    UsuarioId = dto.UsuarioId,
                    ClienteId = dto.Clienteid,
                    TipoComprobante = dto.TipoComprobante
                };

                context.Comprobantes.Add(comprobante);

                context.SaveChanges();

                return comprobante.Id;
            }
        }

        public IEnumerable<ComprobanteCtaCteDto> ObtenerComprobanteCtaCte(string cadenaBuscar)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                return context.Comprobantes.OfType<AccesoDatos.ComprobanteCtaCte>()
                    //.Where(x => x.Descripcion.Contains(cadenaBuscar))
                    .Select(x => new ComprobanteCtaCteDto
                    {

                        Id = x.Id,
                        UsuarioId = x.UsuarioId,
                        Descuento = x.Descuento,
                        Fecha = x.Fecha,
                        Numero = x.Numero,
                        Total = x.Total,
                        TipoComprobante = x.TipoComprobante,

                    }).ToList();
            }
        }
    }
}
