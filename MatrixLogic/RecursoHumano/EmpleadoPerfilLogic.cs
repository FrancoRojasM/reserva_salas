using System;

using Utilitarios;
using System.Data;
using System.Collections.Generic;
using RecursoHumano;
using System.Threading.Tasks;

namespace PersonalLogic
{

    public class EmpleadoPerfilLogic
    {
        private EmpleadoPerfil empleadoperfil;
        private ListaEmpleadoPerfil lista;

        public EmpleadoPerfilLogic()
        {
            empleadoperfil = new EmpleadoPerfil();
            lista = new ListaEmpleadoPerfil();
        }
        public async Task<Mensaje> EliminarEmpleadoPerfilAsync(int Gestor, int IdEmpleadoPerfil, int IdUsuarioAuditoria)
        {
            Mensaje mensaje = new Mensaje();
            if (Gestor == DatosGlobales.GestorSqlServer)
            {
                System.Object[] ParamtrosOutPut = null;
                ParamtrosOutPut = await ConexionSqlServer.GDatos.EjecutarAsync(DatosGlobales.ListaConexiones.cnRecursoHumanoSql, "RecursoHumano.paEliminarEmpleadoPerfil", IdEmpleadoPerfil, IdUsuarioAuditoria, mensaje.DescripcionMensaje, mensaje.CodigoMensaje);
                if (Convert.ToInt32(ParamtrosOutPut[100].ToString()) > 0)
                {
                    mensaje.CodigoMensaje = 1;
                    mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA EN LA CAPA DE DATOS [EliminarEmpleadoPerfil] VERIFICAR CONSOLA";
                    mensaje.DescripcionMensajeSistema = ParamtrosOutPut[99].ToString();
                    return mensaje;
                }
                mensaje.DescripcionMensaje = ParamtrosOutPut[0].ToString();
                mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[1].ToString());
            }
            return mensaje;
        }
        public async Task<EmpleadoPerfil> ObtenerEmpleadoPerfilAsync(int Gestor, int IdEmpleadoPerfil, int IdUsuarioAuditoria)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnRecursoHumanoSql, "RecursoHumano.paObtenerEmpleadoPerfil", IdEmpleadoPerfil, IdUsuarioAuditoria);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        empleadoperfil.mensaje.CodigoMensaje = 1;
                        empleadoperfil.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ObtenerEmpleadoPerfil], VERIFICAR CONSOLA";
                        empleadoperfil.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();

                        return empleadoperfil;
                    }
                    dt = ds.Tables[0].Copy();
                    empleadoperfil.IdEmpleadoPerfil = dt.Rows[0]["IdEmpleadoPerfil"] is System.DBNull ? 0 : (int)dt.Rows[0]["IdEmpleadoPerfil"];
                    empleadoperfil.empleado.IdEmpleado = dt.Rows[0]["IdEmpleado"] is System.DBNull ? 0 : (int)dt.Rows[0]["IdEmpleado"];
                    empleadoperfil.empresasede.IdEmpresaSede = dt.Rows[0]["IdEmpresaSede"] is System.DBNull ? 0 : (int)dt.Rows[0]["IdEmpresaSede"];
                    empleadoperfil.area.IdArea = dt.Rows[0]["IdArea"] is System.DBNull ? 0 : (int)dt.Rows[0]["IdArea"];
                    //empleadoperfil.area.NombreArea = dt.Rows[0]["NombreArea"] is System.DBNull ? null : (string)dt.Rows[0]["NombreArea"];
                    empleadoperfil.cargo.IdCargo = dt.Rows[0]["IdCargo"] is System.DBNull ? 0 : (int)dt.Rows[0]["IdCargo"];
                    empleadoperfil.Activo = dt.Rows[0]["Activo"] is System.DBNull ? false : (bool)dt.Rows[0]["Activo"];
                    empleadoperfil.DestinoTodos = dt.Rows[0]["DestinoTodos"] is System.DBNull ? false : (bool)dt.Rows[0]["DestinoTodos"];

                }
                return empleadoperfil;
            }
            catch (Exception ex)
            {
                empleadoperfil.mensaje.CodigoMensaje = 1;
                empleadoperfil.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ObtenerEmpleadoPerfil], VERIFICAR CONSOLA";
                empleadoperfil.mensaje.DescripcionMensajeSistema = ex.Message;
                return empleadoperfil;
            }
            finally
            {
                ds.Dispose();
                ds.Clear();
                dt.Dispose();
                dt.Clear();
            }
        }
        public async Task<EmpleadoPerfil> GuardaEmpleadoPerfilAsync(int Gestor, int IdEmpleadoPerfil, int IdEmpleado, int IdEmpresaSede, int IdArea, int IdCargo, bool Activo, bool DestinoTodos, int IdUsuarioAuditoria)
        {
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    System.Object[] ParamtrosOutPut = null;
                    ParamtrosOutPut = await ConexionSqlServer.GDatos.EjecutarAsync(DatosGlobales.ListaConexiones.cnRecursoHumanoSql, "RecursoHumano.paGuardarEmpleadoPerfil", IdEmpleadoPerfil, IdEmpleado, IdEmpresaSede, IdArea, IdCargo, Activo, DestinoTodos, IdUsuarioAuditoria, "", 0);
                    if (Convert.ToInt32(ParamtrosOutPut[100].ToString()) > 0)
                    {
                        empleadoperfil.mensaje.CodigoMensaje = 1;
                        empleadoperfil.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [GuardaEmpleadoPerfil], VERIFICAR CONSOLA";
                        empleadoperfil.mensaje.DescripcionMensajeSistema = ParamtrosOutPut[99].ToString();
                        return empleadoperfil;
                    }
                    empleadoperfil.IdEmpleadoPerfil = Convert.ToInt32(ParamtrosOutPut[0].ToString());
                    empleadoperfil.mensaje.DescripcionMensaje = ParamtrosOutPut[1].ToString();
                    empleadoperfil.mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[2].ToString());
                }
                return empleadoperfil;
            }
            catch (Exception ex)
            {
                empleadoperfil.mensaje.CodigoMensaje = 1;
                empleadoperfil.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [GuardaEmpleadoPerfil], VERIFICAR CONSOLA";
                empleadoperfil.mensaje.DescripcionMensajeSistema = ex.Message;
                return empleadoperfil;
            }
        }
        public async Task<ListaEmpleadoPerfil> ListarEmpleadoPerfilAsync(int Gestor, int IdEmpleado, int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnRecursoHumanoSql, "RecursoHumano.paListarEmpleadoPerfil", IdEmpleado, IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarEmpleadoPerfil], VERIFICAR CONSOLA";
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
                    empleadoperfil = new EmpleadoPerfil();
                    empleadoperfil.IdEmpleadoPerfil = row["IdEmpleadoPerfil"] is System.DBNull ? 0 : (int)row["IdEmpleadoPerfil"];

                    empleadoperfil.empleado.IdEmpleado = row["IdEmpleado"] is System.DBNull ? 0 : (int)row["IdEmpleado"];

                    empleadoperfil.empresasede.IdEmpresaSede = row["IdEmpresaSede"] is System.DBNull ? 0 : (int)row["IdEmpresaSede"];
                    empleadoperfil.empresasede.NombreSede = row["NombreSede"] is System.DBNull ? "" : (string)row["NombreSede"];

                    empleadoperfil.area.NombreArea = row["NombreArea"] is System.DBNull ? null : (string)row["NombreArea"];

                    empleadoperfil.cargo.NombreCargo = row["NombreCargo"] is System.DBNull ? null : (string)row["NombreCargo"];
                    empleadoperfil.Activo = row["Activo"] is System.DBNull ? false : (bool)row["Activo"];
                    empleadoperfil.DestinoTodos = row["DestinoTodos"] is System.DBNull ? false : (bool)row["DestinoTodos"];

                    lista.lista.Add(empleadoperfil);
                }
                return lista;
            }
            catch (Exception ex)
            {
                lista.mensaje.CodigoMensaje = 1;
                lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarEmpleadoPerfil], VERIFICAR CONSOLA";
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

        public ListaEmpleadoPerfil ListarComboEmpleadoPerfil(int Gestor, int IdPersona)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                     ds = ConexionSqlServer.GDatos.TraerDataSet(DatosGlobales.ListaConexiones.cnRecursoHumanoSql, "RecursoHumano.paListarComboEmpleadoPerfil", IdPersona);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarComboEmpleadoPerfil], VERIFICAR CONSOLA";
                        lista.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();
                        return lista;
                    }
                    dt = ds.Tables[0].Copy();
                }
                EmpleadoPerfil empleadoperfil = null;
                foreach (DataRow row in dt.Rows)
                {
                    empleadoperfil = new EmpleadoPerfil();
                    empleadoperfil.IdEmpleadoPerfil = (int)row["IdEmpleadoPerfil"];
                    empleadoperfil.NombreEmpleadoPerfil = (string)row["NombreEmpleadoPerfil"];
                    lista.lista.Add(empleadoperfil);
                }
                return lista;
            }
            catch (Exception ex)
            {
                lista.mensaje.CodigoMensaje = 1;
                lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarEmpleadoPerfil], VERIFICAR CONSOLA";
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
        public ListaEmpleadoPerfil ListarEmpleadoPerfilPorAutoComplete(int Gestor, string NombreEmpleadoPerfil)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                     ds = ConexionSqlServer.GDatos.TraerDataSet(DatosGlobales.ListaConexiones.cnRecursoHumanoSql, "RecursoHumano.paListarEmpleadoPerfilPorAutoComplete", NombreEmpleadoPerfil);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarComboEmpleadoPerfil], VERIFICAR CONSOLA";
                        lista.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();
                        return lista;
                    }
                    dt = ds.Tables[0].Copy();
                }
                foreach (DataRow row in dt.Rows)
                {
                    empleadoperfil = new EmpleadoPerfil();
                    empleadoperfil.IdEmpleadoPerfil = (int)row["IdEmpleadoPerfil"];
                    empleadoperfil.NombreEmpleadoPerfil = (string)row["NombreEmpleadoPerfil"];
                    lista.lista.Add(empleadoperfil);
                }
                return lista;
            }
            catch (Exception ex)
            {
                lista.mensaje.CodigoMensaje = 1;
                lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarEmpleadoPerfil], VERIFICAR CONSOLA";
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
        public ListaEmpleadoPerfil ListarEmpleadoPerfilPorAutoCompletePorSede(int Gestor, string NombreEmpleadoPerfil, int IdEmpresaSede)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                     ds = ConexionSqlServer.GDatos.TraerDataSet(DatosGlobales.ListaConexiones.cnRecursoHumanoSql, "RecursoHumano.paListarEmpleadoPerfilPorAutoCompletePorSede", NombreEmpleadoPerfil, IdEmpresaSede);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarComboEmpleadoPerfil], VERIFICAR CONSOLA";
                        lista.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();
                        return lista;
                    }
                    dt = ds.Tables[0].Copy();
                }
                foreach (DataRow row in dt.Rows)
                {
                    empleadoperfil = new EmpleadoPerfil();
                    empleadoperfil.IdEmpleadoPerfil = (int)row["IdEmpleadoPerfil"];
                    empleadoperfil.NombreEmpleadoPerfil = (string)row["NombreEmpleadoPerfil"];
                    lista.lista.Add(empleadoperfil);
                }
                return lista;
            }
            catch (Exception ex)
            {
                lista.mensaje.CodigoMensaje = 1;
                lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarEmpleadoPerfil], VERIFICAR CONSOLA";
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

        //METODOS ASYNCRONOS
        
        public async Task<Mensaje> ActualizarEstadoEmpleadoPerfil(int Gestor, int IdEmpleadoPerfil,  bool Activo, int IdUsuarioAuditoria)
        {
            Mensaje mensaje = new Mensaje();
            try
            {
                
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    System.Object[] ParamtrosOutPut = null;
                    ParamtrosOutPut = await ConexionSqlServer.GDatos.EjecutarAsync(DatosGlobales.ListaConexiones.cnRecursoHumanoSql, "RecursoHumano.paActualizarEstadoEmpleadoPerfil", IdEmpleadoPerfil,  Activo,  IdUsuarioAuditoria, "", 0);
                    if (Convert.ToInt32(ParamtrosOutPut[100].ToString()) > 0)
                    {
                        mensaje.CodigoMensaje = 1;
                        mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ActualizarEstadoEmpleadoPerfil], VERIFICAR CONSOLA";
                        mensaje.DescripcionMensajeSistema = ParamtrosOutPut[99].ToString();
                        return mensaje;
                    }
                    
                    mensaje.DescripcionMensaje = ParamtrosOutPut[1].ToString();
                    mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[2].ToString());
                }
                return mensaje;
            }
            catch (Exception ex)
            {
                mensaje.CodigoMensaje = 1;
                mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ActualizarEstadoEmpleadoPerfil], VERIFICAR CONSOLA";
                mensaje.DescripcionMensajeSistema = ex.Message;
                return mensaje;
            }
        }
        public async Task<ListaEmpleadoPerfil> ListarRolesPorEmpleado(int Gestor, int IdUsuarioAuditoria, int IdEmpleado, int IdEmpleadoPerfilActual)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnRecursoHumanoSql, "RecursoHumano.paListarRolesPorEmpleado", IdUsuarioAuditoria,IdEmpleado, IdEmpleadoPerfilActual);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarRolesPorEmpleado], VERIFICAR CONSOLA";
                        lista.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();
                        return lista;
                    }
                    dt = ds.Tables[0].Copy();                    
                }
                foreach (DataRow row in dt.Rows)
                {
                    empleadoperfil = new EmpleadoPerfil();
                    empleadoperfil.IdEmpleadoPerfil = (int)row["IdEmpleadoPerfil"];
                    empleadoperfil.empleado.IdEmpleado = (int)row["IdEmpleado"];
                    empleadoperfil.empleado.persona.RutaArchivoFoto = (string)row["RutaArchivoFoto"];

                    empleadoperfil.empleado.catalogotipoempleado.Detalle = (string)row["CatalogoTipoEmpleado"];
                    empleadoperfil.empleado.catalogotipoempleado.IdCatalogo = (int)row["IdCatalogoTipoEmpleado"];

                    empleadoperfil.empresasede.IdEmpresaSede = (int)row["IdEmpresaSede"];
               
                    empleadoperfil.area.NombreArea = (string)row["NombreArea"];

                    empleadoperfil.cargo.NombreCargo = (string)row["NombreCargo"];
                    empleadoperfil.cargo.catalogotipocargo.IdCatalogo = (int)row["IdCatalogoTipoCargo"];
                    empleadoperfil.Activo = (bool)row["Activo"];
                    empleadoperfil.Actual = (string)row["Actual"];
                    empleadoperfil.ActivoTexto = (string)row["ActivoTexto"];

                    lista.lista.Add(empleadoperfil);
                }
                return lista;
            }
            catch (Exception ex)
            {
                lista.mensaje.CodigoMensaje = 1;
                lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarRolesPorEmpleado], VERIFICAR CONSOLA";
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
        public async Task<ListaEmpleadoPerfil> ListarEmpleadoPerfilContactosAsync(int Gestor, int IdEmpleado, int IdUsuarioAuditoria, string BusquedaGeneral)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnRecursoHumanoSql, "RecursoHumano.paListarEmpleadoPerfilContactos", IdEmpleado, IdUsuarioAuditoria, BusquedaGeneral);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarEmpleadoPerfil], VERIFICAR CONSOLA";
                        lista.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();
                        return lista;
                    }
                    dt = ds.Tables[0].Copy();

                }
                foreach (DataRow row in dt.Rows)
                {
                    empleadoperfil = new EmpleadoPerfil();
                    empleadoperfil.IdEmpleadoPerfil = (int)row["IdEmpleadoPerfil"];
                    empleadoperfil.empleado.IdEmpleado = (int)row["IdEmpleado"];
                    empleadoperfil.empleado.persona.NombreCompleto = (string)row["NombreCompleto"];

                    empleadoperfil.empresasede.IdEmpresaSede = (int)row["IdEmpresaSede"];
                    empleadoperfil.empresasede.NombreSede = (string)row["NombreSede"];
                    empleadoperfil.area.NombreArea = (string)row["NombreArea"];

                    empleadoperfil.cargo.NombreCargo = (string)row["NombreCargo"];
                    empleadoperfil.Activo = (bool)row["Activo"];

                    empleadoperfil.empleado.persona.Email = (string)row["Email"];
                    empleadoperfil.empleado.persona.Celular = (string)row["Celular"];
                    empleadoperfil.empleado.persona.Anexo = (string)row["Anexo"];

                    empleadoperfil.empleado.persona.RutaArchivoFoto = (string)row["RutaArchivoFoto"];

                    lista.lista.Add(empleadoperfil);
                }
                return lista;
            }
            catch (Exception ex)
            {
                lista.mensaje.CodigoMensaje = 1;
                lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarEmpleadoPerfil], VERIFICAR CONSOLA";
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
        public async Task<ListaEmpleadoPerfil> ListarComboEmpleadoPerfilAsync(int Gestor, int IdPersona)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                     ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnRecursoHumanoSql, "RecursoHumano.paListarComboEmpleadoPerfil", IdPersona);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarComboEmpleadoPerfil], VERIFICAR CONSOLA";
                        lista.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();
                        return lista;
                    }
                    dt = ds.Tables[0].Copy();
                }
                EmpleadoPerfil empleadoperfil = null;
                foreach (DataRow row in dt.Rows)
                {
                    empleadoperfil = new EmpleadoPerfil();
                    empleadoperfil.IdEmpleadoPerfil = (int)row["IdEmpleadoPerfil"];
                    empleadoperfil.NombreEmpleadoPerfil = (string)row["NombreEmpleadoPerfil"];
                    lista.lista.Add(empleadoperfil);
                }
                return lista;
            }
            catch (Exception ex)
            {
                lista.mensaje.CodigoMensaje = 1;
                lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarEmpleadoPerfil], VERIFICAR CONSOLA";
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
        public async Task<ListaEmpleadoPerfil> ListarEmpleadoPerfilPorAutoCompleteAsync(int Gestor, string NombreEmpleadoPerfil)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                     ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnRecursoHumanoSql, "RecursoHumano.paListarEmpleadoPerfilPorAutoComplete", NombreEmpleadoPerfil);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarComboEmpleadoPerfil], VERIFICAR CONSOLA";
                        lista.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();
                        return lista;
                    }
                    dt = ds.Tables[0].Copy();
                }
                foreach (DataRow row in dt.Rows)
                {
                    empleadoperfil = new EmpleadoPerfil();
                    empleadoperfil.IdEmpleadoPerfil = (int)row["IdEmpleadoPerfil"];
                    empleadoperfil.NombreEmpleadoPerfil = (string)row["NombreEmpleadoPerfil"];
                    lista.lista.Add(empleadoperfil);
                }
                return lista;
            }
            catch (Exception ex)
            {
                lista.mensaje.CodigoMensaje = 1;
                lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarEmpleadoPerfil], VERIFICAR CONSOLA";
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
        public async Task<ListaEmpleadoPerfil> ListarEmpleadoPerfilPorAutoCompletePorSedeAsync(int Gestor, string NombreEmpleadoPerfil, int IdEmpresaSede)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                     ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnRecursoHumanoSql, "RecursoHumano.paListarEmpleadoPerfilPorAutoCompletePorSede", NombreEmpleadoPerfil, IdEmpresaSede);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarComboEmpleadoPerfil], VERIFICAR CONSOLA";
                        lista.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();
                        return lista;
                    }
                    dt = ds.Tables[0].Copy();
                }
                foreach (DataRow row in dt.Rows)
                {
                    empleadoperfil = new EmpleadoPerfil();
                    empleadoperfil.IdEmpleadoPerfil = (int)row["IdEmpleadoPerfil"];
                    empleadoperfil.NombreEmpleadoPerfil = (string)row["NombreEmpleadoPerfil"];
                    lista.lista.Add(empleadoperfil);
                }
                return lista;
            }
            catch (Exception ex)
            {
                lista.mensaje.CodigoMensaje = 1;
                lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarEmpleadoPerfil], VERIFICAR CONSOLA";
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