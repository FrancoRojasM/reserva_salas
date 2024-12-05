using System;
using System.Runtime.Serialization;
using Utilitarios;
using System.Data;
using System.Collections.Generic;
using Sistema;
namespace SistemaLogic
{    
    public class ModuloLogic
    {
        private Modulo modulo;
        private ListaModulo lista;

        public ModuloLogic()
        {
            modulo = new Modulo();
            lista = new ListaModulo();
        }
        public Mensaje EliminarModulo(int Gestor, int IdModulo, int IdUsuarioAuditoria)
        {
            Mensaje mensaje = new Mensaje();
            if (Gestor == DatosGlobales.GestorSqlServer)
            {
                System.Object[] ParamtrosOutPut = null; 
                ParamtrosOutPut = ConexionSqlServer.GDatos.Ejecutar(DatosGlobales.ListaConexiones.cnSistemaSql, "Sistema.paEliminarModulo", IdModulo, IdUsuarioAuditoria, mensaje.DescripcionMensaje, mensaje.CodigoMensaje);
                mensaje.DescripcionMensaje = ParamtrosOutPut[0].ToString();
                mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[1].ToString());
            }
            return mensaje;
        }

        public Modulo ObtenerModulo(int Gestor, int IdModulo)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (IdModulo == 0)
                {
                    return modulo;
                }

                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                     ds = ConexionSqlServer.GDatos.TraerDataSet(DatosGlobales.ListaConexiones.cnSistemaSql, "Sistema.paObtenerModulo", IdModulo);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        modulo.mensaje.CodigoMensaje = 1;
                        modulo.mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA EN LA CAPA DE DATOS [OBTENERModulo] VERIFICAR CONSOLA";
                        modulo.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();

                        return modulo;
                    }
                    dt = ds.Tables[0].Copy();
                    modulo.IdModulo = (int)dt.Rows[0]["IdModulo"];
                    modulo.NombreModulo = (string)dt.Rows[0]["NombreModulo"];
                    modulo.DetalleModulo = (string)dt.Rows[0]["DetalleModulo"];
                    modulo.OrdenModulo = (int)dt.Rows[0]["OrdenModulo"];
                    modulo.catalogotipomodulo.IdCatalogo = (int)dt.Rows[0]["IdCatalogoTipoModulo"];
                    modulo.Activo = (bool)dt.Rows[0]["Activo"];
                    modulo.RutaImagenModulo = (string)dt.Rows[0]["RutaImagenModulo"];

                }
                return modulo;
            }
            catch (Exception ex)
            {
                modulo.mensaje.CodigoMensaje = 1;
                modulo.mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA EN LA CAPA LOGICA [OBTENERModulo] VERIFICAR CONSOLA";
                modulo.mensaje.DescripcionMensajeSistema = ex.Message;
                return modulo;
            }
            finally
            {
                ds.Dispose();
                ds.Clear();
                dt.Dispose();
                dt.Clear();
            }
        }


        public Modulo GuardaModulo(int Gestor, int IdModulo, string NombreModulo, string DetalleModulo, int OrdenModulo, int IdCatalogoTipoModulo, bool Activo, string RutaImagenModulo, int IdUsuarioAuditoria)
        {
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    System.Object[] ParamtrosOutPut = null; 
                    ParamtrosOutPut = ConexionSqlServer.GDatos.Ejecutar(DatosGlobales.ListaConexiones.cnSistemaSql, "Sistema.paGuardarModulo", IdModulo, NombreModulo, DetalleModulo, OrdenModulo, IdCatalogoTipoModulo, Activo, RutaImagenModulo, IdUsuarioAuditoria, "", 0);
                    if (Convert.ToInt32(ParamtrosOutPut[100].ToString()) > 0)
                    {
                        modulo.mensaje.CodigoMensaje = 1;
                        modulo.mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA EN LA CAPA DE DATOS [GUARDAModulo] VERIFICAR CONSOLA";
                        modulo.mensaje.DescripcionMensajeSistema = ParamtrosOutPut[99].ToString();
                        return modulo;
                    }
                    modulo.IdModulo = Convert.ToInt32(ParamtrosOutPut[0].ToString());
                    modulo.mensaje.DescripcionMensaje = ParamtrosOutPut[1].ToString();
                    modulo.mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[2].ToString());
                }
                return modulo;
            }
            catch (Exception ex)
            {
                modulo.mensaje.CodigoMensaje = 1;
                modulo.mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA EN LA CAPA LOGICA [GUARDAModulo] VERIFICAR CONSOLA";
                modulo.mensaje.DescripcionMensajeSistema = ex.Message;
                return modulo;
            }
        }



        public ListaModulo ListarModulo(int Gestor, int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                     ds = ConexionSqlServer.GDatos.TraerDataSet(DatosGlobales.ListaConexiones.cnSistemaSql, "Sistema.paListarModulo", IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA EN LA CAPA DE DATOS [ListarModulo] VERIFICAR CONSOLA";
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
                    modulo = new Modulo();
                    modulo.IdModulo = (int)row["IdModulo"];
                    modulo.NombreModulo = (string)row["NombreModulo"];
                    modulo.DetalleModulo = (string)row["DetalleModulo"];
                    modulo.OrdenModulo = (int)row["OrdenModulo"];
                    modulo.catalogotipomodulo.Descripcion = (string)row["CatalogoTipoModulo"];
                    modulo.Activo = (bool)row["Activo"];
                    modulo.RutaImagenModulo = (string)row["RutaImagenModulo"];

                    lista.lista.Add(modulo);
                }
                return lista;
            }
            catch (Exception ex)
            {
                lista.mensaje.CodigoMensaje = 1;
                lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA EN LA CAPA LOGICA [ListarModulo] VERIFICAR CONSOLA";
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
        public ListaModulo ListarComboModulo(int Gestor)
        {
            DataTable dt = new DataTable();
            if (Gestor == DatosGlobales.GestorSqlServer)
            {
                DataSet ds = null;
                 ds = ConexionSqlServer.GDatos.TraerDataSet(DatosGlobales.ListaConexiones.cnSistemaSql, "Sistema.paListarComboModulo");
                if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                {
                    lista.mensaje.CodigoMensaje = 1;
                    lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA EN LA CAPA DE DATOS [ListarComboModulo] VERIFICAR CONSOLA";
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
                Modulo modulo = null;
                foreach (DataRow row in dt.Rows)
                {
                    modulo = new Modulo();
                    modulo.IdModulo = Convert.ToInt32(row["IdModulo"].ToString());
                    modulo.NombreModulo = row["NombreModulo"].ToString();
                    lista.lista.Add(modulo);
                }

            }
            return lista;
        }

    }    
}
