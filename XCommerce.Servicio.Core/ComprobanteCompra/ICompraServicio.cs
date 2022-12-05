using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCommerce.Servicio.Core.ComprobanteCompra.DTOs;

namespace XCommerce.Servicio.Core.ComprobanteCompra
{
    public interface ICompraServicio
    {
        long CrearComprobante();

        void AgregarItem(string codigo, decimal cantidad, long comprobanteId);

        void EliminarItem(long? itemId, long comprobanteId);

        void ActualizarPrecioCostoItem(long? productoId, long comprobanteId, decimal nuevoPrecio);

        ComprobanteCompraDto ObtenerUltimoComprobante();

        IEnumerable<ComprobanteCompraDto> ObtenerComprobanteCompra(string cadenaBuscar);

        void Pagar(decimal descuento, decimal subTotal, decimal total, long comprobanteId);

        void Total(long comprobanteId, decimal descuento);

        int GenerarNumeroFactura();
    }
}
