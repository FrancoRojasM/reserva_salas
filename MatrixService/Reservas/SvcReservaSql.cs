
using Utilitarios;
using System;
using Reservas;
using System.Threading.Tasks;
using System.Data;
namespace MatrixService

{
    public class SvcReservaSql : ISvcReserva
    {
        private ReservasLogic.ReservaLogic reserva;
        public SvcReservaSql()
        {
            reserva = new ReservasLogic.ReservaLogic();
        }
        public async Task<ListaReserva> ListarReservaAsync(int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
        {
            return await reserva.ListarReservaAsync(DatosGlobales.GestorSqlServer, IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
        }
        public async Task<Reserva> GuardaReservaAsync(int IdReserva, int IdSolicitud, int IdSala, string FechaDesdeReserva, string FechaHastaReserva, string HoraInicio, string HoraFin, string Observaciones, int IdUsuarioAuditoria)
        {
            return await reserva.GuardaReservaAsync(DatosGlobales.GestorSqlServer, IdReserva, IdSolicitud, IdSala, FechaDesdeReserva, FechaHastaReserva, HoraInicio, HoraFin, Observaciones, IdUsuarioAuditoria);
        }
        public async Task<Mensaje> EliminarReservaAsync(int IdReserva, int IdUsuarioAuditoria)
        {
            return await reserva.EliminarReservaAsync(DatosGlobales.GestorSqlServer, IdReserva, IdUsuarioAuditoria);
        }
        public async Task<Reserva> ObtenerReservaAsync(int IdReserva, int IdUsuarioAuditoria)
        {
            return await reserva.ObtenerReservaAsync(DatosGlobales.GestorSqlServer, IdReserva, IdUsuarioAuditoria);
        }
        public async Task<DataSet> DescargarReserva(int IdUsuarioAuditoria)
        {
            return await reserva.DescargarReserva(DatosGlobales.GestorSqlServer, IdUsuarioAuditoria);
        }
    }
}
