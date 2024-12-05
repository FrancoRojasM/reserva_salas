using System;
using Utilitarios;
using System.Data;
using System.Threading.Tasks;
using Reservas;
namespace ReservasLogic
{

    public class SolicitudLogic
    {
        private Solicitud solicitud;
        private ListaSolicitud lista;

        public SolicitudLogic()
        {
            solicitud = new Solicitud();
            lista = new ListaSolicitud();
        }

        public async Task<Mensaje> EliminarSolicitudAsync(int Gestor, int IdSolicitud, int IdUsuarioAuditoria)
        {
            Mensaje mensaje = new Mensaje();
            if (Gestor == DatosGlobales.GestorSqlServer)
            {
                System.Object[] ParamtrosOutPut = null;
                ParamtrosOutPut = await ConexionSqlServer.GDatos.EjecutarAsync(DatosGlobales.ListaConexiones.cnReservasSql, "Reservas.paEliminarSolicitud", IdSolicitud, IdUsuarioAuditoria, mensaje.DescripcionMensaje, mensaje.CodigoMensaje);
                if (Convert.ToInt32(ParamtrosOutPut[100].ToString()) > 0)
                {
                    mensaje.CodigoMensaje = 1;
                    mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA EN LA CAPA DE DATOS [EliminarSolicitud] VERIFICAR CONSOLA";
                    mensaje.DescripcionMensajeSistema = ParamtrosOutPut[99].ToString();
                    return mensaje;
                }
                mensaje.DescripcionMensaje = ParamtrosOutPut[0].ToString();
                mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[1].ToString());
            }
            return mensaje;
        }
        public async Task<Solicitud> ObtenerSolicitudAsync(int Gestor, int IdSolicitud, int IdUsuarioAuditoria)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnReservasSql, "Reservas.paObtenerSolicitud", IdSolicitud, IdUsuarioAuditoria);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        solicitud.mensaje.CodigoMensaje = 1;
                        solicitud.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ObtenerSolicitud], VERIFICAR CONSOLA";
                        solicitud.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();

                        return solicitud;
                    }
                    dt = ds.Tables[0].Copy();
                    solicitud.IdSolicitud = dt.Rows[0]["IdSolicitud"] is System.DBNull ? 0 : (int)dt.Rows[0]["IdSolicitud"];
                    solicitud.catalogoconsejoregional.IdCatalogo = dt.Rows[0]["IdCatalogoConsejoRegional"] is System.DBNull ? 0 : (int)dt.Rows[0]["IdCatalogoConsejoRegional"];
                    solicitud.catalogoconsejoregional.Descripcion = dt.Rows[0]["CatalogoConsejoRegional"] is System.DBNull ? "" : (string)dt.Rows[0]["CatalogoConsejoRegional"];
                    solicitud.catalogosecretaria.IdCatalogo = dt.Rows[0]["IdCatalogoSecretaria"] is System.DBNull ? 0 : (int)dt.Rows[0]["IdCatalogoSecretaria"];
                    solicitud.catalogosecretaria.Descripcion = dt.Rows[0]["CatalogoSecretaria"] is System.DBNull ? "" : (string)dt.Rows[0]["CatalogoSecretaria"];
                    solicitud.catalogocomite.IdCatalogo = dt.Rows[0]["IdCatalogoComite"] is System.DBNull ? 0 : (int)dt.Rows[0]["IdCatalogoComite"];
                    solicitud.catalogocomite.Descripcion = dt.Rows[0]["CatalogoComite"] is System.DBNull ? "" : (string)dt.Rows[0]["CatalogoComite"];
                    solicitud.NombreEvento = dt.Rows[0]["NombreEvento"] is System.DBNull ? null : (string)dt.Rows[0]["NombreEvento"];
                    solicitud.NumeroParticipantes = dt.Rows[0]["NumeroParticipantes"] is System.DBNull ? 0 : (int)dt.Rows[0]["NumeroParticipantes"];
                    solicitud.FechaDesde = dt.Rows[0]["FechaDesde"] is System.DBNull ? null : (string)dt.Rows[0]["FechaDesde"];
                    solicitud.FechaHasta = dt.Rows[0]["FechaHasta"] is System.DBNull ? null : (string)dt.Rows[0]["FechaHasta"];
                    solicitud.NumeroDias = dt.Rows[0]["NumeroDias"] is System.DBNull ? 0 : (int)dt.Rows[0]["NumeroDias"];
                    solicitud.HoraInicio = dt.Rows[0]["HoraInicio"] is System.DBNull ? null : (string)dt.Rows[0]["HoraInicio"];
                    solicitud.HoraFin = dt.Rows[0]["HoraFin"] is System.DBNull ? null : (string)dt.Rows[0]["HoraFin"];
                    solicitud.NumeroAuditorios = dt.Rows[0]["NumeroAuditorios"] is System.DBNull ? 0 : (int)dt.Rows[0]["NumeroAuditorios"];
                    solicitud.ResponsableEvento = dt.Rows[0]["ResponsableEvento"] is System.DBNull ? null : (string)dt.Rows[0]["ResponsableEvento"];
                    solicitud.TelefonoContacto = dt.Rows[0]["TelefonoContacto"] is System.DBNull ? null : (string)dt.Rows[0]["TelefonoContacto"];
                    solicitud.CorreoContacto = dt.Rows[0]["CorreoContacto"] is System.DBNull ? null : (string)dt.Rows[0]["CorreoContacto"];
                    solicitud.Observaciones = dt.Rows[0]["Observaciones"] is System.DBNull ? null : (string)dt.Rows[0]["Observaciones"];
                    solicitud.salaasignada.IdSala = dt.Rows[0]["IdSalaAsignada"] is System.DBNull ? 0 : (int)dt.Rows[0]["IdSalaAsignada"];
                    solicitud.catalogoestadosolicitud.IdCatalogo = dt.Rows[0]["IdCatalogoEstadoSolicitud"] is System.DBNull ? 0 : (int)dt.Rows[0]["IdCatalogoEstadoSolicitud"];
                    solicitud.catalogoestadosolicitud.Descripcion = dt.Rows[0]["CatalogoEstadoSolicitud"] is System.DBNull ? "" : (string)dt.Rows[0]["CatalogoEstadoSolicitud"];

                }
                return solicitud;
            }
            catch (Exception ex)
            {
                solicitud.mensaje.CodigoMensaje = 1;
                solicitud.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ObtenerSolicitud], VERIFICAR CONSOLA";
                solicitud.mensaje.DescripcionMensajeSistema = ex.Message;
                return solicitud;
            }
            finally
            {
                ds.Dispose();
                ds.Clear();
                dt.Dispose();
                dt.Clear();
            }
        }
        public async Task<Solicitud> GuardaSolicitudAsync(int Gestor, int IdSolicitud, int IdCatalogoConsejoRegional, int IdCatalogoSecretaria, int IdCatalogoComite, string NombreEvento, int NumeroParticipantes, string FechaDesde, string FechaHasta, int NumeroDias, string HoraInicio, string HoraFin, int NumeroAuditorios, string ResponsableEvento, string TelefonoContacto, string CorreoContacto, string Observaciones, int IdSalaAsignada, int IdCatalogoEstadoSolicitud, int IdUsuarioAuditoria)
        {
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    System.Object[] ParamtrosOutPut = null;
                    ParamtrosOutPut = await ConexionSqlServer.GDatos.EjecutarAsync(DatosGlobales.ListaConexiones.cnReservasSql, "Reservas.paGuardarSolicitud", IdSolicitud, IdCatalogoConsejoRegional, IdCatalogoSecretaria, IdCatalogoComite, NombreEvento, NumeroParticipantes, FechaDesde, FechaHasta, NumeroDias, HoraInicio, HoraFin, NumeroAuditorios, ResponsableEvento, TelefonoContacto, CorreoContacto, Observaciones, IdSalaAsignada, IdCatalogoEstadoSolicitud, IdUsuarioAuditoria, "", 0);
                    if (Convert.ToInt32(ParamtrosOutPut[100].ToString()) > 0)
                    {
                        solicitud.mensaje.CodigoMensaje = 1;
                        solicitud.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [GuardaSolicitud], VERIFICAR CONSOLA";
                        solicitud.mensaje.DescripcionMensajeSistema = ParamtrosOutPut[99].ToString();
                        return solicitud;
                    }
                    solicitud.IdSolicitud = Convert.ToInt32(ParamtrosOutPut[0].ToString());
                    solicitud.mensaje.DescripcionMensaje = ParamtrosOutPut[1].ToString();
                    solicitud.mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[2].ToString());
                }
                return solicitud;
            }
            catch (Exception ex)
            {
                solicitud.mensaje.CodigoMensaje = 1;
                solicitud.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [GuardaSolicitud], VERIFICAR CONSOLA";
                solicitud.mensaje.DescripcionMensajeSistema = ex.Message;
                return solicitud;
            }
        }

        public async Task<ListaSolicitud> ListarSolicitudAsync(int Gestor, int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnReservasSql, "Reservas.paListarSolicitud", IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarSolicitud], VERIFICAR CONSOLA";
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
                    solicitud = new Solicitud();
                    solicitud.IdSolicitud = row["IdSolicitud"] is System.DBNull ? 0 : (int)row["IdSolicitud"];
                    solicitud.catalogoconsejoregional.Descripcion = row["CatalogoConsejoRegional"] is System.DBNull ? null : (string)row["CatalogoConsejoRegional"];
                    solicitud.catalogosecretaria.Descripcion = row["CatalogoSecretaria"] is System.DBNull ? null : (string)row["CatalogoSecretaria"];
                    solicitud.catalogocomite.Descripcion = row["CatalogoComite"] is System.DBNull ? null : (string)row["CatalogoComite"];
                    solicitud.NombreEvento = row["NombreEvento"] is System.DBNull ? null : (string)row["NombreEvento"];
                    solicitud.NumeroParticipantes = row["NumeroParticipantes"] is System.DBNull ? 0 : (int)row["NumeroParticipantes"];
                    solicitud.FechaDesde = row["FechaDesde"] is System.DBNull ? null : (string)row["FechaDesde"];
                    solicitud.FechaHasta = row["FechaHasta"] is System.DBNull ? null : (string)row["FechaHasta"];
                    solicitud.NumeroDias = row["NumeroDias"] is System.DBNull ? 0 : (int)row["NumeroDias"];
                    solicitud.HoraInicio = row["HoraInicio"] is System.DBNull ? null : (string)row["HoraInicio"];
                    solicitud.HoraFin = row["HoraFin"] is System.DBNull ? null : (string)row["HoraFin"];
                    solicitud.NumeroAuditorios = row["NumeroAuditorios"] is System.DBNull ? 0 : (int)row["NumeroAuditorios"];
                    solicitud.ResponsableEvento = row["ResponsableEvento"] is System.DBNull ? null : (string)row["ResponsableEvento"];
                    solicitud.TelefonoContacto = row["TelefonoContacto"] is System.DBNull ? null : (string)row["TelefonoContacto"];
                    solicitud.CorreoContacto = row["CorreoContacto"] is System.DBNull ? null : (string)row["CorreoContacto"];
                    solicitud.Observaciones = row["Observaciones"] is System.DBNull ? null : (string)row["Observaciones"];

                    solicitud.salaasignada.IdSala = row["IdSala"] is System.DBNull ? 0 : (int)row["IdSala"];
                    solicitud.catalogoestadosolicitud.Descripcion = row["CatalogoEstadoSolicitud"] is System.DBNull ? null : (string)row["CatalogoEstadoSolicitud"];

                    lista.lista.Add(solicitud);
                }
                return lista;
            }
            catch (Exception ex)
            {
                lista.mensaje.CodigoMensaje = 1;
                lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarSolicitud], VERIFICAR CONSOLA";
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


        //Mi SP - FARM
        public async Task<ListaSolicitud> ListarSolicitudesAsync(int Gestor, int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    // Eliminar los parámetros de paginación y búsqueda
                    ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnReservasSql, "Reservas.paListarSolicitud", IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, DBNull.Value, DBNull.Value, DBNull.Value);

                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarSolicitud], VERIFICAR CONSOLA";
                        lista.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();
                        return lista;
                    }

                    dt = ds.Tables[0].Copy();
                    DataTable dtParametros = ds.Tables[1].Copy();
                    lista.paginacion.TotalRegistros = Convert.ToInt32(dtParametros.Rows[0][0]);
                }

                // Iterar sobre las filas de la tabla
                foreach (DataRow row in dt.Rows)
                {
                    solicitud = new Solicitud();
                    solicitud.IdSolicitud = row["IdSolicitud"] is System.DBNull ? 0 : (int)row["IdSolicitud"];
                    solicitud.catalogoconsejoregional.Descripcion = row["CatalogoConsejoRegional"] is System.DBNull ? null : (string)row["CatalogoConsejoRegional"];
                    solicitud.catalogosecretaria.Descripcion = row["CatalogoSecretaria"] is System.DBNull ? null : (string)row["CatalogoSecretaria"];
                    solicitud.catalogocomite.Descripcion = row["CatalogoComite"] is System.DBNull ? null : (string)row["CatalogoComite"];
                    solicitud.NombreEvento = row["NombreEvento"] is System.DBNull ? null : (string)row["NombreEvento"];
                    solicitud.NumeroParticipantes = row["NumeroParticipantes"] is System.DBNull ? 0 : (int)row["NumeroParticipantes"];
                    solicitud.FechaDesde = row["FechaDesde"] is System.DBNull ? null : (string)row["FechaDesde"];
                    solicitud.FechaHasta = row["FechaHasta"] is System.DBNull ? null : (string)row["FechaHasta"];
                    solicitud.NumeroDias = row["NumeroDias"] is System.DBNull ? 0 : (int)row["NumeroDias"];
                    solicitud.HoraInicio = row["HoraInicio"] is System.DBNull ? null : (string)row["HoraInicio"];
                    solicitud.HoraFin = row["HoraFin"] is System.DBNull ? null : (string)row["HoraFin"];
                    solicitud.NumeroAuditorios = row["NumeroAuditorios"] is System.DBNull ? 0 : (int)row["NumeroAuditorios"];
                    solicitud.ResponsableEvento = row["ResponsableEvento"] is System.DBNull ? null : (string)row["ResponsableEvento"];
                    solicitud.TelefonoContacto = row["TelefonoContacto"] is System.DBNull ? null : (string)row["TelefonoContacto"];
                    solicitud.CorreoContacto = row["CorreoContacto"] is System.DBNull ? null : (string)row["CorreoContacto"];
                    solicitud.Observaciones = row["Observaciones"] is System.DBNull ? null : (string)row["Observaciones"];

                    solicitud.salaasignada.IdSala = row["IdSala"] is System.DBNull ? 0 : (int)row["IdSala"];
                    solicitud.catalogoestadosolicitud.Descripcion = row["CatalogoEstadoSolicitud"] is System.DBNull ? null : (string)row["CatalogoEstadoSolicitud"];

                    lista.lista.Add(solicitud);
                }

                return lista;
            }
            catch (Exception ex)
            {
                lista.mensaje.CodigoMensaje = 1;
                lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarSolicitud], VERIFICAR CONSOLA";
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



        public async Task<DataSet> DescargarSolicitud(int Gestor, int IdUsuarioAuditoria)
        {
            DataSet ds = new DataSet();
            DataSet listads = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    listads = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnReservasSql, "Reservas.paDescargarSolicitud", IdUsuarioAuditoria);
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
                //dt.Dispose();
                //dt.Clear();
            }
        }
    }

}
