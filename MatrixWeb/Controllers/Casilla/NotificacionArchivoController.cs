
using DataTables.AspNet.Core;
using Microsoft.AspNetCore.Http;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using MatrixService;
using System.Threading.Tasks;
using Casilla;
using System.Configuration;
using System.IO;

namespace MatrixWeb.Controllers
{
    public class NotificacionArchivoController : Controller
    {
        private readonly ISvcNotificacionArchivo inotificacionarchivo;
        private readonly ISvcNotificacion inotificacion;
        public NotificacionArchivoController(ISvcNotificacionArchivo _inotificacionarchivo, ISvcNotificacion _inotificacion)
        {
            inotificacion = _inotificacion;
            inotificacionarchivo = _inotificacionarchivo;
        }
        [HttpPost]
        public async Task<IActionResult> IndexNotificacionArchivo()
        {
            var conpermiso = MatrixUtilitarios.Utilitario.TienePermisoControladorAccion(HttpContext.Request.RouteValues["controller"].ToString(), HttpContext.Request.RouteValues["action"].ToString()); if (conpermiso == -1) { return RedirectToAction("Login", "Usuario"); }
            int IdNotificacion = Convert.ToInt32(Request.Form["IdNotificacion"]);
            ViewBag.IdNotificacion = IdNotificacion;
            var notificacion = await inotificacion.ObtenerNotificacionAsync(IdNotificacion, Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));
            return View(notificacion);

        }
        [HttpPost]
        public async Task<IActionResult> ListarNotificacionArchivo(IDataTablesRequest requestModel)
        {
            int IdNotificacion = Convert.ToInt32(Request.Form["IdNotificacion"]);
            var listanotificacionarchivo = new ListaNotificacionArchivo();
            int numeroPagina = (requestModel.Start / requestModel.Length) + 1;
            try
            {
                listanotificacionarchivo = await inotificacionarchivo.ListarNotificacionArchivoAsync(IdNotificacion, Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")), null, null, numeroPagina, requestModel.Length, requestModel.Search.Value);
                var totalCount = listanotificacionarchivo.paginacion.TotalRegistros;
                var data = listanotificacionarchivo.lista.Select(lq => new
                {
                    IdNotificacionArchivo = lq.IdNotificacionArchivo,
                    Notificacion = lq.notificacion.IdNotificacion,
                    CatalogoTipoDocumento = lq.catalogotipodocumento.Descripcion,
                    NumeroDocumento = lq.NumeroDocumento,
                    RutaArchivo = lq.RutaArchivo,
                    ExtensionArchivo = lq.ExtensionArchivo,
                    PesoArchivo = lq.PesoArchivo.ToString("N0"),
                    FechaHoraLecturaArchivo = String.Format("{0:dd/MM/yyyy HH:mm:ss}", lq.FechaHoraLecturaArchivo),
                    ArchivoLeido = (lq.ArchivoLeido == true ? "Si" : "No"),
                }).ToList();

                var jsonData = new { draw = requestModel.Draw, recordsFiltered = totalCount, recordsTotal = totalCount, data = data }; return Ok(jsonData);
            }
            catch (Exception ex)
            {
                return RedirectToAction("PaginaError", new { DescripcionError = "Hubo un error [" + ex.Message + "]" });
            }
        }


        [HttpPost]
        public async Task<IActionResult> RegistroNotificacionArchivo()
        {
            int IdNotificacion = Convert.ToInt32(Request.Form["IdNotificacion"]);
            int IdNotificacionArchivo = Convert.ToInt32(Request.Form["IdNotificacionArchivo"]);
            var notificacion = new Notificacion();

            var notificacionarchivo = new NotificacionArchivo();
            try
            {



                if (IdNotificacionArchivo > 0)
                {
                    notificacionarchivo = await inotificacionarchivo.ObtenerNotificacionArchivoAsync(IdNotificacionArchivo, Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));
                }

                notificacion = await inotificacion.ObtenerNotificacionAsync(IdNotificacion, Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));
                notificacionarchivo.notificacion = notificacion;
                return View(notificacionarchivo);
            }
            catch (Exception ex)
            {
                notificacionarchivo.mensaje.CodigoMensaje = 1;
                notificacionarchivo.mensaje.DescripcionMensaje = "Sucedió un error en la Vista: " + HttpContext.Request.RouteValues["action"].ToString() + " del controlador: " + HttpContext.Request.RouteValues["controller"].ToString();
                notificacionarchivo.mensaje.DescripcionMensajeSistema = ex.Message;
                return View(notificacionarchivo);
            }
        }
        [HttpPost]
        public async Task<IActionResult> GuardarRegistroNotificacionArchivo()
        {
            var notificacionarchivo = new NotificacionArchivo();
            try
            {
                int IdNotificacionArchivo = Convert.ToInt32(Request.Form["txtRegIdNotificacionArchivo"]);
                int IdNotificacion = Convert.ToInt32(Request.Form["IdNotificacion"]);
                int IdCatalogoTipoDocumento = Convert.ToInt32(Request.Form["cmbCatalogoTipoDocumento"]);
                string NumeroDocumento = Request.Form["txtNumeroDocumento"];


                string ExtensionArchivo = Request.Form["txtExtensionArchivo"];
                decimal PesoArchivo = 0;
                DateTime? FechaHoraLecturaArchivo = null;
                if (Request.Form["txtFechaHoraLecturaArchivo"] != "")
                {
                    Convert.ToDateTime(Request.Form["txtFechaHoraLecturaArchivo"], System.Globalization.CultureInfo.GetCultureInfo("es-ES").DateTimeFormat);
                }
                bool ArchivoLeido = Convert.ToBoolean(Convert.ToInt32(Request.Form["optArchivoLeido"]));


                IFormFile fileRutaArchivo = null;
                string RutaArchivo = "";
                string NombreArchivoRutaArchivo = Request.Form["txtNombreRutaArchivo"];
                if (HttpContext.Request.Form.Files.Count > 0)
                {
                    Random r = new Random();
                    fileRutaArchivo = HttpContext.Request.Form.Files["ArchivoRutaArchivo"];
                    RutaArchivo = HttpContext.Session.GetString("IdUsuario").ToString() + r.Next(1, 100).ToString() + string.Format("{0:ddMMyyyyHHmmss}{1}", DateTime.Now, Path.GetExtension(fileRutaArchivo.FileName));

                }
                else
                {
                    RutaArchivo = NombreArchivoRutaArchivo;
                }

                var pathRutaArchivo = "";
                pathRutaArchivo = Path.Combine(ConfigurationManager.AppSettings["RutaArchivos"], RutaArchivo);
                if (HttpContext.Request.Form.Files.Count > 0)
                {
                    if (!Directory.Exists(ConfigurationManager.AppSettings["RutaArchivos"]))
                    {
                        Directory.CreateDirectory(ConfigurationManager.AppSettings["RutaArchivos"]);
                    }
                    pathRutaArchivo = Path.Combine(ConfigurationManager.AppSettings["RutaArchivos"], RutaArchivo);
                    using (var stream = new FileStream(pathRutaArchivo, FileMode.Create))
                    {
                        fileRutaArchivo.CopyTo(stream);
                    }
                }
                FileInfo fileinfo = new FileInfo(pathRutaArchivo);
                PesoArchivo =Convert.ToDecimal(fileinfo.Length / 1024);
                ExtensionArchivo = fileinfo.Extension; //System.IO.Path.GetExtension(pathRutaArchivo);

                notificacionarchivo = await inotificacionarchivo.GuardaNotificacionArchivoAsync(
                                                        IdNotificacionArchivo,
                                                                IdNotificacion,
                                                                IdCatalogoTipoDocumento,
                                                                NumeroDocumento,
                                                                RutaArchivo,
                                                                ExtensionArchivo,
                                                                PesoArchivo,
                                                                FechaHoraLecturaArchivo,
                                                                ArchivoLeido,
                                                        Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));


                return Json(notificacionarchivo);
            }
            catch (Exception ex)
            {
                notificacionarchivo.mensaje.CodigoMensaje = 1;
                notificacionarchivo.mensaje.DescripcionMensaje = "Sucedió un error en la Vista: " + HttpContext.Request.RouteValues["action"].ToString() + " del controlador: " + HttpContext.Request.RouteValues["controller"].ToString();
                notificacionarchivo.mensaje.DescripcionMensajeSistema = ex.Message;
                return Json(notificacionarchivo);
            }
        }

        [HttpPost]
        public async Task<IActionResult> MiArchivoAdjuntoNotificado()
        {
            int IdNotificacionArchivo = Convert.ToInt32(Request.Form["IdNotificacionArchivo"]);
            NotificacionArchivo notificacionarchivo = new NotificacionArchivo();
            try
            {
               var mensaje = await inotificacionarchivo.RegistrarLecturaArchivoAdjuntoNotificado(IdNotificacionArchivo, Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));
                notificacionarchivo = await inotificacionarchivo.ObtenerNotificacionArchivoAsync(IdNotificacionArchivo, Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));
                return Json(notificacionarchivo);
            }
            catch (Exception ex)
            {
                notificacionarchivo.mensaje.CodigoMensaje = 1;
                notificacionarchivo.mensaje.DescripcionMensaje = "Sucedió un error en la Vista: " + HttpContext.Request.RouteValues["action"].ToString() + " del controlador: " + HttpContext.Request.RouteValues["controller"].ToString();
                notificacionarchivo.mensaje.DescripcionMensajeSistema = ex.Message;
                return Json(notificacionarchivo);
            }
        }

        [HttpPost]
        public async Task<IActionResult> EliminarNotificacionArchivo()
        {
            int IdNotificacionArchivo = Convert.ToInt32(Request.Form["IdNotificacionArchivo"]);
            Utilitarios.Mensaje mensaje = new Utilitarios.Mensaje();
            try
            {
                mensaje = await inotificacionarchivo.EliminarNotificacionArchivoAsync(IdNotificacionArchivo, Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));
                return Json(mensaje);
            }
            catch (Exception ex)
            {
                mensaje.CodigoMensaje = 1;
                mensaje.DescripcionMensaje = "Sucedió un error en la Vista: " + HttpContext.Request.RouteValues["action"].ToString() + " del controlador: " + HttpContext.Request.RouteValues["controller"].ToString();
                mensaje.DescripcionMensajeSistema = ex.Message;
                return Json(mensaje);
            }
        }
    }
}
