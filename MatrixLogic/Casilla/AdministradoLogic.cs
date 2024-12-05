using System;
using Utilitarios;
using System.Data;
using System.Threading.Tasks;
using Casilla;
namespace CasillaLogic
{

	public class AdministradoLogic
	{
		private Administrado administrado;
		private ListaAdministrado lista;

		public AdministradoLogic()
		{
			administrado = new Administrado();
			lista = new ListaAdministrado();
		}

		public async Task<Mensaje> EliminarAdministradoAsync(int Gestor, int IdAdministrado, int IdUsuarioAuditoria)
		{
			Mensaje mensaje = new Mensaje();
			if (Gestor == DatosGlobales.GestorSqlServer)
			{
				System.Object[] ParamtrosOutPut = null;
				ParamtrosOutPut = await ConexionSqlServer.GDatos.EjecutarAsync(DatosGlobales.ListaConexiones.cnCasillaSql, "Casilla.paEliminarAdministrado", IdAdministrado, IdUsuarioAuditoria, mensaje.DescripcionMensaje, mensaje.CodigoMensaje);
				if (Convert.ToInt32(ParamtrosOutPut[100].ToString()) > 0)
				{
					mensaje.CodigoMensaje = 1;
					mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA EN LA CAPA DE DATOS [EliminarAdministrado] VERIFICAR CONSOLA";
					mensaje.DescripcionMensajeSistema = ParamtrosOutPut[99].ToString();
					return mensaje;
				}
				mensaje.DescripcionMensaje = ParamtrosOutPut[0].ToString();
				mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[1].ToString());
			}
			return mensaje;
		}
		public async Task<Mensaje> GenerarCodigoCorreoConfirmacion(int Gestor, int IdAdministrado, string CodigoCorreoConfirmacion,string EmailNotificacion, int IdUsuarioAuditoria)
		{
			Mensaje mensaje = new Mensaje();
			if (Gestor == DatosGlobales.GestorSqlServer)
			{
				System.Object[] ParamtrosOutPut = null;
				ParamtrosOutPut = await ConexionSqlServer.GDatos.EjecutarAsync(DatosGlobales.ListaConexiones.cnCasillaSql, "Casilla.paGenerarCodigoCorreoConfirmacion", IdAdministrado, CodigoCorreoConfirmacion, EmailNotificacion,IdUsuarioAuditoria, mensaje.DescripcionMensaje, mensaje.CodigoMensaje);
				if (Convert.ToInt32(ParamtrosOutPut[100].ToString()) > 0)
				{
					mensaje.CodigoMensaje = 1;
					mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA EN LA CAPA DE DATOS [EliminarAdministrado] VERIFICAR CONSOLA";
					mensaje.DescripcionMensajeSistema = ParamtrosOutPut[99].ToString();
					return mensaje;
				}
				mensaje.DescripcionMensaje = ParamtrosOutPut[0].ToString();
				mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[1].ToString());
			}
			return mensaje;
		}
        public async Task<Mensaje> GenerarCodigoTelefonoConfirmacion(int Gestor, int IdAdministrado, string CodigoCorreoConfirmacion,string NumeroCelularNotificacion, int IdUsuarioAuditoria)
        {
            Mensaje mensaje = new Mensaje();
            if (Gestor == DatosGlobales.GestorSqlServer)
            {
                System.Object[] ParamtrosOutPut = null;
                ParamtrosOutPut = await ConexionSqlServer.GDatos.EjecutarAsync(DatosGlobales.ListaConexiones.cnCasillaSql, "Casilla.paGenerarCodigoTelefonoConfirmacion", IdAdministrado, CodigoCorreoConfirmacion, NumeroCelularNotificacion, IdUsuarioAuditoria, mensaje.DescripcionMensaje, mensaje.CodigoMensaje);
                if (Convert.ToInt32(ParamtrosOutPut[100].ToString()) > 0)
                {
                    mensaje.CodigoMensaje = 1;
                    mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA EN LA CAPA DE DATOS [EliminarAdministrado] VERIFICAR CONSOLA";
                    mensaje.DescripcionMensajeSistema = ParamtrosOutPut[99].ToString();
                    return mensaje;
                }
                mensaje.DescripcionMensaje = ParamtrosOutPut[0].ToString();
                mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[1].ToString());
            }
            return mensaje;
        } 
		public async Task<Mensaje> GenerarCodigoCorreoConfirmacionLogin(int Gestor, int IdVerificacion, string CodigoCorreoConfirmacion,string EmailNotificacion, string CodigoTelefonoConfirmacion, string NumeroCelularNotificacion, int IdUsuarioAuditoria)
        {
            Mensaje mensaje = new Mensaje();
            if (Gestor == DatosGlobales.GestorSqlServer)
            {
                System.Object[] ParamtrosOutPut = null;
                ParamtrosOutPut = await ConexionSqlServer.GDatos.EjecutarAsync(DatosGlobales.ListaConexiones.cnCasillaSql, "Casilla.paGenerarCodigoCorreoConfirmacionLogin", IdVerificacion, CodigoCorreoConfirmacion, EmailNotificacion, CodigoTelefonoConfirmacion, NumeroCelularNotificacion, IdUsuarioAuditoria, mensaje.DescripcionMensaje, mensaje.CodigoMensaje);
                if (Convert.ToInt32(ParamtrosOutPut[100].ToString()) > 0)
                {
                    mensaje.CodigoMensaje = 1;
                    mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA EN LA CAPA DE DATOS [EliminarAdministrado] VERIFICAR CONSOLA";
                    mensaje.DescripcionMensajeSistema = ParamtrosOutPut[99].ToString();
                    return mensaje;
                }
                mensaje.CodigoRegistroTabla = Convert.ToInt32(ParamtrosOutPut[0].ToString());
                mensaje.DescripcionMensaje = ParamtrosOutPut[1].ToString();
                mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[2].ToString());
               
            }
            return mensaje;
        }
        public async Task<Mensaje> GenerarCodigoCorreoValidacionLogin(int Gestor, int IdVerificacion, string CodigoCorreoConfirmacion,string EmailNotificacion, string CodigoTelefonoConfirmacion, string NumeroCelularNotificacion, int IdUsuarioAuditoria)
        {
            Mensaje mensaje = new Mensaje();
            if (Gestor == DatosGlobales.GestorSqlServer)
            {
                System.Object[] ParamtrosOutPut = null;
                ParamtrosOutPut = await ConexionSqlServer.GDatos.EjecutarAsync(DatosGlobales.ListaConexiones.cnCasillaSql, "Casilla.paGenerarCodigoCorreoValidacionLogin", IdVerificacion, CodigoCorreoConfirmacion, EmailNotificacion, CodigoTelefonoConfirmacion, NumeroCelularNotificacion, IdUsuarioAuditoria, mensaje.DescripcionMensaje, mensaje.CodigoMensaje);
                if (Convert.ToInt32(ParamtrosOutPut[100].ToString()) > 0)
                {
                    mensaje.CodigoMensaje = 1;
                    mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA EN LA CAPA DE DATOS [EliminarAdministrado] VERIFICAR CONSOLA";
                    mensaje.DescripcionMensajeSistema = ParamtrosOutPut[99].ToString();
                    return mensaje;
                }
                mensaje.DescripcionMensaje = ParamtrosOutPut[0].ToString();
                mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[1].ToString());
            }
            return mensaje;
        }
        public async Task<Administrado> ObtenerAdministradoAsync(int Gestor, int IdAdministrado, int IdUsuarioAuditoria)
		{
			DataSet ds = new DataSet();
			DataTable dt = new DataTable();
			try
			{
				if (Gestor == DatosGlobales.GestorSqlServer)
				{
					ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnCasillaSql, "Casilla.paObtenerAdministrado", IdAdministrado, IdUsuarioAuditoria);
					if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
					{
						administrado.mensaje.CodigoMensaje = 1;
						administrado.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ObtenerAdministrado], VERIFICAR CONSOLA";
						administrado.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();

						return administrado;
					}
					dt = ds.Tables[0].Copy();
					administrado.IdAdministrado = dt.Rows[0]["IdAdministrado"] is System.DBNull ? 0 : (int)dt.Rows[0]["IdAdministrado"];
					administrado.persona.IdPersona = dt.Rows[0]["IdPersona"] is System.DBNull ? 0 : (int)dt.Rows[0]["IdPersona"];
					administrado.persona.NombreCompleto = dt.Rows[0]["NombreCompletoPersona"] is System.DBNull ? null : (string)dt.Rows[0]["NombreCompletoPersona"];
					administrado.usuario.IdUsuario = dt.Rows[0]["IdUsuario"] is System.DBNull ? 0 : (int)dt.Rows[0]["IdUsuario"];
					administrado.EmailNotificacion = dt.Rows[0]["EmailNotificacion"] is System.DBNull ? null : (string)dt.Rows[0]["EmailNotificacion"];
					administrado.NumeroCelularNotificacion = dt.Rows[0]["NumeroCelularNotificacion"] is System.DBNull ? null : (string)dt.Rows[0]["NumeroCelularNotificacion"];
					administrado.AsientoElectronico = dt.Rows[0]["AsientoElectronico"] is System.DBNull ? null : (string)dt.Rows[0]["AsientoElectronico"];
					administrado.PartidaElectronica = dt.Rows[0]["PartidaElectronica"] is System.DBNull ? null : (string)dt.Rows[0]["PartidaElectronica"];
					administrado.Activo = dt.Rows[0]["Activo"] is System.DBNull ? false : (bool)dt.Rows[0]["Activo"];

				}
				return administrado;
			}
			catch (Exception ex)
			{
				administrado.mensaje.CodigoMensaje = 1;
				administrado.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ObtenerAdministrado], VERIFICAR CONSOLA";
				administrado.mensaje.DescripcionMensajeSistema = ex.Message;
				return administrado;
			}
			finally
			{
				ds.Dispose();
				ds.Clear();
				dt.Dispose();
				dt.Clear();
			}
		}
		public async Task<Administrado> GuardaAdministradoAsync(int Gestor, int IdAdministrado, int IdPersona, int IdUsuario, string EmailNotificacion, string NumeroCelularNotificacion, string AsientoElectronico, string PartidaElectronica, bool Activo, string CodigoTelefonoConfirmacion, string CodigoCorreoConfirmacion, int CodigoCorreoValidar, int CodigoTelefonoValidar, int IdUsuarioAuditoria)
		{
			try
			{
				if (Gestor == DatosGlobales.GestorSqlServer)
				{
					System.Object[] ParamtrosOutPut = null;
					ParamtrosOutPut = await ConexionSqlServer.GDatos.EjecutarAsync(DatosGlobales.ListaConexiones.cnCasillaSql, "Casilla.paGuardarAdministrado", IdAdministrado, IdPersona, IdUsuario, EmailNotificacion, NumeroCelularNotificacion, AsientoElectronico, PartidaElectronica, Activo, CodigoTelefonoConfirmacion, CodigoCorreoConfirmacion, CodigoCorreoValidar, CodigoTelefonoValidar, IdUsuarioAuditoria, "", 0);
					if (Convert.ToInt32(ParamtrosOutPut[100].ToString()) > 0)
					{
						administrado.mensaje.CodigoMensaje = 1;
						administrado.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [GuardaAdministrado], VERIFICAR CONSOLA";
						administrado.mensaje.DescripcionMensajeSistema = ParamtrosOutPut[99].ToString();
						return administrado;
					}
					administrado.IdAdministrado = Convert.ToInt32(ParamtrosOutPut[0].ToString());
					administrado.mensaje.DescripcionMensaje = ParamtrosOutPut[1].ToString();
					administrado.mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[2].ToString());
				}
				return administrado;
			}
			catch (Exception ex)
			{
				administrado.mensaje.CodigoMensaje = 1;
				administrado.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [GuardaAdministrado], VERIFICAR CONSOLA";
				administrado.mensaje.DescripcionMensajeSistema = ex.Message;
				return administrado;
			}
		}

		public async Task<ListaAdministrado> ListarAdministradoAsync(int Gestor, int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
		{
			DataSet ds = new DataSet();
			DataTable dt = new DataTable();
			try
			{
				if (Gestor == DatosGlobales.GestorSqlServer)
				{
					ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnCasillaSql, "Casilla.paListarAdministrado", IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
					if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
					{
						lista.mensaje.CodigoMensaje = 1;
						lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarAdministrado], VERIFICAR CONSOLA";
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
					administrado = new Administrado();
					administrado.IdAdministrado = row["IdAdministrado"] is System.DBNull ? 0 : (int)row["IdAdministrado"];

					administrado.persona.NombreCompleto = row["NombreCompleto"] is System.DBNull ? null : (string)row["NombreCompletoPersona"];

					administrado.usuario.IdUsuario = row["IdUsuario"] is System.DBNull ? 0 : (int)row["IdUsuario"];
					administrado.EmailNotificacion = row["EmailNotificacion"] is System.DBNull ? null : (string)row["EmailNotificacion"];
					administrado.NumeroCelularNotificacion = row["NumeroCelularNotificacion"] is System.DBNull ? null : (string)row["NumeroCelularNotificacion"];
					administrado.AsientoElectronico = row["AsientoElectronico"] is System.DBNull ? null : (string)row["AsientoElectronico"];
					administrado.PartidaElectronica = row["PartidaElectronica"] is System.DBNull ? null : (string)row["PartidaElectronica"];
					administrado.Activo = row["Activo"] is System.DBNull ? false : (bool)row["Activo"];

					lista.lista.Add(administrado);
				}
				return lista;
			}
			catch (Exception ex)
			{
				lista.mensaje.CodigoMensaje = 1;
				lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarAdministrado], VERIFICAR CONSOLA";
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
		public async Task<DataSet> DescargarAdministrado(int Gestor, int IdUsuarioAuditoria)
		{
			DataSet ds = new DataSet();
			DataSet listads = new DataSet();
			DataTable dt = new DataTable();
			try
			{
				if (Gestor == DatosGlobales.GestorSqlServer)
				{
					listads = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnCasillaSql, "Casilla.paDescargarAdministrado", IdUsuarioAuditoria);
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
		public async Task<ListaAdministrado> ListarAdministradoPorAutoComplete(int Gestor, int IdUsuarioAuditoria, string NombreCompleto)
		{
			DataSet ds = new DataSet();
			DataTable dt = new DataTable();
			try
			{
				if (Gestor == DatosGlobales.GestorSqlServer)
				{
					ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnCasillaSql, "Casilla.paListarAdministradoPorAutoComplete", IdUsuarioAuditoria, NombreCompleto);
					if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
					{
						lista.mensaje.CodigoMensaje = 1;
						lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarAdministrado], VERIFICAR CONSOLA";
						lista.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();
						return lista;
					}
					dt = ds.Tables[0].Copy();
				
				}
				foreach (DataRow row in dt.Rows)
				{
					administrado = new Administrado();
					administrado.IdAdministrado = row["IdAdministrado"] is System.DBNull ? 0 : (int)row["IdAdministrado"];

					administrado.persona.NombreCompleto = row["NombreCompleto"] is System.DBNull ? null : (string)row["NombreCompleto"];


					lista.lista.Add(administrado);
				}
				return lista;
			}
			catch (Exception ex)
			{
				lista.mensaje.CodigoMensaje = 1;
				lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarAdministrado], VERIFICAR CONSOLA";
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
