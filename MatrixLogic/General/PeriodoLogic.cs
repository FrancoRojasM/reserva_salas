using System;
using Utilitarios;
using System.Data;
using General;
namespace GeneralLogic
{

	public class PeriodoLogic
	{
		private Periodo periodo;
		private ListaPeriodo lista;

		public PeriodoLogic()
		{
			periodo = new Periodo();
			lista = new ListaPeriodo();
		}
		public Mensaje EliminarPeriodo(int Gestor, int IdPeriodo, int IdUsuarioAuditoria)
		{
			Mensaje mensaje = new Mensaje();
			if (Gestor == DatosGlobales.GestorSqlServer)
			{
				System.Object[] ParamtrosOutPut = null; 
				ParamtrosOutPut = ConexionSqlServer.GDatos.Ejecutar(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paEliminarPeriodo", IdPeriodo, IdUsuarioAuditoria, mensaje.DescripcionMensaje, mensaje.CodigoMensaje);
				if (Convert.ToInt32(ParamtrosOutPut[100].ToString()) > 0)
				{
					mensaje.CodigoMensaje = 1;
					mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA EN LA CAPA DE DATOS [EliminarPeriodo] VERIFICAR CONSOLA";
					mensaje.DescripcionMensajeSistema = ParamtrosOutPut[99].ToString();
					return mensaje;
				}
				mensaje.DescripcionMensaje = ParamtrosOutPut[0].ToString();
				mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[1].ToString());
			}
			return mensaje;
		}
		public Periodo ObtenerPeriodo(int Gestor, int IdPeriodo, int IdUsuarioAuditoria)
		{
			DataSet ds = new DataSet();
			DataTable dt = new DataTable();
			try
			{
				if (Gestor == DatosGlobales.GestorSqlServer)
				{
					 ds = ConexionSqlServer.GDatos.TraerDataSet(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paObtenerPeriodo", IdPeriodo, IdUsuarioAuditoria);
					if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
					{
						periodo.mensaje.CodigoMensaje = 1;
						periodo.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ObtenerPeriodo], VERIFICAR CONSOLA";
						periodo.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();

						return periodo;
					}
					dt = ds.Tables[0].Copy();
					periodo.IdPeriodo = (int)dt.Rows[0]["IdPeriodo"];
					periodo.NombrePeriodo = (string)dt.Rows[0]["NombrePeriodo"];
					periodo.DecenioNombrePeriodo = (string)dt.Rows[0]["DecenioNombrePeriodo"];
					periodo.Decenio = (string)dt.Rows[0]["Decenio"];
					periodo.Actual = (bool)dt.Rows[0]["Actual"];

				}
				return periodo;
			}
			catch (Exception ex)
			{
				periodo.mensaje.CodigoMensaje = 1;
				periodo.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ObtenerPeriodo], VERIFICAR CONSOLA";
				periodo.mensaje.DescripcionMensajeSistema = ex.Message;
				return periodo;
			}
			finally
			{
				ds.Dispose();
				ds.Clear();
				dt.Dispose();
				dt.Clear();
			}
		}
		public Periodo GuardaPeriodo(int Gestor, int IdPeriodo, string NombrePeriodo, string DecenioNombrePeriodo, string Decenio, bool Actual, int IdUsuarioAuditoria)
		{
			try
			{
				if (Gestor == DatosGlobales.GestorSqlServer)
				{
					System.Object[] ParamtrosOutPut = null; 
					ParamtrosOutPut = ConexionSqlServer.GDatos.Ejecutar(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paGuardarPeriodo", IdPeriodo, NombrePeriodo, DecenioNombrePeriodo, Decenio, Actual, IdUsuarioAuditoria, "", 0);
					if (Convert.ToInt32(ParamtrosOutPut[100].ToString()) > 0)
					{
						periodo.mensaje.CodigoMensaje = 1;
						periodo.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [GuardaPeriodo], VERIFICAR CONSOLA";
						periodo.mensaje.DescripcionMensajeSistema = ParamtrosOutPut[99].ToString();
						return periodo;
					}
					periodo.IdPeriodo = Convert.ToInt32(ParamtrosOutPut[0].ToString());
					periodo.mensaje.DescripcionMensaje = ParamtrosOutPut[1].ToString();
					periodo.mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[2].ToString());
				}
				return periodo;
			}
			catch (Exception ex)
			{
				periodo.mensaje.CodigoMensaje = 1;
				periodo.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [GuardaPeriodo], VERIFICAR CONSOLA";
				periodo.mensaje.DescripcionMensajeSistema = ex.Message;
				return periodo;
			}
		}
		public ListaPeriodo ListarPeriodo(int Gestor, int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
		{
			DataSet ds = new DataSet();
			DataTable dt = new DataTable();
			try
			{
				if (Gestor == DatosGlobales.GestorSqlServer)
				{
					 ds = ConexionSqlServer.GDatos.TraerDataSet(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paListarPeriodo", IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
					if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
					{
						lista.mensaje.CodigoMensaje = 1;
						lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarPeriodo], VERIFICAR CONSOLA";
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
					periodo = new Periodo();
					periodo.IdPeriodo = (int)row["IdPeriodo"];
					periodo.NombrePeriodo = (string)row["NombrePeriodo"];
					periodo.DecenioNombrePeriodo = (string)row["DecenioNombrePeriodo"];
					periodo.Decenio = (string)row["Decenio"];
					periodo.Actual = (bool)row["Actual"];

					lista.lista.Add(periodo);
				}
				return lista;
			}
			catch (Exception ex)
			{
				lista.mensaje.CodigoMensaje = 1;
				lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarPeriodo], VERIFICAR CONSOLA";
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
		
		public ListaPeriodo ListarComboPeriodoProcesoEtapaPorResponsable(int Gestor, int IdEmpleadoPerfil, int IdUsuarioAuditoria)
		{
			DataTable dt = new DataTable();
			DataSet ds = new DataSet();
			try
			{
				if (Gestor == DatosGlobales.GestorSqlServer)
				{
					ds = ConexionSqlServer.GDatos.TraerDataSet(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paListarComboPeriodoProcesoEtapaPorResponsable", IdEmpleadoPerfil, IdUsuarioAuditoria);
					if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
					{
						lista.mensaje.CodigoMensaje = 1;
						lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarComboPeriodo], VERIFICAR CONSOLA";
						lista.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();
						return lista;
					}
					if (ds.Tables.Count > 0)
					{
						dt = ds.Tables[0].Copy();
					}
				}
				if (dt != null)
				{
					foreach (DataRow row in dt.Rows)
					{
						periodo = new Periodo();
						periodo.IdPeriodo = Convert.ToInt32(row["IdPeriodo"].ToString());
						periodo.NombrePeriodo = row["NombrePeriodo"].ToString();
						lista.lista.Add(periodo);
					}

				}
				return lista;
			}
			catch (Exception ex)
			{
				lista.mensaje.CodigoMensaje = 1;
				lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarComboPeriodo], VERIFICAR CONSOLA";
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
		
		public ListaPeriodo ListarComboPeriodoProcesoPorResponsable(int Gestor,int IdEmpleadoPerfil, int IdUsuarioAuditoria)
		{
			DataTable dt = new DataTable();
			DataSet ds = new DataSet();
			try
			{
				if (Gestor == DatosGlobales.GestorSqlServer)
				{
					ds = ConexionSqlServer.GDatos.TraerDataSet(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paListarComboPeriodoProcesoPorResponsable", IdEmpleadoPerfil, IdUsuarioAuditoria);
					if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
					{
						lista.mensaje.CodigoMensaje = 1;
						lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarComboPeriodo], VERIFICAR CONSOLA";
						lista.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();
						return lista;
					}
					if (ds.Tables.Count > 0)
					{
						dt = ds.Tables[0].Copy();
					}
				}
				if (dt != null)
				{
					foreach (DataRow row in dt.Rows)
					{
						periodo = new Periodo();
						periodo.IdPeriodo = Convert.ToInt32(row["IdPeriodo"].ToString());
						periodo.NombrePeriodo = row["NombrePeriodo"].ToString();
						lista.lista.Add(periodo);
					}

				}
				return lista;
			}
			catch (Exception ex)
			{
				lista.mensaje.CodigoMensaje = 1;
				lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarComboPeriodo], VERIFICAR CONSOLA";
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
		public ListaPeriodo ListarComboPeriodo(int Gestor, int IdUsuarioAuditoria)
		{
			DataTable dt = new DataTable();
			DataSet ds = new DataSet();
			try
			{
				if (Gestor == DatosGlobales.GestorSqlServer)
				{
					 ds = ConexionSqlServer.GDatos.TraerDataSet(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paListarComboPeriodo", IdUsuarioAuditoria);
					if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
					{
						lista.mensaje.CodigoMensaje = 1;
						lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarComboPeriodo], VERIFICAR CONSOLA";
						lista.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();
						return lista;
					}
					if (ds.Tables.Count > 0)
					{
						dt = ds.Tables[0].Copy();
					}
				}
				if (dt != null)
				{
					foreach (DataRow row in dt.Rows)
					{
						periodo = new Periodo();
						periodo.IdPeriodo = Convert.ToInt32(row["IdPeriodo"].ToString());
						periodo.NombrePeriodo = row["NombrePeriodo"].ToString();
						lista.lista.Add(periodo);
					}

				}
				return lista;
			}
			catch (Exception ex)
			{
				lista.mensaje.CodigoMensaje = 1;
				lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarComboPeriodo], VERIFICAR CONSOLA";
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
