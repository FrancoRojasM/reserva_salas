using System;
using System.Runtime.Serialization;
using Utilitarios;
using System.Data;
using System.Collections.Generic;
using Seguridad;
namespace SeguridadLogic
{    
    public class PerfilLogic
    {
        private Perfil perfil;
        private ListaPerfil lista;

        public PerfilLogic()
        {
            perfil = new Perfil();
            lista = new ListaPerfil();
        }
        public Mensaje EliminarPerfil(int Gestor, int IdPerfil, int IdUsuarioAuditoria)
        {
            Mensaje mensaje = new Mensaje();
            if (Gestor == DatosGlobales.GestorSqlServer)
            {
                System.Object[] ParamtrosOutPut = null; 
                ParamtrosOutPut = ConexionSqlServer.GDatos.Ejecutar(DatosGlobales.ListaConexiones.cnSeguridadSql, "Seguridad.paEliminarPerfil", IdPerfil, IdUsuarioAuditoria, mensaje.DescripcionMensaje, mensaje.CodigoMensaje);
                if (Convert.ToInt32(ParamtrosOutPut[100].ToString()) > 0)
                {
                    mensaje.CodigoMensaje = 1;
                    mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA EN LA CAPA DE DATOS [EliminarPerfil] VERIFICAR CONSOLA";
                    mensaje.DescripcionMensajeSistema = ParamtrosOutPut[99].ToString();
                    return mensaje;
                }
                mensaje.DescripcionMensaje = ParamtrosOutPut[0].ToString();
                mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[1].ToString());
            }
            return mensaje;
        }
        public Perfil ObtenerPerfil(int Gestor, int IdPerfil)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (IdPerfil == 0)
                {
                    return perfil;
                }

                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                     ds = ConexionSqlServer.GDatos.TraerDataSet(DatosGlobales.ListaConexiones.cnSeguridadSql, "Seguridad.paObtenerPerfil", IdPerfil);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        perfil.mensaje.CodigoMensaje = 1;
                        perfil.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ObtenerPerfil], VERIFICAR CONSOLA";
                        perfil.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();

                        return perfil;
                    }
                    dt = ds.Tables[0].Copy();
                    perfil.IdPerfil = (int)dt.Rows[0]["IdPerfil"];
                    perfil.NombrePerfil = (string)dt.Rows[0]["NombrePerfil"];
                    perfil.DetallePerfil = (string)dt.Rows[0]["DetallePerfil"];
                    perfil.Activo = (bool)dt.Rows[0]["Activo"];

                }
                return perfil;
            }
            catch (Exception ex)
            {
                perfil.mensaje.CodigoMensaje = 1;
                perfil.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ObtenerPerfil], VERIFICAR CONSOLA";
                perfil.mensaje.DescripcionMensajeSistema = ex.Message;
                return perfil;
            }
            finally
            {
                ds.Dispose();
                ds.Clear();
                dt.Dispose();
                dt.Clear();
            }
        }


        public Perfil GuardaPerfil(int Gestor, int IdPerfil, string NombrePerfil, string DetallePerfil, bool Activo, int IdUsuarioAuditoria)
        {
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    System.Object[] ParamtrosOutPut = null; 
                    ParamtrosOutPut = ConexionSqlServer.GDatos.Ejecutar(DatosGlobales.ListaConexiones.cnSeguridadSql, "Seguridad.paGuardarPerfil", IdPerfil, NombrePerfil, DetallePerfil, Activo, IdUsuarioAuditoria, "", 0);
                    if (Convert.ToInt32(ParamtrosOutPut[100].ToString()) > 0)
                    {
                        perfil.mensaje.CodigoMensaje = 1;
                        perfil.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [GuardaPerfil], VERIFICAR CONSOLA";
                        perfil.mensaje.DescripcionMensajeSistema = ParamtrosOutPut[99].ToString();
                        return perfil;
                    }
                    perfil.IdPerfil = Convert.ToInt32(ParamtrosOutPut[0].ToString());
                    perfil.mensaje.DescripcionMensaje = ParamtrosOutPut[1].ToString();
                    perfil.mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[2].ToString());
                }
                return perfil;
            }
            catch (Exception ex)
            {
                perfil.mensaje.CodigoMensaje = 1;
                perfil.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [GuardaPerfil], VERIFICAR CONSOLA";
                perfil.mensaje.DescripcionMensajeSistema = ex.Message;
                return perfil;
            }
        }
        public ListaPerfil ListarPerfil(int Gestor, int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                     ds = ConexionSqlServer.GDatos.TraerDataSet(DatosGlobales.ListaConexiones.cnSeguridadSql, "Seguridad.paListarPerfil", IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarPerfil], VERIFICAR CONSOLA";
                        lista.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();
                        return lista;
                    }
                    dt = ds.Tables[0].Copy();
                    DataTable dtParametros = null;
                    dtParametros = ds.Tables[1].Copy();
                    lista.paginacion.TotalRegistros = Convert.ToInt32(dtParametros.Rows[0][0]);
                }
                Perfil perfil = null;
                foreach (DataRow row in dt.Rows)
                {
                    perfil = new Perfil();
                    perfil.IdPerfil = (int)row["IdPerfil"];
                    perfil.NombrePerfil = (string)row["NombrePerfil"];
                    perfil.DetallePerfil = (string)row["DetallePerfil"];
                    perfil.Activo = (bool)row["Activo"];

                    lista.lista.Add(perfil);
                }
                return lista;
            }
            catch (Exception ex)
            {
                lista.mensaje.CodigoMensaje = 1;
                lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarPerfil], VERIFICAR CONSOLA";
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
        public ListaPerfil ListarComboPerfil(int Gestor)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                     ds = ConexionSqlServer.GDatos.TraerDataSet(DatosGlobales.ListaConexiones.cnSeguridadSql, "Seguridad.paListarComboPerfil");
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarComboPerfil], VERIFICAR CONSOLA";
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
                    Perfil perfil = null;
                    foreach (DataRow row in dt.Rows)
                    {
                        perfil = new Perfil();
                        perfil.IdPerfil = Convert.ToInt32(row["IdPerfil"].ToString());
                        perfil.NombrePerfil = row["NombrePerfil"].ToString();
                        lista.lista.Add(perfil);
                    }

                }
                return lista;
            }
            catch (Exception ex)
            {
                lista.mensaje.CodigoMensaje = 1;
                lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarComboPerfil], VERIFICAR CONSOLA";
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



