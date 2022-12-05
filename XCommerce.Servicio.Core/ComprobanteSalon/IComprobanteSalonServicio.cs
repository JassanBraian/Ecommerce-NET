namespace XCommerce.Servicio.Core.Comprobante
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using XCommerce.AccesoDatos;
    using XCommerce.Servicio.Core.Comprobante.DTOs;
    using XCommerce.Servicio.Core.Empleado.DTOs;
    using XCommerce.Servicio.Core.Producto.DTOs;

    public interface IComprobanteSalonServicio
    {
        void Generar(long mesaId, long usuarioId, int comensales, long?  mozoId  );

        ComprobanteMesaDto ObtenerComprobanteMesa(long mesaId);

        IEnumerable<ComprobanteMesaDto> Obtener(string cadenaBuscar);

        void ObtenerProductoNuevo(ProductoDto productoSeleccionado, long comprobanteId);

        void AgregarItem(long mesaId, decimal cantidad, ProductoDto dto);

        void EliminarItem(long? itemId, long comprobanteId);

        void ActualizarMontos(long comprobanteId, decimal subtotal, decimal descuento);

        void CerrarMesa(long mesaId);

        void ObtenerMozoNuevo(EmpleadoDto dto, long comprobanteId);

        void DescontarMontoSenia(long comprobanteId, decimal senia);

        void FormaPagoTarjeta(long tarjetaId, long planTarjetaId, string numeroTicket, string cupon, string numeroTarjeta, ComprobanteMesaDto comprobante);

        void FormaPagoCheque(long bancoId, string enteEmisor, string numero, int dias, DateTime dateTime, ComprobanteMesaDto comprobante);

        void FormaPagoCtaCte(long clienteId, ComprobanteMesaDto comprobante);

        void FormaPagoEfectivo(ComprobanteMesaDto comprobante);

        void AsignarCliente(long comprobanteId, long clienteId);
        void PagarComprobante(ComprobanteMesaDto comprobante, TipoFormaPago tipoFormaPago, TipoComprobante tipoComprobante);

        int GenerarNumeroFactura();

        void CerrarComprobanteSinFacturar(ComprobanteMesaDto comprobante);
    }
}
