using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCommerce.AccesoDatos;
using XCommerce.Servicio.Core.FormaDePago.DTOs;

namespace XCommerce.Servicio.Core.FormaDePago
{
    public class FormaDePagoServicio : IFormaDePago
    {
        public long Insertar(FormaDePagoEfectivoDto dto)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var entidadNueva = new FormaPagoEfectivo()
                {
                    ComprobanteId = dto.ComprobanteId,
                    Monto = dto.Monto,
                    TipoFormaPago = dto.TipoFormaDePago
                };

                context.FormasPagos.Add(entidadNueva);

                context.SaveChanges();

                return entidadNueva.Id;

            }
        }

        public IEnumerable<FormaDePagoGeneralDto> Obtener(string cadenaBuscar)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                return context.FormasPagos
                    .AsNoTracking()
                   .Where(x => x.TipoFormaPago.ToString().Contains(cadenaBuscar))
                    .Select(x => new FormaDePagoGeneralDto
                    {
   
                        ComprobanteId = x.ComprobanteId,
                        TipoFormaDePago = x.TipoFormaPago,
                        Monto = x.Monto,
                        
                      
                    }).ToList();
            }
        }

        public FormaDePagoGeneralDto ObtenerPorId(long entidadId)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                return context.FormasPagos
                    .AsNoTracking()
                    .Select(x => new FormaDePagoGeneralDto
                    {
                        Id = x.Id,
                        ComprobanteId = x.ComprobanteId,
                        Monto = x.Monto,
                        TipoFormaDePago = x.TipoFormaPago

                    }).FirstOrDefault(x => x.Id == entidadId);
            }
        }
    }
}
