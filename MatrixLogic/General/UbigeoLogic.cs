using System;
using System.Runtime.Serialization;
using Utilitarios;
using System.Data;
using System.Collections.Generic;
using General;
namespace GeneralLogic
{    
    public class UbigeoLogic
    {
        private Ubigeo ubigeo;
        private ListaUbigeo lista;

        public UbigeoLogic()
        {
            ubigeo = new Ubigeo();
            lista = new ListaUbigeo();
        }
        public Mensaje EliminarUbigeo(int Gestor, int IdUbigeo, int IdUsuarioAuditoria)
        {
            Mensaje mensaje = new Mensaje();
            if (Gestor == DatosGlobales.GestorSqlServer)
            {
                System.Object[] ParamtrosOutPut = null; 
                ParamtrosOutPut = ConexionSqlServer.GDatos.Ejecutar(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paEliminarUbigeo", IdUbigeo, IdUsuarioAuditoria, mensaje.DescripcionMensaje, mensaje.CodigoMensaje);
                if (Convert.ToInt32(ParamtrosOutPut[100].ToString()) > 0)
                {
                    mensaje.CodigoMensaje = 1;
                    mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA EN LA CAPA DE DATOS [EliminarUbigeo] VERIFICAR CONSOLA";
                    mensaje.DescripcionMensajeSistema = ParamtrosOutPut[99].ToString();
                    return mensaje;
                }
                mensaje.DescripcionMensaje = ParamtrosOutPut[0].ToString();
                mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[1].ToString());
            }
            return mensaje;
        }

        public Ubigeo ObtenerUbigeo( int Gestor, int IdUbigeo)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (IdUbigeo == 0)
                {
                    return ubigeo;
                }

                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                     ds = ConexionSqlServer.GDatos.TraerDataSet(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paObtenerUbigeo", IdUbigeo);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        ubigeo.mensaje.CodigoMensaje = 1;
                        ubigeo.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ObtenerUbigeo], VERIFICAR CONSOLA";
                        ubigeo.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();

                        return ubigeo;
                    }
                    dt = ds.Tables[0].Copy();
                    ubigeo.IdUbigeo = (int)dt.Rows[0]["IdUbigeo"];
                    ubigeo.CodigoUbigeo = (string)dt.Rows[0]["CodigoUbigeo"];
                    ubigeo.CodigoDepartamento = (int)dt.Rows[0]["CodigoDepartamento"];
                    ubigeo.CodigoProvincia = (int)dt.Rows[0]["CodigoProvincia"];
                    ubigeo.CodigoDistrito = (int)dt.Rows[0]["CodigoDistrito"];
                    ubigeo.Descripcion = (string)dt.Rows[0]["Descripcion"];

                }
                return ubigeo;
            }
            catch (Exception ex)
            {
                ubigeo.mensaje.CodigoMensaje = 1;
                ubigeo.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ObtenerUbigeo], VERIFICAR CONSOLA";
                ubigeo.mensaje.DescripcionMensajeSistema = ex.Message;
                return ubigeo;
            }
            finally
            {
                ds.Dispose();
                ds.Clear();
                dt.Dispose();
                dt.Clear();
            }
        }


        public Ubigeo GuardaUbigeo( int Gestor, int IdUbigeo, string CodigoUbigeo, int CodigoDepartamento, int CodigoProvincia, int CodigoDistrito, string Descripcion, int IdUsuarioAuditoria)
        {
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    System.Object[] ParamtrosOutPut = null; 
                    ParamtrosOutPut = ConexionSqlServer.GDatos.Ejecutar(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paGuardarUbigeo", IdUbigeo, CodigoUbigeo, CodigoDepartamento, CodigoProvincia, CodigoDistrito, Descripcion, IdUsuarioAuditoria, "", 0);
                    if (Convert.ToInt32(ParamtrosOutPut[100].ToString()) > 0)
                    {
                        ubigeo.mensaje.CodigoMensaje = 1;
                        ubigeo.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [GuardaUbigeo], VERIFICAR CONSOLA";
                        ubigeo.mensaje.DescripcionMensajeSistema = ParamtrosOutPut[99].ToString();
                        return ubigeo;
                    }
                    ubigeo.IdUbigeo = Convert.ToInt32(ParamtrosOutPut[0].ToString());
                    ubigeo.mensaje.DescripcionMensaje = ParamtrosOutPut[1].ToString();
                    ubigeo.mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[2].ToString());
                }
                return ubigeo;
            }
            catch (Exception ex)
            {
                ubigeo.mensaje.CodigoMensaje = 1;
                ubigeo.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [GuardaUbigeo], VERIFICAR CONSOLA";
                ubigeo.mensaje.DescripcionMensajeSistema = ex.Message;
                return ubigeo;
            }
        }



        public ListaUbigeo ListarUbigeo( int Gestor, int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                     ds = ConexionSqlServer.GDatos.TraerDataSet(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paListarUbigeo", IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarUbigeo], VERIFICAR CONSOLA";
                        lista.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();
                        return lista;
                    }
                    dt = ds.Tables[0].Copy();
                    DataTable dtParametros = null;
                    dtParametros = ds.Tables[1].Copy();
                    lista.paginacion.TotalRegistros = Convert.ToInt32(dtParametros.Rows[0][0]);
                }
                Ubigeo ubigeo = null;
                foreach (DataRow row in dt.Rows)
                {
                    ubigeo = new Ubigeo();
                    ubigeo.IdUbigeo = (int)row["IdUbigeo"];
                    ubigeo.CodigoUbigeo = (string)row["CodigoUbigeo"];
                    ubigeo.CodigoDepartamento = (int)row["CodigoDepartamento"];
                    ubigeo.CodigoProvincia = (int)row["CodigoProvincia"];
                    ubigeo.CodigoDistrito = (int)row["CodigoDistrito"];
                    ubigeo.Descripcion = (string)row["Descripcion"];

                    lista.lista.Add(ubigeo);
                }
                return lista;
            }
            catch (Exception ex)
            {
                lista.mensaje.CodigoMensaje = 1;
                lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarUbigeo], VERIFICAR CONSOLA";
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
        public ListaUbigeo ListarComboDepartamento(int Gestor)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                     ds = ConexionSqlServer.GDatos.TraerDataSet(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paListarComboDepartamento");
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarComboDepartamento], VERIFICAR CONSOLA";
                        lista.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();
                        return lista;
                    }
                    dt = ds.Tables[0].Copy();
                }
                Ubigeo ubigeo = null;
                foreach (DataRow row in dt.Rows)
                {
                    ubigeo = new Ubigeo();
                    ubigeo.CodigoDepartamento = (int)row["CodigoDepartamento"];
                    ubigeo.Departamento = (string)row["Departamento"];
                    lista.lista.Add(ubigeo);
                }
                return lista;
            }
            catch (Exception ex)
            {
                lista.mensaje.CodigoMensaje = 1;
                lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarComboDepartamento], VERIFICAR CONSOLA";
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
        public ListaUbigeo ListarComboProvincia(int Gestor,int CodigoDepartamento)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                     ds = ConexionSqlServer.GDatos.TraerDataSet(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paListarComboProvincia",CodigoDepartamento);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarComboProvincia], VERIFICAR CONSOLA";
                        lista.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();
                        return lista;
                    }
                    dt = ds.Tables[0].Copy();
                }
                Ubigeo ubigeo = null;
                foreach (DataRow row in dt.Rows)
                {
                    ubigeo = new Ubigeo();
                    ubigeo.CodigoProvincia = (int)row["CodigoProvincia"];
                    ubigeo.Provincia = (string)row["Provincia"];
                    lista.lista.Add(ubigeo);
                }
                return lista;
            }
            catch (Exception ex)
            {
                lista.mensaje.CodigoMensaje = 1;
                lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarComboProvincia], VERIFICAR CONSOLA";
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
        public ListaUbigeo ListarComboDistrito(int Gestor, int CodigoDepartamento, int CodigoProvincia)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                     ds = ConexionSqlServer.GDatos.TraerDataSet(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paListarComboDistrito", CodigoDepartamento, CodigoProvincia);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarComboDistrito], VERIFICAR CONSOLA";
                        lista.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();
                        return lista;
                    }
                    dt = ds.Tables[0].Copy();
                }
                Ubigeo ubigeo = null;
                foreach (DataRow row in dt.Rows)
                {
                    ubigeo = new Ubigeo();
                    ubigeo.IdUbigeo = (int)row["IdUbigeo"];
					ubigeo.CodigoDistrito = (int)row["CodigoDistrito"];
					ubigeo.Distrito = (string)row["Distrito"];
                    lista.lista.Add(ubigeo);
                }
                return lista;
            }
            catch (Exception ex)
            {
                lista.mensaje.CodigoMensaje = 1;
                lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarComboDistrito], VERIFICAR CONSOLA";
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



