
using DataTables.AspNet.Core; using Microsoft.AspNetCore.Http;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using MatrixService;
using DataTables.AspNet.AspNetCore;
using System.Threading.Tasks;
using General;
using System.Configuration;
using System.Web;
using System.IO;

namespace MatrixWeb.Controllers
{
   // //[ValidarUsuario]
    public class PersonaController : Controller
    {
        private ISvcPersona ipersona;
        private ISvcEmpleado iempleado;
        private ISvcEmpleadoPerfil iempleadoperfil;
        public PersonaController(ISvcPersona _ipersona, ISvcEmpleado _iempleado, ISvcEmpleadoPerfil _iempleadoperfil)
        {
            ipersona = _ipersona;
            iempleado = _iempleado;
            iempleadoperfil = _iempleadoperfil;
        }

        [HttpGet]
        public IActionResult IndexPersona()
        {
            return View();
        }
       

        public async Task<IActionResult> ListarPersonaNaturalPorAutoComplete(string NombreCompleto = "")
        {
            General.ListaPersona listapersona = new General.ListaPersona();
            General.Persona persona = new General.Persona();

            try
            {
               
                    listapersona = await ipersona.ListarPersonaNaturalPorAutoCompleteAsync( NombreCompleto);
                

                if (listapersona.mensaje.CodigoMensaje > 0)
                {
                    return RedirectToAction("PaginaError", new { DescripcionError = listapersona.mensaje.DescripcionMensaje });
                }
                var totalCount = listapersona.paginacion.TotalRegistros;
                var data = listapersona.lista.Select(lq => new
                {
                    NombreCompleto = lq.NombreCompleto,
                    IdPersona = lq.IdPersona
                }).ToList();

                return Json(data);
            }
            catch (Exception ex)
            {
                return RedirectToAction("PaginaError", new { DescripcionError = "Hubo un error [" + ex.Message + "]" });
            }
        }
        [HttpPost]
        public async Task<IActionResult> ListarPersonaNaturalAutoComplete()
        {
            string NombreCompleto = Request.Form["search"];
            General.ListaPersona listapersona = new General.ListaPersona();
            General.Persona persona = new General.Persona();

            try
            {
              
                    listapersona = await ipersona.ListarPersonaNaturalPorAutoCompleteAsync( NombreCompleto);
                

                if (listapersona.mensaje.CodigoMensaje > 0)
                {
                    return RedirectToAction("PaginaError", new { DescripcionError = listapersona.mensaje.DescripcionMensaje });
                }
                var totalCount = listapersona.paginacion.TotalRegistros;
                var data = listapersona.lista.Select(lq => new
                {
                    text = lq.NombreCompleto,
                    id = lq.IdPersona
                }).ToList();

                return Json(new { items = data });
            }
            catch (Exception ex)
            {
                return RedirectToAction("PaginaError", new { DescripcionError = "Hubo un error [" + ex.Message + "]" });
            }
        }
        public async Task<IActionResult> ListarPersonaJuridicaPorAutoComplete(string NombreCompleto = "")
        {
            General.ListaPersona listapersona = new General.ListaPersona();
            General.Persona persona = new General.Persona();

            try
            {
               
                    listapersona = await ipersona.ListarPersonaJuridicaPorAutoCompleteAsync( NombreCompleto);
               
                if (listapersona.mensaje.CodigoMensaje > 0)
                {
                    return RedirectToAction("PaginaError", new { DescripcionError = listapersona.mensaje.DescripcionMensaje });
                }
                var totalCount = listapersona.paginacion.TotalRegistros;
                var data = listapersona.lista.Select(lq => new
                {
                    NombreCompleto = lq.NombreCompleto,
                    IdPersona = lq.IdPersona
                }).ToList();

                return Json(data);
            }
            catch (Exception ex)
            {
                return RedirectToAction("PaginaError", new { DescripcionError = "Hubo un error [" + ex.Message + "]" });
            }
        }
        [HttpPost]
        public async Task<IActionResult> RegistroPersonaCuenta()
        {

            int IdPersona =Convert.ToInt32(HttpContext.Session.GetString("IdPersona"));
            try
            {
                General.Persona persona = new General.Persona();

                if (IdPersona > 0)
                {
                    
                        persona = await ipersona.ObtenerPersonaAsync( IdPersona);
                   
                }
                return View(persona);
            }
            catch (Exception ex)
            {
                return RedirectToAction("PaginaError", new { DescripcionError = "Hubo un error [" + ex.Message + "]" });
            }
        }
        public async Task<IActionResult> ListarPersonaPorAutoComplete(string NombreCompleto = "")
        { 
            General.ListaPersona listapersona = new General.ListaPersona();
            General.Persona persona = new General.Persona();
          
            try
            {               
                    listapersona = await ipersona.ListarPersonaPorAutoCompleteAsync( NombreCompleto);
              

                if (listapersona.mensaje.CodigoMensaje > 0)
                {
                    return RedirectToAction("PaginaError", new { DescripcionError = listapersona.mensaje.DescripcionMensaje });
                }
                var totalCount = listapersona.paginacion.TotalRegistros;
                var data = listapersona.lista.Select(lq => new
                {
                    NombreCompleto = lq.NombreCompleto,
                    IdPersona = lq.IdPersona
                }).ToList();

                return Json(data);
            }
            catch (Exception ex)
            {
                return RedirectToAction("PaginaError", new { DescripcionError = "Hubo un error [" + ex.Message + "]" });
            }
        }
        public async Task<IActionResult> ListarComboAutocompleteMesaParte(string NombreCompleto = "")
        {
            General.ListaPersona listapersona = new General.ListaPersona();
            General.Persona persona = new General.Persona();

            try
            {
                listapersona = await ipersona.ListarComboAutocompleteMesaParte(NombreCompleto);


                if (listapersona.mensaje.CodigoMensaje > 0)
                {
                    return RedirectToAction("PaginaError", new { DescripcionError = listapersona.mensaje.DescripcionMensaje });
                }
                var totalCount = listapersona.paginacion.TotalRegistros;
                var data = listapersona.lista.Select(lq => new
                {
                    NombreCompleto = lq.NombreCompleto,
                    IdPersona = lq.IdPersona
                }).ToList();

                return Json(data);
            }
            catch (Exception ex)
            {
                return RedirectToAction("PaginaError", new { DescripcionError = "Hubo un error [" + ex.Message + "]" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> ListarPersonaBuscarSISAsync()
        {
            General.ListaPersona listapersona = new General.ListaPersona();
            General.Persona persona = new General.Persona();

            int IdPersona =Convert.ToInt32(Request.Form["IdPersona"]);

            try
            {
                listapersona = await ipersona.ListarPersonaBuscarSISAsync(IdPersona);


                if (listapersona.mensaje.CodigoMensaje > 0)
                {
                    return RedirectToAction("PaginaError", new { DescripcionError = listapersona.mensaje.DescripcionMensaje });
                }
                var totalCount = listapersona.paginacion.TotalRegistros;
                var data = listapersona.lista.Select(lq => new
                {
                    NumeroDocumento = lq.NumeroDocumento,
                    TipoDocumento = lq.TipoDocumento

                }).ToList();

                return Json(data);
            }
            catch (Exception ex)
            {
                return RedirectToAction("PaginaError", new { DescripcionError = "Hubo un error [" + ex.Message + "]" });
            }
        }


        [HttpPost]
        public async Task<IActionResult> ListarPersona(IDataTablesRequest requestModel)
        {

            General.ListaPersona listapersona = new General.ListaPersona();
            General.Persona persona = new General.Persona();
            int numeroPagina = (requestModel.Start / requestModel.Length) + 1;
            try
            {
               
                    listapersona = await ipersona.ListarPersonaAsync( Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")), null, null, numeroPagina, requestModel.Length, requestModel.Search.Value);
                
                if (listapersona.mensaje.CodigoMensaje > 0)
                {
                    return RedirectToAction("PaginaError", new { DescripcionError = listapersona.mensaje.DescripcionMensaje });
                }
                var totalCount = listapersona.paginacion.TotalRegistros;
                var data = listapersona.lista.Select(lq => new
                {
                    IdPersona = lq.IdPersona,
                    CatalogoTipoPersona = lq.catalogotipopersona.Descripcion,
                    NombreCompleto = lq.NombreCompleto,                  
                    CatalogoTipoDocumentoPersonal = lq.catalogotipodocumentopersonal.Descripcion,
                    NumeroDocumento = lq.NumeroDocumento,
                    Departamento = lq.ubigeo.Departamento,
                    Provincia = lq.ubigeo.Provincia,
                    Distrito = lq.ubigeo.Distrito,
                    Direccion = lq.Direccion,
                    Celular = lq.Celular
                }).ToList();

                var jsonData = new { draw = requestModel.Draw, recordsFiltered = totalCount, recordsTotal = totalCount, data = data }; return Ok(jsonData);
            }
            catch (Exception ex)
            {
                return RedirectToAction("PaginaError", new { DescripcionError = "Hubo un error [" + ex.Message + "]" });
            }
        }
        [HttpPost]
        public IActionResult RegistroNuevaPersona()
        {
            string NombreModal = Request.Form["NombreModal"].ToString();
            ViewBag.NombreModal = NombreModal;
            General.Persona persona = new General.Persona();
            try
            {
                           
                return View(persona);
            }
            catch (Exception ex)
            {
                persona.mensaje.CodigoMensaje = 1;
                persona.mensaje.DescripcionMensaje = ex.Message;
                return View(persona);
            }
        }
        
        [HttpPost]
        public IActionResult RegistroPersonaConDocumento()
        {
            try
            {
                General.Persona persona = new General.Persona();
                return View(persona);
            }
            catch (Exception ex)
            {
                return RedirectToAction("PaginaError", new { DescripcionError = "Hubo un error [" + ex.Message + "]" });
            }
        }
        [HttpPost]
        public async Task<IActionResult> RegistroPersona()
        {

            int IdPersona = Convert.ToInt32(Request.Form["IdPersona"]);
            try
            {
                General.Persona persona = new General.Persona();

                if (IdPersona > 0)
                {
                   
                        persona = await ipersona.ObtenerPersonaAsync( IdPersona);
                   
                }
                return View(persona);
            }
            catch (Exception ex)
            {
                return RedirectToAction("PaginaError", new { DescripcionError = "Hubo un error [" + ex.Message + "]" });
            }
        }
        [HttpPost]
        public async Task<IActionResult> GuardarRegistroPersona()
        {
            int IdPersona = Convert.ToInt32(Request.Form["txtRegIdPersona"]);
            int IdCatalogoTipoPersona = Convert.ToInt32(Request.Form["cmbCatalogoTipoPersona"]);
            string NombreCompleto = Request.Form["txtNombreCompleto"];
            string Nombres = Request.Form["txtNombres"];
            string Celular = Request.Form["txtCelular"]; 
            string Anexo = Request.Form["txtAnexo"];
            string ApellidoPaterno = Request.Form["txtApellidoPaterno"];
            string ApellidoMaterno = Request.Form["txtApellidoMaterno"];
            int IdCatalogoTipoDocumentoPersonal = Convert.ToInt32(Request.Form["cmbCatalogoTipoDocumentoPersonal"]);
            string NumeroDocumento = Request.Form["txtNumeroDocumento"];

            int IdUbigeo = Convert.ToInt32(Request.Form["cmbDistrito"]);
            string Direccion = Request.Form["txtDireccion"];
            string FechaNacimiento = Request.Form["txtFechaNacimiento"];
            bool Sexo = Convert.ToBoolean(Convert.ToInt32(Request.Form["optSexo"]));


            General.Persona persona = new General.Persona();
            try
            {              
                    persona = await ipersona.GuardaPersonaAsync(
                
                                        IdPersona,
                                                IdCatalogoTipoPersona,
                                                NombreCompleto,
                                                Nombres,
                                                ApellidoPaterno,
                                                ApellidoMaterno,
                                                IdCatalogoTipoDocumentoPersonal,
                                                NumeroDocumento,
                                                IdUbigeo,
                                                Direccion,
                                                Celular,
                                                FechaNacimiento,
                                                Sexo, Anexo,
                                        Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario"))
                );
                
                return Json(persona);
            }
            catch (Exception ex)
            {
                persona.mensaje.CodigoMensaje = 1;
                persona.mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA AL GUARDAR, COMUNIQUESE CON EL ADMINISTRADOR DEL SISTEMA";
                persona.mensaje.DescripcionMensajeSistema = ex.Message;
                return Json(persona);
            }
        }
        [HttpPost]
        public async Task<IActionResult> EliminarPersona()
        {
            int IdPersona = Convert.ToInt32(Request.Form["IdPersona"]);

            General.Persona persona = new General.Persona();
            Utilitarios.Mensaje mensaje = new Utilitarios.Mensaje();
            try
            {
               
                    mensaje = await ipersona.EliminarPersonaAsync( IdPersona, Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));

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

        //[HttpPost]
        //public async Task<IActionResult> ObtenerDatosPersonaReniec()
        //{            
        //    string NumeroDocumento = Request.Form["NumeroDocumento"];
        //    //string NumeroDocumento = "44746294";
        //    Utilitarios.Mensaje mensaje = new Utilitarios.Mensaje();
        //    General.ListaPersona listapersona = new General.ListaPersona();

        //    var persona = new Persona();
        //    try
        //    {
        //        persona = await ipersona.ObtenerDatosPersonaReniec(NumeroDocumento);

        //        if (persona.Nombres == null) {
        //            persona.mensaje.CodigoMensaje = 1;
        //            persona.mensaje.DescripcionMensaje = "DNI No existe, o no hay conexión";
        //        }
              
        //        return Json(persona);

        //    }
        //    catch (Exception ex)
        //    {

        //        persona.mensaje.CodigoMensaje = 1;
        //        persona.mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA EN LA CONEXION";
        //        persona.mensaje.DescripcionMensajeSistema = ex.Message;
        //        return Json(persona);
        //    }
        //}

        [HttpPost]
        public IActionResult ObtenerDatosPersonaReniecV2()
        {
                   
            string NumeroDocumento = Request.Form["NumeroDocumento"];
            Utilitarios.Mensaje mensaje = new Utilitarios.Mensaje();
            General.ListaPersona listapersona = new General.ListaPersona();

            var persona = new Persona();
            try
            {
                persona =  ipersona.ObtenerDatosPersonaReniecV2(NumeroDocumento);

                if (persona.Nombres == null)
                {
                    persona.mensaje.CodigoMensaje = 1;
                    persona.mensaje.DescripcionMensaje = "DNI No existe, o no hay conexión";
                }
                return Json(persona);

            }
            catch (Exception ex)
            {

                persona.mensaje.CodigoMensaje = 1;
                persona.mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA EN LA CONEXION";
                persona.mensaje.DescripcionMensajeSistema = ex.Message;
                return Json(persona);
            }
        }

        [HttpPost]
        public async Task<IActionResult> GuardarPersonaEnUsuario()
        {
            var persona = new General.Persona();
            int IdPersona = 0;
            int IdCatalogoTipoPersona = Convert.ToInt32(Request.Form["cmbCatalogoTipoPersona"]);
            string NombreCompleto = Request.Form["txtNombreCompleto"];
            string Nombres = Request.Form["txtNombres"];
            string Celular = Request.Form["txtCelular"];
            string ApellidoPaterno = Request.Form["txtApellidoPaterno"];
            string ApellidoMaterno = Request.Form["txtApellidoMaterno"];
            int IdCatalogoTipoDocumentoPersonal = Convert.ToInt32(Request.Form["cmbCatalogoTipoDocumentoPersonal"]);
            string NumeroDocumento = Request.Form["txtNumeroDocumento"];
            string OtroCatalogoTipoDocumentoPersonal = Request.Form["txtOtroCatalogoTipoDocumentoPersonal"];

            int IdUbigeo = 2078;// Convert.ToInt32(Request.Form["cmbDistrito"]);
            string Direccion = Request.Form["txtDireccion"];
            string FechaNacimiento = Request.Form["txtFechaNacimiento"];
            bool Sexo = Convert.ToBoolean(Convert.ToInt32(Request.Form["optSexo"]));       

            int IdEmpresaSede = Convert.ToInt32(Request.Form["cmbEmpresaSede"]);
            int IdArea = Convert.ToInt32(Request.Form["cmbArea"]);
            int IdCargo = Convert.ToInt32(Request.Form["cmbCargo"]);
            int IdCatalogoTipoEmpleado = Convert.ToInt32(Request.Form["cmbCatalogoTipoEmpleado"]);
            
            //validacion
            if (Nombres == "" || Nombres == null) { persona.mensaje.CodigoMensaje = 1; persona.mensaje.DescripcionMensaje = "Debe escribir un nombre"; return Json(persona); }
            if (ApellidoPaterno == "" || ApellidoPaterno == "") { persona.mensaje.CodigoMensaje = 1; persona.mensaje.DescripcionMensaje = "Debe escribir el apellido paterno"; return Json(persona); }
            if (ApellidoMaterno == "" || ApellidoMaterno == "") { persona.mensaje.CodigoMensaje = 1; persona.mensaje.DescripcionMensaje = "Debe escribir el apellido materno"; return Json(persona); }
            if (NumeroDocumento == "" || NumeroDocumento == "") { persona.mensaje.CodigoMensaje = 1; persona.mensaje.DescripcionMensaje = "Debe escribir el numero de documento"; return Json(persona); }

            if (IdEmpresaSede == 0) { persona.mensaje.CodigoMensaje = 1; persona.mensaje.DescripcionMensaje = "Debe elegir una sede"; return Json(persona); }
            if (IdArea == 0) { persona.mensaje.CodigoMensaje = 1; persona.mensaje.DescripcionMensaje = "Debe seleccionar un area"; return Json(persona); }
            if (IdCargo == 0) { persona.mensaje.CodigoMensaje = 1; persona.mensaje.DescripcionMensaje = "Debe seleccionar un cargo"; return Json(persona); }

           
            try
            {
                persona = await ipersona.GuardaPersonaAsync(
                IdPersona,
                IdCatalogoTipoPersona,
                NombreCompleto,
                Nombres,
                ApellidoPaterno,
                ApellidoMaterno,
                IdCatalogoTipoDocumentoPersonal,              
                NumeroDocumento,
                IdUbigeo,
                Direccion,
                Celular,
                FechaNacimiento,
                Sexo,    
                "",
                Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));
                if (persona.mensaje.CodigoMensaje == 0)
                {                   
                    //guardar empleado
                    var empleado = await iempleado.GuardaEmpleadoAsync(0, persona.IdPersona, 1, true, IdCatalogoTipoEmpleado , 2, Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));
                    if (empleado.mensaje.CodigoMensaje == 0)
                    {
                        //guardar perfil del empleado
                        var empleadoperfil = await iempleadoperfil.GuardaEmpleadoPerfilAsync(0, empleado.IdEmpleado, IdEmpresaSede, IdArea, IdCargo, true,false, Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));
                    }
                }
                return Json(persona);
            }
            catch (Exception ex)
            {
                persona.mensaje.CodigoMensaje = 1;
                persona.mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA AL GUARDAR, COMUNIQUESE CON EL ADMINISTRADOR DEL SISTEMA";
                persona.mensaje.DescripcionMensajeSistema = ex.Message;
                return Json(persona);
            }
        }
       

    }
    
}









