
using DataTables.AspNet.Core;
using Microsoft.AspNetCore.Http;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using MatrixService;
using System.Threading.Tasks;
using Casilla;
using System.Collections.Generic;
using System.Data;
using System.IO;
using OfficeOpenXml;
using Utilitarios;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Utilitarios.Servicios;
namespace MatrixWeb.Controllers
{
    public class NotificacionController : Controller
    {

        private readonly ISvcNotificacionArchivo inotificacionarchivo;
        private readonly ISvcNotificacion inotificacion;

        public NotificacionController(ISvcNotificacionArchivo _inotificacionarchivo, ISvcNotificacion _inotificacion)
        {
            inotificacion = _inotificacion;
            inotificacionarchivo = _inotificacionarchivo;

        }
        [HttpGet]
        public IActionResult EnvioNotificaciones()
        {
            //filtrar por categorias
            var conpermiso = MatrixUtilitarios.Utilitario.TienePermisoControladorAccion(HttpContext.Request.RouteValues["controller"].ToString(), HttpContext.Request.RouteValues["action"].ToString()); if (conpermiso == -1) { return RedirectToAction("Login", "Usuario"); }

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ListarNotificacionesGeneradas(IDataTablesRequest requestModel)
        {

            var listanotificacion = new ListaNotificacion();

            int numeroPagina = (requestModel.Start / requestModel.Length) + 1;
            try
            {
                listanotificacion = await inotificacion.ListarNotificacionesGeneradas(Convert.ToInt32(HttpContext.Session.GetString("IdArea")), Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")), null, null, numeroPagina, requestModel.Length, requestModel.Search.Value);
                var totalCount = listanotificacion.paginacion.TotalRegistros;
                var data = listanotificacion.lista.Select(lq => new
                {
                    IdNotificacion = lq.IdNotificacion,
                    AdministradoNotificado = lq.administradonotificado.persona.NombreCompleto,
                    CatalogoCategoria = lq.catalogocategoria.Descripcion,
                    AsuntoNotificacion = lq.AsuntoNotificacion,
                    lq.NombreNumeroNotificacion,
                    lq.NumeroNotificacion,
                    lq.periodo.IdPeriodo,
                    lq.areanotificador.NombreArea,
					EmailNotificacion=lq.administradonotificado.EmailNotificacion,
                    lq.areanotificador.IdArea,
                    lq.CantidadArchivos,
                    FechaHoraRegistroNotificacion = String.Format("{0:dd/MM/yyyy HH:mm}", lq.FechaHoraRegistroNotificacion),
                    MensajeNotificacion = lq.MensajeNotificacion,
                    lq.MensajeNotificacionHtml,
                    FechaHoraNotificacionEnviada = String.Format("{0:dd/MM/yyyy HH:mm}", lq.FechaHoraNotificacionEnviada),
                    NotificacionEnviada = (lq.NotificacionEnviada == true ? "Si" : "No"),
                    FechaHoraRecepcionNotificacion = String.Format("{0:dd/MM/yyyy HH:mm}", lq.FechaHoraRecepcionNotificacion),
                    NotificacionRecibida = (lq.NotificacionRecibida == true ? "Si" : "No"),
                }).ToList();

                var jsonData = new { draw = requestModel.Draw, recordsFiltered = totalCount, recordsTotal = totalCount, data = data }; return Ok(jsonData);
            }
            catch (Exception ex)
            {
                return RedirectToAction("PaginaError", new { DescripcionError = "Hubo un error [" + ex.Message + "]" });
            }
        }
        [HttpGet]
        public async Task<IActionResult> MisNotificaciones()
        {
            //filtrar por categorias
            var conpermiso = MatrixUtilitarios.Utilitario.TienePermisoControladorAccion(HttpContext.Request.RouteValues["controller"].ToString(), HttpContext.Request.RouteValues["action"].ToString()); if (conpermiso == -1) { return RedirectToAction("Login", "Usuario"); }

            ViewBag.conpermiso = conpermiso;
            var listanotificacion = await inotificacion.ListarCategoriaMisNotificaciones(Convert.ToInt32(HttpContext.Session.GetString("IdUsuario")), "");
            return View(listanotificacion);
        }
        [HttpPost]
        public async Task<IActionResult> MiMensajeNotificado()
        {
            int IdNotificacion = Convert.ToInt32(Request.Form["IdNotificacion"]);
            var notificacion = new Notificacion();
            try
            {
                notificacion = await inotificacion.ObtenerNotificacionAsync(IdNotificacion, Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));
                //validar si ya esta recibida si no es asi recibirlo y actualziar el registro
                if (!notificacion.NotificacionRecibida)
                {
                    var mensaje = await inotificacion.RecibirNotificacion(IdNotificacion, Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));
                }

                ListaNotificacionArchivo listanotificacionarchivo = await inotificacionarchivo.ListarNotificacionArchivoAsync(IdNotificacion, Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")), null, null, 1, 100, "");
                ViewBag.ListaNotificacionArchivo = listanotificacionarchivo;
                return View(notificacion);
            }
            catch (Exception ex)
            {
                notificacion.mensaje.CodigoMensaje = 1;
                notificacion.mensaje.DescripcionMensaje = "Sucedió un error en la Vista: " + HttpContext.Request.RouteValues["action"].ToString() + " del controlador: " + HttpContext.Request.RouteValues["controller"].ToString();
                notificacion.mensaje.DescripcionMensajeSistema = ex.Message;
                return View(notificacion);
            }
        }
        [HttpPost]
        public async Task<IActionResult> ListarMisNotificaciones(IDataTablesRequest requestModel)
        {

            var listanotificacion = new ListaNotificacion();
            int IdCatalogoCategoria = Convert.ToInt32(Request.Form["IdCatalogoCategoria"]);
            int numeroPagina = (requestModel.Start / requestModel.Length) + 1;
            try
            {
                listanotificacion = await inotificacion.ListarMisNotificaciones(IdCatalogoCategoria, Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")), null, null, numeroPagina, requestModel.Length, requestModel.Search.Value);
                var totalCount = listanotificacion.paginacion.TotalRegistros;
                var data = listanotificacion.lista.Select(lq => new
                {
                    IdNotificacion = lq.IdNotificacion,
                    AdministradoNotificado = lq.administradonotificado.persona.NombreCompleto,
                    CatalogoCategoria = lq.catalogocategoria.Descripcion,
                    AsuntoNotificacion = lq.AsuntoNotificacion,
                    MensajeNotificacion = lq.MensajeNotificacion,
                    lq.NumeroNotificacion,
                    lq.NombreNumeroNotificacion,
                    lq.areanotificador.NombreArea,
                    lq.areanotificador.IdArea,
                    lq.periodo.IdPeriodo,
                    lq.MensajeNotificacionHtml,
                    lq.CantidadArchivos,
                    FechaHoraRegistroNotificacion = String.Format("{0:dd/MM/yyyy HH:mm}", lq.FechaHoraRegistroNotificacion),
                    FechaHoraNotificacionEnviada = String.Format("{0:dd/MM/yyyy HH:mm}", lq.FechaHoraNotificacionEnviada),
                    NotificacionEnviada = (lq.NotificacionEnviada == true ? "Si" : "No"),
                    FechaHoraRecepcionNotificacion = String.Format("{0:dd/MM/yyyy HH:mm}", lq.FechaHoraRecepcionNotificacion),
                    NotificacionRecibida = (lq.NotificacionRecibida == true ? "Si" : "No"),
                }).ToList();

                var jsonData = new { draw = requestModel.Draw, recordsFiltered = totalCount, recordsTotal = totalCount, data = data }; return Ok(jsonData);
            }
            catch (Exception ex)
            {
                return RedirectToAction("PaginaError", new { DescripcionError = "Hubo un error [" + ex.Message + "]" });
            }
        }
        [HttpGet]
        public IActionResult Control()
        {
            var conpermiso = MatrixUtilitarios.Utilitario.TienePermisoControladorAccion(HttpContext.Request.RouteValues["controller"].ToString(), HttpContext.Request.RouteValues["action"].ToString()); if (conpermiso == -1) { return RedirectToAction("Login", "Usuario"); }
            return View();
        }
        [HttpGet]
        public IActionResult IndexNotificacion()
        {
            var conpermiso = MatrixUtilitarios.Utilitario.TienePermisoControladorAccion(HttpContext.Request.RouteValues["controller"].ToString(), HttpContext.Request.RouteValues["action"].ToString()); if (conpermiso == -1) { return RedirectToAction("Login", "Usuario"); }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ListarNotificacion(IDataTablesRequest requestModel)
        {
            var listanotificacion = new ListaNotificacion();
            int numeroPagina = (requestModel.Start / requestModel.Length) + 1;
            try
            {
                listanotificacion = await inotificacion.ListarNotificacionAsync(Convert.ToInt32(HttpContext.Session.GetString("IdArea")), Convert.ToInt32(HttpContext.Session.GetString("IdUsuario")), null, null, numeroPagina, requestModel.Length, requestModel.Search.Value);
                var totalCount = listanotificacion.paginacion.TotalRegistros;
                var data = listanotificacion.lista.Select(lq => new
                {
                    IdNotificacion = lq.IdNotificacion,
                    AdministradoNotificado = lq.administradonotificado.persona.NombreCompleto,
                    CatalogoCategoria = lq.catalogocategoria.Descripcion,
                    AsuntoNotificacion = lq.AsuntoNotificacion,
                    MensajeNotificacion = lq.MensajeNotificacion,
                    lq.NumeroNotificacion,
                    lq.NombreNumeroNotificacion,
                    lq.MensajeNotificacionHtml,
                    lq.periodo.IdPeriodo,
                    lq.CantidadArchivos,
                    lq.areanotificador.NombreArea,
                    lq.areanotificador.IdArea,
                    FechaHoraRegistroNotificacion = String.Format("{0:dd/MM/yyyy HH:mm}", lq.FechaHoraRegistroNotificacion),
                    FechaHoraNotificacionEnviada = String.Format("{0:dd/MM/yyyy HH:mm}", lq.FechaHoraNotificacionEnviada),
                    NotificacionEnviada = (lq.NotificacionEnviada == true ? "Si" : "No"),
                    FechaHoraRecepcionNotificacion = String.Format("{0:dd/MM/yyyy HH:mm}", lq.FechaHoraRecepcionNotificacion),
                    NotificacionRecibida = (lq.NotificacionRecibida == true ? "Si" : "No"),
                }).ToList();

                var jsonData = new { draw = requestModel.Draw, recordsFiltered = totalCount, recordsTotal = totalCount, data = data }; return Ok(jsonData);
            }
            catch (Exception ex)
            {
                return RedirectToAction("PaginaError", new { DescripcionError = "Hubo un error [" + ex.Message + "]" });
            }
        }
        [HttpPost]
        public async Task<IActionResult> RegistroNotificacion()
        {
            int IdNotificacion = Convert.ToInt32(Request.Form["IdNotificacion"]);

            var notificacion = new Notificacion();
            try
            {


                if (IdNotificacion > 0)
                {
                    notificacion = await inotificacion.ObtenerNotificacionAsync(IdNotificacion, Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));
                }
                return View(notificacion);
            }
            catch (Exception ex)
            {
                notificacion.mensaje.CodigoMensaje = 1;
                notificacion.mensaje.DescripcionMensaje = "Sucedió un error en la Vista: " + HttpContext.Request.RouteValues["action"].ToString() + " del controlador: " + HttpContext.Request.RouteValues["controller"].ToString();
                notificacion.mensaje.DescripcionMensajeSistema = ex.Message;
                return View(notificacion);
            }
        }
        [HttpPost]
        public async Task<IActionResult> GuardarRegistroNotificacion()
        {
            var notificacion = new Notificacion();
            try
            {
                int IdNotificacion = Convert.ToInt32(Request.Form["txtRegIdNotificacion"]);

                int IdAdministradoNotificado = Convert.ToInt32(Request.Form["txtIdAdministradoNotificado"]);
                int IdCatalogoCategoria = Convert.ToInt32(Request.Form["cmbCatalogoCategoria"]);
                string AsuntoNotificacion = Request.Form["txtAsuntoNotificacion"];
                string MensajeNotificacionHtml = Request.Form["txtMensajeNotificacionHtml"];
                string MensajeNotificacion = Request.Form["txtMensajeNotificacion"];
                DateTime? FechaHoraNotificacionEnviada = null;
                if (Request.Form["txtFechaHoraNotificacionEnviada"] != "")
                {
                    Convert.ToDateTime(Request.Form["txtFechaHoraNotificacionEnviada"], System.Globalization.CultureInfo.GetCultureInfo("es-ES").DateTimeFormat);
                }
                bool NotificacionEnviada = Convert.ToBoolean(Convert.ToInt32(Request.Form["optNotificacionEnviada"]));
                DateTime? FechaHoraRecepcionNotificacion = null;
                if (Request.Form["txtFechaHoraRecepcionNotificacion"] != "")
                {
                    Convert.ToDateTime(Request.Form["txtFechaHoraRecepcionNotificacion"], System.Globalization.CultureInfo.GetCultureInfo("es-ES").DateTimeFormat);
                }
                bool NotificacionRecibida = Convert.ToBoolean(Convert.ToInt32(Request.Form["optNotificacionRecibida"]));

                notificacion = await inotificacion.GuardaNotificacionAsync(
                                                        IdNotificacion,
                                                                IdAdministradoNotificado,
                                                                IdCatalogoCategoria,
                                                                AsuntoNotificacion,
                                                                MensajeNotificacion,
                                                                MensajeNotificacionHtml,
                                                                FechaHoraNotificacionEnviada,
                                                                NotificacionEnviada,
                                                                FechaHoraRecepcionNotificacion,
                                                                NotificacionRecibida,
                                                                Convert.ToInt32(HttpContext.Session.GetString("IdArea")),
                                                        Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));
                return Json(notificacion);
            }
            catch (Exception ex)
            {
                notificacion.mensaje.CodigoMensaje = 1;
                notificacion.mensaje.DescripcionMensaje = "Sucedió un error en la Vista: " + HttpContext.Request.RouteValues["action"].ToString() + " del controlador: " + HttpContext.Request.RouteValues["controller"].ToString();
                notificacion.mensaje.DescripcionMensajeSistema = ex.Message;
                return Json(notificacion);
            }
        }
        [HttpPost]
        public async Task<IActionResult> EliminarNotificacion()
        {
            int IdNotificacion = Convert.ToInt32(Request.Form["IdNotificacion"]);
            Utilitarios.Mensaje mensaje = new Utilitarios.Mensaje();
            try
            {
                mensaje = await inotificacion.EliminarNotificacionAsync(IdNotificacion, Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));
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
        [HttpPost]
        public async Task<IActionResult> EnviarNotificacion()
        {
            int IdNotificacion = Convert.ToInt32(Request.Form["IdNotificacion"]);
            Utilitarios.Mensaje mensaje = new Utilitarios.Mensaje();
            try
            {

                var notificacion = await inotificacion.ObtenerNotificacionAsync(IdNotificacion, Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));
                var MensajeConfirmacion = System.IO.File.ReadAllText(System.Configuration.ConfigurationManager.AppSettings["RutaArchivosPlantillas"] + "PlantillaCorreoEnvioNotificacionCasilla.html");
                MensajeConfirmacion = MensajeConfirmacion.Replace("NombreCompleto", "Estimad@ " + notificacion.administradonotificado.persona.NombreCompleto.ToUpper());
                MensajeConfirmacion = MensajeConfirmacion.Replace("LinkCasillaElectronica", System.Configuration.ConfigurationManager.AppSettings["HostAplicacion"] + "Notificacion/MisNotificaciones");
                MatrixUtilitarios.Utilitario.EnviarEmail(notificacion.administradonotificado.EmailNotificacion, MensajeConfirmacion, "Casilla Eléctronica", "", "");
                //ACA ESCRIBIR EL API PARA ENVIAR EL MENSAJE
                //
                if (!String.IsNullOrEmpty(notificacion.administradonotificado.NumeroCelularNotificacion))
                {
                    //ENVIAR EL MENSAJE DE TEXTO
                    ClienteServicioApi clienteApi = new ClienteServicioApi();                    
                    var mensajito = new Mensajito();
                    mensajito.PhoneNumber = notificacion.administradonotificado.NumeroCelularNotificacion;
                    mensajito.Message = "Usted tiene una notificacion en su casilla electronica de CMP, ingresar a " + System.Configuration.ConfigurationManager.AppSettings["HostAplicacion"];
                    await clienteApi.Post<Mensajito>("http://172.20.5.8:5000", "/api/sms/send", mensajito);
                }
                mensaje = await inotificacion.EnviarNotificacion(IdNotificacion, Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));
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
        [HttpGet]
        public async Task<ActionResult> DescargarNotificacionXls()
        {


            //string FechaInicial = Convert.ToDateTime(Request["FechaInicial"]).ToString("dd/MM/yyyy");
            string Titulo = "REPORTE DE NOTIFICACION";
            try
            {
                var dt = await inotificacion.DescargarNotificacion(Convert.ToInt32(HttpContext.Session.GetString("IdUsuario")));
                var stream = new MemoryStream();
                OfficeOpenXml.ExcelPackage pck = new OfficeOpenXml.ExcelPackage(stream);


                dt.Tables[0].TableName = "Reporte";
                dt.Tables[0].ExtendedProperties.Add("Titulo", Titulo);

                pck = MatrixUtilitarios.Utilitario.ExportarReporteV2(dt);
                stream.Position = 0;

                string excelName = "ReporteNotificacion" + DateTime.Now.ToString("ddMMyyyyHHmmss") + ".xlsx";
                return File(pck.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);

            }
            catch (Exception ex)
            {
                return RedirectToAction("PaginaError", new { DescripcionError = "Hubo un error [" + ex.Message + "]" });
            }
        }

        [HttpGet]
        public async Task<ActionResult> DescargarNotificacionPdf()
        {


            //string FechaInicial = Convert.ToDateTime(Request["FechaInicial"]).ToString("dd/MM/yyyy");         
            try
            {
                DataSet dataset = await inotificacion.DescargarNotificacion(Convert.ToInt32(HttpContext.Session.GetString("IdUsuario")));
                List<string> ListaTitulos = new List<string>();
                List<float[]> ListaAnchosColumnas = new List<float[]>();
                float[] anchosColumnasResumen = null;
                List<int> ListaAnchoPorcentajeTabla = new List<int>();
                List<bool> ListaConEcabezadoTabla = new List<bool>();
                List<bool> ListaConNroCorrelativoTabla = new List<bool>();

                foreach (DataTable tabla in dataset.Tables)
                {
                    anchosColumnasResumen = new float[tabla.Columns.Count + 1];
                    for (int i = 0; i < tabla.Columns.Count + 1; i++)
                    {
                        anchosColumnasResumen[i] = 1f;
                    }
                    ListaAnchosColumnas.Add(anchosColumnasResumen);// ancho de cada columna por cada tabla, si va hacerlo independiente sacarlo de acá
                    ListaAnchoPorcentajeTabla.Add(100);//ancho que abarca cada tabla, si lo va hacer independiente, sacarlo de acá
                    ListaTitulos.Add("");//titulo para cada tabla, si va hacer manual sacarlo de acá
                    ListaConEcabezadoTabla.Add(true);//encabezado para cada tabla, si va hacer manual sacarlo de acá
                    ListaConNroCorrelativoTabla.Add(true);//correlativo para cada tabla, si va hacer manual sacarlo de acá
                }
                UtilitarioPdf.GeneradorPDF gen = new UtilitarioPdf.GeneradorPDF();
                var memoryStream = gen.GenerarPDF(dataset,
                    "REPORTE DE NOTIFICACION",
                    ListaTitulos,
                    UtilitarioPdf.GeneradorPDF.TipoHorientacion.Horizontal,
                    ListaAnchosColumnas,
                    UtilitarioPdf.GeneradorPDF.TipoPagina.PaginaA4,
                    ListaAnchoPorcentajeTabla
                    //ListaConEcabezadoTabla,
                    //ListaConNroCorrelativoTabla
                    );

                return File(memoryStream.ToArray(), "application/pdf");
            }
            catch (Exception ex)
            {
                return RedirectToAction("PaginaError", new { DescripcionError = "Hubo un error [" + ex.Message + "]" });
            }
        }

        
    }
    public class Mensajito
    {
        public string PhoneNumber { get; set; }
        public string Message { get; set; }
    }
}
