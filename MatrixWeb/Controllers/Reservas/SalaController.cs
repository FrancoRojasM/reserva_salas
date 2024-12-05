
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
	public class SalaController : Controller
	{
		private readonly ISvcSala isala;
		public SalaController(ISvcSala _isala)
		{
			isala = _isala;
		}
		[HttpGet]
		public IActionResult IndexSala()
		{
			var conpermiso = MatrixUtilitarios.Utilitario.TienePermisoControladorAccion(HttpContext.Request.RouteValues["controller"].ToString(), HttpContext.Request.RouteValues["action"].ToString());
			if (conpermiso == -1)
			{
				return RedirectToAction("Login", "Usuario");
			}
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> ListarSala(IDataTablesRequest requestModel)
		{
			var listasala = new ListaSala();
			int numeroPagina = (requestModel.Start / requestModel.Length) + 1;
			try
			{
			listasala = await isala.ListarSalaAsync(Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")), null, null, numeroPagina, requestModel.Length, requestModel.Search.Value);
				var totalCount = listasala.paginacion.TotalRegistros;
				var data = listasala.lista.Select(lq => new
				{
					IdSala = lq.IdSala,
					Nombre = lq.Nombre,
					Aforo = lq.Aforo,
					CatalogoPiso = lq.catalogopiso.Descripcion,
					Observaciones = lq.Observaciones,
				}).ToList();

				var jsonData = new { draw = requestModel.Draw, recordsFiltered = totalCount, recordsTotal = totalCount, data = data }; 
				return Ok(jsonData);
			}
			catch (Exception ex)
			{
				return RedirectToAction("PaginaError", new { DescripcionError = "Hubo un error [" + ex.Message + "]" });
			}
		}
		[HttpPost]
		public async Task<IActionResult> RegistroSala()
		{
			int IdSala = Convert.ToInt32(Request.Form["IdSala"]);

			var sala = new Sala();
			try
			{


				if (IdSala > 0)
				{
					sala = await isala.ObtenerSalaAsync(IdSala, Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));
				}
				return View(sala);
			}
			catch (Exception ex)
			{
				sala.mensaje.CodigoMensaje = 1;
				sala.mensaje.DescripcionMensaje = "Sucedió un error en la Vista: " + HttpContext.Request.RouteValues["action"].ToString() + " del controlador: " + HttpContext.Request.RouteValues["controller"].ToString();
				sala.mensaje.DescripcionMensajeSistema = ex.Message;
				return View(sala);
			}
		}
		[HttpPost]
		public async Task<IActionResult> GuardarRegistroSala()
		{
			var sala = new Sala();
			try
			{
				int IdSala = Convert.ToInt32(Request.Form["txtRegIdSala"]);
				string Nombre = Request.Form["txtNombre"];
				int Aforo = Convert.ToInt32(Request.Form["txtAforo"]);
				int IdCatalogoPiso = Convert.ToInt32(Request.Form["cmbCatalogoPiso"]);
				string Observaciones = Request.Form["txtObservaciones"];


				sala = await isala.GuardaSalaAsync(
														IdSala,
																Nombre,
																Aforo,
																IdCatalogoPiso,
																Observaciones,
														Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));

				return Json(sala);
			}
			catch (Exception ex)
			{
				sala.mensaje.CodigoMensaje = 1;
				sala.mensaje.DescripcionMensaje = "Sucedió un error en la Vista: " + HttpContext.Request.RouteValues["action"].ToString() + " del controlador: " + HttpContext.Request.RouteValues["controller"].ToString();
				sala.mensaje.DescripcionMensajeSistema = ex.Message;
				return Json(sala);
			}
		}
		[HttpPost]
		public async Task<IActionResult> EliminarSala()
		{
			int IdSala = Convert.ToInt32(Request.Form["IdSala"]);
			Utilitarios.Mensaje mensaje = new Utilitarios.Mensaje();
			try
			{
				mensaje = await isala.EliminarSalaAsync(IdSala, Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));
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
		public async Task<ActionResult> DescargarSalaXls()
		{


			//string FechaInicial = Convert.ToDateTime(Request["FechaInicial"]).ToString("dd/MM/yyyy");
			string Titulo = "REPORTE DE SALA";
			try
			{
				var dt = await isala.DescargarSala(Convert.ToInt32(HttpContext.Session.GetString("IdUsuario")));
				var stream = new MemoryStream();
				OfficeOpenXml.ExcelPackage pck = new OfficeOpenXml.ExcelPackage(stream);


				dt.Tables[0].TableName = "Reporte";
				dt.Tables[0].ExtendedProperties.Add("Titulo", Titulo);

				pck = MatrixUtilitarios.Utilitario.ExportarReporteV2(dt);
				stream.Position = 0;

				string excelName = "ReporteSala" + DateTime.Now.ToString("ddMMyyyyHHmmss") + ".xlsx";
				return File(pck.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);

			}
			catch (Exception ex)
			{
				return RedirectToAction("PaginaError", new { DescripcionError = "Hubo un error [" + ex.Message + "]" });
			}
		}
		//[HttpGet]
		//public async Task<ActionResult> DescargarSalaPdf()
		//{


		//	//string FechaInicial = Convert.ToDateTime(Request["FechaInicial"]).ToString("dd/MM/yyyy");         
		//	try
		//	{
		//		DataSet dataset = await isala.DescargarSala(Convert.ToInt32(HttpContext.Session.GetString("IdUsuario")));
		//		List<string> ListaTitulos = new List<string>();
		//		List<float[]> ListaAnchosColumnas = new List<float[]>();
		//		float[] anchosColumnasResumen = null;
		//		List<int> ListaAnchoPorcentajeTabla = new List<int>();
		//		List<bool> ListaConEcabezadoTabla = new List<bool>();
		//		List<bool> ListaConNroCorrelativoTabla = new List<bool>();

		//		foreach (DataTable tabla in dataset.Tables)
		//		{
		//			anchosColumnasResumen = new float[tabla.Columns.Count + 1];
		//			for (int i = 0; i < tabla.Columns.Count + 1; i++)
		//			{
		//				anchosColumnasResumen[i] = 1f;
		//			}
		//			ListaAnchosColumnas.Add(anchosColumnasResumen);// ancho de cada columna por cada tabla, si va hacerlo independiente sacarlo de acá
		//			ListaAnchoPorcentajeTabla.Add(100);//ancho que abarca cada tabla, si lo va hacer independiente, sacarlo de acá
		//			ListaTitulos.Add("");//titulo para cada tabla, si va hacer manual sacarlo de acá
		//			ListaConEcabezadoTabla.Add(true);//encabezado para cada tabla, si va hacer manual sacarlo de acá
		//			ListaConNroCorrelativoTabla.Add(true);//correlativo para cada tabla, si va hacer manual sacarlo de acá
		//		}
		//		var memoryStream = GeneradorPDF.GenerarPDF(dataset,
		//			"REPORTE DE SALA",
		//			ListaTitulos, GeneradorPDF.TipoHorientacion.Horizontal,
		//			ListaAnchosColumnas, GeneradorPDF.TipoPagina.PaginaA4,
		//			ListaAnchoPorcentajeTabla,
		//			ListaConEcabezadoTabla,
		//			ListaConNroCorrelativoTabla);

		//		return File(memoryStream.ToArray(), "application/pdf");
		//	}
		//	catch (Exception ex)
		//	{
		//		return RedirectToAction("PaginaError", new { DescripcionError = "Hubo un error [" + ex.Message + "]" });
		//	}
		//}
	}
}
