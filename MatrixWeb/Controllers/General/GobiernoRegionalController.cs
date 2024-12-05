	
using DataTables.AspNet.Core; 
using Microsoft.AspNetCore.Http;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using MatrixService;
using System.Threading.Tasks;
using General;
namespace MatrixWeb.Controllers
{
    public class GobiernoRegionalController : Controller
    {
		private readonly ISvcGobiernoRegional igobiernoregional;       
        public GobiernoRegionalController(ISvcGobiernoRegional _igobiernoregional)
        {
            igobiernoregional = _igobiernoregional;
        }
			[HttpGet]
		        public IActionResult IndexGobiernoRegional()
        {
		 var conpermiso = MatrixUtilitarios.Utilitario.TienePermisoControladorAccion(HttpContext.Request.RouteValues["controller"].ToString(), HttpContext.Request.RouteValues["action"].ToString()); if (conpermiso == -1) { return RedirectToAction("Login", "Usuario"); }
						            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ListarGobiernoRegional(IDataTablesRequest requestModel)
        {
				    var listagobiernoregional = new ListaGobiernoRegional();            
            int numeroPagina = (requestModel.Start / requestModel.Length) + 1;
             try
            {			
                listagobiernoregional =await igobiernoregional.ListarGobiernoRegionalAsync( Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")), null, null, numeroPagina, requestModel.Length, requestModel.Search.Value);           
            var totalCount = listagobiernoregional.paginacion.TotalRegistros;
            var data = listagobiernoregional.lista.Select(lq => new
            {
				IdGobiernoRegional=lq.IdGobiernoRegional,								NombreGobiernoRegional=lq.NombreGobiernoRegional,
								Region= lq.region.IdRegion, }).ToList();

			 var jsonData = new { draw = requestModel.Draw, recordsFiltered = totalCount, recordsTotal = totalCount, data = data }; return Ok(jsonData);
			 }
            catch (Exception ex)
            {
                return RedirectToAction("PaginaError", new { DescripcionError = "Hubo un error [" + ex.Message + "]" });
            }
        }
		[HttpPost]
        public async Task<IActionResult> RegistroGobiernoRegional()
        {
				   int IdGobiernoRegional = Convert.ToInt32(Request.Form["IdGobiernoRegional"]);	
		   		   
			var gobiernoregional = new GobiernoRegional();
		try
            {
               
								
                if (IdGobiernoRegional > 0)
                {                   
                        gobiernoregional =await igobiernoregional.ObtenerGobiernoRegionalAsync( IdGobiernoRegional,Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));                    
                }	
				                return View(gobiernoregional);
            }
            catch (Exception ex)
            {
                gobiernoregional.mensaje.CodigoMensaje = 1;
               gobiernoregional.mensaje.DescripcionMensaje = "Sucedió un error en la Vista: " + HttpContext.Request.RouteValues["action"].ToString()+" del controlador: "+ HttpContext.Request.RouteValues["controller"].ToString();
               gobiernoregional.mensaje.DescripcionMensajeSistema = ex.Message;
			    return View(gobiernoregional);
            }           
        }
        [HttpPost]
        public async Task<IActionResult> GuardarRegistroGobiernoRegional()
        {
		var gobiernoregional = new GobiernoRegional();
            try
            {
						 int IdGobiernoRegional = Convert.ToInt32(Request.Form["txtRegIdGobiernoRegional"]);
															string NombreGobiernoRegional = Request.Form["txtNombreGobiernoRegional"];										
																	
							int IdRegion = Convert.ToInt32(Request.Form["cmbRegion"]);
							   
gobiernoregional =await igobiernoregional.GuardaGobiernoRegionalAsync(                
										IdGobiernoRegional,
												NombreGobiernoRegional,
												IdRegion,
						                Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));             
				                return Json(gobiernoregional);
            }
            catch (Exception ex)
            {
			   gobiernoregional.mensaje.CodigoMensaje = 1;
              gobiernoregional.mensaje.DescripcionMensaje = "Sucedió un error en la Vista: " + HttpContext.Request.RouteValues["action"].ToString()+" del controlador: "+ HttpContext.Request.RouteValues["controller"].ToString();
               gobiernoregional.mensaje.DescripcionMensajeSistema = ex.Message;
                return Json(gobiernoregional);
            }			
        }
        [HttpPost]
        public async Task<IActionResult> EliminarGobiernoRegional()
        {          
			int IdGobiernoRegional = Convert.ToInt32(Request.Form["IdGobiernoRegional"]);
            Utilitarios.Mensaje mensaje = new Utilitarios.Mensaje();
            try
            {               
                mensaje =await igobiernoregional.EliminarGobiernoRegionalAsync( IdGobiernoRegional, Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));               
                return Json(mensaje);
            }
             catch (Exception ex)
            {
			    mensaje.CodigoMensaje = 1;
                mensaje.DescripcionMensaje = "Sucedió un error en la Vista: " + HttpContext.Request.RouteValues["action"].ToString()+" del controlador: "+ HttpContext.Request.RouteValues["controller"].ToString();
                mensaje.DescripcionMensajeSistema = ex.Message;
                return Json(mensaje);
            }	
        } 
		[HttpPost]
        public async Task<IActionResult> ListarComboGobiernoRegional()
        {
            var listagobiernoregional = await igobiernoregional.ListarComboGobiernoRegional(Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));
            try
            {              
                var data = listagobiernoregional.lista.Select(lq => new
                {
                    IdGobiernoRegional = lq.IdGobiernoRegional,
                     lq.NombreGobiernoRegional
                }).ToList();

                return Json(listagobiernoregional);
            }
            catch (Exception ex)
            {
                listagobiernoregional.mensaje.CodigoMensaje = 1;
                listagobiernoregional.mensaje.DescripcionMensaje = "Sucedió un error en la Vista: " + HttpContext.Request.RouteValues["action"].ToString() + " del controlador: " + HttpContext.Request.RouteValues["controller"].ToString();
                listagobiernoregional.mensaje.DescripcionMensajeSistema = ex.Message;
                return Json(listagobiernoregional);
            }
        }
    }
}









		