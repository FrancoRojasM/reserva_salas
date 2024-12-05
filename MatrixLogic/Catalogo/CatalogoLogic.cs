using System.Data;
using Utilitarios;
using System.Threading.Tasks;
using System;
using System.Net.Http.Headers;
using System.Net.Http;

namespace UtilitariosLogic
{
    public class CatalogoLogic
    {
        private Catalogo catalogo;
        private ListaCatalogo lista;
        public CatalogoLogic()
        {
            catalogo = new Catalogo();
            lista = new ListaCatalogo();
        }
        public async Task<Mensaje> EliminarCatalogoAsync(int Gestor, int IdCatalogo, int IdUsuarioAuditoria, string Esquema)
        {
            Mensaje mensaje = new Mensaje();
            if (Gestor == DatosGlobales.GestorSqlServer)
            {
                System.Object[] ParamtrosOutPut = null;
                ParamtrosOutPut = await ConexionSqlServer.GDatos.EjecutarAsync(DatosGlobales.ListaConexiones.cnGeneralSql, Esquema + ".paEliminarCatalogo", IdCatalogo, IdUsuarioAuditoria, mensaje.DescripcionMensaje, mensaje.CodigoMensaje);
                if (Convert.ToInt32(ParamtrosOutPut[100].ToString()) > 0)
                {
                    mensaje.CodigoMensaje = 1;
                    mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA EN LA CAPA DE DATOS [EliminarCatalogo] VERIFICAR CONSOLA";
                    mensaje.DescripcionMensajeSistema = ParamtrosOutPut[99].ToString();
                    return mensaje;
                }
                mensaje.DescripcionMensaje = ParamtrosOutPut[0].ToString();
                mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[1].ToString());
            }
            return mensaje;
        }
        public async Task<Catalogo> ObtenerCatalogoAsync(int Gestor, int IdCatalogo, int IdUsuarioAuditoria, string Esquema)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnGeneralSql, Esquema + ".paObtenerCatalogo", IdCatalogo, IdUsuarioAuditoria);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        catalogo.mensaje.CodigoMensaje = 1;
                        catalogo.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ObtenerCatalogo], VERIFICAR CONSOLA";
                        catalogo.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();

                        return catalogo;
                    }
                    dt = ds.Tables[0].Copy();
                    catalogo.IdCatalogo = (int)dt.Rows[0]["IdCatalogo"];
                    catalogo.Grupo = (int)dt.Rows[0]["IdGrupo"];
                    catalogo.IdCatalogoPadre = (int)dt.Rows[0]["IdCatalogoPadre"];
                    catalogo.OrdenItem = (int)dt.Rows[0]["OrdenItem"];
                    catalogo.Descripcion = (string)dt.Rows[0]["Descripcion"];
                    catalogo.Detalle = dt.Rows[0]["Detalle"] is DBNull?null: (string)dt.Rows[0]["Detalle"];
                    catalogo.Activo = (int)dt.Rows[0]["Activo"];

                }
                return catalogo;
            }
            catch (Exception ex)
            {
                catalogo.mensaje.CodigoMensaje = 1;
                catalogo.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ObtenerCatalogo], VERIFICAR CONSOLA";
                catalogo.mensaje.DescripcionMensajeSistema = ex.Message;
                return catalogo;
            }
            finally
            {
                ds.Dispose();
                ds.Clear();
                dt.Dispose();
                dt.Clear();
            }
        }
        public async Task<Catalogo> GuardaCatalogoAsync(int Gestor, int IdCatalogo, int IdGrupo, int IdCatalogoPadre, int OrdenItem, string Descripcion, string Detalle, bool Activo, int IdUsuarioAuditoria, string Esquema)
        {
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    System.Object[] ParamtrosOutPut = null;
                    ParamtrosOutPut = await ConexionSqlServer.GDatos.EjecutarAsync(DatosGlobales.ListaConexiones.cnGeneralSql, Esquema + ".paGuardarCatalogo", IdCatalogo, IdGrupo, IdCatalogoPadre, OrdenItem, Descripcion, Detalle, Activo, IdUsuarioAuditoria, "", 0);
                    if (Convert.ToInt32(ParamtrosOutPut[100].ToString()) > 0)
                    {
                        catalogo.mensaje.CodigoMensaje = 1;
                        catalogo.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [GuardaCatalogo], VERIFICAR CONSOLA";
                        catalogo.mensaje.DescripcionMensajeSistema = ParamtrosOutPut[99].ToString();
                        return catalogo;
                    }
                    catalogo.IdCatalogo = Convert.ToInt32(ParamtrosOutPut[0].ToString());
                    catalogo.mensaje.DescripcionMensaje = ParamtrosOutPut[1].ToString();
                    catalogo.mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[2].ToString());
                }
                return catalogo;
            }
            catch (Exception ex)
            {
                catalogo.mensaje.CodigoMensaje = 1;
                catalogo.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [GuardaCatalogo], VERIFICAR CONSOLA";
                catalogo.mensaje.DescripcionMensajeSistema = ex.Message;
                return catalogo;
            }
        }
        public async Task<ListaCatalogo> ListarCatalogoAsync(int Gestor, int IdUsuarioAuditoria, int IdCatalogoPadre, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral, string Esquema)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnGeneralSql, Esquema + ".paListarCatalogo", IdUsuarioAuditoria, IdCatalogoPadre, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarCatalogo], VERIFICAR CONSOLA";
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
                    catalogo = new Catalogo();
                    catalogo.IdCatalogo = (int)row["IdCatalogo"];
                    catalogo.OrdenItem = (int)row["OrdenItem"];
                    catalogo.Descripcion = (string)row["Descripcion"];
                    catalogo.Detalle = row["Detalle"] is System.DBNull ? null : (string)row["Detalle"];
                    catalogo.Activo = (int)row["Activo"];

                    lista.lista.Add(catalogo);
                }
                return lista;
            }
            catch (Exception ex)
            {
                lista.mensaje.CodigoMensaje = 1;
                lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarCatalogo], VERIFICAR CONSOLA";
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
        public async Task<ListaCatalogo> ListarCatalogoPadreAsync(int Gestor, int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral, string Esquema)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnGeneralSql, Esquema + ".paListarCatalogoPadre", IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarCatalogo], VERIFICAR CONSOLA";
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
                    catalogo = new Catalogo();
                    catalogo.IdCatalogo = (int)row["IdCatalogo"];
                    catalogo.OrdenItem = (int)row["OrdenItem"];
                    catalogo.Descripcion = (string)row["Descripcion"];
                    catalogo.Detalle = row["Detalle"] is System.DBNull?null:(string)row["Detalle"];
                    catalogo.Activo = (int)row["Activo"];

                    lista.lista.Add(catalogo);
                }
                return lista;
            }
            catch (Exception ex)
            {
                lista.mensaje.CodigoMensaje = 1;
                lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarCatalogo], VERIFICAR CONSOLA";
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
        public ListaCatalogo ListarCatalogoCombo(int Gestor, int IdCatalogoPadre, string Esquema)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                     ds = ConexionSqlServer.GDatos.TraerDataSet(DatosGlobales.ListaConexiones.cnGeneralSql, Esquema+ ".paListarCatalogoCombo", IdCatalogoPadre);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarComboArea], VERIFICAR CONSOLA";
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
                        catalogo = new Catalogo();
                        catalogo.IdCatalogo = Convert.ToInt32(row["IdCatalogo"].ToString());
                        catalogo.Descripcion = row["Descripcion"].ToString();
                        lista.lista.Add(catalogo);
                    }

                }
                return lista;
            }
            catch (Exception ex)
            {
                lista.mensaje.CodigoMensaje = 1;
                lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarComboArea], VERIFICAR CONSOLA";
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
        public async Task<ListaCatalogo> ListarCatalogoComboAsync(int Gestor, int IdCatalogoPadre, string Esquema)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            ListaCatalogo lista = new ListaCatalogo();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                     ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnGeneralSql, Esquema + ".paListarCatalogoCombo", IdCatalogoPadre);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarComboArea], VERIFICAR CONSOLA";
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
                        catalogo = new Catalogo();
                        catalogo.IdCatalogo = Convert.ToInt32(row["IdCatalogo"].ToString());
                        catalogo.Descripcion = row["Descripcion"].ToString();
                        lista.lista.Add(catalogo);
                    }

                }
                return await Task.FromResult(lista);
            }
            catch (Exception ex)
            {
                lista.mensaje.CodigoMensaje = 1;
                lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarComboArea], VERIFICAR CONSOLA";
                lista.mensaje.DescripcionMensajeSistema = ex.Message;
                return await Task.FromResult(lista);
            }
            finally
            {
                ds.Dispose();
                ds.Clear();
                dt.Dispose();
                dt.Clear();
            }
        }
        public async Task<ListaCatalogo> ListarCatalogoTipoDocumentoBusquedaExpediente(int Gestor)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    ds =await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnTramiteSql, "Tramite.paListarCatalogoTipoDocumentoBusquedaExpediente");
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["NumeroError"].ToString();
                        lista.mensaje.DescripcionMensaje = "SUCEDIO UN ERROR EN LA CAPA DE DATOS [ListarCatalogoTipoDocumentoBusquedaExpediente], VERIFIQUE CONSOLA";
                        return lista;
                    }
                    if (ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0].Copy();
                    }
                }
                if (dt.Rows.Count > 0)
                {
                    Catalogo catalogo = null;
                    foreach (DataRow row in dt.Rows)
                    {
                        catalogo = new Catalogo();
                        catalogo.IdCatalogo = (int)row["IdCatalogoTipoDocumento"];
                        catalogo.Descripcion = (string)row["CatalogoTipoDocumento"];
                        lista.lista.Add(catalogo);
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
        public async Task<ListaCatalogo> ListarAreaOrigenBusquedaExpediente(int Gestor)
        {
            ListaCatalogo lista = new ListaCatalogo();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnTramiteSql, "Tramite.paListarAreaOrigenBusquedaExpediente");
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["NumeroError"].ToString();
                        lista.mensaje.DescripcionMensaje = "SUCEDIO UN ERROR EN LA CAPA DE DATOS [ListarAreaOrigenBusquedaExpediente], VERIFIQUE CONSOLA";
                        return lista;
                    }
                    if (ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0].Copy();
                    }
                }
                if (dt.Rows.Count > 0)
                {
                    Catalogo catalogo = null;
                    foreach (DataRow row in dt.Rows)
                    {
                        catalogo = new Catalogo();
                        catalogo.IdCatalogo = (int)row["IdArea"];
                        catalogo.Descripcion = (string)row["NombreArea"];
                        lista.lista.Add(catalogo);
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
        
        public async Task<ListaCatalogo> ListarTipoSituacionMovimientoTramite(int Gestor)
        {
            ListaCatalogo lista = new ListaCatalogo();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnTramiteSql, "Tramite.paListarTipoSituacionMovimientoTramite");
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["NumeroError"].ToString();
                        lista.mensaje.DescripcionMensaje = "SUCEDIO UN ERROR EN LA CAPA DE DATOS [ListarTipoMovimientoTramite], VERIFIQUE CONSOLA";
                        return lista;
                    }
                    if (ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0].Copy();
                    }
                }
                if (dt.Rows.Count > 0)
                {
                    Catalogo catalogo = null;
                    foreach (DataRow row in dt.Rows)
                    {
                        catalogo = new Catalogo();
                        catalogo.IdCatalogo = (int)row["IdCatalogo"];
                        catalogo.Descripcion = (string)row["Descripcion"];
                        lista.lista.Add(catalogo);
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
        public async Task<ListaCatalogo> ListarAreaDestinoBusquedaExpediente(int Gestor)
        {
            ListaCatalogo lista = new ListaCatalogo();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnTramiteSql, "Tramite.paListarAreaDestinoBusquedaExpediente");
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["NumeroError"].ToString();
                        lista.mensaje.DescripcionMensaje = "SUCEDIO UN ERROR EN LA CAPA DE DATOS [ListarAreaDestinoBusquedaExpediente], VERIFIQUE CONSOLA";
                        return lista;
                    }
                    if (ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0].Copy();
                    }
                }
                if (dt.Rows.Count > 0)
                {
                    Catalogo catalogo = null;
                    foreach (DataRow row in dt.Rows)
                    {
                        catalogo = new Catalogo();
                        catalogo.IdCatalogo = (int)row["IdArea"];
                        catalogo.Descripcion = (string)row["NombreArea"];
                        lista.lista.Add(catalogo);
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
        public async Task<ListaCatalogo> ListarCargoOrigenBusquedaExpediente(int Gestor)
        {
            ListaCatalogo lista = new ListaCatalogo();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnTramiteSql, "Tramite.paListarCargoOrigenBusquedaExpediente");
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["NumeroError"].ToString();
                        lista.mensaje.DescripcionMensaje = "SUCEDIO UN ERROR EN LA CAPA DE DATOS [ListarCargoOrigenBusquedaExpediente], VERIFIQUE CONSOLA";
                        return lista;
                    }
                    if (ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0].Copy();
                    }
                }
                if (dt.Rows.Count > 0)
                {
                    Catalogo catalogo = null;
                    foreach (DataRow row in dt.Rows)
                    {
                        catalogo = new Catalogo();
                        catalogo.IdCatalogo = (int)row["IdCargo"];
                        catalogo.Descripcion = (string)row["NombreCargo"];
                        lista.lista.Add(catalogo);
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
        public async Task<ListaCatalogo> ListarCargoDestinoBusquedaExpediente(int Gestor)
        {
            ListaCatalogo lista = new ListaCatalogo();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnTramiteSql, "Tramite.paListarCargoDestinoBusquedaExpediente");
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["NumeroError"].ToString();
                        lista.mensaje.DescripcionMensaje = "SUCEDIO UN ERROR EN LA CAPA DE DATOS [ListarCargoDestinoBusquedaExpediente], VERIFIQUE CONSOLA";
                        return lista;
                    }
                    if (ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0].Copy();
                    }
                }
                if (dt.Rows.Count > 0)
                {
                    Catalogo catalogo = null;
                    foreach (DataRow row in dt.Rows)
                    {
                        catalogo = new Catalogo();
                        catalogo.IdCatalogo = (int)row["IdCargo"];
                        catalogo.Descripcion = (string)row["NombreCargo"];
                        lista.lista.Add(catalogo);
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
        public async Task<ListaCatalogo> ListarPeriodoBusquedaExpediente(int Gestor)
        {
            ListaCatalogo lista = new ListaCatalogo();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnTramiteSql, "Tramite.paListarPeriodoBusquedaExpediente");
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["NumeroError"].ToString();
                        lista.mensaje.DescripcionMensaje = "SUCEDIO UN ERROR EN LA CAPA DE DATOS [ListarPeriodoBusquedaExpediente], VERIFIQUE CONSOLA";
                        return lista;
                    }
                    if (ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0].Copy();
                    }
                }
                if (dt.Rows.Count > 0)
                {
                    Catalogo catalogo = null;
                    foreach (DataRow row in dt.Rows)
                    {
                        catalogo = new Catalogo();
                        catalogo.IdCatalogo = (int)row["IdPeriodo"];
                        catalogo.Descripcion = (string)row["NombrePeriodo"];
                        lista.lista.Add(catalogo);
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
        public async Task<ListaCatalogo> ListarPeriodoBusquedaDocumentoEspecialista(int Gestor, int IdPersona)
        {
            ListaCatalogo lista = new ListaCatalogo();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnTramiteSql, "Tramite.paListarPeriodoBusquedaDocumentoEspecialista", IdPersona);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["NumeroError"].ToString();
                        lista.mensaje.DescripcionMensaje = "SUCEDIO UN ERROR EN LA CAPA DE DATOS [ListarPeriodoBusquedaDocumentoEspecialista], VERIFIQUE CONSOLA";
                        return lista;
                    }
                    if (ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0].Copy();
                    }
                }
                if (dt.Rows.Count > 0)
                {
                    Catalogo catalogo = null;
                    foreach (DataRow row in dt.Rows)
                    {
                        catalogo = new Catalogo();
                        catalogo.IdCatalogo = (int)row["IdPeriodo"];
                        catalogo.Descripcion = (string)row["NombrePeriodo"];
                        lista.lista.Add(catalogo);
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
        public async Task<ListaCatalogo> ListarCatalogoTipoDocumentoBusquedaDocumentoJefatura(int Gestor, int IdArea)
        {
            ListaCatalogo lista = new ListaCatalogo();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnTramiteSql, "Tramite.paListarCatalogoTipoDocumentoBusquedaDocumentoEspecialista", IdArea);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["NumeroError"].ToString();
                        lista.mensaje.DescripcionMensaje = "SUCEDIO UN ERROR EN LA CAPA DE DATOS [ListarCatalogoTipoDocumentoBusquedaDocumentoJefatura], VERIFIQUE CONSOLA";
                        return lista;
                    }
                    if (ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0].Copy();
                    }
                }
                if (dt.Rows.Count > 0)
                {
                    Catalogo catalogo = null;
                    foreach (DataRow row in dt.Rows)
                    {
                        catalogo = new Catalogo();
                        catalogo.IdCatalogo = (int)row["IdCatalogo"];
                        catalogo.Descripcion = (string)row["Descripcion"];
                        lista.lista.Add(catalogo);
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
        public async Task<ListaCatalogo> ListarCatalogoTipoDocumentoJefatura(int Gestor, int IdArea)
        {
            ListaCatalogo lista = new ListaCatalogo();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnTramiteSql, "Tramite.paListarCatalogoTipoDocumentoJefatura", IdArea);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["NumeroError"].ToString();
                        lista.mensaje.DescripcionMensaje = "SUCEDIO UN ERROR EN LA CAPA DE DATOS [ListarCatalogoTipoDocumentoJefatura], VERIFIQUE CONSOLA";
                        return lista;
                    }
                    if (ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0].Copy();
                    }
                }
                if (dt.Rows.Count > 0)
                {
                    Catalogo catalogo = null;
                    foreach (DataRow row in dt.Rows)
                    {
                        catalogo = new Catalogo();
                        catalogo.IdCatalogo = (int)row["IdCatalogo"];
                        catalogo.Descripcion = (string)row["Descripcion"];
                        lista.lista.Add(catalogo);
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
        public async Task<ListaCatalogo> ListarPeriodoBusquedaDocumentoJefatura(int Gestor, int IdArea)
        {
            ListaCatalogo lista = new ListaCatalogo();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnTramiteSql, "Tramite.paListarPeriodoBusquedaDocumentoJefatura", IdArea);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["NumeroError"].ToString();
                        lista.mensaje.DescripcionMensaje = "SUCEDIO UN ERROR EN LA CAPA DE DATOS [ListarPeriodoBusquedaDocumentoJefatura], VERIFIQUE CONSOLA";
                        return lista;
                    }
                    if (ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0].Copy();
                    }
                }
                if (dt.Rows.Count > 0)
                {
                    Catalogo catalogo = null;
                    foreach (DataRow row in dt.Rows)
                    {
                        catalogo = new Catalogo();
                        catalogo.IdCatalogo = (int)row["IdPeriodo"];
                        catalogo.Descripcion = (string)row["NombrePeriodo"];
                        lista.lista.Add(catalogo);
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
        public async Task<ListaCatalogo> ListarCatalogoTipoDocumentoPorAreaPersona(int Gestor, int IdArea, int IdPersona)
        {
            ListaCatalogo lista = new ListaCatalogo();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnTramiteSql, "Tramite.paListarCatalogoTipoDocumentoBusquedaDocumentoJefatura", IdArea, IdPersona);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["NumeroError"].ToString();
                        lista.mensaje.DescripcionMensaje = "SUCEDIO UN ERROR EN LA CAPA DE DATOS [ListarCatalogoTipoDocumentoPorAreaPersona], VERIFIQUE CONSOLA";
                        return lista;
                    }
                    if (ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0].Copy();
                    }
                }
                if (dt.Rows.Count > 0)
                {
                    Catalogo catalogo = null;
                    foreach (DataRow row in dt.Rows)
                    {
                        catalogo = new Catalogo();
                        catalogo.IdCatalogo = (int)row["IdCatalogo"];
                        catalogo.Descripcion = (string)row["Descripcion"];
                        lista.lista.Add(catalogo);
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
        public async Task<ListaCatalogo> ListarCatalogoTipoDocumentoBusquedaDocumentoEspecialista(int Gestor, int IdPersona)
        {
            ListaCatalogo lista = new ListaCatalogo();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnTramiteSql, "Tramite.paListarCatalogoTipoDocumentoBusquedaDocumentoEspecialista", IdPersona);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["NumeroError"].ToString();
                        lista.mensaje.DescripcionMensaje = "SUCEDIO UN ERROR EN LA CAPA DE DATOS [ListarCatalogoTipoDocumentoBusquedaDocumentoEspecialista], VERIFIQUE CONSOLA";
                        return lista;
                    }
                    if (ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0].Copy();
                    }
                }
                if (dt.Rows.Count > 0)
                {
                    Catalogo catalogo = null;
                    foreach (DataRow row in dt.Rows)
                    {
                        catalogo = new Catalogo();
                        catalogo.IdCatalogo = (int)row["IdCatalogo"];
                        catalogo.Descripcion = (string)row["Descripcion"];
                        lista.lista.Add(catalogo);
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
        public async Task<ListaCatalogo> ListarCatalogoCategoriaBusquedaExpediente(int Gestor)
        {
            ListaCatalogo lista = new ListaCatalogo();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnTramiteSql, "Tramite.paListarCatalogoCategoriaBusquedaExpediente");
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["NumeroError"].ToString();
                        lista.mensaje.DescripcionMensaje = "SUCEDIO UN ERROR EN LA CAPA DE DATOS [ListarCatalogoCategoriaBusquedaExpediente], VERIFIQUE CONSOLA";
                        return lista;
                    }
                    if (ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0].Copy();
                    }
                }
                if (dt.Rows.Count > 0)
                {
                    Catalogo catalogo = null;
                    foreach (DataRow row in dt.Rows)
                    {
                        catalogo = new Catalogo();
                        catalogo.IdCatalogo = (int)row["IdCatalogoTipoTramite"];
                        catalogo.Descripcion = (string)row["CatalogoTipoTramite"];
                        lista.lista.Add(catalogo);
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
        public async Task<ListaCatalogo> ListarCatalogoCategoriaTramiteSeguimiento(int Gestor)
        {
            ListaCatalogo lista = new ListaCatalogo();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnTramiteSql, "Tramite.paListarCatalogoCategoriaTramiteSeguimiento");
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["NumeroError"].ToString();
                        lista.mensaje.DescripcionMensaje = "SUCEDIO UN ERROR EN LA CAPA DE DATOS [ListarCatalogoCategoriaTramiteSeguimiento], VERIFIQUE CONSOLA";
                        return lista;
                    }
                    if (ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0].Copy();
                    }
                }
                if (dt.Rows.Count > 0)
                {
                    Catalogo catalogo = null;
                    foreach (DataRow row in dt.Rows)
                    {
                        catalogo = new Catalogo();
                        catalogo.IdCatalogo = (int)row["IdCatalogo"];
                        catalogo.Descripcion = (string)row["Descripcion"];
                        lista.lista.Add(catalogo);
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
        public async Task<ListaCatalogo> ListarCatalogoTipoDocumentoEspecialista(int Gestor,int IdEmpresa, int IdArea, int IdCargo, int IdPersona)
        {
            ListaCatalogo lista = new ListaCatalogo();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnTramiteSql, "Tramite.paListarCatalogoTipoDocumentoEspecialista", IdEmpresa, IdArea, IdCargo,  IdPersona);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["NumeroError"].ToString();
                        lista.mensaje.DescripcionMensaje = "SUCEDIO UN ERROR EN LA CAPA DE DATOS [ListarCatalogoTipoDocumentoEspecialista], VERIFIQUE CONSOLA";
                        return lista;
                    }
                    if (ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0].Copy();
                    }
                }
                if (dt.Rows.Count > 0)
                {
                    Catalogo catalogo = null;
                    foreach (DataRow row in dt.Rows)
                    {
                        catalogo = new Catalogo();
                        catalogo.IdCatalogo = (int)row["IdCatalogo"];
                        catalogo.Descripcion = (string)row["Descripcion"];
                        lista.lista.Add(catalogo);
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
        public async Task<ListaCatalogo> ListarCatalogoTipoDevolucion(int Gestor, int IdExpedienteDocumentoOrigenDestino)
        {
            ListaCatalogo lista = new ListaCatalogo();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnTramiteSql, "Tramite.paListarCatalogoTipoDevolucion", IdExpedienteDocumentoOrigenDestino);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["NumeroError"].ToString();
                        lista.mensaje.DescripcionMensaje = "SUCEDIO UN ERROR EN LA CAPA DE DATOS [ListarCatalogoTipoDevolucion], VERIFIQUE CONSOLA";
                        return lista;
                    }
                    if (ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0].Copy();
                    }
                }
                if (dt.Rows.Count > 0)
                {
                    Catalogo catalogo = null;
                    foreach (DataRow row in dt.Rows)
                    {
                        catalogo = new Catalogo();
                        catalogo.IdCatalogo = (int)row["IdCatalogo"];
                        catalogo.Descripcion = (string)row["Descripcion"];
                        lista.lista.Add(catalogo);
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
        public async Task<ListaCatalogo> ListarComboPersonaPorAreaPadrePendientes(int Gestor, int IdUsuario, int IdArea)
        {
            ListaCatalogo lista = new ListaCatalogo();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnTramiteSql, "Tramite.paListarComboPersonaPorAreaPadrePendientes", IdUsuario, IdArea);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["NumeroError"].ToString();
                        lista.mensaje.DescripcionMensaje = "SUCEDIO UN ERROR EN LA CAPA DE DATOS [ListarComboPersonaPorAreaPadrePendientes], VERIFIQUE CONSOLA";
                        return lista;
                    }
                    if (ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0].Copy();
                    }
                }
                if (dt.Rows.Count > 0)
                {
                    Catalogo catalogo = null;
                    foreach (DataRow row in dt.Rows)
                    {
                        catalogo = new Catalogo();
                        catalogo.IdCatalogo = (int)row["IdPersona"];
                        catalogo.Descripcion = (string)row["NombreCompleto"];
                        lista.lista.Add(catalogo);
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
        public async Task<ListaCatalogo> ListarComboPersonaBusquedaDocumentoJefatura(int Gestor, int IdUsuario, int IdArea)
        {
            ListaCatalogo lista = new ListaCatalogo();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnTramiteSql, "Tramite.paListarComboPersonaBusquedaDocumentoJefatura", IdUsuario, IdArea);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["NumeroError"].ToString();
                        lista.mensaje.DescripcionMensaje = "SUCEDIO UN ERROR EN LA CAPA DE DATOS [ListarComboPersonaBusquedaDocumentoJefatura], VERIFIQUE CONSOLA";
                        return lista;
                    }
                    if (ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0].Copy();
                    }
                }
                if (dt.Rows.Count > 0)
                {
                    Catalogo catalogo = null;
                    foreach (DataRow row in dt.Rows)
                    {
                        catalogo = new Catalogo();
                        catalogo.IdCatalogo = (int)row["IdPersona"];
                        catalogo.Descripcion = (string)row["NombreCompleto"];
                        lista.lista.Add(catalogo);
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
        public async Task<ListaCatalogo> ListarComboDocumentosVinculados(int Gestor, int IdUsuario, int IdExpediente)
        {
            ListaCatalogo lista = new ListaCatalogo();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnTramiteSql, "Tramite.paListarComboDocumentosVinculados", IdUsuario, IdExpediente);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["NumeroError"].ToString();
                        lista.mensaje.DescripcionMensaje = "SUCEDIO UN ERROR EN LA CAPA DE DATOS [ListarComboDocumentosVinculados], VERIFIQUE CONSOLA";
                        return lista;
                    }
                    if (ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0].Copy();
                    }
                }
                if (dt.Rows.Count > 0)
                {
                    Catalogo catalogo = null;
                    foreach (DataRow row in dt.Rows)
                    {
                        catalogo = new Catalogo();
                        catalogo.IdCatalogo = (int)row["CorrelativoVinculado"];
                        catalogo.Descripcion = (string)row["NombreCorrelativoVinculado"];
                        lista.lista.Add(catalogo);
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
        public async Task<ListaCatalogo> ListarComboAreaPorAreaPadrePendientes(int Gestor, int IdUsuario, int IdArea)
        {
            ListaCatalogo lista = new ListaCatalogo();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnTramiteSql, "Tramite.paListarComboAreaPorAreaPadrePendientes", IdUsuario, IdArea);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["NumeroError"].ToString();
                        lista.mensaje.DescripcionMensaje = "SUCEDIO UN ERROR EN LA CAPA DE DATOS [ListarComboAreaPorAreaPadrePendientes], VERIFIQUE CONSOLA";
                        return lista;
                    }
                    if (ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0].Copy();
                    }
                }
                if (dt.Rows.Count > 0)
                {
                    Catalogo catalogo = null;
                    foreach (DataRow row in dt.Rows)
                    {
                        catalogo = new Catalogo();
                        catalogo.IdCatalogo = (int)row["IdArea"];
                        catalogo.Descripcion = (string)row["NombreArea"];
                        lista.lista.Add(catalogo);
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
        public async Task<ListaCatalogo> ListarCatalogoEspecialidadAsync(int Gestor, int IdCatalogoPadre, string Esquema)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnGeneralSql, Esquema + ".paListarCatalogoCombo", IdCatalogoPadre);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarComboArea], VERIFICAR CONSOLA";
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
                        catalogo = new Catalogo();
                        catalogo.IdCatalogo = Convert.ToInt32(row["IdCatalogo"].ToString());
                        catalogo.Descripcion = row["Descripcion"].ToString();
                        lista.lista.Add(catalogo);
                    }

                }
                return await Task.FromResult(lista);
            }
            catch (Exception ex)
            {
                lista.mensaje.CodigoMensaje = 1;
                lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarComboArea], VERIFICAR CONSOLA";
                lista.mensaje.DescripcionMensajeSistema = ex.Message;
                return await Task.FromResult(lista);
            }
            finally
            {
                ds.Dispose();
                ds.Clear();
                dt.Dispose();
                dt.Clear();
            }
        }
        public async Task<ListaCatalogo> ListarCatalogoTipoDocumentoAsync(int Gestor, int IdCatalogoPadre, string Esquema)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnGeneralSql, Esquema + ".paListarCatalogoComboTipoDocumento", IdCatalogoPadre);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarComboArea], VERIFICAR CONSOLA";
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
                        catalogo = new Catalogo();
                        catalogo.IdCatalogo = Convert.ToInt32(row["IdCatalogo"].ToString());
                        catalogo.Descripcion = row["Descripcion"].ToString();
                        lista.lista.Add(catalogo);
                    }

                }
                return await Task.FromResult(lista);
            }
            catch (Exception ex)
            {
                lista.mensaje.CodigoMensaje = 1;
                lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarComboArea], VERIFICAR CONSOLA";
                lista.mensaje.DescripcionMensajeSistema = ex.Message;
                return await Task.FromResult(lista);
            }
            finally
            {
                ds.Dispose();
                ds.Clear();
                dt.Dispose();
                dt.Clear();
            }
        }
        public ListaCatalogo ListaEntidadPideAsync(int Gestor, int IdCatalogoPadre, string Codigo)
        {
            ListaCatalogo lista = new ListaCatalogo();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {

                //MatrixLogic.ServicePcmIMgdEntidad.getListaEntidadRequest val = new MatrixLogic.ServicePcmIMgdEntidad.getListaEntidadRequest();
                int sidcatent = Int32.Parse(Codigo);

                string Resultado = "";
                string uri = "https://ws3.pide.gob.pe/Rest/Pcm/ListaEntidad?sidcatent=1&out=json";// + sidcatent;
                var httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(uri);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
                var response = httpClient.GetAsync(uri).Result;
                response.EnsureSuccessStatusCode();
                var content = response.Content.ReadAsStringAsync().Result;

                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    if (ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0].Copy();
                    }
                }
                if (dt.Rows.Count > 0)
                {
                    Catalogo catalogo = null;
                    foreach (DataRow row in dt.Rows)
                    {
                        catalogo = new Catalogo();
                        catalogo.IdCatalogo = Convert.ToInt32(row["IdCatalogo"].ToString());
                        catalogo.Descripcion = row["Descripcion"].ToString();
                        catalogo.CODCAT = row["CODCAT"].ToString();
                        lista.lista.Add(catalogo);
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
        public async Task<ListaCatalogo> ListarCatalogoTipoEntidadAsync(int Gestor, int IdCatalogoPadre, string Esquema)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnInteroperabilidadSql, Esquema + ".paListarCatalogo", IdCatalogoPadre);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarComboArea], VERIFICAR CONSOLA";
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
                        catalogo = new Catalogo();
                        catalogo.IdCatalogo = Convert.ToInt32(row["IdCatalogo"].ToString());
                        catalogo.Descripcion = row["Descripcion"].ToString();
                        lista.lista.Add(catalogo);
                    }

                }
                return await Task.FromResult(lista);
            }
            catch (Exception ex)
            {
                lista.mensaje.CodigoMensaje = 1;
                lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarCombo], VERIFICAR CONSOLA";
                lista.mensaje.DescripcionMensajeSistema = ex.Message;
                return await Task.FromResult(lista);
            }
            finally
            {
                ds.Dispose();
                ds.Clear();
                dt.Dispose();
                dt.Clear();
            }
        }
        public async Task<ListaCatalogo> ListarCatalogoTipoEntidadPideAsync(int Gestor, int IdCatalogoPadre, string Esquema)
        {
            ListaCatalogo lista = new ListaCatalogo();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnTramiteSql, "Interoperabilidad.paListarCatalogoTipoEntidadPide", IdCatalogoPadre);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["NumeroError"].ToString();
                        lista.mensaje.DescripcionMensaje = "SUCEDIO UN ERROR EN LA CAPA DE DATOS [paListarCatalogoTipoEntidadPide], VERIFIQUE CONSOLA";
                        return lista;
                    }
                    if (ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0].Copy();
                    }
                }
                if (dt.Rows.Count > 0)
                {
                    Catalogo catalogo = null;
                    foreach (DataRow row in dt.Rows)
                    {
                        catalogo = new Catalogo();
                        catalogo.IdCatalogo = Convert.ToInt32(row["IdCatalogo"].ToString());
                        catalogo.Descripcion = row["Descripcion"].ToString();
                        catalogo.CODCAT = row["CODCAT"].ToString();
                        lista.lista.Add(catalogo);
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

    }
}