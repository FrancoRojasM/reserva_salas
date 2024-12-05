
using DataTables.AspNet.Core; using Microsoft.AspNetCore.Http;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using MatrixService;
using General;
using DataTables.AspNet.AspNetCore;
using System.Threading.Tasks;
namespace MatrixWeb.Controllers
{
    //[ValidarUsuario]
    public class UnidadEjecutoraController : Controller
    {
        private readonly ISvcUnidadEjecutora iunidadejecutora;
        public UnidadEjecutoraController(ISvcUnidadEjecutora _iunidadejecutora)
        {
            iunidadejecutora = _iunidadejecutora;
        }
        [HttpGet]
        public IActionResult IndexUnidadEjecutora()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ListarUnidadEjecutora(IDataTablesRequest requestModel)
        {
            ListaUnidadEjecutora listaunidadejecutora = new ListaUnidadEjecutora();
            int numeroPagina = (requestModel.Start / requestModel.Length) + 1;
            try
            {
                listaunidadejecutora = await iunidadejecutora.ListarUnidadEjecutoraAsync(Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")), null, null, numeroPagina, requestModel.Length, requestModel.Search.Value);
                if (listaunidadejecutora.mensaje.CodigoMensaje > 0)
                {
                    return RedirectToAction("PaginaError", new { DescripcionError = listaunidadejecutora.mensaje.DescripcionMensaje });
                }
                var totalCount = listaunidadejecutora.paginacion.TotalRegistros;
                var data = listaunidadejecutora.lista.Select(lq => new
                {

                    IdUnidadEjecutora = lq.IdUnidadEjecutora,

                    Descripcion = lq.Descripcion,





                }).ToList();

                var jsonData = new { draw = requestModel.Draw, recordsFiltered = totalCount, recordsTotal = totalCount, data = data }; return Ok(jsonData);
            }
            catch (Exception ex)
            {
                return RedirectToAction("PaginaError", new { DescripcionError = "Hubo un error [" + ex.Message + "]" });
            }
        }
        [HttpPost]
        public async Task<IActionResult> RegistroUnidadEjecutora()
        {
            int IdUnidadEjecutora = Convert.ToInt32(Request.Form["IdUnidadEjecutora"]);

            var unidadejecutora = new UnidadEjecutora();
            try
            {


                if (IdUnidadEjecutora > 0)
                {
                    unidadejecutora = await iunidadejecutora.ObtenerUnidadEjecutoraAsync(IdUnidadEjecutora, Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));
                }
                return View(unidadejecutora);
            }
            catch (Exception ex)
            {
                unidadejecutora.mensaje.CodigoMensaje = 1;
                unidadejecutora.mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA AL GUARDAR, COMUNIQUESE CON EL ADMINISTRADOR DEL SISTEMA";
                unidadejecutora.mensaje.DescripcionMensajeSistema = ex.Message;
                return View(unidadejecutora);
            }
        }
        [HttpPost]
        public async Task<IActionResult> GuardarRegistroUnidadEjecutora()
        {
            var unidadejecutora = new UnidadEjecutora();
            try
            {
                int IdUnidadEjecutora = Convert.ToInt32(Request.Form["txtRegIdUnidadEjecutora"]);
                string Descripcion = Request.Form["txtDescripcion"];



                unidadejecutora = await iunidadejecutora.GuardaUnidadEjecutoraAsync(

                                    IdUnidadEjecutora,
                                            Descripcion,
                                    Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));
                return Json(unidadejecutora);
            }
            catch (Exception ex)
            {
                unidadejecutora.mensaje.CodigoMensaje = 1;
                unidadejecutora.mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA AL GUARDAR, PULSE F12 Y OBSERVE LA FICHA CONSOLE O EN TODO CASO COMUNIQUESE CON EL ADMINISTRADOR DEL SISTEMA";
                unidadejecutora.mensaje.DescripcionMensajeSistema = ex.Message;
                return Json(unidadejecutora);
            }
        }
        [HttpPost]
        public async Task<IActionResult> EliminarUnidadEjecutora()
        {
            int IdUnidadEjecutora = Convert.ToInt32(Request.Form["IdUnidadEjecutora"]);
            Utilitarios.Mensaje mensaje = new Utilitarios.Mensaje();
            try
            {
                mensaje = await iunidadejecutora.EliminarUnidadEjecutoraAsync(IdUnidadEjecutora, Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));
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









