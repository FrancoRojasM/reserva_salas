
using DataTables.AspNet.Core;
using Microsoft.AspNetCore.Http;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using MatrixService;
using System.Threading.Tasks;
using Reservas;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;
namespace MatrixWeb.Controllers
{
    public class SolicitudController : Controller
    {



        private readonly ISvcSolicitud isolicitud;
        private readonly IConfiguration configuration;


        public SolicitudController(ISvcSolicitud _isolicitud, IConfiguration _configuration)
        {
            isolicitud = _isolicitud;
            configuration = _configuration;

        }
        [HttpGet]
        public IActionResult IndexSolicitud()
        {
            var conpermiso = MatrixUtilitarios.Utilitario.TienePermisoControladorAccion(HttpContext.Request.RouteValues["controller"].ToString(), HttpContext.Request.RouteValues["action"].ToString());
            if (conpermiso == -1)
            {
                return RedirectToAction("Login", "Usuario");
            }
            return View();
        }

      

        [AllowAnonymous]
        [HttpGet]
        public IActionResult SolicitudGeneral()
        {
            //var conpermiso = MatrixUtilitarios.Utilitario.TienePermisoControladorAccion(HttpContext.Request.RouteValues["controller"].ToString(), HttpContext.Request.RouteValues["action"].ToString());
            //if (conpermiso == -1)
            //{
            //    return RedirectToAction("Login", "Usuario");
            //}
            return View();
        }


        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> EventosInternos()
        {
            Solicitud solicitud = new Solicitud();

            try
            {
                // Obtener el Id de la solicitud si está presente en la sesión o como parámetro
                int IdSolicitud = Convert.ToInt32(Request.Query["IdSolicitud"]);

                // Si hay un IdSolicitud válido, obtenemos los datos asociados
                if (IdSolicitud > 0)
                {
                    // Aquí puedes usar la lógica que tienes en el controlador `RegistroSolicitud` 
                    // para obtener la solicitud desde la base de datos o el servicio correspondiente.
                    solicitud = await isolicitud.ObtenerSolicitudAsync(
                        IdSolicitud,
                        Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") ?? "0")
                    );
                }

                // Pasar el modelo (solicitud) a la vista
                return View(solicitud);
            }
            catch (Exception ex)
            {
                // Manejo de errores
                solicitud.mensaje.CodigoMensaje = 1;
                solicitud.mensaje.DescripcionMensaje = "Sucedió un error en la Vista: "
                    + HttpContext.Request.RouteValues["action"].ToString()
                    + " del controlador: " + HttpContext.Request.RouteValues["controller"].ToString();
                solicitud.mensaje.DescripcionMensajeSistema = ex.Message;

                // Pasar el modelo con el error a la vista
                return View(solicitud);
            }
        }


        
        [HttpGet]
        public IActionResult ListaSolicitudes()
        {
            var conpermiso = MatrixUtilitarios.Utilitario.TienePermisoControladorAccion(HttpContext.Request.RouteValues["controller"].ToString(), HttpContext.Request.RouteValues["action"].ToString());
            if (conpermiso == -1)
            {
                return RedirectToAction("Login", "Usuario");
            }
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> ListarSolicitud(IDataTablesRequest requestModel)
        {
            // Imprimir en la consola para depurar
            
            var listasolicitud = new ListaSolicitud();
            int numeroPagina = (requestModel.Start / requestModel.Length) + 1;
            try
            {
                listasolicitud = await isolicitud.ListarSolicitudAsync(Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")), null, null, numeroPagina=1, 10, requestModel.Search.Value);
                var totalCount = listasolicitud.paginacion.TotalRegistros;
                var data = listasolicitud.lista.Select(lq => new
                {
                    IdSolicitud = lq.IdSolicitud,
                    CatalogoConsejoRegional = lq.catalogoconsejoregional.Descripcion,
                    CatalogoSecretaria = lq.catalogosecretaria.Descripcion,
                    CatalogoComite = lq.catalogocomite.Descripcion,
                    NombreEvento = lq.NombreEvento,
                    NumeroParticipantes = lq.NumeroParticipantes,
                    FechaDesde = lq.FechaDesde,
                    FechaHasta = lq.FechaHasta,
                    NumeroDias = lq.NumeroDias,
                    HoraInicio = lq.HoraInicio,
                    HoraFin = lq.HoraFin,
                    NumeroAuditorios = lq.NumeroAuditorios,
                    ResponsableEvento = lq.ResponsableEvento,
                    TelefonoContacto = lq.TelefonoContacto,
                    CorreoContacto = lq.CorreoContacto,
                    Observaciones = lq.Observaciones,
                    SalaAsignada = lq.salaasignada.IdSala,
                    CatalogoEstadoSolicitud = lq.catalogoestadosolicitud.Descripcion,
                }).ToList();

                var jsonData = new { draw = requestModel.Draw, recordsFiltered = totalCount, recordsTotal = totalCount, data = data }; return Ok(jsonData);
            }
            catch (Exception ex)
            {
                return RedirectToAction("PaginaError", new { DescripcionError = "Hubo un error [" + ex.Message + "]" });
            }
        }

        //mi controller

        [HttpPost]
        public async Task<IActionResult> ListarSolicitudes()
        {
            try
            {
                // Obtener la cadena de conexión desde la configuración
                var connectionString = configuration.GetConnectionString("ConexionSqlGeneral");

                // Lista para almacenar los resultados de la consulta
                var solicitudes = new List<object>();

                // Establecer la conexión a la base de datos
                using (var connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    // Crear el comando para ejecutar el procedimiento almacenado
                    using (var command = new SqlCommand("[Reservas].[paListarSolicitudes]", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Ejecutar el lector y procesar los resultados
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                solicitudes.Add(new
                                {
                                    IdSolicitud = reader["IdSolicitud"],
                                    ConsejoRegional = reader["IdCatalogoConsejoRegional"],
                                    NombreEvento = reader["NombreEvento"],
                                    FechaDesde = reader["FechaDesde"],
                                    FechaHasta = reader["FechaHasta"]
                                });
                            }
                        }
                    }
                }

                // Preparar la respuesta para DataTables
                var response = new
                {
                    draw = 1,  // Número de solicitud de DataTable
                    recordsFiltered = solicitudes.Count, // Total después de aplicar filtro
                    recordsTotal = solicitudes.Count,    // Total de registros sin filtro
                    data = solicitudes // Los registros que vamos a mostrar
                };

                return Json(response);
            }
            catch (Exception ex)
            {
                // En caso de error, devolver un mensaje de error en formato JSON
                return Json(new { error = "Ocurrió un error en el servidor: " + ex.Message });
            }
        }





        [HttpPost]
        public async Task<IActionResult> RegistroSolicitud()
        {
            int IdSolicitud = Convert.ToInt32(Request.Form["IdSolicitud"]);

            var solicitud = new Solicitud();
            try
            {


                if (IdSolicitud > 0)
                {
                    solicitud = await isolicitud.ObtenerSolicitudAsync(IdSolicitud, Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));
                }
                return View(solicitud);
            }
            catch (Exception ex)
            {
                solicitud.mensaje.CodigoMensaje = 1;
                solicitud.mensaje.DescripcionMensaje = "Sucedió un error en la Vista: " + HttpContext.Request.RouteValues["action"].ToString() + " del controlador: " + HttpContext.Request.RouteValues["controller"].ToString();
                solicitud.mensaje.DescripcionMensajeSistema = ex.Message;
                return View(solicitud);
            }
        }
        [HttpPost]
        public async Task<IActionResult> GuardarRegistroSolicitud()
        {
            var solicitud = new Solicitud();
            try
            {
                int IdSolicitud = Convert.ToInt32(Request.Form["txtRegIdSolicitud"]);
                int IdCatalogoConsejoRegional = Convert.ToInt32(Request.Form["cmbCatalogoConsejoRegional"]);
                int IdCatalogoSecretaria = Convert.ToInt32(Request.Form["cmbCatalogoSecretaria"]);
                int IdCatalogoComite = Convert.ToInt32(Request.Form["cmbCatalogoComite"]);
                string NombreEvento = Request.Form["txtNombreEvento"];
                int NumeroParticipantes = Convert.ToInt32(Request.Form["txtNumeroParticipantes"]);
                string FechaDesde = Request.Form["txtFechaDesde"];
                string FechaHasta = Request.Form["txtFechaHasta"];
                int NumeroDias = Convert.ToInt32(Request.Form["txtNumeroDias"]);
                string HoraInicio = Request.Form["txtHoraInicio"];
                string HoraFin = Request.Form["txtHoraFin"];
                int NumeroAuditorios = Convert.ToInt32(Request.Form["txtNumeroAuditorios"]);
                string ResponsableEvento = Request.Form["txtResponsableEvento"];
                string TelefonoContacto = Request.Form["txtTelefonoContacto"];
                string CorreoContacto = Request.Form["txtCorreoContacto"];
                string Observaciones = Request.Form["txtObservaciones"];
                int IdSalaAsignada = Convert.ToInt32(Request.Form["cmbSalaAsignada"]);
                int IdCatalogoEstadoSolicitud = Convert.ToInt32(Request.Form["cmbCatalogoEstadoSolicitud"]);


                solicitud = await isolicitud.GuardaSolicitudAsync(
                                                        IdSolicitud,
                                                                IdCatalogoConsejoRegional,
                                                                IdCatalogoSecretaria,
                                                                IdCatalogoComite,
                                                                NombreEvento,
                                                                NumeroParticipantes,
                                                                FechaDesde,
                                                                FechaHasta,
                                                                NumeroDias,
                                                                HoraInicio,
                                                                HoraFin,
                                                                NumeroAuditorios,
                                                                ResponsableEvento,
                                                                TelefonoContacto,
                                                                CorreoContacto,
                                                                Observaciones,
                                                                IdSalaAsignada,
                                                                IdCatalogoEstadoSolicitud,
                                                        Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));

                return Json(solicitud);
            }
            catch (Exception ex)
            {
                solicitud.mensaje.CodigoMensaje = 1;
                solicitud.mensaje.DescripcionMensaje = "Sucedió un error en la Vista: " + HttpContext.Request.RouteValues["action"].ToString() + " del controlador: " + HttpContext.Request.RouteValues["controller"].ToString();
                solicitud.mensaje.DescripcionMensajeSistema = ex.Message;
                return Json(solicitud);
            }
        }
        [HttpPost]
        public async Task<IActionResult> EliminarSolicitud()
        {
            int IdSolicitud = Convert.ToInt32(Request.Form["IdSolicitud"]);
            Utilitarios.Mensaje mensaje = new Utilitarios.Mensaje();
            try
            {
                mensaje = await isolicitud.EliminarSolicitudAsync(IdSolicitud, Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));
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
        public async Task<ActionResult> DescargarSolicitudXls()
        {


            //string FechaInicial = Convert.ToDateTime(Request["FechaInicial"]).ToString("dd/MM/yyyy");
            string Titulo = "REPORTE DE SOLICITUD";
            try
            {
                var dt = await isolicitud.DescargarSolicitud(Convert.ToInt32(HttpContext.Session.GetString("IdUsuario")));
                var stream = new MemoryStream();
                OfficeOpenXml.ExcelPackage pck = new OfficeOpenXml.ExcelPackage(stream);


                dt.Tables[0].TableName = "Reporte";
                dt.Tables[0].ExtendedProperties.Add("Titulo", Titulo);

                pck = MatrixUtilitarios.Utilitario.ExportarReporteV2(dt);
                stream.Position = 0;

                string excelName = "ReporteSolicitud" + DateTime.Now.ToString("ddMMyyyyHHmmss") + ".xlsx";
                return File(pck.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);

            }
            catch (Exception ex)
            {
                return RedirectToAction("PaginaError", new { DescripcionError = "Hubo un error [" + ex.Message + "]" });
            }
        }
        //[HttpGet]
        //public async Task<ActionResult> DescargarSolicitudPdf()
        //{


        //    //string FechaInicial = Convert.ToDateTime(Request["FechaInicial"]).ToString("dd/MM/yyyy");         
        //    try
        //    {
        //        DataSet dataset = await isolicitud.DescargarSolicitud(Convert.ToInt32(HttpContext.Session.GetString("IdUsuario")));
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
        //            "REPORTE DE SOLICITUD",
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
