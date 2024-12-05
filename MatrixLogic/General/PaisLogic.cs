using System;
using Utilitarios;
using System.Data;
using General;
using System.Threading.Tasks;

namespace GeneralLogic
{

	public class PaisLogic
	{
		private Pais pais;
		private ListaPais lista;

		public PaisLogic()
		{
			pais = new Pais();
			lista = new ListaPais();
		}
		public Mensaje EliminarPais(int Gestor, int IdPais, int IdUsuarioAuditoria)
		{
			Mensaje mensaje = new Mensaje();
			if (Gestor == DatosGlobales.GestorSqlServer)
			{
				System.Object[] ParamtrosOutPut = null; 
				ParamtrosOutPut = ConexionSqlServer.GDatos.Ejecutar(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paEliminarPais", IdPais, IdUsuarioAuditoria, mensaje.DescripcionMensaje, mensaje.CodigoMensaje);
				if (Convert.ToInt32(ParamtrosOutPut[100].ToString()) > 0)
				{
					mensaje.CodigoMensaje = 1;
					mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA EN LA CAPA DE DATOS [EliminarPais] VERIFICAR CONSOLA";
					mensaje.DescripcionMensajeSistema = ParamtrosOutPut[99].ToString();
					return mensaje;
				}
				mensaje.DescripcionMensaje = ParamtrosOutPut[0].ToString();
				mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[1].ToString());
			}
			return mensaje;
		}
		public Pais ObtenerPais(int Gestor, int IdPais, int IdUsuarioAuditoria)
		{
			DataSet ds = new DataSet();
			DataTable dt = new DataTable();
			try
			{
				if (Gestor == DatosGlobales.GestorSqlServer)
				{
					 ds = ConexionSqlServer.GDatos.TraerDataSet(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paObtenerPais", IdPais, IdUsuarioAuditoria);
					if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
					{
						pais.mensaje.CodigoMensaje = 1;
						pais.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ObtenerPais], VERIFICAR CONSOLA";
						pais.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();

						return pais;
					}
					dt = ds.Tables[0].Copy();
					pais.IdPais = (int)dt.Rows[0]["IdPais"];
					pais.NombrePais = (string)dt.Rows[0]["NombrePais"];
					pais.Gentilicio = (string)dt.Rows[0]["Gentilicio"];
					pais.Alfa3 = (string)dt.Rows[0]["Alfa3"];
					pais.Alfa2 = (string)dt.Rows[0]["Alfa2"];
					pais.Bandera = (string)dt.Rows[0]["Bandera"];

				}
				return pais;
			}
			catch (Exception ex)
			{
				pais.mensaje.CodigoMensaje = 1;
				pais.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ObtenerPais], VERIFICAR CONSOLA";
				pais.mensaje.DescripcionMensajeSistema = ex.Message;
				return pais;
			}
			finally
			{
				ds.Dispose();
				ds.Clear();
				dt.Dispose();
				dt.Clear();
			}
		}
		public Pais GuardaPais(int Gestor, int IdPais, string NombrePais, string Gentilicio, string Alfa3, string Alfa2, string Bandera, int IdUsuarioAuditoria)
		{
			try
			{
				if (Gestor == DatosGlobales.GestorSqlServer)
				{
					System.Object[] ParamtrosOutPut = null; 
					ParamtrosOutPut = ConexionSqlServer.GDatos.Ejecutar(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paGuardarPais", IdPais, NombrePais, Gentilicio, Alfa3, Alfa2, Bandera, IdUsuarioAuditoria, "", 0);
					if (Convert.ToInt32(ParamtrosOutPut[100].ToString()) > 0)
					{
						pais.mensaje.CodigoMensaje = 1;
						pais.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [GuardaPais], VERIFICAR CONSOLA";
						pais.mensaje.DescripcionMensajeSistema = ParamtrosOutPut[99].ToString();
						return pais;
					}
					pais.IdPais = Convert.ToInt32(ParamtrosOutPut[0].ToString());
					pais.mensaje.DescripcionMensaje = ParamtrosOutPut[1].ToString();
					pais.mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[2].ToString());
				}
				return pais;
			}
			catch (Exception ex)
			{
				pais.mensaje.CodigoMensaje = 1;
				pais.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [GuardaPais], VERIFICAR CONSOLA";
				pais.mensaje.DescripcionMensajeSistema = ex.Message;
				return pais;
			}
		}
		public ListaPais ListarPais(int Gestor, int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
		{
			DataSet ds = new DataSet();
			DataTable dt = new DataTable();
			try
			{
				if (Gestor == DatosGlobales.GestorSqlServer)
				{
					 ds = ConexionSqlServer.GDatos.TraerDataSet(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paListarPais", IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
					if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
					{
						lista.mensaje.CodigoMensaje = 1;
						lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarPais], VERIFICAR CONSOLA";
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
					pais = new Pais();
					pais.IdPais = (int)row["IdPais"];
					pais.NombrePais = (string)row["NombrePais"];
					pais.Gentilicio = (string)row["Gentilicio"];
					pais.Alfa3 = (string)row["Alfa3"];
					pais.Alfa2 = (string)row["Alfa2"];
					pais.Bandera = (string)row["Bandera"];

					lista.lista.Add(pais);
				}
				return lista;
			}
			catch (Exception ex)
			{
				lista.mensaje.CodigoMensaje = 1;
				lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarPais], VERIFICAR CONSOLA";
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
		public ListaPais ListarComboPais(int Gestor, int IdUsuarioAuditoria)
		{
			DataTable dt = new DataTable();
			DataSet ds = new DataSet();
			try
			{
				if (Gestor == DatosGlobales.GestorSqlServer)
				{
					 ds = ConexionSqlServer.GDatos.TraerDataSet(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paListarComboPais", IdUsuarioAuditoria);
					if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
					{
						lista.mensaje.CodigoMensaje = 1;
						lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarComboPais], VERIFICAR CONSOLA";
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
						pais = new Pais();
						pais.IdPais = Convert.ToInt32(row["IdPais"].ToString());
						pais.NombrePais = row["NombrePais"].ToString();
						lista.lista.Add(pais);
					}

				}
				return lista;
			}
			catch (Exception ex)
			{
				lista.mensaje.CodigoMensaje = 1;
				lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarComboPais], VERIFICAR CONSOLA";
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
		public async Task<ListaPais> ListarComboPaisAsync(int Gestor, int IdUsuarioAuditoria)
		{
			DataTable dt = new DataTable();
			DataSet ds = new DataSet();
			try
			{
				if (Gestor == DatosGlobales.GestorSqlServer)
				{
					ds =await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paListarComboPais", IdUsuarioAuditoria);
					if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
					{
						lista.mensaje.CodigoMensaje = 1;
						lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarComboPais], VERIFICAR CONSOLA";
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
						pais = new Pais();
						pais.IdPais = Convert.ToInt32(row["IdPais"].ToString());
						pais.NombrePais = row["NombrePais"].ToString();
						lista.lista.Add(pais);
					}

				}
				return lista;
			}
			catch (Exception ex)
			{
				lista.mensaje.CodigoMensaje = 1;
				lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarComboPais], VERIFICAR CONSOLA";
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
