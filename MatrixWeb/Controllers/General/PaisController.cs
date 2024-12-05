
using DataTables.AspNet.Core; using Microsoft.AspNetCore.Http;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using MatrixService;
using DataTables.AspNet.AspNetCore;
namespace MatrixWeb.Controllers
{
    //[ValidarUsuario]
    public class PaisController : Controller
    {
        private readonly ISvcPais ipais;
        public PaisController(ISvcPais _ipais)
        {
            ipais = _ipais;
        }
        [HttpGet]
        public IActionResult IndexPais()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ListarPais(IDataTablesRequest requestModel)
        {
            General.ListaPais listapais = new General.ListaPais();
            General.Pais pais = new General.Pais();
            int numeroPagina = (requestModel.Start / requestModel.Length) + 1;
            try
            {
                listapais = ipais.ListarPais( Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")), null, null, numeroPagina, requestModel.Length, requestModel.Search.Value);
                if (listapais.mensaje.CodigoMensaje > 0)
                {
                    return RedirectToAction("PaginaError", new { DescripcionError = listapais.mensaje.DescripcionMensaje });
                }
                var totalCount = listapais.paginacion.TotalRegistros;
                var data = listapais.lista.Select(lq => new
                {

                    IdPais = lq.IdPais,

                    NombrePais = lq.NombrePais,

                    Gentilicio = lq.Gentilicio,

                    Alfa3 = lq.Alfa3,

                    Alfa2 = lq.Alfa2,

                    Bandera = lq.Bandera,





                }).ToList();

                var jsonData = new { draw = requestModel.Draw, recordsFiltered = totalCount, recordsTotal = totalCount, data = data }; return Ok(jsonData);
            }
            catch (Exception ex)
            {
                return RedirectToAction("PaginaError", new { DescripcionError = "Hubo un error [" + ex.Message + "]" });
            }
        }
        [HttpPost]
        public IActionResult RegistroPais()
        {
            int IdPais = Convert.ToInt32(Request.Form["IdPais"]);
            General.Pais pais = new General.Pais();
            try
            {


                if (IdPais > 0)
                {
                    pais = ipais.ObtenerPais( IdPais, Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));
                }
                return View(pais);
            }
            catch (Exception ex)
            {
                pais.mensaje.CodigoMensaje = 1;
                pais.mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA AL GUARDAR, COMUNIQUESE CON EL ADMINISTRADOR DEL SISTEMA";
                pais.mensaje.DescripcionMensajeSistema = ex.Message;
                return View(pais);
            }
        }
        [HttpPost]
        public IActionResult GuardarRegistroPais()
        {
            int IdPais = Convert.ToInt32(Request.Form["txtRegIdPais"]);
            string NombrePais = Request.Form["txtNombrePais"];
            string Gentilicio = Request.Form["txtGentilicio"];
            string Alfa3 = Request.Form["txtAlfa3"];
            string Alfa2 = Request.Form["txtAlfa2"];
            string Bandera = Request.Form["txtBandera"];


            General.Pais pais = new General.Pais();
            try
            {
                pais = ipais.GuardaPais(
            
                                    IdPais,
                                            NombrePais,
                                            Gentilicio,
                                            Alfa3,
                                            Alfa2,
                                            Bandera,
                                    Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));
                return Json(pais);
            }
            catch (Exception ex)
            {
                pais.mensaje.CodigoMensaje = 1;
                pais.mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA AL GUARDAR, COMUNIQUESE CON EL ADMINISTRADOR DEL SISTEMA";
                pais.mensaje.DescripcionMensajeSistema = ex.Message;
                return Json(pais);
            }
        }
        [HttpPost]
        public IActionResult EliminarPais()
        {
            int IdPais = Convert.ToInt32(Request.Form["IdPais"]);
            Utilitarios.Mensaje mensaje = new Utilitarios.Mensaje();
            try
            {
                mensaje = ipais.EliminarPais( IdPais, Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));
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
        public IActionResult ListarComboPais()
        {
            General.Pais pais = new General.Pais();
            General.ListaPais listapais = new General.ListaPais();
            try
            {
                listapais = ipais.ListarComboPais( Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));
                return Json(listapais);
            }
            catch (Exception ex)
            {
                listapais.mensaje.CodigoMensaje = 1;
                listapais.mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA EN LA ACCION [ListarComboPais] DEL CONTROLADOR Pais , COMUNIQUESE CON EL ADMINISTRADOR DEL SISTEMA";
                listapais.mensaje.DescripcionMensajeSistema = ex.Message;
                return Json(listapais);
            }
        }
    }
}









