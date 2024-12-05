using System;
using Utilitarios;
using System.Data;
using Seguridad;
using System.Threading.Tasks;
using ApiLogic;
using System.Collections.Generic;
using Newtonsoft.Json;
using RecursoHumano;
using Inventario;
using General;
using System.Collections;

namespace SeguridadLogic
{
    public class UsuarioLogic
    {
        private Seguridad.Usuario usuario;
        private Seguridad.ListaUsuario Lista;
        private General.Sucursal sucursal;
        public UsuarioLogic()
        {
            usuario = new Seguridad.Usuario();
            Lista = new Seguridad.ListaUsuario();
        }
        public Mensaje EliminarUsuario(int Gestor, int IdUsuario, int IdUsuarioAuditoria)
        {
            Mensaje mensaje = new Mensaje();
            if (Gestor == DatosGlobales.GestorSqlServer)
            {
                System.Object[] ParamtrosOutPut = null;
                ParamtrosOutPut = ConexionSqlServer.GDatos.Ejecutar(DatosGlobales.ListaConexiones.cnSeguridadSql, "Seguridad.paEliminarUsuario", IdUsuario, IdUsuarioAuditoria, mensaje.DescripcionMensaje, mensaje.CodigoMensaje);
                mensaje.DescripcionMensaje = ParamtrosOutPut[0].ToString();
                mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[1].ToString());
            }

            return mensaje;
        }
        public async Task<Mensaje> EliminarUsuarioAsync(int Gestor, int IdUsuario, int IdUsuarioAuditoria)
        {
            Mensaje mensaje = new Mensaje();
            if (Gestor == DatosGlobales.GestorSqlServer)
            {


                System.Object[] ParamtrosOutPut = null;
                ParamtrosOutPut = await ConexionSqlServer.GDatos.EjecutarAsync(DatosGlobales.ListaConexiones.cnSeguridadSql, "Seguridad.paEliminarUsuario", IdUsuario, IdUsuarioAuditoria, mensaje.DescripcionMensaje, mensaje.CodigoMensaje);
                mensaje.DescripcionMensaje = ParamtrosOutPut[0].ToString();
                mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[1].ToString());
            }
            //if (Gestor == DatosGlobales.GestorApi)
            //{
            //    ClienteApi cla = new ClienteApi();
            //    await cla.Post<Mensaje>("api/EliiminarUsuario", mensaje);
            //}
            return mensaje;
        }
        public Usuario ObtenerUsuario(int Gestor, int IdUsuario)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (IdUsuario == 0)
                {
                    return usuario;
                }

                if (Gestor == DatosGlobales.GestorSqlServer) { ds = ConexionSqlServer.GDatos.TraerDataSet(DatosGlobales.ListaConexiones.cnSeguridadSql, "Seguridad.paObtenerUsuario", IdUsuario); }
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0].Copy();
                    if (dt.Rows.Count > 0)
                    {
                        usuario.IdUsuario = (int)dt.Rows[0]["IdUsuario"];
                        usuario.persona.IdPersona = (int)dt.Rows[0]["IdPersona"];
                        usuario.persona.NombreCompleto = (string)dt.Rows[0]["NombreCompleto"];
                        usuario.catalogotipousuario.IdCatalogo = (int)dt.Rows[0]["IdCatalogoTipoUsuario"];
                        usuario.Logueo = (string)dt.Rows[0]["Logueo"];
                        usuario.Clave = (string)dt.Rows[0]["Clave"];
                        usuario.Bloqueado = (bool)dt.Rows[0]["Bloqueado"];
                        usuario.Email = (string)dt.Rows[0]["Email"];
                        usuario.RutaArchivoFoto = (string)dt.Rows[0]["RutaArchivoFoto"];

                    }
                }
                return usuario;
            }
            catch (Exception ex)
            {
                usuario.mensaje.CodigoMensaje = 1;
                usuario.mensaje.DescripcionMensaje = ex.Message;
                return usuario;
            }
            finally
            {
                ds.Dispose();
                ds.Clear();
                dt.Dispose();
                dt.Clear();
            }
        }

        public List<Sucursal> ObtenerSucursales(int Gestor)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            List<Sucursal> lista = new List<Sucursal>();
            try
            {

                if (Gestor == DatosGlobales.GestorSqlServer) { ds = ConexionSqlServer.GDatos.TraerDataSet(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paListarSucursal"); }
                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0].Copy();
                    foreach (DataRow row in dt.Rows)
                    {
                        sucursal = new Sucursal
                        {
                            Id = (int)row["Id"],
                            BPLId = (int)row["BPLId"],
                            BPLName = row["BPLName"] as string ?? string.Empty,
                        };
                        lista.Add(sucursal);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("SUCEDIO UN ERROR EN LA CAPA DE DATOS [ObtenerSucursales]: " + ex.Message);
            }
            finally
            {
                ds.Dispose();
                ds.Clear();
                dt.Dispose();
                dt.Clear();
            }
            return lista;
        }

        public async Task<Usuario> ObtenerDatosUsuarioAutenticadoLdap(int Gestor, string Logueo, string Ip)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnSeguridadSql, "Seguridad.paObtenerDatosUsuarioAutenticadoLdap", Logueo, Ip);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        usuario.mensaje.CodigoMensaje = 1;
                        usuario.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();
                        usuario.mensaje.DescripcionMensaje = "SUCEDIO UN ERROR EN LA CAPA DE DATOS [AutenticarUsuario], VERIFIQUE CONSOLA";
                        return usuario;
                    }
                    else {
						usuario.mensaje.CodigoMensaje = 0;
					}
                    dt = ds.Tables[0].Copy();
                    if (dt.Rows.Count > 0)
                    {
                        usuario.IdUsuario = (int)dt.Rows[0]["IdUsuario"];
                        usuario.persona.IdPersona = (int)dt.Rows[0]["IdPersona"];
                        usuario.persona.NombreCompleto = (string)dt.Rows[0]["NombreCompleto"];
                        usuario.catalogotipousuario.IdCatalogo = (int)dt.Rows[0]["IdCatalogoTipoUsuario"];
                        usuario.Logueo = (string)dt.Rows[0]["Logueo"];
                        usuario.Bloqueado = (bool)dt.Rows[0]["Bloqueado"];
                        //usuario.Bloqueado = (bool)dt.Rows[0]["Bloqueado"];
                        usuario.Email = (string)dt.Rows[0]["Email"];
                        usuario.persona.Celular = (string)dt.Rows[0]["NumeroCelular"];
                        usuario.persona.TelefonoFijo = (string)dt.Rows[0]["TelefonoFijo"];
                        usuario.persona.Direccion = (string)dt.Rows[0]["Direccion"];
                        usuario.persona.NumeroDocumento = (string)dt.Rows[0]["NumeroDocumento"];
                        usuario.IdAdministrado = (int)dt.Rows[0]["IdAdministrado"];
                        usuario.persona.catalogotipodocumentopersonal.IdCatalogo = (int)dt.Rows[0]["IdCatalogoTipoDocumentoPersonal"];



                        usuario.RutaArchivoFoto = (string)dt.Rows[0]["RutaArchivoFoto"];
                        if (usuario.Bloqueado)
                        {
                            usuario.mensaje.CodigoMensaje = 1;
                            usuario.mensaje.DescripcionMensaje = "USUARIO BLOQUEADO COMUNIQUESE CON EL ADMINSTRADOR";
                        }
                    }
                    else
                    {
                        usuario.mensaje.CodigoMensaje = 1;
                        usuario.mensaje.DescripcionMensaje = "USUARIO O CONTRASEÑA NO EXISTEN";
                    }
                }
                return usuario;
            }
            catch (Exception ex)
            {
                usuario.mensaje.CodigoMensaje = 1;
                usuario.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR AL AUTENTICAR";
                usuario.mensaje.DescripcionMensajeSistema = ex.Message;
                return usuario;
            }
            finally
            {
                ds.Dispose();
                ds.Clear();
                dt.Dispose();
                dt.Clear();
            }
        }
		public Usuario ObtenerDatosUsuarioAutenticadoLdapNoAsincrono(int Gestor, string Logueo)
		{
			DataSet ds = new DataSet();
			DataTable dt = new DataTable();
			try
			{
				if (Gestor == DatosGlobales.GestorSqlServer)
				{
					ds = ConexionSqlServer.GDatos.TraerDataSet(DatosGlobales.ListaConexiones.cnSeguridadSql, "Seguridad.paObtenerDatosUsuarioAutenticadoLdap", Logueo,"IP");
					if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
					{
						usuario.mensaje.CodigoMensaje = 1;
						usuario.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();
						usuario.mensaje.DescripcionMensaje = "SUCEDIO UN ERROR EN LA CAPA DE DATOS [AutenticarUsuario], VERIFIQUE CONSOLA";
						return usuario;
					}
					dt = ds.Tables[0].Copy();
					if (dt.Rows.Count > 0)
					{
						usuario.IdUsuario = (int)dt.Rows[0]["IdUsuario"];
						usuario.persona.IdPersona = (int)dt.Rows[0]["IdPersona"];
						usuario.persona.NombreCompleto = (string)dt.Rows[0]["NombreCompleto"];
						usuario.catalogotipousuario.IdCatalogo = (int)dt.Rows[0]["IdCatalogoTipoUsuario"];
						usuario.Logueo = (string)dt.Rows[0]["Logueo"];
						usuario.Bloqueado = (bool)dt.Rows[0]["Bloqueado"];
						usuario.Email = (string)dt.Rows[0]["Email"];
						usuario.persona.Celular = (string)dt.Rows[0]["NumeroCelular"];
						usuario.persona.TelefonoFijo = (string)dt.Rows[0]["TelefonoFijo"];
						usuario.persona.Direccion = (string)dt.Rows[0]["Direccion"];
						usuario.persona.NumeroDocumento = (string)dt.Rows[0]["NumeroDocumento"];
						usuario.persona.catalogotipodocumentopersonal.IdCatalogo = (int)dt.Rows[0]["IdCatalogoTipoDocumentoPersonal"];
                        usuario.IdAdministrado = (int)dt.Rows[0]["IdAdministrado"];


                        usuario.RutaArchivoFoto = (string)dt.Rows[0]["RutaArchivoFoto"];
						if (usuario.Bloqueado)
						{
							usuario.mensaje.CodigoMensaje = 1;
							usuario.mensaje.DescripcionMensaje = "USUARIO BLOQUEADO COMUNIQUESE CON EL ADMINSTRADOR";
						}
					}
					else
					{
						usuario.mensaje.CodigoMensaje = 1;
						usuario.mensaje.DescripcionMensaje = "USUARIO O CONTRASEÑA NO EXISTEN";
					}
				}
				return usuario;
			}
			catch (Exception ex)
			{
				usuario.mensaje.CodigoMensaje = 1;
				usuario.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR AL AUTENTICAR";
				usuario.mensaje.DescripcionMensajeSistema = ex.Message;
				return usuario;
			}
			finally
			{
				ds.Dispose();
				ds.Clear();
				dt.Dispose();
				dt.Clear();
			}
		}
		public async Task<Usuario> AutenticarUsuarioAsync(int Gestor, string Logueo, string Clave, string Ip)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnSeguridadSql, "Seguridad.paAutenticarUsuario", Logueo, Clave, Ip);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        usuario.mensaje.CodigoMensaje = 1;
                        usuario.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();
                        usuario.mensaje.DescripcionMensaje = "SUCEDIO UN ERROR EN LA CAPA DE DATOS [AutenticarUsuario], VERIFIQUE CONSOLA";
                        return usuario;
                    }
                    dt = ds.Tables[0].Copy();
                    if (dt.Rows.Count > 0)
                    {
                        usuario.IdUsuario = (int)dt.Rows[0]["IdUsuario"];
                        usuario.persona.IdPersona = (int)dt.Rows[0]["IdPersona"];
                        usuario.persona.NombreCompleto = (string)dt.Rows[0]["NombreCompleto"];
                        usuario.catalogotipousuario.IdCatalogo = (int)dt.Rows[0]["IdCatalogoTipoUsuario"];
                        usuario.Logueo = (string)dt.Rows[0]["Logueo"];
                        //usuario.Bloqueado = (bool)dt.Rows[0]["Bloqueado"];
                        usuario.Bloqueado = (bool)dt.Rows[0]["Bloqueado"];
                        usuario.Email = (string)dt.Rows[0]["Email"];

                        usuario.persona.Celular = (string)dt.Rows[0]["NumeroCelular"];
                        usuario.persona.TelefonoFijo = (string)dt.Rows[0]["TelefonoFijo"];
                        usuario.persona.Direccion = (string)dt.Rows[0]["Direccion"];
                        usuario.persona.NumeroDocumento = (string)dt.Rows[0]["NumeroDocumento"];
                        usuario.RutaArchivoFoto = (string)dt.Rows[0]["RutaArchivoFoto"];
                        usuario.persona.catalogotipodocumentopersonal.IdCatalogo = (int)dt.Rows[0]["IdCatalogoTipoDocumentoPersonal"];
                        usuario.IdAdministrado = (int)dt.Rows[0]["IdAdministrado"];
                        if (usuario.Bloqueado)
                        {
                            usuario.mensaje.CodigoMensaje = 1;
                            usuario.mensaje.DescripcionMensaje = "USUARIO BLOQUEADO COMUNIQUESE CON EL ADMINSTRADOR";
                        }
                    }
                    else
                    {
                        usuario.mensaje.CodigoMensaje = 1;
                        usuario.mensaje.DescripcionMensaje = "USUARIO O CONTRASEÑA NO EXISTEN";
                    }
                }
                if (Gestor == DatosGlobales.GestorApi)
                {
                    ClienteApi cla = new ClienteApi();
                    var parametroExtraEnjson = new { NombreUsuario = Logueo, Clave = Clave };//la estructura debe ser igual al Swagger en el API
                    dynamic Datos = await cla.EjecutarDataDinamicaPostAsync("Usuario/AutenticarUsuario", parametroExtraEnjson);

                    usuario.IdUsuario = (int)Datos.dt.IdUsuario;
                    usuario.persona.IdPersona = (int)Datos.dt.IdPersona;
                    usuario.persona.NombreCompleto = (string)Datos.dt.NombreCompleto;
                    usuario.catalogotipousuario.IdCatalogo = (int)Datos.dt.IdCatalogoTipoUsuario;
                    usuario.Logueo = (string)Datos.dt.Logueo;
                    usuario.Bloqueado = (bool)Datos.dt.Bloqueado;
                    usuario.Email = (string)Datos.dt.Email;
                    usuario.RutaArchivoFoto = (string)Datos.dt.RutaArchivoFoto;
                    //  usuario = JsonConvert.DeserializeObject<Usuario>(Datos.dt);

                    if (usuario.Bloqueado)
                    {
                        usuario.mensaje.CodigoMensaje = 1;
                        usuario.mensaje.DescripcionMensaje = "USUARIO BLOQUEADO COMUNIQUESE CON EL ADMINISTRADOR";
                    }
                    if (usuario.IdUsuario == 0)
                    {
                        usuario.mensaje.CodigoMensaje = 1;
                        usuario.mensaje.DescripcionMensaje = "USUARIO O CONTRASEÑA NO EXISTEN";
                    }
                }
                return usuario;
            }
            catch (Exception ex)
            {
                usuario.mensaje.CodigoMensaje = 1;
                usuario.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR AL AUTENTICAR";
                usuario.mensaje.DescripcionMensajeSistema = ex.Message;
                return usuario;
            }
            finally
            {
                ds.Dispose();
                ds.Clear();
                dt.Dispose();
                dt.Clear();
            }
        }
        public Usuario AutenticarUsuario(int Gestor, string Logueo, string Clave)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    ds = ConexionSqlServer.GDatos.TraerDataSet(DatosGlobales.ListaConexiones.cnSeguridadSql, "Seguridad.paAutenticarUsuario", Logueo, Clave);

                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        usuario.mensaje.CodigoMensaje = 1;
                        usuario.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();
                        usuario.mensaje.DescripcionMensaje = "SUCEDIO UN ERROR EN LA CAPA DE DATOS [AutenticarUsuario], VERIFIQUE CONSOLA";
                        return usuario;
                    }
                    dt = ds.Tables[0].Copy();
                    if (dt.Rows.Count > 0)
                    {
                        usuario.IdUsuario = (int)dt.Rows[0]["IdUsuario"];
                        usuario.persona.IdPersona = (int)dt.Rows[0]["IdPersona"];
                        usuario.persona.NombreCompleto = (string)dt.Rows[0]["NombreCompleto"];
                        usuario.catalogotipousuario.IdCatalogo = (int)dt.Rows[0]["IdCatalogoTipoUsuario"];
                        usuario.Logueo = (string)dt.Rows[0]["Logueo"];

                        usuario.Bloqueado = (bool)dt.Rows[0]["Bloqueado"];
                        usuario.Email = (string)dt.Rows[0]["Email"];
                        usuario.RutaArchivoFoto = (string)dt.Rows[0]["RutaArchivoFoto"];
                        if (usuario.Bloqueado)
                        {
                            usuario.mensaje.CodigoMensaje = 1;
                            usuario.mensaje.DescripcionMensaje = "USUARIO BLOQUEADO COMUNIQUESE CON EL ADMINSTRADOR";
                        }
                    }
                    else
                    {
                        usuario.mensaje.CodigoMensaje = 1;
                        usuario.mensaje.DescripcionMensaje = "USUARIO O CONTRASEÑA NO EXISTEN";
                    }
                }
                return usuario;
            }
            catch (Exception ex)
            {
                usuario.mensaje.CodigoMensaje = 1;
                usuario.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR AL AUTENTICAR";
                usuario.mensaje.DescripcionMensajeSistema = ex.Message;
                return usuario;
            }
            finally
            {
                ds.Dispose();
                ds.Clear();
                dt.Dispose();
                dt.Clear();
            }
        }
        public async Task<Usuario> GuardarFotoUsuarioAsync(int Gestor, int IdUsuario, string RutaArchivoFoto, int IdUsuarioAuditoria)
        {
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    System.Object[] ParamtrosOutPut = null;
                    ParamtrosOutPut = await ConexionSqlServer.GDatos.EjecutarAsync(DatosGlobales.ListaConexiones.cnSeguridadSql, "Seguridad.paGuardarFotoUsuario", IdUsuario, RutaArchivoFoto, IdUsuarioAuditoria, "", 0);
                    usuario.IdUsuario = Convert.ToInt32(ParamtrosOutPut[0].ToString());
                    usuario.mensaje.DescripcionMensaje = ParamtrosOutPut[1].ToString();
                    usuario.mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[2].ToString());
                }
                return usuario;
            }
            catch (Exception ex)
            {
                usuario.mensaje.CodigoMensaje = 1;
                usuario.mensaje.DescripcionMensaje = ex.Message;
                return usuario;
            }
        }
        public Usuario GuardarFotoUsuario(int Gestor, int IdUsuario, string RutaArchivoFoto, int IdUsuarioAuditoria)
        {
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    System.Object[] ParamtrosOutPut = null;
                    ParamtrosOutPut = ConexionSqlServer.GDatos.Ejecutar(DatosGlobales.ListaConexiones.cnSeguridadSql, "Seguridad.paGuardarFotoUsuario", IdUsuario, RutaArchivoFoto, IdUsuarioAuditoria, "", 0);
                    usuario.IdUsuario = Convert.ToInt32(ParamtrosOutPut[0].ToString());
                    usuario.mensaje.DescripcionMensaje = ParamtrosOutPut[1].ToString();
                    usuario.mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[2].ToString());
                }
                return usuario;
            }
            catch (Exception ex)
            {
                usuario.mensaje.CodigoMensaje = 1;
                usuario.mensaje.DescripcionMensaje = ex.Message;
                return usuario;
            }
        }
        public async Task<Usuario> GuardaUsuarioAsync(int Gestor, int IdUsuario, int IdPersona, int IdCatalogoTipoUsuario, string Logueo, string Clave, bool Bloqueado, bool EsInstitucion, string Email, string RutaArchivoFoto, int IdUsuarioAuditoria)
        {
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    System.Object[] ParamtrosOutPut = null;
                    ParamtrosOutPut = await ConexionSqlServer.GDatos.EjecutarAsync(DatosGlobales.ListaConexiones.cnSeguridadSql, "Seguridad.paGuardarUsuario", IdUsuario, IdPersona, IdCatalogoTipoUsuario, Logueo, Clave, Bloqueado, EsInstitucion, Email, RutaArchivoFoto, IdUsuarioAuditoria, "", 0);
                    usuario.IdUsuario = Convert.ToInt32(ParamtrosOutPut[0].ToString());
                    usuario.mensaje.DescripcionMensaje = ParamtrosOutPut[1].ToString();
                    usuario.mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[2].ToString());
                }
                if (Gestor == DatosGlobales.GestorApi)
                {
                    ClienteApi cla = new ClienteApi();
                    dynamic Datos = await cla.EjecutarDataDinamicaPostAsync("Usuario/GuardarUsuario?IdUsuario=" + IdUsuario + "&IdPersona=" + IdPersona + "&IdCatalogoTipoUsuario=" + IdCatalogoTipoUsuario + "&Logueo=" + Logueo + "&Clave=" + Clave + "&Bloqueado=" + Bloqueado + "&Email=" + Email + "&RutaArchivoFoto=" + RutaArchivoFoto + "&IdUsuarioAuditoria=" + IdUsuarioAuditoria);
                    usuario.IdUsuario = Convert.ToInt32(Datos.respuesta.idUsuario);
                    usuario.mensaje.DescripcionMensaje = Datos.respuesta.descripcionMensaje;
                    usuario.mensaje.CodigoMensaje = Convert.ToInt32(Datos.respuesta.codigoMensaje);
                }
                return usuario;
            }
            catch (Exception ex)
            {
                usuario.mensaje.CodigoMensaje = 1;
                usuario.mensaje.DescripcionMensaje = ex.Message;
                return usuario;
            }
        }
        public Usuario GuardaUsuario(int Gestor, int IdUsuario, int IdPersona, int IdCatalogoTipoUsuario, string Logueo, string Clave, bool Bloqueado, string Email, string RutaArchivoFoto, int IdUsuarioAuditoria)
        {
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    System.Object[] ParamtrosOutPut = null;
                    ParamtrosOutPut = ConexionSqlServer.GDatos.Ejecutar(DatosGlobales.ListaConexiones.cnSeguridadSql, "Seguridad.paGuardarUsuario", IdUsuario, IdPersona, IdCatalogoTipoUsuario, Logueo, Clave, Bloqueado, Email, RutaArchivoFoto, IdUsuarioAuditoria, "", 0);
                    usuario.IdUsuario = Convert.ToInt32(ParamtrosOutPut[0].ToString());
                    usuario.mensaje.DescripcionMensaje = ParamtrosOutPut[1].ToString();
                    usuario.mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[2].ToString());
                }
                return usuario;
            }
            catch (Exception ex)
            {
                usuario.mensaje.CodigoMensaje = 1;
                usuario.mensaje.DescripcionMensaje = ex.Message;
                return usuario;
            }
        }
        public async Task<Usuario> GuardaCambioClaveAsync(int Gestor, int IdUsuario, string Clave, string ClaveAnterior, int IdUsuarioAuditoria)
        {
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    System.Object[] ParamtrosOutPut = null;
                    ParamtrosOutPut = await ConexionSqlServer.GDatos.EjecutarAsync(DatosGlobales.ListaConexiones.cnSeguridadSql, "Seguridad.paGuardaCambioClave", IdUsuario, Clave, ClaveAnterior, IdUsuarioAuditoria, "", 0);
                    usuario.IdUsuario = Convert.ToInt32(ParamtrosOutPut[0].ToString());
                    usuario.mensaje.DescripcionMensaje = ParamtrosOutPut[1].ToString();
                    usuario.mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[2].ToString());
                }
                return usuario;
            }
            catch (Exception ex)
            {
                usuario.mensaje.CodigoMensaje = 1;
                usuario.mensaje.DescripcionMensaje = ex.Message;
                return usuario;
            }
        }
        public Usuario GuardaCambioClave(int Gestor, int IdUsuario, string Clave, string ClaveAnterior, int IdUsuarioAuditoria)
        {
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    System.Object[] ParamtrosOutPut = null;
                    ParamtrosOutPut = ConexionSqlServer.GDatos.Ejecutar(DatosGlobales.ListaConexiones.cnSeguridadSql, "Seguridad.paGuardaCambioClave", IdUsuario, Clave, ClaveAnterior, IdUsuarioAuditoria, "", 0);
                    usuario.IdUsuario = Convert.ToInt32(ParamtrosOutPut[0].ToString());
                    usuario.mensaje.DescripcionMensaje = ParamtrosOutPut[1].ToString();
                    usuario.mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[2].ToString());
                }
                return usuario;
            }
            catch (Exception ex)
            {
                usuario.mensaje.CodigoMensaje = 1;
                usuario.mensaje.DescripcionMensaje = ex.Message;
                return usuario;
            }
        }

        public async Task<ListaUsuario> ListarUsuarioPorAutoComplete(int Gestor, int IdUsuarioAuditoria, string NombreUsuario)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {

                    ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnSeguridadSql, "Seguridad.paListarUsuarioPorAutoComplete", IdUsuarioAuditoria, NombreUsuario);
                    if (ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0].Copy();

                        foreach (DataRow row in dt.Rows)
                        {
                            usuario = new Seguridad.Usuario();
                            usuario.IdUsuario = (int)row["IdUsuario"];
                            usuario.persona.NombreCompleto = (string)row["NombreCompleto"];
                            Lista.lista.Add(usuario);
                        }
                    }
                }
                return await Task.FromResult(Lista);
            }
            catch (Exception ex)
            {
                Lista.mensaje.CodigoMensaje = 1;
                Lista.mensaje.DescripcionMensaje = ex.Message;
                return await Task.FromResult(Lista);
            }
            finally
            {
                ds.Dispose();
                ds.Clear();
                dt.Dispose();
                dt.Clear();
            }
        }
        public async Task<ListaUsuario> ListarUsuarioAsync(int Gestor, int Bloqueado, int EsInstitucion, int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {

                    ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnSeguridadSql, "Seguridad.paListarUsuario", Bloqueado, EsInstitucion, IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
                    if (ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0].Copy();
                        DataTable dtParametros = null;
                        dtParametros = ds.Tables[1].Copy();
                        Lista.paginacion.TotalRegistros = Convert.ToInt32(dtParametros.Rows[0][0]);
                        foreach (DataRow row in dt.Rows)
                        {
                            usuario = new Seguridad.Usuario();
                            usuario.IdUsuario = (int)row["IdUsuario"];
                            usuario.persona.NombreCompleto = (string)row["NombreCompleto"];
                            usuario.catalogotipousuario.Descripcion = (string)row["CatalogoTipoUsuario"];
                            usuario.catalogotipousuario.IdCatalogo = (int)row["IdCatalogoTipoUsuario"];
                            usuario.Logueo = (string)row["Logueo"];
                            usuario.CantidadToken = (int)row["CantidadToken"];
                            usuario.CantidadPerfil = (int)row["CantidadPerfil"];
                            usuario.Bloqueado = (bool)row["Bloqueado"];
                            usuario.EsInstitucion = (bool)row["EsInstitucion"];

                            usuario.Email = (string)row["Email"];
                            usuario.RutaArchivoFoto = (string)row["RutaArchivoFoto"];
                            Lista.lista.Add(usuario);
                        }
                    }
                }
                if (Gestor == DatosGlobales.GestorApi)
                {
                    ClienteApi cla = new ClienteApi();
                    dynamic datos = await cla.TraerDataDinamicaGetAsync("Usuario/ListarUsuario?IdUsuarioAuditoria=" + IdUsuarioAuditoria.ToString() + "&NumeroPagina=" + NumeroPagina.ToString() + "&DimensionPagina=" + DimensionPagina.ToString());
                    foreach (var item in ((IEnumerable<dynamic>)datos.dt))
                    {
                        usuario = new Seguridad.Usuario();
                        usuario.IdUsuario = (int)item.IdUsuario;
                        usuario.persona.NombreCompleto = (string)item.NombreCompleto;
                        usuario.catalogotipousuario.Descripcion = (string)item.CatalogoTipoUsuario;
                        usuario.catalogotipousuario.IdCatalogo = (int)item.IdCatalogoTipoUsuario;
                        usuario.Logueo = (string)item.Logueo;
                        usuario.CantidadToken = (int)item.CantidadToken;
                        usuario.CantidadPerfil = (int)item.CantidadPerfil;
                        usuario.Bloqueado = (bool)item.Bloqueado;
                        usuario.Email = (string)item.Email;
                        usuario.RutaArchivoFoto = (string)item.RutaArchivoFoto;
                        Lista.lista.Add(usuario);
                    }
                    Lista.paginacion.TotalRegistros = Convert.ToInt32(datos.dtParametros);
                }
                return await Task.FromResult(Lista);
            }
            catch (Exception ex)
            {
                Lista.mensaje.CodigoMensaje = 1;
                Lista.mensaje.DescripcionMensaje = ex.Message;
                return await Task.FromResult(Lista);
            }
            finally
            {
                ds.Dispose();
                ds.Clear();
                dt.Dispose();
                dt.Clear();
            }
        }
        public async Task<Usuario> ObtenerUsuarioAsync(int Gestor, int IdUsuario)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (IdUsuario == 0)
                {
                    return usuario;
                }

                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnSeguridadSql, "Seguridad.paObtenerUsuario", IdUsuario);
                    dt = ds.Tables[0].Copy();
                    if (dt.Rows.Count > 0)
                    {
                        usuario.IdUsuario = (int)dt.Rows[0]["IdUsuario"];
                        usuario.persona.IdPersona = (int)dt.Rows[0]["IdPersona"];
                        usuario.persona.NombreCompleto = (string)dt.Rows[0]["NombreCompleto"];
                        usuario.catalogotipousuario.IdCatalogo = (int)dt.Rows[0]["IdCatalogoTipoUsuario"];
                        usuario.Logueo = (string)dt.Rows[0]["Logueo"];
                        usuario.Clave = (string)dt.Rows[0]["Clave"];
                        usuario.Bloqueado = (bool)dt.Rows[0]["Bloqueado"];
                        usuario.EsInstitucion = (bool)dt.Rows[0]["EsInstitucion"];
                        usuario.Email = (string)dt.Rows[0]["Email"];
                        usuario.RutaArchivoFoto = (string)dt.Rows[0]["RutaArchivoFoto"];
                    }
                }
                if (Gestor == DatosGlobales.GestorApi)
                {
                    ClienteApi cla = new ClienteApi();
                    dynamic Datos = await cla.TraerDataDinamicaGetAsync("Usuario/ObtenerUsuario?IdUsuario=" + IdUsuario.ToString());
                    usuario.IdUsuario = (int)Datos.dt.IdUsuario;
                    usuario.persona.IdPersona = (int)Datos.dt.IdPersona;
                    usuario.persona.NombreCompleto = (string)Datos.dt.NombreCompleto;
                    usuario.catalogotipousuario.IdCatalogo = (int)Datos.dt.IdCatalogoTipoUsuario;
                    usuario.Logueo = (string)Datos.dt.Logueo;
                    usuario.Clave = (string)Datos.dt.Clave;
                    usuario.Bloqueado = (bool)Datos.dt.Bloqueado;
                    usuario.Email = (string)Datos.dt.Email;
                    usuario.RutaArchivoFoto = (string)Datos.dt.RutaArchivoFoto;
                }
                return usuario;
            }
            catch (Exception ex)
            {
                usuario.mensaje.CodigoMensaje = 1;
                usuario.mensaje.DescripcionMensaje = ex.Message;
                return usuario;
            }
            finally
            {
                ds.Dispose();
                ds.Clear();
                dt.Dispose();
                dt.Clear();
            }
        }
        public ListaUsuario ListarUsuario(int Gestor, int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    ds = ConexionSqlServer.GDatos.TraerDataSet(DatosGlobales.ListaConexiones.cnSeguridadSql, "Seguridad.paListarUsuario", IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
                }

                if (ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0].Copy();
                    DataTable dtParametros = null;
                    dtParametros = ds.Tables[1].Copy();
                    Lista.paginacion.TotalRegistros = Convert.ToInt32(dtParametros.Rows[0][0]);
                }

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        usuario = new Seguridad.Usuario();
                        usuario.IdUsuario = (int)row["IdUsuario"];

                        usuario.persona.NombreCompleto = (string)row["NombreCompleto"];
                        usuario.catalogotipousuario.Descripcion = (string)row["CatalogoTipoUsuario"];
                        usuario.Logueo = (string)row["Logueo"];

                        usuario.Bloqueado = (bool)row["Bloqueado"];
                        usuario.Email = (string)row["Email"];
                        usuario.RutaArchivoFoto = (string)row["RutaArchivoFoto"];

                        Lista.lista.Add(usuario);
                    }
                }
                return Lista;
            }
            catch (Exception ex)
            {
                Lista.mensaje.CodigoMensaje = 1;
                Lista.mensaje.DescripcionMensaje = ex.Message;
                return Lista;
            }
            finally
            {
                ds.Dispose();
                ds.Clear();
                dt.Dispose();
                dt.Clear();
            }
        }
        public async Task<Usuario> BuscarEmail(int Gestor, string Email)
        {

            if (Email == "")
            {
                return usuario;
            }
            try
            {
                DataTable dt = new DataTable();
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    DataSet ds = null;
                    ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnSeguridadSql, "Seguridad.paBuscarEmail", Email);
                    if (ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0].Copy();
                        if (dt.Rows.Count > 0)
                        {
                            usuario.IdUsuario = (int)dt.Rows[0]["IdUsuario"];
                            usuario.Clave = (string)dt.Rows[0]["Clave"];
                        }
                    }
                }
                return usuario;
            }
            catch (Exception ex)
            {
                usuario.mensaje.CodigoMensaje = 1;
                usuario.mensaje.DescripcionMensaje = ex.Message;
                return usuario;
            }
        }
        public async Task<Usuario> BuscarUsuario(int Gestor, string Usuario)
        {
            if (Usuario == "")
            {
                return usuario;
            }
            try
            {
                DataTable dt = new DataTable();
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    DataSet ds = null;
                    ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnSeguridadSql, "Seguridad.paBuscarUsuario", Usuario);
                    if (ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0].Copy();
                        if (dt.Rows.Count > 0)
                        {
                            usuario.IdUsuario = (int)dt.Rows[0]["IdUsuario"];
                        }
                    }
                }
                return usuario;
            }
            catch (Exception ex)
            {
                usuario.mensaje.CodigoMensaje = 1;
                usuario.mensaje.DescripcionMensaje = ex.Message;
                return usuario;
            }
        }
        public async Task<Usuario> GuardarNuevaCuentaCasilla(int Gestor, int CatalogoTipoDocumentoPersonal, string NumeroDocumento, string Nombres, string ApellidoPaterno, string ApellidoMaterno, int Pais, string Email, string NombreUsuario, string FechaNacimiento, int Sexo, string LugarNacimiento, string NumeroCelular, string Password,int IdVerificacion, int IdUsuarioAuditoria)
        {
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    System.Object[] ParamtrosOutPut = null;
                    ParamtrosOutPut = await ConexionSqlServer.GDatos.EjecutarAsync(DatosGlobales.ListaConexiones.cnSeguridadSql, "Seguridad.paGuardarNuevaCuentaCasilla",
                                0,
                                CatalogoTipoDocumentoPersonal,
                                NumeroDocumento,
                                Nombres,
                                ApellidoPaterno,
                                ApellidoMaterno,
                                Pais,
                                Email,
                                NombreUsuario,
                                FechaNacimiento,
                                Sexo,
                                LugarNacimiento,
                                NumeroCelular,
                                Password,
                                IdVerificacion,
                                IdUsuarioAuditoria, "", 0);
                    if (Convert.ToInt32(ParamtrosOutPut[100].ToString()) > 0)
                    {
                        usuario.mensaje.CodigoMensaje = 1;
                        usuario.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [GuardarNuevaCuentaCasilla], VERIFICAR CONSOLA";
                        usuario.mensaje.DescripcionMensajeSistema = ParamtrosOutPut[99].ToString();
                        return usuario;
                    }
                    usuario.IdUsuario = Convert.ToInt32(ParamtrosOutPut[0].ToString());
                    usuario.mensaje.DescripcionMensaje = ParamtrosOutPut[1].ToString();
                    usuario.mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[2].ToString());

                }
                return usuario;
            }
            catch (Exception ex)
            {
                usuario.mensaje.CodigoMensaje = 1;
                usuario.mensaje.DescripcionMensaje = ex.Message;
                return usuario;
            }
        }
    }
}
