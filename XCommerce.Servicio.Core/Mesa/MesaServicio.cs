namespace XCommerce.Servicio.Core.Mesa
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using XCommerce.AccesoDatos;
    using XCommerce.Servicio.Core.Mesa.DTOs;

    public class MesaServicio : IMesaServicio
    {
        private static long? _mesaEjecutadaId;

        private static List<MesaDto> mesasAUnir = new List<MesaDto>();

        public void AsignarMesaEjecutada(long? mesaId)
        {
            _mesaEjecutadaId = mesaId;
        }

        public void BorrarMesaAsignada()
        {
            _mesaEjecutadaId = null;
        }

        public void Eliminar(long mesaId)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var eliminarMesa = context.Mesas
                    .FirstOrDefault(x => x.Id == mesaId);
                if (eliminarMesa == null)
                {
                    throw new Exception("Ocurrio un error al obtener la mesa");
                }

                eliminarMesa.EstaEliminado = true;
                context.SaveChanges();

            }
        }

        public bool ExisteMesaConNumero(int numeroMesa)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                return context.Mesas.Any(x => x.Numero == numeroMesa);
            }
        }
    

        public long Insertar(MesaDto dto)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var nuevaMesa = new AccesoDatos.Mesa
                {
                    Numero = dto.Numero,
                    Descripcion = dto.Descripcion,
                    SalonId = dto.SalonId,
                    EstadoMesa = dto.EstadoMesa,
                    EstaEliminado = dto.EstaEliminado,


                };
                context.Mesas.Add(nuevaMesa);
                context.SaveChanges();
                return nuevaMesa.Id;
            }
        }

        public void Modificar(MesaDto dto)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var modificarMesa = context.Mesas
                    .FirstOrDefault(x => x.Id == dto.Id);
                if (modificarMesa == null)
                {
                    throw new Exception("Ocurrio un error al obtener la mesa");
                }
                modificarMesa.Numero = dto.Numero;
                modificarMesa.Descripcion = dto.Descripcion;
                modificarMesa.SalonId = dto.SalonId;
                context.SaveChanges();
            }
        }

        public IEnumerable<MesaDto> Obtener(string cadenaBuscar)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                return context.Mesas
                    .AsNoTracking()
                    .Where(x => x.Numero.ToString().Contains(cadenaBuscar)
                    || x.Descripcion.Contains(cadenaBuscar))
                    .Select(x => new MesaDto
                    {
                        Id = x.Id,
                        Numero = x.Numero,
                        Descripcion = x.Descripcion,
                        EstaEliminado = x.EstaEliminado,
                        Salon = x.Salon.Descripcion
                    }).ToList();
            }
        }

        public long? ObtenerMesaEjecutada()
        {
            return _mesaEjecutadaId;
        }

        public IEnumerable<MesaDto> ObtenerMesasVigentes(string cadenaBuscar)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                return context.Mesas
                    .AsNoTracking()
                    .Where(x => (x.Numero.ToString().Contains(cadenaBuscar)
                    || x.Descripcion.Contains(cadenaBuscar)) && x.EstaEliminado == false)
                    .Select(x => new MesaDto
                    {
                        Id = x.Id,
                        Numero = x.Numero,
                        Descripcion = x.Descripcion,
                        EstaEliminado = x.EstaEliminado,
                        Salon = x.Salon.Descripcion
                    }).ToList();
            }
        }

        public IEnumerable<MesaDto> ObtenerMesasVigentesSinReserva(string cadenaBuscar)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                return context.Mesas
                    .AsNoTracking()
                    .Where(x => (x.Numero.ToString().Contains(cadenaBuscar)
                    || x.Descripcion.Contains(cadenaBuscar)) && x.EstaEliminado == false
                    && x.EstadoMesa == EstadoMesa.Cerrada)
                    .Select(x => new MesaDto
                    {
                        Id = x.Id,
                        Numero = x.Numero,
                        Descripcion = x.Descripcion,
                        EstaEliminado = x.EstaEliminado,
                        Salon = x.Salon.Descripcion
                    }).ToList();
            }
        }

        public long ObtenerNuevoNumeroMesa()
        {
            using (var context = new ModeloXCommerceContainer())
            {
                if (context.Mesas.Any())
                {
                    return context.Mesas.Max(x => x.Numero) + 1;
                }
                else return 1;
                
            }
        }

        public MesaDto ObtenerPorId(long entidadId)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                return context.Mesas
                    .AsNoTracking()
                    .Select(x => new MesaDto
                    {
                        Id = x.Id,
                        Numero = x.Numero,
                        Descripcion = x.Descripcion,
                        EstaEliminado = x.EstaEliminado,
                        EstadoMesa = x.EstadoMesa,
                        SalonId = x.SalonId


                    }).FirstOrDefault(x => x.Id == entidadId);
            }
        }

        public IEnumerable<MesaDto> ObtenerPorSalon(long salonId, string cadenaBuscar)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                return context.Mesas
                    .AsNoTracking()
                    .Where(x => x.SalonId == salonId
                                && x.Descripcion.Contains(cadenaBuscar))
                    .Select(x => new MesaDto
                    {
                        Id = x.Id,
                        Numero = x.Numero,
                        EstadoMesa = x.EstadoMesa,
                        Descripcion = x.Descripcion,
                        SalonId = x.SalonId,
                        Salon = x.Salon.Descripcion,
                        EstaEliminado = x.EstaEliminado,


                    }).ToList();
            }
        }

        public IEnumerable<MesaDto> ObtenerVigentesPorSalon(long salonId, string cadenaBuscar)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                return context.Mesas
                    .AsNoTracking()
                    .Where(x => x.SalonId == salonId
                                && x.Descripcion.Contains(cadenaBuscar) 
                                && x.EstadoMesa != EstadoMesa.FueraServicio
                                && x.EstaEliminado == false)
                    .Select(x => new MesaDto
                    {
                        Id = x.Id,
                        Numero = x.Numero,
                        EstadoMesa = x.EstadoMesa,
                        Descripcion = x.Descripcion,
                        SalonId = x.SalonId,
                        Salon = x.Salon.Descripcion,
                        EstaEliminado = x.EstaEliminado,


                    }).ToList();
            }
        }

        public List<MesaDto> ObtenerMesasAUnir()
        {
            return mesasAUnir;
        }

        public void LimpiarMesasAUnir()
        {
            mesasAUnir.Clear();
        }

        public void AgregarMesaAMesasAUnir(MesaDto dto)
        {
            mesasAUnir.Add(dto);
        }

        public void EliminarMesaDeMesasAUnir(MesaDto dto)
        {
            mesasAUnir.Remove(dto);
        }

        public void UnirMesas()
        {
            // Cargar el id del comprobante de la primera mesa cargada a la lista, en todos los detalles de comprobante de las mesas restantes

            

            // Ver que hacer con los comprobantes de las mesas restantes

            // Desaparecer las mesas restantes

            // Hacer rectangular la mesa que queda
        }
    }
}
