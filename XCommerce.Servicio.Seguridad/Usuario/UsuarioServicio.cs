using System;
using System.Collections.Generic;
using System.Linq;
using XCommerce.AccesoDatos;
using XCommerce.Servicio.Seguridad.Usuario.DTOs;
using static Presentacion.Helpers.CadenaCaracteres;

namespace XCommerce.Servicio.Seguridad.Usuario
{
    public class UsuarioServicio : IUsuarioServicio
    {
        public void CambiarEstado(string nombreUsuario, bool estado)
        {
            using (var conetxt = new ModeloXCommerceContainer())
            {
                var usuario = conetxt.Usuarios
                    .FirstOrDefault(usu => usu.Nombre == nombreUsuario);

                if(usuario == null)
                    throw new Exception($"No se encontro el Usuario: {nombreUsuario}.");

                usuario.EstaBloqueado = estado;

                conetxt.SaveChanges();
            }
        }

        public IEnumerable<UsuarioDto> Obtener(string cadenaBuscar)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                return context.Personas
                    .AsNoTracking()
                    .Where(x => !x.EstaEliminado
                    && (x.Apellido.Contains(cadenaBuscar) || x.Nombre.Contains(cadenaBuscar)))
                    .Select(x => new UsuarioDto
                    {
                        PersonaId = x.Id,
                        ApellidoPersona = x.Apellido,
                        NombrePersona = x.Nombre,
                        // La linea siguiente equivale a "si yo tengo un usuario asociado a esa persona"
                        Nombre = x.Usuarios.Any()
                            ? x.Usuarios.FirstOrDefault().Nombre
                            : "NO ASIGNADO",
                        Id = x.Usuarios.Any()
                            ? x.Usuarios.FirstOrDefault().Id
                            : 0,
                        EstaBloqueado = x.Usuarios.Any()
                            ? x.Usuarios.FirstOrDefault().EstaBloqueado
                            : false
                    }).ToList();
            }
        }

        public void Crear(long personaId, string apellido, string nombre)
        {
            var nombreUsuario = CrearNombreUsuario(apellido, nombre);

            using (var context = new ModeloXCommerceContainer())
            {
                context.Usuarios.Add(new AccesoDatos.Usuario
                {
                    PersonaId = personaId,
                    EstaBloqueado = false,
                    Nombre = nombreUsuario.ToLower(),
                    Password = Encriptar("123")
                });

                context.SaveChanges();
            }
        }

        private string CrearNombreUsuario(string apellido, string nombre)
        {
            int contadorLetras = 1;
            int digito = 1;
            var usuarioNuevo = $"{nombre.Trim().Substring(0, contadorLetras)}{apellido.Trim()}";    // Trim quita espacios en blanco

            using (var context = new ModeloXCommerceContainer())
            {
                // el Any verifica si existe algun registro en la tabla Usuario que contega un nombre igual al que se esta pasando
                while (context.Usuarios.Any(x => x.Nombre == usuarioNuevo))
                {
                    if(contadorLetras <= nombre.Trim().Length)
                    {
                        contadorLetras++;   // ya que se repite, le agrega una letra.
                        usuarioNuevo = $"{nombre.Substring(0, contadorLetras)}{apellido.Trim()}";
                    }
                    else
                    {
                        usuarioNuevo = $"{nombre.Substring(0, contadorLetras)}{apellido.Trim()}{digito}";
                        digito++;
                    }                    
                }
            }
            return usuarioNuevo;
        }
        public bool Existe(long personaId)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                var usuario = context.Usuarios.Any(x => x.PersonaId == personaId);

                if (usuario == true)
                {
                    return true;
                }
                return false;

            }
        }

        public long ObtenerIdPorUsuario(string nombre)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                return context.Usuarios
                    .AsNoTracking()
                    .FirstOrDefault(x => x.Nombre == nombre).Id;    
                    
            }
        }

        public UsuarioDto ObtenerUsuarioPorEmpleadoId(long empleadoId)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                return context.Usuarios
                    .AsNoTracking()
                    .Select(x => new UsuarioDto
                    {
                        Id = x.Id,
                        Nombre = x.Nombre,
                        PersonaId = x.PersonaId,
                        ApellidoPersona = x.Persona.Apellido,
                        NombrePersona = x.Persona.Nombre,
                        EstaBloqueado = x.EstaBloqueado

                    }).FirstOrDefault(x => x.PersonaId == empleadoId);

            }
        }
    }
}
