using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCommerce.AccesoDatos;
using XCommerce.Servicio.Core.Reserva.DTOs;

namespace XCommerce.Servicio.Core.Reserva
{
    public class ReservaServicio : IReservaServicio
    {
        public bool CancelarReserva(long _mesaId)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var reservaEliminar = context.Reservas.FirstOrDefault(x => x.MesaId == _mesaId && x.EstaElimido == false);
                if (reservaEliminar != null)
                {
                    reservaEliminar.EstaElimido = true;

                    reservaEliminar.EstadoReserva = EstadoReserva.Cancelada;

                    var cambiarEstadoMesa = context.Mesas.FirstOrDefault(d => d.Id == _mesaId);

                    cambiarEstadoMesa.EstadoMesa = EstadoMesa.Cerrada;

                    context.SaveChanges();

                    return true;
                }
                return false;

            }
        }

        public void Eliminar(long reservaId)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var reservaEliminar = context.Reservas
                    .FirstOrDefault(x => x.Id == reservaId);

                if (reservaEliminar == null)
                    throw new Exception("Ocurrio un error al Obtener la reserva");

                reservaEliminar.EstaElimido = true;

                var mesa = context.Mesas.FirstOrDefault(d => d.Id == reservaEliminar.MesaId);

                if (mesa != null)
                {
                    mesa.EstadoMesa = EstadoMesa.Cerrada;
                }

                context.SaveChanges();
            }
        }

        public long Insertar(ReservaDto dto)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var reservaNueva = new AccesoDatos.Reserva
                {
                    Senia = dto.Senia,
                    Fecha = dto.Fecha,
                    EstadoReserva = dto.EstadoReserva,
                    MesaId = dto.MesaId,
                    ClienteId = dto.ClienteId,
                    UsuarioId = dto.UsuarioId,
                    MotivoReservaId = dto.MotivoReservaId,
                    EstaElimido = dto.EstaEliminado

                };

                context.Reservas.Add(reservaNueva);

                var mesaReservada = context.Mesas.FirstOrDefault(x => x.Id == reservaNueva.MesaId &&
                x.EstadoMesa != EstadoMesa.Reservada && x.EstadoMesa != EstadoMesa.Abierta && x.EstadoMesa != EstadoMesa.FueraServicio);

                if (mesaReservada != null)
                {
                    mesaReservada.EstadoMesa = EstadoMesa.Reservada;
                    context.Reservas.Add(reservaNueva);

                    context.SaveChanges();

                }

                return reservaNueva.Id;
            }
        }

        public void Modificar(ReservaDto reservaDto)
        {
            using (var context = new ModeloXCommerceContainer())
            {

                var reservaModificar = context.Reservas
                    .FirstOrDefault(x => x.Id == reservaDto.Id);

                if (reservaModificar == null)
                    throw new Exception("Ocurrio un error al Obtener la reserva");

                reservaModificar.Senia = reservaDto.Senia;
                reservaModificar.Fecha = reservaDto.Fecha;
                reservaModificar.EstadoReserva = reservaDto.EstadoReserva;
                reservaModificar.MesaId = reservaDto.MesaId;
                reservaModificar.ClienteId = reservaDto.ClienteId;
                reservaModificar.UsuarioId = reservaDto.UsuarioId;
                reservaModificar.MotivoReservaId = reservaDto.MotivoReservaId;

                context.SaveChanges();


            }
        }

        public IEnumerable<ReservaDto> Obtener(string cadenaBuscar)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                return context.Reservas
                    .Select(x => new ReservaDto
                    {
                        Id = x.Id,
                        Senia = x.Senia,
                        EstaEliminado = x.EstaElimido,
                        Fecha = x.Fecha,
                        UsuarioId = x.UsuarioId,
                        ClienteId = x.ClienteId,
                        ClienteApellido = context.Personas.FirstOrDefault(p => p.Id == x.ClienteId).Apellido,
                        ClienteDni = context.Personas.FirstOrDefault(p => p.Id == x.ClienteId).Dni,
                        MotivoReservaId = x.MotivoReservaId,
                        MesaId = x.MesaId,
                        Mesa = x.Mesa.Numero.ToString(),
                        EstadoReserva = x.EstadoReserva,
                    }).ToList();
            }
        }

        public IEnumerable<ReservaDto> ObtenerActuales(string cadenaBuscar)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var cajaAbierta = context.Cajas.FirstOrDefault(x => x.Estado == EstadoCaja.Abierta);

                return context.Reservas
                    .Where(x => x.Fecha > cajaAbierta.FechaApertura && x.EstadoReserva != EstadoReserva.Cancelada && x.EstaElimido == false)
                    .Select(x => new ReservaDto
                    {
                        Id = x.Id,
                        Senia = x.Senia,
                        EstaEliminado = x.EstaElimido,
                        Fecha = x.Fecha,
                        UsuarioId = x.UsuarioId,
                        ClienteId = x.ClienteId,
                        ClienteApellido = context.Personas.FirstOrDefault(p => p.Id == x.ClienteId).Apellido,
                        ClienteDni = context.Personas.FirstOrDefault(p => p.Id == x.ClienteId).Dni,
                        MotivoReservaId = x.MotivoReservaId,
                        MesaId = x.MesaId,
                        Mesa = x.Mesa.Numero.ToString(),
                        EstadoReserva = x.EstadoReserva,
                    }).ToList();
            }
        }

        public IEnumerable<ReservaDto> ObtenerDeCajaActual(string cadenaBuscar)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var cajaAbierta = context.Cajas.FirstOrDefault(x => x.Estado == EstadoCaja.Abierta);

                return context.Reservas
                    .Where(x=>x.Fecha > cajaAbierta.FechaApertura && x.EstaElimido == false)
                    .Select(x => new ReservaDto
                    {
                        Id = x.Id,
                        Senia = x.Senia,
                        EstaEliminado = x.EstaElimido,
                        Fecha = x.Fecha,
                        UsuarioId = x.UsuarioId,
                        ClienteId = x.ClienteId,
                        ClienteApellido = context.Personas.FirstOrDefault(p => p.Id == x.ClienteId).Apellido,
                        ClienteDni = context.Personas.FirstOrDefault(p => p.Id == x.ClienteId).Dni,
                        MotivoReservaId = x.MotivoReservaId,
                        MesaId = x.MesaId,
                        Mesa = x.Mesa.Numero.ToString(),
                        EstadoReserva = x.EstadoReserva,
                    }).ToList();
            }
        }

        public ReservaDto ObtenerPorId(long entidadId)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                return context.Reservas
                    .AsNoTracking()
                    .Select(x => new ReservaDto
                    {
                        Id = x.Id,
                        Senia = x.Senia,
                        EstaEliminado = x.EstaElimido,
                        Fecha = x.Fecha,
                        UsuarioId = x.UsuarioId,
                        ClienteId = x.ClienteId,
                        ClienteApellido = context.Personas.FirstOrDefault(p => p.Id == x.ClienteId).Apellido,
                        ClienteDni = context.Personas.FirstOrDefault(p => p.Id == x.ClienteId).Dni,
                        MotivoReservaId = x.MotivoReservaId,
                        MesaId = x.MesaId,
                        EstadoReserva = x.EstadoReserva,
                    }).FirstOrDefault(x => x.Id == entidadId);
            }
        }

        public ReservaDto ObtenerPorMesaId(long mesaId)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                return context.Reservas
                    .AsNoTracking()
                    .Select(x => new ReservaDto
                    {
                        Id = x.Id,
                        Senia = x.Senia,
                        EstaEliminado = x.EstaElimido,
                        Fecha = x.Fecha,
                        UsuarioId = x.UsuarioId,
                        ClienteId = x.ClienteId,
                        ClienteApellido = context.Personas.FirstOrDefault(p => p.Id == x.ClienteId).Apellido,
                        ClienteDni = context.Personas.FirstOrDefault(p => p.Id == x.ClienteId).Dni,
                        MotivoReservaId = x.MotivoReservaId,
                        MesaId = x.MesaId,
                        EstadoReserva = x.EstadoReserva,
                    }).FirstOrDefault(x => x.MesaId == mesaId);
            }
        }
    }
}
