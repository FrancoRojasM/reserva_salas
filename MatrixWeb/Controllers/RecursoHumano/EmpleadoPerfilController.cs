using DataTables.AspNet.Core; using Microsoft.AspNetCore.Http;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using MatrixService;
using System.Threading.Tasks;

namespace MatrixWeb.Controllers
{
    //[ValidarUsuario]
    public class EmpleadoPerfilController : Controller
    {
        private ISvcEmpleadoPerfil iempleadoperfil;
        private ISvcEmpleado iempleado;
        public EmpleadoPerfilController(ISvcEmpleadoPerfil _iempleadoperfil, ISvcEmpleado _iempleado)
        {
            iempleado = _iempleado;
            iempleadoperfil = _iempleadoperfil;
        }
        [HttpPost] 
        public IActionResult IndexEmpleadoPerfil()
        {
            int IdEmpleado = Convert.ToInt32(Request.Form["IdEmpleado"]);
            ViewBag.IdEmpleado = IdEmpleado;
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Roles()
        {
            int IdEmpleado = Convert.ToInt32(HttpContext.Session.GetString("IdEmpleado") == null ? 0 : HttpContext.Session.GetString("IdEmpleado"));
            var listaempleadoperfil = await iempleadoperfil.ListarEmpleadoPerfilAsync(IdEmpleado, Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")), null, null, 1, 100, "");
            return View(listaempleadoperfil);
        }
        [HttpGet]
        public IActionResult DirectorioPerfil()
        {
            int IdEmpleado = Convert.ToInt32(Request.Query["IdEmpleado"]);
            ViewBag.IdEmpleado = IdEmpleado;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ListarEmpleadoPerfil(IDataTablesRequest requestModel)
        {
            int IdEmpleado = Convert.ToInt32(Request.Form["IdEmpleado"]);

            RecursoHumano.ListaEmpleadoPerfil listaempleadoperfil = new RecursoHumano.ListaEmpleadoPerfil();
            RecursoHumano.EmpleadoPerfil empleadoperfil = new RecursoHumano.EmpleadoPerfil();
            int numeroPagina = (requestModel.Start / requestModel.Length) + 1;
            try
            {
                listaempleadoperfil = await iempleadoperfil.ListarEmpleadoPerfilAsync(IdEmpleado, Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")), null, null, numeroPagina, requestModel.Length, requestModel.Search.Value);
                if (listaempleadoperfil.mensaje.CodigoMensaje > 0)
                {
                    return RedirectToAction("PaginaError", new { DescripcionError = listaempleadoperfil.mensaje.DescripcionMensaje });
                }
                var totalCount = listaempleadoperfil.paginacion.TotalRegistros;
                var data = listaempleadoperfil.lista.Select(lq => new
                {

                    IdEmpleadoPerfil = lq.IdEmpleadoPerfil,
                    Empleado = lq.empleado.IdEmpleado,
                    EmpresaSede = lq.empresasede.NombreSede,
                    IdEmpresaSede = lq.empresasede.IdEmpresaSede,
                    Area = lq.area.NombreArea,
                    Cargo = lq.cargo.NombreCargo,

                    DestinoTodos = (lq.DestinoTodos == true ? "Si" : "No"),
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
        public async Task<IActionResult> ListarComboEmpleadoPerfil()
        {
            int IdPersona = Convert.ToInt32(Request.Form["IdPersona"]);
            RecursoHumano.ListaEmpleadoPerfil listaempleadoperfil = new RecursoHumano.ListaEmpleadoPerfil();
            RecursoHumano.EmpleadoPerfil empleadoperfil = new RecursoHumano.EmpleadoPerfil();
            try
            {

                listaempleadoperfil = await iempleadoperfil.ListarComboEmpleadoPerfilAsync(IdPersona);

                if (listaempleadoperfil.mensaje.CodigoMensaje > 0)
                {
                    return RedirectToAction("PaginaError", new { DescripcionError = listaempleadoperfil.mensaje.DescripcionMensaje });
                }
                var totalCount = listaempleadoperfil.paginacion.TotalRegistros;
                var data = listaempleadoperfil.lista.Select(lq => new
                {
                    IdEmpleadoPerfil = lq.IdEmpleadoPerfil,
                    NombreEmpleadoPerfil = lq.NombreEmpleadoPerfil

                }).ToList();

                return Json(data);
            }
            catch (Exception ex)
            {
                return RedirectToAction("PaginaError", new { DescripcionError = "Hubo un error [" + ex.Message + "]" });
            }
        }
        [HttpGet]
        public async Task<IActionResult> ListarEmpleadoPerfilPorAutoCompletePorSede()
        {
            string NombreEmpleadoPerfil = Request.Form["NombreEmpleadoPerfil"].ToString();
            int IdEmpresaSede = Convert.ToInt32(Request.Form["IdEmpresaSede"].ToString());
            RecursoHumano.ListaEmpleadoPerfil listaempleadoperfil = new RecursoHumano.ListaEmpleadoPerfil();
            RecursoHumano.EmpleadoPerfil empleadoperfil = new RecursoHumano.EmpleadoPerfil();
            try
            {
                listaempleadoperfil = await iempleadoperfil.ListarEmpleadoPerfilPorAutoCompletePorSedeAsync(NombreEmpleadoPerfil, IdEmpresaSede);
                if (listaempleadoperfil.mensaje.CodigoMensaje > 0)
                {
                    return RedirectToAction("PaginaError", new { DescripcionError = listaempleadoperfil.mensaje.DescripcionMensaje });
                }
                var totalCount = listaempleadoperfil.paginacion.TotalRegistros;
                var data = listaempleadoperfil.lista.Select(lq => new
                {
                    lq.IdEmpleadoPerfil,
                    lq.NombreEmpleadoPerfil
                }).ToList();

                return Json(data);
            }
            catch (Exception ex)
            {
                return RedirectToAction("PaginaError", new { DescripcionError = "Hubo un error [" + ex.Message + "]" });
            }
        }
        [HttpGet]
        public async Task<IActionResult> ListarEmpleadoPerfilPorAutoComplete()
        {
            string NombreEmpleadoPerfil = Request.Form["NombreEmpleadoPerfil"].ToString();
            RecursoHumano.ListaEmpleadoPerfil listaempleadoperfil = new RecursoHumano.ListaEmpleadoPerfil();
            RecursoHumano.EmpleadoPerfil empleadoperfil = new RecursoHumano.EmpleadoPerfil();
            try
            {
                listaempleadoperfil = await iempleadoperfil.ListarEmpleadoPerfilPorAutoCompleteAsync(NombreEmpleadoPerfil);
                if (listaempleadoperfil.mensaje.CodigoMensaje > 0)
                {
                    return RedirectToAction("PaginaError", new { DescripcionError = listaempleadoperfil.mensaje.DescripcionMensaje });
                }
                var totalCount = listaempleadoperfil.paginacion.TotalRegistros;
                var data = listaempleadoperfil.lista.Select(lq => new
                {
                    lq.IdEmpleadoPerfil,
                    lq.NombreEmpleadoPerfil
                }).ToList();

                return Json(data);
            }
            catch (Exception ex)
            {
                return RedirectToAction("PaginaError", new { DescripcionError = "Hubo un error [" + ex.Message + "]" });
            }
        }
        [HttpPost]
        public async Task<IActionResult> RegistroEmpleadoPerfil()
        {
            int IdEmpleado = Convert.ToInt32(Request.Form["IdEmpleado"]);

            int IdEmpleadoPerfil = Convert.ToInt32(Request.Form["IdEmpleadoPerfil"]);
            try
            {
                RecursoHumano.EmpleadoPerfil empleadoperfil = new RecursoHumano.EmpleadoPerfil();
                RecursoHumano.Empleado empleado = new RecursoHumano.Empleado();

                if (IdEmpleadoPerfil > 0)
                {

                    empleadoperfil = await iempleadoperfil.ObtenerEmpleadoPerfilAsync(IdEmpleadoPerfil, Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));

                }

                empleado = await iempleado.ObtenerEmpleadoAsync(IdEmpleado, Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));

                empleadoperfil.empleado = empleado;
                return View(empleadoperfil);
            }
            catch (Exception ex)
            {
                return RedirectToAction("PaginaError", new { DescripcionError = "Hubo un error [" + ex.Message + "]" });
            }
        }
        [HttpPost]
        public async Task<IActionResult> GuardarRegistroEmpleadoPerfil()
        {
            int IdEmpleadoPerfil = Convert.ToInt32(Request.Form["txtRegIdEmpleadoPerfil"]);
            int IdEmpleado = Convert.ToInt32(Request.Form["IdEmpleado"]);
            int IdEmpresaSede = Convert.ToInt32(Request.Form["cmbEmpresaSede"]);

            int IdArea = Convert.ToInt32(Request.Form["cmbArea"]);

            int IdCargo = Convert.ToInt32(Request.Form["cmbCargo"]);
            bool Activo = Convert.ToBoolean(Convert.ToInt32(Request.Form["optActivo"]));
            bool DestinoTodos = Convert.ToBoolean(Convert.ToInt32(Request.Form["optDestinoTodos"]));


            RecursoHumano.EmpleadoPerfil empleadoperfil = new RecursoHumano.EmpleadoPerfil();
            try
            {

                empleadoperfil = await iempleadoperfil.GuardaEmpleadoPerfilAsync(

                                    IdEmpleadoPerfil,
                                            IdEmpleado,
                                            IdEmpresaSede,
                                            IdArea,
                                            IdCargo,
                                            Activo,
                                            DestinoTodos,
                                    Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario"))
            );

                return Json(empleadoperfil);
            }
            catch (Exception ex)
            {
                empleadoperfil.mensaje.CodigoMensaje = 1;
                empleadoperfil.mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA AL GUARDAR, COMUNIQUESE CON EL ADMINISTRADOR DEL SISTEMA";
                empleadoperfil.mensaje.DescripcionMensajeSistema = ex.Message;
                return Json(empleadoperfil);
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> ActualizarEstadoEmpleadoPerfil()
        {
            int IdEmpleadoPerfil = Convert.ToInt32(Request.Form["IdEmpleadoPerfil"]);
            bool Activo =Convert.ToBoolean( Convert.ToInt32(Request.Form["Activo"]));
            Utilitarios.Mensaje mensaje = new Utilitarios.Mensaje();
            try
            {

                mensaje = await iempleadoperfil.ActualizarEstadoEmpleadoPerfil(IdEmpleadoPerfil, Activo,Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));

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
        public async Task<IActionResult> EliminarEmpleadoPerfil()
        {
            int IdEmpleadoPerfil = Convert.ToInt32(Request.Form["IdEmpleadoPerfil"]);

            RecursoHumano.EmpleadoPerfil empleadoperfil = new RecursoHumano.EmpleadoPerfil();
            Utilitarios.Mensaje mensaje = new Utilitarios.Mensaje();
            try
            {

                mensaje = await iempleadoperfil.EliminarEmpleadoPerfilAsync(IdEmpleadoPerfil, Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));

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
        public async Task<IActionResult> ListarEmpleadoPerfilContactos()
        {
            string Search =(Request.Form["Search"]);

            RecursoHumano.ListaEmpleadoPerfil listaempleadoperfil = new RecursoHumano.ListaEmpleadoPerfil();
            RecursoHumano.EmpleadoPerfil empleadoperfil = new RecursoHumano.EmpleadoPerfil();

            try
            {
                listaempleadoperfil = await iempleadoperfil.ListarEmpleadoPerfilContactosAsync(0, Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")), Search);
                if (listaempleadoperfil.mensaje.CodigoMensaje > 0)
                {
                    return RedirectToAction("PaginaError", new { DescripcionError = listaempleadoperfil.mensaje.DescripcionMensaje });
                }
                var totalCount = listaempleadoperfil.paginacion.TotalRegistros;
                var data = listaempleadoperfil.lista.Select(lq => new
                {

                    IdEmpleadoPerfil = lq.IdEmpleadoPerfil,
                    IdEmpleado = lq.empleado.IdEmpleado,
                    Empleado = lq.empleado.persona.NombreCompleto,
                    EmpresaSede = lq.empresasede.NombreSede,
                    IdEmpresaSede = lq.empresasede.IdEmpresaSede,
                    Area = lq.area.NombreArea,
                    Cargo = lq.cargo.NombreCargo,
                    Email = lq.empleado.persona.Email,
                    Celular = lq.empleado.persona.Celular,
                    Anexo = lq.empleado.persona.Anexo,
                    RutaArchivoFoto= lq.empleado.persona.RutaArchivoFoto,

                }).ToList();

                return Json(new { data= listaempleadoperfil });
            }
            catch (Exception ex)
            {
                return RedirectToAction("PaginaError", new { DescripcionError = "Hubo un error [" + ex.Message + "]" });
            }
        }
    }
}