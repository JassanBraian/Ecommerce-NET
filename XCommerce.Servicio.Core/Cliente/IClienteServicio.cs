namespace XCommerce.Servicio.Core.Cliente
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using XCommerce.Servicio.Core.Cliente.DTOs;
    using XCommerce.Servicio.Core.ComprobanteCtaCte.DTOs;

    public interface IClienteServicio
    {
        long Insertar(ClienteDto dto);

        void Modificar(ClienteDto dto);

        void Eliminar(long empleadoId);

        // ===================================================== //

        IEnumerable<ClienteDto> Obtener(string cadenaBuscar);
        
        ClienteDto ObtenerPorId(long entidadId);

        IEnumerable<ClienteDto> ObtenerExistentes(string cadenaBuscar);

        bool ExisteClienteDni(string dni);

        long ObtenerIdCliente(string dni);

        ClienteDto ObtenerClientePorCuil(string cuil);        

        bool ExisteCuilCargado(string cuil);

        void AsignarEntidadSelecId(long entidadId);

        long ObtenerEntidadSelecId();

        void LimpiarEntidadSelecId();


        // ===================================================== //
                              //  Cta Cte  //

        IEnumerable<ClienteDto> ObtenerClienteCtaCte(string cadenaBuscar);

        void AumentarSaldoCtaCte(long entidadId, decimal montoaumentar);

        bool HaySaldoSuficiente(long entidadId, decimal montodescontar);

        long ObtenerClienteCtaCte();

        void EstablecerClienteCtaCte(long clienteId);

        void PagarSaldoCtaCte(long clienteId, decimal monto);

        

        // ===================================================== //
    }
}

