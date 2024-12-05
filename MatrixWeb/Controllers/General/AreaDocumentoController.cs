
using System;
using System.Linq;
using MatrixService;
using General;
using System.Threading.Tasks;
using DataTables.AspNet.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using DataTables.AspNet.Core; using Microsoft.AspNetCore.Http;


namespace MatrixWeb.Controllers
{
    //[ValidarUsuario]
    public class AreaDocumentoController : Controller
    {
        private readonly ISvcAreaDocumento iareadocumento;
        private readonly ISvcArea iarea;
        public AreaDocumentoController(ISvcAreaDocumento _iareadocumento, ISvcArea _iarea)
        {
            iareadocumento = _iareadocumento;
            iarea = _iarea;
        }
        [HttpPost]
        public IActionResult IndexAreaDocumento()
        {
            int IdArea = Convert.ToInt32(Request.Form["IdArea"]);
            ViewBag.IdArea = IdArea;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ListarAreaDocumento(IDataTablesRequest requestModel)
        {
            int IdArea = Convert.ToInt32(Request.Form["IdArea"]);
            ListaAreaDocumento listaareadocumento = new ListaAreaDocumento();
            int numeroPagina = (requestModel.Start / requestModel.Length) + 1;
            try
            {
                listaareadocumento = await iareadocumento.ListarAreaDocumentoAsync(IdArea, Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")), null, null, numeroPagina, requestModel.Length, requestModel.Search.Value);
                if (listaareadocumento.mensaje.CodigoMensaje > 0)
                {
                    return RedirectToAction("PaginaError", new { DescripcionError = listaareadocumento.mensaje.DescripcionMensaje });
                }
                var totalCount = listaareadocumento.paginacion.TotalRegistros;
                var data = listaareadocumento.lista.Select(lq => new
                {

                    IdAreaDocumento = lq.IdAreaDocumento,


                    Area = lq.area.NombreArea,

                    CatalogoTipoDocumento = lq.catalogotipodocumento.Descripcion,

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
        public async Task<IActionResult> RegistroAreaDocumento()
        {
            int IdArea = Convert.ToInt32(Request.Form["IdArea"]);
            int IdAreaDocumento = Convert.ToInt32(Request.Form["IdAreaDocumento"]);
            var area = new Area();

            var areadocumento = new AreaDocumento();
            try
            {
                if (IdAreaDocumento > 0)
                {
                    areadocumento = await iareadocumento.ObtenerAreaDocumentoAsync(IdAreaDocumento, Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));
                }

                area = await iarea.ObtenerAreaAsync(IdArea);
                areadocumento.area = area;
                return View(areadocumento);
            }
            catch (Exception ex)
            {
                areadocumento.mensaje.CodigoMensaje = 1;
                areadocumento.mensaje.DescripcionMensaje = "Sucedió un error en la Vista: " ;
                areadocumento.mensaje.DescripcionMensajeSistema = ex.Message;
                return View(areadocumento);
            }
        }
        [HttpPost]
        public async Task<IActionResult> GuardarRegistroAreaDocumento()
        {
            var areadocumento = new AreaDocumento();
            try
            {
                int IdAreaDocumento = Convert.ToInt32(Request.Form["txtRegIdAreaDocumento"]);
                int IdArea = Convert.ToInt32(Request.Form["IdArea"]);
                int IdCatalogoTipoDocumento = Convert.ToInt32(Request.Form["cmbCatalogoTipoDocumento"]);
                bool Activo = Convert.ToBoolean(Convert.ToInt32(Request.Form["optActivoAreaDocumento"]));



                areadocumento = await iareadocumento.GuardaAreaDocumentoAsync(

                                    IdAreaDocumento,
                                            IdArea,
                                            IdCatalogoTipoDocumento,
                                            Activo,
                                    Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));
                return Json(areadocumento);
            }
            catch (Exception ex)
            {
                areadocumento.mensaje.CodigoMensaje = 1;
                areadocumento.mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA AL GUARDAR, PULSE F12 Y OBSERVE LA FICHA CONSOLE O EN TODO CASO COMUNIQUESE CON EL ADMINISTRADOR DEL SISTEMA";
                areadocumento.mensaje.DescripcionMensajeSistema = ex.Message;
                return Json(areadocumento);
            }
        }
        [HttpPost]
        public async Task<IActionResult> EliminarAreaDocumento()
        {
            int IdAreaDocumento = Convert.ToInt32(Request.Form["IdAreaDocumento"]);
            Utilitarios.Mensaje mensaje = new Utilitarios.Mensaje();
            try
            {
                mensaje = await iareadocumento.EliminarAreaDocumentoAsync(IdAreaDocumento, Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));
                return Json(mensaje);
            }
            catch (Exception ex)
            {
                mensaje.CodigoMensaje = 1;
                mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA AL ELIMINAR, COMUNIQUESE CON EL ADMINISTRADOR DEL SISTEMA";
                mensaje.DescripcionMensajeSistema = ex.Message;
                return Json(mensaje);
            }
        }
    }
}









