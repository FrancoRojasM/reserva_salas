using System;
using Utilitarios;
using System.Data;
using Sistema;
using System.Threading.Tasks;

namespace SistemaLogic
{

	public class SoftwareLogic
	{
		private Software software;
		private ListaSoftware lista;

		public SoftwareLogic()
		{
			software = new Software();
			lista = new ListaSoftware();
		}
		public Mensaje EliminarSoftware(int Gestor, int IdSoftware, int IdUsuarioAuditoria)
		{
			Mensaje mensaje = new Mensaje();
			if (Gestor == DatosGlobales.GestorSqlServer)
			{
				System.Object[] ParamtrosOutPut = null; 
				ParamtrosOutPut = ConexionSqlServer.GDatos.Ejecutar(DatosGlobales.ListaConexiones.cnSistemaSql, "Sistema.paEliminarSoftware", IdSoftware, IdUsuarioAuditoria, mensaje.DescripcionMensaje, mensaje.CodigoMensaje);
				if (Convert.ToInt32(ParamtrosOutPut[100].ToString()) > 0)
				{
					mensaje.CodigoMensaje = 1;
					mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA EN LA CAPA DE DATOS [EliminarSoftware] VERIFICAR CONSOLA";
					mensaje.DescripcionMensajeSistema = ParamtrosOutPut[99].ToString();
					return mensaje;
				}
				mensaje.DescripcionMensaje = ParamtrosOutPut[0].ToString();
				mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[1].ToString());
			}
			return mensaje;
		}
		public async Task<Mensaje> EliminarSoftwareAsync(int Gestor, int IdSoftware, int IdUsuarioAuditoria)
		{
			Mensaje mensaje = new Mensaje();
			if (Gestor == DatosGlobales.GestorSqlServer)
			{
				System.Object[] ParamtrosOutPut = null; 
				ParamtrosOutPut = await ConexionSqlServer.GDatos.EjecutarAsync(DatosGlobales.ListaConexiones.cnSistemaSql, "Sistema.paEliminarSoftware", IdSoftware, IdUsuarioAuditoria, mensaje.DescripcionMensaje, mensaje.CodigoMensaje);
				if (Convert.ToInt32(ParamtrosOutPut[100].ToString()) > 0)
				{
					mensaje.CodigoMensaje = 1;
					mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA EN LA CAPA DE DATOS [EliminarSoftware] VERIFICAR CONSOLA";
					mensaje.DescripcionMensajeSistema = ParamtrosOutPut[99].ToString();
					return mensaje;
				}
				mensaje.DescripcionMensaje = ParamtrosOutPut[0].ToString();
				mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[1].ToString());
			}
			return mensaje;
		}
		public Software ObtenerSoftware(int Gestor, int IdSoftware, int IdUsuarioAuditoria)
		{
			DataSet ds = new DataSet();
			DataTable dt = new DataTable();
			try
			{
				if (Gestor == DatosGlobales.GestorSqlServer)
				{
					 ds = ConexionSqlServer.GDatos.TraerDataSet(DatosGlobales.ListaConexiones.cnSistemaSql, "Sistema.paObtenerSoftware", IdSoftware, IdUsuarioAuditoria);
					if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
					{
						software.mensaje.CodigoMensaje = 1;
						software.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ObtenerSoftware], VERIFICAR CONSOLA";
						software.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();

						return software;
					}
					dt = ds.Tables[0].Copy();
					software.IdSoftware = (int)dt.Rows[0]["IdSoftware"];
					software.NombreLargoSoftware = (string)dt.Rows[0]["NombreLargoSoftware"];
					software.NombreCortoSoftware = (string)dt.Rows[0]["NombreCortoSoftware"];
					software.NumeroVersionSoftware = (string)dt.Rows[0]["NumeroVersionSoftware"];
					software.RutaImagenSoftware = (string)dt.Rows[0]["RutaImagenSoftware"];
					software.RutaImagenLogoSoftware = (string)dt.Rows[0]["RutaImagenLogoSoftware"];
					software.NombreLargoEmpresa = (string)dt.Rows[0]["NombreLargoEmpresa"];
					software.NombreCortoEmpresa = (string)dt.Rows[0]["NombreCortoEmpresa"];

				}
				return software;
			}
			catch (Exception ex)
			{
				software.mensaje.CodigoMensaje = 1;
				software.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ObtenerSoftware], VERIFICAR CONSOLA";
				software.mensaje.DescripcionMensajeSistema = ex.Message;
				return software;
			}
			finally
			{
				ds.Dispose();
				ds.Clear();
				dt.Dispose();
				dt.Clear();
			}
		}
		public async Task<Software> ObtenerSoftwareAsync(int Gestor, int IdSoftware, int IdUsuarioAuditoria)
		{
			DataSet ds = new DataSet();
			DataTable dt = new DataTable();
			try
			{
				if (Gestor == DatosGlobales.GestorSqlServer)
				{
					 ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnSistemaSql, "Sistema.paObtenerSoftware", IdSoftware, IdUsuarioAuditoria);
					if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
					{
						software.mensaje.CodigoMensaje = 1;
						software.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ObtenerSoftware], VERIFICAR CONSOLA";
						software.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();

						return software;
					}
					dt = ds.Tables[0].Copy();
					software.IdSoftware = (int)dt.Rows[0]["IdSoftware"];
					software.NombreLargoSoftware = (string)dt.Rows[0]["NombreLargoSoftware"];
					software.NombreCortoSoftware = (string)dt.Rows[0]["NombreCortoSoftware"];
					software.NumeroVersionSoftware = (string)dt.Rows[0]["NumeroVersionSoftware"];
					software.RutaImagenSoftware = (string)dt.Rows[0]["RutaImagenSoftware"];
					software.RutaImagenLogoSoftware = (string)dt.Rows[0]["RutaImagenLogoSoftware"];
					software.NombreLargoEmpresa = (string)dt.Rows[0]["NombreLargoEmpresa"];
					software.NombreCortoEmpresa = (string)dt.Rows[0]["NombreCortoEmpresa"];

				}
				return software;
			}
			catch (Exception ex)
			{
				software.mensaje.CodigoMensaje = 1;
				software.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ObtenerSoftware], VERIFICAR CONSOLA";
				software.mensaje.DescripcionMensajeSistema = ex.Message;
				return software;
			}
			finally
			{
				ds.Dispose();
				ds.Clear();
				dt.Dispose();
				dt.Clear();
			}
		}
		public Software GuardaSoftware(int Gestor, int IdSoftware, string NombreLargoSoftware, string NombreCortoSoftware, string NumeroVersionSoftware, string RutaImagenSoftware, string RutaImagenLogoSoftware, string NombreLargoEmpresa, string NombreCortoEmpresa, int IdUsuarioAuditoria)
		{
			try
			{
				if (Gestor == DatosGlobales.GestorSqlServer)
				{
					System.Object[] ParamtrosOutPut = null; 
					ParamtrosOutPut = ConexionSqlServer.GDatos.Ejecutar(DatosGlobales.ListaConexiones.cnSistemaSql, "Sistema.paGuardarSoftware", IdSoftware, NombreLargoSoftware, NombreCortoSoftware, NumeroVersionSoftware, RutaImagenSoftware, RutaImagenLogoSoftware, NombreLargoEmpresa, NombreCortoEmpresa, IdUsuarioAuditoria, "", 0);
					if (Convert.ToInt32(ParamtrosOutPut[100].ToString()) > 0)
					{
						software.mensaje.CodigoMensaje = 1;
						software.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [GuardaSoftware], VERIFICAR CONSOLA";
						software.mensaje.DescripcionMensajeSistema = ParamtrosOutPut[99].ToString();
						return software;
					}
					software.IdSoftware = Convert.ToInt32(ParamtrosOutPut[0].ToString());
					software.mensaje.DescripcionMensaje = ParamtrosOutPut[1].ToString();
					software.mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[2].ToString());
				}
				return software;
			}
			catch (Exception ex)
			{
				software.mensaje.CodigoMensaje = 1;
				software.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [GuardaSoftware], VERIFICAR CONSOLA";
				software.mensaje.DescripcionMensajeSistema = ex.Message;
				return software;
			}
		}
		public async Task<Software> GuardaSoftwareAsync(int Gestor, int IdSoftware, string NombreLargoSoftware, string NombreCortoSoftware, string NumeroVersionSoftware, string RutaImagenSoftware, string RutaImagenLogoSoftware, string NombreLargoEmpresa, string NombreCortoEmpresa, int IdUsuarioAuditoria)
		{
			try
			{
				if (Gestor == DatosGlobales.GestorSqlServer)
				{
					System.Object[] ParamtrosOutPut = null; 
					ParamtrosOutPut = await ConexionSqlServer.GDatos.EjecutarAsync(DatosGlobales.ListaConexiones.cnSistemaSql, "Sistema.paGuardarSoftware", IdSoftware, NombreLargoSoftware, NombreCortoSoftware, NumeroVersionSoftware, RutaImagenSoftware, RutaImagenLogoSoftware, NombreLargoEmpresa, NombreCortoEmpresa, IdUsuarioAuditoria, "", 0);
					if (Convert.ToInt32(ParamtrosOutPut[100].ToString()) > 0)
					{
						software.mensaje.CodigoMensaje = 1;
						software.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [GuardaSoftware], VERIFICAR CONSOLA";
						software.mensaje.DescripcionMensajeSistema = ParamtrosOutPut[99].ToString();
						return software;
					}
					software.IdSoftware = Convert.ToInt32(ParamtrosOutPut[0].ToString());
					software.mensaje.DescripcionMensaje = ParamtrosOutPut[1].ToString();
					software.mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[2].ToString());
				}
				return software;
			}
			catch (Exception ex)
			{
				software.mensaje.CodigoMensaje = 1;
				software.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [GuardaSoftware], VERIFICAR CONSOLA";
				software.mensaje.DescripcionMensajeSistema = ex.Message;
				return software;
			}
		}
		public ListaSoftware ListarSoftware(int Gestor, int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
		{
			DataSet ds = new DataSet();
			DataTable dt = new DataTable();
			try
			{
				if (Gestor == DatosGlobales.GestorSqlServer)
				{
					 ds = ConexionSqlServer.GDatos.TraerDataSet(DatosGlobales.ListaConexiones.cnSistemaSql, "Sistema.paListarSoftware", IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
					if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
					{
						lista.mensaje.CodigoMensaje = 1;
						lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarSoftware], VERIFICAR CONSOLA";
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
					software = new Software();
					software.IdSoftware = (int)row["IdSoftware"];
					software.NombreLargoSoftware = (string)row["NombreLargoSoftware"];
					software.NombreCortoSoftware = (string)row["NombreCortoSoftware"];
					software.NumeroVersionSoftware = (string)row["NumeroVersionSoftware"];
					software.RutaImagenSoftware = (string)row["RutaImagenSoftware"];
					software.RutaImagenLogoSoftware = (string)row["RutaImagenLogoSoftware"];
					software.NombreLargoEmpresa = (string)row["NombreLargoEmpresa"];
					software.NombreCortoEmpresa = (string)row["NombreCortoEmpresa"];

					lista.lista.Add(software);
				}
				return lista;
			}
			catch (Exception ex)
			{
				lista.mensaje.CodigoMensaje = 1;
				lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarSoftware], VERIFICAR CONSOLA";
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
		public async Task<ListaSoftware> ListarSoftwareAsync(int Gestor, int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
		{
			DataSet ds = new DataSet();
			DataTable dt = new DataTable();
			try
			{
				if (Gestor == DatosGlobales.GestorSqlServer)
				{
					 ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnSistemaSql, "Sistema.paListarSoftware", IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
					if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
					{
						lista.mensaje.CodigoMensaje = 1;
						lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarSoftware], VERIFICAR CONSOLA";
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
					software = new Software();
					software.IdSoftware = (int)row["IdSoftware"];
					software.NombreLargoSoftware = (string)row["NombreLargoSoftware"];
					software.NombreCortoSoftware = (string)row["NombreCortoSoftware"];
					software.NumeroVersionSoftware = (string)row["NumeroVersionSoftware"];
					software.RutaImagenSoftware = (string)row["RutaImagenSoftware"];
					software.RutaImagenLogoSoftware = (string)row["RutaImagenLogoSoftware"];
					software.NombreLargoEmpresa = (string)row["NombreLargoEmpresa"];
					software.NombreCortoEmpresa = (string)row["NombreCortoEmpresa"];

					lista.lista.Add(software);
				}
				return lista;
			}
			catch (Exception ex)
			{
				lista.mensaje.CodigoMensaje = 1;
				lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarSoftware], VERIFICAR CONSOLA";
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
