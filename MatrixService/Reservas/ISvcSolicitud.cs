using Utilitarios;
using System;
using System.Threading.Tasks;
using Reservas;
using System.Data;
namespace MatrixService
{
    public interface ISvcSolicitud
    {
        Task<ListaSolicitud> ListarSolicitudAsync(int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral);

        Task<ListaSolicitud> ListarSolicitudesAsync(int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion);


        Task<Solicitud> GuardaSolicitudAsync(int IdSolicitud, int IdCatalogoConsejoRegional, int IdCatalogoSecretaria, int IdCatalogoComite, string NombreEvento, int NumeroParticipantes, string FechaDesde, string FechaHasta, int NumeroDias, string HoraInicio, string HoraFin, int NumeroAuditorios, string ResponsableEvento, string TelefonoContacto, string CorreoContacto, string Observaciones, int IdSalaAsignada, int IdCatalogoEstadoSolicitud, int IdUsuarioAuditoria);

        Task<Mensaje> EliminarSolicitudAsync(int IdSolicitud, int IdUsuarioAuditoria);
        Task<Solicitud> ObtenerSolicitudAsync(int IdSolicitud, int IdUsuarioAuditoria);
        Task<DataSet> DescargarSolicitud(int IdUsuarioAuditoria);
    }
}
