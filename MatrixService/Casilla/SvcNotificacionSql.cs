
using Utilitarios;
using System;
using Casilla;
using System.Threading.Tasks;
using System.Data;

namespace MatrixService

{
	public class SvcNotificacionSql : ISvcNotificacion
	{
		private CasillaLogic.NotificacionLogic notificacion;
		public SvcNotificacionSql()
		{
			notificacion = new CasillaLogic.NotificacionLogic();
		}        
        public async Task<ListaNotificacion> ListarCategoriaMisNotificaciones(int IdUsuarioAuditoria,string BusquedaGeneral)
        {
            return await notificacion.ListarCategoriaMisNotificaciones(DatosGlobales.GestorSqlServer, IdUsuarioAuditoria, BusquedaGeneral);
        }
        public async Task<ListaNotificacion> ListarMisNotificaciones(int IdCatalogoCategoria ,int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
		{
			return await notificacion.ListarMisNotificaciones(DatosGlobales.GestorSqlServer, IdCatalogoCategoria, IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
		}
        
        public async Task<ListaNotificacion> ListarNotificacionesGeneradas(int IdArea,int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
        {
            return await notificacion.ListarNotificacionesGeneradas(DatosGlobales.GestorSqlServer, IdArea, IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
        }
        public async Task<ListaNotificacion> ListarNotificacionAsync(int IdArea,int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
        {
            return await notificacion.ListarNotificacionAsync(DatosGlobales.GestorSqlServer, IdArea, IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
        }
        public async Task<Notificacion> GuardaNotificacionAsync(int IdNotificacion, int IdAdministradoNotificada, int IdCatalogoCategoria, string AsuntoNotificacion, string MensajeNotificacion,
            string MensajeNotificacionHtml, DateTime? FechaHoraNotificacionEnviada, bool NotificacionEnviada, DateTime? FechaHoraRecepcionNotificacion, bool NotificacionRecibida, int IdAreaNotificador, int IdUsuarioAuditoria)
		{
			return await notificacion.GuardaNotificacionAsync(DatosGlobales.GestorSqlServer, IdNotificacion, IdAdministradoNotificada, IdCatalogoCategoria, AsuntoNotificacion, MensajeNotificacion,
                MensajeNotificacionHtml, FechaHoraNotificacionEnviada, NotificacionEnviada, FechaHoraRecepcionNotificacion, NotificacionRecibida,  IdAreaNotificador, IdUsuarioAuditoria);
		}
         public async Task<Mensaje> RecibirNotificacion(int IdNotificacion, int IdUsuarioAuditoria)
        {
            return await notificacion.RecibirNotificacion(DatosGlobales.GestorSqlServer, IdNotificacion, IdUsuarioAuditoria);
        }
        public async Task<Mensaje> EnviarNotificacion(int IdNotificacion, int IdUsuarioAuditoria)
        {
            return await notificacion.EnviarNotificacion(DatosGlobales.GestorSqlServer, IdNotificacion, IdUsuarioAuditoria);
        }
        public async Task<Mensaje> EliminarNotificacionAsync(int IdNotificacion, int IdUsuarioAuditoria)
		{
			return await notificacion.EliminarNotificacionAsync(DatosGlobales.GestorSqlServer, IdNotificacion, IdUsuarioAuditoria);
		}
		public async Task<Notificacion> ObtenerNotificacionAsync(int IdNotificacion, int IdUsuarioAuditoria)
		{
			return await notificacion.ObtenerNotificacionAsync(DatosGlobales.GestorSqlServer, IdNotificacion, IdUsuarioAuditoria);
		}
        public async Task<DataSet> DescargarNotificacion(int IdUsuarioAuditoria)
        {
            return await notificacion.DescargarNotificacion(DatosGlobales.GestorSqlServer, IdUsuarioAuditoria);
        }
    }
}
