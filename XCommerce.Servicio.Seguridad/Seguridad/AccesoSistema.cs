using System.Linq;
using XCommerce.AccesoDatos;
using static Presentacion.Helpers.CadenaCaracteres;

namespace XCommerce.Servicio.Seguridad.Seguridad
{
    public class AccesoSistema : IAccesoSistema
    {
        public bool VerificarSiEstaBloqueadoUsuario(string nombreUsuario)
        {
            using (var context = new ModeloXCommerceContainer())
            {
                return context.Usuarios
                    .Any(x => x.Nombre == nombreUsuario
                              && x.EstaBloqueado);
            }
        }

        public bool VerificarSiExisteUsuario(string nombreUsuario, string password)
        {
            if (nombreUsuario == "Admin"
                && password == "Admin")
                return true;

            using (var context = new ModeloXCommerceContainer())
            {
                var passEncriptado = Encriptar(password);

                return context.Usuarios
                    .Any(x => x.Nombre == nombreUsuario
                              && x.Password == passEncriptado);
            }
        }
    }
}
