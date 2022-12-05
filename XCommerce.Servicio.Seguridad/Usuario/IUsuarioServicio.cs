using System.Collections.Generic;
using XCommerce.Servicio.Seguridad.Usuario.DTOs;

namespace XCommerce.Servicio.Seguridad.Usuario
{
    public interface IUsuarioServicio
    {
        /// <summary>
        /// Metodo para Bloquear o Desbloquear el Usuario
        /// </summary>
        /// <param name="nombreUsuario">Nombre del USuario</param>
        /// <param name="estado">Estado => True: bloquear; False: desbloquear</param>
        void CambiarEstado(string nombreUsuario, bool estado);

        IEnumerable<UsuarioDto> Obtener(string cadenaBuscar);

        /* para crear usuario se debe capturar primero el nombre, luego el apellido, 
            quitar los espacios en blanco (trim) y verificar que no exista*/
        void Crear(long personaId, string apellido, string nombre);

        long ObtenerIdPorUsuario(string text);

        UsuarioDto ObtenerUsuarioPorEmpleadoId(long empleadoId);

    }
}
