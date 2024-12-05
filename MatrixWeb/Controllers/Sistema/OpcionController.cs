using DataTables.AspNet.AspNetCore;
using DataTables.AspNet.Core; using Microsoft.AspNetCore.Http;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using MatrixService;

namespace MatrixWeb.Controllers
{
    //[ValidarUsuario]
    public class OpcionController : Controller
    {
        private ISvcOpcion iopcion;
        public OpcionController(ISvcOpcion _iopcion)
        {
            iopcion = _iopcion;
        }
       
        [HttpGet]
        public IActionResult IndexOpcion()
        {
            var conpermiso = MatrixUtilitarios.Utilitario.TienePermisoControladorAccion(HttpContext.Request.RouteValues["controller"].ToString(), HttpContext.Request.RouteValues["action"].ToString()); if (conpermiso == -1) { return RedirectToAction("Login", "Usuario"); }
            return View();
        }
        [HttpPost]
        public IActionResult ListarOpcion(IDataTablesRequest requestModel)
        {

            Sistema.ListaOpcion listaopcion = new Sistema.ListaOpcion();
            Sistema.Opcion opcion = new Sistema.Opcion();
            int numeroPagina = (requestModel.Start / requestModel.Length) + 1;
            try
            {
               
                    listaopcion = iopcion.ListarOpcion( Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")), null, null, numeroPagina, requestModel.Length, requestModel.Search.Value);
               
                if (listaopcion.mensaje.CodigoMensaje > 0)
                {
                    return RedirectToAction("PaginaError", new { DescripcionError = listaopcion.mensaje.DescripcionMensaje });
                }
                var totalCount = listaopcion.paginacion.TotalRegistros;
                var data = listaopcion.lista.Select(lq => new
                {

                    IdOpcion = lq.IdOpcion,

                    Modulo = lq.modulo.NombreModulo,

                    OpcionPadre = lq.IdOpcionPadre,

                    NombreOpcion = lq.NombreOpcion,
                    NombreOpcionPadre = lq.NombreOpcionPadre,
                    DetalleOpcion = lq.DetalleOpcion,

                    CatalogoTipoOpcion = lq.catalogotipoopcion.Descripcion,

                    OrdenOpcion = lq.OrdenOpcion,

                    RutaImagenOpcion = lq.RutaImagenOpcion,

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
        public IActionResult RegistroOpcion()
        {

            int IdOpcion = Convert.ToInt32(Request.Form["IdOpcion"]);
            try
            {
                Sistema.Opcion opcion = new Sistema.Opcion();

                if (IdOpcion > 0)
                {
                  
                        opcion = iopcion.ObtenerOpcion( IdOpcion);
                   
                }
                return View(opcion);
            }
            catch (Exception ex)
            {
                return RedirectToAction("PaginaError", new { DescripcionError = "Hubo un error [" + ex.Message + "]" });
            }
        }
        [HttpPost]
        public IActionResult GuardarRegistroOpcion()
        {
            int IdOpcion = Convert.ToInt32(Request.Form["txtRegIdOpcion"]);

            int IdModulo = Convert.ToInt32(Request.Form["cmbModulo"]);
            int IdOpcionPadre = Convert.ToInt32(Request.Form["cmbOpcionPadre"]);
            string NombreOpcion = Request.Form["txtNombreOpcion"];
            string DetalleOpcion = Request.Form["txtDetalleOpcion"];
            int IdCatalogoTipoOpcion = Convert.ToInt32(Request.Form["cmbCatalogoTipoOpcion"]);
            int OrdenOpcion = Convert.ToInt32(Request.Form["txtOrdenOpcion"]);
            string RutaImagenOpcion = Request.Form["txtRutaImagenOpcion"];
            bool Activo = Convert.ToBoolean(Convert.ToInt32(Request.Form["optActivo"]));


            Sistema.Opcion opcion = new Sistema.Opcion();
            try
            {
               
                    opcion = iopcion.GuardaOpcion(
                
                                        IdOpcion,
                                                IdModulo,
                                                IdOpcionPadre,
                                                NombreOpcion,
                                                DetalleOpcion,
                                                IdCatalogoTipoOpcion,
                                                OrdenOpcion,
                                                RutaImagenOpcion,
                                                Activo,
                                        Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario"))
                );
                
                return Json(opcion);
            }
            catch (Exception ex)
            {
                opcion.mensaje.CodigoMensaje = 1;
                opcion.mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA AL GUARDAR, COMUNIQUESE CON EL ADMINISTRADOR DEL SISTEMA";
                opcion.mensaje.DescripcionMensajeSistema = ex.Message;
                return Json(opcion);
            }
        }
        [HttpPost]
        public IActionResult EliminarOpcion()
        {
            int IdOpcion = Convert.ToInt32(Request.Form["IdOpcion"]);

            Sistema.Opcion opcion = new Sistema.Opcion();
            Utilitarios.Mensaje mensaje = new Utilitarios.Mensaje();
            try
            {
               
                    mensaje = iopcion.EliminarOpcion( IdOpcion, Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));
               
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
        public IActionResult ListarComboOpcion()
        {
            Sistema.Opcion opcion = new Sistema.Opcion();
            Sistema.ListaOpcion listaopcion = new Sistema.ListaOpcion();
         
            try
            {
               
                    listaopcion = iopcion.ListarComboOpcion();
               
                var data = listaopcion.lista.Select(lq => new
                {
                    IdOpcion = lq.IdOpcion,
                    NombreOpcion = lq.NombreOpcion
                }).ToList();

                return Json(data);
            }
            catch (Exception ex)
            {
                listaopcion.mensaje.CodigoMensaje = 1;
                listaopcion.mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA AL ELIMINAR, COMUNIQUESE CON EL ADMINISTRADOR DEL SISTEMA";
                listaopcion.mensaje.DescripcionMensajeSistema = ex.Message;
                return Json(listaopcion.lista);
            }
        }
        [HttpPost]
        public IActionResult ListarComboOpcionPadre()
        {
            int IdModulo = Convert.ToInt32(Request.Form["IdModulo"]);
            Sistema.Opcion opcion = new Sistema.Opcion();
            Sistema.ListaOpcion listaopcion = new Sistema.ListaOpcion();
            try
            {
               
                    listaopcion = iopcion.ListarComboOpcionPadre( IdModulo);
              
                var data = listaopcion.lista.Select(lq => new
                {
                    IdOpcion = lq.IdOpcion,
                    NombreOpcion = lq.NombreOpcion
                }).ToList();

                return Json(data);
            }
            catch (Exception ex)
            {
                listaopcion.mensaje.CodigoMensaje = 1;
                listaopcion.mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA EN LA ACCION [ListarComboOpcionPadre] DEL CONTROLADOR Opcion, COMUNIQUESE CON EL ADMINISTRADOR DEL SISTEMA";
                listaopcion.mensaje.DescripcionMensajeSistema = ex.Message;
                return Json(listaopcion.lista);
            }
        }
    }
}









