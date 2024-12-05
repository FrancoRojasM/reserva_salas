		using System;
		using Utilitarios;
		using System.Data;
		using System.Threading.Tasks;
		using Seguridad;
namespace SeguridadLogic
{

	public class UsuarioTokenLogic
	{
		private UsuarioToken usuariotoken;
		private ListaUsuarioToken lista;

		public UsuarioTokenLogic()
		{
			usuariotoken = new UsuarioToken();
			lista = new ListaUsuarioToken();
		}
		public Mensaje EliminarUsuarioToken(int Gestor, int IdUsuarioToken, int IdUsuarioAuditoria)
		{
			Mensaje mensaje = new Mensaje();
			if (Gestor == DatosGlobales.GestorSqlServer)
			{
				System.Object[] ParamtrosOutPut = null;
				ParamtrosOutPut = ConexionSqlServer.GDatos.Ejecutar(DatosGlobales.ListaConexiones.cnSeguridadSql, "Seguridad.paEliminarUsuarioToken", IdUsuarioToken, IdUsuarioAuditoria, mensaje.DescripcionMensaje, mensaje.CodigoMensaje);
				if (Convert.ToInt32(ParamtrosOutPut[100].ToString()) > 0)
				{
					mensaje.CodigoMensaje = 1;
					mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA EN LA CAPA DE DATOS [EliminarUsuarioToken] VERIFICAR CONSOLA";
					mensaje.DescripcionMensajeSistema = ParamtrosOutPut[99].ToString();
					return mensaje;
				}
				mensaje.DescripcionMensaje = ParamtrosOutPut[0].ToString();
				mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[1].ToString());
			}
			return mensaje;
		}
		public async Task<Mensaje> EliminarUsuarioTokenAsync(int Gestor, int IdUsuarioToken, int IdUsuarioAuditoria)
		{
			Mensaje mensaje = new Mensaje();
			if (Gestor == DatosGlobales.GestorSqlServer)
			{
				System.Object[] ParamtrosOutPut = null;
				ParamtrosOutPut = await ConexionSqlServer.GDatos.EjecutarAsync(DatosGlobales.ListaConexiones.cnSeguridadSql, "Seguridad.paEliminarUsuarioToken", IdUsuarioToken, IdUsuarioAuditoria, mensaje.DescripcionMensaje, mensaje.CodigoMensaje);
				if (Convert.ToInt32(ParamtrosOutPut[100].ToString()) > 0)
				{
					mensaje.CodigoMensaje = 1;
					mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA EN LA CAPA DE DATOS [EliminarUsuarioToken] VERIFICAR CONSOLA";
					mensaje.DescripcionMensajeSistema = ParamtrosOutPut[99].ToString();
					return mensaje;
				}
				mensaje.DescripcionMensaje = ParamtrosOutPut[0].ToString();
				mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[1].ToString());
			}
			return mensaje;
		}
		public UsuarioToken ObtenerUsuarioToken(int Gestor, int IdUsuarioToken, int IdUsuarioAuditoria)
		{
			DataSet ds = new DataSet();
			DataTable dt = new DataTable();
			try
			{
				if (Gestor == DatosGlobales.GestorSqlServer)
				{
					ds = ConexionSqlServer.GDatos.TraerDataSet(DatosGlobales.ListaConexiones.cnSeguridadSql, "Seguridad.paObtenerUsuarioToken", IdUsuarioToken, IdUsuarioAuditoria);
					if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
					{
						usuariotoken.mensaje.CodigoMensaje = 1;
						usuariotoken.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ObtenerUsuarioToken], VERIFICAR CONSOLA";
						usuariotoken.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();

						return usuariotoken;
					}
					dt = ds.Tables[0].Copy();
					usuariotoken.IdUsuarioToken = (int)dt.Rows[0]["IdUsuarioToken"];
					usuariotoken.usuario.IdUsuario = (int)dt.Rows[0]["IdUsuario"];

					usuariotoken.Issuer = (string)dt.Rows[0]["Issuer"];
					usuariotoken.Audience = (string)dt.Rows[0]["Audience"];

					usuariotoken.HostDeUso = (string)dt.Rows[0]["HostDeUso"];
					usuariotoken.Token = (string)dt.Rows[0]["Token"];
					usuariotoken.FechaCreacion = (string)dt.Rows[0]["FechaCreacion"];
					usuariotoken.FechaVencimiento = (string)dt.Rows[0]["FechaVencimiento"];
					usuariotoken.Activo = (bool)dt.Rows[0]["Activo"];

				}
				return usuariotoken;
			}
			catch (Exception ex)
			{
				usuariotoken.mensaje.CodigoMensaje = 1;
				usuariotoken.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ObtenerUsuarioToken], VERIFICAR CONSOLA";
				usuariotoken.mensaje.DescripcionMensajeSistema = ex.Message;
				return usuariotoken;
			}
			finally
			{
				ds.Dispose();
				ds.Clear();
				dt.Dispose();
				dt.Clear();
			}
		}

		public async Task<UsuarioToken> ObtenerUsuarioTokenAsync(int Gestor, int IdUsuarioToken, int IdUsuarioAuditoria)
		{
			DataSet ds = new DataSet();
			DataTable dt = new DataTable();
			try
			{
				if (Gestor == DatosGlobales.GestorSqlServer)
				{
					ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnSeguridadSql, "Seguridad.paObtenerUsuarioToken", IdUsuarioToken, IdUsuarioAuditoria);
					if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
					{
						usuariotoken.mensaje.CodigoMensaje = 1;
						usuariotoken.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ObtenerUsuarioToken], VERIFICAR CONSOLA";
						usuariotoken.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();

						return usuariotoken;
					}
					dt = ds.Tables[0].Copy();
					usuariotoken.IdUsuarioToken = (int)dt.Rows[0]["IdUsuarioToken"];
					usuariotoken.usuario.IdUsuario = (int)dt.Rows[0]["IdUsuario"];
					usuariotoken.Issuer = (string)dt.Rows[0]["Issuer"];
					usuariotoken.Audience = (string)dt.Rows[0]["Audience"];
					usuariotoken.HostDeUso = (string)dt.Rows[0]["HostDeUso"];
					usuariotoken.Token = (string)dt.Rows[0]["Token"];
					usuariotoken.FechaCreacion = (string)dt.Rows[0]["FechaCreacion"];
					usuariotoken.FechaVencimiento = (string)dt.Rows[0]["FechaVencimiento"];
					usuariotoken.Activo = (bool)dt.Rows[0]["Activo"];

				}
				return usuariotoken;
			}
			catch (Exception ex)
			{
				usuariotoken.mensaje.CodigoMensaje = 1;
				usuariotoken.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ObtenerUsuarioToken], VERIFICAR CONSOLA";
				usuariotoken.mensaje.DescripcionMensajeSistema = ex.Message;
				return usuariotoken;
			}
			finally
			{
				ds.Dispose();
				ds.Clear();
				dt.Dispose();
				dt.Clear();
			}
		}

		public UsuarioToken GuardaUsuarioToken(int Gestor, int IdUsuarioToken, int IdUsuario, string Issuer, string Audience, string HostDeUso, string Token, string FechaCreacion, string FechaVencimiento, bool Activo, int IdUsuarioAuditoria)
		{
			try
			{
				if (Gestor == DatosGlobales.GestorSqlServer)
				{
					System.Object[] ParamtrosOutPut = null;
					ParamtrosOutPut = ConexionSqlServer.GDatos.Ejecutar(DatosGlobales.ListaConexiones.cnSeguridadSql, "Seguridad.paGuardarUsuarioToken", IdUsuarioToken, IdUsuario, Issuer, Audience, HostDeUso, Token, FechaCreacion, FechaVencimiento, Activo, IdUsuarioAuditoria, "", 0);
					if (Convert.ToInt32(ParamtrosOutPut[100].ToString()) > 0)
					{
						usuariotoken.mensaje.CodigoMensaje = 1;
						usuariotoken.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [GuardaUsuarioToken], VERIFICAR CONSOLA";
						usuariotoken.mensaje.DescripcionMensajeSistema = ParamtrosOutPut[99].ToString();
						return usuariotoken;
					}
					usuariotoken.IdUsuarioToken = Convert.ToInt32(ParamtrosOutPut[0].ToString());
					usuariotoken.mensaje.DescripcionMensaje = ParamtrosOutPut[1].ToString();
					usuariotoken.mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[2].ToString());
				}
				return usuariotoken;
			}
			catch (Exception ex)
			{
				usuariotoken.mensaje.CodigoMensaje = 1;
				usuariotoken.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [GuardaUsuarioToken], VERIFICAR CONSOLA";
				usuariotoken.mensaje.DescripcionMensajeSistema = ex.Message;
				return usuariotoken;
			}
		}
		public async Task<UsuarioToken> ActualizarFechaUltimoAccesoToken(int Gestor, string Token, int IdUsuarioAuditoria)
		{
			try
			{
				if (Gestor == DatosGlobales.GestorSqlServer)
				{
					System.Object[] ParamtrosOutPut = null;
					ParamtrosOutPut = await ConexionSqlServer.GDatos.EjecutarAsync(DatosGlobales.ListaConexiones.cnSeguridadSql, "Seguridad.paActualizarFechaUltimoAccesoToken", 0, Token, IdUsuarioAuditoria, "", 0);
					if (Convert.ToInt32(ParamtrosOutPut[100].ToString()) > 0)
					{
						usuariotoken.mensaje.CodigoMensaje = 1;
						usuariotoken.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [GuardaUsuarioToken], VERIFICAR CONSOLA";
						usuariotoken.mensaje.DescripcionMensajeSistema = ParamtrosOutPut[99].ToString();
						return usuariotoken;
					}
					usuariotoken.IdUsuarioToken = Convert.ToInt32(ParamtrosOutPut[0].ToString());
					usuariotoken.mensaje.DescripcionMensaje = ParamtrosOutPut[1].ToString();
					usuariotoken.mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[2].ToString());
				}
				return usuariotoken;
			}
			catch (Exception ex)
			{
				usuariotoken.mensaje.CodigoMensaje = 1;
				usuariotoken.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [GuardaUsuarioToken], VERIFICAR CONSOLA";
				usuariotoken.mensaje.DescripcionMensajeSistema = ex.Message;
				return usuariotoken;
			}
		}
		public async Task<UsuarioToken> GuardaUsuarioTokenAsync(int Gestor, int IdUsuarioToken, int IdUsuario, string Issuer, string Audience, string HostDeUso, string Token, string FechaCreacion, string FechaVencimiento, bool Activo, int IdUsuarioAuditoria)
		{
			try
			{
				if (Gestor == DatosGlobales.GestorSqlServer)
				{
					System.Object[] ParamtrosOutPut = null;
					ParamtrosOutPut = await ConexionSqlServer.GDatos.EjecutarAsync(DatosGlobales.ListaConexiones.cnSeguridadSql, "Seguridad.paGuardarUsuarioToken", IdUsuarioToken, IdUsuario, Issuer, Audience, HostDeUso, Token, FechaCreacion, FechaVencimiento, Activo, IdUsuarioAuditoria, "", 0);
					if (Convert.ToInt32(ParamtrosOutPut[100].ToString()) > 0)
					{
						usuariotoken.mensaje.CodigoMensaje = 1;
						usuariotoken.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [GuardaUsuarioToken], VERIFICAR CONSOLA";
						usuariotoken.mensaje.DescripcionMensajeSistema = ParamtrosOutPut[99].ToString();
						return usuariotoken;
					}
					usuariotoken.IdUsuarioToken = Convert.ToInt32(ParamtrosOutPut[0].ToString());
					usuariotoken.mensaje.DescripcionMensaje = ParamtrosOutPut[1].ToString();
					usuariotoken.mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[2].ToString());
				}
				return usuariotoken;
			}
			catch (Exception ex)
			{
				usuariotoken.mensaje.CodigoMensaje = 1;
				usuariotoken.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [GuardaUsuarioToken], VERIFICAR CONSOLA";
				usuariotoken.mensaje.DescripcionMensajeSistema = ex.Message;
				return usuariotoken;
			}
		}

		public ListaUsuarioToken ListarUsuarioToken(int Gestor, int IdUsuario, int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
		{
			DataSet ds = new DataSet();
			DataTable dt = new DataTable();
			try
			{
				if (Gestor == DatosGlobales.GestorSqlServer)
				{
					ds = ConexionSqlServer.GDatos.TraerDataSet(DatosGlobales.ListaConexiones.cnSeguridadSql, "Seguridad.paListarUsuarioToken", IdUsuario, IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
					if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
					{
						lista.mensaje.CodigoMensaje = 1;
						lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarUsuarioToken], VERIFICAR CONSOLA";
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
					usuariotoken = new UsuarioToken();
					usuariotoken.IdUsuarioToken = (int)row["IdUsuarioToken"];
					usuariotoken.usuario.IdUsuario = (int)row["IdUsuario"];

					usuariotoken.Issuer = (string)row["Issuer"];
					usuariotoken.Audience = (string)row["Audience"];

					usuariotoken.HostDeUso = (string)row["HostDeUso"];
					usuariotoken.Token = (string)row["Token"];
					usuariotoken.FechaCreacion = (string)row["FechaCreacion"];
					usuariotoken.FechaVencimiento = (string)row["FechaVencimiento"];
					usuariotoken.Activo = (bool)row["Activo"];

					lista.lista.Add(usuariotoken);
				}
				return lista;
			}
			catch (Exception ex)
			{
				lista.mensaje.CodigoMensaje = 1;
				lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarUsuarioToken], VERIFICAR CONSOLA";
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
		public async Task<ListaUsuarioToken> ListarUsuarioTokenAsync(int Gestor, int IdUsuario, int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
		{
			DataSet ds = new DataSet();
			DataTable dt = new DataTable();
			try
			{
				if (Gestor == DatosGlobales.GestorSqlServer)
				{
					ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnSeguridadSql, "Seguridad.paListarUsuarioToken", IdUsuario, IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
					if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
					{
						lista.mensaje.CodigoMensaje = 1;
						lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarUsuarioToken], VERIFICAR CONSOLA";
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
					usuariotoken = new UsuarioToken();
					usuariotoken.IdUsuarioToken = (int)row["IdUsuarioToken"];
					usuariotoken.usuario.IdUsuario = (int)row["IdUsuario"];
					usuariotoken.Issuer = (string)row["Issuer"];
					usuariotoken.Audience = (string)row["Audience"];
					usuariotoken.HostDeUso = (string)row["HostDeUso"];
					usuariotoken.Token = (string)row["Token"];
					usuariotoken.FechaCreacion = (string)row["FechaCreacion"];
					usuariotoken.FechaVencimiento = (string)row["FechaVencimiento"];
					usuariotoken.Activo = (bool)row["Activo"];

					lista.lista.Add(usuariotoken);
				}
				return lista;
			}
			catch (Exception ex)
			{
				lista.mensaje.CodigoMensaje = 1;
				lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarUsuarioToken], VERIFICAR CONSOLA";
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
		