using System;
using System.Runtime.Serialization;
using Utilitarios;
using System.Data;
using System.Collections.Generic;
using Inventario;
using System.Threading.Tasks;
using General;
using Seguridad;
using System.Drawing;
using System.Data.SqlClient;

namespace InventarioLogic
{

    public class ArticuloLogic
    {
        private Articulo articulo;
        private ListaArticulo lista;
        private Sucursal sucursal;

        public ArticuloLogic()
        {
            articulo = new Articulo();
            lista = new ListaArticulo();
        }
        public Mensaje EliminarArticulo(int Gestor, int Id, int IdUsuarioAuditoria)
        {
            Mensaje mensaje = new Mensaje();
            if (Gestor == DatosGlobales.GestorSqlServer)
            {
                object[] ParamtrosOutPut = null;
                ParamtrosOutPut = ConexionSqlServer.GDatos.Ejecutar(DatosGlobales.ListaConexiones.cnInventarioSql, "Inventario.paEliminarArticulo", Id, IdUsuarioAuditoria, mensaje.DescripcionMensaje, mensaje.CodigoMensaje);
                if (Convert.ToInt32(ParamtrosOutPut[100].ToString()) > 0)
                {
                    mensaje.CodigoMensaje = 1;
                    mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA EN LA CAPA DE DATOS [EliminarArticulo] VERIFICAR CONSOLA";
                    mensaje.DescripcionMensajeSistema = ParamtrosOutPut[99].ToString();
                    return mensaje;
                }
                mensaje.DescripcionMensaje = ParamtrosOutPut[0].ToString();
                mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[1].ToString());
            }
            return mensaje;
        }
        public async Task<Mensaje> EliminarArticuloAsync(int Gestor, int Id, int IdUsuarioAuditoria)
        {
            Mensaje mensaje = new Mensaje();
            if (Gestor == DatosGlobales.GestorSqlServer)
            {
                object[] ParamtrosOutPut = null;
                ParamtrosOutPut = await ConexionSqlServer.GDatos.EjecutarAsync(DatosGlobales.ListaConexiones.cnInventarioSql, "Inventario.paEliminarArticulo", Id, IdUsuarioAuditoria, mensaje.DescripcionMensaje, mensaje.CodigoMensaje);
                if (Convert.ToInt32(ParamtrosOutPut[100].ToString()) > 0)
                {
                    mensaje.CodigoMensaje = 1;
                    mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA EN LA CAPA DE DATOS [EliminarArticulo] VERIFICAR CONSOLA";
                    mensaje.DescripcionMensajeSistema = ParamtrosOutPut[99].ToString();
                    return mensaje;
                }
                mensaje.DescripcionMensaje = ParamtrosOutPut[0].ToString();
                mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[1].ToString());
            }
            return mensaje;
        }
        public Articulo ObtenerArticulo(int Gestor, int Id)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Id == 0)
                {
                    return articulo;
                }

                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    ds = ConexionSqlServer.GDatos.TraerDataSet(DatosGlobales.ListaConexiones.cnInventarioSql, "Inventario.paObtenerArticulo", Id);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        articulo.mensaje.CodigoMensaje = 1;
                        articulo.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ObtenerArticulo], VERIFICAR CONSOLA";
                        articulo.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();

                        return articulo;
                    }
                    dt = ds.Tables[0].Copy();
                    articulo.Id = (int)dt.Rows[0]["Id"];
                    articulo.Codigo_Barra = dt.Rows[0]["Codigo_Barra"] as string ?? string.Empty;
                    articulo.ItemCode = dt.Rows[0]["ItemCode"] as string ?? string.Empty;
                    articulo.Ubicacion_Region = dt.Rows[0]["Ubicacion_Region"] as string ?? string.Empty;
                    articulo.Ubicacion_Sede = dt.Rows[0]["Ubicacion_Sede"] as string ?? string.Empty;
                    articulo.Ubicacion_Area = dt.Rows[0]["Ubicacion_Area"] as string ?? string.Empty;
                    articulo.Ubicacion_Sub_Area = dt.Rows[0]["Ubicacion_Sub_Area"] as string ?? string.Empty;
                    articulo.Piso = dt.Rows[0]["Piso"] as string ?? string.Empty;
                    articulo.ItemName = dt.Rows[0]["ItemName"] as string ?? string.Empty;
                    articulo.Detalle = dt.Rows[0]["Detalle"] as string ?? string.Empty;
                    articulo.Marca = dt.Rows[0]["Marca"] as string ?? string.Empty;
                    articulo.Modelo = dt.Rows[0]["Modelo"] as string ?? string.Empty;
                    articulo.Serie = dt.Rows[0]["Serie"] as string ?? string.Empty;
                    articulo.Material = dt.Rows[0]["Material"] as string ?? string.Empty;
                    articulo.Medida = dt.Rows[0]["Medida"] as string ?? string.Empty;
                    articulo.Color = dt.Rows[0]["Color"] as string ?? string.Empty;
                    articulo.Estado = dt.Rows[0]["Estado"] as string ?? string.Empty;
                    articulo.Condicion_Uso = dt.Rows[0]["Condicion_Uso"] as string ?? string.Empty;
                    articulo.Usuario = dt.Rows[0]["Usuario"] as string ?? string.Empty;
                    articulo.Documento = dt.Rows[0]["Documento"] as string ?? string.Empty;
                    articulo.Cargo = dt.Rows[0]["Cargo"] as string ?? string.Empty;
                    articulo.Gerencia = dt.Rows[0]["Gerencia"] as string ?? string.Empty;
                    articulo.GroupName = dt.Rows[0]["GroupName"] as string ?? string.Empty;
                    articulo.EstadoConta = dt.Rows[0]["EstadoConta"] as string ?? string.Empty;
                    articulo.Periodo = dt.Rows[0]["Periodo"] as string ?? string.Empty;
                    articulo.Tipo_Inventario = dt.Rows[0]["Tipo_Inventario"] as string ?? string.Empty;
                    articulo.Tipo_Asignacion = dt.Rows[0]["Tipo_Asignacion"] as string ?? string.Empty;
                    articulo.Sucursal = dt.Rows[0]["Sucursal"] != DBNull.Value ? Convert.ToInt32(dt.Rows[0]["Sucursal"]) : 0;
                    articulo.Image1 = dt.Rows[0]["Image1"] as string ?? string.Empty;
                    articulo.Image2 = dt.Rows[0]["Image2"] as string ?? string.Empty;
                    articulo.Image3 = dt.Rows[0]["Image3"] as string ?? string.Empty;
                    articulo.Image4 = dt.Rows[0]["Image4"] as string ?? string.Empty;

                }
                return articulo;
            }
            catch (Exception ex)
            {
                articulo.mensaje.CodigoMensaje = 1;
                articulo.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ObtenerArticulo], VERIFICAR CONSOLA";
                articulo.mensaje.DescripcionMensajeSistema = ex.Message;
                return articulo;
            }
            finally
            {
                ds.Dispose();
                ds.Clear();
                dt.Dispose();
                dt.Clear();
            }
        }

        public async Task<Articulo> ObtenerArticuloAsync(int Gestor, int Id)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Id == 0)
                {
                    return articulo;
                }

                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnInventarioSql, "Inventario.paObtenerArticulo", Id);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        articulo.mensaje.CodigoMensaje = 1;
                        articulo.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ObtenerArticulo], VERIFICAR CONSOLA";
                        articulo.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();

                        return articulo;
                    }
                    dt = ds.Tables[0].Copy();
                    if (dt.Rows.Count > 0)
                    {
                        articulo.Id = (int)dt.Rows[0]["Id"];
                        articulo.Codigo_Barra = dt.Rows[0]["Codigo_Barra"] as string ?? string.Empty;
                        articulo.ItemCode = dt.Rows[0]["ItemCode"] as string ?? string.Empty;
                        articulo.Ubicacion_Region = dt.Rows[0]["Ubicacion_Region"] as string ?? string.Empty;
                        articulo.Ubicacion_Sede = dt.Rows[0]["Ubicacion_Sede"] as string ?? string.Empty;
                        articulo.Ubicacion_Area = dt.Rows[0]["Ubicacion_Area"] as string ?? string.Empty;
                        articulo.Ubicacion_Sub_Area = dt.Rows[0]["Ubicacion_Sub_Area"] as string ?? string.Empty;
                        articulo.Piso = dt.Rows[0]["Piso"] as string ?? string.Empty;
                        articulo.ItemName = dt.Rows[0]["ItemName"] as string ?? string.Empty;
                        articulo.Detalle = dt.Rows[0]["Detalle"] as string ?? string.Empty;
                        articulo.Marca = dt.Rows[0]["Marca"] as string ?? string.Empty;
                        articulo.Modelo = dt.Rows[0]["Modelo"] as string ?? string.Empty;
                        articulo.Serie = dt.Rows[0]["Serie"] as string ?? string.Empty;
                        articulo.Material = dt.Rows[0]["Material"] as string ?? string.Empty;
                        articulo.Medida = dt.Rows[0]["Medida"] as string ?? string.Empty;
                        articulo.Color = dt.Rows[0]["Color"] as string ?? string.Empty;
                        articulo.Estado = dt.Rows[0]["Estado"] as string ?? string.Empty;
                        articulo.Condicion_Uso = dt.Rows[0]["Condicion_Uso"] as string ?? string.Empty;
                        articulo.Usuario = dt.Rows[0]["Usuario"] as string ?? string.Empty;
                        articulo.Documento = dt.Rows[0]["Documento"] as string ?? string.Empty;
                        articulo.Cargo = dt.Rows[0]["Cargo"] as string ?? string.Empty;
                        articulo.Gerencia = dt.Rows[0]["Gerencia"] as string ?? string.Empty;
                        articulo.GroupName = dt.Rows[0]["GroupName"] as string ?? string.Empty;
                        articulo.EstadoConta = dt.Rows[0]["EstadoConta"] as string ?? string.Empty;
                        articulo.Periodo = dt.Rows[0]["Periodo"] as string ?? string.Empty;
                        articulo.Tipo_Inventario = dt.Rows[0]["Tipo_Inventario"] as string ?? string.Empty;
                        articulo.Tipo_Asignacion = dt.Rows[0]["Tipo_Asignacion"] as string ?? string.Empty;
                        articulo.Sucursal = dt.Rows[0]["Sucursal"] != DBNull.Value ? Convert.ToInt32(dt.Rows[0]["Sucursal"]) : 0;
                        articulo.Image1 = dt.Rows[0]["Image1"] as string ?? string.Empty;
                        articulo.Image2 = dt.Rows[0]["Image2"] as string ?? string.Empty;
                        articulo.Image3 = dt.Rows[0]["Image3"] as string ?? string.Empty;
                        articulo.Image4 = dt.Rows[0]["Image4"] as string ?? string.Empty;
                    }
                    
                }
                return articulo;
            }
            catch (Exception ex)
            {
                articulo.mensaje.CodigoMensaje = 1;
                articulo.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ObtenerArticulo], VERIFICAR CONSOLA";
                articulo.mensaje.DescripcionMensajeSistema = ex.Message;
                return articulo;
            }
            finally
            {
                ds.Dispose();
                ds.Clear();
                dt.Dispose();
                dt.Clear();
            }
        }

        public async Task<Articulo> ObtenerArticuloPorCodeAsync(int Gestor, string Id)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnInventarioSql, "Inventario.paObtenerArticuloPorCode", Id);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        articulo.mensaje.CodigoMensaje = 1;
                        articulo.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ObtenerArticuloPorCode], VERIFICAR CONSOLA";
                        articulo.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();

                        return articulo;
                    }
                    dt = ds.Tables[0].Copy();
                    if (dt.Rows.Count > 0)
                    {
                        articulo.Id = (int)dt.Rows[0]["Id"];
                        articulo.Codigo_Barra = dt.Rows[0]["Codigo_Barra"] as string ?? string.Empty;
                        articulo.ItemCode = dt.Rows[0]["ItemCode"] as string ?? string.Empty;
                        articulo.Ubicacion_Region = dt.Rows[0]["Ubicacion_Region"] as string ?? string.Empty;
                        articulo.Ubicacion_Sede = dt.Rows[0]["Ubicacion_Sede"] as string ?? string.Empty;
                        articulo.Ubicacion_Area = dt.Rows[0]["Ubicacion_Area"] as string ?? string.Empty;
                        articulo.Ubicacion_Sub_Area = dt.Rows[0]["Ubicacion_Sub_Area"] as string ?? string.Empty;
                        articulo.Piso = dt.Rows[0]["Piso"] as string ?? string.Empty;
                        articulo.ItemName = dt.Rows[0]["ItemName"] as string ?? string.Empty;
                        articulo.Detalle = dt.Rows[0]["Detalle"] as string ?? string.Empty;
                        articulo.Marca = dt.Rows[0]["Marca"] as string ?? string.Empty;
                        articulo.Modelo = dt.Rows[0]["Modelo"] as string ?? string.Empty;
                        articulo.Serie = dt.Rows[0]["Serie"] as string ?? string.Empty;
                        articulo.Material = dt.Rows[0]["Material"] as string ?? string.Empty;
                        articulo.Medida = dt.Rows[0]["Medida"] as string ?? string.Empty;
                        articulo.Color = dt.Rows[0]["Color"] as string ?? string.Empty;
                        articulo.Estado = dt.Rows[0]["Estado"] as string ?? string.Empty;
                        articulo.Condicion_Uso = dt.Rows[0]["Condicion_Uso"] as string ?? string.Empty;
                        articulo.Usuario = dt.Rows[0]["Usuario"] as string ?? string.Empty;
                        articulo.Documento = dt.Rows[0]["Documento"] as string ?? string.Empty;
                        articulo.Cargo = dt.Rows[0]["Cargo"] as string ?? string.Empty;
                        articulo.Gerencia = dt.Rows[0]["Gerencia"] as string ?? string.Empty;
                        articulo.GroupName = dt.Rows[0]["GroupName"] as string ?? string.Empty;
                        articulo.EstadoConta = dt.Rows[0]["EstadoConta"] as string ?? string.Empty;
                        articulo.Periodo = dt.Rows[0]["Periodo"] as string ?? string.Empty;
                        articulo.Tipo_Inventario = dt.Rows[0]["Tipo_Inventario"] as string ?? string.Empty;
                        articulo.Tipo_Asignacion = dt.Rows[0]["Tipo_Asignacion"] as string ?? string.Empty;
                        articulo.Sucursal = dt.Rows[0]["Sucursal"] != DBNull.Value ? Convert.ToInt32(dt.Rows[0]["Sucursal"]) : 0;
                        articulo.Image1 = dt.Rows[0]["Image1"] as string ?? string.Empty;
                        articulo.Image2 = dt.Rows[0]["Image2"] as string ?? string.Empty;
                        articulo.Image3 = dt.Rows[0]["Image3"] as string ?? string.Empty;
                        articulo.Image4 = dt.Rows[0]["Image4"] as string ?? string.Empty;
                    }

                }
                return articulo;
            }
            catch (Exception ex)
            {
                articulo.mensaje.CodigoMensaje = 1;
                articulo.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ObtenerArticuloPorCode], VERIFICAR CONSOLA";
                articulo.mensaje.DescripcionMensajeSistema = ex.Message;
                return articulo;
            }
            finally
            {
                ds.Dispose();
                ds.Clear();
                dt.Dispose();
                dt.Clear();
            }
        }

        public Articulo GuardaArticulo(int Gestor, int Id)
        {
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    object[] ParamtrosOutPut = null;
                    ParamtrosOutPut = ConexionSqlServer.GDatos.Ejecutar(DatosGlobales.ListaConexiones.cnInventarioSql, "Inventario.paGuardarArticulo", Id);
                    if (Convert.ToInt32(ParamtrosOutPut[100].ToString()) > 0)
                    {
                        articulo.mensaje.CodigoMensaje = 1;
                        articulo.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [GuardaArticulo], VERIFICAR CONSOLA";
                        articulo.mensaje.DescripcionMensajeSistema = ParamtrosOutPut[99].ToString();
                        return articulo;
                    }
                    articulo.Id = Convert.ToInt32(ParamtrosOutPut[0].ToString());
                    articulo.mensaje.DescripcionMensaje = ParamtrosOutPut[1].ToString();
                    articulo.mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[2].ToString());
                }
                return articulo;
            }
            catch (Exception ex)
            {
                articulo.mensaje.CodigoMensaje = 1;
                articulo.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [GuardaArticulo], VERIFICAR CONSOLA";
                articulo.mensaje.DescripcionMensajeSistema = ex.Message;
                return articulo;
            }
        }
        public async Task<Articulo> GuardaArticuloAsync(int Gestor, int Id, string Codigo_Barra, string ItemCode, string Ubicacion_Region, string Ubicacion_Sede, string Ubicacion_Area, string Ubicacion_Sub_Area, string Piso, string ItemName, string Detalle, string Marca, string Modelo, string Serie, string Material, string Medida, string Color, string Estado, string Condicion_Uso, string Usuario, string Documento, string Cargo, string Gerencia, string Periodo, string Tipo_Inventario, string Tipo_Asignacion, int Sucursal, string image1, string image2, string image3, string image4, int IdUsuarioAuditoria)
        {
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    object[] ParamtrosOutPut = null;
                    ParamtrosOutPut = await ConexionSqlServer.GDatos.EjecutarAsync(DatosGlobales.ListaConexiones.cnInventarioSql, "Inventario.paGuardarArticulo", Id, Codigo_Barra, ItemCode, Ubicacion_Region, Ubicacion_Sede, Ubicacion_Area, Ubicacion_Sub_Area, Piso, ItemName, Detalle, Marca, Modelo, Serie, Material, Medida, Color, Estado, Condicion_Uso, Usuario, Documento, Cargo, Gerencia, Periodo, Tipo_Inventario, Tipo_Asignacion, Sucursal, image1, image2, image3, image4, IdUsuarioAuditoria, "", 0);
                    if (Convert.ToInt32(ParamtrosOutPut[100].ToString()) > 0)
                    {
                        articulo.mensaje.CodigoMensaje = 1;
                        articulo.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [GuardaArticulo], VERIFICAR CONSOLA";
                        articulo.mensaje.DescripcionMensajeSistema = ParamtrosOutPut[99].ToString();
                        return articulo;
                    }
                    articulo = await ObtenerArticuloAsync(Gestor, Convert.ToInt32(ParamtrosOutPut[0].ToString()));
                    articulo.mensaje.DescripcionMensaje = ParamtrosOutPut[1].ToString();
                    articulo.mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[2].ToString());
                }
                return articulo;
            }
            catch (Exception ex)
            {
                articulo.mensaje.CodigoMensaje = 1;
                articulo.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [GuardaArticulo], VERIFICAR CONSOLA";
                articulo.mensaje.DescripcionMensajeSistema = ex.Message;
                return articulo;
            }
        }
        public ListaArticulo ListarArticulo(int Gestor, int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    ds = ConexionSqlServer.GDatos.TraerDataSet(DatosGlobales.ListaConexiones.cnInventarioSql, "Inventario.paListarArticulo", IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarArticulo], VERIFICAR CONSOLA";
                        lista.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();
                        return lista;
                    }
                    dt = ds.Tables[0].Copy();
                    DataTable dtParametros = null;
                    dtParametros = ds.Tables[1].Copy();
                    lista.paginacion.TotalRegistros = Convert.ToInt32(dtParametros.Rows[0][0]);
                }
                Articulo articulo = null;
                foreach (DataRow row in dt.Rows)
                {
                    articulo = new Articulo
                    {
                        Id = (int)row["Id"],
                        Codigo_Barra = row["Codigo_Barra"] as string ?? string.Empty,
                        ItemCode = row["ItemCode"] as string ?? string.Empty,
                        Ubicacion_Region = row["Ubicacion_Region"] as string ?? string.Empty,
                        Ubicacion_Sede = row["Ubicacion_Sede"] as string ?? string.Empty,
                        Ubicacion_Area = row["Ubicacion_Area"] as string ?? string.Empty,
                        Ubicacion_Sub_Area = row["Ubicacion_Sub_Area"] as string ?? string.Empty,
                        Piso = row["Piso"] as string ?? string.Empty,
                        ItemName = row["ItemName"] as string ?? string.Empty,
                        Detalle = row["Detalle"] as string ?? string.Empty,
                        Marca = row["Marca"] as string ?? string.Empty,
                        Modelo = row["Modelo"] as string ?? string.Empty,
                        Serie = row["Serie"] as string ?? string.Empty,
                        Material = row["Material"] as string ?? string.Empty,
                        Medida = row["Medida"] as string ?? string.Empty,
                        Color = row["Color"] as string ?? string.Empty,
                        Estado = row["Estado"] as string ?? string.Empty,
                        Condicion_Uso = row["Condicion_Uso"] as string ?? string.Empty,
                        Usuario = row["Usuario"] as string ?? string.Empty,
                        Documento = row["Documento"] as string ?? string.Empty,
                        Cargo = row["Cargo"] as string ?? string.Empty,
                        Gerencia = row["Gerencia"] as string ?? string.Empty,
                        GroupName = row["GroupName"] as string ?? string.Empty,
                        EstadoConta = row["EstadoConta"] as string ?? string.Empty,
                        Periodo = row["Periodo"] as string ?? string.Empty,
                        Tipo_Inventario = row["Tipo_Inventario"] as string ?? string.Empty,
                        Tipo_Asignacion = row["Tipo_Asignacion"] as string ?? string.Empty,
                        Sucursal = row["Sucursal"] != DBNull.Value ? Convert.ToInt32(row["Sucursal"]) : 0,
                        Image1 = row["Image1"] as string ?? string.Empty,
                        Image2 = row["Image2"] as string ?? string.Empty,
                        Image3 = row["Image3"] as string ?? string.Empty,
                        Image4 = row["Image4"] as string ?? string.Empty,
                        US_Registro = row["US_Registro"] as string ?? string.Empty,
                    }; 
                    lista.lista.Add(articulo);
                }
                return lista;
            }
            catch (Exception ex)
            {
                lista.mensaje.CodigoMensaje = 1;
                lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarArticulo], VERIFICAR CONSOLA";
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
        public async Task<ListaArticulo> ListarArticuloAsync(int Gestor, int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnInventarioSql, "Inventario.paListarArticulo", IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarArticulo], VERIFICAR CONSOLA";
                        lista.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();
                        return lista;
                    }
                    dt = ds.Tables[0].Copy();
                    DataTable dtParametros = null;
                    dtParametros = ds.Tables[1].Copy();
                    lista.paginacion.TotalRegistros = Convert.ToInt32(dtParametros.Rows[0][0]);
                }
                Articulo articulo = null;
                foreach (DataRow row in dt.Rows)
                {
                    articulo = new Articulo
                    {
                        Id = (int)row["Id"],
                        Codigo_Barra = row["Codigo_Barra"] as string ?? string.Empty,
                        ItemCode = row["ItemCode"] as string ?? string.Empty,
                        Ubicacion_Region = row["Ubicacion_Region"] as string ?? string.Empty,
                        Ubicacion_Sede = row["Ubicacion_Sede"] as string ?? string.Empty,
                        Ubicacion_Area = row["Ubicacion_Area"] as string ?? string.Empty,
                        Ubicacion_Sub_Area = row["Ubicacion_Sub_Area"] as string ?? string.Empty,
                        Piso = row["Piso"] as string ?? string.Empty,
                        ItemName = row["ItemName"] as string ?? string.Empty,
                        Detalle = row["Detalle"] as string ?? string.Empty,
                        Marca = row["Marca"] as string ?? string.Empty,
                        Modelo = row["Modelo"] as string ?? string.Empty,
                        Serie = row["Serie"] as string ?? string.Empty,
                        Material = row["Material"] as string ?? string.Empty,
                        Medida = row["Medida"] as string ?? string.Empty,
                        Color = row["Color"] as string ?? string.Empty,
                        Estado = row["Estado"] as string ?? string.Empty,
                        Condicion_Uso = row["Condicion_Uso"] as string ?? string.Empty,
                        Usuario = row["Usuario"] as string ?? string.Empty,
                        Documento = row["Documento"] as string ?? string.Empty,
                        Cargo = row["Cargo"] as string ?? string.Empty,
                        Gerencia = row["Gerencia"] as string ?? string.Empty,
                        GroupName = row["GroupName"] as string ?? string.Empty,
                        EstadoConta = row["EstadoConta"] as string ?? string.Empty,
                        Periodo = row["Periodo"] as string ?? string.Empty,
                        Tipo_Inventario = row["Tipo_Inventario"] as string ?? string.Empty,
                        Tipo_Asignacion = row["Tipo_Asignacion"] as string ?? string.Empty,
                        Sucursal = row["Sucursal"] != DBNull.Value ? Convert.ToInt32(row["Sucursal"]) : 0,
                        Image1 = row["Image1"] as string ?? string.Empty,
                        Image2 = row["Image2"] as string ?? string.Empty,
                        Image3 = row["Image3"] as string ?? string.Empty,
                        Image4 = row["Image4"] as string ?? string.Empty,
                        US_Registro = row["US_Registro"] as string ?? string.Empty,
                        FE_CREA = row["FE_CREA"] != DBNull.Value ? (DateTime?)row["FE_CREA"] : null,
                    };
                    lista.lista.Add(articulo);
                }
                return lista;
            }
            catch (Exception ex)
            {
                lista.mensaje.CodigoMensaje = 1;
                lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarArticulo], VERIFICAR CONSOLA";
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

        public async Task<ListaArticulo> ListarArticuloPorAutoCompleteAsync(int Gestor, string NombreCompleto)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnInventarioSql, "Inventario.paListarArticuloPorAutoComplete", NombreCompleto);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarArticulo], VERIFICAR CONSOLA";
                        lista.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();
                        return lista;
                    }
                    dt = ds.Tables[0].Copy();
                }
                foreach (DataRow row in dt.Rows)
                {
                    articulo = new Articulo
                    {
                        Id = (int)row["Id"],
                        Codigo_Barra = row["Codigo_Barra"] as string ?? string.Empty,
                        ItemCode = row["ItemCode"] as string ?? string.Empty,
                        ItemName = row["ItemName"] as string ?? string.Empty,
                    };
                    lista.lista.Add(articulo);
                }
                return lista;
            }
            catch (Exception ex)
            {
                lista.mensaje.CodigoMensaje = 1;
                lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarArticulo], VERIFICAR CONSOLA";
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

        public async Task<string> ReservarCorrelativoAsync()
        {
            string correlativo = string.Empty;
            string cadenadeconexion = Utilitarios.DatosGlobales.ObtenerCadenaDeConexion(DatosGlobales.ListaConexiones.cnInventarioSql);
            using (var connection = new SqlConnection(cadenadeconexion))
            {
                await connection.OpenAsync();
                using (SqlTransaction transaction = (SqlTransaction)await connection.BeginTransactionAsync())
                {
                    try
                    {
                        var command = connection.CreateCommand();
                        command.Transaction = transaction;
                        command.CommandText = @"
                        SELECT TOP 1 Correlativo 
                        FROM [Inventario].[Correlativos] 
                        WITH (UPDLOCK, ROWLOCK)
                        WHERE EnUso = 0 
                        ORDER BY Correlativo ASC";

                        int correlativoActual = 0;
                        var reader = await command.ExecuteReaderAsync();
                        if (await reader.ReadAsync())
                        {
                            correlativoActual = reader.GetInt32(0);
                            correlativo = correlativoActual.ToString("D6");
                        }
                        await reader.CloseAsync();

                        if (correlativoActual > 0)
                        {
                            command.CommandText = @"
                            UPDATE [Inventario].[Correlativos] 
                            SET EnUso = 1 
                            WHERE Correlativo = @CorrelativoActual";
                            command.Parameters.AddWithValue("@CorrelativoActual", correlativoActual);
                            await command.ExecuteNonQueryAsync();
                        }
                        else
                        {
                            command.CommandText = @"
                            SELECT ISNULL(MAX(Correlativo), 0) + 1 
                            FROM [Inventario].[Correlativos]";
                            correlativoActual = Convert.ToInt32(await command.ExecuteScalarAsync());
                            correlativo = correlativoActual.ToString("D6");

                            command.CommandText = @"
                            INSERT INTO [Inventario].[Correlativos] (Correlativo, EnUso) 
                            VALUES (@CorrelativoActual, 1)";
                            command.Parameters.Clear();
                            command.Parameters.AddWithValue("@CorrelativoActual", correlativoActual);
                            await command.ExecuteNonQueryAsync();
                        }

                        await transaction.CommitAsync();
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        throw new Exception($"Error al reservar correlativo: {ex.Message}");
                    }
                }
            }
            return $"CMP - {correlativo}";
        }

        public async Task<Mensaje> LiberarCorrelativoAsync(int Id)
        {

            Mensaje mensaje = new Mensaje();
            string cadenadeconexion = Utilitarios.DatosGlobales.ObtenerCadenaDeConexion(DatosGlobales.ListaConexiones.cnInventarioSql);
            using (var connection = new SqlConnection(cadenadeconexion))
            {
                await connection.OpenAsync();
                using (var command = connection.CreateCommand())
                {
                    try
                    {
                        command.CommandText = @"
                        UPDATE [Inventario].[Correlativos]
                        SET EnUso = 0 
                        WHERE Correlativo = @CorrelativoActual";
                        command.Parameters.AddWithValue("@CorrelativoActual", Id);

                        await command.ExecuteNonQueryAsync();
                        mensaje.DescripcionMensaje = "CORRELATIVO LIBERADO CON ÉXITO";
                        mensaje.CodigoMensaje = Convert.ToInt32(0);
                        return mensaje;
                    }
                    catch (Exception ex)
                    {
                        mensaje.CodigoMensaje = 1;
                        mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA EN LA CAPA DE DATOS [LiberarCorrelativo] VERIFICAR CONSOLA";
                        mensaje.DescripcionMensajeSistema = ex.Message.ToString();
                        return mensaje;
                    }
                }
            }

        }

        public async Task<List<string>> BuscarCampoAsync(string Valor, string Campo)
        {
            List<string> resultados = new List<string>();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnInventarioSql, "Inventario.paBuscarArticuloDinamico", Campo, Valor);
                if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                {
                    throw new Exception(ds.ExtendedProperties["DescripcionError"].ToString());
                }

                dt = ds.Tables[0].Copy();

                foreach (DataRow row in dt.Rows)
                {
                    resultados.Add(row[0] as string);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en la capa lógica [BuscarPorCampoDinamico]: " + ex.Message);
            }
            finally
            {
                ds.Dispose();
                ds.Clear();
                dt.Dispose();
                dt.Clear();
            }

            return resultados;
        }

        public async Task<DataSet> DescargarReporteExcel(int Gestor, int IdUsuarioAuditoria)
        {
            DataSet ds = new DataSet();
            DataSet listads = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    listads = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnInventarioSql, "Inventario.paDescargarReporte", IdUsuarioAuditoria);
                    if (Convert.ToInt32(listads.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        return listads;
                    }
                    if (ds.Tables.Count > 0)
                    {
                        dt = listads.Tables[0].Copy();

                    }
                }
                return listads;
            }
            finally
            {
                ds.Dispose();
                ds.Clear();
            }
        }

        public async Task<List<Sucursal>> ObtenerSucursalesAsync(int Gestor)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            List<Sucursal> lista = new List<Sucursal>();
            try
            {

                if (Gestor == DatosGlobales.GestorSqlServer) { ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paListarSucursal"); }
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

        public async Task<DataSet> DescargarReporteCodigosExcel(int Gestor, int IdUsuarioAuditoria)
        {
            DataSet ds = new DataSet();
            DataSet listads = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    listads = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnInventarioSql, "Inventario.paDescargarReporteSecuenciaCodigosBarra", IdUsuarioAuditoria);
                    if (Convert.ToInt32(listads.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        return listads;
                    }
                    if (ds.Tables.Count > 0)
                    {
                        dt = listads.Tables[0].Copy();

                    }
                }
                return listads;
            }
            finally
            {
                ds.Dispose();
                ds.Clear();
            }
        }

        public async Task<DataSet> DescargarReporteCodigosQRExcel(int Gestor, int IdUsuarioAuditoria)
        {
            DataSet ds = new DataSet();
            DataSet listads = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    listads = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnInventarioSql, "Inventario.paDescargarReporteSecuenciaCodigosQR", IdUsuarioAuditoria);
                    if (Convert.ToInt32(listads.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        return listads;
                    }
                    if (ds.Tables.Count > 0)
                    {
                        dt = listads.Tables[0].Copy();

                    }
                }
                return listads;
            }
            finally
            {
                ds.Dispose();
                ds.Clear();
            }
        }

        public async Task<DataSet> DescargarReporteBienesExcel(int Gestor, int IdUsuarioAuditoria)
        {
            DataSet ds = new DataSet();
            DataSet listads = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    listads = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnInventarioSql, "Inventario.paDescargarReporteInicial", IdUsuarioAuditoria);
                    if (Convert.ToInt32(listads.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        return listads;
                    }
                    if (ds.Tables.Count > 0)
                    {
                        dt = listads.Tables[0].Copy();

                    }
                }
                return listads;
            }
            finally
            {
                ds.Dispose();
                ds.Clear();
            }
        }

    }

}



