
using DataTables.AspNet.Core; using Microsoft.AspNetCore.Http;
using System;
using Microsoft.AspNetCore.Mvc;
using DataTables.AspNet.AspNetCore;
using System.Linq;
using MatrixService;
namespace MatrixWeb.Controllers
{
    //[ValidarUsuario]
    public class PerfilController : Controller
    {
        private ISvcPerfil iperfil;
        public PerfilController(ISvcPerfil _iperfil)
        {
            iperfil = _iperfil;
        }
        [HttpGet]
        public IActionResult IndexPerfil()
        {
            var conpermiso = MatrixUtilitarios.Utilitario.TienePermisoControladorAccion(HttpContext.Request.RouteValues["controller"].ToString(), HttpContext.Request.RouteValues["action"].ToString()); if (conpermiso == -1) { return RedirectToAction("Login", "Usuario"); }
            return View();
        }
        [HttpPost]
        public IActionResult ListarPerfil(IDataTablesRequest requestModel)
        {

            Seguridad.ListaPerfil listaperfil = new Seguridad.ListaPerfil();
            Seguridad.Perfil perfil = new Seguridad.Perfil();
            int numeroPagina = (requestModel.Start / requestModel.Length) + 1;
            try
            {
               
                    listaperfil = iperfil.ListarPerfil( Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")), null, null, numeroPagina, requestModel.Length, requestModel.Search.Value);
              

                if (listaperfil.mensaje.CodigoMensaje > 0)
                {
                    return RedirectToAction("PaginaError", new { DescripcionError = listaperfil.mensaje.DescripcionMensaje });
                }
                var totalCount = listaperfil.paginacion.TotalRegistros;
                var data = listaperfil.lista.Select(lq => new
                {

                    IdPerfil = lq.IdPerfil,

                    NombrePerfil = lq.NombrePerfil,

                    DetallePerfil = lq.DetallePerfil,

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
        public IActionResult RegistroPerfil()
        {

            int IdPerfil = Convert.ToInt32(Request.Form["IdPerfil"]);
            try
            {
                Seguridad.Perfil perfil = new Seguridad.Perfil();

                if (IdPerfil > 0)
                {
                   
                        perfil = iperfil.ObtenerPerfil( IdPerfil);
                    
                }
                return View(perfil);
            }
            catch (Exception ex)
            {
                return RedirectToAction("PaginaError", new { DescripcionError = "Hubo un error [" + ex.Message + "]" });
            }
        }
        [HttpPost]
        public IActionResult GuardarRegistroPerfil()
        {
            int IdPerfil = Convert.ToInt32(Request.Form["txtRegIdPerfil"]);
            string NombrePerfil = Request.Form["txtNombrePerfil"];
            string DetallePerfil = Request.Form["txtDetallePerfil"];
            bool Activo = Convert.ToBoolean(Convert.ToInt32(Request.Form["optActivo"]));


            Seguridad.Perfil perfil = new Seguridad.Perfil();
            try
            {
               
                    perfil = iperfil.GuardaPerfil(
                
                                        IdPerfil,
                                                NombrePerfil,
                                                DetallePerfil,
                                                Activo,
                                        Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario"))
                );
               
                return Json(perfil);
            }
            catch (Exception ex)
            {
                perfil.mensaje.CodigoMensaje = 1;
                perfil.mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA AL GUARDAR, COMUNIQUESE CON EL ADMINISTRADOR DEL SISTEMA";
                perfil.mensaje.DescripcionMensajeSistema = ex.Message;
                return Json(perfil);
            }
        }
        [HttpPost]
        public IActionResult EliminarPerfil()
        {
            int IdPerfil = Convert.ToInt32(Request.Form["IdPerfil"]);

            Seguridad.Perfil perfil = new Seguridad.Perfil();
            Utilitarios.Mensaje mensaje = new Utilitarios.Mensaje();
            try
            {
               
                    mensaje = iperfil.EliminarPerfil( IdPerfil, Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));
               
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
        public IActionResult ListarComboPerfil()
        {
            Seguridad.Perfil perfil = new Seguridad.Perfil();
            Seguridad.ListaPerfil listaperfil = new Seguridad.ListaPerfil();
            try
            {
               
                    listaperfil = iperfil.ListarComboPerfil();
                return Json(listaperfil);
            }
            catch (Exception ex)
            {
                listaperfil.mensaje.CodigoMensaje = 1;
                listaperfil.mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA EN LA ACCION [ListarComboPerfil] DEL CONTROLADOR Perfil , COMUNIQUESE CON EL ADMINISTRADOR DEL SISTEMA";
                listaperfil.mensaje.DescripcionMensajeSistema = ex.Message;
                return Json(listaperfil.lista);
            }
        }
    }
}









