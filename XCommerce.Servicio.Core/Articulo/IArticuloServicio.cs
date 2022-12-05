namespace XCommerce.Servicio.Core.Articulo
{
    using System.Collections.Generic;
    using XCommerce.Servicio.Core.Articulo.DTOs;

    public interface IArticuloServicio
    {
        long Insertar(ArticuloDto dto);

        void Modificar(ArticuloDto dto);

        void Eliminar(long articuloId);

        // ========================================= //

        IEnumerable<ArticuloDto> Obtener(string cadenaBuscar);

        ArticuloDto ObtenerPorId(long entidadId);

        ArticuloDto ObtenerPorCodigo(string codigo);

        long ObtenerSiguienteNumero();

        bool CodigoYaExiste(string codigo);

        bool CodigoBarraYaExiste(string codigoBarra);

        void DescontarStock(long articuloid, decimal cantidadEliminar);

        void AumentarStock(long articuloid, decimal cantidadAumentar);

        void Discontinuar(long articuloid);

      


    }
}
