using System;
using Utilitarios;
using System.Data;
using General;
using System.Threading.Tasks;

namespace GeneralLogic
{
    public class AreaLogic
    {
        private Area area;
        private ListaArea Lista;

        public AreaLogic()
        {
            area = new Area();
            Lista = new ListaArea();
        }

        public Mensaje EliminarArea(int Gestor, int IdArea, int IdUsuarioAuditoria)
        {
            Mensaje mensaje = new Mensaje();
            System.Object[] ParamtrosOutPut = null;

            if (Gestor == DatosGlobales.GestorSqlServer)
            {
                ParamtrosOutPut = ConexionSqlServer.GDatos.Ejecutar(DatosGlobales.ListaConexiones.cnGeneralSql,
                    "General.paEliminarArea", IdArea, IdUsuarioAuditoria, mensaje.DescripcionMensaje,
                    mensaje.CodigoMensaje);
            }
            
            if (Convert.ToInt32(ParamtrosOutPut[100].ToString()) > 0)
            {
                mensaje.CodigoMensaje = 1;
                mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA EN LA CAPA DE DATOS [EliminarArea] VERIFICAR CONSOLA";
                mensaje.DescripcionMensajeSistema = ParamtrosOutPut[99].ToString();
                return mensaje;
            }
            mensaje.DescripcionMensaje = ParamtrosOutPut[0].ToString();
            mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[1].ToString());

            return mensaje;
        } 

        public async Task<Mensaje> EliminarAreaAsync(int Gestor, int IdArea, int IdUsuarioAuditoria)
        {
            Mensaje mensaje = new Mensaje();
            if (Gestor == DatosGlobales.GestorSqlServer)
            {
                System.Object[] ParamtrosOutPut = null; 
                ParamtrosOutPut = await ConexionSqlServer.GDatos.EjecutarAsync(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paEliminarArea", IdArea, IdUsuarioAuditoria, mensaje.DescripcionMensaje, mensaje.CodigoMensaje);
                if (Convert.ToInt32(ParamtrosOutPut[100].ToString()) > 0)
                {
                    mensaje.CodigoMensaje = 1;
                    mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA EN LA CAPA DE DATOS [EliminarArea] VERIFICAR CONSOLA";
                    mensaje.DescripcionMensajeSistema = ParamtrosOutPut[99].ToString();
                    return mensaje;
                }
                mensaje.DescripcionMensaje = ParamtrosOutPut[0].ToString();
                mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[1].ToString());
            }
            return mensaje;
        }
        public Area ObtenerArea(int Gestor, int IdArea)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (IdArea == 0)
                {
                    return area;
                }

                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                     ds = ConexionSqlServer.GDatos.TraerDataSet(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paObtenerArea", IdArea);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        area.mensaje.CodigoMensaje = 1;
                        area.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ObtenerArea], VERIFICAR CONSOLA";
                        area.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();

                        return area;
                    }
                    dt = ds.Tables[0].Copy();
                    area.IdArea = (int)dt.Rows[0]["IdArea"];
                    area.IdAreaPadre = (int)dt.Rows[0]["IdAreaPadre"];
                    area.NombreArea = (string)dt.Rows[0]["NombreArea"];
                    area.Abreviatura = (string)dt.Rows[0]["Abreviatura"];
                    area.Sigla = (string)dt.Rows[0]["Sigla"];
                    area.Activo = (bool)dt.Rows[0]["Activo"];
                    area.VerRecepcion = (bool)dt.Rows[0]["VerRecepcion"];
                    
                    area.empresa.IdEmpresa = (int)dt.Rows[0]["IdEmpresa"];
                    area.catalogotipoarea.IdCatalogo = (int)dt.Rows[0]["IdCatalogoTipoArea"];

                }
                return area;
            }
            catch (Exception ex)
            {
                area.mensaje.CodigoMensaje = 1;
                area.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ObtenerArea], VERIFICAR CONSOLA";
                area.mensaje.DescripcionMensajeSistema = ex.Message;
                return area;
            }
            finally
            {
                ds.Dispose();
                ds.Clear();
                dt.Dispose();
                dt.Clear();
            }
        }
        public async Task<Area> ObtenerAreaAsync(int Gestor, int IdArea)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (IdArea == 0)
                {
                    return area;
                }

                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                     ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paObtenerArea", IdArea);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        area.mensaje.CodigoMensaje = 1;
                        area.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ObtenerArea], VERIFICAR CONSOLA";
                        area.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();

                        return area;
                    }
                    dt = ds.Tables[0].Copy();
                    area.IdArea = (int)dt.Rows[0]["IdArea"];
                    area.IdAreaPadre = (int)dt.Rows[0]["IdAreaPadre"];
                    area.NombreArea = (string)dt.Rows[0]["NombreArea"];
                    area.Abreviatura = (string)dt.Rows[0]["Abreviatura"];
                    area.Sigla = (string)dt.Rows[0]["Sigla"];
                    area.Activo = (bool)dt.Rows[0]["Activo"];
                   area.VerRecepcion = (bool)dt.Rows[0]["VerRecepcion"];
                    area.empresa.IdEmpresa = (int)dt.Rows[0]["IdEmpresa"];
                    area.catalogotipoarea.IdCatalogo = (int)dt.Rows[0]["IdCatalogoTipoArea"];

                }
                return area;
            }
            catch (Exception ex)
            {
                area.mensaje.CodigoMensaje = 1;
                area.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ObtenerArea], VERIFICAR CONSOLA";
                area.mensaje.DescripcionMensajeSistema = ex.Message;
                return area;
            }
            finally
            {
                ds.Dispose();
                ds.Clear();
                dt.Dispose();
                dt.Clear();
            }
        }
        public Area GuardaArea(int Gestor, int IdArea, int IdEmpresa, int IdAreaPadre, string NombreArea, string Abreviatura, string Sigla, bool Activo, int IdCatalogoTipoArea, int IdUsuarioAuditoria)
        {
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    System.Object[] ParamtrosOutPut = null; 
                    ParamtrosOutPut = ConexionSqlServer.GDatos.Ejecutar(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paGuardarArea", IdArea, IdEmpresa, IdAreaPadre, NombreArea, Abreviatura, Sigla, Activo, IdCatalogoTipoArea, IdUsuarioAuditoria, "", 0);
                    if (Convert.ToInt32(ParamtrosOutPut[100].ToString()) > 0)
                    {
                        area.mensaje.CodigoMensaje = 1;
                        area.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [GuardaArea], VERIFICAR CONSOLA";
                        area.mensaje.DescripcionMensajeSistema = ParamtrosOutPut[99].ToString();
                        return area;
                    }
                    area.IdArea = Convert.ToInt32(ParamtrosOutPut[0].ToString());
                    area.mensaje.DescripcionMensaje = ParamtrosOutPut[1].ToString();
                    area.mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[2].ToString());
                }
                return area;
            }
            catch (Exception ex)
            {
                area.mensaje.CodigoMensaje = 1;
                area.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [GuardaArea], VERIFICAR CONSOLA";
                area.mensaje.DescripcionMensajeSistema = ex.Message;
                return area;
            }
        }
        public async Task<Area> GuardaAreaAsync(int Gestor, int IdArea, int IdEmpresa, int IdAreaPadre, string NombreArea, string Abreviatura, string Sigla, bool Activo, int IdCatalogoTipoArea,bool VerRecepcion, int IdUsuarioAuditoria)
        {
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    System.Object[] ParamtrosOutPut = null; 
                    ParamtrosOutPut =await ConexionSqlServer.GDatos.EjecutarAsync(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paGuardarArea", IdArea, IdEmpresa, IdAreaPadre, NombreArea, Abreviatura, Sigla, Activo, IdCatalogoTipoArea, VerRecepcion, IdUsuarioAuditoria, "", 0);
                    if (Convert.ToInt32(ParamtrosOutPut[100].ToString()) > 0)
                    {
                        area.mensaje.CodigoMensaje = 1;
                        area.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [GuardaArea], VERIFICAR CONSOLA";
                        area.mensaje.DescripcionMensajeSistema = ParamtrosOutPut[99].ToString();
                        return area;
                    }
                    area.IdArea = Convert.ToInt32(ParamtrosOutPut[0].ToString());
                    area.mensaje.DescripcionMensaje = ParamtrosOutPut[1].ToString();
                    area.mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[2].ToString());
                }
                return area;
            }
            catch (Exception ex)
            {
                area.mensaje.CodigoMensaje = 1;
                area.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [GuardaArea], VERIFICAR CONSOLA";
                area.mensaje.DescripcionMensajeSistema = ex.Message;
                return area;
            }
        }
        public ListaArea ListarArea(int Gestor,int IdEmpresa, int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                     ds = ConexionSqlServer.GDatos.TraerDataSet(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paListarArea", IdEmpresa,IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        Lista.mensaje.CodigoMensaje = 1;
                        Lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarArea], VERIFICAR CONSOLA";
                        Lista.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();
                        return Lista;
                    }
                    dt = ds.Tables[0].Copy();
                    DataTable dtParametros = null;
                    dtParametros = ds.Tables[1].Copy();
                    Lista.paginacion.TotalRegistros = Convert.ToInt32(dtParametros.Rows[0][0]);
                }
                foreach (DataRow row in dt.Rows)
                {
                    area = new Area();
                    area.IdArea = (int)row["IdArea"];
                    area.NombreArea = (string)row["NombreArea"];
                    area.Abreviatura = (string)row["Abreviatura"];
                    area.Sigla = (string)row["Sigla"];
                    area.Activo = (bool)row["Activo"];
                    area.catalogotipoarea.Descripcion = (string)row["CatalogoTipoArea"];
                    area.empresa.NombreEmpresa = (string)row["NombreEmpresa"];
                    area.NombreAreaPadre = (string)row["NombreAreaPadre"];
                    Lista.lista.Add(area);
                }
                return Lista;
            }
            catch (Exception ex)
            {
                Lista.mensaje.CodigoMensaje = 1;
                Lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarArea], VERIFICAR CONSOLA";
                Lista.mensaje.DescripcionMensajeSistema = ex.Message;
                return Lista;
            }
            finally
            {
                ds.Dispose();
                ds.Clear();
                dt.Dispose();
                dt.Clear();
            }
        }
        public async Task<ListaArea> ListarAreaAsync(int Gestor, int IdEmpresa, int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                     ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paListarArea", IdEmpresa, IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        Lista.mensaje.CodigoMensaje = 1;
                        Lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarArea], VERIFICAR CONSOLA";
                        Lista.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();
                        return Lista;
                    }
                    dt = ds.Tables[0].Copy();
                    DataTable dtParametros = null;
                    dtParametros = ds.Tables[1].Copy();
                    Lista.paginacion.TotalRegistros = Convert.ToInt32(dtParametros.Rows[0][0]);
                }
                foreach (DataRow row in dt.Rows)
                {
                    area = new Area();
                    area.IdArea = (int)row["IdArea"];
                    area.NombreArea = (string)row["NombreArea"];
                    area.Abreviatura = (string)row["Abreviatura"];
                    area.Sigla = (string)row["Sigla"];
                    area.Activo = (bool)row["Activo"];
                    area.catalogotipoarea.Descripcion = (string)row["CatalogoTipoArea"];
                    area.empresa.NombreEmpresa = (string)row["NombreEmpresa"];
                    area.NombreAreaPadre = (string)row["NombreAreaPadre"];
                    area.VerRecepcion = (bool)row["VerRecepcion"];
                    Lista.lista.Add(area);
                }
                return Lista;
            }
            catch (Exception ex)
            {
                Lista.mensaje.CodigoMensaje = 1;
                Lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarArea], VERIFICAR CONSOLA";
                Lista.mensaje.DescripcionMensajeSistema = ex.Message;
                return Lista;
            }
            finally
            {
                ds.Dispose();
                ds.Clear();
                dt.Dispose();
                dt.Clear();
            }
        }
        public ListaArea ListarComboArea(int Gestor,int IdEmpresaSede)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                     ds = ConexionSqlServer.GDatos.TraerDataSet(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paListarComboArea", IdEmpresaSede);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        Lista.mensaje.CodigoMensaje = 1;
                        Lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarComboArea], VERIFICAR CONSOLA";
                        Lista.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();
                        return Lista;
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
                           area.IdArea = Convert.ToInt32(row["IdArea"].ToString());
                        area.NombreArea = row["NombreArea"].ToString();
                        Lista.lista.Add(area);
                    }

                }
                return Lista;
            }
            catch (Exception ex)
            {
                Lista.mensaje.CodigoMensaje = 1;
                Lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarComboArea], VERIFICAR CONSOLA";
                Lista.mensaje.DescripcionMensajeSistema = ex.Message;
                return Lista;
            }
            finally
            {
                ds.Dispose();
                ds.Clear();
                dt.Dispose();
                dt.Clear();
            }
        }        
      
        public async Task<ListaArea> ListarComboAreaAsync(int Gestor, int IdEmpresaSede)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                     ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paListarComboArea", IdEmpresaSede);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        Lista.mensaje.CodigoMensaje = 1;
                        Lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarComboArea], VERIFICAR CONSOLA";
                        Lista.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();
                        return Lista;
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
                        area = new Area();
                        area.IdArea = Convert.ToInt32(row["IdArea"].ToString());
                        area.NombreArea = row["NombreArea"].ToString();
                        Lista.lista.Add(area);
                    }

                }
                return await Task.FromResult(Lista);
            }
            catch (Exception ex)
            {
                Lista.mensaje.CodigoMensaje = 1;
                Lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarComboArea], VERIFICAR CONSOLA";
                Lista.mensaje.DescripcionMensajeSistema = ex.Message;
                return await Task.FromResult(Lista);
            }
            finally
            {
                ds.Dispose();
                ds.Clear();
                dt.Dispose();
                dt.Clear();
            }
        }
        public async Task<ListaArea> ListarComboArea3Async(int Gestor, int IdUsuarioAuditoria)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnGeneralSql, "Asistencias.paListarComboArea", IdUsuarioAuditoria);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        Lista.mensaje.CodigoMensaje = 1;
                        Lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarComboArea], VERIFICAR CONSOLA";
                        Lista.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();
                        return Lista;
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
                        area = new Area();
                        area.IdArea = Convert.ToInt32(row["IdArea"].ToString());
                        area.NombreArea = row["NombreArea"].ToString();
                        Lista.lista.Add(area);
                    }

                }
                return await Task.FromResult(Lista);
            }
            catch (Exception ex)
            {
                Lista.mensaje.CodigoMensaje = 1;
                Lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarComboArea], VERIFICAR CONSOLA";
                Lista.mensaje.DescripcionMensajeSistema = ex.Message;
                return await Task.FromResult(Lista);
            }
            finally
            {
                ds.Dispose();
                ds.Clear();
                dt.Dispose();
                dt.Clear();
            }
        }
        public ListaArea ListarComboAreaPadre(int Gestor, int IdEmpresa)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                     ds = ConexionSqlServer.GDatos.TraerDataSet(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paListarComboAreaPadre", IdEmpresa);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        Lista.mensaje.CodigoMensaje = 1;
                        Lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarComboArea], VERIFICAR CONSOLA";
                        Lista.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();
                        return Lista;
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
                        area = new Area();
                        area.IdArea = Convert.ToInt32(row["IdArea"].ToString());
                        area.NombreArea = row["NombreArea"].ToString();
                        Lista.lista.Add(area);
                    }

                }
                return Lista;
            }
            catch (Exception ex)
            {
                Lista.mensaje.CodigoMensaje = 1;
                Lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarComboArea], VERIFICAR CONSOLA";
                Lista.mensaje.DescripcionMensajeSistema = ex.Message;
                return Lista;
            }
            finally
            {
                ds.Dispose();
                ds.Clear();
                dt.Dispose();
                dt.Clear();
            }
        }
        public async Task<ListaArea> ListarComboAreaPadreAsync(int Gestor, int IdEmpresa)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                     ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paListarComboAreaPadre", IdEmpresa);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        Lista.mensaje.CodigoMensaje = 1;
                        Lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarComboArea], VERIFICAR CONSOLA";
                        Lista.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();
                        return Lista;
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
                        area = new Area();
                        area.IdArea = Convert.ToInt32(row["IdArea"].ToString());
                        area.NombreArea = row["NombreArea"].ToString();
                        Lista.lista.Add(area);
                    }

                }
                return Lista;
            }
            catch (Exception ex)
            {
                Lista.mensaje.CodigoMensaje = 1;
                Lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarComboArea], VERIFICAR CONSOLA";
                Lista.mensaje.DescripcionMensajeSistema = ex.Message;
                return Lista;
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