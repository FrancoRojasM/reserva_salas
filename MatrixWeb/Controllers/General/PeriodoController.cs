using DataTables.AspNet.AspNetCore;
using DataTables.AspNet.Core; using Microsoft.AspNetCore.Http;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using MatrixService;
namespace MatrixWeb.Controllers
{
    //[ValidarUsuario]
    public class PeriodoController : Controller
    {
        private readonly ISvcPeriodo iperiodo;
        public PeriodoController(ISvcPeriodo _iperiodo)
        {
            iperiodo = _iperiodo;
        }
        [HttpGet]
        public IActionResult IndexPeriodo()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ListarPeriodo(IDataTablesRequest requestModel)
        {
            General.ListaPeriodo listaperiodo = new General.ListaPeriodo();
            General.Periodo periodo = new General.Periodo();
            int numeroPagina = (requestModel.Start / requestModel.Length) + 1;
            try
            {
                listaperiodo = iperiodo.ListarPeriodo( Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")), null, null, numeroPagina, requestModel.Length, requestModel.Search.Value);
                if (listaperiodo.mensaje.CodigoMensaje > 0)
                {
                    return RedirectToAction("PaginaError", new { DescripcionError = listaperiodo.mensaje.DescripcionMensaje });
                }
                var totalCount = listaperiodo.paginacion.TotalRegistros;
                var data = listaperiodo.lista.Select(lq => new
                {

                    IdPeriodo = lq.IdPeriodo,

                    NombrePeriodo = lq.NombrePeriodo,

                    DecenioNombrePeriodo = lq.DecenioNombrePeriodo,

                    Decenio = lq.Decenio,

                    Actual = (lq.Actual == true ? "Si" : "No"),




                }).ToList();

                var jsonData = new { draw = requestModel.Draw, recordsFiltered = totalCount, recordsTotal = totalCount, data = data }; return Ok(jsonData);
            }
            catch (Exception ex)
            {
                return RedirectToAction("PaginaError", new { DescripcionError = "Hubo un error [" + ex.Message + "]" });
            }
        }
        [HttpPost]
        public IActionResult RegistroPeriodo()
        {
            int IdPeriodo = Convert.ToInt32(Request.Form["IdPeriodo"]);
            General.Periodo periodo = new General.Periodo();
            try
            {


                if (IdPeriodo > 0)
                {
                    periodo = iperiodo.ObtenerPeriodo( IdPeriodo, Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));
                }
                return View(periodo);
            }
            catch (Exception ex)
            {
                periodo.mensaje.CodigoMensaje = 1;
                periodo.mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA AL GUARDAR, COMUNIQUESE CON EL ADMINISTRADOR DEL SISTEMA";
                periodo.mensaje.DescripcionMensajeSistema = ex.Message;
                return View(periodo);
            }
        }
        [HttpPost]
        public IActionResult GuardarRegistroPeriodo()
        {
            int IdPeriodo = Convert.ToInt32(Request.Form["txtRegIdPeriodo"]);
            string NombrePeriodo = Request.Form["txtNombrePeriodo"];
            string DecenioNombrePeriodo = Request.Form["txtDecenioNombrePeriodo"];
            string Decenio = Request.Form["txtDecenio"];
            bool Actual = Convert.ToBoolean(Convert.ToInt32(Request.Form["optActual"]));


            General.Periodo periodo = new General.Periodo();
            try
            {
                periodo = iperiodo.GuardaPeriodo(
            
                                    IdPeriodo,
                                            NombrePeriodo,
                                            DecenioNombrePeriodo,
                                            Decenio,
                                            Actual,
                                    Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));
                return Json(periodo);
            }
            catch (Exception ex)
            {
                periodo.mensaje.CodigoMensaje = 1;
                periodo.mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA AL GUARDAR, COMUNIQUESE CON EL ADMINISTRADOR DEL SISTEMA";
                periodo.mensaje.DescripcionMensajeSistema = ex.Message;
                return Json(periodo);
            }
        }
        [HttpPost]
        public IActionResult EliminarPeriodo()
        {
            int IdPeriodo = Convert.ToInt32(Request.Form["IdPeriodo"]);
            Utilitarios.Mensaje mensaje = new Utilitarios.Mensaje();
            try
            {
                mensaje = iperiodo.EliminarPeriodo( IdPeriodo, Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));
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
        public IActionResult ListarComboPeriodo()
        {
            General.Periodo periodo = new General.Periodo();
            General.ListaPeriodo listaperiodo = new General.ListaPeriodo();
            try
            {
                listaperiodo = iperiodo.ListarComboPeriodo( Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));
                return Json(listaperiodo);
            }
            catch (Exception ex)
            {
                listaperiodo.mensaje.CodigoMensaje = 1;
                listaperiodo.mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA EN LA ACCION [ListarComboPeriodo] DEL CONTROLADOR Periodo , COMUNIQUESE CON EL ADMINISTRADOR DEL SISTEMA";
                listaperiodo.mensaje.DescripcionMensajeSistema = ex.Message;
                return Json(listaperiodo);
            }
        }
        
        [HttpPost]
        public IActionResult ListarComboPeriodoProcesoEtapaPorResponsable()
        {
            General.Periodo periodo = new General.Periodo();
            General.ListaPeriodo listaperiodo = new General.ListaPeriodo();
            try
            {
                listaperiodo = iperiodo.ListarComboPeriodoProcesoEtapaPorResponsable(Convert.ToInt32(HttpContext.Session.GetString("IdEmpleadoPerfil")), Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));
                return Json(listaperiodo);
            }
            catch (Exception ex)
            {
                listaperiodo.mensaje.CodigoMensaje = 1;
                listaperiodo.mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA EN LA ACCION [ListarComboPeriodo] DEL CONTROLADOR Periodo , COMUNIQUESE CON EL ADMINISTRADOR DEL SISTEMA";
                listaperiodo.mensaje.DescripcionMensajeSistema = ex.Message;
                return Json(listaperiodo);
            }
        }
        [HttpPost]
        public IActionResult ListarComboPeriodoProcesoPorResponsable()
        {
            General.Periodo periodo = new General.Periodo();
            General.ListaPeriodo listaperiodo = new General.ListaPeriodo();
            try
            {
                listaperiodo = iperiodo.ListarComboPeriodoProcesoPorResponsable(Convert.ToInt32(HttpContext.Session.GetString("IdEmpleadoPerfil")), Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));
                return Json(listaperiodo);
            }
            catch (Exception ex)
            {
                listaperiodo.mensaje.CodigoMensaje = 1;
                listaperiodo.mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA EN LA ACCION [ListarComboPeriodo] DEL CONTROLADOR Periodo , COMUNIQUESE CON EL ADMINISTRADOR DEL SISTEMA";
                listaperiodo.mensaje.DescripcionMensajeSistema = ex.Message;
                return Json(listaperiodo);
            }
        }
        
    }
}









