namespace XCommerce.Servicio.Core.Cliente
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using XCommerce.AccesoDatos;
    using XCommerce.Servicio.Core.Cliente.DTOs;
    using XCommerce.Servicio.Core.ComprobanteCtaCte.DTOs;

    public class ClienteServicio : IClienteServicio
    {
        private static long ClienteSelecionadoCtaCte;
        private static long ClienteSeleccionadoId;

        public void Eliminar(long clienteId)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var clienteEliminar = context.Personas.OfType<AccesoDatos.Cliente>()
                    .FirstOrDefault(x => x.Id == clienteId);

                if (clienteEliminar == null)
                    throw new Exception("No se encontro el Cliente");

                clienteEliminar.EstaEliminado = true;


                context.SaveChanges();
            }
        }

        public long Insertar(ClienteDto dto)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var nuevoCliente = new AccesoDatos.Cliente
                {
                    Apellido = dto.Apellido,
                    Nombre = dto.Nombre,
                    Dni = dto.Dni,
                    Telefono = dto.Telefono,
                    Celular = dto.Celular,
                    Email = dto.Email,
                    Cuil = dto.Cuil,
                    FechaNacimiento = dto.FechaNacimiento,
                    Foto = dto.Foto,
                    MontoMaximoCtaCte = dto.MontoMaximoCtaCte,
                    SaldoCtaCte = 0m,
                    Direccion = new Direccion
                    {
                        Calle = dto.Calle,
                        Numero = dto.Numero,
                        Piso = dto.Piso,
                        Dpto = dto.Dpto,
                        Casa = dto.Casa,
                        Lote = dto.Lote,
                        Barrio = dto.Barrio,
                        Mza = dto.Mza,
                        LocalidadId = dto.LocalidadId
                    }

                };


                context.Personas.Add(nuevoCliente);

                context.SaveChanges();

                return nuevoCliente.Id;
            }
        }

        public void Modificar(ClienteDto dto)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var clienteModificar = context.Personas.OfType<AccesoDatos.Cliente>()
                    .Include(x => x.Direccion)
                    .FirstOrDefault(x => x.Id == dto.Id);

                if (clienteModificar == null)
                    throw new Exception("No se encontro el Cliente");

                clienteModificar.Apellido = dto.Apellido;
                clienteModificar.Nombre = dto.Nombre;
                clienteModificar.Dni = dto.Dni;
                clienteModificar.Telefono = dto.Telefono;
                clienteModificar.Celular = dto.Celular;
                clienteModificar.Email = dto.Email;
                clienteModificar.Cuil = dto.Cuil;
                clienteModificar.FechaNacimiento = dto.FechaNacimiento;
                clienteModificar.Foto = dto.Foto;
                clienteModificar.MontoMaximoCtaCte = dto.MontoMaximoCtaCte;
                clienteModificar.SaldoCtaCte = dto.SaldoCtaCte;

                clienteModificar.Direccion.Calle = dto.Calle;
                clienteModificar.Direccion.Numero = dto.Numero;
                clienteModificar.Direccion.Piso = dto.Piso;
                clienteModificar.Direccion.Dpto = dto.Dpto;
                clienteModificar.Direccion.Casa = dto.Casa;
                clienteModificar.Direccion.Lote = dto.Lote;
                clienteModificar.Direccion.Barrio = dto.Barrio;
                clienteModificar.Direccion.Mza = dto.Mza;
                clienteModificar.Direccion.LocalidadId = dto.LocalidadId;


                context.SaveChanges();
            }
        }

        public IEnumerable<ClienteDto> Obtener(string cadenaBuscar)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                return context.Personas.OfType<AccesoDatos.Cliente>()
                    .AsNoTracking()
                    .Include(x => x.Direccion)
                    .Include(x => x.Direccion.Localidad)
                    .Where(x => (x.Nombre.Contains(cadenaBuscar)
                                || x.Apellido.Contains(cadenaBuscar)
                                || x.Dni == cadenaBuscar
                                || x.Email == cadenaBuscar)
                                && x.Cuil != "99999999999")

                    .Select(x => new ClienteDto
                    {
                        Id = x.Id,
                        Apellido = x.Apellido,
                        Nombre = x.Nombre,
                        Dni = x.Dni,
                        Telefono = x.Telefono,
                        Celular = x.Celular,
                        Email = x.Email,
                        Cuil = x.Cuil,
                        FechaNacimiento = x.FechaNacimiento,
                        Foto = x.Foto,
                        EstaEliminado = x.EstaEliminado,
                        Calle = x.Direccion.Calle,
                        Numero = x.Direccion.Numero,
                        Piso = x.Direccion.Piso,
                        Dpto = x.Direccion.Dpto,
                        Casa = x.Direccion.Casa,
                        Lote = x.Direccion.Lote,
                        Barrio = x.Direccion.Barrio,
                        Mza = x.Direccion.Mza,
                        LocalidadId = x.Direccion.LocalidadId,
                        ProvinciaId = x.Direccion.Localidad.ProvinciaId,
                        MontoMaximoCtaCte = x.MontoMaximoCtaCte,
                        SaldoCtaCte = x.SaldoCtaCte,
                    }).ToList();
            }
        }

        public ClienteDto ObtenerPorId(long entidadId)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                return context.Personas.OfType<AccesoDatos.Cliente>()
                    .AsNoTracking()
                    .Include(x => x.Direccion)
                    .Include(x => x.Direccion.Localidad)
                    .Select(x => new ClienteDto
                    {
                        Id = x.Id,
                        Apellido = x.Apellido,
                        Nombre = x.Nombre,
                        Dni = x.Dni,
                        Telefono = x.Telefono,
                        Celular = x.Celular,
                        Email = x.Email,
                        Cuil = x.Cuil,
                        FechaNacimiento = x.FechaNacimiento,
                        Foto = x.Foto,
                        EstaEliminado = x.EstaEliminado,
                        Calle = x.Direccion.Calle,
                        Numero = x.Direccion.Numero,
                        Piso = x.Direccion.Piso,
                        Dpto = x.Direccion.Dpto,
                        Casa = x.Direccion.Casa,
                        Lote = x.Direccion.Lote,
                        Barrio = x.Direccion.Barrio,
                        Mza = x.Direccion.Mza,
                        LocalidadId = x.Direccion.LocalidadId,
                        ProvinciaId = x.Direccion.Localidad.ProvinciaId,
                        MontoMaximoCtaCte = x.MontoMaximoCtaCte,
                        SaldoCtaCte = x.SaldoCtaCte,
                    }
                    ).FirstOrDefault(x => x.Id == entidadId);
            }
        }

        public IEnumerable<ClienteDto> ObtenerExistentes(string cadenaBuscar)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                return context.Personas.OfType<AccesoDatos.Cliente>()
                    .AsNoTracking()
                    .Include(x => x.Direccion)
                    .Include(x => x.Direccion.Localidad)
                    .Where(x => (x.Nombre.Contains(cadenaBuscar)
                                || x.Apellido.Contains(cadenaBuscar)
                                || x.Dni == cadenaBuscar
                                || x.Email == cadenaBuscar) && x.Dni != "99999999" && x.EstaEliminado == false)
                    .Select(x => new ClienteDto
                    {
                        Id = x.Id,
                        Apellido = x.Apellido,
                        Nombre = x.Nombre,
                        Dni = x.Dni,
                        Telefono = x.Telefono,
                        Celular = x.Celular,
                        Email = x.Email,
                        Cuil = x.Cuil,
                        FechaNacimiento = x.FechaNacimiento,
                        Foto = x.Foto,
                        EstaEliminado = x.EstaEliminado,
                        Calle = x.Direccion.Calle,
                        Numero = x.Direccion.Numero,
                        Piso = x.Direccion.Piso,
                        Dpto = x.Direccion.Dpto,
                        Casa = x.Direccion.Casa,
                        Lote = x.Direccion.Lote,
                        Barrio = x.Direccion.Barrio,
                        Mza = x.Direccion.Mza,
                        LocalidadId = x.Direccion.LocalidadId,
                        ProvinciaId = x.Direccion.Localidad.ProvinciaId,
                        MontoMaximoCtaCte = x.MontoMaximoCtaCte,
                        SaldoCtaCte = x.SaldoCtaCte,
                    }).ToList();
            }
        }
        public bool ExisteClienteDni(string dni)
          {
            using (var context = new ModeloXCommerceContainer())
            {
                return context.Personas.OfType<AccesoDatos.Cliente>().Any(x => x.Dni == dni && x.EstaEliminado == false);
            }
          }


        public long ObtenerIdCliente(string dni)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var cliente = context.Personas.OfType<AccesoDatos.Cliente>().Where(x => x.Dni == dni)
                    .Select(x => new ClienteDto
                    {
                        Id = x.Id,
                    }).FirstOrDefault();

                return cliente.Id;
            }
        }

        public ClienteDto ObtenerClientePorCuil(string cuil)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                return context.Personas.OfType<AccesoDatos.Cliente>()
                    .AsNoTracking()
                    .Include(x => x.Direccion)
                    .Include(x => x.Direccion.Localidad)
                    .Select(x => new ClienteDto
                    {
                        Id = x.Id,
                        Apellido = x.Apellido,
                        Nombre = x.Nombre,
                        Dni = x.Dni,
                        Telefono = x.Telefono,
                        Celular = x.Celular,
                        Email = x.Email,
                        Cuil = x.Cuil,
                        FechaNacimiento = x.FechaNacimiento,
                        Foto = x.Foto,
                        EstaEliminado = x.EstaEliminado,
                        Calle = x.Direccion.Calle,
                        Numero = x.Direccion.Numero,
                        Piso = x.Direccion.Piso,
                        Dpto = x.Direccion.Dpto,
                        Casa = x.Direccion.Casa,
                        Lote = x.Direccion.Lote,
                        Barrio = x.Direccion.Barrio,
                        Mza = x.Direccion.Mza,
                        LocalidadId = x.Direccion.LocalidadId,
                        ProvinciaId = x.Direccion.Localidad.ProvinciaId,
                        MontoMaximoCtaCte = x.MontoMaximoCtaCte,
                        SaldoCtaCte = x.SaldoCtaCte,
                    }
                    ).FirstOrDefault(x => x.Cuil == cuil);
            }
        }

        public void AumentarSaldoCtaCte(long entidadId, decimal montoaumentar)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var cliente = context.Personas.OfType<AccesoDatos.Cliente>()
                    .FirstOrDefault(x => x.Id == entidadId);



                cliente.SaldoCtaCte += montoaumentar;


                context.SaveChanges();
            }
        }

        public bool HaySaldoSuficiente(long entidadId, decimal monto)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var cliente = context.Personas.OfType<AccesoDatos.Cliente>()
                    .FirstOrDefault(x => x.Id == entidadId);

                var saldoFinal = cliente.SaldoCtaCte + monto;

                if (cliente.MontoMaximoCtaCte > saldoFinal) return true;
                else return false;
                
            }
        }

        public long ObtenerClienteCtaCte()
        {
            return ClienteSelecionadoCtaCte;
        }

        public void EstablecerClienteCtaCte(long clienteId)
        {
            ClienteSelecionadoCtaCte = clienteId;
        }

        public void PagarSaldoCtaCte(long clienteId, decimal monto)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var cliente = context.Personas.OfType<AccesoDatos.Cliente>()
                    .FirstOrDefault(x => x.Id == clienteId);

                cliente.SaldoCtaCte -= monto;

                context.SaveChanges();
            }
        }

        public bool ExisteCuilCargado(string cuil)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var cliente = context.Personas.OfType<AccesoDatos.Cliente>()
                    .FirstOrDefault(x => x.Cuil == cuil);

                if (cliente == null) return false;
                else return true;
            }
        }

        public IEnumerable<ClienteDto> ObtenerClienteCtaCte(string cadenaBuscar)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                return context.Personas.OfType<AccesoDatos.Cliente>()
                  .AsNoTracking()
                  .Select(x => new ClienteDto
                  {
                      Id = x.Id,
                      Apellido = x.Apellido,
                      Nombre = x.Nombre,                    
                      Cuil = x.Cuil,
                      MontoMaximoCtaCte = x.MontoMaximoCtaCte,
                      SaldoCtaCte = x.SaldoCtaCte,
                  }).ToList();


            }
        }

        public void AsignarEntidadSelecId(long entidadId)
        {
            ClienteSeleccionadoId = entidadId;
        }

        public long ObtenerEntidadSelecId()
        {
            return ClienteSeleccionadoId;
        }

        public void LimpiarEntidadSelecId()
        {
            ClienteSeleccionadoId = 0;
        }
    }

}



