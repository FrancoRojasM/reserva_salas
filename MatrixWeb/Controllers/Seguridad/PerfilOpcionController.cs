using DataTables.AspNet.AspNetCore;
using DataTables.AspNet.Core; using Microsoft.AspNetCore.Http;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using MatrixService;

namespace MatrixWeb.Controllers
{
    //[ValidarUsuario]
    public class PerfilOpcionController : Controller
    {
        private ISvcPerfil iperfil;
        private readonly ISvcPerfilOpcion iperfilopcion;
        public PerfilOpcionController(ISvcPerfilOpcion _iperfilopcion, ISvcPerfil _iperfil)
        {
            iperfilopcion = _iperfilopcion;
            iperfil = _iperfil;
        }
        [HttpPost]
        public IActionResult IndexPerfilOpcion()
        {
            int IdPerfil = Convert.ToInt32(Request.Form["IdPerfil"]);
            ViewBag.IdPerfil = IdPerfil;
            return View();
        }
        [HttpPost]
        public IActionResult ListarPerfilOpcion(IDataTablesRequest requestModel)
        {
            int IdPerfil = Convert.ToInt32(Request.Form["IdPerfil"]);

            Seguridad.ListaPerfilOpcion listaperfilopcion = new Seguridad.ListaPerfilOpcion();
            Seguridad.PerfilOpcion perfilopcion = new Seguridad.PerfilOpcion();
            int numeroPagina = (requestModel.Start / requestModel.Length) + 1;
            try
            {
              
                    listaperfilopcion = iperfilopcion.ListarPerfilOpcion( IdPerfil, Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")), null, null, numeroPagina, requestModel.Length, requestModel.Search.Value);
            

                if (listaperfilopcion.mensaje.CodigoMensaje > 0)
                {
                    return RedirectToAction("PaginaError", new { DescripcionError = listaperfilopcion.mensaje.DescripcionMensaje });
                }
                var totalCount = listaperfilopcion.paginacion.TotalRegistros;
                var data = listaperfilopcion.lista.Select(lq => new
                {
                    CatalogoTipoOpcion=lq.opcion.catalogotipoopcion.Descripcion,
                    NombreModulo=lq.opcion.modulo.NombreModulo,
                    NombreOpcionPadre=lq.opcion.NombreOpcionPadre,
                    NombreOpcion=lq.opcion.NombreOpcion,
                    IdCatalogoTipoOpcion = lq.opcion.catalogotipoopcion.IdCatalogo,
                    IdPerfilOpcion = lq.IdPerfilOpcion,
                    Perfil = lq.perfil.IdPerfil,
                    OpcionPadre = lq.IdOpcionPadre,
                    Opcion = lq.opcion.IdOpcion,
                    Ver = (lq.Ver == true ? "Si" : "No"),
                    Nuevo = (lq.Nuevo == true ? "Si" : "No"),
                    Editar = (lq.Editar == true ? "Si" : "No"),
                    Guardar = (lq.Guardar == true ? "Si" : "No"),
                    Eliminar = (lq.Eliminar == true ? "Si" : "No"),
                    Imprimir = (lq.Imprimir == true ? "Si" : "No"),
                    Exportar = (lq.Exportar == true ? "Si" : "No"),
                    VistaPrevia = (lq.VistaPrevia == true ? "Si" : "No"),
                    Consultar = (lq.Consultar == true ? "Si" : "No"),

                    Ver0 = (lq.Ver0 == true ? "Si" : "No"),
                    Nuevo0 = (lq.Nuevo0 == true ? "Si" : "No"),
                    Editar0 = (lq.Editar0 == true ? "Si" : "No"),
                    Guardar0 = (lq.Guardar0 == true ? "Si" : "No"),
                    Eliminar0 = (lq.Eliminar0 == true ? "Si" : "No"),
                    Imprimir0 = (lq.Imprimir0 == true ? "Si" : "No"),
                    Exportar0 = (lq.Exportar0 == true ? "Si" : "No"),
                    VistaPrevia0 = (lq.VistaPrevia0 == true ? "Si" : "No"),
                    Consultar0 = (lq.Consultar0== true ? "Si" : "No")
                }).ToList();

                var jsonData = new { draw = requestModel.Draw, recordsFiltered = totalCount, recordsTotal = totalCount, data = data }; return Ok(jsonData);
            }
            catch (Exception ex)
            {
                return RedirectToAction("PaginaError", new { DescripcionError = "Hubo un error [" + ex.Message + "]" });
            }
        }
        [HttpPost]
        public IActionResult RegistroPerfilOpcion()
        {
            int IdPerfil = Convert.ToInt32(Request.Form["IdPerfil"]);

            int IdPerfilOpcion = Convert.ToInt32(Request.Form["IdPerfilOpcion"]);
            try
            {
                Seguridad.PerfilOpcion perfilopcion = new Seguridad.PerfilOpcion();
                Seguridad.Perfil perfil = new Seguridad.Perfil();

                if (IdPerfilOpcion > 0)
                {
                        perfilopcion = iperfilopcion.ObtenerPerfilOpcion( IdPerfilOpcion);
                    
                }
               
                    perfil = iperfil.ObtenerPerfil( IdPerfil);
                
                perfilopcion.perfil = perfil;
                return View(perfilopcion);
            }
            catch (Exception ex)
            {
                return RedirectToAction("PaginaError", new { DescripcionError = "Hubo un error [" + ex.Message + "]" });
            }
        }
        [HttpPost]
        public IActionResult GuardarRegistroPerfilOpcion()
        {
            int IdPerfilOpcion = Convert.ToInt32(Request.Form["txtRegIdPerfilOpcion"]);
            int IdPerfil = Convert.ToInt32(Request.Form["IdPerfil"]);
            int IdOpcionPadre = Convert.ToInt32(Request.Form["cmbOpcionPadre"]);

            int IdOpcion = Convert.ToInt32(Request.Form["cmbOpcion"]);
            bool Ver = Convert.ToBoolean(Convert.ToInt32(Request.Form["optVer"]));
            bool Nuevo = Convert.ToBoolean(Convert.ToInt32(Request.Form["optNuevo"]));
            bool Editar = Convert.ToBoolean(Convert.ToInt32(Request.Form["optEditar"]));
            bool Guardar = Convert.ToBoolean(Convert.ToInt32(Request.Form["optGuardar"]));
            bool Eliminar = Convert.ToBoolean(Convert.ToInt32(Request.Form["optEliminar"]));
            bool Imprimir = Convert.ToBoolean(Convert.ToInt32(Request.Form["optImprimir"]));
            bool Exportar = Convert.ToBoolean(Convert.ToInt32(Request.Form["optExportar"]));
            bool VistaPrevia = Convert.ToBoolean(Convert.ToInt32(Request.Form["optVistaPrevia"]));
            bool Consultar = Convert.ToBoolean(Convert.ToInt32(Request.Form["optConsultar"]));

            Seguridad.PerfilOpcion perfilopcion = new Seguridad.PerfilOpcion();
            try
            {
                perfilopcion = iperfilopcion.GuardaPerfilOpcion(
                
                IdPerfilOpcion,
                IdPerfil,
                IdOpcionPadre,
                IdOpcion,
                Ver,
                Nuevo,
                Editar,
                Guardar,
                Eliminar,
                Imprimir,
                Exportar,
                VistaPrevia,
                Consultar,
                Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario"))
            );

                return Json(perfilopcion);
            }
            catch (Exception ex)
            {
                return RedirectToAction("PaginaError", new { DescripcionError = "Hubo un error [" + ex.Message + "]" });
            }
        }
        [HttpPost]
        public IActionResult EliminarPerfilOpcion()
        {
            int IdPerfilOpcion = Convert.ToInt32(Request.Form["IdPerfilOpcion"]);
            Seguridad.PerfilOpcion perfilopcion = new Seguridad.PerfilOpcion();
            Utilitarios.Mensaje mensaje = new Utilitarios.Mensaje();
            try
            {               
                mensaje = iperfilopcion.EliminarPerfilOpcion( IdPerfilOpcion, Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));
                return Json(mensaje);
            }
            catch (Exception ex)
            {
                return RedirectToAction("PaginaError", new { DescripcionError = "Hubo un error [" + ex.Message + "]" });
            }
        }
    }
}









		