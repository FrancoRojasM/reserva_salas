using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MatrixService;
using Utilitarios;
using System;
using DataTables.AspNet.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace MatrixWeb.Controllers
{
    //[ValidarUsuario]
    public class CatalogoSancionController : Controller
    {
        private ListaCatalogo listacatalogo;
        private readonly ISvcCatalogo icatalogo;

        public CatalogoSancionController(ISvcCatalogo _icatalogo)
        {
            icatalogo = _icatalogo;
            listacatalogo=  new ListaCatalogo();
        }
        [HttpGet]
        public IActionResult IndexCatalogoSancion()
        {
            var conpermiso = MatrixUtilitarios.Utilitario.TienePermisoControladorAccion(HttpContext.Request.RouteValues["controller"].ToString(), HttpContext.Request.RouteValues["action"].ToString()); if (conpermiso == -1) { return RedirectToAction("Login", "Usuario"); }
            return View();
        }
        [HttpGet]
        public IActionResult IndexCatalogo()
        {
            int IdCatalogoPadre = Convert.ToInt32(Request.Query["IdCatalogoPadre"]);
            ViewBag.IdCatalogoPadre = IdCatalogoPadre;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ListarCatalogoPadre(IDataTablesRequest requestModel)
        {
            ListaCatalogo listacatalogo = new ListaCatalogo();
            int numeroPagina = (requestModel.Start / requestModel.Length) + 1;
            try
            {
                listacatalogo = await icatalogo.ListarCatalogoPadreAsync(Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")), null, null, numeroPagina, requestModel.Length, requestModel.Search.Value, "Sancion");
                if (listacatalogo.mensaje.CodigoMensaje > 0)
                {
                    return RedirectToAction("PaginaError", new { DescripcionError = listacatalogo.mensaje.DescripcionMensaje });
                }
                var totalCount = listacatalogo.paginacion.TotalRegistros;
                var data = listacatalogo.lista.Select(lq => new
                {
                    lq.IdCatalogo,
                    lq.OrdenItem,
                    lq.Descripcion,
                    lq.Detalle
                }).ToList();

                var jsonData = new { draw = requestModel.Draw, recordsFiltered = totalCount, recordsTotal = totalCount, data = data }; return Ok(jsonData);
            }
            catch (Exception ex)
            {
                return RedirectToAction("PaginaError", new { DescripcionError = "Hubo un error [" + ex.Message + "]" });
            }
        }
        [HttpPost]
        public async Task<IActionResult> ListarCatalogo(IDataTablesRequest requestModel)
        {
            int IdCatalogoPadre = Convert.ToInt32(Request.Form["IdCatalogoPadre"]);
            ListaCatalogo listacatalogo = new ListaCatalogo();
            int numeroPagina = (requestModel.Start / requestModel.Length) + 1;
            try
            {
                listacatalogo = await icatalogo.ListarCatalogoAsync(Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")), IdCatalogoPadre, null, null, numeroPagina, requestModel.Length, requestModel.Search.Value, "Sancion");
                if (listacatalogo.mensaje.CodigoMensaje > 0)
                {
                    return RedirectToAction("PaginaError", new { DescripcionError = listacatalogo.mensaje.DescripcionMensaje });
                }
                var totalCount = listacatalogo.paginacion.TotalRegistros;
                var data = listacatalogo.lista.Select(lq => new
                {
                    lq.IdCatalogo,
                    lq.OrdenItem,
                    lq.Descripcion,
                    lq.Detalle,
                    Activo = (lq.Activo == 1 ? "Si" : "No")
                }).ToList();

                var jsonData = new { draw = requestModel.Draw, recordsFiltered = totalCount, recordsTotal = totalCount, data = data }; return Ok(jsonData);
            }
            catch (Exception ex)
            {
                return RedirectToAction("PaginaError", new { DescripcionError = "Hubo un error [" + ex.Message + "]" });
            }
        }
        [HttpPost]
        public async Task<IActionResult> RegistroCatalogo()
        {
            int IdCatalogo = Convert.ToInt32(Request.Form["IdCatalogo"]);
            int IdCatalogoPadre = Convert.ToInt32(Request.Form["IdCatalogoPadre"]);

            var catalogo = new Catalogo();
            try
            {
                if (IdCatalogo > 0)
                {
                    catalogo = await icatalogo.ObtenerCatalogoAsync(IdCatalogo, Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")), "Sancion");
                }
                catalogo.IdCatalogoPadre = IdCatalogoPadre;
                return View(catalogo);
            }
            catch (Exception ex)
            {
                catalogo.mensaje.CodigoMensaje = 1;
                catalogo.mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA AL GUARDAR, COMUNIQUESE CON EL ADMINISTRADOR DEL SISTEMA";
                catalogo.mensaje.DescripcionMensajeSistema = ex.Message;
                return View(catalogo);
            }
        }
        [HttpPost]
        public async Task<IActionResult> GuardarRegistroCatalogo()
        {
            var catalogo = new Catalogo();
            try
            {
                int IdCatalogo = Convert.ToInt32(Request.Form["txtRegIdCatalogo"]);

                int IdGrupo = Convert.ToInt32(Request.Form["cmbGrupo"]);
                int IdCatalogoPadre = Convert.ToInt32(Request.Form["txtIdCatalogoPadre"]);
                int OrdenItem = Convert.ToInt32(Request.Form["txtOrdenItem"]);
                string Descripcion = Request.Form["txtDescripcion"];
                string Detalle = Request.Form["txtDetalle"];
                bool Activo = Convert.ToBoolean(Convert.ToInt32(Request.Form["optActivo"]));

                catalogo = await icatalogo.GuardaCatalogoAsync(
                IdCatalogo,
                IdGrupo,
                IdCatalogoPadre,
                OrdenItem,
                Descripcion,
                Detalle,
                Activo,
                Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")), "Sancion");
                return Json(catalogo);
            }
            catch (Exception ex)
            {
                catalogo.mensaje.CodigoMensaje = 1;
                catalogo.mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA AL GUARDAR, PULSE F12 Y OBSERVE LA FICHA CONSOLE O EN TODO CASO COMUNIQUESE CON EL ADMINISTRADOR DEL SISTEMA";
                catalogo.mensaje.DescripcionMensajeSistema = ex.Message;
                return Json(catalogo);
            }
        }
        [HttpPost]
        public async Task<IActionResult> EliminarCatalogo()
        {
            int IdCatalogo = Convert.ToInt32(Request.Form["IdCatalogo"]);
            Utilitarios.Mensaje mensaje = new Utilitarios.Mensaje();
            try
            {
                mensaje = await icatalogo.EliminarCatalogoAsync(IdCatalogo, Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")), "Sancion");
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

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> ListarCatalogoTipoDocumentoPersonal()
        {           
            listacatalogo =await icatalogo.ListarCatalogoComboAsync( 1, "Sancion");
            var data = listacatalogo.lista.Select(lq => new
            {
                lq.IdCatalogo,
                lq.Descripcion
            }).ToList();
            return Json(data);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> ListarCatalogoTipoSancion()
        {
            listacatalogo = await icatalogo.ListarCatalogoComboAsync(5, "Sancion");
            var data = listacatalogo.lista.Select(lq => new
            {
                lq.IdCatalogo,
                lq.Descripcion
            }).ToList();
            return Json(data);
        }

    }
}









		