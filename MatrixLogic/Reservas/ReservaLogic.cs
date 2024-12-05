using System;
using Utilitarios;
using System.Data;
using System.Threading.Tasks;
using Reservas;
namespace ReservasLogic
{

    public class ReservaLogic
    {
        private Reserva reserva;
        private ListaReserva lista;

        public ReservaLogic()
        {
            reserva = new Reserva();
            lista = new ListaReserva();
        }

        public async Task<Mensaje> EliminarReservaAsync(int Gestor, int IdReserva, int IdUsuarioAuditoria)
        {
            Mensaje mensaje = new Mensaje();
            if (Gestor == DatosGlobales.GestorSqlServer)
            {
                System.Object[] ParamtrosOutPut = null;
                ParamtrosOutPut = await ConexionSqlServer.GDatos.EjecutarAsync(DatosGlobales.ListaConexiones.cnReservasSql, "Reservas.paEliminarReserva", IdReserva, IdUsuarioAuditoria, mensaje.DescripcionMensaje, mensaje.CodigoMensaje);
                if (Convert.ToInt32(ParamtrosOutPut[100].ToString()) > 0)
                {
                    mensaje.CodigoMensaje = 1;
                    mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA EN LA CAPA DE DATOS [EliminarReserva] VERIFICAR CONSOLA";
                    mensaje.DescripcionMensajeSistema = ParamtrosOutPut[99].ToString();
                    return mensaje;
                }
                mensaje.DescripcionMensaje = ParamtrosOutPut[0].ToString();
                mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[1].ToString());
            }
            return mensaje;
        }
        public async Task<Reserva> ObtenerReservaAsync(int Gestor, int IdReserva, int IdUsuarioAuditoria)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnReservasSql, "Reservas.paObtenerReserva", IdReserva, IdUsuarioAuditoria);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        reserva.mensaje.CodigoMensaje = 1;
                        reserva.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ObtenerReserva], VERIFICAR CONSOLA";
                        reserva.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();

                        return reserva;
                    }
                    dt = ds.Tables[0].Copy();
                    reserva.IdReserva = dt.Rows[0]["IdReserva"] is System.DBNull ? 0 : (int)dt.Rows[0]["IdReserva"];
                    reserva.solicitud.IdSolicitud = dt.Rows[0]["IdSolicitud"] is System.DBNull ? 0 : (int)dt.Rows[0]["IdSolicitud"];
                    reserva.sala.IdSala = dt.Rows[0]["IdSala"] is System.DBNull ? 0 : (int)dt.Rows[0]["IdSala"];
                    reserva.FechaDesdeReserva = dt.Rows[0]["FechaDesdeReserva"] is System.DBNull ? null : (string)dt.Rows[0]["FechaDesdeReserva"];
                    reserva.FechaHastaReserva = dt.Rows[0]["FechaHastaReserva"] is System.DBNull ? null : (string)dt.Rows[0]["FechaHastaReserva"];
                    reserva.HoraInicio = dt.Rows[0]["HoraInicio"] is System.DBNull ? null : (string)dt.Rows[0]["HoraInicio"];
                    reserva.HoraFin = dt.Rows[0]["HoraFin"] is System.DBNull ? null : (string)dt.Rows[0]["HoraFin"];
                    reserva.Observaciones = dt.Rows[0]["Observaciones"] is System.DBNull ? null : (string)dt.Rows[0]["Observaciones"];

                }
                return reserva;
            }
            catch (Exception ex)
            {
                reserva.mensaje.CodigoMensaje = 1;
                reserva.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ObtenerReserva], VERIFICAR CONSOLA";
                reserva.mensaje.DescripcionMensajeSistema = ex.Message;
                return reserva;
            }
            finally
            {
                ds.Dispose();
                ds.Clear();
                dt.Dispose();
                dt.Clear();
            }
        }
        public async Task<Reserva> GuardaReservaAsync(int Gestor, int IdReserva, int IdSolicitud, int IdSala, string FechaDesdeReserva, string FechaHastaReserva, string HoraInicio, string HoraFin, string Observaciones, int IdUsuarioAuditoria)
        {
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    System.Object[] ParamtrosOutPut = null;
                    ParamtrosOutPut = await ConexionSqlServer.GDatos.EjecutarAsync(DatosGlobales.ListaConexiones.cnReservasSql, "Reservas.paGuardarReserva", IdReserva, IdSolicitud, IdSala, FechaDesdeReserva, FechaHastaReserva, HoraInicio, HoraFin, Observaciones, IdUsuarioAuditoria, "", 0);
                    if (Convert.ToInt32(ParamtrosOutPut[100].ToString()) > 0)
                    {
                        reserva.mensaje.CodigoMensaje = 1;
                        reserva.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [GuardaReserva], VERIFICAR CONSOLA";
                        reserva.mensaje.DescripcionMensajeSistema = ParamtrosOutPut[99].ToString();
                        return reserva;
                    }
                    reserva.IdReserva = Convert.ToInt32(ParamtrosOutPut[0].ToString());
                    reserva.mensaje.DescripcionMensaje = ParamtrosOutPut[1].ToString();
                    reserva.mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[2].ToString());
                }
                return reserva;
            }
            catch (Exception ex)
            {
                reserva.mensaje.CodigoMensaje = 1;
                reserva.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [GuardaReserva], VERIFICAR CONSOLA";
                reserva.mensaje.DescripcionMensajeSistema = ex.Message;
                return reserva;
            }
        }

        public async Task<ListaReserva> ListarReservaAsync(int Gestor, int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnReservasSql, "Reservas.paListarReserva", IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarReserva], VERIFICAR CONSOLA";
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
                    reserva = new Reserva();
                    reserva.IdReserva = row["IdReserva"] is System.DBNull ? 0 : (int)row["IdReserva"];

                    reserva.solicitud.IdSolicitud = row["IdSolicitud"] is System.DBNull ? 0 : (int)row["IdSolicitud"];

                    reserva.sala.IdSala = row["IdSala"] is System.DBNull ? 0 : (int)row["IdSala"];
                    reserva.FechaDesdeReserva = row["FechaDesdeReserva"] is System.DBNull ? null : (string)row["FechaDesdeReserva"];
                    reserva.FechaHastaReserva = row["FechaHastaReserva"] is System.DBNull ? null : (string)row["FechaHastaReserva"];
                    reserva.HoraInicio = row["HoraInicio"] is System.DBNull ? null : (string)row["HoraInicio"];
                    reserva.HoraFin = row["HoraFin"] is System.DBNull ? null : (string)row["HoraFin"];
                    reserva.Observaciones = row["Observaciones"] is System.DBNull ? null : (string)row["Observaciones"];

                    lista.lista.Add(reserva);
                }
                return lista;
            }
            catch (Exception ex)
            {
                lista.mensaje.CodigoMensaje = 1;
                lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarReserva], VERIFICAR CONSOLA";
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
        public async Task<DataSet> DescargarReserva(int Gestor, int IdUsuarioAuditoria)
        {
            DataSet ds = new DataSet();
            DataSet listads = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    listads = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnReservasSql, "Reservas.paDescargarReserva", IdUsuarioAuditoria);
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
