using System;
using System.Runtime.Serialization;
using Utilitarios;
using System.Data;
using System.Collections.Generic;
using General;
using System.Threading.Tasks;

namespace GeneralLogic
{    
    public class EmpresaLogic
    {
        private Empresa empresa;
        private ListaEmpresa lista;

        public EmpresaLogic()
        {
            empresa = new Empresa();
            lista = new ListaEmpresa();
        }
        public Mensaje EliminarEmpresa(int Gestor, int IdEmpresa, int IdUsuarioAuditoria) 
        {
            Mensaje mensaje = new Mensaje();
            if (Gestor == DatosGlobales.GestorSqlServer)
            {
                System.Object[] ParamtrosOutPut = null; 
                ParamtrosOutPut = ConexionSqlServer.GDatos.Ejecutar(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paEliminarEmpresa", IdEmpresa, IdUsuarioAuditoria, mensaje.DescripcionMensaje, mensaje.CodigoMensaje);
                if (Convert.ToInt32(ParamtrosOutPut[100].ToString()) > 0)
                {
                    mensaje.CodigoMensaje = 1;
                    mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA EN LA CAPA DE DATOS [EliminarEmpresa] VERIFICAR CONSOLA";
                    mensaje.DescripcionMensajeSistema = ParamtrosOutPut[99].ToString();
                    return mensaje;
                }
                mensaje.DescripcionMensaje = ParamtrosOutPut[0].ToString();
                mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[1].ToString());
            }
            return mensaje;
        }
        public Empresa ObtenerEmpresa(int Gestor, int IdEmpresa)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (IdEmpresa == 0)
                {
                    return empresa;
                }

                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                     ds = ConexionSqlServer.GDatos.TraerDataSet(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paObtenerEmpresa", IdEmpresa);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        empresa.mensaje.CodigoMensaje = 1;
                        empresa.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ObtenerEmpresa], VERIFICAR CONSOLA";
                        empresa.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();

                        return empresa;
                    }
                    dt = ds.Tables[0].Copy();
                    empresa.IdEmpresa = (int)dt.Rows[0]["IdEmpresa"];
                    empresa.persona.IdPersona = (int)dt.Rows[0]["IdPersona"];
                    empresa.IdEmpresaPadre = (int)dt.Rows[0]["IdEmpresaPadre"];
                    empresa.persona.NombreCompleto = (string)dt.Rows[0]["NombreCompleto"];
                    empresa.NombreEmpresa = (string)dt.Rows[0]["NombreEmpresa"];
                    empresa.Activo = (bool)dt.Rows[0]["Activo"];
                    empresa.IdEmpresaPadre = (int)dt.Rows[0]["IdEmpresaPadre"];
                    

                }
                return empresa;
            }
            catch (Exception ex)
            {
                empresa.mensaje.CodigoMensaje = 1;
                empresa.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ObtenerEmpresa], VERIFICAR CONSOLA";
                empresa.mensaje.DescripcionMensajeSistema = ex.Message;
                return empresa;
            }
            finally
            {
                ds.Dispose();
                ds.Clear();
                dt.Dispose();
                dt.Clear();
            }
        }
        public Empresa ObtenerEmpresaPrincipal(int Gestor, int IdEmpresa)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (IdEmpresa == 0)
                {
                    return empresa;
                }

                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                     ds = ConexionSqlServer.GDatos.TraerDataSet(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paObtenerEmpresa", IdEmpresa);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        empresa.mensaje.CodigoMensaje = 1;
                        empresa.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ObtenerEmpresa], VERIFICAR CONSOLA";
                        empresa.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();

                        return empresa;
                    }
                    dt = ds.Tables[0].Copy();
                    empresa.IdEmpresa = (int)dt.Rows[0]["IdEmpresa"];
                    empresa.persona.IdPersona = (int)dt.Rows[0]["IdPersona"];
                    empresa.IdEmpresaPadre = (int)dt.Rows[0]["IdEmpresaPadre"];
                    empresa.persona.NombreCompleto = (string)dt.Rows[0]["NombreCompleto"];
                    empresa.NombreEmpresa = (string)dt.Rows[0]["NombreEmpresa"];
                    empresa.Activo = (bool)dt.Rows[0]["Activo"];
                    empresa.IdEmpresaPadre = (int)dt.Rows[0]["IdEmpresaPadre"];


                }
                return empresa;
            }
            catch (Exception ex)
            {
                empresa.mensaje.CodigoMensaje = 1;
                empresa.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ObtenerEmpresa], VERIFICAR CONSOLA";
                empresa.mensaje.DescripcionMensajeSistema = ex.Message;
                return empresa;
            }
            finally
            {
                ds.Dispose();
                ds.Clear();
                dt.Dispose();
                dt.Clear();
            }
        }
        public Empresa GuardaEmpresaPrincipal(int Gestor, int IdEmpresa,int IdEmpresaPadre, int IdPersona, string NombreEmpresa, bool Activo, int IdUsuarioAuditoria)
        {
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    System.Object[] ParamtrosOutPut = null; 
                    ParamtrosOutPut = ConexionSqlServer.GDatos.Ejecutar(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paGuardarEmpresaPrincipal", IdEmpresa, IdEmpresaPadre, IdPersona, NombreEmpresa, Activo, IdUsuarioAuditoria, "", 0);
                    if (Convert.ToInt32(ParamtrosOutPut[100].ToString()) > 0)
                    {
                        empresa.mensaje.CodigoMensaje = 1;
                        empresa.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [GuardaEmpresa], VERIFICAR CONSOLA";
                        empresa.mensaje.DescripcionMensajeSistema = ParamtrosOutPut[99].ToString();
                        return empresa;
                    }
                    empresa.IdEmpresa = Convert.ToInt32(ParamtrosOutPut[0].ToString());
                    empresa.mensaje.DescripcionMensaje = ParamtrosOutPut[1].ToString();
                    empresa.mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[2].ToString());
                }
                return empresa;
            }
            catch (Exception ex)
            {
                empresa.mensaje.CodigoMensaje = 1;
                empresa.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [GuardaEmpresa], VERIFICAR CONSOLA";
                empresa.mensaje.DescripcionMensajeSistema = ex.Message;
                return empresa;
            }
        }
        public Empresa GuardaEmpresa(int Gestor, int IdEmpresa, int IdEmpresaPadre, int IdPersona, string NombreEmpresa, bool Activo, int IdUsuarioAuditoria)
        {
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    System.Object[] ParamtrosOutPut = null; 
                    ParamtrosOutPut = ConexionSqlServer.GDatos.Ejecutar(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paGuardarEmpresa", IdEmpresa, IdEmpresaPadre, IdPersona, NombreEmpresa, Activo, IdUsuarioAuditoria, "", 0);
                    if (Convert.ToInt32(ParamtrosOutPut[100].ToString()) > 0)
                    {
                        empresa.mensaje.CodigoMensaje = 1;
                        empresa.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [GuardaEmpresa], VERIFICAR CONSOLA";
                        empresa.mensaje.DescripcionMensajeSistema = ParamtrosOutPut[99].ToString();
                        return empresa;
                    }
                    empresa.IdEmpresa = Convert.ToInt32(ParamtrosOutPut[0].ToString());
                    empresa.mensaje.DescripcionMensaje = ParamtrosOutPut[1].ToString();
                    empresa.mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[2].ToString());
                }
                return empresa;
            }
            catch (Exception ex)
            {
                empresa.mensaje.CodigoMensaje = 1;
                empresa.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [GuardaEmpresa], VERIFICAR CONSOLA";
                empresa.mensaje.DescripcionMensajeSistema = ex.Message;
                return empresa;
            }
        }
        public ListaEmpresa ListarEmpresaPrincipal(int Gestor, int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                     ds = ConexionSqlServer.GDatos.TraerDataSet(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paListarEmpresaPrincipal", IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarEmpresa], VERIFICAR CONSOLA";
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
                    empresa = new Empresa();
                    empresa.IdEmpresa = (int)row["IdEmpresa"];
                    empresa.persona.NombreCompleto = (string)row["NombreCompleto"];
                    empresa.NombreEmpresa = (string)row["NombreEmpresa"];                  
                    empresa.Activo = (bool)row["Activo"];
                    lista.lista.Add(empresa);
                }
                return lista;
            }
            catch (Exception ex)
            {
                lista.mensaje.CodigoMensaje = 1;
                lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarEmpresa], VERIFICAR CONSOLA";
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
        public ListaEmpresa ListarEmpresa(int Gestor, int IdUsuarioAuditoria,int IdEmpresaPadre, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                     ds = ConexionSqlServer.GDatos.TraerDataSet(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paListarEmpresa", IdUsuarioAuditoria, IdEmpresaPadre, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarEmpresa], VERIFICAR CONSOLA";
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
                    empresa = new Empresa();
                    empresa.IdEmpresa = (int)row["IdEmpresa"];
                    empresa.persona.NombreCompleto = (string)row["NombreCompleto"];
                    empresa.NombreEmpresa = (string)row["NombreEmpresa"];                   
                    empresa.Activo = (bool)row["Activo"];                    
                    lista.lista.Add(empresa);
                }
                return lista;
            }
            catch (Exception ex)
            {
                lista.mensaje.CodigoMensaje = 1;
                lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarEmpresa], VERIFICAR CONSOLA";
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
        public ListaEmpresa ListarComboEmpresa(int Gestor)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                     ds = ConexionSqlServer.GDatos.TraerDataSet(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paListarComboEmpresa");
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarComboEmpresa], VERIFICAR CONSOLA";
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
                        empresa = new Empresa();
                        empresa.IdEmpresa = Convert.ToInt32(row["IdEmpresa"].ToString());
                        empresa.NombreEmpresa = row["NombreEmpresa"].ToString();
                        lista.lista.Add(empresa);
                    }

                }
                return lista;
            }
            catch (Exception ex)
            {
                lista.mensaje.CodigoMensaje = 1;
                lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarComboEmpresa], VERIFICAR CONSOLA";
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
        public ListaEmpresa ListarComboEmpresaPadre(int Gestor)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                     ds = ConexionSqlServer.GDatos.TraerDataSet(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paListarComboEmpresaPadre");
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarComboEmpresaPadre], VERIFICAR CONSOLA";
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
                        empresa = new Empresa();
                        empresa.IdEmpresa = Convert.ToInt32(row["IdEmpresa"].ToString());
                        empresa.NombreEmpresa = row["NombreEmpresa"].ToString();
                        lista.lista.Add(empresa);
                    }

                }
                return lista;
            }
            catch (Exception ex)
            {
                lista.mensaje.CodigoMensaje = 1;
                lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarComboEmpresa], VERIFICAR CONSOLA";
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

        //METODOS ASIMCRONOS

        public async Task<Mensaje> EliminarEmpresaAsync(int Gestor, int IdEmpresa, int IdUsuarioAuditoria)
        {
            Mensaje mensaje = new Mensaje();
            if (Gestor == DatosGlobales.GestorSqlServer)
            {
                System.Object[] ParamtrosOutPut = null; 
                ParamtrosOutPut =await ConexionSqlServer.GDatos.EjecutarAsync(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paEliminarEmpresa", IdEmpresa, IdUsuarioAuditoria, mensaje.DescripcionMensaje, mensaje.CodigoMensaje);
                if (Convert.ToInt32(ParamtrosOutPut[100].ToString()) > 0)
                {
                    mensaje.CodigoMensaje = 1;
                    mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA EN LA CAPA DE DATOS [EliminarEmpresa] VERIFICAR CONSOLA";
                    mensaje.DescripcionMensajeSistema = ParamtrosOutPut[99].ToString();
                    return mensaje;
                }
                mensaje.DescripcionMensaje = ParamtrosOutPut[0].ToString();
                mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[1].ToString());
            }
            return mensaje;
        }
        public  async Task<Empresa> ObtenerEmpresaAsync(int Gestor, int IdEmpresa)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (IdEmpresa == 0)
                {
                    return empresa;
                }

                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                     ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paObtenerEmpresa", IdEmpresa);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        empresa.mensaje.CodigoMensaje = 1;
                        empresa.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ObtenerEmpresa], VERIFICAR CONSOLA";
                        empresa.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();

                        return empresa;
                    }
                    dt = ds.Tables[0].Copy();
                    empresa.IdEmpresa = (int)dt.Rows[0]["IdEmpresa"];
                    empresa.persona.IdPersona = (int)dt.Rows[0]["IdPersona"];
                    empresa.IdEmpresaPadre = (int)dt.Rows[0]["IdEmpresaPadre"];
                    empresa.persona.NombreCompleto = (string)dt.Rows[0]["NombreCompleto"];
                    empresa.NombreEmpresa = (string)dt.Rows[0]["NombreEmpresa"];
                    empresa.Activo = (bool)dt.Rows[0]["Activo"];
                   
                    empresa.IdEmpresaPadre = (int)dt.Rows[0]["IdEmpresaPadre"];
                    

                }
                return empresa;
            }
            catch (Exception ex)
            {
                empresa.mensaje.CodigoMensaje = 1;
                empresa.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ObtenerEmpresa], VERIFICAR CONSOLA";
                empresa.mensaje.DescripcionMensajeSistema = ex.Message;
                return empresa;
            }
            finally
            {
                ds.Dispose();
                ds.Clear();
                dt.Dispose();
                dt.Clear();
            }
        }
        public  async Task<Empresa> ObtenerEmpresaPrincipalAsync(int Gestor, int IdEmpresa)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (IdEmpresa == 0)
                {
                    return empresa;
                }

                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                     ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paObtenerEmpresa", IdEmpresa);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        empresa.mensaje.CodigoMensaje = 1;
                        empresa.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ObtenerEmpresa], VERIFICAR CONSOLA";
                        empresa.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();

                        return empresa;
                    }
                    dt = ds.Tables[0].Copy();
                    empresa.IdEmpresa = (int)dt.Rows[0]["IdEmpresa"];
                    empresa.persona.IdPersona = (int)dt.Rows[0]["IdPersona"];
                    empresa.IdEmpresaPadre = (int)dt.Rows[0]["IdEmpresaPadre"];
                    empresa.persona.NombreCompleto = (string)dt.Rows[0]["NombreCompleto"];
                    empresa.NombreEmpresa = (string)dt.Rows[0]["NombreEmpresa"];
                    empresa.Activo = (bool)dt.Rows[0]["Activo"];
                    empresa.IdEmpresaPadre = (int)dt.Rows[0]["IdEmpresaPadre"];


                }
                return empresa;
            }
            catch (Exception ex)
            {
                empresa.mensaje.CodigoMensaje = 1;
                empresa.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ObtenerEmpresa], VERIFICAR CONSOLA";
                empresa.mensaje.DescripcionMensajeSistema = ex.Message;
                return empresa;
            }
            finally
            {
                ds.Dispose();
                ds.Clear();
                dt.Dispose();
                dt.Clear();
            }
        }
        public  async Task<Empresa> GuardaEmpresaPrincipalAsync(int Gestor, int IdEmpresa, int IdEmpresaPadre, int IdPersona, string NombreEmpresa, bool Activo, int IdUsuarioAuditoria)
        {
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    System.Object[] ParamtrosOutPut = null; 
                    ParamtrosOutPut =await ConexionSqlServer.GDatos.EjecutarAsync(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paGuardarEmpresaPrincipal", IdEmpresa, IdEmpresaPadre, IdPersona, NombreEmpresa, Activo, IdUsuarioAuditoria, "", 0);
                    if (Convert.ToInt32(ParamtrosOutPut[100].ToString()) > 0)
                    {
                        empresa.mensaje.CodigoMensaje = 1;
                        empresa.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [GuardaEmpresa], VERIFICAR CONSOLA";
                        empresa.mensaje.DescripcionMensajeSistema = ParamtrosOutPut[99].ToString();
                        return empresa;
                    }
                    empresa.IdEmpresa = Convert.ToInt32(ParamtrosOutPut[0].ToString());
                    empresa.mensaje.DescripcionMensaje = ParamtrosOutPut[1].ToString();
                    empresa.mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[2].ToString());
                }
                return empresa;
            }
            catch (Exception ex)
            {
                empresa.mensaje.CodigoMensaje = 1;
                empresa.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [GuardaEmpresa], VERIFICAR CONSOLA";
                empresa.mensaje.DescripcionMensajeSistema = ex.Message;
                return empresa;
            }
        }
        public  async Task<Empresa> GuardaEmpresaAsync(int Gestor, int IdEmpresa, int IdEmpresaPadre, int IdPersona, string NombreEmpresa, bool Activo, int IdUsuarioAuditoria)
        {
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    System.Object[] ParamtrosOutPut = null; 
                    ParamtrosOutPut =await ConexionSqlServer.GDatos.EjecutarAsync(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paGuardarEmpresa", IdEmpresa, IdEmpresaPadre, IdPersona, NombreEmpresa, Activo, IdUsuarioAuditoria, "", 0);
                    if (Convert.ToInt32(ParamtrosOutPut[100].ToString()) > 0)
                    {
                        empresa.mensaje.CodigoMensaje = 1;
                        empresa.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [GuardaEmpresa], VERIFICAR CONSOLA";
                        empresa.mensaje.DescripcionMensajeSistema = ParamtrosOutPut[99].ToString();
                        return empresa;
                    }
                    empresa.IdEmpresa = Convert.ToInt32(ParamtrosOutPut[0].ToString());
                    empresa.mensaje.DescripcionMensaje = ParamtrosOutPut[1].ToString();
                    empresa.mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[2].ToString());
                }
                return empresa;
            }
            catch (Exception ex)
            {
                empresa.mensaje.CodigoMensaje = 1;
                empresa.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [GuardaEmpresa], VERIFICAR CONSOLA";
                empresa.mensaje.DescripcionMensajeSistema = ex.Message;
                return empresa;
            }
        }
        public async Task<ListaEmpresa> ListarEmpresaPrincipalAsync(int Gestor, int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                     ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paListarEmpresaPrincipal", IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarEmpresa], VERIFICAR CONSOLA";
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
                    empresa = new Empresa();
                    empresa.IdEmpresa = (int)row["IdEmpresa"];
                    empresa.persona.NombreCompleto = (string)row["NombreCompleto"];
                    empresa.NombreEmpresa = (string)row["NombreEmpresa"];
                    empresa.Activo = (bool)row["Activo"];
                    lista.lista.Add(empresa);
                }
                return lista;
            }
            catch (Exception ex)
            {
                lista.mensaje.CodigoMensaje = 1;
                lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarEmpresa], VERIFICAR CONSOLA";
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
        public async Task<ListaEmpresa> ListarEmpresaAsync(int Gestor, int IdUsuarioAuditoria, int IdEmpresaPadre, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                     ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paListarEmpresa", IdUsuarioAuditoria, IdEmpresaPadre, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarEmpresa], VERIFICAR CONSOLA";
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
                    empresa = new Empresa();
                    empresa.IdEmpresa = (int)row["IdEmpresa"];
                    empresa.persona.NombreCompleto = (string)row["NombreCompleto"];
                    empresa.NombreEmpresa = (string)row["NombreEmpresa"];
                    empresa.Activo = (bool)row["Activo"];
                    lista.lista.Add(empresa);
                }
                return lista;
            }
            catch (Exception ex)
            {
                lista.mensaje.CodigoMensaje = 1;
                lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarEmpresa], VERIFICAR CONSOLA";
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
        public async Task<ListaEmpresa> ListarComboEmpresaAsync(int Gestor)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                     ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paListarComboEmpresa");
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarComboEmpresa], VERIFICAR CONSOLA";
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
                        empresa = new Empresa();
                        empresa.IdEmpresa = Convert.ToInt32(row["IdEmpresa"].ToString());
                        empresa.NombreEmpresa = row["NombreEmpresa"].ToString();
                        lista.lista.Add(empresa);
                    }

                }
                return lista;
            }
            catch (Exception ex)
            {
                lista.mensaje.CodigoMensaje = 1;
                lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarComboEmpresa], VERIFICAR CONSOLA";
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
        public async Task<ListaEmpresa> ListarComboEmpresaPadreAsync(int Gestor)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                     ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paListarComboEmpresaPadre");
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarComboEmpresaPadre], VERIFICAR CONSOLA";
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
                        empresa = new Empresa();
                        empresa.IdEmpresa = Convert.ToInt32(row["IdEmpresa"].ToString());
                        empresa.NombreEmpresa = row["NombreEmpresa"].ToString();
                        lista.lista.Add(empresa);
                    }

                }
                return lista;
            }
            catch (Exception ex)
            {
                lista.mensaje.CodigoMensaje = 1;
                lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarComboEmpresa], VERIFICAR CONSOLA";
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



