
using DataTables.AspNet.AspNetCore;
using DataTables.AspNet.Core; using Microsoft.AspNetCore.Http;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using MatrixService;

namespace MatrixWeb.Controllers
{
    //[ValidarUsuario]
    public class ModuloController : Controller
    {
        private ISvcModulo imodulo;
        public ModuloController(ISvcModulo _imodulo)
        {
            imodulo = _imodulo;
        }

        [HttpGet]
        public IActionResult ping()
        {        
            if (HttpContext.Session.Keys.Count() == 0) { return Json("-1"); }
            if (Convert.ToInt32(HttpContext.Session.GetString("IdPersona") == null ? 0 : HttpContext.Session.GetString("IdPersona")) == 0)
            {              
                return Json("-1");             
            }
            return Json("1");
        }
        [HttpGet]
        public IActionResult IndexModulo()
        {
            var conpermiso = MatrixUtilitarios.Utilitario.TienePermisoControladorAccion(HttpContext.Request.RouteValues["controller"].ToString(), HttpContext.Request.RouteValues["action"].ToString()); if (conpermiso == -1) { return RedirectToAction("Login", "Usuario"); }
            return View();
        }
        [HttpPost]
        public IActionResult ListarModulo(IDataTablesRequest requestModel)
        {
            Sistema.ListaModulo listamodulo = new Sistema.ListaModulo();
            Sistema.Modulo modulo = new Sistema.Modulo();
            int numeroPagina = (requestModel.Start / requestModel.Length) + 1;
            try
            {               
                listamodulo = imodulo.ListarModulo( Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")), null, null, numeroPagina, requestModel.Length, requestModel.Search.Value);
                if (listamodulo.mensaje.CodigoMensaje > 0)
                {
                    return RedirectToAction("PaginaError", new { DescripcionError = listamodulo.mensaje.DescripcionMensaje });
                }
                var totalCount = listamodulo.paginacion.TotalRegistros;
                var data = listamodulo.lista.Select(lq => new
                {
                    IdModulo = lq.IdModulo,
                    NombreModulo = lq.NombreModulo,
                    DetalleModulo = lq.DetalleModulo,
                    OrdenModulo = lq.OrdenModulo,
                    CatalogoTipoModulo = lq.catalogotipomodulo.Descripcion,
                    Activo = (lq.Activo == true ? "Si" : "No"),
                    RutaImagenModulo = lq.RutaImagenModulo
                }).ToList();
                var jsonData = new { draw = requestModel.Draw, recordsFiltered = totalCount, recordsTotal = totalCount, data = data }; return Ok(jsonData);
            }
            catch (Exception ex)
            {
                return RedirectToAction("PaginaError", new { DescripcionError = "Hubo un error [" + ex.Message + "]" });
            }
        }
        [HttpPost]
        public IActionResult RegistroModulo()
        {

            int IdModulo = Convert.ToInt32(Request.Form["IdModulo"]);
            try
            {
                Sistema.Modulo modulo = new Sistema.Modulo();

                if (IdModulo > 0)
                {
                    
                        modulo = imodulo.ObtenerModulo( IdModulo);
                   
                }
                return View(modulo);
            }
            catch (Exception ex)
            {
                return RedirectToAction("PaginaError", new { DescripcionError = "Hubo un error [" + ex.Message + "]" });
            }
        }
        [HttpPost]
        public IActionResult GuardarRegistroModulo()
        {
            int IdModulo = Convert.ToInt32(Request.Form["txtRegIdModulo"]);
            string NombreModulo = Request.Form["txtNombreModulo"];
            string DetalleModulo = Request.Form["txtDetalleModulo"];
            int OrdenModulo = Convert.ToInt32(Request.Form["txtOrdenModulo"]);
            int IdCatalogoTipoModulo = Convert.ToInt32(Request.Form["cmbCatalogoTipoModulo"]);
            bool Activo = Convert.ToBoolean(Convert.ToInt32(Request.Form["optActivo"]));
            string RutaImagenModulo = Request.Form["txtRutaImagenModulo"];


            Sistema.Modulo modulo = new Sistema.Modulo();
            try
            {
                
                    modulo = imodulo.GuardaModulo(
                
                                        IdModulo,
                                                NombreModulo,
                                                DetalleModulo,
                                                OrdenModulo,
                                                IdCatalogoTipoModulo,
                                                Activo,
                                                RutaImagenModulo,
                                        Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario"))
                );
                
                return Json(modulo);
            }
            catch (Exception ex)
            {
                modulo.mensaje.CodigoMensaje = 1;
                modulo.mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA AL GUARDAR, COMUNIQUESE CON EL ADMINISTRADOR DEL SISTEMA";
                modulo.mensaje.DescripcionMensajeSistema = ex.Message;
                return Json(modulo);
            }
        }
        [HttpPost]
        public IActionResult EliminarModulo()
        {
            int IdModulo = Convert.ToInt32(Request.Form["IdModulo"]);

            Sistema.Modulo modulo = new Sistema.Modulo();
            Utilitarios.Mensaje mensaje = new Utilitarios.Mensaje();
            try
            {
               
                    mensaje = imodulo.EliminarModulo( IdModulo, Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));
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

        [HttpPost]
        public IActionResult ListarComboModulo()
        {
            Sistema.Modulo modulo = new Sistema.Modulo();
            Sistema.ListaModulo listamodulo = new Sistema.ListaModulo();
            Utilitarios.Mensaje mensaje = new Utilitarios.Mensaje();
            try
            {
               
                    listamodulo = imodulo.ListarComboModulo();
                var data = listamodulo.lista.Select(lq => new
                {
                    IdModulo = lq.IdModulo,
                    NombreModulo = lq.NombreModulo
                }).ToList();

                return Json(data);
            }
            catch (Exception ex)
            {
                listamodulo.mensaje.CodigoMensaje = 1;
                listamodulo.mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA AL ELIMINAR, COMUNIQUESE CON EL ADMINISTRADOR DEL SISTEMA";
                listamodulo.mensaje.DescripcionMensajeSistema = ex.Message;
                return Json(listamodulo.lista);
            }
        }
    }
}
