using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCommerce.Servicio.Core.Caja.DTOs;

namespace XCommerce.Servicio.Core.Caja
{
    public interface ICajaServicio
    {
        void AbrirCaja(decimal montoApertura, DateTime fechaApertura, long usuarioAperturaId);

        long CerrarCaja(decimal montoCierre, DateTime fechaCierre, decimal monto);
        
        decimal CalcularMontoSistema(DateTime fechaApertura);

        CajaDto ObtenerDatosCajaCerrada(long ultimacajaid);

        bool ObtenerEstadoCaja();

        bool HayMesasAbiertas();

        void CrearDetalleCaja(long cajaId, decimal monto);

        IEnumerable<CajaDto> Obtener(string fecha);

        CajaDto ObtenerCajaAbierta();

        void AumentarMontoSistema(long cajaId, decimal montoAumentar);

        void DescontarMontoSistema(long cajaId, decimal montoDescontar);
    }
}
