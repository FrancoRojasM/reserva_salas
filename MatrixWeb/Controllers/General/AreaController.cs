
using DataTables.AspNet.Core; using Microsoft.AspNetCore.Http;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using MatrixService;
using System.Threading.Tasks;
using General;
using DataTables.AspNet.AspNetCore;

namespace MatrixWeb.Controllers
{
    //[ValidarUsuario]
    public class AreaController : Controller
    {
        private ISvcArea iarea;
        private ISvcEmpresa iempresa;
        public AreaController(ISvcArea _iarea, ISvcEmpresa _iempresa)
        {
            iarea = _iarea;
            iempresa = _iempresa;            
        }
        [HttpGet]
        public IActionResult IndexArea()
        {
            ViewBag.IdEmpresa =Convert.ToInt32(Request.Query["IdEmpresa"].ToString()); 
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ListarArea(IDataTablesRequest requestModel)
        {
            int IdEmpresa = Convert.ToInt32(Request.Form["IdEmpresa"]);
            var listaarea = new ListaArea();
            var area = new Area();
            int numeroPagina = (requestModel.Start / requestModel.Length) + 1;
            try
            {
                listaarea = await iarea.ListarAreaAsync(IdEmpresa, Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")), null, null, numeroPagina, requestModel.Length, requestModel.Search.Value);
                if (listaarea.mensaje.CodigoMensaje > 0)
                {
                    return RedirectToAction("PaginaError","Errores", new { listaarea.mensaje });
                }
                var totalCount = listaarea.paginacion.TotalRegistros;
                var data = listaarea.lista.Select(lq => new
                {
                    IdArea = lq.IdArea,
                    NombreArea = lq.NombreArea,
                    Abreviatura = lq.Abreviatura,
                    Sigla = lq.Sigla,
                    VerRecepcion = (lq.VerRecepcion == true ? "Si" : "No"),
                    Activo = (lq.Activo == true ? "Si" : "No"),
                    CatalogoTipoArea = lq.catalogotipoarea.Descripcion,
                    NombreEmpresa = lq.empresa.NombreEmpresa,
                    NombreAreaPadre = lq.NombreAreaPadre
                }).ToList();
                var jsonData = new { draw = requestModel.Draw, recordsFiltered = totalCount, recordsTotal = totalCount, data = data }; return Ok(jsonData);
            }
            catch (Exception ex)
            {
                listaarea.mensaje.CodigoMensaje = 1;
                listaarea.mensaje.DescripcionMensaje = ex.Message;
                return RedirectToAction("PaginaError", "Errores", new { listaarea.mensaje });
            }
        }
        [HttpPost]
        public async Task<IActionResult> RegistroArea()
        {
            int IdEmpresa = Convert.ToInt32(Request.Form["IdEmpresa"]);
            int IdArea = Convert.ToInt32(Request.Form["IdArea"]);
            try
            {
                Area area = new Area();
                Empresa empresa = new Empresa();
                empresa =await iempresa.ObtenerEmpresaAsync( IdEmpresa);
                if (IdArea > 0)
                {
                    area =await  iarea.ObtenerAreaAsync(IdArea);
                }
                area.empresa = empresa;
                return View(area);
            }
            catch (Exception ex)
            {
                return RedirectToAction("PaginaError","Errores", new { DescripcionError = "Hubo un error [" + ex.Message + "]" });
            }
        }
        [HttpPost]
        public async Task<IActionResult> GuardarRegistroArea()
        {
            int IdArea = Convert.ToInt32(Request.Form["txtRegIdArea"]);
            int IdAreaPadre = Convert.ToInt32(Request.Form["cmbAreaPadre"]==""?"0": Request.Form["cmbAreaPadre"]);
            string NombreArea = Request.Form["txtNombreArea"];
            string Abreviatura = Request.Form["txtAbreviatura"];
            string Sigla = Request.Form["txtSigla"];
            bool Activo = Convert.ToBoolean(Convert.ToInt32(Request.Form["optActivoArea"]));
            bool VerRecepcion = Convert.ToBoolean(Convert.ToInt32(Request.Form["optVerRecepcionArea"]));
            int IdCatalogoTipoArea = Convert.ToInt32(Request.Form["cmbCatalogoTipoArea"]);
            int IdEmpresa = Convert.ToInt32(Request.Form["txtIdEmpresa"]);
            
            var area = new Area();
            try
            {
                area =await iarea.GuardaAreaAsync(
            
                                    IdArea,
                                    IdEmpresa,
                                            IdAreaPadre,
                                            NombreArea,
                                            Abreviatura,
                                            Sigla,
                                            Activo,
                                            IdCatalogoTipoArea,
                                            VerRecepcion,
                                    Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario"))
            );

                return Json(area);
            }
            catch (Exception ex)
            {
                area.mensaje.CodigoMensaje = 1;
                area.mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA AL GUARDAR, COMUNIQUESE CON EL ADMINISTRADOR DEL SISTEMA";
                area.mensaje.DescripcionMensajeSistema = ex.Message;
                return Json(area);
            }
        }
        [HttpPost]
        public async Task<IActionResult> EliminarArea()
        {
            int IdArea = Convert.ToInt32(Request.Form["IdArea"]);

            Area area = new Area();
            Utilitarios.Mensaje mensaje = new Utilitarios.Mensaje();
            try
            {

                mensaje =await iarea.EliminarAreaAsync( IdArea, Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));

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
        public async Task<IActionResult> ListarComboArea()
        {
           int IdEmpresaSede = Convert.ToInt32(Request.Form["IdEmpresaSede"]);
            var listaarea =await iarea.ListarComboAreaAsync(IdEmpresaSede);

                var data = listaarea.lista.Select(lq => new
                {
                    IdArea = lq.IdArea,
                    NombreArea = lq.NombreArea
                }).ToList();

                return Json(listaarea);
           
        }
        [HttpPost]
        public async Task<IActionResult> ListarComboArea2()
        {
            int IdEmpresaSede = 1;
            var listaarea = await iarea.ListarComboAreaAsync(IdEmpresaSede);
            var data = listaarea.lista.Select(lq => new
            {
                IdArea = lq.IdArea,
                NombreArea = lq.NombreArea
            }).ToList();
            return Json(data);
        }
        [HttpPost]
        public async Task<IActionResult> ListarComboArea3()
        {
            var listaarea = await iarea.ListarComboArea3Async(Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));
            var data = listaarea.lista.Select(lq => new
            {
                IdArea = lq.IdArea,
                NombreArea = lq.NombreArea
            }).ToList();
            return Json(data);
        }

        [HttpPost]
        public async Task<IActionResult> ListarComboAreaPadre()
        {
            int IdEmpresa = Convert.ToInt32(Request.Form["IdEmpresa"]);
            Area area = new Area();
            ListaArea listaarea = new ListaArea();
            try
            {

                listaarea =await iarea.ListarComboAreaPadreAsync( IdEmpresa);

                var data = listaarea.lista.Select(lq => new
                {
                    IdArea = lq.IdArea,
                    NombreArea = lq.NombreArea
                }).ToList();

                return Json(data);
            }
            catch (Exception ex)
            {
                listaarea.mensaje.CodigoMensaje = 1;
                listaarea.mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA EN LA ACCION [ListarComboArea] DEL CONTROLADOR Area , COMUNIQUESE CON EL ADMINISTRADOR DEL SISTEMA";
                listaarea.mensaje.DescripcionMensajeSistema = ex.Message;
                return Json(listaarea.lista);
            }
        }
    }
}








