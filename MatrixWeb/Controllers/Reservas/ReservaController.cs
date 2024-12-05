
using DataTables.AspNet.Core;
using Microsoft.AspNetCore.Http;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using MatrixService;
using System.Threading.Tasks;
using Reservas;
using System.IO;
namespace MatrixWeb.Controllers
{
    public class ReservaController : Controller
    {
        private readonly ISvcReserva ireserva;
        public ReservaController(ISvcReserva _ireserva)
        {
            ireserva = _ireserva;
        }
        [HttpGet]
        public IActionResult IndexReserva()
        {
            var conpermiso = MatrixUtilitarios.Utilitario.TienePermisoControladorAccion(HttpContext.Request.RouteValues["controller"].ToString(), HttpContext.Request.RouteValues["action"].ToString());
            if (conpermiso == -1)
            {
                return RedirectToAction("Login", "Usuario");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ListarReserva(IDataTablesRequest requestModel)
        {
            var listareserva = new ListaReserva();
            int numeroPagina = (requestModel.Start / requestModel.Length) + 1;
            try
            {
                listareserva = await ireserva.ListarReservaAsync(Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")), null, null, numeroPagina, requestModel.Length, requestModel.Search.Value);
                var totalCount = listareserva.paginacion.TotalRegistros;
                var data = listareserva.lista.Select(lq => new
                {
                    IdReserva = lq.IdReserva,
                    Solicitud = lq.solicitud.IdSolicitud,
                    Sala = lq.sala.IdSala,
                    FechaDesdeReserva = lq.FechaDesdeReserva,
                    FechaHastaReserva = lq.FechaHastaReserva,
                    HoraInicio = lq.HoraInicio,
                    HoraFin = lq.HoraFin,
                    Observaciones = lq.Observaciones,
                }).ToList();

                var jsonData = new { draw = requestModel.Draw, recordsFiltered = totalCount, recordsTotal = totalCount, data = data }; return Ok(jsonData);
            }
            catch (Exception ex)
            {
                return RedirectToAction("PaginaError", new { DescripcionError = "Hubo un error [" + ex.Message + "]" });
            }
        }
        [HttpPost]
        public async Task<IActionResult> RegistroReserva()
        {
            int IdReserva = Convert.ToInt32(Request.Form["IdReserva"]);

            var reserva = new Reserva();
            try
            {


                if (IdReserva > 0)
                {
                    reserva = await ireserva.ObtenerReservaAsync(IdReserva, Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));
                }
                return View(reserva);
            }
            catch (Exception ex)
            {
                reserva.mensaje.CodigoMensaje = 1;
                reserva.mensaje.DescripcionMensaje = "Sucedió un error en la Vista: " + HttpContext.Request.RouteValues["action"].ToString() + " del controlador: " + HttpContext.Request.RouteValues["controller"].ToString();
                reserva.mensaje.DescripcionMensajeSistema = ex.Message;
                return View(reserva);
            }
        }
        [HttpPost]
        public async Task<IActionResult> GuardarRegistroReserva()
        {
            var reserva = new Reserva();
            try
            {
                int IdReserva = Convert.ToInt32(Request.Form["txtRegIdReserva"]);
                int IdSolicitud = Convert.ToInt32(Request.Form["cmbSolicitud"]);

                int IdSala = Convert.ToInt32(Request.Form["cmbSala"]);
                string FechaDesdeReserva = Request.Form["txtFechaDesdeReserva"];
                string FechaHastaReserva = Request.Form["txtFechaHastaReserva"];
                string HoraInicio = Request.Form["txtHoraInicio"];
                string HoraFin = Request.Form["txtHoraFin"];
                string Observaciones = Request.Form["txtObservaciones"];


                reserva = await ireserva.GuardaReservaAsync(
                                                        IdReserva,
                                                                IdSolicitud,
                                                                IdSala,
                                                                FechaDesdeReserva,
                                                                FechaHastaReserva,
                                                                HoraInicio,
                                                                HoraFin,
                                                                Observaciones,
                                                        Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));

                return Json(reserva);
            }
            catch (Exception ex)
            {
                reserva.mensaje.CodigoMensaje = 1;
                reserva.mensaje.DescripcionMensaje = "Sucedió un error en la Vista: " + HttpContext.Request.RouteValues["action"].ToString() + " del controlador: " + HttpContext.Request.RouteValues["controller"].ToString();
                reserva.mensaje.DescripcionMensajeSistema = ex.Message;
                return Json(reserva);
            }
        }
        [HttpPost]
        public async Task<IActionResult> EliminarReserva()
        {
            int IdReserva = Convert.ToInt32(Request.Form["IdReserva"]);
            Utilitarios.Mensaje mensaje = new Utilitarios.Mensaje();
            try
            {
                mensaje = await ireserva.EliminarReservaAsync(IdReserva, Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));
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
        public async Task<ActionResult> DescargarReservaXls()
        {


            //string FechaInicial = Convert.ToDateTime(Request["FechaInicial"]).ToString("dd/MM/yyyy");
            string Titulo = "REPORTE DE RESERVA";
            try
            {
                var dt = await ireserva.DescargarReserva(Convert.ToInt32(HttpContext.Session.GetString("IdUsuario")));
                var stream = new MemoryStream();
                OfficeOpenXml.ExcelPackage pck = new OfficeOpenXml.ExcelPackage(stream);


                dt.Tables[0].TableName = "Reporte";
                dt.Tables[0].ExtendedProperties.Add("Titulo", Titulo);

                pck = MatrixUtilitarios.Utilitario.ExportarReporteV2(dt);
                stream.Position = 0;

                string excelName = "ReporteReserva" + DateTime.Now.ToString("ddMMyyyyHHmmss") + ".xlsx";
                return File(pck.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);

            }
            catch (Exception ex)
            {
                return RedirectToAction("PaginaError", new { DescripcionError = "Hubo un error [" + ex.Message + "]" });
            }
        }
        //[HttpGet]
        //public async Task<ActionResult> DescargarReservaPdf()
        //{


        //    //string FechaInicial = Convert.ToDateTime(Request["FechaInicial"]).ToString("dd/MM/yyyy");         
        //    try
        //    {
        //        DataSet dataset = await ireserva.DescargarReserva(Convert.ToInt32(HttpContext.Session.GetString("IdUsuario")));
        //        List<string> ListaTitulos = new List<string>();
        //        List<float[]> ListaAnchosColumnas = new List<float[]>();
        //        float[] anchosColumnasResumen = null;
        //        List<int> ListaAnchoPorcentajeTabla = new List<int>();
        //        List<bool> ListaConEcabezadoTabla = new List<bool>();
        //        List<bool> ListaConNroCorrelativoTabla = new List<bool>();

        //        foreach (DataTable tabla in dataset.Tables)
        //        {
        //            anchosColumnasResumen = new float[tabla.Columns.Count + 1];
        //            for (int i = 0; i < tabla.Columns.Count + 1; i++)
        //            {
        //                anchosColumnasResumen[i] = 1f;
        //            }
        //            ListaAnchosColumnas.Add(anchosColumnasResumen);// ancho de cada columna por cada tabla, si va hacerlo independiente sacarlo de acá
        //            ListaAnchoPorcentajeTabla.Add(100);//ancho que abarca cada tabla, si lo va hacer independiente, sacarlo de acá
        //            ListaTitulos.Add("");//titulo para cada tabla, si va hacer manual sacarlo de acá
        //            ListaConEcabezadoTabla.Add(true);//encabezado para cada tabla, si va hacer manual sacarlo de acá
        //            ListaConNroCorrelativoTabla.Add(true);//correlativo para cada tabla, si va hacer manual sacarlo de acá
        //        }
        //        var memoryStream = GeneradorPDF.GenerarPDF(dataset,
        //            "REPORTE DE RESERVA",
        //            ListaTitulos, GeneradorPDF.TipoHorientacion.Horizontal,
        //            ListaAnchosColumnas, GeneradorPDF.TipoPagina.PaginaA4,
        //            ListaAnchoPorcentajeTabla,
        //            ListaConEcabezadoTabla,
        //            ListaConNroCorrelativoTabla);

        //        return File(memoryStream.ToArray(), "application/pdf");
        //    }
        //    catch (Exception ex)
        //    {
        //        return RedirectToAction("PaginaError", new { DescripcionError = "Hubo un error [" + ex.Message + "]" });
        //    }
        //}
    }
}
