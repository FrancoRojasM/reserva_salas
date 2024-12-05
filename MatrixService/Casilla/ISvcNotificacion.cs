using Utilitarios;
using System;
using System.Threading.Tasks;
using Casilla;
using System.Data;

namespace MatrixService
{
	public interface ISvcNotificacion
	{
        
        Task<ListaNotificacion> ListarCategoriaMisNotificaciones(int IdUsuarioAuditoria, string BusquedaGeneral);
        Task<ListaNotificacion> ListarMisNotificaciones(int IdCatalogoCategoria, int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral);
        Task<ListaNotificacion> ListarNotificacionesGeneradas(int IdArea, int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral);
        Task<ListaNotificacion> ListarNotificacionAsync(int IdArea,int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral);
		Task<Notificacion> GuardaNotificacionAsync(int IdNotificacion, int IdAdministradoNotificada, int IdCatalogoCategoria, string AsuntoNotificacion, string MensajeNotificacion,string MensajeNotificacionHtml,
            DateTime? FechaHoraNotificacionEnviada, bool NotificacionEnviada, DateTime? FechaHoraRecepcionNotificacion, bool NotificacionRecibida,int IdAreaNotificador, int IdUsuarioAuditoria);
		Task<Mensaje> EliminarNotificacionAsync(int IdNotificacion, int IdUsuarioAuditoria);
        Task<Mensaje> EnviarNotificacion(int IdNotificacion, int IdUsuarioAuditoria);
        Task<Mensaje> RecibirNotificacion(int IdNotificacion, int IdUsuarioAuditoria);
        Task<Notificacion> ObtenerNotificacionAsync(int IdNotificacion, int IdUsuarioAuditoria); 
		Task<DataSet> DescargarNotificacion(int IdUsuarioAuditoria);
    }
}
