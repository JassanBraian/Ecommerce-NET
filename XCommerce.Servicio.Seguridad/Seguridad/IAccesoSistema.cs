namespace XCommerce.Servicio.Seguridad.Seguridad
{
    public interface IAccesoSistema
    {
        bool VerificarSiExisteUsuario(string nombreUsuario, string password);

        bool VerificarSiEstaBloqueadoUsuario(string nombreUsuario);

    }
}
