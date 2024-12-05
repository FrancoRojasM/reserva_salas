using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;

namespace MatrixWeb.Controllers
{
    public class ErroresController : Controller
    {
        public ErroresController()
        {

        }
       [HttpGet]
        public IActionResult PaginaError()
        {
            string DescripcionMensaje = Request.Query["DescripcionMensaje"];
            string DescripcionMensajeSistema = Request.Query["DescripcionMensajeSistema"];
            ViewBag.DescripcionMensajeSistema = DescripcionMensajeSistema;
            ViewBag.DescripcionMensaje = DescripcionMensaje;
            return View();
        }
    }
}