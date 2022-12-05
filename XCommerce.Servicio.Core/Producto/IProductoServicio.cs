namespace XCommerce.Servicio.Core.Producto
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using XCommerce.Servicio.Core.Producto.DTOs;

    public interface IProductoServicio
    {

        // Servicios para Venta en Salon

        ProductoDto ObtenerPorCodigoParaMesa(long mesaId, string codigo);

        IEnumerable<ProductoDto> ObtenerParaMesa(string cadenaBuscar, long mesaId);



        // Servicios para Kiosco

        IEnumerable<ProductoDto> ObtenerParaKiosco(string cadenaBuscar);




        // Servicios para Delivery

        IEnumerable<ProductoDto> ObtenerParaDelivery(string cadenaBuscar);




        // Servicios para Compra

        ProductoDto ObtenerPorCodigoParaCompra(string codigo);

        IEnumerable<ProductoDto> ObtenerParaCompra(string cadenaBuscar);



        // servicio de busqueda por codigo GENERICO

        ProductoDto ObtenerPorCodigo(string codigo, string listaPrecio);



        //para saber si tiene precio 
        bool TienePrecioParaSalon(string codigo, long salonId, long mesaId);

        bool TienePrecioParaGeneral(string codigo, string listaPrecioDescripcion);

        bool ExisteProducto(string codigo);

        bool SuperaLimiteVenta(long articuloid , decimal cantidadvendida);

        bool CumpleStockMaximo(long productoId, decimal stockIngresante);

        bool CumpleStockMinimo(long productoId, decimal stockEgresante);

        bool PermiteStockNegativo(long productoId, decimal cantidadvendida);

        void AsignarProducSelecId(long productoId);

        long ObtenerProducSelecId();

        void LimpiarProducSelecId();
    }
}
