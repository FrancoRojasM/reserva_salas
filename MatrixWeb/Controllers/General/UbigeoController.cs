
using DataTables.AspNet.Core; using Microsoft.AspNetCore.Http;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using MatrixService;
using Microsoft.AspNetCore.Authorization;
namespace MatrixWeb.Controllers
{
    ////[ValidarUsuario]
    public class UbigeoController : Controller
    {
        private ISvcUbigeo iubigeo;
        public UbigeoController(ISvcUbigeo _iubigeo)
        {
            iubigeo = _iubigeo;
        }
        [HttpGet]
        public IActionResult IndexUbigeo()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult ListarComboProvincia()
        {
            int CodigoDepartamento = Convert.ToInt32(Request.Form["CodigoDepartamento"]);
            General.ListaUbigeo listaubigeo = new General.ListaUbigeo();

            try
            {

                listaubigeo = iubigeo.ListarComboProvincia( CodigoDepartamento);


                if (listaubigeo.mensaje.CodigoMensaje > 0)
                {
                    return RedirectToAction("PaginaError", new { DescripcionError = listaubigeo.mensaje.DescripcionMensaje });
                }

                var data = listaubigeo.lista.Select(lq => new
                {
                    CodigoProvincia = lq.CodigoProvincia,
                    Provincia = lq.Provincia
                }).ToList();

                return Json(data);
            }
            catch (Exception ex)
            {
                return RedirectToAction("PaginaError", new { DescripcionError = "Hubo un error [" + ex.Message + "]" });
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult ListarComboDistrito()
        {
            int CodigoDepartamento = Convert.ToInt32(Request.Form["CodigoDepartamento"]);
            int CodigoProvincia = Convert.ToInt32(Request.Form["CodigoProvincia"]);
            General.ListaUbigeo listaubigeo = new General.ListaUbigeo();

            try
            {

                listaubigeo = iubigeo.ListarComboDistrito( CodigoDepartamento, CodigoProvincia);


                if (listaubigeo.mensaje.CodigoMensaje > 0)
                {
                    return RedirectToAction("PaginaError", new { DescripcionError = listaubigeo.mensaje.DescripcionMensaje });
                }

                var data = listaubigeo.lista.Select(lq => new
                {
                    IdUbigeo = lq.IdUbigeo,
                    lq.CodigoDistrito,
                    Distrito = lq.Distrito
                }).ToList();

                return Json(data);
            }
            catch (Exception ex)
            {
                return RedirectToAction("PaginaError", new { DescripcionError = "Hubo un error [" + ex.Message + "]" });
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult ListarComboDepartamento()
        {

            General.ListaUbigeo listaubigeo = new General.ListaUbigeo();

            try
            {

                listaubigeo = iubigeo.ListarComboDepartamento();

                if (listaubigeo.mensaje.CodigoMensaje > 0)
                {
                    return RedirectToAction("PaginaError", new { DescripcionError = listaubigeo.mensaje.DescripcionMensaje });
                }

                var data = listaubigeo.lista.Select(lq => new
                {
                    CodigoDepartamento = lq.CodigoDepartamento,
                    Departamento = lq.Departamento
                }).ToList();

                return Json(data);
            }
            catch (Exception ex)
            {
                return RedirectToAction("PaginaError", new { DescripcionError = "Hubo un error [" + ex.Message + "]" });
            }
        }
    }
}









