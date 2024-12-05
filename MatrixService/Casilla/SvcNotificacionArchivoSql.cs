
using Utilitarios;
using System;
using Casilla;
using System.Threading.Tasks;
namespace MatrixService

{
	public class SvcNotificacionArchivoSql : ISvcNotificacionArchivo
	{
		private CasillaLogic.NotificacionArchivoLogic notificacionarchivo;
		public SvcNotificacionArchivoSql()
		{
			notificacionarchivo = new CasillaLogic.NotificacionArchivoLogic();
		}
		public async Task<ListaNotificacionArchivo> ListarNotificacionArchivoAsync(int IdNotificacion, int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
		{
			return await notificacionarchivo.ListarNotificacionArchivoAsync(DatosGlobales.GestorSqlServer, IdNotificacion, IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
		}
		public async Task<NotificacionArchivo> GuardaNotificacionArchivoAsync(int IdNotificacionArchivo, int IdNotificacion, int IdCatalogoTipoDocumento, string NumeroDocumento, string RutaArchivo, string ExtensionArchivo, decimal PesoArchivo, DateTime? FechaHoraLecturaArchivo, bool ArchivoLeido, int IdUsuarioAuditoria)
		{
			return await notificacionarchivo.GuardaNotificacionArchivoAsync(DatosGlobales.GestorSqlServer, IdNotificacionArchivo, IdNotificacion, IdCatalogoTipoDocumento, NumeroDocumento, RutaArchivo, ExtensionArchivo, PesoArchivo, FechaHoraLecturaArchivo, ArchivoLeido, IdUsuarioAuditoria);
		}
        
        public async Task<Mensaje> RegistrarLecturaArchivoAdjuntoNotificado(int IdNotificacionArchivo, int IdUsuarioAuditoria)
        {
            return await notificacionarchivo.RegistrarLecturaArchivoAdjuntoNotificado(DatosGlobales.GestorSqlServer, IdNotificacionArchivo, IdUsuarioAuditoria);
        }
        public async Task<Mensaje> EliminarNotificacionArchivoAsync(int IdNotificacionArchivo, int IdUsuarioAuditoria)
		{
			return await notificacionarchivo.EliminarNotificacionArchivoAsync(DatosGlobales.GestorSqlServer, IdNotificacionArchivo, IdUsuarioAuditoria);
		}
		public async Task<NotificacionArchivo> ObtenerNotificacionArchivoAsync(int IdNotificacionArchivo, int IdUsuarioAuditoria)
		{
			return await notificacionarchivo.ObtenerNotificacionArchivoAsync(DatosGlobales.GestorSqlServer, IdNotificacionArchivo, IdUsuarioAuditoria);
		}
	}
}
