using DataTables.AspNet.AspNetCore;
using DataTables.AspNet.Core; using Microsoft.AspNetCore.Http;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using MatrixService;
using System.Threading.Tasks;

namespace MatrixWeb.Controllers
{
    //[ValidarUsuario]
    public class EmpresaSedeController : Controller
    {       
        private ISvcEmpresa iempresa;
        private ISvcEmpresaSede iempresasede;
        public EmpresaSedeController(ISvcEmpresaSede _iempresasede, ISvcEmpresa _iempresa)
        {
            iempresasede = _iempresasede;
            iempresa = _iempresa;
        }
        [HttpGet]
        public IActionResult  IndexEmpresaSedeProducto() 
        {
            if (HttpContext.Session.GetString("NombreEmpleadoPerfil").ToString() == "Administrador")
            {
                return View();
            }
            else
            {
                string Id = HttpContext.Session.GetString("IdSede").ToString();
                return RedirectToAction("IndexProductoAdmin", "Producto", new{ IdEmpresaSede= Id} );
            }
        }

        [HttpPost]
        public IActionResult  IndexEmpresaSede()
        {
            int IdEmpresa = Convert.ToInt32(Request.Form["IdEmpresa"]);
            ViewBag.IdEmpresa = IdEmpresa;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult>  ListarEmpresaSede(IDataTablesRequest requestModel)
        {
            int IdEmpresa = Convert.ToInt32(Request.Form["IdEmpresa"]);

            General.ListaEmpresaSede listaempresasede = new General.ListaEmpresaSede();
            General.EmpresaSede empresasede = new General.EmpresaSede();
            int numeroPagina = (requestModel.Start / requestModel.Length) + 1;
            try
            {
               
                    listaempresasede = await iempresasede.ListarEmpresaSedeAsync(IdEmpresa, Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")), null, null, numeroPagina, requestModel.Length, requestModel.Search.Value);
                

                if (listaempresasede.mensaje.CodigoMensaje > 0)
                {
                    return RedirectToAction("PaginaError", new { DescripcionError = listaempresasede.mensaje.DescripcionMensaje });
                }
                var totalCount = listaempresasede.paginacion.TotalRegistros;
                var data = listaempresasede.lista.Select(lq => new
                {

                    IdEmpresaSede = lq.IdEmpresaSede,
                    Empresa = lq.empresa.IdEmpresa,
                    DireccionSede = lq.DireccionSede,
                    NombreSede = lq.NombreSede,
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
        public async Task<IActionResult>  RegistroEmpresaSede()
        {
            int IdEmpresa = Convert.ToInt32(Request.Form["IdEmpresa"]);

            int IdEmpresaSede = Convert.ToInt32(Request.Form["IdEmpresaSede"]);
            try
            {
                General.EmpresaSede empresasede = new General.EmpresaSede();
                General.Empresa empresa = new General.Empresa();

                if (IdEmpresaSede > 0)
                {

                    empresasede = await iempresasede.ObtenerEmpresaSedeAsync( IdEmpresaSede, Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));

                }
              
                    empresa = iempresa.ObtenerEmpresa( IdEmpresa);
                
                empresasede.empresa = empresa;
                return View(empresasede);
            }
            catch (Exception ex)
            {
                return RedirectToAction("PaginaError", new { DescripcionError = "Hubo un error [" + ex.Message + "]" });
            }
        }
        [HttpPost]
        public async Task<IActionResult> GuardarRegistroEmpresaSede()
        {
            int IdEmpresaSede = Convert.ToInt32(Request.Form["txtRegIdEmpresaSede"]);
            int IdEmpresa = Convert.ToInt32(Request.Form["IdEmpresa"]);
            string DireccionSede = Request.Form["txtDireccionSede"];

            string NombreSede = Request.Form["txtNombreSede"];
            bool Activo = Convert.ToBoolean(Convert.ToInt32(Request.Form["optActivo"]));


            General.EmpresaSede empresasede = new General.EmpresaSede();
            try
            {
                empresasede = await iempresasede.GuardaEmpresaSedeAsync(
                                    IdEmpresaSede,
                                            IdEmpresa,
                                            DireccionSede,
                                            NombreSede,
                                            Activo,
                                    Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario"))
            );

                return Json(empresasede);
            }
            catch (Exception ex)
            {
                empresasede.mensaje.CodigoMensaje = 1;
                empresasede.mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA AL GUARDAR, COMUNIQUESE CON EL ADMINISTRADOR DEL SISTEMA";
                empresasede.mensaje.DescripcionMensajeSistema = ex.Message;
                return Json(empresasede);
            }
        }
        [HttpPost]
        public async Task<IActionResult>  EliminarEmpresaSede()
        {
            int IdEmpresaSede = Convert.ToInt32(Request.Form["IdEmpresaSede"]);

            General.EmpresaSede empresasede = new General.EmpresaSede();
            Utilitarios.Mensaje mensaje = new Utilitarios.Mensaje();
            try
            {
               
                    mensaje = await iempresasede.EliminarEmpresaSedeAsync( IdEmpresaSede, Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));
                
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
        public async Task<IActionResult>  ListarComboEmpresaSede()
        {
            General.EmpresaSede empresasede = new General.EmpresaSede();
            General.ListaEmpresaSede listaempresasede = new General.ListaEmpresaSede();
            int IdEmpresaPadre = Convert.ToInt32(HttpContext.Session.GetString("IdEmpresaPadre").ToString());
            try
            {
                listaempresasede = await iempresasede.ListarComboEmpresaSedeAsync( Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")), IdEmpresaPadre);
                var data = listaempresasede.lista.Select(lq => new
                {
                    IdEmpresaSede = lq.IdEmpresaSede,
                    NombreEmpresaSede = lq.NombreSede

                }).ToList();

                return Json(data);
            }
            catch (Exception ex)
            {
                listaempresasede.mensaje.CodigoMensaje = 1;
                listaempresasede.mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA EN LA ACCION [ListarComboEmpresaSede] DEL CONTROLADOR EmpresaSede , COMUNIQUESE CON EL ADMINISTRADOR DEL SISTEMA";
                listaempresasede.mensaje.DescripcionMensajeSistema = ex.Message;
                return Json(listaempresasede.lista);
            }
        }
    }
}









