using Utilitarios;
using System;
using System.Threading.Tasks;
using Reservas;
using System.Data;
namespace MatrixService
{
    public interface ISvcReserva
    {
        Task<ListaReserva> ListarReservaAsync(int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral);
        Task<Reserva> GuardaReservaAsync(int IdReserva, int IdSolicitud, int IdSala, string FechaDesdeReserva, string FechaHastaReserva, string HoraInicio, string HoraFin, string Observaciones, int IdUsuarioAuditoria);
        Task<Mensaje> EliminarReservaAsync(int IdReserva, int IdUsuarioAuditoria);
        Task<Reserva> ObtenerReservaAsync(int IdReserva, int IdUsuarioAuditoria);
        Task<DataSet> DescargarReserva(int IdUsuarioAuditoria);
    }
}
