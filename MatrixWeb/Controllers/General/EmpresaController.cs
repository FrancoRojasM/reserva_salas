
using DataTables.AspNet.Core; using Microsoft.AspNetCore.Http;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using MatrixService;
using System.Threading.Tasks;
using DataTables.AspNet.AspNetCore;
namespace MatrixWeb.Controllers
{
    //[ValidarUsuario]
    public class EmpresaController : Controller
    { 
        private ISvcEmpresa iempresa;
        public EmpresaController(ISvcEmpresa _iempresa)
        {
            iempresa = _iempresa;
        }
        [HttpGet] 
        public IActionResult IndexEmpresa()
        {
            return View();
        }
        [HttpGet]
        public IActionResult EmpresaPrincipal()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ListarEmpresaPrincipal(IDataTablesRequest requestModel)
        {

            General.ListaEmpresa listaempresa = new General.ListaEmpresa();
            General.Empresa empresa = new General.Empresa();
            int numeroPagina = (requestModel.Start / requestModel.Length) + 1;
            try
            {
                listaempresa =await iempresa.ListarEmpresaPrincipalAsync( Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")), null, null, numeroPagina, requestModel.Length, requestModel.Search.Value);
                if (listaempresa.mensaje.CodigoMensaje > 0)
                {
                    return RedirectToAction("PaginaError", new { DescripcionError = listaempresa.mensaje.DescripcionMensaje });
                }
                var totalCount = listaempresa.paginacion.TotalRegistros;
                var data = listaempresa.lista.Select(lq => new
                {
                    IdEmpresa = lq.IdEmpresa,
                    NombreCompleto = lq.persona.NombreCompleto,
                    NombreEmpresa = lq.NombreEmpresa,                  
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
        public async Task<IActionResult> ListarEmpresa(IDataTablesRequest requestModel)
        {
            int IdEmpresaPadre = Convert.ToInt32(HttpContext.Session.GetString("IdEmpresaPadre") == null ? 0 : HttpContext.Session.GetString("IdEmpresaPadre"));
            General.ListaEmpresa listaempresa = new General.ListaEmpresa();
            General.Empresa empresa = new General.Empresa();
            if (IdEmpresaPadre == 0) { IdEmpresaPadre = 1; }
            int numeroPagina = (requestModel.Start / requestModel.Length) + 1;
            try
            {              
                listaempresa =await iempresa.ListarEmpresaAsync( Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")), IdEmpresaPadre,null, null, numeroPagina, requestModel.Length, requestModel.Search.Value);
                if (listaempresa.mensaje.CodigoMensaje > 0)
                {
                    return RedirectToAction("PaginaError", new { DescripcionError = listaempresa.mensaje.DescripcionMensaje });
                }
                var totalCount = listaempresa.paginacion.TotalRegistros;
                var data = listaempresa.lista.Select(lq => new
                {
                    IdEmpresa = lq.IdEmpresa,
                    NombreCompleto = lq.persona.NombreCompleto,
                    NombreEmpresa = lq.NombreEmpresa,
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
        public async Task<IActionResult> RegistroEmpresaPrincipal()
        {

            int IdEmpresa = Convert.ToInt32(Request.Form["IdEmpresa"]);
            try
            {
                General.Empresa empresa = new General.Empresa();
                if (IdEmpresa > 0)
                {
                    empresa = await iempresa.ObtenerEmpresaAsync(IdEmpresa);
                }
                return View(empresa);
            }
            catch (Exception ex)
            {
                return RedirectToAction("PaginaError", new { DescripcionError = "Hubo un error [" + ex.Message + "]" });
            }
        }
        //[HttpPost]
        //public IActionResult RegistroEmpresaPrincipal()
        //{

        //    int IdEmpresa = Convert.ToInt32(Request.Form["IdEmpresa"]);
        //    try
        //    {
        //        General.Empresa empresa = new General.Empresa();
        //        if (IdEmpresa > 0)
        //        {
        //            empresa =  iempresa.ObtenerEmpresa(IdEmpresa);
        //        }
        //        return View(empresa);
        //    }
        //    catch (Exception ex)
        //    {
        //        return RedirectToAction("PaginaError", new { DescripcionError = "Hubo un error [" + ex.Message + "]" });
        //    }
        //}
        [HttpPost]
        public async Task<IActionResult> RegistroEmpresa()
        {

            int IdEmpresa = Convert.ToInt32(Request.Form["IdEmpresa"]);
            try
            {
                General.Empresa empresa = new General.Empresa();

                if (IdEmpresa > 0)
                {
                   
                        empresa =await iempresa.ObtenerEmpresaAsync( IdEmpresa);
                   
                }
                return View(empresa);
            }
            catch (Exception ex)
            {
                return RedirectToAction("PaginaError", new { DescripcionError = "Hubo un error [" + ex.Message + "]" });
            }
        }
        [HttpPost]
        public async Task<IActionResult> GuardarRegistroEmpresaPrincipal()
        {
            int IdEmpresa = Convert.ToInt32(Request.Form["txtRegIdEmpresa"]);
            int IdEmpresaPadre = 0;
            int IdPersona = Convert.ToInt32(Request.Form["txtIdPersona"]);
            string NombreEmpresa = Request.Form["txtNombreEmpresa"];
            bool Activo = Convert.ToBoolean(Convert.ToInt32(Request.Form["optActivo"]));
            General.Empresa empresa = new General.Empresa();
            try
            {
                empresa = await iempresa.GuardaEmpresaPrincipalAsync(
                    IdEmpresa,
                    IdEmpresaPadre,
                    IdPersona,
                    NombreEmpresa,
                    Activo,
                    Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario"))
            );
                return Json(empresa);
            }
            catch (Exception ex)
            {
                empresa.mensaje.CodigoMensaje = 1;
                empresa.mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA AL GUARDAR, COMUNIQUESE CON EL ADMINISTRADOR DEL SISTEMA";
                empresa.mensaje.DescripcionMensajeSistema = ex.Message;
                return Json(empresa);
            }
        }
        [HttpPost]
        public async Task<IActionResult> GuardarRegistroEmpresa()
        {
            int IdEmpresa = Convert.ToInt32(Request.Form["txtRegIdEmpresa"]);
            int IdEmpresaPadre = Convert.ToInt32(HttpContext.Session.GetString("IdEmpresaPadre").ToString());
            int IdPersona = Convert.ToInt32(Request.Form["txtIdPersona"]);
            string NombreEmpresa = Request.Form["txtNombreEmpresa"];
            bool Activo = Convert.ToBoolean(Convert.ToInt32(Request.Form["optActivo"]));


            General.Empresa empresa = new General.Empresa();
            try
            {
               
                    empresa =await iempresa.GuardaEmpresaAsync(
                
                                        IdEmpresa,
                                        IdEmpresaPadre,
                                                IdPersona,
                                                NombreEmpresa,
                                                Activo,
                                        Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario"))
                );
               
                return Json(empresa);
            }
            catch (Exception ex)
            {
                empresa.mensaje.CodigoMensaje = 1;
                empresa.mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA AL GUARDAR, COMUNIQUESE CON EL ADMINISTRADOR DEL SISTEMA";
                empresa.mensaje.DescripcionMensajeSistema = ex.Message;
                return Json(empresa);
            }
        }
        [HttpPost]
        public async Task<IActionResult> EliminarEmpresa()
        {
            int IdEmpresa = Convert.ToInt32(Request.Form["IdEmpresa"]);


            Utilitarios.Mensaje mensaje = new Utilitarios.Mensaje();
            try
            {
               
                    mensaje =await iempresa.EliminarEmpresaAsync( IdEmpresa, Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));
                
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
        public async Task<IActionResult> ListarComboEmpresa()
        {
            General.Empresa empresa = new General.Empresa();
            General.ListaEmpresa listaempresa = new General.ListaEmpresa();
            try
            {
               
                    listaempresa =await iempresa.ListarComboEmpresaAsync();
                
                var data = listaempresa.lista.Select(lq => new
                {
                    IdEmpresa = lq.IdEmpresa,
                    NombreEmpresa = lq.NombreEmpresa
                }).ToList();

                return Json(data);
            }
            catch (Exception ex)
            {
                listaempresa.mensaje.CodigoMensaje = 1;
                listaempresa.mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA EN LA ACCION [ListarComboEmpresa] DEL CONTROLADOR Empresa , COMUNIQUESE CON EL ADMINISTRADOR DEL SISTEMA";
                listaempresa.mensaje.DescripcionMensajeSistema = ex.Message;
                return Json(listaempresa.lista);
            }
        }
        [HttpPost]
        public async Task<IActionResult>ListarComboEmpresaPadre()
        {
            General.Empresa empresa = new General.Empresa();
            General.ListaEmpresa listaempresa = new General.ListaEmpresa();
            try
            {
               
                    listaempresa =await iempresa.ListarComboEmpresaPadreAsync();
               
                var data = listaempresa.lista.Select(lq => new
                {
                    IdEmpresa = lq.IdEmpresa,                  
                    NombreEmpresa = lq.NombreEmpresa                 
                }).ToList();

                return Json( data);
            }
            catch (Exception ex)
            {
                listaempresa.mensaje.CodigoMensaje = 1;
                listaempresa.mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA EN LA ACCION [ListarComboEmpresa] DEL CONTROLADOR Empresa , COMUNIQUESE CON EL ADMINISTRADOR DEL SISTEMA";
                listaempresa.mensaje.DescripcionMensajeSistema = ex.Message;
                return Json(listaempresa.lista);
            }
        }
    }
}









