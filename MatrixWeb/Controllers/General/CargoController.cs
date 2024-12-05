
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
    public class CargoController : Controller
    {
        private ISvcCargo icargo;
        public CargoController(ISvcCargo _icargo)
        {
            icargo = _icargo;
        }
        [HttpGet]
        public IActionResult IndexCargo()
        {
            var conpermiso = MatrixUtilitarios.Utilitario.TienePermisoControladorAccion(HttpContext.Request.RouteValues["controller"].ToString(), HttpContext.Request.RouteValues["action"].ToString()); if (conpermiso == -1) { return RedirectToAction("Login", "Usuario"); }
            return View();
        }
        [HttpPost]
        public IActionResult ListarCargo(IDataTablesRequest requestModel)
        {

            General.ListaCargo listacargo = new General.ListaCargo();
            General.Cargo cargo = new General.Cargo();
            int numeroPagina = (requestModel.Start / requestModel.Length) + 1;
            try
            {
               
                    listacargo = icargo.ListarCargo( Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")), null, null, numeroPagina, requestModel.Length, requestModel.Search.Value);
                if (listacargo.mensaje.CodigoMensaje > 0)
                {
                    return RedirectToAction("PaginaError", new { DescripcionError = listacargo.mensaje.DescripcionMensaje });
                }
                var totalCount = listacargo.paginacion.TotalRegistros;
                var data = listacargo.lista.Select(lq => new
                {

                    IdCargo = lq.IdCargo,
                    CatalogoTipoCargo=lq.catalogotipocargo.Descripcion ,
                    NombreCargo = lq.NombreCargo,

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
        public IActionResult RegistroCargo()
        {

            int IdCargo = Convert.ToInt32(Request.Form["IdCargo"]);
            try
            {
                General.Cargo cargo = new General.Cargo();

                if (IdCargo > 0)
                {
                   
                        cargo = icargo.ObtenerCargo( IdCargo);
                    
                }
                return View(cargo);
            }
            catch (Exception ex)
            {
                return RedirectToAction("PaginaError", new { DescripcionError = "Hubo un error [" + ex.Message + "]" });
            }
        }
        [HttpPost]
        public IActionResult GuardarRegistroCargo()
        {
            int IdCargo = Convert.ToInt32(Request.Form["txtRegIdCargo"]);
            int IdCatalogoTipoCargo = Convert.ToInt32(Request.Form["cmbCatalogoTipoCargo"]);            
            string NombreCargo = Request.Form["txtNombreCargo"];
            bool Activo = Convert.ToBoolean(Convert.ToInt32(Request.Form["optActivo"]));
            General.Cargo cargo = new General.Cargo();
            try
            {
               
                    cargo = icargo.GuardaCargo(
                
                                        IdCargo,
                                                NombreCargo,
                                                IdCatalogoTipoCargo,
                                                Activo,
                                        Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario"))
                );
                
                return Json(cargo);
            }
            catch (Exception ex)
            {
                cargo.mensaje.CodigoMensaje = 1;
                cargo.mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA AL GUARDAR, COMUNIQUESE CON EL ADMINISTRADOR DEL SISTEMA";
                cargo.mensaje.DescripcionMensajeSistema = ex.Message;
                return Json(cargo);
            }
        }
        [HttpPost]
        public IActionResult EliminarCargo()
        {
            int IdCargo = Convert.ToInt32(Request.Form["IdCargo"]);

            General.Cargo cargo = new General.Cargo();
            Utilitarios.Mensaje mensaje = new Utilitarios.Mensaje();
            try
            {
               
                    mensaje = icargo.EliminarCargo( IdCargo, Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));
                
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
        public async Task<IActionResult> ListarComboCargo()
        {
            General.Cargo cargo = new General.Cargo();
            General.ListaCargo listacargo = new General.ListaCargo();
            try
            {
                listacargo =await icargo.ListarComboCargoAsync();
                var data = listacargo.lista.Select(lq => new
                {
                    lq.IdCargo,
                    lq.NombreCargo
                }).ToList();
                return Json(data);
            }
            catch (Exception ex)
            {
                listacargo.mensaje.CodigoMensaje = 1;
                listacargo.mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA EN LA ACCION [ListarComboCargo] DEL CONTROLADOR Cargo , COMUNIQUESE CON EL ADMINISTRADOR DEL SISTEMA";
                listacargo.mensaje.DescripcionMensajeSistema = ex.Message;
                return Json(listacargo.lista);
            }
        }
    }
}









