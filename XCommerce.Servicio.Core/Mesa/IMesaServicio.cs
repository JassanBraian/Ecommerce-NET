namespace XCommerce.Servicio.Core.Mesa
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using XCommerce.Servicio.Core.Mesa.DTOs;

    public interface IMesaServicio
    {
        long Insertar(MesaDto dto);

        void Modificar(MesaDto dto);

        void Eliminar(long salonId);

        IEnumerable<MesaDto> Obtener(string cadenaBuscar);

        IEnumerable<MesaDto> ObtenerPorSalon(long salonId, string cadenaBuscar);

        IEnumerable<MesaDto> ObtenerVigentesPorSalon(long salonId, string cadenaBuscar);

        MesaDto ObtenerPorId(long entidadId);

        bool ExisteMesaConNumero(int numeroMesa);

        IEnumerable<MesaDto> ObtenerMesasVigentes(string cadenaBuscar);

        IEnumerable<MesaDto> ObtenerMesasVigentesSinReserva(string cadenaBuscar);



        // pantalla de Busqueda //

        void AsignarMesaEjecutada(long? mesaId);

        long? ObtenerMesaEjecutada();

        void BorrarMesaAsignada();

        long ObtenerNuevoNumeroMesa();


        // Union de mesas

        List<MesaDto> ObtenerMesasAUnir();

        void LimpiarMesasAUnir();

        void AgregarMesaAMesasAUnir(MesaDto dto);

        void EliminarMesaDeMesasAUnir(MesaDto dto);
        void UnirMesas();
    }
}
