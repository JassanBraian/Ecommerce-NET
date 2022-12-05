using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCommerce.Servicio.Core.Precio.DTOs;

namespace XCommerce.Servicio.Core.Precio
{
    public  interface IPrecioServicio
    {
        long Insertar(PrecioDto dto);

        void Modificar(PrecioDto dto);

        //==============================================================//

        IEnumerable<PrecioDto> Obtener(string cadenaBuscar);

        IEnumerable<PrecioDto> ObtenerPorListaPrecioDescrip(string cadenaBuscar, string listaPrecioDescrip);

        PrecioDto ObtenerPorId(long? entidadId);

        PrecioDto ObtenerPorArticuloYListaCosto(long articuloId);

        bool PrecioYaExiste(long ArticuloId, long ListaPrecioId);


        // Actualizar precio publico por cambio de precio costo articulo

        void ActualizarPrecioPublico(PrecioDto precio,decimal precioCostoNuevo);

        IEnumerable<PrecioDto> ObtenerSegunArticulo(long articuloId);


        // Actualizar precio publico por cambio de rentabilidad

        void ActualizarRentabilidad(PrecioDto precio, decimal rentabilidad);

        IEnumerable<PrecioDto> ObtenerSegunListaPrecio(long listaPrecioId);
        
    }
}
