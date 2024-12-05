using System;
using Seguridad;
using Utilitarios;
using System.Data;
using System.Threading.Tasks;
using ApiLogic;

namespace SeguridadLogic
{
    public class UsuarioPerfilLogic
    {
        private Seguridad.UsuarioPerfil usuarioperfil;
        private Seguridad.ListaUsuarioPerfil lista;
        public UsuarioPerfilLogic()
        {
            usuarioperfil = new Seguridad.UsuarioPerfil();
            lista = new Seguridad.ListaUsuarioPerfil();
        }
        public Mensaje EliminarUsuarioPerfil(int Gestor, int IdUsuarioPerfil, int IdUsuarioAuditoria)
        {
            Mensaje mensaje = new Mensaje();
            if (Gestor == DatosGlobales.GestorSqlServer)
            {
                System.Object[] ParamtrosOutPut = null; 
                ParamtrosOutPut = ConexionSqlServer.GDatos.Ejecutar(DatosGlobales.ListaConexiones.cnSeguridadSql, "Seguridad.paEliminarUsuarioPerfil", IdUsuarioPerfil, IdUsuarioAuditoria, mensaje.DescripcionMensaje, mensaje.CodigoMensaje);
                mensaje.DescripcionMensaje = ParamtrosOutPut[0].ToString();
                mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[1].ToString());
            }
            return mensaje;
        }
        public UsuarioPerfil ObtenerUsuarioPerfil(int Gestor, int IdUsuarioPerfil)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (IdUsuarioPerfil == 0)
                {
                    return usuarioperfil;
                }

                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                     ds = ConexionSqlServer.GDatos.TraerDataSet(DatosGlobales.ListaConexiones.cnSeguridadSql, "Seguridad.paObtenerUsuarioPerfil", IdUsuarioPerfil);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        usuarioperfil.mensaje.CodigoMensaje = 1;
                        usuarioperfil.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ObtenerUsuarioPerfil], VERIFICAR CONSOLA";
                        usuarioperfil.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();

                        return usuarioperfil;
                    }
                    dt = ds.Tables[0].Copy();
                    usuarioperfil.IdUsuarioPerfil = (int)dt.Rows[0]["IdUsuarioPerfil"];
                    usuarioperfil.usuario.IdUsuario = (int)dt.Rows[0]["IdUsuario"];
                    usuarioperfil.usuario.persona.IdPersona = (int)dt.Rows[0]["IdPersona"];
                    usuarioperfil.empleadoperfil.IdEmpleadoPerfil = (int)dt.Rows[0]["IdEmpleadoPerfil"];
                    usuarioperfil.perfil.IdPerfil = (int)dt.Rows[0]["IdPerfil"];
                    usuarioperfil.Activo = (bool)dt.Rows[0]["Activo"];

                }
                return usuarioperfil;
            }
            catch (Exception ex)
            {
                usuarioperfil.mensaje.CodigoMensaje = 1;
                usuarioperfil.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ObtenerUsuarioPerfil], VERIFICAR CONSOLA";
                usuarioperfil.mensaje.DescripcionMensajeSistema = ex.Message;
                return usuarioperfil;
            }
            finally
            {
                ds.Dispose();
                ds.Clear();
                dt.Dispose();
                dt.Clear();
            }
        }
        public UsuarioPerfil ObtenerUsuarioPorEmpleadoPerfil(int Gestor, int IdEmpleadoPerfil)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (IdEmpleadoPerfil == 0)
                {
                    return usuarioperfil;
                }

                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    ds = ConexionSqlServer.GDatos.TraerDataSet(DatosGlobales.ListaConexiones.cnSeguridadSql, "Seguridad.paObtenerUsuarioPorEmpleadoPerfil", IdEmpleadoPerfil);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        usuarioperfil.mensaje.CodigoMensaje = 1;
                        usuarioperfil.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ObtenerUsuarioPerfil], VERIFICAR CONSOLA";
                        usuarioperfil.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();

                        return usuarioperfil;
                    }
                    dt = ds.Tables[0].Copy();                    
                    usuarioperfil.usuario.Email = (string)dt.Rows[0]["Email"];
                    usuarioperfil.usuario.persona.NombreCompleto = (string)dt.Rows[0]["NombreCompleto"];                    
                }
                return usuarioperfil;
            }
            catch (Exception ex)
            {
                usuarioperfil.mensaje.CodigoMensaje = 1;
                usuarioperfil.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ObtenerUsuarioPerfil], VERIFICAR CONSOLA";
                usuarioperfil.mensaje.DescripcionMensajeSistema = ex.Message;
                return usuarioperfil;
            }
            finally
            {
                ds.Dispose();
                ds.Clear();
                dt.Dispose();
                dt.Clear();
            }
        }
        public ListaUsuarioPerfil ListarComboUsuarioPerfil(int Gestor, int IdUsuario)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                     ds = ConexionSqlServer.GDatos.TraerDataSet(DatosGlobales.ListaConexiones.cnSeguridadSql, "Seguridad.paListarComboUsuarioPerfil", IdUsuario);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarComboEmpleadoPerfil], VERIFICAR CONSOLA";
                        lista.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();
                        return lista;
                    }
                    dt = ds.Tables[0].Copy();
                }
                if (dt.Rows.Count > 0)
                {
                    UsuarioPerfil usuarioperfil = null;
                    foreach (DataRow row in dt.Rows)
                    {
                        usuarioperfil = new UsuarioPerfil();
                        usuarioperfil.empleadoperfil.NombreEmpleadoPerfil = (string)row["NombreEmpleadoPerfil"];
                        usuarioperfil.empleadoperfil.IdEmpleadoPerfil = (int)row["IdEmpleadoPerfil"];
                        lista.lista.Add(usuarioperfil);
                    }
                }
                return lista;
            }
            catch (Exception ex)
            {
                lista.mensaje.CodigoMensaje = 1;
                lista.mensaje.DescripcionMensaje = ex.Message;
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
        public UsuarioPerfil ObtenerAreaCargoPorEmpleadoPerfil(int Gestor, int IdEmpleadoPerfil)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {

                     ds = ConexionSqlServer.GDatos.TraerDataSet(DatosGlobales.ListaConexiones.cnSeguridadSql, "Seguridad.paObtenerAreaCargoPorEmpleadoPerfil", IdEmpleadoPerfil);
                    if (ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0].Copy();
                        if (dt.Rows.Count > 0)
                        {
                            usuarioperfil.IdUsuarioPerfil = (int)dt.Rows[0]["IdUsuarioPerfil"];
                            usuarioperfil.usuario.IdUsuario = (int)dt.Rows[0]["IdUsuario"];
                            usuarioperfil.empleadoperfil.IdEmpleadoPerfil = (int)dt.Rows[0]["IdEmpleadoPerfil"];
                            usuarioperfil.empleadoperfil.NombreEmpleadoPerfil = (string)dt.Rows[0]["NombreEmpleadoPerfil"];
                            usuarioperfil.perfil.IdPerfil = (int)dt.Rows[0]["IdPerfil"];
                            usuarioperfil.Activo = (bool)dt.Rows[0]["Activo"];
                            usuarioperfil.empleadoperfil.cargo.NombreCargo = (string)dt.Rows[0]["NombreCargo"];
                            usuarioperfil.empleadoperfil.cargo.IdCargo = (int)dt.Rows[0]["IdCargo"];
                            usuarioperfil.empleadoperfil.area.NombreArea = (string)dt.Rows[0]["NombreArea"];
                            usuarioperfil.empleadoperfil.empresasede.IdEmpresaSede = (int)dt.Rows[0]["IdEmpresaSede"];
                            usuarioperfil.empleadoperfil.empresasede.NombreSede = (string)dt.Rows[0]["NombreSede"];
                            usuarioperfil.empleadoperfil.area.IdArea = (int)dt.Rows[0]["IdArea"];
                            usuarioperfil.empleadoperfil.empresasede.empresa.NombreEmpresa = (string)dt.Rows[0]["NombreEmpresa"];
                            usuarioperfil.empleadoperfil.empresasede.empresa.IdEmpresa = (int)dt.Rows[0]["IdEmpresa"];
                            usuarioperfil.empleadoperfil.empresasede.empresa.IdEmpresaPadre = (int)dt.Rows[0]["IdEmpresaPadre"];
                        }
                    }
                }
                return usuarioperfil;
            }
            catch (Exception ex)
            {
                usuarioperfil.mensaje.CodigoMensaje = 1;
                usuarioperfil.mensaje.DescripcionMensaje = ex.Message;
                return usuarioperfil;
            }
            finally
            {
                ds.Dispose();
                ds.Clear();
                dt.Dispose();
                dt.Clear();
            }
        }
        public async Task<UsuarioPerfil> ObtenerAreaCargoPorEmpleadoPerfilAsync(int Gestor, int IdEmpleadoPerfil)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {

                     ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnSeguridadSql, "Seguridad.paObtenerAreaCargoPorEmpleadoPerfil", IdEmpleadoPerfil);
                    if (ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0].Copy();
                        if (dt.Rows.Count > 0)
                        {
                            //usuarioperfil.IdUsuarioPerfil = (int)dt.Rows[0]["IdUsuarioPerfil"];
                            usuarioperfil.usuario.IdUsuario = (int)dt.Rows[0]["IdUsuario"];
                            usuarioperfil.empleadoperfil.empleado.IdEmpleado = (int)dt.Rows[0]["IdEmpleado"];
                            usuarioperfil.empleadoperfil.empleado.catalogotipoempleado.Detalle = (string)dt.Rows[0]["CatalogoTipoEmpleado"];
                            usuarioperfil.empleadoperfil.empleado.catalogotipoempleado.IdCatalogo = (int)dt.Rows[0]["IdCatalogoTipoEmpleado"];

                            usuarioperfil.empleadoperfil.NombreEmpleadoPerfil = (string)dt.Rows[0]["NombreEmpleadoPerfil"];
                            usuarioperfil.perfil.IdPerfil = (int)dt.Rows[0]["IdPerfil"];
                            usuarioperfil.Activo = (bool)dt.Rows[0]["Activo"];
                            usuarioperfil.empleadoperfil.cargo.NombreCargo = (string)dt.Rows[0]["NombreCargo"];
                            usuarioperfil.empleadoperfil.cargo.IdCargo = (int)dt.Rows[0]["IdCargo"];
                            usuarioperfil.empleadoperfil.area.NombreArea = (string)dt.Rows[0]["NombreArea"];
                            usuarioperfil.empleadoperfil.area.Abreviatura = (string)dt.Rows[0]["Abreviatura"];
                            usuarioperfil.empleadoperfil.empresasede.IdEmpresaSede = (int)dt.Rows[0]["IdEmpresaSede"];
                            usuarioperfil.empleadoperfil.empresasede.NombreSede = (string)dt.Rows[0]["NombreSede"];
                            usuarioperfil.empleadoperfil.area.IdArea = (int)dt.Rows[0]["IdArea"];
                            usuarioperfil.empleadoperfil.empresasede.empresa.NombreEmpresa = (string)dt.Rows[0]["NombreEmpresa"];
                            usuarioperfil.empleadoperfil.empresasede.empresa.persona.NumeroDocumento = (string)dt.Rows[0]["RucEmpresa"];
                            usuarioperfil.empleadoperfil.empresasede.empresa.persona.NombreCompleto = (string)dt.Rows[0]["NombreCompletoEmpresa"];
                            usuarioperfil.empleadoperfil.empresasede.empresa.IdEmpresa = (int)dt.Rows[0]["IdEmpresa"];
                            usuarioperfil.empleadoperfil.empresasede.empresa.IdEmpresaPadre = (int)dt.Rows[0]["IdEmpresaPadre"];
                            usuarioperfil.empleadoperfil.cargo.catalogotipocargo.IdCatalogo = (int)dt.Rows[0]["IdCatalogoTipoCargo"];
                        }
                    }
                }
                if (Gestor == DatosGlobales.GestorApi)
                {
                    ClienteApi cla = new ClienteApi();
                    dynamic Datos = await cla.TraerDataDinamicaGetAsync("UsuarioPerfil/ObtenerAreaCargoPorEmpleadoPerfil?IdEmpleadoPerfil=" + IdEmpleadoPerfil.ToString());
                    usuarioperfil.IdUsuarioPerfil = (int)Datos.dt.idUsuarioPerfil;
                    usuarioperfil.usuario.IdUsuario = (int)Datos.dt.idUsuario;
                    usuarioperfil.empleadoperfil.IdEmpleadoPerfil = (int)Datos.dt.idEmpleadoPerfil;
                    usuarioperfil.empleadoperfil.NombreEmpleadoPerfil = (string)Datos.dt.nombreEmpleadoPerfil;
                    usuarioperfil.perfil.IdPerfil = (int)Datos.dt.idPerfil;
                    usuarioperfil.Activo = (bool)Datos.dt.activo;
                    usuarioperfil.empleadoperfil.cargo.NombreCargo = (string)Datos.dt.nombreCargo;
                    usuarioperfil.empleadoperfil.cargo.IdCargo = (int)Datos.dt.idCargo;
                    usuarioperfil.empleadoperfil.area.NombreArea = (string)Datos.dt.nombreArea;
                    usuarioperfil.empleadoperfil.area.Abreviatura = (string)Datos.dt.abreviatura;
                    usuarioperfil.empleadoperfil.empresasede.IdEmpresaSede = (int)Datos.dt.idEmpresaSede;
                    usuarioperfil.empleadoperfil.empresasede.NombreSede = (string)Datos.dt.nombreSede;
                    usuarioperfil.empleadoperfil.area.IdArea = (int)Datos.dt.idArea;
                    usuarioperfil.empleadoperfil.empresasede.empresa.NombreEmpresa = (string)Datos.dt.nombreEmpresa;
                    usuarioperfil.empleadoperfil.empresasede.empresa.IdEmpresa = (int)Datos.dt.idEmpresa;
                    usuarioperfil.empleadoperfil.empresasede.empresa.IdEmpresaPadre = (int)Datos.dt.idEmpresaPadre;
                    usuarioperfil.empleadoperfil.cargo.catalogotipocargo.IdCatalogo = (int)Datos.dt.idCatalogoTipoCargo;
                }
                return usuarioperfil;
            }
            catch (Exception ex)
            {
                usuarioperfil.mensaje.CodigoMensaje = 1;
                usuarioperfil.mensaje.DescripcionMensaje = ex.Message;
                return usuarioperfil;
            }
            finally
            {
                ds.Dispose();
                ds.Clear();
                dt.Dispose();
                dt.Clear();
            }
        }
        public UsuarioPerfil GuardaUsuarioPerfil(int Gestor, int IdUsuarioPerfil, int IdUsuario, int IdEmpleadoPerfil, int IdPerfil, bool Activo, int IdUsuarioAuditoria)
        {
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    System.Object[] ParamtrosOutPut = null; 
                    ParamtrosOutPut = ConexionSqlServer.GDatos.Ejecutar(DatosGlobales.ListaConexiones.cnSeguridadSql, "Seguridad.paGuardarUsuarioPerfil", IdUsuarioPerfil, IdUsuario, IdEmpleadoPerfil, IdPerfil, Activo, IdUsuarioAuditoria, "", 0);
                    if (Convert.ToInt32(ParamtrosOutPut[100].ToString()) > 0)
                    {
                        usuarioperfil.mensaje.CodigoMensaje = 1;
                        usuarioperfil.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [GuardaUsuarioPerfil], VERIFICAR CONSOLA";
                        usuarioperfil.mensaje.DescripcionMensajeSistema = ParamtrosOutPut[99].ToString();
                        return usuarioperfil;
                    }
                    usuarioperfil.IdUsuarioPerfil = Convert.ToInt32(ParamtrosOutPut[0].ToString());
                    usuarioperfil.mensaje.DescripcionMensaje = ParamtrosOutPut[1].ToString();
                    usuarioperfil.mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[2].ToString());
                }
                return usuarioperfil;
            }
            catch (Exception ex)
            {
                usuarioperfil.mensaje.CodigoMensaje = 1;
                usuarioperfil.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [GuardaUsuarioPerfil], VERIFICAR CONSOLA";
                usuarioperfil.mensaje.DescripcionMensajeSistema = ex.Message;
                return usuarioperfil;
            }
        }
        public ListaUsuarioPerfil ListarUsuarioPerfil(int Gestor, int IdUsuario, int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                     ds = ConexionSqlServer.GDatos.TraerDataSet(DatosGlobales.ListaConexiones.cnSeguridadSql, "Seguridad.paListarUsuarioPerfil", IdUsuario, IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarUsuarioPerfil], VERIFICAR CONSOLA";
                        lista.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();
                        return lista;
                    }
                    dt = ds.Tables[0].Copy();
                    DataTable dtParametros = null;
                    dtParametros = ds.Tables[1].Copy();
                    lista.paginacion.TotalRegistros = Convert.ToInt32(dtParametros.Rows[0][0]);
                }
                UsuarioPerfil usuarioperfil = null;
                foreach (DataRow row in dt.Rows)
                {
                    usuarioperfil = new UsuarioPerfil();
                    usuarioperfil.IdUsuarioPerfil = (int)row["IdUsuarioPerfil"];
                    usuarioperfil.empleadoperfil.NombreEmpleadoPerfil = (string)row["NombreEmpleadoPerfil"];
                    usuarioperfil.perfil.NombrePerfil = (string)row["NombrePerfil"];
                    usuarioperfil.Activo = (bool)row["Activo"];
                    lista.lista.Add(usuarioperfil);
                }
                return lista;
            }
            catch (Exception ex)
            {
                lista.mensaje.CodigoMensaje = 1;
                lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarUsuarioPerfil], VERIFICAR CONSOLA";
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

        public async Task<int> ValidarAccesoUsuarioPerfilAsync(int Gestor, int IdUsuario, int IdPerfil)
        {
            int acceso = 0;
            if (Gestor == DatosGlobales.GestorSqlServer)
            {
                object[] ParamtrosOutPut = null;
                ParamtrosOutPut = await ConexionSqlServer.GDatos.EjecutarAsync(DatosGlobales.ListaConexiones.cnInventarioSql, "Seguridad.paValidarAccesoPerfilUsuario", IdUsuario, IdPerfil, 0);
                acceso = Convert.ToInt32(ParamtrosOutPut[0].ToString());
            }
            return acceso;
        }

    }
}



