using System;
using Utilitarios;
using System.Data;
using System.Threading.Tasks;
using General;
namespace GeneralLogic
{

	public class UnidadEjecutoraLogic
	{
		private UnidadEjecutora unidadejecutora;
		private ListaUnidadEjecutora lista;

		public UnidadEjecutoraLogic()
		{
			unidadejecutora = new UnidadEjecutora();
			lista = new ListaUnidadEjecutora();
		}
		public Mensaje EliminarUnidadEjecutora(int Gestor, int IdUnidadEjecutora, int IdUsuarioAuditoria)
		{
			Mensaje mensaje = new Mensaje();
			if (Gestor == DatosGlobales.GestorSqlServer)
			{
				System.Object[] ParamtrosOutPut = null;
				ParamtrosOutPut = ConexionSqlServer.GDatos.Ejecutar(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paEliminarUnidadEjecutora", IdUnidadEjecutora, IdUsuarioAuditoria, mensaje.DescripcionMensaje, mensaje.CodigoMensaje);
				if (Convert.ToInt32(ParamtrosOutPut[100].ToString()) > 0)
				{
					mensaje.CodigoMensaje = 1;
					mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA EN LA CAPA DE DATOS [EliminarUnidadEjecutora] VERIFICAR CONSOLA";
					mensaje.DescripcionMensajeSistema = ParamtrosOutPut[99].ToString();
					return mensaje;
				}
				mensaje.DescripcionMensaje = ParamtrosOutPut[0].ToString();
				mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[1].ToString());
			}
			return mensaje;
		}
		public async Task<Mensaje> EliminarUnidadEjecutoraAsync(int Gestor, int IdUnidadEjecutora, int IdUsuarioAuditoria)
		{
			Mensaje mensaje = new Mensaje();
			if (Gestor == DatosGlobales.GestorSqlServer)
			{
				System.Object[] ParamtrosOutPut = null;
				ParamtrosOutPut = await ConexionSqlServer.GDatos.EjecutarAsync(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paEliminarUnidadEjecutora", IdUnidadEjecutora, IdUsuarioAuditoria, mensaje.DescripcionMensaje, mensaje.CodigoMensaje);
				if (Convert.ToInt32(ParamtrosOutPut[100].ToString()) > 0)
				{
					mensaje.CodigoMensaje = 1;
					mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA EN LA CAPA DE DATOS [EliminarUnidadEjecutora] VERIFICAR CONSOLA";
					mensaje.DescripcionMensajeSistema = ParamtrosOutPut[99].ToString();
					return mensaje;
				}
				mensaje.DescripcionMensaje = ParamtrosOutPut[0].ToString();
				mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[1].ToString());
			}
			return mensaje;
		}
		public UnidadEjecutora ObtenerUnidadEjecutora(int Gestor, int IdUnidadEjecutora, int IdUsuarioAuditoria)
		{
			DataSet ds = new DataSet();
			DataTable dt = new DataTable();
			try
			{
				if (Gestor == DatosGlobales.GestorSqlServer)
				{
					ds = ConexionSqlServer.GDatos.TraerDataSet(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paObtenerUnidadEjecutora", IdUnidadEjecutora, IdUsuarioAuditoria);
					if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
					{
						unidadejecutora.mensaje.CodigoMensaje = 1;
						unidadejecutora.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ObtenerUnidadEjecutora], VERIFICAR CONSOLA";
						unidadejecutora.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();

						return unidadejecutora;
					}
					dt = ds.Tables[0].Copy();
					unidadejecutora.IdUnidadEjecutora = (int)dt.Rows[0]["IdUnidadEjecutora"];
					unidadejecutora.Descripcion = (string)dt.Rows[0]["Descripcion"];

				}
				return unidadejecutora;
			}
			catch (Exception ex)
			{
				unidadejecutora.mensaje.CodigoMensaje = 1;
				unidadejecutora.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ObtenerUnidadEjecutora], VERIFICAR CONSOLA";
				unidadejecutora.mensaje.DescripcionMensajeSistema = ex.Message;
				return unidadejecutora;
			}
			finally
			{
				ds.Dispose();
				ds.Clear();
				dt.Dispose();
				dt.Clear();
			}
		}

		public async Task<UnidadEjecutora> ObtenerUnidadEjecutoraAsync(int Gestor, int IdUnidadEjecutora, int IdUsuarioAuditoria)
		{
			DataSet ds = new DataSet();
			DataTable dt = new DataTable();
			try
			{
				if (Gestor == DatosGlobales.GestorSqlServer)
				{
					ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paObtenerUnidadEjecutora", IdUnidadEjecutora, IdUsuarioAuditoria);
					if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
					{
						unidadejecutora.mensaje.CodigoMensaje = 1;
						unidadejecutora.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ObtenerUnidadEjecutora], VERIFICAR CONSOLA";
						unidadejecutora.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();

						return unidadejecutora;
					}
					dt = ds.Tables[0].Copy();
					unidadejecutora.IdUnidadEjecutora = (int)dt.Rows[0]["IdUnidadEjecutora"];
					unidadejecutora.Descripcion = (string)dt.Rows[0]["Descripcion"];

				}
				return unidadejecutora;
			}
			catch (Exception ex)
			{
				unidadejecutora.mensaje.CodigoMensaje = 1;
				unidadejecutora.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ObtenerUnidadEjecutora], VERIFICAR CONSOLA";
				unidadejecutora.mensaje.DescripcionMensajeSistema = ex.Message;
				return unidadejecutora;
			}
			finally
			{
				ds.Dispose();
				ds.Clear();
				dt.Dispose();
				dt.Clear();
			}
		}
		public UnidadEjecutora GuardaUnidadEjecutora(int Gestor, int IdUnidadEjecutora, string Descripcion, int IdUsuarioAuditoria)
		{
			try
			{
				if (Gestor == DatosGlobales.GestorSqlServer)
				{
					System.Object[] ParamtrosOutPut = null;
					ParamtrosOutPut = ConexionSqlServer.GDatos.Ejecutar(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paGuardarUnidadEjecutora", IdUnidadEjecutora, Descripcion, IdUsuarioAuditoria, "", 0);
					if (Convert.ToInt32(ParamtrosOutPut[100].ToString()) > 0)
					{
						unidadejecutora.mensaje.CodigoMensaje = 1;
						unidadejecutora.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [GuardaUnidadEjecutora], VERIFICAR CONSOLA";
						unidadejecutora.mensaje.DescripcionMensajeSistema = ParamtrosOutPut[99].ToString();
						return unidadejecutora;
					}
					unidadejecutora.IdUnidadEjecutora = Convert.ToInt32(ParamtrosOutPut[0].ToString());
					unidadejecutora.mensaje.DescripcionMensaje = ParamtrosOutPut[1].ToString();
					unidadejecutora.mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[2].ToString());
				}
				return unidadejecutora;
			}
			catch (Exception ex)
			{
				unidadejecutora.mensaje.CodigoMensaje = 1;
				unidadejecutora.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [GuardaUnidadEjecutora], VERIFICAR CONSOLA";
				unidadejecutora.mensaje.DescripcionMensajeSistema = ex.Message;
				return unidadejecutora;
			}
		}

		public async Task<UnidadEjecutora> GuardaUnidadEjecutoraAsync(int Gestor, int IdUnidadEjecutora, string Descripcion, int IdUsuarioAuditoria)
		{
			try
			{
				if (Gestor == DatosGlobales.GestorSqlServer)
				{
					System.Object[] ParamtrosOutPut = null;
					ParamtrosOutPut = await ConexionSqlServer.GDatos.EjecutarAsync(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paGuardarUnidadEjecutora", IdUnidadEjecutora, Descripcion, IdUsuarioAuditoria, "", 0);
					if (Convert.ToInt32(ParamtrosOutPut[100].ToString()) > 0)
					{
						unidadejecutora.mensaje.CodigoMensaje = 1;
						unidadejecutora.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [GuardaUnidadEjecutora], VERIFICAR CONSOLA";
						unidadejecutora.mensaje.DescripcionMensajeSistema = ParamtrosOutPut[99].ToString();
						return unidadejecutora;
					}
					unidadejecutora.IdUnidadEjecutora = Convert.ToInt32(ParamtrosOutPut[0].ToString());
					unidadejecutora.mensaje.DescripcionMensaje = ParamtrosOutPut[1].ToString();
					unidadejecutora.mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[2].ToString());
				}
				return unidadejecutora;
			}
			catch (Exception ex)
			{
				unidadejecutora.mensaje.CodigoMensaje = 1;
				unidadejecutora.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [GuardaUnidadEjecutora], VERIFICAR CONSOLA";
				unidadejecutora.mensaje.DescripcionMensajeSistema = ex.Message;
				return unidadejecutora;
			}
		}

		public ListaUnidadEjecutora ListarUnidadEjecutora(int Gestor, int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
		{
			DataSet ds = new DataSet();
			DataTable dt = new DataTable();
			try
			{
				if (Gestor == DatosGlobales.GestorSqlServer)
				{
					ds = ConexionSqlServer.GDatos.TraerDataSet(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paListarUnidadEjecutora", IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
					if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
					{
						lista.mensaje.CodigoMensaje = 1;
						lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarUnidadEjecutora], VERIFICAR CONSOLA";
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
					unidadejecutora = new UnidadEjecutora();
					unidadejecutora.IdUnidadEjecutora = (int)row["IdUnidadEjecutora"];
					unidadejecutora.Descripcion = (string)row["Descripcion"];

					lista.lista.Add(unidadejecutora);
				}
				return lista;
			}
			catch (Exception ex)
			{
				lista.mensaje.CodigoMensaje = 1;
				lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarUnidadEjecutora], VERIFICAR CONSOLA";
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
		public async Task<ListaUnidadEjecutora> ListarUnidadEjecutoraAsync(int Gestor, int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
		{
			DataSet ds = new DataSet();
			DataTable dt = new DataTable();
			try
			{
				if (Gestor == DatosGlobales.GestorSqlServer)
				{
					ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paListarUnidadEjecutora", IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
					if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
					{
						lista.mensaje.CodigoMensaje = 1;
						lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarUnidadEjecutora], VERIFICAR CONSOLA";
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
					unidadejecutora = new UnidadEjecutora();
					unidadejecutora.IdUnidadEjecutora = (int)row["IdUnidadEjecutora"];
					unidadejecutora.Descripcion = (string)row["Descripcion"];

					lista.lista.Add(unidadejecutora);
				}
				return lista;
			}
			catch (Exception ex)
			{
				lista.mensaje.CodigoMensaje = 1;
				lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarUnidadEjecutora], VERIFICAR CONSOLA";
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
