using System;
using System.Runtime.Serialization;
using Utilitarios;
using System.Data;
using System.Collections.Generic;
using Sistema;
namespace SistemaLogic
{    
    public class OpcionLogic
    {
        private Opcion opcion;
        private ListaOpcion lista;

        public OpcionLogic()
        {
            opcion = new Opcion();
            lista = new ListaOpcion();
        }
        public Mensaje EliminarOpcion(int Gestor, int IdOpcion, int IdUsuarioAuditoria)
        {
            Mensaje mensaje = new Mensaje();
            if (Gestor == DatosGlobales.GestorSqlServer)
            {
                System.Object[] ParamtrosOutPut = null; 
                ParamtrosOutPut = ConexionSqlServer.GDatos.Ejecutar(DatosGlobales.ListaConexiones.cnSistemaSql, "Sistema.paEliminarOpcion", IdOpcion, IdUsuarioAuditoria, mensaje.DescripcionMensaje, mensaje.CodigoMensaje);
                mensaje.DescripcionMensaje = ParamtrosOutPut[0].ToString();
                mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[1].ToString());
            }
            return mensaje;
        }

        public Opcion ObtenerOpcion(int Gestor, int IdOpcion)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (IdOpcion == 0)
                {
                    return opcion;
                }

                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                     ds = ConexionSqlServer.GDatos.TraerDataSet(DatosGlobales.ListaConexiones.cnSistemaSql, "Sistema.paObtenerOpcion", IdOpcion);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        opcion.mensaje.CodigoMensaje = 1;
                        opcion.mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA EN LA CAPA DE DATOS [OBTENEROpcion] VERIFICAR CONSOLA";
                        opcion.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();

                        return opcion;
                    }
                    dt = ds.Tables[0].Copy();
                    opcion.IdOpcion = (int)dt.Rows[0]["IdOpcion"];
                    opcion.modulo.IdModulo = (int)dt.Rows[0]["IdModulo"];
                    opcion.IdOpcionPadre = (int)dt.Rows[0]["IdOpcionPadre"];
                    opcion.NombreOpcion = (string)dt.Rows[0]["NombreOpcion"];
                    opcion.DetalleOpcion = (string)dt.Rows[0]["DetalleOpcion"];
                    opcion.catalogotipoopcion.IdCatalogo = (int)dt.Rows[0]["IdCatalogoTipoOpcion"];
                    opcion.OrdenOpcion = (int)dt.Rows[0]["OrdenOpcion"];
                    opcion.RutaImagenOpcion = (string)dt.Rows[0]["RutaImagenOpcion"];
                    opcion.Activo = (bool)dt.Rows[0]["Activo"];

                }
                return opcion;
            }
            catch (Exception ex)
            {
                opcion.mensaje.CodigoMensaje = 1;
                opcion.mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA EN LA CAPA LOGICA [OBTENEROpcion] VERIFICAR CONSOLA";
                opcion.mensaje.DescripcionMensajeSistema = ex.Message;
                return opcion;
            }
            finally
            {
                ds.Dispose();
                ds.Clear();
                dt.Dispose();
                dt.Clear();
            }
        }


        public Opcion GuardaOpcion(int Gestor, int IdOpcion, int IdModulo, int IdOpcionPadre, string NombreOpcion, string DetalleOpcion, int IdCatalogoTipoOpcion, int OrdenOpcion, string RutaImagenOpcion, bool Activo, int IdUsuarioAuditoria)
        {
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    System.Object[] ParamtrosOutPut = null; 
                    ParamtrosOutPut = ConexionSqlServer.GDatos.Ejecutar(DatosGlobales.ListaConexiones.cnSistemaSql, "Sistema.paGuardarOpcion", IdOpcion, IdModulo, IdOpcionPadre, NombreOpcion, DetalleOpcion, IdCatalogoTipoOpcion, OrdenOpcion, RutaImagenOpcion, Activo, IdUsuarioAuditoria, "", 0);
                    if (Convert.ToInt32(ParamtrosOutPut[100].ToString()) > 0)
                    {
                        opcion.mensaje.CodigoMensaje = 1;
                        opcion.mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA EN LA CAPA DE DATOS [GUARDAOpcion] VERIFICAR CONSOLA";
                        opcion.mensaje.DescripcionMensajeSistema = ParamtrosOutPut[99].ToString();
                        return opcion;
                    }
                    opcion.IdOpcion = Convert.ToInt32(ParamtrosOutPut[0].ToString());
                    opcion.mensaje.DescripcionMensaje = ParamtrosOutPut[1].ToString();
                    opcion.mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[2].ToString());
                }
                return opcion;
            }
            catch (Exception ex)
            {
                opcion.mensaje.CodigoMensaje = 1;
                opcion.mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA EN LA CAPA LOGICA [GUARDAOpcion] VERIFICAR CONSOLA";
                opcion.mensaje.DescripcionMensajeSistema = ex.Message;
                return opcion;
            }
        }



        public ListaOpcion ListarOpcion( int Gestor, int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                     ds = ConexionSqlServer.GDatos.TraerDataSet(DatosGlobales.ListaConexiones.cnSistemaSql, "Sistema.paListarOpcion", IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA EN LA CAPA DE DATOS [ListarOpcion] VERIFICAR CONSOLA";
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
                    opcion = new Opcion();
                    opcion.IdOpcion = (int)row["IdOpcion"];
                    opcion.modulo.NombreModulo = (string)row["NombreModulo"];
                    opcion.IdOpcionPadre = (int)row["IdOpcionPadre"];
                    opcion.NombreOpcion = (string)row["NombreOpcion"];
                    opcion.NombreOpcionPadre = (string)row["NombreOpcionPadre"];
                    opcion.DetalleOpcion = (string)row["DetalleOpcion"];
                    opcion.catalogotipoopcion.Descripcion = (string)row["CatalogoTipoOpcion"];
                    opcion.OrdenOpcion = (int)row["OrdenOpcion"];
                    opcion.RutaImagenOpcion = (string)row["RutaImagenOpcion"];
                    opcion.Activo = (bool)row["Activo"];

                    lista.lista.Add(opcion);
                }
                return lista;
            }
            catch (Exception ex)
            {
                lista.mensaje.CodigoMensaje = 1;
                lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA EN LA CAPA LOGICA [ListarOpcion] VERIFICAR CONSOLA";
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
        public ListaOpcion ListarComboOpcion( int Gestor)
        {
            DataTable dt = new DataTable();
            if (Gestor == DatosGlobales.GestorSqlServer)
            {
                DataSet ds = null;
                 ds = ConexionSqlServer.GDatos.TraerDataSet(DatosGlobales.ListaConexiones.cnSistemaSql, "Sistema.paListarComboOpcion");
                if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                {
                    lista.mensaje.CodigoMensaje = 1;
                    lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA EN LA CAPA DE DATOS [ListarComboOpcion] VERIFICAR CONSOLA";
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
                    opcion = new Opcion();
                    opcion.IdOpcion = Convert.ToInt32(row["IdOpcion"].ToString());
                    opcion.NombreOpcion = row["NombreOpcion"].ToString();
                    lista.lista.Add(opcion);
                }

            }
            return lista;
        }
        public ListaOpcion ListarComboOpcionPadre(int Gestor, int IdModulo)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            try
            {              
                if (Gestor == DatosGlobales.GestorSqlServer)
                {                
                     ds = ConexionSqlServer.GDatos.TraerDataSet(DatosGlobales.ListaConexiones.cnSistemaSql, "Sistema.paListarComboOpcionPadre", IdModulo);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA EN LA CAPA DE DATOS [ListarComboOpcionPadre] VERIFICAR CONSOLA";
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
                    Opcion opcion = null;
                    foreach (DataRow row in dt.Rows)
                    {
                        opcion = new Opcion();
                        opcion.IdOpcion = Convert.ToInt32(row["IdOpcion"].ToString());
                        opcion.NombreOpcion = row["NombreOpcion"].ToString();
                        lista.lista.Add(opcion);
                    }

                }
                return lista;
            }
            catch (Exception ex)
            {
                lista.mensaje.CodigoMensaje = 1;
                lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA EN LA CAPA LOGICA [ListarOpcionPadre] VERIFICAR CONSOLA";
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



