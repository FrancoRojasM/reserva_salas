using System;
using System.Runtime.Serialization;
using Utilitarios;
using System.Data;
using System.Collections.Generic;
using General;
using System.Threading.Tasks;

namespace GeneralLogic
{
    
    public class EmpresaSedeLogic
    {

        private EmpresaSede empresasede;
        private ListaEmpresaSede lista;

        public EmpresaSedeLogic()
        {
            empresasede = new EmpresaSede();
            lista = new ListaEmpresaSede();
        }
        public Mensaje EliminarEmpresaSede(int Gestor, int IdEmpresaSede, int IdUsuarioAuditoria) 
        {
            Mensaje mensaje = new Mensaje();
            if (Gestor == DatosGlobales.GestorSqlServer)
            {
                System.Object[] ParamtrosOutPut = null; 
                ParamtrosOutPut = ConexionSqlServer.GDatos.Ejecutar(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paEliminarEmpresaSede", IdEmpresaSede, IdUsuarioAuditoria, mensaje.DescripcionMensaje, mensaje.CodigoMensaje);
                if (Convert.ToInt32(ParamtrosOutPut[100].ToString()) > 0)
                {
                    mensaje.CodigoMensaje = 1;
                    mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA EN LA CAPA DE DATOS [EliminarEmpresaSede] VERIFICAR CONSOLA";
                    mensaje.DescripcionMensajeSistema = ParamtrosOutPut[99].ToString();
                    return mensaje;
                }
                mensaje.DescripcionMensaje = ParamtrosOutPut[0].ToString();
                mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[1].ToString());
            }
            return mensaje;
        }
        public EmpresaSede ObtenerEmpresaSede(int Gestor, int IdEmpresaSede, int IdUsuarioAuditoria)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (IdEmpresaSede == 0)
                {
                    return empresasede;
                }

                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                     ds = ConexionSqlServer.GDatos.TraerDataSet(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paObtenerEmpresaSede", IdEmpresaSede);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        empresasede.mensaje.CodigoMensaje = 1;
                        empresasede.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ObtenerEmpresaSede], VERIFICAR CONSOLA";
                        empresasede.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();

                        return empresasede;
                    }
                    dt = ds.Tables[0].Copy();
                    empresasede.IdEmpresaSede = (int)dt.Rows[0]["IdEmpresaSede"];
                    empresasede.empresa.IdEmpresa = (int)dt.Rows[0]["IdEmpresa"];
                    empresasede.DireccionSede = (string)dt.Rows[0]["DireccionSede"];
                    empresasede.NombreSede = (string)dt.Rows[0]["NombreSede"];
                    empresasede.Activo = (bool)dt.Rows[0]["Activo"];

                }
                return empresasede;
            }
            catch (Exception ex)
            {
                empresasede.mensaje.CodigoMensaje = 1;
                empresasede.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ObtenerEmpresaSede], VERIFICAR CONSOLA";
                empresasede.mensaje.DescripcionMensajeSistema = ex.Message;
                return empresasede;
            }
            finally
            {
                ds.Dispose();
                ds.Clear();
                dt.Dispose();
                dt.Clear();
            }
        }
        public EmpresaSede GuardaEmpresaSede(int Gestor, int IdEmpresaSede, int IdEmpresa, string DireccionSede, string NombreSede, bool Activo, int IdUsuarioAuditoria)
        {
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    System.Object[] ParamtrosOutPut = null; 
                    ParamtrosOutPut = ConexionSqlServer.GDatos.Ejecutar(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paGuardarEmpresaSede", IdEmpresaSede, IdEmpresa, DireccionSede, NombreSede, Activo, IdUsuarioAuditoria, "", 0);
                    if (Convert.ToInt32(ParamtrosOutPut[100].ToString()) > 0)
                    {
                        empresasede.mensaje.CodigoMensaje = 1;
                        empresasede.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [GuardaEmpresaSede], VERIFICAR CONSOLA";
                        empresasede.mensaje.DescripcionMensajeSistema = ParamtrosOutPut[99].ToString();
                        return empresasede;
                    }
                    empresasede.IdEmpresaSede = Convert.ToInt32(ParamtrosOutPut[0].ToString());
                    empresasede.mensaje.DescripcionMensaje = ParamtrosOutPut[1].ToString();
                    empresasede.mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[2].ToString());
                }
                return empresasede;
            }
            catch (Exception ex)
            {
                empresasede.mensaje.CodigoMensaje = 1;
                empresasede.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [GuardaEmpresaSede], VERIFICAR CONSOLA";
                empresasede.mensaje.DescripcionMensajeSistema = ex.Message;
                return empresasede;
            }
        }
        public ListaEmpresaSede ListarEmpresaSede(int Gestor, int IdEmpresa, int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                     ds = ConexionSqlServer.GDatos.TraerDataSet(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paListarEmpresaSede", IdEmpresa, IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarEmpresaSede], VERIFICAR CONSOLA";
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
                    empresasede = new EmpresaSede();
                    empresasede.IdEmpresaSede = (int)row["IdEmpresaSede"];
                    empresasede.empresa.IdEmpresa = (int)row["IdEmpresa"];

                    empresasede.DireccionSede = (string)row["DireccionSede"];
                    empresasede.NombreSede = (string)row["NombreSede"];
                    empresasede.Activo = (bool)row["Activo"];

                    lista.lista.Add(empresasede);
                }
                return lista;
            }
            catch (Exception ex)
            {
                lista.mensaje.CodigoMensaje = 1;
                lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarEmpresaSede], VERIFICAR CONSOLA";
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
        public ListaEmpresaSede ListarComboEmpresaSede(int Gestor , int IdUsuarioAuditoria, int IdEmpresaPadre)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                     ds = ConexionSqlServer.GDatos.TraerDataSet(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paListarComboEmpresaSede", IdEmpresaPadre);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarComboEmpresaSede], VERIFICAR CONSOLA";
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
                        empresasede = new EmpresaSede();
                        empresasede.IdEmpresaSede = Convert.ToInt32(row["IdEmpresaSede"].ToString());
                        empresasede.NombreSede = row["NombreSede"].ToString();
                        lista.lista.Add(empresasede);
                    }

                }
                return lista;
            }
            catch (Exception ex)
            {
                lista.mensaje.CodigoMensaje = 1;
                lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarComboEmpresaSede], VERIFICAR CONSOLA";
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
        //METODOS ASINCRONOS
        public async Task<Mensaje> EliminarEmpresaSedeAsync(int Gestor, int IdEmpresaSede, int IdUsuarioAuditoria)
        {
            Mensaje mensaje = new Mensaje();
            if (Gestor == DatosGlobales.GestorSqlServer)
            {
                System.Object[] ParamtrosOutPut = null; 
                ParamtrosOutPut = await ConexionSqlServer.GDatos.EjecutarAsync(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paEliminarEmpresaSede", IdEmpresaSede, IdUsuarioAuditoria, mensaje.DescripcionMensaje, mensaje.CodigoMensaje);
                if (Convert.ToInt32(ParamtrosOutPut[100].ToString()) > 0)
                {
                    mensaje.CodigoMensaje = 1;
                    mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA EN LA CAPA DE DATOS [EliminarEmpresaSede] VERIFICAR CONSOLA";
                    mensaje.DescripcionMensajeSistema = ParamtrosOutPut[99].ToString();
                    return mensaje;
                }
                mensaje.DescripcionMensaje = ParamtrosOutPut[0].ToString();
                mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[1].ToString());
            }
            return mensaje;
        }
        public async Task<EmpresaSede> ObtenerEmpresaSedeAsync(int Gestor, int IdEmpresaSede, int IdUsuarioAuditoria)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (IdEmpresaSede == 0)
                {
                    return empresasede;
                }

                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                     ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paObtenerEmpresaSede", IdEmpresaSede);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        empresasede.mensaje.CodigoMensaje = 1;
                        empresasede.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ObtenerEmpresaSede], VERIFICAR CONSOLA";
                        empresasede.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();

                        return empresasede;
                    }
                    dt = ds.Tables[0].Copy();
                    empresasede.IdEmpresaSede = (int)dt.Rows[0]["IdEmpresaSede"];
                    empresasede.empresa.IdEmpresa = (int)dt.Rows[0]["IdEmpresa"];
                    empresasede.DireccionSede = (string)dt.Rows[0]["DireccionSede"];
                    empresasede.NombreSede = (string)dt.Rows[0]["NombreSede"];
                    empresasede.Activo = (bool)dt.Rows[0]["Activo"];

                }
                return empresasede;
            }
            catch (Exception ex)
            {
                empresasede.mensaje.CodigoMensaje = 1;
                empresasede.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ObtenerEmpresaSede], VERIFICAR CONSOLA";
                empresasede.mensaje.DescripcionMensajeSistema = ex.Message;
                return empresasede;
            }
            finally
            {
                ds.Dispose();
                ds.Clear();
                dt.Dispose();
                dt.Clear();
            }
        }
        public async Task<EmpresaSede> GuardaEmpresaSedeAsync(int Gestor, int IdEmpresaSede, int IdEmpresa, string DireccionSede, string NombreSede, bool Activo, int IdUsuarioAuditoria)
        {
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    System.Object[] ParamtrosOutPut = null; 
                    ParamtrosOutPut = await ConexionSqlServer.GDatos.EjecutarAsync(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paGuardarEmpresaSede", IdEmpresaSede, IdEmpresa, DireccionSede, NombreSede, Activo, IdUsuarioAuditoria, "", 0);
                    if (Convert.ToInt32(ParamtrosOutPut[100].ToString()) > 0)
                    {
                        empresasede.mensaje.CodigoMensaje = 1;
                        empresasede.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [GuardaEmpresaSede], VERIFICAR CONSOLA";
                        empresasede.mensaje.DescripcionMensajeSistema = ParamtrosOutPut[99].ToString();
                        return empresasede;
                    }
                    empresasede.IdEmpresaSede = Convert.ToInt32(ParamtrosOutPut[0].ToString());
                    empresasede.mensaje.DescripcionMensaje = ParamtrosOutPut[1].ToString();
                    empresasede.mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[2].ToString());
                }
                return empresasede;
            }
            catch (Exception ex)
            {
                empresasede.mensaje.CodigoMensaje = 1;
                empresasede.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [GuardaEmpresaSede], VERIFICAR CONSOLA";
                empresasede.mensaje.DescripcionMensajeSistema = ex.Message;
                return empresasede;
            }
        }
        public async Task<ListaEmpresaSede> ListarEmpresaSedeAsync(int Gestor, int IdEmpresa, int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                     ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paListarEmpresaSede", IdEmpresa, IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarEmpresaSede], VERIFICAR CONSOLA";
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
                    empresasede = new EmpresaSede();
                    empresasede.IdEmpresaSede = (int)row["IdEmpresaSede"];
                    empresasede.empresa.IdEmpresa = (int)row["IdEmpresa"];

                    empresasede.DireccionSede = (string)row["DireccionSede"];
                    empresasede.NombreSede = (string)row["NombreSede"];
                    empresasede.Activo = (bool)row["Activo"];

                    lista.lista.Add(empresasede);
                }
                return lista;
            }
            catch (Exception ex)
            {
                lista.mensaje.CodigoMensaje = 1;
                lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarEmpresaSede], VERIFICAR CONSOLA";
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
        public async Task<ListaEmpresaSede> ListarComboEmpresaSedeAsync(int Gestor, int IdUsuarioAuditoria, int IdEmpresaPadre)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                     ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paListarComboEmpresaSede", IdEmpresaPadre);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarComboEmpresaSede], VERIFICAR CONSOLA";
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
                        empresasede = new EmpresaSede();
                        empresasede.IdEmpresaSede = Convert.ToInt32(row["IdEmpresaSede"].ToString());
                        empresasede.NombreSede = row["NombreSede"].ToString();
                        lista.lista.Add(empresasede);
                    }

                }
                return lista;
            }
            catch (Exception ex)
            {
                lista.mensaje.CodigoMensaje = 1;
                lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarComboEmpresaSede], VERIFICAR CONSOLA";
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



