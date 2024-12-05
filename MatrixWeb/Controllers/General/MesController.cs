using DataTables.AspNet.AspNetCore;
using DataTables.AspNet.Core; using Microsoft.AspNetCore.Http;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using MatrixService;
namespace MatrixWeb.Controllers
{
    //[ValidarUsuario]
    public class MesController : Controller
    {
        private ISvcMes imes;
        public MesController(ISvcMes _imes)
        {
            imes = _imes;
        }
        [HttpGet]
        public IActionResult IndexMes()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ListarMes(IDataTablesRequest requestModel)
        {
            General.ListaMes listames = new General.ListaMes();
            General.Mes mes = new General.Mes();
            int numeroPagina = (requestModel.Start / requestModel.Length) + 1;
            try
            {
                listames = imes.ListarMes( Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")), null, null, numeroPagina, requestModel.Length, requestModel.Search.Value);
                if (listames.mensaje.CodigoMensaje > 0)
                {
                    return RedirectToAction("PaginaError", new { DescripcionError = listames.mensaje.DescripcionMensaje });
                }
                var totalCount = listames.paginacion.TotalRegistros;
                var data = listames.lista.Select(lq => new
                {

                    IdMes = lq.IdMes,

                    NombreMes = lq.NombreMes,

                    NominacionAbreviada = lq.NominacionAbreviada,
                }).ToList();

                var jsonData = new { draw = requestModel.Draw, recordsFiltered = totalCount, recordsTotal = totalCount, data = data }; return Ok(jsonData);
            }
            catch (Exception ex)
            {
                return RedirectToAction("PaginaError", new { DescripcionError = "Hubo un error [" + ex.Message + "]" });
            }
        }
        [HttpPost]
        public IActionResult RegistroMes()
        {
            int IdMes = Convert.ToInt32(Request.Form["IdMes"]);
            General.Mes mes = new General.Mes();
            try
            {


                if (IdMes > 0)
                {
                    mes = imes.ObtenerMes( IdMes, Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));
                }
                return View(mes);
            }
            catch (Exception ex)
            {
                mes.mensaje.CodigoMensaje = 1;
                mes.mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA AL GUARDAR, COMUNIQUESE CON EL ADMINISTRADOR DEL SISTEMA";
                mes.mensaje.DescripcionMensajeSistema = ex.Message;
                return View(mes);
            }
        }
        [HttpPost]
        public IActionResult GuardarRegistroMes()
        {
            int IdMes = Convert.ToInt32(Request.Form["txtRegIdMes"]);
            string NombreMes = Request.Form["txtNombreMes"];
            string NominacionAbreviada = Request.Form["txtNominacionAbreviada"];


            General.Mes mes = new General.Mes();
            try
            {
                mes = imes.GuardaMes(
            
                                    IdMes,
                                            NombreMes,
                                            NominacionAbreviada,
                                    Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));
                return Json(mes);
            }
            catch (Exception ex)
            {
                mes.mensaje.CodigoMensaje = 1;
                mes.mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA AL GUARDAR, COMUNIQUESE CON EL ADMINISTRADOR DEL SISTEMA";
                mes.mensaje.DescripcionMensajeSistema = ex.Message;
                return Json(mes);
            }
        }
        [HttpPost]
        public IActionResult EliminarMes()
        {
            int IdMes = Convert.ToInt32(Request.Form["IdMes"]);
            Utilitarios.Mensaje mensaje = new Utilitarios.Mensaje();
            try
            {
                mensaje = imes.EliminarMes( IdMes, Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));
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
        public IActionResult ListarComboMes()
        {
            General.Mes mes = new General.Mes();
            General.ListaMes listames = new General.ListaMes();
            try
            {
                listames = imes.ListarComboMes( Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));
                return Json(listames);
            }
            catch (Exception ex)
            {
                listames.mensaje.CodigoMensaje = 1;
                listames.mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA EN LA ACCION [ListarComboMes] DEL CONTROLADOR Mes , COMUNIQUESE CON EL ADMINISTRADOR DEL SISTEMA";
                listames.mensaje.DescripcionMensajeSistema = ex.Message;
                return Json(listames);
            }
        }
    }
}









