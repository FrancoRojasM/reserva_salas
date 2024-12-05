using Utilitarios;
using System;
using System.Threading.Tasks;
using Casilla;
namespace MatrixService
{
	public interface ISvcNotificacionArchivo
	{
		Task<ListaNotificacionArchivo> ListarNotificacionArchivoAsync(int IdNotificacion, int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral);
		Task<NotificacionArchivo> GuardaNotificacionArchivoAsync(int IdNotificacionArchivo, int IdNotificacion, int IdCatalogoTipoDocumento, string NumeroDocumento, string RutaArchivo, string ExtensionArchivo, decimal PesoArchivo, DateTime? FechaHoraLecturaArchivo, bool ArchivoLeido, int IdUsuarioAuditoria);
		Task<Mensaje> EliminarNotificacionArchivoAsync(int IdNotificacionArchivo, int IdUsuarioAuditoria);
        Task<Mensaje> RegistrarLecturaArchivoAdjuntoNotificado(int IdNotificacionArchivo, int IdUsuarioAuditoria);
        
        Task<NotificacionArchivo> ObtenerNotificacionArchivoAsync(int IdNotificacionArchivo, int IdUsuarioAuditoria);
	}
}
