
using DataTables.AspNet.Core; using Microsoft.AspNetCore.Http;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using MatrixService;
using DataTables.AspNet.AspNetCore;
namespace MatrixWeb.Controllers
{
    //[ValidarUsuario]
    public class OpcionFormularioController : Controller
    {
        private ISvcOpcion iopcion;
        private ISvcOpcionFormulario iopcionformulario;
        public OpcionFormularioController(ISvcOpcion _iopcion, ISvcOpcionFormulario _iopcionformulario)
        {
            iopcion = _iopcion;
            iopcionformulario = _iopcionformulario;
        }

       
        [HttpGet]
        public IActionResult IndexOpcionFormulario()
        {
            int IdOpcion = Convert.ToInt32(Request.Form["IdOpcion"]);
            ViewBag.IdOpcion = IdOpcion;
            return View();
        }
        [HttpPost]
        public IActionResult ListarOpcionFormulario(IDataTablesRequest requestModel)
        {
            int IdOpcion = Convert.ToInt32(Request.Form["IdOpcion"]);

            Sistema.ListaOpcionFormulario listaopcionformulario = new Sistema.ListaOpcionFormulario();
            Sistema.OpcionFormulario opcionformulario = new Sistema.OpcionFormulario();
            int numeroPagina = (requestModel.Start / requestModel.Length) + 1;
            try
            {
               
                    listaopcionformulario = iopcionformulario.ListarOpcionFormulario( IdOpcion, Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")), null, null, numeroPagina, requestModel.Length, requestModel.Search.Value);
                

                if (listaopcionformulario.mensaje.CodigoMensaje > 0)
                {
                    return RedirectToAction("PaginaError", new { DescripcionError = listaopcionformulario.mensaje.DescripcionMensaje });
                }
                var totalCount = listaopcionformulario.paginacion.TotalRegistros;
                var data = listaopcionformulario.lista.Select(lq => new
                {

                    IdOpcionFormulario = lq.IdOpcionFormulario,

                    Opcion = lq.opcion.IdOpcion,

                    NombreFormulario = lq.NombreFormulario,

                    RutaFormulario = lq.RutaFormulario,

                    Controlador = lq.Controlador,

                    Accion = lq.Accion,

                    Parametros = lq.Parametros,

                    Ver = (lq.Ver == true ? "Si" : "No"),

                    Nuevo = (lq.Nuevo == true ? "Si" : "No"),

                    Editar = (lq.Editar == true ? "Si" : "No"),

                    Guardar = (lq.Guardar == true ? "Si" : "No"),

                    Eliminar = (lq.Eliminar == true ? "Si" : "No"),

                    Imprimir = (lq.Imprimir == true ? "Si" : "No"),

                    Exportar = (lq.Exportar == true ? "Si" : "No"),

                    VistaPrevia = (lq.VistaPrevia == true ? "Si" : "No"),

                    Consultar = (lq.Consultar == true ? "Si" : "No"),

                    Activo = (lq.Activo == true ? "Si" : "No"),





                }).ToList();

                var jsonData = new { draw = requestModel.Draw, recordsFiltered = totalCount, recordsTotal = totalCount, data = data }; return Ok(jsonData);
            }
            catch (Exception ex)
            {
                return RedirectToAction("PaginaError", new { DescripcionError = "Hubo un error [" + ex.Message + "]" });
            }
        }
        [HttpPost]
        public IActionResult RegistroOpcionFormulario()
        {
            int IdOpcion = Convert.ToInt32(Request.Form["IdOpcion"]);

          //  int IdOpcionFormulario = Convert.ToInt32(Request.Form["IdOpcionFormulario"]);
            try
            {
                Sistema.OpcionFormulario opcionformulario = new Sistema.OpcionFormulario();
                Sistema.Opcion opcion = new Sistema.Opcion();

                if (IdOpcion > 0)
                {
                  
                        opcionformulario = iopcionformulario.ObtenerOpcionFormulario( IdOpcion);
                   
                }
               
                    opcion = iopcion.ObtenerOpcion( IdOpcion);
                
                opcionformulario.opcion = opcion;
                return View(opcionformulario);
            }
            catch (Exception ex)
            {
                return RedirectToAction("PaginaError", new { DescripcionError = "Hubo un error [" + ex.Message + "]" });
            }
        }
        [HttpPost]
        public IActionResult GuardarRegistroOpcionFormulario()
        {
            int IdOpcionFormulario = Convert.ToInt32(Request.Form["txtRegIdOpcionFormulario"]);
            int IdOpcion = Convert.ToInt32(Request.Form["IdOpcion"]);
            string NombreFormulario = Request.Form["txtNombreFormulario"];
            string RutaFormulario = Request.Form["txtRutaFormulario"];
            string Controlador = Request.Form["txtControlador"];
            string Accion = Request.Form["txtAccion"];
            string Parametros = Request.Form["txtParametros"];
            bool Ver = Convert.ToBoolean(Convert.ToInt32(Request.Form["optVer"]));
            bool Nuevo = Convert.ToBoolean(Convert.ToInt32(Request.Form["optNuevo"]));
            bool Editar = Convert.ToBoolean(Convert.ToInt32(Request.Form["optEditar"]));
            bool Guardar = Convert.ToBoolean(Convert.ToInt32(Request.Form["optGuardar"]));
            bool Eliminar = Convert.ToBoolean(Convert.ToInt32(Request.Form["optEliminar"]));
            bool Imprimir = Convert.ToBoolean(Convert.ToInt32(Request.Form["optImprimir"]));
            bool Exportar = Convert.ToBoolean(Convert.ToInt32(Request.Form["optExportar"]));
            bool VistaPrevia = Convert.ToBoolean(Convert.ToInt32(Request.Form["optVistaPrevia"]));
            bool Consultar = Convert.ToBoolean(Convert.ToInt32(Request.Form["optConsultar"]));
            bool Activo = Convert.ToBoolean(Convert.ToInt32(Request.Form["optActivoFromulario"]));


            Sistema.OpcionFormulario opcionformulario = new Sistema.OpcionFormulario();
            try
            {
               
                    opcionformulario = iopcionformulario.GuardaOpcionFormulario(
                
                                        IdOpcionFormulario,
                                                IdOpcion,
                                                NombreFormulario,
                                                RutaFormulario,
                                                Controlador,
                                                Accion,
                                                Parametros,
                                                Ver,
                                                Nuevo,
                                                Editar,
                                                Guardar,
                                                Eliminar,
                                                Imprimir,
                                                Exportar,
                                                VistaPrevia,
                                                Consultar,
                                                Activo,
                                        Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario"))
                );
               
                return Json(opcionformulario);
            }
            catch (Exception ex)
            {
                opcionformulario.mensaje.CodigoMensaje = 1;
                opcionformulario.mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA AL GUARDAR, COMUNIQUESE CON EL ADMINISTRADOR DEL SISTEMA";
                opcionformulario.mensaje.DescripcionMensajeSistema = ex.Message;
                return Json(opcionformulario);
            }
        }
        [HttpPost]
        public IActionResult EliminarOpcionFormulario()
        {
            int IdOpcionFormulario = Convert.ToInt32(Request.Form["IdOpcionFormulario"]);

            Sistema.OpcionFormulario opcionformulario = new Sistema.OpcionFormulario();
            Utilitarios.Mensaje mensaje = new Utilitarios.Mensaje();
            try
            {
              
                    mensaje = iopcionformulario.EliminarOpcionFormulario( IdOpcionFormulario, Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));
               
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
    }
}









