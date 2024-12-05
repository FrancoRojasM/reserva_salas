using System;
using Utilitarios;
using System.Data;
using System.Threading.Tasks;
using Casilla;
namespace CasillaLogic
{

	public class NotificacionArchivoLogic
	{
		private NotificacionArchivo notificacionarchivo;
		private ListaNotificacionArchivo lista;

		public NotificacionArchivoLogic()
		{
			notificacionarchivo = new NotificacionArchivo();
			lista = new ListaNotificacionArchivo();
		}
        
        public async Task<Mensaje> RegistrarLecturaArchivoAdjuntoNotificado(int Gestor, int IdNotificacionArchivo, int IdUsuarioAuditoria)
        {
            Mensaje mensaje = new Mensaje();
            if (Gestor == DatosGlobales.GestorSqlServer)
            {
                System.Object[] ParamtrosOutPut = null;
                ParamtrosOutPut = await ConexionSqlServer.GDatos.EjecutarAsync(DatosGlobales.ListaConexiones.cnCasillaSql, "Casilla.paRegistrarLecturaArchivoAdjuntoNotificado", IdNotificacionArchivo, IdUsuarioAuditoria, mensaje.DescripcionMensaje, mensaje.CodigoMensaje);
                if (Convert.ToInt32(ParamtrosOutPut[100].ToString()) > 0)
                {
                    mensaje.CodigoMensaje = 1;
                    mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA EN LA CAPA DE DATOS [EliminarNotificacionArchivo] VERIFICAR CONSOLA";
                    mensaje.DescripcionMensajeSistema = ParamtrosOutPut[99].ToString();
                    return mensaje;
                }
                mensaje.DescripcionMensaje = ParamtrosOutPut[0].ToString();
                mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[1].ToString());
            }
            return mensaje;
        }
        public async Task<Mensaje> EliminarNotificacionArchivoAsync(int Gestor, int IdNotificacionArchivo, int IdUsuarioAuditoria)
		{
			Mensaje mensaje = new Mensaje();
			if (Gestor == DatosGlobales.GestorSqlServer)
			{
				System.Object[] ParamtrosOutPut = null;
				ParamtrosOutPut = await ConexionSqlServer.GDatos.EjecutarAsync(DatosGlobales.ListaConexiones.cnCasillaSql, "Casilla.paEliminarNotificacionArchivo", IdNotificacionArchivo, IdUsuarioAuditoria, mensaje.DescripcionMensaje, mensaje.CodigoMensaje);
				if (Convert.ToInt32(ParamtrosOutPut[100].ToString()) > 0)
				{
					mensaje.CodigoMensaje = 1;
					mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA EN LA CAPA DE DATOS [EliminarNotificacionArchivo] VERIFICAR CONSOLA";
					mensaje.DescripcionMensajeSistema = ParamtrosOutPut[99].ToString();
					return mensaje;
				}
				mensaje.DescripcionMensaje = ParamtrosOutPut[0].ToString();
				mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[1].ToString());
			}
			return mensaje;
		}
		public async Task<NotificacionArchivo> ObtenerNotificacionArchivoAsync(int Gestor, int IdNotificacionArchivo, int IdUsuarioAuditoria)
		{
			DataSet ds = new DataSet();
			DataTable dt = new DataTable();
			try
			{
				if (Gestor == DatosGlobales.GestorSqlServer)
				{
					ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnCasillaSql, "Casilla.paObtenerNotificacionArchivo", IdNotificacionArchivo, IdUsuarioAuditoria);
					if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
					{
						notificacionarchivo.mensaje.CodigoMensaje = 1;
						notificacionarchivo.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ObtenerNotificacionArchivo], VERIFICAR CONSOLA";
						notificacionarchivo.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();

						return notificacionarchivo;
					}
					dt = ds.Tables[0].Copy();
					notificacionarchivo.IdNotificacionArchivo = dt.Rows[0]["IdNotificacionArchivo"] is System.DBNull ? 0 : (int)dt.Rows[0]["IdNotificacionArchivo"];
					notificacionarchivo.notificacion.IdNotificacion = dt.Rows[0]["IdNotificacion"] is System.DBNull ? 0 : (int)dt.Rows[0]["IdNotificacion"];
					notificacionarchivo.catalogotipodocumento.IdCatalogo = dt.Rows[0]["IdCatalogoTipoDocumento"] is System.DBNull ? 0 : (int)dt.Rows[0]["IdCatalogoTipoDocumento"];
					notificacionarchivo.catalogotipodocumento.Descripcion = dt.Rows[0]["CatalogoTipoDocumento"] is System.DBNull ? "" : (string)dt.Rows[0]["CatalogoTipoDocumento"];
					notificacionarchivo.NumeroDocumento = dt.Rows[0]["NumeroDocumento"] is System.DBNull ? null : (string)dt.Rows[0]["NumeroDocumento"];
					notificacionarchivo.RutaArchivo = dt.Rows[0]["RutaArchivo"] is System.DBNull ? null : (string)dt.Rows[0]["RutaArchivo"];
					notificacionarchivo.ExtensionArchivo = dt.Rows[0]["ExtensionArchivo"] is System.DBNull ? null : (string)dt.Rows[0]["ExtensionArchivo"];
					notificacionarchivo.PesoArchivo = dt.Rows[0]["PesoArchivo"] is System.DBNull ? 0 : (int)dt.Rows[0]["PesoArchivo"];
					notificacionarchivo.FechaHoraLecturaArchivo = dt.Rows[0]["FechaHoraLecturaArchivo"] is System.DBNull ? null : (DateTime?)dt.Rows[0]["FechaHoraLecturaArchivo"];
					notificacionarchivo.ArchivoLeido = dt.Rows[0]["ArchivoLeido"] is System.DBNull ? false : (bool)dt.Rows[0]["ArchivoLeido"];

				}
				return notificacionarchivo;
			}
			catch (Exception ex)
			{
				notificacionarchivo.mensaje.CodigoMensaje = 1;
				notificacionarchivo.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ObtenerNotificacionArchivo], VERIFICAR CONSOLA";
				notificacionarchivo.mensaje.DescripcionMensajeSistema = ex.Message;
				return notificacionarchivo;
			}
			finally
			{
				ds.Dispose();
				ds.Clear();
				dt.Dispose();
				dt.Clear();
			}
		}
		public async Task<NotificacionArchivo> GuardaNotificacionArchivoAsync(int Gestor, int IdNotificacionArchivo, int IdNotificacion, int IdCatalogoTipoDocumento, string NumeroDocumento, string RutaArchivo, string ExtensionArchivo, decimal PesoArchivo, DateTime? FechaHoraLecturaArchivo, bool ArchivoLeido, int IdUsuarioAuditoria)
		{
			try
			{
				if (Gestor == DatosGlobales.GestorSqlServer)
				{
					System.Object[] ParamtrosOutPut = null;
					ParamtrosOutPut = await ConexionSqlServer.GDatos.EjecutarAsync(DatosGlobales.ListaConexiones.cnCasillaSql, "Casilla.paGuardarNotificacionArchivo", IdNotificacionArchivo, IdNotificacion, IdCatalogoTipoDocumento, NumeroDocumento, RutaArchivo, ExtensionArchivo, PesoArchivo, FechaHoraLecturaArchivo, ArchivoLeido, IdUsuarioAuditoria, "", 0);
					if (Convert.ToInt32(ParamtrosOutPut[100].ToString()) > 0)
					{
						notificacionarchivo.mensaje.CodigoMensaje = 1;
						notificacionarchivo.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [GuardaNotificacionArchivo], VERIFICAR CONSOLA";
						notificacionarchivo.mensaje.DescripcionMensajeSistema = ParamtrosOutPut[99].ToString();
						return notificacionarchivo;
					}
					notificacionarchivo.IdNotificacionArchivo = Convert.ToInt32(ParamtrosOutPut[0].ToString());
					notificacionarchivo.mensaje.DescripcionMensaje = ParamtrosOutPut[1].ToString();
					notificacionarchivo.mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[2].ToString());
				}
				return notificacionarchivo;
			}
			catch (Exception ex)
			{
				notificacionarchivo.mensaje.CodigoMensaje = 1;
				notificacionarchivo.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [GuardaNotificacionArchivo], VERIFICAR CONSOLA";
				notificacionarchivo.mensaje.DescripcionMensajeSistema = ex.Message;
				return notificacionarchivo;
			}
		}

		public async Task<ListaNotificacionArchivo> ListarNotificacionArchivoAsync(int Gestor, int IdNotificacion, int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
		{
			DataSet ds = new DataSet();
			DataTable dt = new DataTable();
			try
			{
				if (Gestor == DatosGlobales.GestorSqlServer)
				{
					ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnCasillaSql, "Casilla.paListarNotificacionArchivo", IdNotificacion, IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
					if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
					{
						lista.mensaje.CodigoMensaje = 1;
						lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarNotificacionArchivo], VERIFICAR CONSOLA";
						lista.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();
						return lista;
					}
					dt = ds.Tables[0].Copy();
					DataTable dtParametros = null;
					dtParametros = ds.Tables[1].Copy();
					lista.paginacion.TotalRegistros = Convert.ToInt32(dtParametros.Rows[0][0]);
				}
				foreach (DataRow row in dt.Rows)
				{
					notificacionarchivo = new NotificacionArchivo();
					notificacionarchivo.IdNotificacionArchivo = row["IdNotificacionArchivo"] is System.DBNull ? 0 : (int)row["IdNotificacionArchivo"];

					notificacionarchivo.notificacion.IdNotificacion = row["IdNotificacion"] is System.DBNull ? 0 : (int)row["IdNotificacion"];
					notificacionarchivo.catalogotipodocumento.Descripcion = row["CatalogoTipoDocumento"] is System.DBNull ? null : (string)row["CatalogoTipoDocumento"];
					notificacionarchivo.NumeroDocumento = row["NumeroDocumento"] is System.DBNull ? null : (string)row["NumeroDocumento"];
					notificacionarchivo.RutaArchivo = row["RutaArchivo"] is System.DBNull ? null : (string)row["RutaArchivo"];
					notificacionarchivo.ExtensionArchivo = row["ExtensionArchivo"] is System.DBNull ? null : (string)row["ExtensionArchivo"];
					notificacionarchivo.PesoArchivo = row["PesoArchivo"] is System.DBNull ? 0 : (decimal)row["PesoArchivo"];
					notificacionarchivo.FechaHoraLecturaArchivo = row["FechaHoraLecturaArchivo"] is System.DBNull ? null : (DateTime?)row["FechaHoraLecturaArchivo"];
					notificacionarchivo.ArchivoLeido = row["ArchivoLeido"] is System.DBNull ? false : (bool)row["ArchivoLeido"];

					lista.lista.Add(notificacionarchivo);
				}
				return lista;
			}
			catch (Exception ex)
			{
				lista.mensaje.CodigoMensaje = 1;
				lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarNotificacionArchivo], VERIFICAR CONSOLA";
				lista.mensaje.DescripcionMensajeSistema = ex.Message;
				return lista;
			}
			finally
			{
				ds.Dispose();
				ds.Clear();
				dt.Dispose();
				dt.Clear();
			}
		}


	}
}
