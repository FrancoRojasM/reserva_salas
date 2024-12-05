	
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
    public class RegionController : Controller
    {
		private readonly ISvcRegion iregion;       
        public RegionController(ISvcRegion _iregion)
        {
            iregion = _iregion;
        }
			[HttpGet]
		        public IActionResult IndexRegion()
        {
		 var conpermiso = MatrixUtilitarios.Utilitario.TienePermisoControladorAccion(HttpContext.Request.RouteValues["controller"].ToString(), HttpContext.Request.RouteValues["action"].ToString()); if (conpermiso == -1) { return RedirectToAction("Login", "Usuario"); }
						            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ListarRegion(IDataTablesRequest requestModel)
        {
				    var listaregion = new ListaRegion();            
            int numeroPagina = (requestModel.Start / requestModel.Length) + 1;
             try
            {			
                listaregion =await iregion.ListarRegionAsync( Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")), null, null, numeroPagina, requestModel.Length, requestModel.Search.Value);           
            var totalCount = listaregion.paginacion.TotalRegistros;
            var data = listaregion.lista.Select(lq => new
            {
				IdRegion=lq.IdRegion,								NombreRegion=lq.NombreRegion,
								 }).ToList();

			 var jsonData = new { draw = requestModel.Draw, recordsFiltered = totalCount, recordsTotal = totalCount, data = data }; return Ok(jsonData);
			 }
            catch (Exception ex)
            {
                return RedirectToAction("PaginaError", new { DescripcionError = "Hubo un error [" + ex.Message + "]" });
            }
        }
		[HttpPost]
        public async Task<IActionResult> RegistroRegion()
        {
				   int IdRegion = Convert.ToInt32(Request.Form["IdRegion"]);	
		   		   
			var region = new Region();
		try
            {
               
								
                if (IdRegion > 0)
                {                   
                        region =await iregion.ObtenerRegionAsync( IdRegion,Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));                    
                }	
				                return View(region);
            }
            catch (Exception ex)
            {
                region.mensaje.CodigoMensaje = 1;
               region.mensaje.DescripcionMensaje = "Sucedió un error en la Vista: " + HttpContext.Request.RouteValues["action"].ToString()+" del controlador: "+ HttpContext.Request.RouteValues["controller"].ToString();
               region.mensaje.DescripcionMensajeSistema = ex.Message;
			    return View(region);
            }           
        }
        [HttpPost]
        public async Task<IActionResult> GuardarRegistroRegion()
        {
		var region = new Region();
            try
            {
						 int IdRegion = Convert.ToInt32(Request.Form["txtRegIdRegion"]);
															string NombreRegion = Request.Form["txtNombreRegion"];										
											   
region =await iregion.GuardaRegionAsync(                
										IdRegion,
												NombreRegion,
						                Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));             
				                return Json(region);
            }
            catch (Exception ex)
            {
			   region.mensaje.CodigoMensaje = 1;
              region.mensaje.DescripcionMensaje = "Sucedió un error en la Vista: " + HttpContext.Request.RouteValues["action"].ToString()+" del controlador: "+ HttpContext.Request.RouteValues["controller"].ToString();
               region.mensaje.DescripcionMensajeSistema = ex.Message;
                return Json(region);
            }			
        }
        [HttpPost]
        public async Task<IActionResult> EliminarRegion()
        {          
			int IdRegion = Convert.ToInt32(Request.Form["IdRegion"]);
            Utilitarios.Mensaje mensaje = new Utilitarios.Mensaje();
            try
            {               
                mensaje =await iregion.EliminarRegionAsync( IdRegion, Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));               
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
        public async Task<IActionResult> ListarComboRegion()
        {
            var listaregion = await iregion.ListarComboRegion(Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));
            try
            {              
                var data = listaregion.lista.Select(lq => new
                {
                    IdRegion = lq.IdRegion,
                    lq.NombreRegion
                }).ToList();

                return Json(listaregion);
            }
            catch (Exception ex)
            {
                listaregion.mensaje.CodigoMensaje = 1;
                listaregion.mensaje.DescripcionMensaje = "Sucedió un error en la Vista: " + HttpContext.Request.RouteValues["action"].ToString() + " del controlador: " + HttpContext.Request.RouteValues["controller"].ToString();
                listaregion.mensaje.DescripcionMensajeSistema = ex.Message;
                return Json(listaregion);
            }
        }
    }
}









		