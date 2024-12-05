	
using DataTables.AspNet.Core; using Microsoft.AspNetCore.Http;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Seguridad;
using MatrixService;
using DataTables.AspNet.AspNetCore;
namespace MatrixWeb.Controllers
{
    //[ValidarUsuario]
    public class UsuarioPerfilController : Controller
    {
        private ISvcUsuario iusuario;
        private ISvcUsuarioPerfil iusuarioperfil;
        public UsuarioPerfilController(ISvcUsuario _iusuario,  ISvcUsuarioPerfil _iusuarioperfil)
        {
            iusuario = _iusuario;
            iusuarioperfil = _iusuarioperfil;
        }
        [HttpGet]
        public IActionResult IndexUsuarioPerfil()
        {
            int IdUsuario = Convert.ToInt32(Request.Query["IdUsuario"]);
            ViewBag.IdUsuario = IdUsuario;
            return View();
        }
        [HttpPost]
        public IActionResult ListarUsuarioPerfil(IDataTablesRequest requestModel)
        {
            int IdUsuario = Convert.ToInt32(Request.Form["IdUsuario"]);

            Seguridad.ListaUsuarioPerfil listausuarioperfil = new Seguridad.ListaUsuarioPerfil();
            
            int numeroPagina = (requestModel.Start / requestModel.Length) + 1;
            try
            {
               
                    listausuarioperfil = iusuarioperfil.ListarUsuarioPerfil( IdUsuario, Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")), null, null, numeroPagina, requestModel.Length, requestModel.Search.Value);
 
                if (listausuarioperfil.mensaje.CodigoMensaje > 0)
                {
                    return RedirectToAction("PaginaError", new { DescripcionError = listausuarioperfil.mensaje.DescripcionMensaje });
                }
                var totalCount = listausuarioperfil.paginacion.TotalRegistros;
                var data = listausuarioperfil.lista.Select(lq => new
                {

                    IdUsuarioPerfil = lq.IdUsuarioPerfil,
                    NombreEmpleadoPerfil = lq.empleadoperfil.NombreEmpleadoPerfil,
                    NombrePerfil = lq.perfil.NombrePerfil,
                    Activo = (lq.Activo == true ? "Si" : "No")
                }).ToList();
               // return Json(new DataTablesResponse(requestModel.Draw, data, totalCount, totalCount), JsonRequestBehavior.AllowGet);
               var jsonData = new { draw = requestModel.Draw, recordsFiltered = totalCount, recordsTotal = totalCount, data = data }; return Ok(jsonData);
            }
            catch (Exception ex)
            {
                return RedirectToAction("PaginaError", new { DescripcionError = "Hubo un error [" + ex.Message + "]" });
            }
        }
        [HttpPost]
        public IActionResult ListarComboUsuarioPerfil()
        {
            int IdUsuario = Convert.ToInt32(Request.Form["IdUsuario"]);
            Seguridad.ListaUsuarioPerfil listausuarioperfil = new Seguridad.ListaUsuarioPerfil();
            
         
            try
            {
               
                    listausuarioperfil = iusuarioperfil.ListarComboUsuarioPerfil( IdUsuario);
                

                if (listausuarioperfil.mensaje.CodigoMensaje > 0)
                {
                    return RedirectToAction("PaginaError", new { DescripcionError = listausuarioperfil.mensaje.DescripcionMensaje });
                }
                var totalCount = listausuarioperfil.paginacion.TotalRegistros;
                var data = listausuarioperfil.lista.Select(lq => new
                {
                    IdEmpleadoPerfil = lq.empleadoperfil.IdEmpleadoPerfil,
                    NombreEmpleadoPerfil = lq.empleadoperfil.NombreEmpleadoPerfil
                }).ToList();

                return Json(data);
            }
            catch (Exception ex)
            {
                return RedirectToAction("PaginaError", new { DescripcionError = "Hubo un error [" + ex.Message + "]" });
            }
        }
        [HttpPost]
        public IActionResult RegistroUsuarioPerfil()
        {
            int IdUsuario = Convert.ToInt32(Request.Form["IdUsuario"]);
            var usuarioperfil = new UsuarioPerfil();
            var usuario = new Usuario();
            int IdUsuarioPerfil = Convert.ToInt32(Request.Form["IdUsuarioPerfil"]);
            try
            {          
                if (IdUsuarioPerfil > 0)
                {
                   
                        usuarioperfil = iusuarioperfil.ObtenerUsuarioPerfil( IdUsuarioPerfil);
                    
                }                
                usuario = iusuario.ObtenerUsuario( IdUsuario);
                usuarioperfil.usuario = usuario;
                return View(usuarioperfil);
            }
            catch (Exception ex)
            {
                usuarioperfil.mensaje.CodigoMensaje = 1;
                usuarioperfil.mensaje.DescripcionMensaje = ex.Message;
                return View(usuarioperfil);
            }
        }
        [HttpPost]
        public IActionResult GuardarRegistroUsuarioPerfil()
        {
            int IdUsuarioPerfil = Convert.ToInt32(Request.Form["txtRegIdUsuarioPerfil"]);
            int IdUsuario = Convert.ToInt32(Request.Form["IdUsuario"]);

            int IdEmpleadoPerfil = Convert.ToInt32(Request.Form["cmbEmpleadoUsuarioPerfil"]);

            int IdPerfil = Convert.ToInt32(Request.Form["cmbPerfil"]);
            bool Activo = Convert.ToBoolean(Convert.ToInt32(Request.Form["optActivo"]));
            UsuarioPerfil usuarioperfil = new UsuarioPerfil();


            try
            {
               
                    usuarioperfil = iusuarioperfil.GuardaUsuarioPerfil(                
                                        IdUsuarioPerfil,
                                                IdUsuario,
                                                IdEmpleadoPerfil,
                                                IdPerfil,
                                                Activo,
                                        Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario"))
                );
            
                return Json(usuarioperfil);
            }
            catch (Exception ex)
            {
                return RedirectToAction("PaginaError", new { DescripcionError = "Hubo un error [" + ex.Message + "]" });
            }
        }
        [HttpPost]
        public IActionResult EliminarUsuarioPerfil()
        {
            int IdUsuarioPerfil = Convert.ToInt32(Request.Form["IdUsuarioPerfil"]);            
            Utilitarios.Mensaje mensaje = new Utilitarios.Mensaje();
            try
            {
               
                    mensaje = iusuarioperfil.EliminarUsuarioPerfil( IdUsuarioPerfil, Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));
                
                return Json(mensaje);
            }
            catch (Exception ex)
            {
                return RedirectToAction("PaginaError", new { DescripcionError = "Hubo un error [" + ex.Message + "]" });
            }
        }
    }
}








		