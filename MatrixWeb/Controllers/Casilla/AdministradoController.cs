
using DataTables.AspNet.Core;
using Microsoft.AspNetCore.Http;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using MatrixService;
using System.Threading.Tasks;
using Casilla;
using System.Collections.Generic;
using MatrixWeb.UtilitarioPdf;
using System.Data;
using System.IO;
using Utilitarios.Servicios;

namespace MatrixWeb.Controllers
{
    public class AdministradoController : Controller
    {
        private readonly ISvcAdministrado iadministrado;
        public AdministradoController(ISvcAdministrado _iadministrado)
        {
            iadministrado = _iadministrado;
        }
        [HttpGet]
        public IActionResult IndexAdministrado()
        {
            var conpermiso = MatrixUtilitarios.Utilitario.TienePermisoControladorAccion(HttpContext.Request.RouteValues["controller"].ToString(), HttpContext.Request.RouteValues["action"].ToString()); if (conpermiso == -1) { return RedirectToAction("Login", "Usuario"); }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ListarAdministrado(IDataTablesRequest requestModel)
        {
            var listaadministrado = new ListaAdministrado();
            int numeroPagina = (requestModel.Start / requestModel.Length) + 1;
            try
            {
                listaadministrado = await iadministrado.ListarAdministradoAsync(Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")), null, null, numeroPagina, requestModel.Length, requestModel.Search.Value);
                var totalCount = listaadministrado.paginacion.TotalRegistros;
                var data = listaadministrado.lista.Select(lq => new
                {
                    IdAdministrado = lq.IdAdministrado,
                    Persona = lq.persona.NombreCompleto,
                    Usuario = lq.usuario.IdUsuario,
                    EmailNotificacion = lq.EmailNotificacion,
                    NumeroCelularNotificacion = lq.NumeroCelularNotificacion,
                    AsientoElectronico = lq.AsientoElectronico,
                    PartidaElectronica = lq.PartidaElectronica,
                    Activo = (lq.Activo == true ? "Si" : "No"),
                }).ToList();

                var jsonData = new { draw = requestModel.Draw, recordsFiltered = totalCount, recordsTotal = totalCount, data = data }; return Ok(jsonData);
            }
            catch (Exception ex)
            {
                return RedirectToAction("PaginaError", new { DescripcionError = "Hubo un error [" + ex.Message + "]" });
            }
        }
        [HttpPost]
        public async Task<IActionResult> RegistroAdministrado()
        {
            int IdAdministrado = Convert.ToInt32(Request.Form["IdAdministrado"]);

            var administrado = new Administrado();
            try
            {


                if (IdAdministrado > 0)
                {
                    administrado = await iadministrado.ObtenerAdministradoAsync(IdAdministrado, Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));
                }
                return View(administrado);
            }
            catch (Exception ex)
            {
                administrado.mensaje.CodigoMensaje = 1;
                administrado.mensaje.DescripcionMensaje = "Sucedió un error en la Vista: " + HttpContext.Request.RouteValues["action"].ToString() + " del controlador: " + HttpContext.Request.RouteValues["controller"].ToString();
                administrado.mensaje.DescripcionMensajeSistema = ex.Message;
                return View(administrado);
            }
        }

        [HttpGet]
        public IActionResult IndexAdministradoValidarDatos()
        {
            var conpermiso = MatrixUtilitarios.Utilitario.TienePermisoControladorAccion(HttpContext.Request.RouteValues["controller"].ToString(), HttpContext.Request.RouteValues["action"].ToString()); if (conpermiso == -1) { return RedirectToAction("Login", "Usuario"); }

            ViewBag.IdAdministrado = Convert.ToInt32(HttpContext.Session.GetString("IdAdministrado"));

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RegistroAdministradoValidarDatos()
        {
            int IdAdministrado = Convert.ToInt32(Request.Form["IdAdministrado"]);

            var administrado = new Administrado();
            try
            {


                if (IdAdministrado > 0)
                {
                    administrado = await iadministrado.ObtenerAdministradoAsync(IdAdministrado, Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));
                }
                return View(administrado);
            }
            catch (Exception ex)
            {
                administrado.mensaje.CodigoMensaje = 1;
                administrado.mensaje.DescripcionMensaje = "Sucedió un error en la Vista: " + HttpContext.Request.RouteValues["action"].ToString() + " del controlador: " + HttpContext.Request.RouteValues["controller"].ToString();
                administrado.mensaje.DescripcionMensajeSistema = ex.Message;
                return View(administrado);
            }
        }


        [HttpPost]
        public async Task<IActionResult> GuardarRegistroAdministrado()
        {
            var administrado = new Administrado();
            try
            {
				int IdAdministrado = Convert.ToInt32(HttpContext.Session.GetString("IdAdministrado"));

                int IdPersona = Convert.ToInt32(HttpContext.Session.GetString("IdPersona"));// Convert.ToInt32(Request.Form["txtIdPersona"]);
                int IdUsuario = Convert.ToInt32(HttpContext.Session.GetString("IdUsuario"));// Convert.ToInt32(Request.Form["cmbUsuario"]);
                string EmailNotificacion = Request.Form["txtEmailNotificacion"];
                string NumeroCelularNotificacion = Request.Form["txtNumeroCelularNotificacion"];
                string AsientoElectronico = "";// Request.Form["txtAsientoElectronico"];
                string PartidaElectronica = "";//Request.Form["txtPartidaElectronica"];
                bool Activo = false;// Convert.ToBoolean(Convert.ToInt32(Request.Form["optActivo"]));

				string CodigoTelefonoConfirmacion = Request.Form["txtCodigoTelefonoConfirmacion"];
				string CodigoCorreoConfirmacion = Request.Form["txtCodigoCorreoConfirmacion"];
				
                int CodigoCorreoValidar = Convert.ToInt32(Request.Form["txtCodigoCorreoValidar"]);
                int CodigoTelefonoValidar = Convert.ToInt32(Request.Form["txtCodigoTelefonoValidar"]);




				administrado = await iadministrado.GuardaAdministradoAsync(
                                                        IdAdministrado,
                                                                IdPersona,
                                                                IdUsuario,
                                                                EmailNotificacion,
                                                                NumeroCelularNotificacion,
                                                                AsientoElectronico,
                                                                PartidaElectronica,
                                                                Activo,
																CodigoTelefonoConfirmacion,
																CodigoCorreoConfirmacion,
																CodigoCorreoValidar,
																CodigoTelefonoValidar,
														Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));

                return Json(administrado);
            }
            catch (Exception ex)
            {
                administrado.mensaje.CodigoMensaje = 1;
                administrado.mensaje.DescripcionMensaje = "Sucedió un error en la Vista: " + HttpContext.Request.RouteValues["action"].ToString() + " del controlador: " + HttpContext.Request.RouteValues["controller"].ToString();
                administrado.mensaje.DescripcionMensajeSistema = ex.Message;
                return Json(administrado);
            }
        }
        [HttpPost]
        public async Task<IActionResult> EliminarAdministrado()
        {
            int IdAdministrado = Convert.ToInt32(Request.Form["IdAdministrado"]);
            Utilitarios.Mensaje mensaje = new Utilitarios.Mensaje();
            try
            {
                mensaje = await iadministrado.EliminarAdministradoAsync(IdAdministrado, Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));
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
        public async Task<ActionResult> DescargarAdministradoXls()
        {


            //string FechaInicial = Convert.ToDateTime(Request["FechaInicial"]).ToString("dd/MM/yyyy");
            string Titulo = "REPORTE DE ADMINISTRADO";
            try
            {
                var dt = await iadministrado.DescargarAdministrado(Convert.ToInt32(HttpContext.Session.GetString("IdUsuario")));
                var stream = new MemoryStream();
                OfficeOpenXml.ExcelPackage pck = new OfficeOpenXml.ExcelPackage(stream);


                dt.Tables[0].TableName = "Reporte";
                dt.Tables[0].ExtendedProperties.Add("Titulo", Titulo);

                pck = MatrixUtilitarios.Utilitario.ExportarReporteV2(dt);
                stream.Position = 0;

                string excelName = "ReporteAdministrado" + DateTime.Now.ToString("ddMMyyyyHHmmss") + ".xlsx";
                return File(pck.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);

            }
            catch (Exception ex)
            {
                return RedirectToAction("PaginaError", new { DescripcionError = "Hubo un error [" + ex.Message + "]" });
            }
        }
        /*
        [HttpGet]
        public async Task<ActionResult> DescargarAdministradoPdf()
        {


            //string FechaInicial = Convert.ToDateTime(Request["FechaInicial"]).ToString("dd/MM/yyyy");         
            try
            {
                DataSet dataset = await iadministrado.DescargarAdministrado(Convert.ToInt32(HttpContext.Session.GetString("IdUsuario")));
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
                var memoryStream = GeneradorPDF.GenerarPDF(
                    dataset,
                    "REPORTE DE ADMINISTRADO",
                    ListaTitulos, 
                    GeneradorPDF.TipoHorientacion.Horizontal,
                    ListaAnchosColumnas, 
                    GeneradorPDF.TipoPagina.PaginaA4,
                    ListaAnchoPorcentajeTabla,
                    ListaConEcabezadoTabla,
                    ListaConNroCorrelativoTabla
                    );

                return File(memoryStream.ToArray(), "application/pdf");
            }
            catch (Exception ex)
            {
                return RedirectToAction("PaginaError", new { DescripcionError = "Hubo un error [" + ex.Message + "]" });
            }
        }
        */
		[HttpGet]
		public async Task<IActionResult> ListarAdministradoPorAutoComplete(string NombreCompleto = "")
        {
            Casilla.ListaAdministrado listaAdministrado = new Casilla.ListaAdministrado();
            Casilla.Administrado Administrado = new Casilla.Administrado();

            try
            {

                listaAdministrado = await iadministrado.ListarAdministradoPorAutoComplete(Convert.ToInt32(HttpContext.Session.GetString("IdUsuario")),NombreCompleto);


                if (listaAdministrado.mensaje.CodigoMensaje > 0)
                {
                    return RedirectToAction("PaginaError", new { DescripcionError = listaAdministrado.mensaje.DescripcionMensaje });
                }
                var totalCount = listaAdministrado.paginacion.TotalRegistros;
                var data = listaAdministrado.lista.Select(lq => new
                {
                    NombreCompleto = lq.persona.NombreCompleto,
                    IdAdministrado = lq.IdAdministrado
                }).ToList();

                return Json(data);
            }
            catch (Exception ex)
            {
                return RedirectToAction("PaginaError", new { DescripcionError = "Hubo un error [" + ex.Message + "]" });
            }
        }

		[HttpPost]
		public async Task<IActionResult> EnviarCodigoCorreoConfirmacion()
		{
			
			int IdAdministrado = Convert.ToInt32(HttpContext.Session.GetString("IdAdministrado"));
			string EmailNotificacion = Convert.ToString(Request.Form["EmailNotificacion"]);

			Utilitarios.Mensaje mensaje = new Utilitarios.Mensaje();
			Utilitarios.Mensaje mensajeGenerarlaveConfirmacion = new Utilitarios.Mensaje();

			string CodigoCorreoConfirmacion = MatrixUtilitarios.Utilitario.GetRandomCodigo(6);
			var administrado = new Administrado();

			try
			{
				mensajeGenerarlaveConfirmacion = await iadministrado.GenerarCodigoCorreoConfirmacion(IdAdministrado, CodigoCorreoConfirmacion, EmailNotificacion, Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));


				administrado = await iadministrado.ObtenerAdministradoAsync(IdAdministrado, Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));
				var MensajeConfirmacion = System.IO.File.ReadAllText(System.Configuration.ConfigurationManager.AppSettings["RutaArchivosPlantillas"] + "PlantillaCorreoEnvioCodigoValidacionCorreo.html");
				MensajeConfirmacion = MensajeConfirmacion.Replace("NombreCompleto", "Estimad@ " + administrado.persona.NombreCompleto.ToUpper());
				MensajeConfirmacion = MensajeConfirmacion.Replace("CodigoCorreo", CodigoCorreoConfirmacion);
				MensajeConfirmacion = MensajeConfirmacion.Replace("LinkCasillaElectronica", System.Configuration.ConfigurationManager.AppSettings["HostAplicacion"] + "Notificacion/MisNotificaciones");
				int Confirmacion = MatrixUtilitarios.Utilitario.EnviarEmailConfirmacion(EmailNotificacion, MensajeConfirmacion, "Casilla Eléctronica", "", "");

				if (Confirmacion == 1)
				{
					mensaje = mensajeGenerarlaveConfirmacion;
				}
				else
				{
					mensaje.CodigoMensaje = 1;
					mensaje.DescripcionMensaje = "Error en el envio";

				}

				//  mensaje = await inotificacion.EnviarNotificacion(IdNotificacion, Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));
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
        public async Task<IActionResult> EnviarCodigoCorreoConfirmacionLogin()
        {

            int IdVerificacion  = Convert.ToInt32(HttpContext.Session.GetString("IdVerificacion "));
            string EmailNotificacion = Convert.ToString(Request.Form["EmailNotificacion"]);
            string NumeroCelularNotificacion = Convert.ToString(Request.Form["NumeroCelularNotificacion"]);

            Utilitarios.Mensaje mensaje = new Utilitarios.Mensaje();
            Utilitarios.Mensaje mensajeGenerarlaveConfirmacion = new Utilitarios.Mensaje();

            string CodigoCorreoConfirmacion = MatrixUtilitarios.Utilitario.GetRandomCodigo(6);
            string CodigoTelefonoConfirmacion = MatrixUtilitarios.Utilitario.GetRandomCodigo(6);
            var administrado = new Administrado();

            try
            {
               

              //  administrado = await iadministrado.ObtenerAdministradoAsync(IdVerificacion , Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));
                var MensajeConfirmacion = System.IO.File.ReadAllText(System.Configuration.ConfigurationManager.AppSettings["RutaArchivosPlantillas"] + "PlantillaCorreoEnvioCodigoValidacionCorreo.html");
                MensajeConfirmacion = MensajeConfirmacion.Replace("NombreCompleto", "Estimad@ Usuario " );
                MensajeConfirmacion = MensajeConfirmacion.Replace("CodigoCorreo", CodigoCorreoConfirmacion);
                MensajeConfirmacion = MensajeConfirmacion.Replace("LinkCasillaElectronica", System.Configuration.ConfigurationManager.AppSettings["HostAplicacion"] + "Notificacion/MisNotificaciones");
                int Confirmacion = MatrixUtilitarios.Utilitario.EnviarEmailConfirmacion(EmailNotificacion, MensajeConfirmacion, "Casilla Eléctronica", "", "");


                ClienteServicioApi clienteApi = new ClienteServicioApi();
                var mensajito = new Mensajito();
                mensajito.PhoneNumber = NumeroCelularNotificacion;
                //mensajito.Message = "Clave de confirmacion de telefono:" + CodigoCorreoConfirmacion;
                mensajito.Message = "Se envio un codigo de verificación de telefono: " + CodigoTelefonoConfirmacion + " ,ingresar a " + System.Configuration.ConfigurationManager.AppSettings["HostAplicacion"];
                await clienteApi.Post<Mensajito>("http://172.20.5.8:5000", "/api/sms/send", mensajito);


                if (Confirmacion == 1)
                {
                    mensaje = await iadministrado.GenerarCodigoCorreoConfirmacionLogin(IdVerificacion, CodigoCorreoConfirmacion, EmailNotificacion, CodigoTelefonoConfirmacion, NumeroCelularNotificacion, Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));

                }
                else
                {
                    mensaje.CodigoMensaje = 1;
                    mensaje.DescripcionMensaje = "Error en el envio";

                }

                //  mensaje = await inotificacion.EnviarNotificacion(IdNotificacion, Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));
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
        public async Task<IActionResult> EnviarCodigoCorreoValidacionLogin()
        {

            int IdVerificacion = Convert.ToInt32(Request.Form["IdVerificacion"]);
            string EmailNotificacion = Convert.ToString(Request.Form["EmailNotificacion"]);
            string NumeroCelularNotificacion = Convert.ToString(Request.Form["NumeroCelularNotificacion"]);


            string CodigoCorreoConfirmacion = Convert.ToString(Request.Form["CodigoCorreoConfirmacion"]);
            string CodigoTelefonoConfirmacion = Convert.ToString(Request.Form["CodigoTelefonoConfirmacion"]);


            Utilitarios.Mensaje mensaje = new Utilitarios.Mensaje();


            var administrado = new Administrado();

            try
            {


                //if (Confirmacion == 1)
                {
                    mensaje = await iadministrado.GenerarCodigoCorreoValidacionLogin(IdVerificacion, CodigoCorreoConfirmacion, EmailNotificacion, CodigoTelefonoConfirmacion, NumeroCelularNotificacion, Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));

                }
              

                //  mensaje = await inotificacion.EnviarNotificacion(IdNotificacion, Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));
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
		public async Task<IActionResult> EnviarCodigoTelefonoConfirmacion()
		{
			
			int IdAdministrado = Convert.ToInt32(HttpContext.Session.GetString("IdAdministrado"));
			string NumeroCelularNotificacion = Convert.ToString(Request.Form["NumeroCelularNotificacion"]);
			Utilitarios.Mensaje mensaje = new Utilitarios.Mensaje();
			Utilitarios.Mensaje mensajeGenerarlaveConfirmacion = new Utilitarios.Mensaje();

			string CodigoCorreoConfirmacion = MatrixUtilitarios.Utilitario.GetRandomCodigo(6);
			var administrado = new Administrado();

			try
			{
				mensajeGenerarlaveConfirmacion = await iadministrado.GenerarCodigoTelefonoConfirmacion(IdAdministrado, CodigoCorreoConfirmacion, NumeroCelularNotificacion, Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));

				administrado = await iadministrado.ObtenerAdministradoAsync(IdAdministrado, Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));

				ClienteServicioApi clienteApi = new ClienteServicioApi();
				var mensajito = new Mensajito();
				mensajito.PhoneNumber = NumeroCelularNotificacion;
				//mensajito.Message = "Clave de confirmacion de telefono:" + CodigoCorreoConfirmacion;
				mensajito.Message = "Se envio un codigo de verificación de telefono: " + CodigoCorreoConfirmacion+ " ,ingresar a " + System.Configuration.ConfigurationManager.AppSettings["HostAplicacion"];
				await clienteApi.Post<Mensajito>("http://172.20.5.8:5000", "/api/sms/send", mensajito);



				{
					mensaje.CodigoMensaje = 0;
					mensaje.DescripcionMensaje = "Mensaje enviado";

				}

				//  mensaje = await inotificacion.EnviarNotificacion(IdNotificacion, Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));
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
