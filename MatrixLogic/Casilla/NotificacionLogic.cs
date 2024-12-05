using System;
using Utilitarios;
using System.Data;
using System.Threading.Tasks;
using Casilla;
namespace CasillaLogic
{

    public class NotificacionLogic
    {
        private Notificacion notificacion;
        private ListaNotificacion lista;

        public NotificacionLogic()
        {
            notificacion = new Notificacion();
            lista = new ListaNotificacion();
        }
        
        public async Task<Mensaje> RecibirNotificacion(int Gestor, int IdNotificacion, int IdUsuarioAuditoria)
        {
            Mensaje mensaje = new Mensaje();
            if (Gestor == DatosGlobales.GestorSqlServer)
            {
                System.Object[] ParamtrosOutPut = null;
                ParamtrosOutPut = await ConexionSqlServer.GDatos.EjecutarAsync(DatosGlobales.ListaConexiones.cnCasillaSql, "Casilla.paRecibirNotificacion", IdNotificacion, IdUsuarioAuditoria, mensaje.DescripcionMensaje, mensaje.CodigoMensaje);
                if (Convert.ToInt32(ParamtrosOutPut[100].ToString()) > 0)
                {
                    mensaje.CodigoMensaje = 1;
                    mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA EN LA CAPA DE DATOS [EnviarNotificacion] VERIFICAR CONSOLA";
                    mensaje.DescripcionMensajeSistema = ParamtrosOutPut[99].ToString();
                    return mensaje;
                }
                mensaje.DescripcionMensaje = ParamtrosOutPut[0].ToString();
                mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[1].ToString());
            }
            return mensaje;
        }
        public async Task<Mensaje> EnviarNotificacion(int Gestor, int IdNotificacion, int IdUsuarioAuditoria)
        {
            Mensaje mensaje = new Mensaje();
            if (Gestor == DatosGlobales.GestorSqlServer)
            {
                System.Object[] ParamtrosOutPut = null;
                ParamtrosOutPut = await ConexionSqlServer.GDatos.EjecutarAsync(DatosGlobales.ListaConexiones.cnCasillaSql, "Casilla.paEnviarNotificacion", IdNotificacion, IdUsuarioAuditoria, mensaje.DescripcionMensaje, mensaje.CodigoMensaje);
                if (Convert.ToInt32(ParamtrosOutPut[100].ToString()) > 0)
                {
                    mensaje.CodigoMensaje = 1;
                    mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA EN LA CAPA DE DATOS [EnviarNotificacion] VERIFICAR CONSOLA";
                    mensaje.DescripcionMensajeSistema = ParamtrosOutPut[99].ToString();
                    return mensaje;
                }
                mensaje.DescripcionMensaje = ParamtrosOutPut[0].ToString();
                mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[1].ToString());
            }
            return mensaje;
        }
        public async Task<Mensaje> EliminarNotificacionAsync(int Gestor, int IdNotificacion, int IdUsuarioAuditoria)
        {
            Mensaje mensaje = new Mensaje();
            if (Gestor == DatosGlobales.GestorSqlServer)
            {
                System.Object[] ParamtrosOutPut = null;
                ParamtrosOutPut = await ConexionSqlServer.GDatos.EjecutarAsync(DatosGlobales.ListaConexiones.cnCasillaSql, "Casilla.paEliminarNotificacion", IdNotificacion, IdUsuarioAuditoria, mensaje.DescripcionMensaje, mensaje.CodigoMensaje);
                if (Convert.ToInt32(ParamtrosOutPut[100].ToString()) > 0)
                {
                    mensaje.CodigoMensaje = 1;
                    mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA EN LA CAPA DE DATOS [EliminarNotificacion] VERIFICAR CONSOLA";
                    mensaje.DescripcionMensajeSistema = ParamtrosOutPut[99].ToString();
                    return mensaje;
                }
                mensaje.DescripcionMensaje = ParamtrosOutPut[0].ToString();
                mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[1].ToString());
            }
            return mensaje;
        }
        public async Task<Notificacion> ObtenerNotificacionAsync(int Gestor, int IdNotificacion, int IdUsuarioAuditoria)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnCasillaSql, "Casilla.paObtenerNotificacion", IdNotificacion, IdUsuarioAuditoria);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        notificacion.mensaje.CodigoMensaje = 1;
                        notificacion.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ObtenerNotificacion], VERIFICAR CONSOLA";
                        notificacion.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();

                        return notificacion;
                    }
                    dt = ds.Tables[0].Copy();
                    notificacion.IdNotificacion = dt.Rows[0]["IdNotificacion"] is System.DBNull ? 0 : (int)dt.Rows[0]["IdNotificacion"];
					

						notificacion.CantidadArchivos = dt.Rows[0]["CantidadArchivos"] is System.DBNull ? 0 : (int)dt.Rows[0]["CantidadArchivos"];
					notificacion.administradonotificado.IdAdministrado = dt.Rows[0]["IdAdministradoNotificado"] is System.DBNull ? 0 : (int)dt.Rows[0]["IdAdministradoNotificado"];
                    notificacion.administradonotificado.persona.NombreCompleto = dt.Rows[0]["NombreCompletoPersonaNotificada"] is System.DBNull ? null : (string)dt.Rows[0]["NombreCompletoPersonaNotificada"];
                    notificacion.catalogocategoria.IdCatalogo = dt.Rows[0]["IdCatalogoCategoria"] is System.DBNull ? 0 : (int)dt.Rows[0]["IdCatalogoCategoria"];

                    notificacion.areanotificador.IdArea = dt.Rows[0]["IdAreaNotificador"] is System.DBNull ? 0 : (int)dt.Rows[0]["IdAreaNotificador"];
                    notificacion.areanotificador.NombreArea = dt.Rows[0]["NombreArea"] is System.DBNull ? null : (string)dt.Rows[0]["NombreArea"];

                    notificacion.periodo.IdPeriodo = dt.Rows[0]["IdPeriodo"] is System.DBNull ? 0 : (int)dt.Rows[0]["IdPeriodo"];
                    notificacion.NumeroNotificacion = dt.Rows[0]["NumeroNotificacion"] is System.DBNull ? 0 : (int)dt.Rows[0]["NumeroNotificacion"];
                    notificacion.catalogocategoria.Descripcion = dt.Rows[0]["CatalogoCategoria"] is System.DBNull ? "" : (string)dt.Rows[0]["CatalogoCategoria"];
                    notificacion.AsuntoNotificacion = dt.Rows[0]["AsuntoNotificacion"] is System.DBNull ? null : (string)dt.Rows[0]["AsuntoNotificacion"];
                    notificacion.MensajeNotificacion = dt.Rows[0]["MensajeNotificacion"] is System.DBNull ? null : (string)dt.Rows[0]["MensajeNotificacion"];
                    notificacion.MensajeNotificacionHtml = dt.Rows[0]["MensajeNotificacionHtml"] is System.DBNull ? null : (string)dt.Rows[0]["MensajeNotificacionHtml"];
                    notificacion.NombreNumeroNotificacion = dt.Rows[0]["NombreNumeroNotificacion"] is System.DBNull ? null : (string)dt.Rows[0]["NombreNumeroNotificacion"];

					notificacion.administradonotificado.NumeroCelularNotificacion = dt.Rows[0]["NumeroCelularNotificacion"] is System.DBNull ? null : (string)dt.Rows[0]["NumeroCelularNotificacion"];
					notificacion.administradonotificado.EmailNotificacion = dt.Rows[0]["EmailNotificacion"] is System.DBNull ? null : (string)dt.Rows[0]["EmailNotificacion"];

					notificacion.FechaHoraRegistroNotificacion = dt.Rows[0]["FechaHoraRegistroNotificacion"] is System.DBNull ? null : (DateTime?)dt.Rows[0]["FechaHoraRegistroNotificacion"];
                    notificacion.FechaHoraNotificacionEnviada = dt.Rows[0]["FechaHoraNotificacionEnviada"] is System.DBNull ? null : (DateTime?)dt.Rows[0]["FechaHoraNotificacionEnviada"];
                    notificacion.NotificacionEnviada = dt.Rows[0]["NotificacionEnviada"] is System.DBNull ? false : (bool)dt.Rows[0]["NotificacionEnviada"];
                    notificacion.FechaHoraRecepcionNotificacion = dt.Rows[0]["FechaHoraRecepcionNotificacion"] is System.DBNull ? null : (DateTime?)dt.Rows[0]["FechaHoraRecepcionNotificacion"];
                    notificacion.NotificacionRecibida = dt.Rows[0]["NotificacionRecibida"] is System.DBNull ? false : (bool)dt.Rows[0]["NotificacionRecibida"];

                }
                return notificacion;
            }
            catch (Exception ex)
            {
                notificacion.mensaje.CodigoMensaje = 1;
                notificacion.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ObtenerNotificacion], VERIFICAR CONSOLA";
                notificacion.mensaje.DescripcionMensajeSistema = ex.Message;
                return notificacion;
            }
            finally
            {
                ds.Dispose();
                ds.Clear();
                dt.Dispose();
                dt.Clear();
            }
        }
        public async Task<Notificacion> GuardaNotificacionAsync(int Gestor, int IdNotificacion, int IdAdministradoNotificado, int IdCatalogoCategoria, string AsuntoNotificacion, string MensajeNotificacion,
            string MensajeNotificacionHtml, DateTime? FechaHoraNotificacionEnviada, bool NotificacionEnviada, DateTime? FechaHoraRecepcionNotificacion, bool NotificacionRecibida, int IdAreaNotificador, int IdUsuarioAuditoria)
        {
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    System.Object[] ParamtrosOutPut = null;
                    ParamtrosOutPut = await ConexionSqlServer.GDatos.EjecutarAsync(DatosGlobales.ListaConexiones.cnCasillaSql, "Casilla.paGuardarNotificacion", IdNotificacion, IdAdministradoNotificado, IdCatalogoCategoria,
                        AsuntoNotificacion, MensajeNotificacion, MensajeNotificacionHtml, FechaHoraNotificacionEnviada, NotificacionEnviada, FechaHoraRecepcionNotificacion, NotificacionRecibida,  IdAreaNotificador, IdUsuarioAuditoria, "", 0);
                    if (Convert.ToInt32(ParamtrosOutPut[100].ToString()) > 0)
                    {
                        notificacion.mensaje.CodigoMensaje = 1;
                        notificacion.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [GuardaNotificacion], VERIFICAR CONSOLA";
                        notificacion.mensaje.DescripcionMensajeSistema = ParamtrosOutPut[99].ToString();
                        return notificacion;
                    }
                    notificacion.IdNotificacion = Convert.ToInt32(ParamtrosOutPut[0].ToString());
                    notificacion.mensaje.DescripcionMensaje = ParamtrosOutPut[1].ToString();
                    notificacion.mensaje.CodigoMensaje = Convert.ToInt32(ParamtrosOutPut[2].ToString());
                }
                return notificacion;
            }
            catch (Exception ex)
            {
                notificacion.mensaje.CodigoMensaje = 1;
                notificacion.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [GuardaNotificacion], VERIFICAR CONSOLA";
                notificacion.mensaje.DescripcionMensajeSistema = ex.Message;
                return notificacion;
            }
        }
        
        public async Task<ListaNotificacion> ListarCategoriaMisNotificaciones(int Gestor, int IdUsuarioAuditoria,  string BusquedaGeneral)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnCasillaSql, "Casilla.paListarCategoriaMisNotificaciones", IdUsuarioAuditoria, BusquedaGeneral);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarNotificacion], VERIFICAR CONSOLA";
                        lista.mensaje.DescripcionMensajeSistema = ds.ExtendedProperties["DescripcionError"].ToString();
                        return lista;
                    }
                    dt = ds.Tables[0].Copy();                    
                }
                foreach (DataRow row in dt.Rows)
                {
                    notificacion = new Notificacion();                  
                    notificacion.catalogocategoria.Descripcion = row["CatalogoCategoria"] is System.DBNull ? null : (string)row["CatalogoCategoria"];
                    notificacion.catalogocategoria.Detalle = row["DetalleCategoria"] is System.DBNull ? null : (string)row["DetalleCategoria"];
                    notificacion.catalogocategoria.IdCatalogo = row["IdCatalogoCategoria"] is System.DBNull ? 0 : (int)row["IdCatalogoCategoria"];
                    notificacion.catalogocategoria.CantidadCatalogo = row["CantidadCatalogoCategoria"] is System.DBNull ? 0 : (int)row["CantidadCatalogoCategoria"];
                    notificacion.catalogocategoria.CantidadCatalogoRecibidas = row["CantidadCatalogoCategoriaRecibidas"] is System.DBNull ? 0 : (int)row["CantidadCatalogoCategoriaRecibidas"];
                    
                    lista.lista.Add(notificacion);
                }
                return lista;
            }
            catch (Exception ex)
            {
                lista.mensaje.CodigoMensaje = 1;
                lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarNotificacion], VERIFICAR CONSOLA";
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
        public async Task<ListaNotificacion> ListarMisNotificaciones(int Gestor, int IdCatalogoCategoria, int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnCasillaSql, "Casilla.paListarMisNotificaciones", IdCatalogoCategoria, IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarNotificacion], VERIFICAR CONSOLA";
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
                    notificacion = new Notificacion();
                    notificacion.IdNotificacion = row["IdNotificacion"] is System.DBNull ? 0 : (int)row["IdNotificacion"];
					notificacion.CantidadArchivos = row["IdNotificacion"] is System.DBNull ? 0 : (int)row["CantidadArchivos"];

                    notificacion.areanotificador.IdArea = row["IdAreaNotificador"] is System.DBNull ? 0 : (int)row["IdAreaNotificador"];
                    notificacion.areanotificador.NombreArea = row["NombreArea"] is System.DBNull ? null : (string)row["NombreArea"];

                    notificacion.periodo.IdPeriodo = row["IdPeriodo"] is System.DBNull ? 0 : (int)row["IdPeriodo"];
                    notificacion.NumeroNotificacion = row["NumeroNotificacion"] is System.DBNull ? 0 : (int)row["NumeroNotificacion"];

                    notificacion.administradonotificado.persona.NombreCompleto = row["NombreCompletoPersonaNotificada"] is System.DBNull ? null : (string)row["NombreCompletoPersonaNotificada"];
                    notificacion.catalogocategoria.Descripcion = row["CatalogoCategoria"] is System.DBNull ? null : (string)row["CatalogoCategoria"];
                    notificacion.AsuntoNotificacion = row["AsuntoNotificacion"] is System.DBNull ? null : (string)row["AsuntoNotificacion"];
                    notificacion.MensajeNotificacion = row["MensajeNotificacion"] is System.DBNull ? null : (string)row["MensajeNotificacion"];
                    notificacion.MensajeNotificacionHtml = row["MensajeNotificacionHtml"] is System.DBNull ? null : (string)row["MensajeNotificacionHtml"];
                    notificacion.NombreNumeroNotificacion = row["NombreNumeroNotificacion"] is System.DBNull ? null : (string)row["NombreNumeroNotificacion"];
                    notificacion.FechaHoraRegistroNotificacion = row["FechaHoraRegistroNotificacion"] is System.DBNull ? null : (DateTime?)row["FechaHoraRegistroNotificacion"];
                    notificacion.FechaHoraNotificacionEnviada = row["FechaHoraRegistroNotificacion"] is System.DBNull ? null : (DateTime?)row["FechaHoraNotificacionEnviada"];
                    notificacion.NotificacionEnviada = row["NotificacionEnviada"] is System.DBNull ? false : (bool)row["NotificacionEnviada"];
                    notificacion.FechaHoraRecepcionNotificacion = row["FechaHoraRecepcionNotificacion"] is System.DBNull ? null : (DateTime?)row["FechaHoraRecepcionNotificacion"];
                    notificacion.NotificacionRecibida = row["NotificacionRecibida"] is System.DBNull ? false : (bool)row["NotificacionRecibida"];

                    lista.lista.Add(notificacion);
                }
                return lista;
            }
            catch (Exception ex)
            {
                lista.mensaje.CodigoMensaje = 1;
                lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarNotificacion], VERIFICAR CONSOLA";
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
        public async Task<ListaNotificacion> ListarNotificacionesGeneradas(int Gestor,int IdArea, int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnCasillaSql, "Casilla.paListarNotificacionesGeneradas",IdArea, IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarNotificacionesGeneradas], VERIFICAR CONSOLA";
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
                    notificacion = new Notificacion();
                    notificacion.IdNotificacion = row["IdNotificacion"] is System.DBNull ? 0 : (int)row["IdNotificacion"];
					notificacion.CantidadArchivos = row["IdNotificacion"] is System.DBNull ? 0 : (int)row["CantidadArchivos"];
					notificacion.periodo.IdPeriodo = row["IdPeriodo"] is System.DBNull ? 0 : (int)row["IdPeriodo"];
                    notificacion.NumeroNotificacion = row["NumeroNotificacion"] is System.DBNull ? 0 : (int)row["NumeroNotificacion"];
                    notificacion.administradonotificado.persona.NombreCompleto = row["NombreCompletoPersonaNotificada"] is System.DBNull ? null : (string)row["NombreCompletoPersonaNotificada"];
                    notificacion.catalogocategoria.Descripcion = row["CatalogoCategoria"] is System.DBNull ? null : (string)row["CatalogoCategoria"];
                    notificacion.AsuntoNotificacion = row["AsuntoNotificacion"] is System.DBNull ? null : (string)row["AsuntoNotificacion"];
                    notificacion.NombreNumeroNotificacion = row["NombreNumeroNotificacion"] is System.DBNull ? null : (string)row["NombreNumeroNotificacion"];
                    notificacion.MensajeNotificacionHtml = row["MensajeNotificacionHtml"] is System.DBNull ? null : (string)row["MensajeNotificacionHtml"];
                    notificacion.MensajeNotificacion = row["MensajeNotificacion"] is System.DBNull ? null : (string)row["MensajeNotificacion"];
                    notificacion.FechaHoraNotificacionEnviada = row["FechaHoraNotificacionEnviada"] is System.DBNull ? null : (DateTime?)row["FechaHoraNotificacionEnviada"];
                    notificacion.FechaHoraRegistroNotificacion = row["FechaHoraRegistroNotificacion"] is System.DBNull ? null : (DateTime?)row["FechaHoraRegistroNotificacion"];

                    notificacion.areanotificador.NombreArea = row["NombreArea"] is System.DBNull ? null : (string)row["NombreArea"];
                    notificacion.areanotificador.IdArea = row["IdAreaNotificador"] is System.DBNull ? 0 : (int)row["IdAreaNotificador"];

                    notificacion.NotificacionEnviada = row["NotificacionEnviada"] is System.DBNull ? false : (bool)row["NotificacionEnviada"];
                    notificacion.FechaHoraRecepcionNotificacion = row["FechaHoraRecepcionNotificacion"] is System.DBNull ? null : (DateTime?)row["FechaHoraRecepcionNotificacion"];
                    notificacion.NotificacionRecibida = row["NotificacionRecibida"] is System.DBNull ? false : (bool)row["NotificacionRecibida"];

					notificacion.administradonotificado.EmailNotificacion = row["EmailNotificacion"] is System.DBNull ? null : (string)row["EmailNotificacion"];

					lista.lista.Add(notificacion);
                }
                return lista;
            }
            catch (Exception ex)
            {
                lista.mensaje.CodigoMensaje = 1;
                lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarNotificacion], VERIFICAR CONSOLA";
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
        public async Task<ListaNotificacion> ListarNotificacionAsync(int Gestor,int IdArea, int IdUsuarioAuditoria, string CampoOrdenado, string TipoOrdenacion, int NumeroPagina, int DimensionPagina, string BusquedaGeneral)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    ds = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnCasillaSql, "Casilla.paListarNotificacion", IdArea, IdUsuarioAuditoria, CampoOrdenado, TipoOrdenacion, NumeroPagina, DimensionPagina, BusquedaGeneral);
                    if (Convert.ToInt32(ds.ExtendedProperties["NumeroError"].ToString()) > 0)
                    {
                        lista.mensaje.CodigoMensaje = 1;
                        lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA DE DATOS [ListarNotificacion], VERIFICAR CONSOLA";
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
                    notificacion = new Notificacion();
                    notificacion.IdNotificacion = row["IdNotificacion"] is System.DBNull ? 0 : (int)row["IdNotificacion"];
					notificacion.CantidadArchivos = row["IdNotificacion"] is System.DBNull ? 0 : (int)row["CantidadArchivos"];
					notificacion.periodo.IdPeriodo = row["IdPeriodo"] is System.DBNull ? 0 : (int)row["IdPeriodo"];
                    notificacion.NumeroNotificacion = row["NumeroNotificacion"] is System.DBNull ? 0 : (int)row["NumeroNotificacion"];
                    notificacion.administradonotificado.persona.NombreCompleto = row["NombreCompletoPersonaNotificada"] is System.DBNull ? null : (string)row["NombreCompletoPersonaNotificada"];
                    notificacion.catalogocategoria.Descripcion = row["CatalogoCategoria"] is System.DBNull ? null : (string)row["CatalogoCategoria"];
                    notificacion.AsuntoNotificacion = row["AsuntoNotificacion"] is System.DBNull ? null : (string)row["AsuntoNotificacion"];
                    notificacion.MensajeNotificacion = row["MensajeNotificacion"] is System.DBNull ? null : (string)row["MensajeNotificacion"];
                    notificacion.MensajeNotificacionHtml = row["MensajeNotificacionHtml"] is System.DBNull ? null : (string)row["MensajeNotificacionHtml"];
                    notificacion.FechaHoraNotificacionEnviada = row["FechaHoraNotificacionEnviada"] is System.DBNull ? null : (DateTime?)row["FechaHoraNotificacionEnviada"];
                    notificacion.FechaHoraRegistroNotificacion = row["FechaHoraRegistroNotificacion"] is System.DBNull ? null : (DateTime?)row["FechaHoraRegistroNotificacion"];
                    notificacion.NombreNumeroNotificacion = row["NombreNumeroNotificacion"] is System.DBNull ? null : (string)row["NombreNumeroNotificacion"];
                    notificacion.NotificacionEnviada = row["NotificacionEnviada"] is System.DBNull ? false : (bool)row["NotificacionEnviada"];
                    notificacion.FechaHoraRecepcionNotificacion = row["FechaHoraRecepcionNotificacion"] is System.DBNull ? null : (DateTime?)row["FechaHoraRecepcionNotificacion"];
                    notificacion.NotificacionRecibida = row["NotificacionRecibida"] is System.DBNull ? false : (bool)row["NotificacionRecibida"];
                    notificacion.areanotificador.NombreArea = row["NombreArea"] is System.DBNull ? null : (string)row["NombreArea"];
                    notificacion.areanotificador.IdArea = row["IdAreaNotificador"] is System.DBNull ? 0 : (int)row["IdAreaNotificador"];

                    lista.lista.Add(notificacion);
                }
                return lista;
            }
            catch (Exception ex)
            {
                lista.mensaje.CodigoMensaje = 1;
                lista.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ListarNotificacion], VERIFICAR CONSOLA";
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
        public async Task<DataSet> DescargarNotificacion(int Gestor, int IdUsuarioAuditoria)
        {
            DataSet ds = new DataSet();
            DataSet listads = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                if (Gestor == DatosGlobales.GestorSqlServer)
                {
                    listads = await ConexionSqlServer.GDatos.TraerDataSetAsync(DatosGlobales.ListaConexiones.cnCasillaSql, "Casilla.paDescargarNotificacion", IdUsuarioAuditoria);
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
