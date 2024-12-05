	
using DataTables.AspNet.Core; using Microsoft.AspNetCore.Http;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using MatrixService;

namespace MatrixWeb.Controllers
{
    //[ValidarUsuario]
    public class EmpresaSedeAmbienteController : Controller
    {
        private ISvcEmpresaSedeAmbiente iempresasedeambiente;
        private ISvcEmpresaSede iempresasede;
        public EmpresaSedeAmbienteController(ISvcEmpresaSede _iempresasede, ISvcEmpresaSedeAmbiente _iempresasedeambiente)
        {
            iempresasede = _iempresasede;
            iempresasedeambiente = _iempresasedeambiente;
        }
        [HttpPost]
        public IActionResult IndexEmpresaSedeAmbiente()
        {
            int IdEmpresaSede = Convert.ToInt32(Request.Form["IdEmpresaSede"]);
            ViewBag.IdEmpresaSede = IdEmpresaSede;
            return View();
        }
        [HttpPost]
        public IActionResult ListarEmpresaSedeAmbiente(IDataTablesRequest requestModel)
        {
            int IdEmpresaSede = Convert.ToInt32(Request.Form["IdEmpresaSede"]);
            General.ListaEmpresaSedeAmbiente listaempresasedeambiente = new General.ListaEmpresaSedeAmbiente();
            General.EmpresaSedeAmbiente empresasedeambiente = new General.EmpresaSedeAmbiente();
            int numeroPagina = (requestModel.Start / requestModel.Length) + 1;
            try
            {
                listaempresasedeambiente = iempresasedeambiente.ListarEmpresaSedeAmbiente( IdEmpresaSede, Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")), null, null, numeroPagina, requestModel.Length, requestModel.Search.Value);
                if (listaempresasedeambiente.mensaje.CodigoMensaje > 0)
                {
                    return RedirectToAction("PaginaError", new { DescripcionError = listaempresasedeambiente.mensaje.DescripcionMensaje });
                }
                var totalCount = listaempresasedeambiente.paginacion.TotalRegistros;
                var data = listaempresasedeambiente.lista.Select(lq => new
                {
                    lq.IdEmpresaSedeAmbiente,
                    EmpresaSede = lq.empresasede.NombreSede,
                    lq.CodigoAmbiente,
                    lq.NombreAmbiente,
                    lq.DescripcionAmbiente,
                    Activo = (lq.Activo == true ? "Si" : "No")
                }).ToList();

                var jsonData = new { draw = requestModel.Draw, recordsFiltered = totalCount, recordsTotal = totalCount, data = data }; return Ok(jsonData);
            }
            catch (Exception ex)
            {
                return RedirectToAction("PaginaError", new { DescripcionError = "Hubo un error [" + ex.Message + "]" });
            }
        }
        [HttpPost]
        public IActionResult RegistroEmpresaSedeAmbiente()
        {
            int IdEmpresaSede = Convert.ToInt32(Request.Form["IdEmpresaSede"]);
            int IdEmpresaSedeAmbiente = Convert.ToInt32(Request.Form["IdEmpresaSedeAmbiente"]);
            try
            {
                General.EmpresaSedeAmbiente empresasedeambiente = new General.EmpresaSedeAmbiente();
                General.EmpresaSede empresasede = new General.EmpresaSede();

                if (IdEmpresaSedeAmbiente > 0)
                {
                    empresasedeambiente = iempresasedeambiente.ObtenerEmpresaSedeAmbiente( IdEmpresaSedeAmbiente, Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));
                }

                empresasede = iempresasede.ObtenerEmpresaSede( IdEmpresaSede, Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));
                empresasedeambiente.empresasede = empresasede;
                return View(empresasedeambiente);
            }
            catch (Exception ex)
            {
                return RedirectToAction("PaginaError", new { DescripcionError = "Hubo un error [" + ex.Message + "]" });
            }
        }
        [HttpPost]
        public IActionResult GuardarRegistroEmpresaSedeAmbiente()
        {
            int IdEmpresaSedeAmbiente = Convert.ToInt32(Request.Form["txtRegIdEmpresaSedeAmbiente"]);
            int IdEmpresaSede = Convert.ToInt32(Request.Form["IdEmpresaSede"]);
            string CodigoAmbiente = Request.Form["txtCodigoAmbiente"];
            string NombreAmbiente = Request.Form["txtNombreAmbiente"];
            string DescripcionAmbiente = Request.Form["txtDescripcionAmbiente"];
            bool Activo = Convert.ToBoolean(Convert.ToInt32(Request.Form["optActivo"]));


            General.EmpresaSedeAmbiente empresasedeambiente = new General.EmpresaSedeAmbiente();
            try
            {
                empresasedeambiente = iempresasedeambiente.GuardaEmpresaSedeAmbiente(
            
                                    IdEmpresaSedeAmbiente,
                                            IdEmpresaSede,
                                            CodigoAmbiente,
                                            NombreAmbiente,
                                            DescripcionAmbiente,
                                            Activo,
                                    Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));
                return Json(empresasedeambiente);
            }
            catch (Exception ex)
            {
                empresasedeambiente.mensaje.CodigoMensaje = 1;
                empresasedeambiente.mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA AL GUARDAR, COMUNIQUESE CON EL ADMINISTRADOR DEL SISTEMA";
                empresasedeambiente.mensaje.DescripcionMensajeSistema = ex.Message;
                return Json(empresasedeambiente);
            }
        }
        [HttpPost]
        public IActionResult EliminarEmpresaSedeAmbiente()
        {
            int IdEmpresaSedeAmbiente = Convert.ToInt32(Request.Form["IdEmpresaSedeAmbiente"]);
            General.EmpresaSedeAmbiente empresasedeambiente = new General.EmpresaSedeAmbiente();
            Utilitarios.Mensaje mensaje = new Utilitarios.Mensaje();
            try
            {
                mensaje = iempresasedeambiente.EliminarEmpresaSedeAmbiente( IdEmpresaSedeAmbiente, Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));
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
        public IActionResult ListarComboEmpresaSedeAmbiente()
        {
            General.EmpresaSedeAmbiente empresasedeambiente = new General.EmpresaSedeAmbiente();
            General.ListaEmpresaSedeAmbiente listaempresasedeambiente = new General.ListaEmpresaSedeAmbiente();
            try
            {
                listaempresasedeambiente = iempresasedeambiente.ListarComboEmpresaSedeAmbiente( Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));
                var data = listaempresasedeambiente.lista.Select(lq => new
                {
                    lq.IdEmpresaSedeAmbiente,                   
                    lq.NombreAmbiente                    
                }).ToList();
                return Json(data);
            }
            catch (Exception ex)
            {
                listaempresasedeambiente.mensaje.CodigoMensaje = 1;
                listaempresasedeambiente.mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA EN LA ACCION [ListarComboEmpresaSedeAmbiente] DEL CONTROLADOR EmpresaSedeAmbiente , COMUNIQUESE CON EL ADMINISTRADOR DEL SISTEMA";
                listaempresasedeambiente.mensaje.DescripcionMensajeSistema = ex.Message;
                return Json(listaempresasedeambiente);
            }
        }
    }
}









		