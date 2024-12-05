using System;
using Utilitarios;
using System.Data;
using RecursoHumano;
using System.Threading.Tasks;
using General;
using System.Numerics;

namespace PersonalLogic
{
    public class EmpleadoLogic
    {
        private Empleado empleado;
        private ListaEmpleado lista;

        public EmpleadoLogic()
        {
            empleado = new Empleado();
            lista = new ListaEmpleado();
        }
        public async Task<Mensaje> EliminarEmpleadoAsync(int Gestor, int IdEmpleado, int IdUsuarioAuditoria)
        {
            Mensaje mensaje = new Mensaje();
            if (Gestor == DatosGlobales.GestorSqlServer)
            {
                System.Object[] ParamtrosOutPut = null;
                ParamtrosOutPut = await ConexionSqlServer.GDatos.EjecutarAsync(DatosGlobales.ListaConexiones.cnRecursoHumanoSql, "RecursoHumano.paEliminarEmpleado", IdEmpleado, IdUsuarioAuditoria, mensaje.DescripcionMensaje, mensaje.CodigoMensaje);
                if (Convert.ToInt32(ParamtrosOutPut[100].ToString()) > 0)
                {
                    mensaje.CodigoMensaje = 1;
                    mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA EN LA CAPA DE DATOS [EliminarEmpleado] VERIFICAR CONSOLA";
                    mensaje.DescripcionMensajeSistema = ParamtrosOutPut[99].ToString();
                    return mensaje;
                }
                mensaje.DescripcionMensaje = ParamtrosOutPut[0].ToString();
                mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[1].ToString());
            }
            return mensaje;
        }
        public async Task<Empleado> ObtenerEmpleadoAsync(int Gestor, int IdEmpleado, int IdUsuarioAuditoria)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnRecursoHumanoSql, "RecursoHumano.paObtenerEmpleado", IdEmpleado, IdUsuarioAuditoria);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        empleado.mensaje.CodigoMensaje = 1;
                        empleado.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ObtenerEmpleado], VERIFICAR CONSOLA";
                        empleado.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();

                        return empleado;
                    }
                    dt = ds.Tables[0].Copy();
                    empleado.IdEmpleado = dt.Rows[0]["IdEmpleado"] is System.DBNull ? 0 : (int)dt.Rows[0]["IdEmpleado"];
                    empleado.empresapadre.IdEmpresaPadre = dt.Rows[0]["IdEmpresaPadre"] is System.DBNull ? 0 : (int)dt.Rows[0]["IdEmpresaPadre"];
                    empleado.persona.IdPersona = dt.Rows[0]["IdPersona"] is System.DBNull ? 0 : (int)dt.Rows[0]["IdPersona"];
                    empleado.persona.NombreCompleto = dt.Rows[0]["NombreCompletoPersona"] is System.DBNull ? null : (string)dt.Rows[0]["NombreCompletoPersona"];
                    empleado.Activo = dt.Rows[0]["Activo"] is System.DBNull ? false : (bool)dt.Rows[0]["Activo"];
                    empleado.catalogoestadoempleado.IdCatalogo = dt.Rows[0]["IdCatalogoEstadoEmpleado"] is System.DBNull ? 0 : (int)dt.Rows[0]["IdCatalogoEstadoEmpleado"];
                    empleado.catalogoestadoempleado.Descripcion = dt.Rows[0]["CatalogoEstadoEmpleado"] is System.DBNull ? "" : (string)dt.Rows[0]["CatalogoEstadoEmpleado"];
                    empleado.catalogotipoempleado.IdCatalogo = dt.Rows[0]["IdCatalogoTipoEmpleado"] is System.DBNull ? 0 : (int)dt.Rows[0]["IdCatalogoTipoEmpleado"];
                    empleado.catalogotipoempleado.Descripcion = dt.Rows[0]["CatalogoTipoEmpleado"] is System.DBNull ? "" : (string)dt.Rows[0]["CatalogoTipoEmpleado"];

                }
                return empleado;
            }
            catch (Exception ex)
            {
                empleado.mensaje.CodigoMensaje = 1;
                empleado.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ObtenerEmpleado], VERIFICAR CONSOLA";
                empleado.mensaje.DescripcionMensajeSistema = ex.Message;
                return empleado;
            }
            finally
            {
                ds.Dispose();
                ds.Clear();
                dt.Dispose();
                dt.Clear();
            }
        }
        public async Task<Empleado> GuardaEmpleadoAsync(int Gestor, int IdEmpleado, int IdEmpresaPadre, int IdPersona, bool Activo, int IdCatalogoEstadoEmpleado, int IdCatalogoTipoEmpleado, int IdUsuarioAuditoria)
        {
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    System.Object[] ParamtrosOutPut = null;
                    ParamtrosOutPut = await ConexionSqlServer.GDatos.EjecutarAsync(DatosGlobales.ListaConexiones.cnRecursoHumanoSql, "RecursoHumano.paGuardarEmpleado", IdEmpleado, IdEmpresaPadre, IdPersona, Activo, IdCatalogoEstadoEmpleado, IdCatalogoTipoEmpleado, IdUsuarioAuditoria, "", 0);
                    if (Convert.ToInt32(ParamtrosOutPut[100].ToString()) > 0)
                    {
                        empleado.mensaje.CodigoMensaje = 1;
                        empleado.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [GuardaEmpleado], VERIFICAR CONSOLA";
                        empleado.mensaje.DescripcionMensajeSistema = ParamtrosOutPut[99].ToString();
                        return empleado;
                    }
                    empleado.IdEmpleado = Convert.ToInt32(ParamtrosOutPut[0].ToString());
                    empleado.mensaje.DescripcionMensaje = ParamtrosOutPut[1].ToString();
                    empleado.mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[2].ToString());
                }
                return empleado;
            }
            catch (Exception ex)
            {
                empleado.mensaje.CodigoMensaje = 1;
                empleado.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [GuardaEmpleado], VERIFICAR CONSOLA";
                empleado.mensaje.DescripcionMensajeSistema = ex.Message;
                return empleado;
            }
        }

        public async Task<ListaEmpleado> ListarEmpleadoAsync(int Gestor, int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnRecursoHumanoSql, "RecursoHumano.paListarEmpleado", IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarEmpleado], VERIFICAR CONSOLA";
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
                    empleado = new Empleado();
                    empleado.IdEmpleado = row["IdEmpleado"] is System.DBNull ? 0 : (int)row["IdEmpleado"];

                    empleado.empresapadre.IdEmpresaPadre = row["IdEmpresaPadre"] is System.DBNull ? 0 : (int)row["IdEmpresaPadre"];

                    empleado.persona.NombreCompleto = row["NombreCompletoPersona"] is System.DBNull ? null : (string)row["NombreCompletoPersona"];
                    empleado.Activo = row["Activo"] is System.DBNull ? false : (bool)row["Activo"];
                    empleado.catalogoestadoempleado.Descripcion = row["CatalogoEstadoEmpleado"] is System.DBNull ? null : (string)row["CatalogoEstadoEmpleado"];
                    empleado.catalogotipoempleado.Descripcion = row["CatalogoTipoEmpleado"] is System.DBNull ? null : (string)row["CatalogoTipoEmpleado"];

                    lista.lista.Add(empleado);
                }
                return lista;
            }
            catch (Exception ex)
            {
                lista.mensaje.CodigoMensaje = 1;
                lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarEmpleado], VERIFICAR CONSOLA";
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

        public async Task<ListaEmpleado> ListarDirectorio(int Gestor, int IdUsuarioAuditoria, int IdArea, int IdCargo, string NombreCompleto)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnRecursoHumanoSql, "RecursoHumano.paListarDirectorio", IdUsuarioAuditoria, IdArea, IdCargo, NombreCompleto);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarEmpleado], VERIFICAR CONSOLA";
                        lista.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();
                        return lista;
                    }
                    dt = ds.Tables[0].Copy();               
                }
                Empleado empleado = null;
                foreach (DataRow row in dt.Rows)
                {
                    empleado = new Empleado();
                    empleado.IdEmpleado = (int)row["IdEmpleado"];
                    empleado.persona.NombreCompleto = (string)row["NombreCompleto"];
                    empleado.persona.Email = (string)row["Email"];
                    empleado.persona.RutaArchivoFoto = (string)row["RutaArchivoFoto"];
                    empleado.persona.FechaNacimiento = (string)row["FechaNacimiento"];
                    empleado.persona.Celular = (string)row["Celular"];
                    empleado.persona.TelefonoFijo = (string)row["TelefonoFijo"];
                    lista.lista.Add(empleado);
                }
                return lista;
            }
            catch (Exception ex)
            {
                lista.mensaje.CodigoMensaje = 1;
                lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarEmpleado], VERIFICAR CONSOLA";
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
        public async Task<ListaEmpleado> ListarEmpleadoCumpleanios(int Gestor, int IdUsuarioAuditoria)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnRecursoHumanoSql, "RecursoHumano.paListarEmpleadoCumpleanios", IdUsuarioAuditoria);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarEmpleado], VERIFICAR CONSOLA";
                        lista.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();
                        return lista;
                    }
                    dt = ds.Tables[0].Copy();
                }

                foreach (DataRow row in dt.Rows)
                {
                    empleado = new Empleado();

                    empleado.persona.NombreCompleto = (string)row["NombreCompleto"];
                    empleado.persona.FechaNacimiento = (string)row["FechaNacimiento"];
                    empleado.persona.RutaArchivoFoto = (string)row["RutaArchivoFoto"];
                    empleado.AbreviaturaArea = (string)row["Abreviatura"];

                    lista.lista.Add(empleado);
                }
                return lista;
            }
            catch (Exception ex)
            {
                lista.mensaje.CodigoMensaje = 1;
                lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarEmpleado], VERIFICAR CONSOLA";
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
        public async Task<ListaEmpleado> ListarComboEmpleado(int Gestor, int IdUsuarioAuditoria)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnRecursoHumanoSql, "RecursoHumano.paListarComboEmpleado", IdUsuarioAuditoria);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarEmpleado], VERIFICAR CONSOLA";
                        lista.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();
                        return lista;
                    }
                    dt = ds.Tables[0].Copy();
                }
                foreach (DataRow row in dt.Rows)
                {
                    empleado = new Empleado();
                    empleado.IdEmpleado = (int)row["IdEmpleado"];
                    empleado.persona = new General.Persona() { IdPersona = (int)row["IdPersona"], NombreCompleto = (string)row["NombreCompleto"] };
                    lista.lista.Add(empleado);
                }
                return lista;
            }
            catch (Exception ex)
            {
                lista.mensaje.CodigoMensaje = 1;
                lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarEmpleado], VERIFICAR CONSOLA";
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
        public async Task<ListaEmpleado> ListarComboEmpleado2(int Gestor, int IdUsuarioAuditoria)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnRecursoHumanoSql, "RecursoHumano.paListarComboEmpleado", IdUsuarioAuditoria);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarEmpleado], VERIFICAR CONSOLA";
                        lista.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();
                        return lista;
                    }
                    dt = ds.Tables[0].Copy();
                }
                foreach (DataRow row in dt.Rows)
                {
                    empleado = new Empleado();
                    empleado.IdEmpleado = (int)row["IdEmpleado"];
                    empleado.persona.NombreCompleto = (string)row["NombreCompleto"];
                    lista.lista.Add(empleado);
                }
                return lista;
            }
            catch (Exception ex)
            {
                lista.mensaje.CodigoMensaje = 1;
                lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarEmpleado], VERIFICAR CONSOLA";
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

        public async Task<ListaEmpleado> ObtenerDocumentosAsync(int Gestor, string search)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    ds = await ConexionSqlServer.GDatos.TraerDataSetPorQueryAsync(DatosGlobales.ListaConexiones.cnInventarioSql, $"SELECT * FROM RecursoHumano.visPersonalActivo WHERE NumeroDocumento LIKE '%{search}%' OR NombreCompleto LIKE '%{search}%'");
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarPersonalActivo], VERIFICAR CONSOLA";
                        lista.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();
                        return lista;
                    }
                    dt = ds.Tables[0].Copy();
                }

                foreach (DataRow row in dt.Rows)
                {
                    var empleado = new Empleado
                    {
                        Documento = row["NumeroDocumento"] as string ?? string.Empty,
                        Nombre = row["NombreCompleto"] as string ?? string.Empty,
                        Cargo = row["NombreCargo"] as string ?? string.Empty
                    };
                    lista.lista.Add(empleado);
                }
                return lista;
            }
            catch (Exception ex)
            {
                lista.mensaje.CodigoMensaje = 1;
                lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarPersonalActivo], VERIFICAR CONSOLA";
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