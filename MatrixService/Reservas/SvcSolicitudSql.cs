
using Utilitarios;
using System;
using Reservas;
using System.Threading.Tasks;
using System.Data;
namespace MatrixService

{
    public class SvcSolicitudSql : ISvcSolicitud
    {
        private ReservasLogic.SolicitudLogic solicitud;
        public SvcSolicitudSql()
        {
            solicitud = new ReservasLogic.SolicitudLogic();
        }
        public async Task<ListaSolicitud> ListarSolicitudAsync(int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
        {
            return await solicitud.ListarSolicitudAsync(DatosGlobales.GestorSqlServer, IdUsuarioAuditoria, CampoOrdenado=null, TipoOrdenacion=null, NumeroPagina=1, DimensionPagina=10, BusquedaGeneral=null);
        }
        public async Task<Solicitud> GuardaSolicitudAsync(int IdSolicitud, int IdCatalogoConsejoRegional, int IdCatalogoSecretaria, int IdCatalogoComite, string NombreEvento, int NumeroParticipantes, string FechaDesde, string FechaHasta, int NumeroDias, string HoraInicio, string HoraFin, int NumeroAuditorios, string ResponsableEvento, string TelefonoContacto, string CorreoContacto, string Observaciones, int IdSalaAsignada, int IdCatalogoEstadoSolicitud, int IdUsuarioAuditoria)
        {
            return await solicitud.GuardaSolicitudAsync(DatosGlobales.GestorSqlServer, IdSolicitud, IdCatalogoConsejoRegional, IdCatalogoSecretaria, IdCatalogoComite, NombreEvento, NumeroParticipantes, FechaDesde, FechaHasta, NumeroDias, HoraInicio, HoraFin, NumeroAuditorios, ResponsableEvento, TelefonoContacto, CorreoContacto, Observaciones, IdSalaAsignada, IdCatalogoEstadoSolicitud, IdUsuarioAuditoria);
        }
        public async Task<Mensaje> EliminarSolicitudAsync(int IdSolicitud, int IdUsuarioAuditoria)
        {
            return await solicitud.EliminarSolicitudAsync(DatosGlobales.GestorSqlServer, IdSolicitud, IdUsuarioAuditoria);
        }
        public async Task<Solicitud> ObtenerSolicitudAsync(int IdSolicitud, int IdUsuarioAuditoria)
        {
            return await solicitud.ObtenerSolicitudAsync(DatosGlobales.GestorSqlServer, IdSolicitud, IdUsuarioAuditoria);
        }
        public async Task<DataSet> DescargarSolicitud(int IdUsuarioAuditoria)
        {
            return await solicitud.DescargarSolicitud(DatosGlobales.GestorSqlServer, IdUsuarioAuditoria);
        }

        public async Task<ListaSolicitud> ListarSolicitudesAsync(int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion)
        {
            return await solicitud.ListarSolicitudesAsync(DatosGlobales.GestorSqlServer, IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion);
        }
    }
}
