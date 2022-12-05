using System;
using System.Collections.Generic;
using System.Linq;
using Presentacion.Helpers;
using XCommerce.AccesoDatos;
using XCommerce.Servicio.Core.Caja.DTOs;

namespace XCommerce.Servicio.Core.Caja.DTOs
{
    public class CajaServicio : ICajaServicio
    {
        public void AbrirCaja(decimal montoApertura, DateTime fechaApertura, long usuarioAperturaId)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var usuarioLogeado = context.Usuarios.Find(usuarioAperturaId);

                var nuevaCaja = new AccesoDatos.Caja
                {
                    FechaApertura = fechaApertura,
                    FechaCierre = null,
                    Diferencia = 0m,
                    Estado = EstadoCaja.Abierta,
                    MontoApertura = montoApertura,
                    MontoCierre = 0m,
                    MontoSistema = montoApertura,
                    UsuarioAperturaId = usuarioAperturaId,
                    UsuarioCierreId = usuarioAperturaId,
                };
                context.Cajas.Add(nuevaCaja);
                context.SaveChanges();

            }
            Validacion.CajaAbierta = true;
        }

        public decimal CalcularMontoSistema(DateTime fechaApertura)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var caja = context.Cajas.FirstOrDefault(x => x.Estado == EstadoCaja.Abierta);

                if (!context.Comprobantes.Where(x => x.Fecha >= fechaApertura && x.Fecha <= DateTime.Now).Any())
                {
                    return 0m;
                }
                /*var montoSistema =*/

                return context.Comprobantes.Where(x => x.Fecha >= fechaApertura && x.Fecha <= DateTime.Now).Sum(a => a.Total);

                //return montoSistema;
            }
        }

        public long CerrarCaja(decimal montoCierre, DateTime fechaApertura, decimal monto)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var caja = context.Cajas.FirstOrDefault(x => x.Estado == EstadoCaja.Abierta);

                caja.Estado = EstadoCaja.Cerrada;
                caja.FechaCierre = DateTime.Now;
                caja.MontoSistema = CalcularMontoSistema(fechaApertura) + caja.MontoSistema;
                caja.MontoCierre = montoCierre;
                caja.Diferencia=montoCierre- CalcularMontoSistema(fechaApertura) + caja.MontoSistema;

                context.SaveChanges();

                return caja.Id;

            }
        }

        public void CrearDetalleCaja(long cajaId, decimal monto)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var detalleCaja = new DetalleCaja
                {
                    CajaId = cajaId,
                    Monto = monto,
                    TipoPago = TipoPago.Efectivo
                };

                context.DetalleCajas.Add(detalleCaja);

                context.SaveChanges();
            }
        }

        public void AumentarMontoSistema(long cajaId, decimal montoDescontar)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var cajaModif = context.Cajas
                    .FirstOrDefault(x => x.Id == cajaId);

                if (cajaModif == null)
                    throw new Exception("Ocurrio un error al Obtener la Caja");

                cajaModif.MontoSistema += montoDescontar;

                context.SaveChanges();
            }
        }

        public bool HayMesasAbiertas()
        {
            using (var context = new ModeloXCommerceContainer())
            {
                return context.Mesas.Where(x => x.EstadoMesa == EstadoMesa.Abierta || x.EstadoMesa == EstadoMesa.Reservada).Any();
            };
        }

        /*public CajaDto Montos()
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var caja = context.Cajas.FirstOrDefault(x => x.Estado == EstadoCaja.Abierta);
                return new CajaDto
                {
                    MontoApertura = caja.MontoApertura,
                    MontoSistema = caja.MontoSistema,
                    Diferencia = caja.MontoCierre - caja.MontoSistema,
                    MontoCierre = caja.MontoCierre,
                    FechaApertura = caja.FechaApertura,
                    FechaCierre = caja.FechaCierre
                };
            }
        }*/

        public IEnumerable<CajaDto> Obtener(string fecha)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                return context.Cajas
                    .AsNoTracking()
                    //.Where(x => x.FechaApertura.ToString()==fecha)
                    .Select(x => new CajaDto
                    {
                        Id = x.Id,
                        MontoApertura = x.MontoApertura,
                        MontoCierre = x.MontoCierre,
                        FechaApertura = x.FechaApertura,
                        FechaCierre = x.FechaCierre,
                        MontoSistema = x.MontoSistema,
                        Diferencia = x.Diferencia,
                        UsuarioAperturaId = x.UsuarioAperturaId,
                        UsuarioCierreId = x.UsuarioCierreId,
                        Estado = x.Estado,
                        
                    }).ToList();
            }
        }

        public CajaDto ObtenerCajaAbierta()
        {
            using(var context = new ModeloXCommerceContainer())
            {
                return context.Cajas
                    .AsNoTracking()
                    .Select(x => new CajaDto
                    {
                        Id = x.Id,
                        FechaApertura = x.FechaApertura,
                        FechaCierre = x.FechaCierre,
                        MontoApertura = x.MontoApertura,
                        MontoCierre = x.MontoCierre,
                        MontoSistema = x.MontoSistema,
                        UsuarioAperturaId = x.UsuarioAperturaId,
                        Diferencia = x.Diferencia,
                        Estado = x.Estado
                        
                    }).FirstOrDefault(x => x.Estado == EstadoCaja.Abierta);
            }
        }

        public CajaDto ObtenerDatosCajaCerrada(long ultimacajaId)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var ultimacaja = context.Cajas.FirstOrDefault(x => x.Id == ultimacajaId);

                return new CajaDto
                {
                    Id = ultimacaja.Id,
                    MontoApertura = ultimacaja.MontoApertura,
                    MontoSistema = ultimacaja.MontoSistema,
                    Diferencia = ultimacaja.MontoCierre - ultimacaja.MontoSistema,
                    MontoCierre = ultimacaja.MontoCierre,
                    FechaApertura = ultimacaja.FechaApertura,
                    FechaCierre = ultimacaja.FechaCierre
                };

                //return context.Cajas.FirstOrDefault(c => c.FechaCierre == caja.FechaCierre).Select(new CajaDto
                //{
                //    MontoApertura = caja.MontoApertura,
                //    MontoSistema = caja.MontoSistema,
                //    Diferencia = caja.MontoCierre - caja.MontoSistema,
                //    MontoCierre = caja.MontoCierre,
                //    FechaApertura = caja.FechaApertura,
                //});
            }
        }

        public bool ObtenerEstadoCaja()
        {
            using (var context = new ModeloXCommerceContainer())
            {
                return context.Cajas.Where(x => x.Estado == EstadoCaja.Abierta).Any();
            }
        }

        public void DescontarMontoSistema(long cajaId, decimal montoDescontar)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var cajaModif = context.Cajas
                    .FirstOrDefault(x => x.Id == cajaId);

                if (cajaModif == null)
                    throw new Exception("Ocurrio un error al Obtener la Caja");

                cajaModif.MontoSistema -= montoDescontar;

                context.SaveChanges();
            }
        }
    }
}
