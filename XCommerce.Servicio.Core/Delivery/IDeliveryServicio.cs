using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCommerce.AccesoDatos;
using XCommerce.Servicio.Core.Delivery.DTOs;
using XCommerce.Servicio.Core.Empleado.DTOs;

namespace XCommerce.Servicio.Core.Delivery
{
    public interface IDeliveryServicio
    {
        long CrearComprobante();

        void AgregarItem(string codigo, decimal cantidad, long comprobanteId);

        void EliminarItem(long? itemId, long comprobanteId);

        DeliveryDto ObtenerUltimoComprobante();

        IEnumerable<DeliveryDto> Obtener(string cadenaBuscar);

        void ObtenerCadeteNuevo(EmpleadoDto dto, long comprobanteId);

        

        void Pagar(decimal descuento, decimal subTotal, decimal total, long comprobanteId, TipoFormaPago formaPago, TipoComprobante tipoComprobante);

        void Total(long comprobanteId, decimal descuento);

        void FormaPagoTarjeta(long tarjetaId, long planTarjetaId, string numeroTicket, string cupon, string numeroTarjeta);

        void FormaPagoCheque(long bancoId, string enteEmisor, string numero, int dias, DateTime dateTime);

        void FormaPagoCtaCte(long clienteId);

        void FormaPagoEfectivo();

        int GenerarNumeroFactura();
    }
}
