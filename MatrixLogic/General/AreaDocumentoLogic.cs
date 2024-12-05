using System;
using Utilitarios;
using System.Data;
using System.Threading.Tasks;
using General;
namespace GeneralLogic
{

	public class AreaDocumentoLogic
	{
		private AreaDocumento areadocumento;
		private ListaAreaDocumento lista;

		public AreaDocumentoLogic()
		{
			areadocumento = new AreaDocumento();
			lista = new ListaAreaDocumento();
		}
		public Mensaje EliminarAreaDocumento(int Gestor, int IdAreaDocumento, int IdUsuarioAuditoria)
		{
			Mensaje mensaje = new Mensaje();
			if (Gestor == DatosGlobales.GestorSqlServer)
			{
				System.Object[] ParamtrosOutPut = null;
				ParamtrosOutPut = ConexionSqlServer.GDatos.Ejecutar(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paEliminarAreaDocumento", IdAreaDocumento, IdUsuarioAuditoria, mensaje.DescripcionMensaje, mensaje.CodigoMensaje);
				if (Convert.ToInt32(ParamtrosOutPut[100].ToString()) > 0)
				{
					mensaje.CodigoMensaje = 1;
					mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA EN LA CAPA DE DATOS [EliminarAreaDocumento] VERIFICAR CONSOLA";
					mensaje.DescripcionMensajeSistema = ParamtrosOutPut[99].ToString();
					return mensaje;
				}
				mensaje.DescripcionMensaje = ParamtrosOutPut[0].ToString();
				mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[1].ToString());
			}
			return mensaje;
		}
		public async Task<Mensaje> EliminarAreaDocumentoAsync(int Gestor, int IdAreaDocumento, int IdUsuarioAuditoria)
		{
			Mensaje mensaje = new Mensaje();
			if (Gestor == DatosGlobales.GestorSqlServer)
			{
				System.Object[] ParamtrosOutPut = null;
				ParamtrosOutPut = await ConexionSqlServer.GDatos.EjecutarAsync(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paEliminarAreaDocumento", IdAreaDocumento, IdUsuarioAuditoria, mensaje.DescripcionMensaje, mensaje.CodigoMensaje);
				if (Convert.ToInt32(ParamtrosOutPut[100].ToString()) > 0)
				{
					mensaje.CodigoMensaje = 1;
					mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA EN LA CAPA DE DATOS [EliminarAreaDocumento] VERIFICAR CONSOLA";
					mensaje.DescripcionMensajeSistema = ParamtrosOutPut[99].ToString();
					return mensaje;
				}
				mensaje.DescripcionMensaje = ParamtrosOutPut[0].ToString();
				mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[1].ToString());
			}
			return mensaje;
		}
		public AreaDocumento ObtenerAreaDocumento(int Gestor, int IdAreaDocumento, int IdUsuarioAuditoria)
		{
			DataSet ds = new DataSet();
			DataTable dt = new DataTable();
			try
			{
				if (Gestor == DatosGlobales.GestorSqlServer)
				{
					ds = ConexionSqlServer.GDatos.TraerDataSet(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paObtenerAreaDocumento", IdAreaDocumento, IdUsuarioAuditoria);
					if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
					{
						areadocumento.mensaje.CodigoMensaje = 1;
						areadocumento.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ObtenerAreaDocumento], VERIFICAR CONSOLA";
						areadocumento.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();

						return areadocumento;
					}
					dt = ds.Tables[0].Copy();
					areadocumento.IdAreaDocumento = (int)dt.Rows[0]["IdAreaDocumento"];
					areadocumento.area.IdArea = (int)dt.Rows[0]["IdArea"];
					areadocumento.area.NombreArea = (string)dt.Rows[0]["NombreArea"];
					areadocumento.catalogotipodocumento.IdCatalogo = (int)dt.Rows[0]["IdCatalogoTipoDocumento"];
					areadocumento.Activo = (bool)dt.Rows[0]["Activo"];

				}
				return areadocumento;
			}
			catch (Exception ex)
			{
				areadocumento.mensaje.CodigoMensaje = 1;
				areadocumento.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ObtenerAreaDocumento], VERIFICAR CONSOLA";
				areadocumento.mensaje.DescripcionMensajeSistema = ex.Message;
				return areadocumento;
			}
			finally
			{
				ds.Dispose();
				ds.Clear();
				dt.Dispose();
				dt.Clear();
			}
		}

		public async Task<AreaDocumento> ObtenerAreaDocumentoAsync(int Gestor, int IdAreaDocumento, int IdUsuarioAuditoria)
		{
			DataSet ds = new DataSet();
			DataTable dt = new DataTable();
			try
			{
				if (Gestor == DatosGlobales.GestorSqlServer)
				{
					ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paObtenerAreaDocumento", IdAreaDocumento, IdUsuarioAuditoria);
					if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
					{
						areadocumento.mensaje.CodigoMensaje = 1;
						areadocumento.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ObtenerAreaDocumento], VERIFICAR CONSOLA";
						areadocumento.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();

						return areadocumento;
					}
					dt = ds.Tables[0].Copy();
					areadocumento.IdAreaDocumento = (int)dt.Rows[0]["IdAreaDocumento"];
					areadocumento.area.IdArea = (int)dt.Rows[0]["IdArea"];
					areadocumento.catalogotipodocumento.IdCatalogo = (int)dt.Rows[0]["IdCatalogoTipoDocumento"];
					areadocumento.Activo = (bool)dt.Rows[0]["Activo"];

				}
				return areadocumento;
			}
			catch (Exception ex)
			{
				areadocumento.mensaje.CodigoMensaje = 1;
				areadocumento.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ObtenerAreaDocumento], VERIFICAR CONSOLA";
				areadocumento.mensaje.DescripcionMensajeSistema = ex.Message;
				return areadocumento;
			}
			finally
			{
				ds.Dispose();
				ds.Clear();
				dt.Dispose();
				dt.Clear();
			}
		}
		public AreaDocumento GuardaAreaDocumento(int Gestor, int IdAreaDocumento, int IdArea, int IdCatalogoTipoDocumento, bool Activo, int IdUsuarioAuditoria)
		{
			try
			{
				if (Gestor == DatosGlobales.GestorSqlServer)
				{
					System.Object[] ParamtrosOutPut = null;
					ParamtrosOutPut = ConexionSqlServer.GDatos.Ejecutar(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paGuardarAreaDocumento", IdAreaDocumento, IdArea, IdCatalogoTipoDocumento, Activo, IdUsuarioAuditoria, "", 0);
					if (Convert.ToInt32(ParamtrosOutPut[100].ToString()) > 0)
					{
						areadocumento.mensaje.CodigoMensaje = 1;
						areadocumento.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [GuardaAreaDocumento], VERIFICAR CONSOLA";
						areadocumento.mensaje.DescripcionMensajeSistema = ParamtrosOutPut[99].ToString();
						return areadocumento;
					}
					areadocumento.IdAreaDocumento = Convert.ToInt32(ParamtrosOutPut[0].ToString());
					areadocumento.mensaje.DescripcionMensaje = ParamtrosOutPut[1].ToString();
					areadocumento.mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[2].ToString());
				}
				return areadocumento;
			}
			catch (Exception ex)
			{
				areadocumento.mensaje.CodigoMensaje = 1;
				areadocumento.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [GuardaAreaDocumento], VERIFICAR CONSOLA";
				areadocumento.mensaje.DescripcionMensajeSistema = ex.Message;
				return areadocumento;
			}
		}

		public async Task<AreaDocumento> GuardaAreaDocumentoAsync(int Gestor, int IdAreaDocumento, int IdArea, int IdCatalogoTipoDocumento, bool Activo, int IdUsuarioAuditoria)
		{
			try
			{
				if (Gestor == DatosGlobales.GestorSqlServer)
				{
					System.Object[] ParamtrosOutPut = null;
					ParamtrosOutPut = await ConexionSqlServer.GDatos.EjecutarAsync(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paGuardarAreaDocumento", IdAreaDocumento, IdArea, IdCatalogoTipoDocumento, Activo, IdUsuarioAuditoria, "", 0);
					if (Convert.ToInt32(ParamtrosOutPut[100].ToString()) > 0)
					{
						areadocumento.mensaje.CodigoMensaje = 1;
						areadocumento.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [GuardaAreaDocumento], VERIFICAR CONSOLA";
						areadocumento.mensaje.DescripcionMensajeSistema = ParamtrosOutPut[99].ToString();
						return areadocumento;
					}
					areadocumento.IdAreaDocumento = Convert.ToInt32(ParamtrosOutPut[0].ToString());
					areadocumento.mensaje.DescripcionMensaje = ParamtrosOutPut[1].ToString();
					areadocumento.mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[2].ToString());
				}
				return areadocumento;
			}
			catch (Exception ex)
			{
				areadocumento.mensaje.CodigoMensaje = 1;
				areadocumento.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [GuardaAreaDocumento], VERIFICAR CONSOLA";
				areadocumento.mensaje.DescripcionMensajeSistema = ex.Message;
				return areadocumento;
			}
		}

		public ListaAreaDocumento ListarAreaDocumento(int Gestor, int IdArea, int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
		{
			DataSet ds = new DataSet();
			DataTable dt = new DataTable();
			try
			{
				if (Gestor == DatosGlobales.GestorSqlServer)
				{
					ds = ConexionSqlServer.GDatos.TraerDataSet(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paListarAreaDocumento", IdArea, IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
					if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
					{
						lista.mensaje.CodigoMensaje = 1;
						lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarAreaDocumento], VERIFICAR CONSOLA";
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
					areadocumento = new AreaDocumento();
					areadocumento.IdAreaDocumento = (int)row["IdAreaDocumento"];

					areadocumento.area.NombreArea = (string)row["NombreArea"];
					areadocumento.catalogotipodocumento.Descripcion = (string)row["CatalogoTipoDocumento"];
					areadocumento.Activo = (bool)row["Activo"];

					lista.lista.Add(areadocumento);
				}
				return lista;
			}
			catch (Exception ex)
			{
				lista.mensaje.CodigoMensaje = 1;
				lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarAreaDocumento], VERIFICAR CONSOLA";
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
		public async Task<ListaAreaDocumento> ListarAreaDocumentoAsync(int Gestor, int IdArea, int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
		{
			DataSet ds = new DataSet();
			DataTable dt = new DataTable();
			try
			{
				if (Gestor == DatosGlobales.GestorSqlServer)
				{
					ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paListarAreaDocumento", IdArea, IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
					if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
					{
						lista.mensaje.CodigoMensaje = 1;
						lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarAreaDocumento], VERIFICAR CONSOLA";
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
					areadocumento = new AreaDocumento();
					areadocumento.IdAreaDocumento = (int)row["IdAreaDocumento"];										
					areadocumento.catalogotipodocumento.Descripcion = (string)row["CatalogoTipoDocumento"];
					areadocumento.Activo = (bool)row["Activo"];

					lista.lista.Add(areadocumento);
				}
				return lista;
			}
			catch (Exception ex)
			{
				lista.mensaje.CodigoMensaje = 1;
				lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarAreaDocumento], VERIFICAR CONSOLA";
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
