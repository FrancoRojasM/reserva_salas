using System;
using Utilitarios;
using System.Data;
using System.Threading.Tasks;
using Reservas;
namespace ReservasLogic
{

	public class SalaLogic
	{
		private Sala sala;
		private ListaSala lista;

		public SalaLogic()
		{
			sala = new Sala();
			lista = new ListaSala();
		}

		public async Task<Mensaje> EliminarSalaAsync(int Gestor, int IdSala, int IdUsuarioAuditoria)
		{
			Mensaje mensaje = new Mensaje();
			if (Gestor == DatosGlobales.GestorSqlServer)
			{
				System.Object[] ParamtrosOutPut = null;
				ParamtrosOutPut = await ConexionSqlServer.GDatos.EjecutarAsync(DatosGlobales.ListaConexiones.cnReservasSql, "Reservas.paEliminarSala", IdSala, IdUsuarioAuditoria, mensaje.DescripcionMensaje, mensaje.CodigoMensaje);
				if (Convert.ToInt32(ParamtrosOutPut[100].ToString()) > 0)
				{
					mensaje.CodigoMensaje = 1;
					mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA EN LA CAPA DE DATOS [EliminarSala] VERIFICAR CONSOLA";
					mensaje.DescripcionMensajeSistema = ParamtrosOutPut[99].ToString();
					return mensaje;
				}
				mensaje.DescripcionMensaje = ParamtrosOutPut[0].ToString();
				mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[1].ToString());
			}
			return mensaje;
		}
		public async Task<Sala> ObtenerSalaAsync(int Gestor, int IdSala, int IdUsuarioAuditoria)
		{
			DataSet ds = new DataSet();
			DataTable dt = new DataTable();
			try
			{
				if (Gestor == DatosGlobales.GestorSqlServer)
				{
					ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnReservasSql, "Reservas.paObtenerSala", IdSala, IdUsuarioAuditoria);
					if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
					{
						sala.mensaje.CodigoMensaje = 1;
						sala.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ObtenerSala], VERIFICAR CONSOLA";
						sala.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();

						return sala;
					}
					dt = ds.Tables[0].Copy();
					sala.IdSala = dt.Rows[0]["IdSala"] is System.DBNull ? 0 : (int)dt.Rows[0]["IdSala"];
					sala.Nombre = dt.Rows[0]["Nombre"] is System.DBNull ? null : (string)dt.Rows[0]["Nombre"];
					sala.Aforo = dt.Rows[0]["Aforo"] is System.DBNull ? 0 : (int)dt.Rows[0]["Aforo"];
					sala.catalogopiso.IdCatalogo = dt.Rows[0]["IdCatalogoPiso"] is System.DBNull ? 0 : (int)dt.Rows[0]["IdCatalogoPiso"];
					sala.catalogopiso.Descripcion = dt.Rows[0]["CatalogoPiso"] is System.DBNull ? "" : (string)dt.Rows[0]["CatalogoPiso"];
					sala.Observaciones = dt.Rows[0]["Observaciones"] is System.DBNull ? null : (string)dt.Rows[0]["Observaciones"];

				}
				return sala;
			}
			catch (Exception ex)
			{
				sala.mensaje.CodigoMensaje = 1;
				sala.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ObtenerSala], VERIFICAR CONSOLA";
				sala.mensaje.DescripcionMensajeSistema = ex.Message;
				return sala;
			}
			finally
			{
				ds.Dispose();
				ds.Clear();
				dt.Dispose();
				dt.Clear();
			}
		}
		public async Task<Sala> GuardaSalaAsync(int Gestor, int IdSala, string Nombre, int Aforo, int IdCatalogoPiso, string Observaciones, int IdUsuarioAuditoria)
		{
			try
			{
				if (Gestor == DatosGlobales.GestorSqlServer)
				{
					System.Object[] ParamtrosOutPut = null;
					ParamtrosOutPut = await ConexionSqlServer.GDatos.EjecutarAsync(DatosGlobales.ListaConexiones.cnReservasSql, "Reservas.paGuardarSala", IdSala, Nombre, Aforo, IdCatalogoPiso, Observaciones, IdUsuarioAuditoria, "", 0);
					if (Convert.ToInt32(ParamtrosOutPut[100].ToString()) > 0)
					{
						sala.mensaje.CodigoMensaje = 1;
						sala.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [GuardaSala], VERIFICAR CONSOLA";
						sala.mensaje.DescripcionMensajeSistema = ParamtrosOutPut[99].ToString();
						return sala;
					}
					sala.IdSala = Convert.ToInt32(ParamtrosOutPut[0].ToString());
					sala.mensaje.DescripcionMensaje = ParamtrosOutPut[1].ToString();
					sala.mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[2].ToString());
				}
				return sala;
			}
			catch (Exception ex)
			{
				sala.mensaje.CodigoMensaje = 1;
				sala.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [GuardaSala], VERIFICAR CONSOLA";
				sala.mensaje.DescripcionMensajeSistema = ex.Message;
				return sala;
			}
		}

		public async Task<ListaSala> ListarSalaAsync(int Gestor, int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
		{
			DataSet ds = new DataSet();
			DataTable dt = new DataTable();
			try
			{
				if (Gestor == DatosGlobales.GestorSqlServer)
				{
					ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnReservasSql, "Reservas.paListarSala", IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
					if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
					{
						lista.mensaje.CodigoMensaje = 1;
						lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarSala], VERIFICAR CONSOLA";
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
					sala = new Sala();
					sala.IdSala = row["IdSala"] is System.DBNull ? 0 : (int)row["IdSala"];
					sala.Nombre = row["Nombre"] is System.DBNull ? null : (string)row["Nombre"];
					sala.Aforo = row["Aforo"] is System.DBNull ? 0 : (int)row["Aforo"];
					sala.catalogopiso.Descripcion = row["CatalogoPiso"] is System.DBNull ? null : (string)row["CatalogoPiso"];
					sala.Observaciones = row["Observaciones"] is System.DBNull ? null : (string)row["Observaciones"];

					lista.lista.Add(sala);
				}
				return lista;
			}
			catch (Exception ex)
			{
				lista.mensaje.CodigoMensaje = 1;
				lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarSala], VERIFICAR CONSOLA";
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
		public async Task<DataSet> DescargarSala(int Gestor, int IdUsuarioAuditoria)
		{
			DataSet ds = new DataSet();
			DataSet listads = new DataSet();
			DataTable dt = new DataTable();
			try
			{
				if (Gestor == DatosGlobales.GestorSqlServer)
				{
					listads = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnReservasSql, "Reservas.paDescargarSala", IdUsuarioAuditoria);
					if (Convert.ToInt32(listads.ExtendedProperties["NumeroError"].ToString()) > 0)
					{
						return listads;
					}
					if (ds.Tables.Count > 0)
					{
						dt = listads.Tables[0].Copy();

					}
				}
				return listads;
			}
			finally
			{
				ds.Dispose();
				ds.Clear();
				//dt.Dispose();
				//dt.Clear();
			}
		}
	}

}
