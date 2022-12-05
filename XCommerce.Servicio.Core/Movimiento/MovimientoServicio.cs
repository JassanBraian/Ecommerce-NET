using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicios.Core.Movimiento.DTOs;
using XCommerce.AccesoDatos;

namespace Servicios.Core.Movimiento
{
    public class MovimientoServicio : IMovimientoServicio
    {
        public long Insertar(MovimientoDto dto)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var nuevoMovimiento = new XCommerce.AccesoDatos.Movimiento()
                {
                    Fecha = dto.Fecha,
                    CajaId = dto.CajaId,
                    ComprobanteId = dto.ComprobanteId,
                    Descripcion = dto.Descripcion,
                    Monto = dto.Monto,
                    TipoMovimento = dto.TipoMovimiento,
                    UsuarioId = dto.UsuarioId,
                };

                context.Movimientos.Add(nuevoMovimiento);

                context.SaveChanges();

                return nuevoMovimiento.Id;
            }
        }

        public IEnumerable<MovimientoDto> Obtener(string cadenaBuscar)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                return context.Movimientos
                    .AsNoTracking()
                    .Where(x => x.Descripcion.Contains(cadenaBuscar))
                    .Select(x => new MovimientoDto
                    {
                        Id = x.Id,
                        CajaId = x.CajaId,
                        ComprobanteId = x.ComprobanteId,
                        TipoMovimiento = x.TipoMovimento,
                        UsuarioId = x.UsuarioId,
                        Monto = x.Monto,
                        Fecha = x.Fecha,
                        Descripcion = x.Descripcion,
                        //EstaEliminado = x.EstaEliminado,
                        TipoFormaPago = context.FormasPagos.FirstOrDefault(f => f.ComprobanteId == x.ComprobanteId).TipoFormaPago,

                    }).ToList();
            }
        }

        public IEnumerable<MovimientoDto> ObtenerCtaCte(string cadenaBuscar)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                return context.Movimientos
                    .AsNoTracking()
                    .Where(x => x.Descripcion.Contains(cadenaBuscar) && (x.Descripcion == "Pago de Saldo CtaCte" ||
                        x.Descripcion == "Pago en Bar con CtaCte" || x.Descripcion == "Pago en Kiosco con CtaCte" || x.Descripcion == "Pago en Delivery con CtaCte"))
                    .Select(x => new MovimientoDto
                    {
                        Id = x.Id,
                        CajaId = x.CajaId,
                        ComprobanteId = x.ComprobanteId,
                        TipoMovimiento = x.TipoMovimento,
                        UsuarioId = x.UsuarioId,
                        Monto = x.Monto,
                        Fecha = x.Fecha,
                        Descripcion = x.Descripcion,
                        //EstaEliminado = x.EstaEliminado,
                        TipoFormaPago = context.FormasPagos.FirstOrDefault(f => f.ComprobanteId == x.ComprobanteId).TipoFormaPago,

                    }).ToList();
            }
        }

        public IEnumerable<MovimientoDto> ObtenerSegunCajaId(long cajaId)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                return context.Movimientos
                    .AsNoTracking()
                    .Where(x => x.CajaId == cajaId)
                    .Select(x => new MovimientoDto
                    {
                        Id = x.Id,
                        CajaId = x.CajaId,
                        ComprobanteId = x.ComprobanteId,
                        TipoMovimiento = x.TipoMovimento,
                        UsuarioId = x.UsuarioId,
                        Monto = x.Monto,
                        Fecha = x.Fecha,
                        Descripcion = x.Descripcion,
                        //EstaEliminado = x.EstaEliminado
                        TipoFormaPago = context.FormasPagos.FirstOrDefault(f => f.ComprobanteId == x.ComprobanteId).TipoFormaPago,
                    }).ToList();
            }
        }
    }
}
