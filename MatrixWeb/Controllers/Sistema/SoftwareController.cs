
using DataTables.AspNet.Core; using Microsoft.AspNetCore.Http;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using MatrixService;
using Sistema;
using DataTables.AspNet.AspNetCore;
namespace MatrixWeb.Controllers
{
    //[ValidarUsuario]
    public class SoftwareController : Controller
    {
        private readonly ISvcSoftware isoftware;
        public SoftwareController(ISvcSoftware _isoftware)
        {
            isoftware = _isoftware;
        }
        [HttpGet]
        public IActionResult IndexSoftware()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ListarSoftware(IDataTablesRequest requestModel)
        {
            ListaSoftware listasoftware = new ListaSoftware();
            Software software = new Software();
            int numeroPagina = (requestModel.Start / requestModel.Length) + 1;
            try
            {
                listasoftware = isoftware.ListarSoftware(Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")), null, null, numeroPagina, requestModel.Length, requestModel.Search.Value);
                if (listasoftware.mensaje.CodigoMensaje > 0)
                {
                    return RedirectToAction("PaginaError", new { DescripcionError = listasoftware.mensaje.DescripcionMensaje });
                }
                var totalCount = listasoftware.paginacion.TotalRegistros;
                var data = listasoftware.lista.Select(lq => new
                {

                    IdSoftware = lq.IdSoftware,

                    NombreLargoSoftware = lq.NombreLargoSoftware,

                    NombreCortoSoftware = lq.NombreCortoSoftware,

                    NumeroVersionSoftware = lq.NumeroVersionSoftware,

                    RutaImagenSoftware = lq.RutaImagenSoftware,

                    RutaImagenLogoSoftware = lq.RutaImagenLogoSoftware,

                    NombreLargoEmpresa = lq.NombreLargoEmpresa,

                    NombreCortoEmpresa = lq.NombreCortoEmpresa,





                }).ToList();

                var jsonData = new { draw = requestModel.Draw, recordsFiltered = totalCount, recordsTotal = totalCount, data = data }; return Ok(jsonData);
            }
            catch (Exception ex)
            {
                return RedirectToAction("PaginaError", new { DescripcionError = "Hubo un error [" + ex.Message + "]" });
            }
        }
        [HttpPost]
        public IActionResult RegistroSoftware()
        {
            int IdSoftware = Convert.ToInt32(Request.Form["IdSoftware"]);
            Software software = new Software();
            try
            {


                if (IdSoftware > 0)
                {
                    software = isoftware.ObtenerSoftware(IdSoftware, Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));
                }
                return View(software);
            }
            catch (Exception ex)
            {
                software.mensaje.CodigoMensaje = 1;
                software.mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA AL GUARDAR, COMUNIQUESE CON EL ADMINISTRADOR DEL SISTEMA";
                software.mensaje.DescripcionMensajeSistema = ex.Message;
                return View(software);
            }
        }
        [HttpPost]
        public IActionResult GuardarRegistroSoftware()
        {
            int IdSoftware = Convert.ToInt32(Request.Form["txtRegIdSoftware"]);
            string NombreLargoSoftware = Request.Form["txtNombreLargoSoftware"];
            string NombreCortoSoftware = Request.Form["txtNombreCortoSoftware"];
            string NumeroVersionSoftware = Request.Form["txtNumeroVersionSoftware"];
            string RutaImagenSoftware = Request.Form["txtRutaImagenSoftware"];
            string RutaImagenLogoSoftware = Request.Form["txtRutaImagenLogoSoftware"];
            string NombreLargoEmpresa = Request.Form["txtNombreLargoEmpresa"];
            string NombreCortoEmpresa = Request.Form["txtNombreCortoEmpresa"];


            Software software = new Software();
            try
            {
                software = isoftware.GuardaSoftware(

                                    IdSoftware,
                                            NombreLargoSoftware,
                                            NombreCortoSoftware,
                                            NumeroVersionSoftware,
                                            RutaImagenSoftware,
                                            RutaImagenLogoSoftware,
                                            NombreLargoEmpresa,
                                            NombreCortoEmpresa,
                                    Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));
                return Json(software);
            }
            catch (Exception ex)
            {
                software.mensaje.CodigoMensaje = 1;
                software.mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA AL GUARDAR, COMUNIQUESE CON EL ADMINISTRADOR DEL SISTEMA";
                software.mensaje.DescripcionMensajeSistema = ex.Message;
                return Json(software);
            }
        }
        [HttpPost]
        public IActionResult EliminarSoftware()
        {
            int IdSoftware = Convert.ToInt32(Request.Form["IdSoftware"]);
            Utilitarios.Mensaje mensaje = new Utilitarios.Mensaje();
            try
            {
                mensaje = isoftware.EliminarSoftware(IdSoftware, Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));
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









