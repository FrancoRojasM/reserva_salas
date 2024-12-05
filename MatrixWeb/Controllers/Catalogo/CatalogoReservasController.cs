
using DataTables.AspNet.Core;
using Microsoft.AspNetCore.Http;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using MatrixService;
using System.Threading.Tasks;
using Reservas;
using Utilitarios;
namespace MatrixWeb.Controllers
{
	public class CatalogoReservasController : Controller
	{
		private readonly ISvcCatalogo icatalogo;
		public CatalogoReservasController(ISvcCatalogo _icatalogo)
		{
			icatalogo = _icatalogo;
		}
		[HttpGet]
		public IActionResult IndexCatalogo()
		{
			var conpermiso = MatrixUtilitarios.Utilitario.TienePermisoControladorAccion(HttpContext.Request.RouteValues["controller"].ToString(), HttpContext.Request.RouteValues["action"].ToString());
			if (conpermiso == -1)
			{
				return RedirectToAction("Login", "Usuario");
			}
			return View();
		}

        [HttpPost]
        public async Task<IActionResult> ListarCatalogoPiso()
        {
            Utilitarios.ListaCatalogo listacatalogo = new Utilitarios.ListaCatalogo();
            listacatalogo = await icatalogo.ListarCatalogoComboAsync(1, "Reservas");
            var data = listacatalogo.lista.Select(lq => new
            {
                lq.IdCatalogo,
                lq.Descripcion
            }).ToList();
            return Json(data);
        }

        [HttpPost]
        public async Task<IActionResult> ListarCatalogoConsejoRegional()
        {
            Utilitarios.ListaCatalogo listacatalogo = new Utilitarios.ListaCatalogo();
            listacatalogo = await icatalogo.ListarCatalogoComboAsync(48, "Reservas");
            var data = listacatalogo.lista.Select(lq => new
            {
                lq.IdCatalogo,
                lq.Descripcion
            }).ToList();
            return Json(data);
        }

        [HttpPost]
        public async Task<IActionResult> ListarCatalogoSecretaria()
        {
            Utilitarios.ListaCatalogo listacatalogo = new Utilitarios.ListaCatalogo();
            listacatalogo = await icatalogo.ListarCatalogoComboAsync(6, "Reservas");
            var data = listacatalogo.lista.Select(lq => new
            {
                lq.IdCatalogo,
                lq.Descripcion
            }).ToList();
            return Json(data);
        }

        [HttpPost]
        public async Task<IActionResult> ListarCatalogoComite(int idSecretaria)
        {
            Utilitarios.ListaCatalogo listacatalogo = new Utilitarios.ListaCatalogo();
            listacatalogo = await icatalogo.ListarCatalogoComboAsync(idSecretaria, "Reservas");
            var data = listacatalogo.lista.Select(lq => new
            {
                lq.IdCatalogo,
                lq.Descripcion
            }).ToList();
            return Json(data);
        }

        [HttpPost]
        public async Task<IActionResult> ListarCatalogoSala()
        {
            Utilitarios.ListaCatalogo listacatalogo = new Utilitarios.ListaCatalogo();
            listacatalogo = await icatalogo.ListarCatalogoComboAsync(80, "Reservas");
            var data = listacatalogo.lista.Select(lq => new
            {
                lq.IdCatalogo,
                lq.Descripcion
            }).ToList();
            return Json(data);
        }


        [HttpPost]
        public async Task<IActionResult> ListarCatalogoEstadoSolicitud()
        {
            Utilitarios.ListaCatalogo listacatalogo = new Utilitarios.ListaCatalogo();
            listacatalogo = await icatalogo.ListarCatalogoComboAsync(77, "Reservas");
            var data = listacatalogo.lista.Select(lq => new
            {
                lq.IdCatalogo,
                lq.Descripcion
            }).ToList();
            return Json(data);
        }


        //[HttpGet]
        //public async Task<ActionResult> DescargarCatalogoXls()
        //{


        //	//string FechaInicial = Convert.ToDateTime(Request["FechaInicial"]).ToString("dd/MM/yyyy");
        //	string Titulo = "REPORTE DE CATALOGO";
        //	try
        //	{
        //		var dt = await icatalogo.DescargarCatalogo(Convert.ToInt32(HttpContext.Session.GetString("IdUsuario")));
        //		var stream = new MemoryStream();
        //		OfficeOpenXml.ExcelPackage pck = new OfficeOpenXml.ExcelPackage(stream);


        //		dt.Tables[0].TableName = "Reporte";
        //		dt.Tables[0].ExtendedProperties.Add("Titulo", Titulo);

        //		pck = MatrixUtilitarios.Utilitario.ExportarReporteV2(dt);
        //		stream.Position = 0;

        //		string excelName = "ReporteCatalogo" + DateTime.Now.ToString("ddMMyyyyHHmmss") + ".xlsx";
        //		return File(pck.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);

        //	}
        //	catch (Exception ex)
        //	{
        //		return RedirectToAction("PaginaError", new { DescripcionError = "Hubo un error [" + ex.Message + "]" });
        //	}
        //}
        //[HttpGet]
        //public async Task<ActionResult> DescargarCatalogoPdf()
        //{


        //	//string FechaInicial = Convert.ToDateTime(Request["FechaInicial"]).ToString("dd/MM/yyyy");         
        //	try
        //	{
        //		DataSet dataset = await icatalogo.DescargarCatalogo(Convert.ToInt32(HttpContext.Session.GetString("IdUsuario")));
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
        //			"REPORTE DE CATALOGO",
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
