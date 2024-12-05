using System;
using Utilitarios;
using System.Data;
using System.Collections.Generic;
using Sanciones;
using System.Threading.Tasks;
using System.Data.SqlClient;
using General;

namespace SancionesLogic
{
    public class SancionLogic
    {
        private Sancion sancion;
        private ListaSancion lista;
        private Sucursal sucursal;

        public SancionLogic()
        {
            sancion = new Sancion();
            lista = new ListaSancion();
        }

        public async Task<ListaSancion> ListarSancionAsync(int Gestor, int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            ListaSancion lista = new ListaSancion();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnSancionesSql, "Sancion.paListarSancion", IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);

                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarSancion], VERIFICAR CONSOLA";
                        lista.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();
                        return lista;
                    }

                    dt = ds.Tables[0].Copy();
                    DataTable dtParametros = ds.Tables[1].Copy();
                    lista.paginacion.TotalRegistros = Convert.ToInt32(dtParametros.Rows[0][0]);
                }

                Sancion sancion = null;
                foreach (DataRow row in dt.Rows)
                {
                    sancion = new Sancion
                    {
                        Id = (int)row["Id"],
                        TipoDocumento = row["TipoDocumento"] as string ?? string.Empty,
                        NroDocumento = row["NroDocumento"] as string ?? string.Empty,
                        CMP = row["CMP"] as string ?? string.Empty,
                        NombreCompleto = row["NombreCompleto"] as string ?? string.Empty,
                        TipoSancion = row["TipoSancion"] as string ?? string.Empty,
                        ConsejoRegional = row["ConsejoRegional"] as string ?? string.Empty,
                        NumeroResolucion = row["NumeroResolucion"] as string ?? string.Empty,
                        FechaResolucion = row["FechaResolucion"] != DBNull.Value ? (DateTime?)row["FechaResolucion"] : null,
                        FechaNotificacionMedico = row["FechaNotificacionMedico"] != DBNull.Value ? (DateTime?)row["FechaNotificacionMedico"] : null,
                        FechaVencimientoSancion = row["FechaVencimientoSancion"] != DBNull.Value ? (DateTime?)row["FechaVencimientoSancion"] : null,
                        FechaCumplimientoMulta = row["FechaCumplimientoMulta"] != DBNull.Value ? (DateTime?)row["FechaCumplimientoMulta"] : null,
                        FechaRegistroInscripcion = row["FechaRegistroInscripcion"] != DBNull.Value ? (DateTime?)row["FechaRegistroInscripcion"] : null,
                        EstadoSancion = row["EstadoSancion"] as string ?? string.Empty,
                        SancionImpuesta = row["SancionImpuesta"] as string ?? string.Empty,
                        IndicacionNormaVulnerada = row["IndicacionNormaVulnerada"] as string ?? string.Empty
                    };
                    lista.lista.Add(sancion);
                }

                return lista;
            }
            catch (Exception ex)
            {
                lista.mensaje.CodigoMensaje = 1;
                lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LÓGICA [ListarSancion], VERIFICAR CONSOLA";
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

        public async Task<Sancion> ObtenerSancionAsync(int Gestor, int Id)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Id == 0)
                {
                    return sancion;
                }

                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnSancionesSql, "Sancion.paObtenerSancion", Id);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        sancion.mensaje.CodigoMensaje = 1;
                        sancion.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ObtenerSancion], VERIFICAR CONSOLA";
                        sancion.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();

                        return sancion;
                    }
                    dt = ds.Tables[0].Copy();
                    if (dt.Rows.Count > 0)
                    {
                        sancion.Id = (int)dt.Rows[0]["Id"];
                        sancion.IdCatalogoTipoDocumentoPersonal = (int)dt.Rows[0]["IdCatalogoTipoDocumentoPersonal"];
                        sancion.IdCatalogoTipoSancion = (int)dt.Rows[0]["IdCatalogoTipoSancion"];
                        sancion.IdSucursal = (int)dt.Rows[0]["IdSucursal"];
                        sancion.TipoDocumento = (string)dt.Rows[0]["TipoDocumento"] as string ?? string.Empty;
                        sancion.NroDocumento = (string)dt.Rows[0]["NroDocumento"] as string ?? string.Empty;
                        sancion.NombreCompleto = (string)dt.Rows[0]["NombreCompleto"] as string ?? string.Empty;
                        sancion.TipoSancion = (string)dt.Rows[0]["TipoSancion"] as string ?? string.Empty;
                        sancion.ConsejoRegional = (string)dt.Rows[0]["ConsejoRegional"] as string ?? string.Empty; ;
                        sancion.CMP = (string)dt.Rows[0]["CMP"] as string ?? string.Empty;
                        sancion.NumeroResolucion = (string)dt.Rows[0]["NumeroResolucion"] as string ?? string.Empty;
                        sancion.FechaResolucion = dt.Rows[0]["FechaResolucion"] != DBNull.Value ? (DateTime?)dt.Rows[0]["FechaResolucion"] : null;
                        sancion.FechaNotificacionMedico = dt.Rows[0]["FechaNotificacionMedico"] != DBNull.Value ? (DateTime?)dt.Rows[0]["FechaNotificacionMedico"] : null;
                        sancion.FechaVencimientoSancion = dt.Rows[0]["FechaVencimientoSancion"] != DBNull.Value ? (DateTime?)dt.Rows[0]["FechaVencimientoSancion"] : null;
                        sancion.FechaCumplimientoMulta = dt.Rows[0]["FechaCumplimientoMulta"] != DBNull.Value ? (DateTime?)dt.Rows[0]["FechaCumplimientoMulta"] : null;
                        sancion.FechaRegistroInscripcion = dt.Rows[0]["FechaRegistroInscripcion"] != DBNull.Value ? (DateTime?)dt.Rows[0]["FechaRegistroInscripcion"] : null;
                        sancion.EstadoSancion = (string)dt.Rows[0]["EstadoSancion"] as string ?? string.Empty;
                        sancion.SancionImpuesta = (string)dt.Rows[0]["SancionImpuesta"] as string ?? string.Empty;
                        sancion.IndicacionNormaVulnerada = (string)dt.Rows[0]["IndicacionNormaVulnerada"] as string ?? string.Empty;
                    }

                }
                return sancion;
            }
            catch (Exception ex)
            {
                sancion.mensaje.CodigoMensaje = 1;
                sancion.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ObtenerSancion], VERIFICAR CONSOLA";
                sancion.mensaje.DescripcionMensajeSistema = ex.Message;
                return sancion;
            }
            finally
            {
                ds.Dispose();
                ds.Clear();
                dt.Dispose();
                dt.Clear();
            }
        }

        public async Task<Sancion> ObtenerSancionPorCodeAsync(int Gestor, string Id)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnSancionesSql, "Sancion.paObtenerSancionPorCode", Id);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        sancion.mensaje.CodigoMensaje = 1;
                        sancion.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ObtenerSancionPorCode], VERIFICAR CONSOLA";
                        sancion.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();

                        return sancion;
                    }
                    dt = ds.Tables[0].Copy();
                    if (dt.Rows.Count > 0)
                    {
                        sancion.Id = (int)dt.Rows[0]["Id"];
                        sancion.IdCatalogoTipoDocumentoPersonal = (int)dt.Rows[0]["IdCatalogoTipoDocumentoPersonal"];
                        sancion.IdCatalogoTipoSancion = (int)dt.Rows[0]["IdCatalogoTipoSancion"];
                        sancion.IdSucursal = (int)dt.Rows[0]["IdSucursal"];
                        sancion.TipoDocumento = (string)dt.Rows[0]["TipoDocumento"] as string ?? string.Empty;
                        sancion.NroDocumento = (string)dt.Rows[0]["NroDocumento"] as string ?? string.Empty;
                        sancion.NombreCompleto = (string)dt.Rows[0]["NombreCompleto"] as string ?? string.Empty;
                        sancion.TipoSancion = (string)dt.Rows[0]["TipoSancion"] as string ?? string.Empty;
                        sancion.ConsejoRegional = (string)dt.Rows[0]["ConsejoRegional"] as string ?? string.Empty; ;
                        sancion.CMP = (string)dt.Rows[0]["CMP"] as string ?? string.Empty;
                        sancion.NumeroResolucion = (string)dt.Rows[0]["NumeroResolucion"] as string ?? string.Empty;
                        sancion.FechaResolucion = dt.Rows[0]["FechaResolucion"] != DBNull.Value ? (DateTime?)dt.Rows[0]["FechaResolucion"] : null;
                        sancion.FechaNotificacionMedico = dt.Rows[0]["FechaNotificacionMedico"] != DBNull.Value ? (DateTime?)dt.Rows[0]["FechaNotificacionMedico"] : null;
                        sancion.FechaVencimientoSancion = dt.Rows[0]["FechaVencimientoSancion"] != DBNull.Value ? (DateTime?)dt.Rows[0]["FechaVencimientoSancion"] : null;
                        sancion.FechaCumplimientoMulta = dt.Rows[0]["FechaCumplimientoMulta"] != DBNull.Value ? (DateTime?)dt.Rows[0]["FechaCumplimientoMulta"] : null;
                        sancion.FechaRegistroInscripcion = dt.Rows[0]["FechaRegistroInscripcion"] != DBNull.Value ? (DateTime?)dt.Rows[0]["FechaRegistroInscripcion"] : null;
                        sancion.EstadoSancion = (string)dt.Rows[0]["EstadoSancion"] as string ?? string.Empty;
                        sancion.SancionImpuesta = (string)dt.Rows[0]["SancionImpuesta"] as string ?? string.Empty;
                        sancion.IndicacionNormaVulnerada = (string)dt.Rows[0]["IndicacionNormaVulnerada"] as string ?? string.Empty;
                    }

                }
                return sancion;
            }
            catch (Exception ex)
            {
                sancion.mensaje.CodigoMensaje = 1;
                sancion.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ObtenerSancionPorCode], VERIFICAR CONSOLA";
                sancion.mensaje.DescripcionMensajeSistema = ex.Message;
                return sancion;
            }
            finally
            {
                ds.Dispose();
                ds.Clear();
                dt.Dispose();
                dt.Clear();
            }
        }

        public async Task<Sancion> GuardaSancionAsync(int Gestor, int Id, string NombreCompleto, string TipoDocumento, string NroDocumento, string TipoSancion, string ConsejoRegional, string CMP, string NumeroResolucion, DateTime? FechaResolucion, DateTime? FechaNotificacionMedico, DateTime? FechaVencimientoSancion, DateTime? FechaCumplimientoMulta, DateTime? FechaRegistroInscripcion, string EstadoSancion, string SancionImpuesta, string IndicacionNormaVulnerada, int IdUsuarioAuditoria)
        {
            Sancion sancion = new Sancion();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    object[] ParamtrosOutPut = null;

                    ParamtrosOutPut = await ConexionSqlServer.GDatos.EjecutarAsync(DatosGlobales.ListaConexiones.cnSancionesSql, "Sancion.paGuardarSancion", Id, NombreCompleto, Convert.ToInt32(TipoDocumento), NroDocumento, Convert.ToInt32(TipoSancion), Convert.ToInt32(ConsejoRegional), CMP, NumeroResolucion, FechaResolucion, FechaNotificacionMedico, FechaVencimientoSancion, FechaCumplimientoMulta, FechaRegistroInscripcion, EstadoSancion, SancionImpuesta, IndicacionNormaVulnerada, IdUsuarioAuditoria, "", 0);

                    if (Convert.ToInt32(ParamtrosOutPut[100].ToString()) > 0)
                    {
                        sancion.mensaje.CodigoMensaje = 1;
                        sancion.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [GuardaSancion], VERIFICAR CONSOLA";
                        sancion.mensaje.DescripcionMensajeSistema = ParamtrosOutPut[99].ToString();
                        return sancion;
                    }

                    sancion = await ObtenerSancionAsync(Gestor, Convert.ToInt32(ParamtrosOutPut[0].ToString()));
                    sancion.mensaje.DescripcionMensaje = ParamtrosOutPut[1].ToString();
                    sancion.mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[2].ToString());
                }
                return sancion;
            }
            catch (Exception ex)
            {
                sancion.mensaje.CodigoMensaje = 1;
                sancion.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [GuardaSancion], VERIFICAR CONSOLA";
                sancion.mensaje.DescripcionMensajeSistema = ex.Message;
                return sancion;
            }
        }

        public async Task<Mensaje> EliminarSancionAsync(int Gestor, int Id, int IdUsuarioAuditoria)
        {
            Mensaje mensaje = new Mensaje();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    object[] ParamtrosOutPut = null;

                    ParamtrosOutPut = await ConexionSqlServer.GDatos.EjecutarAsync(DatosGlobales.ListaConexiones.cnSancionesSql, "Sancion.paEliminarSancion", Id, IdUsuarioAuditoria, mensaje.DescripcionMensaje, mensaje.CodigoMensaje);

                    if (Convert.ToInt32(ParamtrosOutPut[100].ToString()) > 0)
                    {
                        mensaje.CodigoMensaje = 1;
                        mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA EN LA CAPA DE DATOS [EliminarSancion], VERIFICAR CONSOLA";
                        mensaje.DescripcionMensajeSistema = ParamtrosOutPut[2].ToString();
                        return mensaje;
                    }

                    mensaje.DescripcionMensaje = ParamtrosOutPut[0].ToString();
                    mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[1].ToString());
                }
                return mensaje;
            }
            catch (Exception ex)
            {
                mensaje.CodigoMensaje = 1;
                mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LÓGICA [EliminarSancion], VERIFICAR CONSOLA";
                mensaje.DescripcionMensajeSistema = ex.Message;
                return mensaje;
            }
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
                    listads = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnSancionesSql, "Sancion.paDescargarReporte", IdUsuarioAuditoria);
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

        public async Task<ListaSancion> BuscarSancionesAsync(int Gestor, int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral, string TipoDocumento = null, string NroDocumento = null, string NombreCompleto = null, string ConsejoRegional = null, string TipoSancion = null, string Departamento = null, string Provincia = null, string Distrito = null)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            ListaSancion lista = new ListaSancion();

            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnSancionesSql, "Sancion.paBuscarSancion", IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral, TipoDocumento, NroDocumento, NombreCompleto, ConsejoRegional, TipoSancion, Departamento, Provincia, Distrito);

                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [BuscarSancion], VERIFICAR CONSOLA";
                        lista.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();
                        return lista;
                    }

                    dt = ds.Tables[0].Copy();
                    DataTable dtParametros = ds.Tables[1].Copy();
                    lista.paginacion.TotalRegistros = Convert.ToInt32(dtParametros.Rows[0][0]);
                }

                Sancion sancion = null;
                foreach (DataRow row in dt.Rows)
                {
                    sancion = new Sancion
                    {
                        Id = (int)row["Id"],
                        TipoDocumento = row["TipoDocumento"] as string ?? string.Empty,
                        NroDocumento = row["NroDocumento"] as string ?? string.Empty,
                        CMP = row["CMP"] as string ?? string.Empty,
                        NombreCompleto = row["NombreCompleto"] as string ?? string.Empty,
                        TipoSancion = row["TipoSancion"] as string ?? string.Empty,
                        ConsejoRegional = row["ConsejoRegional"] as string ?? string.Empty,
                        NumeroResolucion = row["NumeroResolucion"] as string ?? string.Empty,
                        FechaResolucion = row["FechaResolucion"] != DBNull.Value ? (DateTime?)row["FechaResolucion"] : null,
                        FechaNotificacionMedico = row["FechaNotificacionMedico"] != DBNull.Value ? (DateTime?)row["FechaNotificacionMedico"] : null,
                        FechaVencimientoSancion = row["FechaVencimientoSancion"] != DBNull.Value ? (DateTime?)row["FechaVencimientoSancion"] : null,
                        FechaCumplimientoMulta = row["FechaCumplimientoMulta"] != DBNull.Value ? (DateTime?)row["FechaCumplimientoMulta"] : null,
                        FechaRegistroInscripcion = row["FechaRegistroInscripcion"] != DBNull.Value ? (DateTime?)row["FechaRegistroInscripcion"] : null,
                        EstadoSancion = row["EstadoSancion"] as string ?? string.Empty,
                        SancionImpuesta = row["SancionImpuesta"] as string ?? string.Empty,
                        IndicacionNormaVulnerada = row["IndicacionNormaVulnerada"] as string ?? string.Empty
                    };
                    lista.lista.Add(sancion);
                }

                return lista;
            }
            catch (Exception ex)
            {
                lista.mensaje.CodigoMensaje = 1;
                lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LÓGICA [BuscarSancion], VERIFICAR CONSOLA";
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

        public async Task<DataSet> DescargarReporteExcelExterno(int Gestor, int IdUsuarioAuditoria, string TipoDocumento, string NroDocumento, string NombreCompleto, string ConsejoRegional, string TipoSancion, string Departamento, string Provincia, string Distrito)
        {
            DataSet ds = new DataSet();
            DataSet listads = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    listads = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnSancionesSql, "Sancion.paDescargarReporteExterno", IdUsuarioAuditoria, TipoDocumento, NroDocumento, NombreCompleto, ConsejoRegional, TipoSancion, Departamento, Provincia, Distrito);
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

        public async Task<List<Sucursal>> ObtenerConsejosRegionalesAsync(int Gestor)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            List<Sucursal> lista = new List<Sucursal>();
            try
            {

                if (Gestor == DatosGlobales.GestorSqlServer) { ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnGeneralSql, "General.paListarConsejosRegionales"); }
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



    }

}



