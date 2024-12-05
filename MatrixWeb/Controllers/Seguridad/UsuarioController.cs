using DataTables.AspNet.Core;
using Microsoft.AspNetCore.Http;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using MatrixService;
using System.Threading.Tasks;
using Utilitarios;
using System.Text;
using System.Configuration;
using System.DirectoryServices;
using iText.Layout.Element;
using Seguridad;
using Sistema;
using Casilla;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Extensions.Logging;
using MatrixUtilitarios;
using System.DirectoryServices.Protocols;
using System.Net;

namespace MatrixWeb.Controllers
{
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public class UsuarioController : Controller
    {
        //public string CaptchaCode { get; set; }

        private readonly ISvcUsuario iusuario;
        private readonly ISvcPersona ipersona;
        private readonly ISvcPais ipais;
        private readonly ISvcCatalogo icatalogo;
        private readonly ISvcPerfilOpcion iperfilopcion;
        private readonly ISvcUsuarioPerfil iusuarioperfil;
        public UsuarioController(ISvcUsuario _iusuario, ISvcPerfilOpcion _iperfilopcion, ISvcUsuarioPerfil _iusuarioperfil, ISvcCatalogo _icatalogo, ISvcPais _ipais, ISvcPersona _ipersona)
        {
            iusuario = _iusuario;
            iperfilopcion = _iperfilopcion;
            iusuarioperfil = _iusuarioperfil;
            icatalogo = _icatalogo;
            ipais = _ipais;
            ipersona = _ipersona;

        }
        public IActionResult Error()
        {
            // Puedes personalizar la lógica y la vista aquí
            return View();
        }
        [HttpGet]
        public IActionResult RecuperarClave()
        {
            return View();
        }
        [HttpPost]
        public async Task<Mensaje> EnviarClaveUsuarioPorEmail()
        {
            var mensaje = new Utilitarios.Mensaje();
            string Email = Request.Form["emailaddress"].ToString();
            string NombreCompleto = "";
            string Clave = "";
            if (Email == "")
            {
                mensaje.DescripcionMensaje = "No ha escrito el Email";
                mensaje.CodigoMensaje = 1;
                return mensaje;
            }
            var usuario = await iusuario.BuscarEmail(Email);
            if (usuario.IdUsuario == 0)
            {
                mensaje.DescripcionMensaje = "No existe el correo en nuestras bases de datos";
                mensaje.CodigoMensaje = 1;
                return mensaje;
            }

            usuario = await iusuario.ObtenerUsuarioAsync(usuario.IdUsuario);
            if (usuario.mensaje.CodigoMensaje > 0)
            {
                mensaje.DescripcionMensaje = usuario.mensaje.DescripcionMensaje;
                mensaje.CodigoMensaje = 1;
                return mensaje;
            }
            NombreCompleto = usuario.persona.NombreCompleto;
            Clave = MatrixUtilitarios.Utilitario.desEncriptar(usuario.Clave);
            try
            {
                var MensajeConfirmacion = System.IO.File.ReadAllText(System.Configuration.ConfigurationManager.AppSettings["RutaArchivosPlantillas"] + "PlantillaCorreoEnvioClaveUsuario.html");
                MensajeConfirmacion = MensajeConfirmacion.Replace("NombreCompleto", "Estimad@ " + NombreCompleto.ToUpper());
                MensajeConfirmacion = MensajeConfirmacion.Replace("UsuarioCasilla", Email);
                MensajeConfirmacion = MensajeConfirmacion.Replace("PasswordCasilla", Clave);
                MensajeConfirmacion = MensajeConfirmacion.Replace("LinkCasillaElectronica", System.Configuration.ConfigurationManager.AppSettings["HostAplicacion"] + "Notificacion/MisNotificaciones");
                MatrixUtilitarios.Utilitario.EnviarEmail(Email, MensajeConfirmacion, "Recuperación de Clave", "", "");
                mensaje.DescripcionMensaje = "Se Envió el Email correctamente";
                mensaje.CodigoMensaje = 0;
                return mensaje;
            }
            catch (Exception ex)
            {
                mensaje.DescripcionMensaje = "Sucedió un error: " + ex.Message;
                mensaje.CodigoMensaje = 1;
                return mensaje;
            }
        }

        [HttpPost]
        public IActionResult ConfirmacionEnvioClaveUsuario()
        {
            string Email = Request.Form["Email"].ToString();
            ViewBag.Email = Email;
            return View();
        }
        [HttpPost]
        public IActionResult ConfirmacionCreacionCuenta()
        {
            string Email = Request.Form["Email"].ToString();
            string Celular = Request.Form["Celular"].ToString();
            ViewBag.Email = Email;
            ViewBag.Celular = Celular;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> EnviarEmailPorClave()
        {
            string Email = Request.Form["Email"].ToString();
            string Clave = Request.Form["Clave"].ToString();
            string ClaveEncriptada = Request.Form["ClaveEncriptada"].ToString();
            var mensaje = new Utilitarios.Mensaje();
            try
            {
                var usuario = await iusuario.BuscarEmail(Email);
                if (usuario.IdUsuario > 0)
                {

                    usuario = await iusuario.ObtenerUsuarioAsync(usuario.IdUsuario);

                    string NombreCompleto = usuario.persona.NombreCompleto;
                    string Logueo = usuario.Logueo;
                    usuario = await iusuario.GuardaCambioClaveAsync(
                 usuario.IdUsuario,
               ClaveEncriptada,
                usuario.Clave,
                usuario.IdUsuario
               );


                    //Enviar Email
                    string MensajeConfirmacion = "<!DOCTYPE html>" +
                            "<html>" +
                            "<head>" +
                            "<style>" +
                            "html {" +
                            "position: relative;" +
                            "font-size: 14px;}" +
                            "body {" +
                            "font-family: -apple - system,BlinkMacSystemFont,'Segoe UI',Roboto,'Helvetica Neue',Arial,sans-serif;" +
                            "font-size: 1rem;" +
                            "line-height: 1.5;" +
                            "color: #373a3c;" +
                            "background - color: #fff;" +
                            "align: center;}" +
                            ".card - header {" +
                            "padding: 0.75rem 1.25rem; " +
                            "background - color: #fff;" +
                            "border-bottom: 1px solid rgba(0, 0, 0, 0.125);}" +
                            ".text-muted {" +
                            "color: #818a91!important;}" +
                            ".card-footer {" +
                            "padding: .75rem 1.25rem;" +
                            "background-color: #fff;" +
                            "border-top: 1px solid rgba(0, 0, 0, .125);}" +
                            ".tablita {" +
                            "width: 100 %;" +
                            "margin: 0 0 0.2em 0;" +
                            "caption-side: top;}" +
                            "</style >" +
                            "</head>" +
                            "<body bgcolor =#8d8e90;>" +
                            "<table class='tablita' border='1' align='center' cellpadding='0' cellspacing='0' style='color: purple; font-family:arial,helvetica,sans-serif; text-align:center;'>" +
                                     "<tr>" +
                                     "<td>" +
                                     "<div class='card-header'>" +
                                     "<h4>BIENVENIDO</h4>" +
                                     "</div>" +
                                     "</td>" +
                                     "</tr>" +
                                     "<tr>" +
                                     "<td style = 'background: #f5f5f5;'>" +
                             "<table align='center' style='padding: 3px 3px 3px 3px;'>" +
                                 "<tr>" +
                                     "<td colspan = '2' align='center'>" +
                                         "<h4><strong>" + NombreCompleto + "</strong></h4>" +
                                     "</td>" +
                                 "</tr>" +
                                  "<tr>" +
                                     "<td colspan = '2'>" +
                                         "Sus credenciales para el acceso a la PLATAFORMA CMP" +
                                     "</td>" +
                                 "</tr>" +
                                 "<tr>" +
                                     "<td style = 'text-align:right;'> Usuario:</td>" +
                                      "<td style = 'text-align:left;'>" + Logueo + "</td>" +
                                  "</tr>" +
                                 "<tr> " +
                                     "<td style='text-align:right;'>Contraseña:</td>" +
                                     "<td style = 'text-align:left;'>" + Clave + "</td>" +
                                 "</tr>" +
                                    "<tr>" +
                                     "<td style='text-align:center;' colspan='2'><a href = '" + ConfigurationManager.AppSettings["HostAplicacion"] + "Usuario/Login' > clíc aquí para ir a la PLATAFORMA - CMP</a></td>" +
                                 "</tr>" +
                             "</table>" +
                             "</br>" +
                             "</br>" +
                             "Por favor no responder este email" +
                         "</td>" +
                         "</tr>" +
                         "<tr>" +
                             "<td>" +
                                 "<div class='card-footer text-muted'>" +
                                         "" +
                                     "</div>" +
                                 "</td>" +
                             "</tr>" +
                         "</Table>" +
                         "</body>" +
                         "</html>";
                    MatrixUtilitarios.Utilitario.EnviarEmail(Email, MensajeConfirmacion, "Bienvenido", "");
                    mensaje.CodigoMensaje = 0;
                    mensaje.DescripcionMensaje = "Se ha enviado sus credenciales a su cuenta de correo electrónico";
                }
                else
                {
                    mensaje.CodigoMensaje = 1;
                    mensaje.DescripcionMensaje = "El Email no está registrado en nuestras bases de datos, vuelva a intentar";
                }

                return Json(mensaje);
            }
            catch (Exception ex)
            {
                mensaje.CodigoMensaje = 1;
                mensaje.DescripcionMensaje = "Sucedió un error al intentar enviar la credenciales";
                mensaje.DescripcionMensajeSistema = ex.Message;
                return Json(mensaje);
            }
        }
        [HttpPost]
        public async Task<IActionResult> EnviarUsuarioPasswordPorEmail(int IdUsuario)
        {
            var mensaje = await EnviarEmailUsuario(IdUsuario, "");
            return Json(mensaje);
        }
        public async Task<Mensaje> EnviarEmailUsuario(int IdUsuario, string Password)
        {
            var mensaje = new Utilitarios.Mensaje();
            try
            {
                var usuario = await iusuario.ObtenerUsuarioAsync(IdUsuario);
                if (usuario.mensaje.CodigoMensaje > 0)
                {
                    mensaje.DescripcionMensaje = usuario.mensaje.DescripcionMensaje;
                    mensaje.CodigoMensaje = 1;
                    return mensaje;
                }

                string email = usuario.Email;
                string Usuario = usuario.Logueo;
                //string Password = MatrixUtilitarios.Utilitario.desEncriptar(usuario.Clave);

                if (!MatrixUtilitarios.Utilitario.ComprobarFormatoEmail(email))
                {
                    mensaje.DescripcionMensaje = "El email no tiene el formato correcto";
                    mensaje.CodigoMensaje = 1;
                    return mensaje;
                }
                string MensajeConfirmacion = "<!DOCTYPE html>" +
                           "<html>" +
                           "<head>" +
                           "<style>" +
                           "html {" +
                           "position: relative;" +
                           "font-size: 14px;}" +
                           "body {" +
                           "font-family: -apple - system,BlinkMacSystemFont,'Segoe UI',Roboto,'Helvetica Neue',Arial,sans-serif;" +
                           "font-size: 1rem;" +
                           "line-height: 1.5;" +
                           "color: #373a3c;" +
                           "background - color: #fff;" +
                           "align: center;}" +
                           ".card - header {" +
                           "padding: 0.75rem 1.25rem; " +
                           "background - color: #fff;" +
                           "border-bottom: 1px solid rgba(0, 0, 0, 0.125);}" +
                           ".text-muted {" +
                           "color: #818a91!important;}" +
                           ".card-footer {" +
                           "padding: .75rem 1.25rem;" +
                           "background-color: #fff;" +
                           "border-top: 1px solid rgba(0, 0, 0, .125);}" +
                           ".tablita {" +
                           "width: 100 %;" +
                           "margin: 0 0 0.2em 0;" +
                           "caption-side: top;}" +
                           "</style >" +
                           "</head>" +
                           "<body bgcolor =#8d8e90;>" +
                           "<table class='tablita' border='1' align='center' cellpadding='0' cellspacing='0' style='color: purple; font-family:arial,helvetica,sans-serif; text-align:center;'>" +
                                    "<tr>" +
                                    "<td>" +
                                    "<div class='card-header'>" +
                                    // "<img src = '" + ConfigurationManager.AppSettings["HostImagenes"] + "LogoInstitucion.png' alt=''>" +
                                    "<h4>BIENVENIDO</h4>" +
                                    "</div>" +
                                    "</td>" +
                                    "</tr>" +
                                    "<tr>" +
                                    "<td style = 'background: #f5f5f5;'>" +
                            "<table align='center' style='padding: 3px 3px 3px 3px;'>" +
                                "<tr>" +
                                    "<td colspan = '2' align='center'>" +
                                        "<h4><strong>" + usuario.persona.NombreCompleto + "</strong></h4>" +
                                    "</td>" +
                                "</tr>" +
                                 "<tr>" +
                                    "<td colspan = '2'>" +
                                        "Sus credenciales para el acceso a la PLATAFORMA CMP" +
                                    "</td>" +
                                "</tr>" +
                                "<tr>" +
                                    "<td style = 'text-align:right;'> Usuario:</td>" +
                                     "<td style = 'text-align:left;'>" + usuario.Logueo + "</td>" +
                                 "</tr>" +
                                "<tr> " +
                                    "<td style='text-align:right;'>Contraseña:</td>" +
                                    "<td style = 'text-align:left;'>" + Password + "</td>" +
                                "</tr>" +
                                   "<tr>" +
                                    "<td style='text-align:center;' colspan='2'><a href = '" + ConfigurationManager.AppSettings["HostAplicacion"] + "Usuario/Login' > clíc aquí para ir a la PLATAFORMA - CMP</a></td>" +
                                "</tr>" +
                            "</table>" +
                            "</br>" +
                            "</br>" +
                            "Por favor no responder este email" +
                        "</td>" +
                        "</tr>" +
                        "<tr>" +
                            "<td>" +
                                "<div class='card-footer text-muted'>" +
                                        "" +
                                    "</div>" +
                                "</td>" +
                            "</tr>" +
                        "</Table>" +
                        "</body>" +
                        "</html>";
                MatrixUtilitarios.Utilitario.EnviarEmail(email, MensajeConfirmacion, "Bienvenido", "");
                mensaje.DescripcionMensaje = "Se Envió el Email correctamente";
                mensaje.CodigoMensaje = 0;
                return mensaje;
            }
            catch (Exception ex)
            {
                mensaje.DescripcionMensaje = "Sucedió un error: " + ex.Message;
                mensaje.CodigoMensaje = 1;
                return mensaje;
            }
        }
        [HttpPost]
        public async Task<IActionResult> ListarCatalogoTipoDocumentoPersonalMesaVirtual()
        {
            var listacatalogo = await icatalogo.ListarCatalogoComboAsync(1, "General");
            var data = listacatalogo.lista.Select(lq => new
            {
                lq.IdCatalogo,
                lq.Descripcion
            }).Where(x => x.IdCatalogo != 15).ToList();
            return Json(data);
        }
        [HttpPost]
        public async Task<IActionResult> ListarComboPaisMesaVirtual()
        {
            var listapais = await ipais.ListarComboPaisAsync(Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));
            var data = listapais.lista.Select(lq => new
            {
                lq.IdPais,
                lq.NombrePais
            }).ToList();
            return Json(data);
        }
        [HttpPost]
        public async Task<IActionResult> ObtenerPersonaPorDocumento(int TipoDocumentoPersona = 0, string NumeroDocumento = "")
        {
            var persona = new General.Persona();
            try
            {
                persona = await ipersona.ObtenerPersonaPorDocumento(NumeroDocumento, TipoDocumentoPersona);
                if (persona.mensaje.CodigoMensaje > 0)
                {
                    return Json(persona);
                }
                if (persona.IdPersona == 0)
                {
                    if (TipoDocumentoPersona == 2)
                    {
                        persona.mensaje.CodigoMensaje = 1;
                        persona.mensaje.DescripcionMensaje = persona.mensaje.DescripcionMensaje + " Por favor registre manualmente los datos del Dni digitado";
                        persona.mensaje.DescripcionMensajeSistema = "";
                        /*
                         * servicio reniec
                         */
                        /*
                        persona = ipersona.ObtenerDatosPersonaReniecV2(NumeroDocumento);
                        persona.IdGenero = persona.IdGenero == 0 ? 1 : 0;
                        if (persona.mensaje.CodigoMensaje > 0)
                        {
                            persona.mensaje.CodigoMensaje = 1;
                            persona.mensaje.DescripcionMensaje = persona.mensaje.DescripcionMensaje + " Por favor registre manualmente los datos del Dni digitado";
                            persona.mensaje.DescripcionMensajeSistema = "";
                        }
                        else
                        {
                            if (persona.FechaNacimiento != "")
                            {
                                persona.FechaNacimiento = MatrixUtilitarios.Utilitario.Right(persona.FechaNacimiento, 2) + "/" + MatrixUtilitarios.Utilitario.Mid(persona.FechaNacimiento, 4, 2) + "/" + MatrixUtilitarios.Utilitario.Left(persona.FechaNacimiento, 4);
                            }
                        }
                        */

                    }
                    else
                    {
                        if (TipoDocumentoPersona == 14)
                        {
                            if (NumeroDocumento.Length != 11)
                            { //no se pudo conseguir la razon social
                                persona.Nombres = "";
                                persona.mensaje.CodigoMensaje = 1;
                                persona.NumeroDocumento = NumeroDocumento;
                                persona.mensaje.DescripcionMensaje = "EL RUC NO CONTIENE LOS 11 DIGITOS O ESTÁ MAL ESCRITO";
                                return Json(persona);
                            }
                            //    persona = await ipersona.ObtenerDatosSunat(NumeroDocumento);
                            if (persona.mensaje.CodigoMensaje > 0 || persona.Nombres == "" || persona.Nombres == null)
                            {
                                persona.Nombres = "";
                                persona.mensaje.CodigoMensaje = 1;
                                persona.mensaje.DescripcionMensaje = " No se pudo conseguir la razon social, intente con otro RUC o digite manualmente la Institución";
                            }
                        }
                        else
                        {
                            persona.mensaje.CodigoMensaje = 1;
                            persona.mensaje.DescripcionMensaje = "Por favor registre manualmente los datos del tipo de documento seleccionado";
                            persona.mensaje.DescripcionMensajeSistema = "";
                        }
                    }
                }
                return Json(persona);
            }
            catch (Exception ex)
            {
                persona.mensaje.CodigoMensaje = 1;
                persona.mensaje.DescripcionMensaje = "Persona no encontrada en nuestras base de datos, por favor registre manualmente";
                persona.mensaje.DescripcionMensajeSistema = ex.Message;
                return Json(persona);
            }
        }
        [HttpPost]
        public async Task<IActionResult> GuardarNuevaCuentaCasilla()
        {
            var usuario = new Seguridad.Usuario();
            var mensaje = new Utilitarios.Mensaje();
            int CatalogoTipoDocumentoPersonal = Convert.ToInt32(Request.Form["cmbTipoDocumento"]);
            string NumeroDocumento = Request.Form["txtNumeroDocumento"].ToString();
            string Nombres = Request.Form["txtNombreCompleto"];
            string ApellidoPaterno = Request.Form["txtApellidoPaterno"].ToString();
            string ApellidoMaterno = Request.Form["txtApellidoMaterno"].ToString();
            string NumeroCelular = Request.Form["txtCelular"].ToString();
            string Email = Request.Form["txtEmail"].ToString().ToLower();
            string Usuario = Request.Form["txtCelular"].ToString();
            string Condiciones = Request.Form["chkCondiciones"];
            int IdVerificacion = Convert.ToInt32(Request.Form["txtIdVerificacion"]);
            if (Condiciones != "on")
            {
                mensaje.CodigoMensaje = 1;
                mensaje.DescripcionMensaje = "DEBE ACEPTAR LOS TERMINOS Y CONDICIONES";
                return Json(mensaje);
            }
            string NombreCompleto = Nombres + " " + ApellidoPaterno + " " + ApellidoMaterno;
            if (CatalogoTipoDocumentoPersonal == 14)
            {
                NombreCompleto = Nombres;
            }
            if (Email == "")
            {
                mensaje.CodigoMensaje = 1;
                mensaje.DescripcionMensaje = "AGREGUE UN EMAIL VALIDO";
                return Json(mensaje);
            }
            else
            {
                if (!MatrixUtilitarios.Utilitario.ValidarEmail(Email))
                {
                    mensaje.CodigoMensaje = 1;
                    mensaje.DescripcionMensaje = "AGREGUE CORRECTAMENTE UN EMAIL VALIDO";
                    return Json(mensaje);
                }
            }

            if (CatalogoTipoDocumentoPersonal == 0)
            {
                mensaje.CodigoMensaje = 1;
                mensaje.DescripcionMensaje = "SELECCIONE CORRECTAMENTE UN TIPO DE DOCUMENTO";
                return Json(mensaje);
            }
            if (NumeroDocumento == "")
            {
                mensaje.CodigoMensaje = 1;
                mensaje.DescripcionMensaje = "INGRESE CORRECTAMENTE UN NUMERO DE DOCUMENTO";
                return Json(mensaje);
            }
            usuario = await iusuario.BuscarEmail(Email);
            if (usuario.IdUsuario > 0)
            {
                mensaje.CodigoMensaje = 1;
                mensaje.DescripcionMensaje = "EL CORREO YA SE ENCUENTRA REGISTRADO";
                return Json(mensaje);
            }
            string Clave = MatrixUtilitarios.Utilitario.Left(MatrixUtilitarios.Utilitario.Encriptar(Email), 10);
            string Password = MatrixUtilitarios.Utilitario.Encriptar(Clave);

            try
            {

                usuario = await iusuario.GuardarNuevaCuentaCasilla(
                                    CatalogoTipoDocumentoPersonal,
                                    NumeroDocumento,
                                    Nombres,
                                    ApellidoPaterno,
                                    ApellidoMaterno,
                                    0,
                                    Email,
                                    Usuario,
                                    "",
                                    0,
                                    "",
                                    NumeroCelular,
                                    Password, IdVerificacion,
                                    Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario"))
                );
                if (usuario.mensaje.CodigoMensaje == 0)
                {
                    var MensajeConfirmacion = System.IO.File.ReadAllText(System.Configuration.ConfigurationManager.AppSettings["RutaArchivosPlantillas"] + "PlantillaCorreoEnvioUsuarioCasilla.html");
                    MensajeConfirmacion = MensajeConfirmacion.Replace("NombreCompleto", "Estimad@ " + NombreCompleto.ToUpper());
                    MensajeConfirmacion = MensajeConfirmacion.Replace("NumeroCelular", NumeroCelular);
                    MensajeConfirmacion = MensajeConfirmacion.Replace("UsuarioCasilla", Email);
                    MensajeConfirmacion = MensajeConfirmacion.Replace("PasswordCasilla", Clave);
                    MensajeConfirmacion = MensajeConfirmacion.Replace("LinkCasillaElectronica", System.Configuration.ConfigurationManager.AppSettings["HostAplicacion"] + "Notificacion/MisNotificaciones");
                    MatrixUtilitarios.Utilitario.EnviarEmail(Email, MensajeConfirmacion, "Casilla Eléctronica", "", "");

                }

                return Json(usuario.mensaje);
            }
            catch (Exception ex)
            {
                mensaje.CodigoMensaje = 1;
                mensaje.DescripcionMensaje = ex.Message;
                return Json(mensaje);
            }
        }
        [HttpPost]
        public async Task<IActionResult> BuscarEmail()
        {
            var usuario = new Seguridad.Usuario();
            var mensaje = new Utilitarios.Mensaje();
            string Email = Request.Form["Email"].ToString();
            if (!MatrixUtilitarios.Utilitario.ValidarEmail(Email))
            {
                mensaje.CodigoMensaje = 1;
                mensaje.DescripcionMensaje = "Escriba correctamente un Email válido";
                return Json(mensaje);
            }

            try
            {
                usuario = await iusuario.BuscarEmail(Email);
                if (usuario.IdUsuario > 0)
                {
                    mensaje.CodigoMensaje = 1;
                    mensaje.DescripcionMensaje = "Este Email no está disponible";
                }
                if (usuario.IdUsuario == 0)
                {
                    mensaje.CodigoMensaje = 2;
                    mensaje.DescripcionMensaje = "Este Email esta disponible";
                }
                return Json(mensaje);
            }
            catch (Exception ex)
            {
                mensaje.CodigoMensaje = 3;
                mensaje.DescripcionMensaje = ex.Message;
                return Json(mensaje);
            }
        }

        [HttpPost]
        public async Task<IActionResult> BuscarUsuario()
        {
            string NombreUsuario = Request.Form["Usuario"].ToString();
            var mensaje = new Utilitarios.Mensaje();
            try
            {
                var usuario = await iusuario.BuscarUsuario(NombreUsuario);
                if (usuario.IdUsuario > 0)
                {
                    mensaje.CodigoMensaje = 1;
                    mensaje.DescripcionMensaje = "Este Usuario no está disponible";
                }
                if (usuario.IdUsuario == 0)
                {
                    mensaje.CodigoMensaje = 2;
                    mensaje.DescripcionMensaje = "Este Usuario esta disponible";
                }
                return Json(mensaje);

            }
            catch (Exception ex)
            {
                mensaje.CodigoMensaje = 3;
                mensaje.DescripcionMensaje = ex.Message;
                return Json(mensaje);
            }
        }
        [HttpGet]
        public IActionResult NuevaCuentaCasilla()
        {
            return View();
        }

        [HttpGet]
        public IActionResult VistaCaptcha()
        {

            Random rd = new Random();
            int rand_num = rd.Next(1000, 9999);
            int[] array = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, };
            int[] captcha_numbers = MatrixUtilitarios.Utilitario.Shuffle(array); ;

            ViewBag.captcha_numbers = captcha_numbers;
            ViewBag.captcha_value = rand_num.ToString();
            ViewBag.current_captcha_codificado = MatrixUtilitarios.Utilitario.Encriptar(rand_num.ToString());
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario"));
            int IdEmpleadoPerfil = Convert.ToInt32(HttpContext.Session.GetString("IdEmpleadoPerfil") == null ? 0 : HttpContext.Session.GetString("IdEmpleadoPerfil"));
            Random rd = new Random();
            int rand_num = rd.Next(1000, 9999);
            int[] array = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, };

            //int array =  { 0, "1", "2", "3" , "4", "5", "6", "7", "8", "9" };
            int[] captcha_numbers = MatrixUtilitarios.Utilitario.Shuffle(array); ;

            var sucursales = iusuario.ObtenerSucursales();

            ViewBag.Sucursales = sucursales;

            ViewBag.captcha_numbers = captcha_numbers;
            ViewBag.captcha_value = rand_num.ToString();
            ViewBag.current_captcha_codificado = MatrixUtilitarios.Utilitario.Encriptar(rand_num.ToString());
            if (IdUsuario > 0) { return RedirectToAction("Cuenta", "Usuario"); }

            //if (IdUsuario>0) { return RedirectToAction("Inicio","CadenaPoaDashBoard"); }
            return View();
        }
        [HttpGet]
        public IActionResult CambiarClave()
        {
            return View();
        }
        //[ValidarUsuario]
        [HttpPost]
        public async Task<IActionResult> CambiarClaveUsuario()
        {
            string Clave = Request.Form["txtClave"];
            string ClaveAnterior = Request.Form["txtClaveAnterior"];
            string ReClave = Request.Form["txtReClave"];

            Seguridad.Usuario usuario = new Seguridad.Usuario();
            if (Clave == ReClave)
            {
                usuario = await iusuario.GuardaCambioClaveAsync(
                  Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")),
               MatrixUtilitarios.Utilitario.Encriptar(Clave),
                MatrixUtilitarios.Utilitario.Encriptar(ClaveAnterior),
                 Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario"))
                 );
            }
            else
            {
                usuario.mensaje.DescripcionMensaje = "LAS CONTRASEÑAS NO COINCIDEN";
                usuario.mensaje.CodigoMensaje = 1;
            }
            //usuario.Clave = MatrixUtilitarios.Utilitario.desEncriptar(Clave); ;

            return Json(usuario.mensaje);
        }
        /*
        private async Task<Mensaje> IsCaptchaVerified(string captchaImage)
        {

            var mensaje = new Mensaje();
            try
            {
                using var client = new HttpClient();
                var url = "https://www.google.com/recaptcha/api/siteverify?secret=" + ConfigurationManager.AppSettings["SecretKeyWebServerReCaptchaV3"].ToString() + "&response=" + captchaImage;
                var response = await client.PostAsync(url, null);
                var jsonString = await response.Content.ReadAsStringAsync();
                var captchaVerfication = JsonConvert.DeserializeObject<GoogleCaptchConfig>(jsonString);

                var result = captchaVerfication.success;
                if (result)
                {
                    mensaje.CodigoMensaje = 0;
                    mensaje.DescripcionMensaje = "VALIDACIÓN DE CAPTCHA CORRECTA";
                }
                else
                {
                    mensaje.CodigoMensaje = 1;
                    mensaje.DescripcionMensaje = "VALIDACIÓN DE CAPTCHA INCORRECTA";
                }
                return mensaje;
            }
            catch (Exception e)
            {
                mensaje.DescripcionMensaje = "ERROR AL VALIDAR CAPTCHA [" + e.Message + "]";
                mensaje.CodigoMensaje = 1;
                return mensaje;
            }
        }
        */

        public bool AutenticaLDAP(string username, string pwd)
        {
            bool validation;
            try
            {
                LdapConnection ldc = new LdapConnection(new LdapDirectoryIdentifier((string)null, false, false));
                NetworkCredential nc = new NetworkCredential(username, pwd, ConfigurationManager.AppSettings["Dominio"].ToString());
                ldc.Credential = nc;
                ldc.AuthType = AuthType.Negotiate;
                ldc.Bind(nc); // user has authenticated at this point, as the credentials were used to login to the dc.
                validation = true;
            }
            catch (LdapException)
            //catch (Exception ex)
            {
                validation = false;
            }
            return validation;
        }
        //public bool AutenticaLDAP(string username, string pwd)
        //{

        //    string domainAndUsername = ConfigurationManager.AppSettings["Dominio"].ToString() + @"\" + username;
        //    string path = ConfigurationManager.AppSettings["Path_Ldap"];

        //    try
        //    {

        //        using (DirectoryEntry entry = new DirectoryEntry(path, username, pwd))
        //        {
        //            using (DirectorySearcher searcher = new DirectorySearcher(entry))
        //            {
        //                //Buscamos por la propiedad SamAccountName
        //                searcher.Filter = "(samaccountname=" + username + ")";
        //                //Buscamos el usuario con la cuenta indicada
        //                var result = searcher.FindOne();
        //                if (result != null)
        //                {
        //                    return true;
        //                }
        //                else
        //                    return false;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}

        [HttpPost]
        public async Task<IActionResult> AutenticarUsuario()
        {

            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario"));
            int IdEmpleadoPerfil = Convert.ToInt32(HttpContext.Session.GetString("IdEmpleadoPerfil") == null ? 0 : HttpContext.Session.GetString("IdEmpleadoPerfil"));
            if (IdUsuario > 0) { return RedirectToAction("Cuenta", "Usuario"); }
            var UsuarioLogueado = new Seguridad.Usuario();
            string captchaImage = HttpContext.Request.Form["captcha"].ToString().Replace("{", "").Replace("}", "");
            string current_captcha_codificado = MatrixUtilitarios.Utilitario.desEncriptar(HttpContext.Request.Form["current_captcha_codificado"].ToString());
            try
            {
                if (string.IsNullOrEmpty(captchaImage))
                {
                    UsuarioLogueado.mensaje.CodigoMensaje = 1;
                    UsuarioLogueado.mensaje.DescripcionMensaje = "VERIFICACION CAPTCHA INVALIDA";
                    return Json(UsuarioLogueado.mensaje);
                }
                if (current_captcha_codificado.ToString() != captchaImage.ToString())
                {
                    UsuarioLogueado.mensaje.DescripcionMensaje = "VERIFICACION CAPTCHA INVALIDA";
                    UsuarioLogueado.mensaje.CodigoMensaje = 1;
                    return Json(UsuarioLogueado.mensaje);
                }
                string Logue = Request.Form["Usuario"];
                string Sucursal = Request.Form["Sucursal"];
                string campoEspecial = Request.Form["Ubicacion"];

                string clave = Request.Form["Clave"];//trae clave sin encriptar
                string pasword = MatrixUtilitarios.Utilitario.Encriptar(clave);// Request.Form["Password"];//trae clave encriptada
                string ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Dirección IP no disponible";
                UsuarioLogueado = await iusuario.AutenticarUsuarioAsync(Logue, pasword, ipAddress);
                UsuarioLogueado.Clave = "";
                //VALIDAR A TODO USUARIO Q ENTRE POR EL LOGIN
                
                if (!AutenticaLDAP(Logue, clave))
                {
                    //SI NO EXISTE EN EL LDAP, ES POR DOS MOTIVOS, QUE NO INGRESO SU CLAVE CORECTAMENTE O QUE NO ESTA REGISTRADO EN EL LDAP
                    //AHORA SE BUSCARÁ EN EL SISTEMA SI EXISTE
                    UsuarioLogueado = await iusuario.AutenticarUsuarioAsync(Logue, pasword, ipAddress);
                }
                else
                {
                    //SI EXISTE EN EL LDAP E INGRESO CORRECTAMENTE SUS CREDENCIALES
                    //BUSCAREMOS SUS DATOS DE USUARIO
                    UsuarioLogueado = await iusuario.ObtenerDatosUsuarioAutenticadoLdap(Logue, ipAddress);
                    if (UsuarioLogueado.IdUsuario == 0)
                    {
                        //NO EXISTEN EN EL SISTEMA, ENTONCES ENVIAR MENSAJE QUE NO EXISTE EN SISTEMA
                        UsuarioLogueado.mensaje.CodigoMensaje = 1;
                        UsuarioLogueado.mensaje.DescripcionMensaje = "Usuario no está registrado en el Sistema, comuiniquese con el Administrador";
                        return Json(UsuarioLogueado);
                    }
                }
               
                //UsuarioLogueado = await iusuario.AutenticarUsuarioAsync(Logue, clave);
                string Bienvenido = "Bienvenido ";
                //if (UsuarioLogueado.mensaje.CodigoMensaje == 0)
                if (UsuarioLogueado.IdUsuario != 0)
                {
                    if (UsuarioLogueado.persona.Sexo) { Bienvenido = "Bienvenida "; }
                    UsuarioLogueado.mensaje.DescripcionMensaje = Bienvenido + "<b>" + UsuarioLogueado.persona.NombreCompleto + "</b>" + ", que tenga un buen día.";
                    HttpContext.Session.SetString("Login", UsuarioLogueado.Logueo);

                    HttpContext.Session.SetString("Email", UsuarioLogueado.Email);
                    HttpContext.Session.SetString("NumeroDocumento", UsuarioLogueado.persona.NumeroDocumento);
                    HttpContext.Session.SetString("RutaFotoUsuario", UsuarioLogueado.RutaArchivoFoto);
                    HttpContext.Session.SetString("IdPersona", UsuarioLogueado.persona.IdPersona.ToString());
                    HttpContext.Session.SetString("NombreCompleto", UsuarioLogueado.persona.NombreCompleto);
                    HttpContext.Session.SetString("IdUsuario", UsuarioLogueado.IdUsuario.ToString());
                    HttpContext.Session.SetString("Sucursal", Sucursal.ToString());

                    HttpContext.Session.SetString("NumeroCelular", UsuarioLogueado.persona.Celular);
                    HttpContext.Session.SetString("TelefonoFijo", UsuarioLogueado.persona.TelefonoFijo);
                    HttpContext.Session.SetString("Direccion", UsuarioLogueado.persona.Direccion);
                    HttpContext.Session.SetString("IdCatalogoTipoDocumento", UsuarioLogueado.persona.catalogotipodocumentopersonal.IdCatalogo.ToString());
                    HttpContext.Session.SetString("IdAdministrado", UsuarioLogueado.IdAdministrado.ToString());
                    /*PIDE*/
                    string IdCatalogoTipoDocumentoPIDE = "";
                    if (UsuarioLogueado.persona.catalogotipodocumentopersonal.IdCatalogo == 2) { IdCatalogoTipoDocumentoPIDE = "1"; }
                    if (UsuarioLogueado.persona.catalogotipodocumentopersonal.IdCatalogo == 3) { IdCatalogoTipoDocumentoPIDE = "2"; }
                    HttpContext.Session.SetString("IdCatalogoTipoDocumentoPIDE", IdCatalogoTipoDocumentoPIDE);

                    HttpContext.Session.SetString("IdArea", "0");
                    HttpContext.Session.SetString("IdCargo", "0");
                    HttpContext.Session.SetString("TipoCargo", "0");
                    HttpContext.Session.SetString("Area", "");
                    HttpContext.Session.SetString("AbreviaturaArea", "");
                    HttpContext.Session.SetString("Cargo", "");
                    HttpContext.Session.SetString("IdEmpleadoPerfil", "0");
                    HttpContext.Session.SetString("IdEmpleado", "0");
                    HttpContext.Session.SetString("NombreEmpleadoPerfil", "");
                    HttpContext.Session.SetString("Idsede", "0");
                    HttpContext.Session.SetString("NombreSede", "");
                    HttpContext.Session.SetString("IdEmpresa", "0");
                    HttpContext.Session.SetString("IdEmpresaPadre", "0");
                    HttpContext.Session.SetString("NombreEmpresa", MatrixUtilitarios.DatosGlobales.NombreInstitucion);

                    HttpContext.Session.SetString("IdCatalogoTipoEmpleado", "");
                    HttpContext.Session.SetString("CatalogoTipoEmpleado", "");

                    if (!string.IsNullOrEmpty(campoEspecial))
                    {
                        HttpContext.Session.SetString("Ubicacion", campoEspecial);
                    }

                }
                return Json(UsuarioLogueado.mensaje);
            }
            catch (Exception ex)
            {
                UsuarioLogueado.mensaje.CodigoMensaje = 1;
                UsuarioLogueado.mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA EN LA ACCION [AutenticarPerfil] DEL CONTROLADOR USUARIO VERIFICAR CONSOLA";
                UsuarioLogueado.mensaje.DescripcionMensajeSistema = ex.Message;
                return Json(UsuarioLogueado.mensaje);
            }
        }
        [HttpPost]
        public async Task<IActionResult> AutenticarPerfil()
        {
            int IdEmpleadoPerfil = Convert.ToInt32(Request.Form["cmbUsuarioPerfil"] == "null" ? "0" : Request.Form["cmbUsuarioPerfil"]);
            Seguridad.UsuarioPerfil usuarioperfil = new Seguridad.UsuarioPerfil();
            try
            {
                if (IdEmpleadoPerfil != 0)
                {

                    usuarioperfil = await iusuarioperfil.ObtenerAreaCargoPorEmpleadoPerfilAsync(IdEmpleadoPerfil);

                    if (usuarioperfil.mensaje.CodigoMensaje == 0)
                    {
                        HttpContext.Session.SetString("IdArea", usuarioperfil.empleadoperfil.area.IdArea.ToString());
                        HttpContext.Session.SetString("IdCargo", usuarioperfil.empleadoperfil.cargo.IdCargo.ToString());
                        HttpContext.Session.SetString("Area", usuarioperfil.empleadoperfil.area.NombreArea);
                        HttpContext.Session.SetString("AbreviaturaArea", usuarioperfil.empleadoperfil.area.Abreviatura);
                        HttpContext.Session.SetString("Cargo", usuarioperfil.empleadoperfil.cargo.NombreCargo);
                        HttpContext.Session.SetString("TipoCargo", usuarioperfil.empleadoperfil.cargo.catalogotipocargo.IdCatalogo.ToString());
                        HttpContext.Session.SetString("IdEmpleado", usuarioperfil.empleadoperfil.empleado.IdEmpleado.ToString());

                        HttpContext.Session.SetString("IdCatalogoTipoEmpleado", usuarioperfil.empleadoperfil.empleado.catalogotipoempleado.IdCatalogo.ToString());
                        HttpContext.Session.SetString("CatalogoTipoEmpleado", usuarioperfil.empleadoperfil.empleado.catalogotipoempleado.Detalle.ToString());

                        HttpContext.Session.SetString("IdEmpleadoPerfil", IdEmpleadoPerfil.ToString());
                        HttpContext.Session.SetString("NombreEmpleadoPerfil", usuarioperfil.empleadoperfil.NombreEmpleadoPerfil);
                        HttpContext.Session.SetString("Idsede", usuarioperfil.empleadoperfil.empresasede.IdEmpresaSede.ToString());
                        HttpContext.Session.SetString("NombreSede", usuarioperfil.empleadoperfil.empresasede.NombreSede);
                        HttpContext.Session.SetString("IdEmpresa", usuarioperfil.empleadoperfil.empresasede.empresa.IdEmpresa.ToString());
                        HttpContext.Session.SetString("IdEmpresaPadre", usuarioperfil.empleadoperfil.empresasede.empresa.IdEmpresaPadre.ToString());
                        HttpContext.Session.SetString("NombreEmpresa", usuarioperfil.empleadoperfil.empresasede.empresa.NombreEmpresa);
                        HttpContext.Session.SetString("RucEmpresa", usuarioperfil.empleadoperfil.empresasede.empresa.persona.NumeroDocumento);
                        HttpContext.Session.SetString("NombreCompletoEmpresa", usuarioperfil.empleadoperfil.empresasede.empresa.persona.NombreCompleto);

                    }
                    else
                    {
                        return Json(usuarioperfil);
                    }
                }


                return Json(usuarioperfil);
            }
            catch (Exception ex)
            {
                usuarioperfil.mensaje.CodigoMensaje = 1;
                usuarioperfil.mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA EN LA ACCION [AutenticarPerfil] DEL CONTROLADOR USUARIO VERIFICAR CONSOLA";
                usuarioperfil.mensaje.DescripcionMensajeSistema = ex.Message;
                return Json(usuarioperfil);
            }
        }
        public async Task<IActionResult> ActualizarMenu(string Modulo, string Controlador, string Vista, string Parametro)
        {
            string NuevoMenuHtml = string.Empty;
            Seguridad.ListaPerfilOpcion listaperfilopcion = new Seguridad.ListaPerfilOpcion();
            Seguridad.PerfilOpcion perfilopcion = new Seguridad.PerfilOpcion();

            listaperfilopcion = await iperfilopcion.ListarMenuPorUsuarioAsync(Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")), Convert.ToInt32(HttpContext.Session.GetString("IdEmpleadoPerfil") == null ? 0 : HttpContext.Session.GetString("IdEmpleadoPerfil")));


            HttpContext.Session.SetOject("ListaPermiso", listaperfilopcion.lista);

            string menuPrincipal = "";
            // string dd = Request.ApplicationPath;
            var listaModulo = listaperfilopcion.lista.Where(y => y.opcion.catalogotipoopcion.IdCatalogo != 8).GroupBy(x => new { x.opcion.modulo.IdModulo, x.opcion.modulo.NombreModulo, x.opcion.modulo.NuevoModulo, x.opcion.modulo.RutaImagenModulo })
                    .Select(y => new Sistema.Modulo
                    {
                        IdModulo = y.Key.IdModulo,
                        NombreModulo = y.Key.NombreModulo,
                        RutaImagenModulo = y.Key.RutaImagenModulo,
                        NuevoModulo = y.Key.NuevoModulo
                    }
                    );

            foreach (var modulo in listaModulo)
            {


                string nuevomodulo = "";
                if (modulo.NuevoModulo) { nuevomodulo = ""; }// "<span class='label label-rouded label-menu label-warning'>new</span>"; }

                string ModuloSeleccionado = "<li class='menu-item'>" +
                            "<a href='#menu" + modulo.IdModulo.ToString() + "' data-bs-toggle='collapse' class='menu-link collapsed' aria-expanded='false'>" +
                                "<span class='menu-icon'><i class='" + modulo.RutaImagenModulo + "'></i></span>" +
                                "<span class='menu-text'> " + modulo.NombreModulo + " </span>" +
                                "<span class='menu-arrow'></span>" +
                           "</a>" +
                            "<div class='collapse' id='menu" + modulo.IdModulo.ToString() + "' style>" +
                            "<ul class='sub-menu'>";
                if (modulo.IdModulo.ToString() == Modulo)
                {
                    ModuloSeleccionado = "<li class='menu-item'>" +
                            "<a href='#menu" + modulo.IdModulo.ToString() + "' data-bs-toggle='collapse' class='menu-link' aria-expanded='true'>" +
                                "<span class='menu-icon'><i class='mdi mdi-map-outline'></i></span>" +
                                "<span class='menu-text'> " + modulo.NombreModulo + " </span>" +
                                "<span class='menu-arrow'></span>" +
                            "</a>" +
                             "<div class='collapse show' id='menu" + modulo.IdModulo.ToString() + "'>" +
                            "<ul class='sub-menu'>";

                }

                menuPrincipal = menuPrincipal + ModuloSeleccionado + nuevomodulo;



                foreach (var opcionPadre in listaperfilopcion.lista.Where(x => x.opcion.IdOpcionPadre == 0 && (x.opcion.modulo.NombreModulo == modulo.NombreModulo)))
                {


                    //if (modulo.IdModulo.ToString() == Modulo && opcionPadre.opcion.catalogotipoopcion.IdCatalogo == 6)
                    //{
                    //    foreach (var opcionHijo in listaperfilopcion.lista.Where(x => x.opcion.IdOpcionPadre == opcionPadre.opcion.IdOpcion).OrderBy(x => x.opcion.OrdenOpcion))
                    //    {
                    //        if (modulo.IdModulo.ToString() == Modulo && Controlador == opcionHijo.opcion.Controlador && Vista == opcionHijo.opcion.Accion)
                    //        {
                    //            OpcionSeleccionada = "nav-item active";
                    //        }
                    //    }
                    //}
                    //else
                    //{
                    //    if (modulo.IdModulo.ToString() == Modulo && Controlador == opcionPadre.opcion.Controlador && Vista == opcionPadre.opcion.Accion)
                    //    {
                    //        OpcionSeleccionada = "<li class='menu-item'>" +
                    //                                "<a href='" + relativeUrl + "' class='menu-link'>" +
                    //                                    "<span class='menu-text'>"+ opcionPadre.opcion.NombreOpcion + "</span>" +
                    //                                "</a>" +
                    //                            "</li>";
                    //    }
                    //}
                    //if (opcionPadre.opcion.catalogotipoopcion.IdCatalogo == 7 && (opcionPadre.opcion.NombreOpcion == "Ayuda Memoria" || opcionPadre.opcion.NombreOpcion == "8 UIT" || opcionPadre.opcion.NombreOpcion == "Proceso Selección"))
                    //{
                    //    var relativeUrl = opcionPadre.opcion.NombreFormulario + Convert.ToInt32(HttpContext.Session.GetString("IdUsuario")) + opcionPadre.opcion.RutaFormulario + Convert.ToInt32(HttpContext.Session.GetString("IdArea"));
                    //    string link1 = "<li class='" + OpcionSeleccionada + "' style='background-color:#950606!important'><a target=\"_blank\" background-color:#950606!important' href = '" + relativeUrl + "' class='nav-link'><i class='" + opcionPadre.opcion.RutaImagenOpcion + "'></i><span class='title'>" + opcionPadre.opcion.NombreOpcion + "</span></a></li>";
                    //    menuPrincipal = menuPrincipal + link1;
                    //}
                    //else if (opcionPadre.opcion.catalogotipoopcion.IdCatalogo == 7)
                    //{
                    //    var relativeUrl = Url.Action("ActualizarMenu", "Usuario", new { Modulo = modulo.IdModulo, Controlador = opcionPadre.opcion.Controlador, Vista = opcionPadre.opcion.Accion, Parametro = opcionPadre.opcion.Parametros });
                    //    string link1 = "<li class='" + OpcionSeleccionada + "' style='background-color:#950606!important'><a style='background-color:#950606!important' href = '" + relativeUrl + "' class='nav-link'><i class='" + opcionPadre.opcion.RutaImagenOpcion + "'></i><span class='title'>" + opcionPadre.opcion.NombreOpcion + "</span></a></li>";
                    //    menuPrincipal = menuPrincipal + link1;
                    //}

                    //if (opcionPadre.opcion.catalogotipoopcion.IdCatalogo == 9)
                    //{
                    //    var relativeUrl = opcionPadre.opcion.Accion;//ACCION O LINK
                    //    string link1 = "<li class='" + OpcionSeleccionada + "' style='background-color:#950606!important'><a target=\"_blank\" style='background-color:#950606!important' href = '" + relativeUrl + "' class='nav-link'><i class='" + opcionPadre.opcion.RutaImagenOpcion + "'></i><span class='title'>" + opcionPadre.opcion.NombreOpcion + "</span></a></li>";
                    //    menuPrincipal = menuPrincipal + link1;
                    //}

                    string ariaExpanded = "aria-expanded='false'";
                    string menuLink = "menu-link collapsed";
                    string divCollapse = "collapse";
                    if (opcionPadre.opcion.catalogotipoopcion.IdCatalogo == 6)
                    {
                        foreach (var opcionHijo in listaperfilopcion.lista.Where(x => x.opcion.IdOpcionPadre == opcionPadre.opcion.IdOpcion).OrderBy(x => x.opcion.OrdenOpcion))
                        {
                            if (modulo.IdModulo.ToString() == Modulo && Controlador == opcionHijo.opcion.Controlador && Vista == opcionHijo.opcion.Accion)
                            {
                                ariaExpanded = "aria-expanded='true'";
                                menuLink = "menu-link";
                                divCollapse = "collapse show";
                            }
                        }


                        menuPrincipal = menuPrincipal +
                                            "<li class='menu-item'>" +
                                                "<a href='#subMenu" + opcionPadre.opcion.IdOpcion + "' data-bs-toggle='collapse' class='" + menuLink + "' " + ariaExpanded + ">" +
                                                    "<span class='menu-text'>" + opcionPadre.opcion.NombreOpcion + "</span>" +
                                                    "<span class='menu-arrow'>" +
                                                "</a>" +
                                                "<div class='" + divCollapse + "' id='subMenu" + opcionPadre.opcion.IdOpcion + "'>" +
                                                    "<ul class='sub-menu'>";


                        //menuPrincipal = menuPrincipal + "<li class='" + OpcionSeleccionada + "' style='background-color:#950606!important'><a style='background-color:#950606!important' class='nav-link nav-toggle'><i class='" + opcionPadre.opcion.RutaImagenOpcion + "'></i>" + opcionPadre.opcion.NombreOpcion + "<span class='arrow'></span></a><ul class='sub-menu'>";
                        foreach (var opcionHijo in listaperfilopcion.lista.Where(x => x.opcion.IdOpcionPadre == opcionPadre.opcion.IdOpcion).OrderBy(x => x.opcion.OrdenOpcion))
                        {
                            string SubOpcionSeleccionada = "";
                            string itemactivo = "";
                            if (modulo.IdModulo.ToString() == Modulo && Controlador == opcionHijo.opcion.Controlador && Vista == opcionHijo.opcion.Accion)
                            {
                                SubOpcionSeleccionada = " active";
                                itemactivo = " menuitem-active";
                            }

                            if (opcionHijo.opcion.catalogotipoopcion.IdCatalogo == 7)
                            {
                                var relativeUrl1 = Url.Action("ActualizarMenu", "Usuario", new { Modulo = modulo.IdModulo, Controlador = opcionHijo.opcion.Controlador, Vista = opcionHijo.opcion.Accion, Parametro = opcionHijo.opcion.Parametros });//Url.Action(opcionHijo.opcion.Accion, opcionHijo.opcion.Controlador) + opcionHijo.opcion.Parametros;
                                menuPrincipal = menuPrincipal +
                                                "<li class='menu-item" + itemactivo + "'>" +
                                                    "<a href='" + relativeUrl1 + "' class='menu-link" + SubOpcionSeleccionada + "'>" +
                                                        "<span class='menu-text'>" + opcionHijo.opcion.NombreOpcion + "</span>" +
                                                    "</a>" +
                                                "</li>";
                            }
                            else
                            {
                                if (opcionHijo.opcion.catalogotipoopcion.IdCatalogo == 9)
                                {
                                    var relativeUrl = opcionHijo.opcion.Accion;//ACCION O LINK
                                    string link11 = "<li class='" + SubOpcionSeleccionada + "' style='background-color:#950606!important'><a target=\"_blank\" style='background-color:#950606!important' href = '" + relativeUrl + "' class='nav-link'><i class='" + opcionHijo.opcion.RutaImagenOpcion + "'></i><span class='title'>" + opcionHijo.opcion.NombreOpcion + "</span></a></li>";
                                    menuPrincipal = menuPrincipal + link11;
                                }
                                else
                                {
                                    menuPrincipal = menuPrincipal + "error";
                                }
                            }
                        }
                        menuPrincipal = menuPrincipal + "</ul></div></li>";
                    }
                    if (opcionPadre.opcion.catalogotipoopcion.IdCatalogo == 7)
                    {
                        string SubOpcionSelec = "";
                        string itemactivoSelect = "";
                        var relativ = Url.Action("ActualizarMenu", "Usuario", new { Modulo = modulo.IdModulo, Controlador = opcionPadre.opcion.Controlador, Vista = opcionPadre.opcion.Accion, Parametro = opcionPadre.opcion.Parametros });//Url.Action(opcionHijo.opcion.Accion, opcionHijo.opcion.Controlador) + opcionHijo.opcion.Parametros;
                        if (modulo.IdModulo.ToString() == Modulo && Controlador == opcionPadre.opcion.Controlador && Vista == opcionPadre.opcion.Accion)
                        {
                            SubOpcionSelec = " active";
                            itemactivoSelect = " menuitem-active";
                        }                                                                                                                                                                                                                  //string link2 = "<li class='" + SubOpcionSeleccionada + "' style='background-color:#950606!important'><a style='background-color:#950606!important' href = '" + relativeUrl1 + "' class='nav-link'><i></i><span class='title'>" + opcionHijo.opcion.NombreOpcion + "</span></a></li>";
                                                                                                                                                                                                                                           //menuPrincipal = menuPrincipal + link2;
                        menuPrincipal = menuPrincipal +
                                        "<li class='menu-item" + itemactivoSelect + "'>" +
                                            "<a href='" + relativ + "' class='menu-link" + SubOpcionSelec + "'>" +
                                                "<span class='menu-text'>" + opcionPadre.opcion.NombreOpcion + "</span>" +
                                            "</a>" +
                                        "</li>";
                    }
                }
                menuPrincipal = menuPrincipal + "</ul></div></li>";
            }
            HttpContext.Session.SetString("MenuPrincipal", menuPrincipal);
            if (Modulo == null || Modulo == "")
            {
                if (Convert.ToInt32(HttpContext.Session.GetString("IdEmpleadoPerfil") == null ? 0 : HttpContext.Session.GetString("IdEmpleadoPerfil")) == 0)
                {
                    return RedirectToAction("Login", "Usuario");
                }
                else
                {
                    return RedirectToAction("Cuenta", "Usuario");
                }
            }
            else
            {

                if (Parametro != null && Parametro != "")
                {
                    var p = new Parametro();
                    string[] items = Parametro.Split(',');
                    p.Parametro1 = items[0];
                    if (items.Count() > 1)
                    {
                        p.Parametro2 = items[1];
                    }
                    if (items.Count() > 2)
                    {
                        p.Parametro3 = items[2];
                    }
                    if (items.Count() > 3)
                    {
                        p.Parametro4 = items[3];
                    }

                    return RedirectToAction(Vista, Controlador, p);
                }
                else
                {
                    return RedirectToAction(Vista, Controlador);
                }

            }

        }
        //[ValidarUsuario]
        [HttpGet]
        public async Task<IActionResult> Cuenta()
        {
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetString("IdUsuario"));
            //aca colocar una restriccion de si es de la institucion o no, y jalar una variable de sesion EsInstitucion
            int IdEmpleadoPerfil = Convert.ToInt32(HttpContext.Session.GetString("IdEmpleadoPerfil"));
            //si es institucion entonces traer el primer perfil
            if (IdEmpleadoPerfil == 0)
            {

                var listausuarioperfil = iusuarioperfil.ListarComboUsuarioPerfil(IdUsuario);
                if (listausuarioperfil.lista.Count > 0)
                {
                    IdEmpleadoPerfil = listausuarioperfil.lista[0].empleadoperfil.IdEmpleadoPerfil;
                }
                var usuarioperfil = await iusuarioperfil.ObtenerAreaCargoPorEmpleadoPerfilAsync(IdEmpleadoPerfil);

                if (usuarioperfil.mensaje.CodigoMensaje == 0 && IdEmpleadoPerfil > 0)
                {
                    HttpContext.Session.SetString("IdArea", usuarioperfil.empleadoperfil.area.IdArea.ToString());
                    HttpContext.Session.SetString("IdCargo", usuarioperfil.empleadoperfil.cargo.IdCargo.ToString());
                    HttpContext.Session.SetString("Area", usuarioperfil.empleadoperfil.area.NombreArea);
                    HttpContext.Session.SetString("AbreviaturaArea", usuarioperfil.empleadoperfil.area.Abreviatura);
                    HttpContext.Session.SetString("Cargo", usuarioperfil.empleadoperfil.cargo.NombreCargo);
                    HttpContext.Session.SetString("TipoCargo", usuarioperfil.empleadoperfil.cargo.catalogotipocargo.IdCatalogo.ToString());
                    HttpContext.Session.SetString("IdEmpleado", usuarioperfil.empleadoperfil.empleado.IdEmpleado.ToString());

                    HttpContext.Session.SetString("IdCatalogoTipoEmpleado", usuarioperfil.empleadoperfil.empleado.catalogotipoempleado.IdCatalogo.ToString());
                    HttpContext.Session.SetString("CatalogoTipoEmpleado", usuarioperfil.empleadoperfil.empleado.catalogotipoempleado.Detalle.ToString());

                    HttpContext.Session.SetString("IdEmpleadoPerfil", IdEmpleadoPerfil.ToString());
                    HttpContext.Session.SetString("NombreEmpleadoPerfil", usuarioperfil.empleadoperfil.NombreEmpleadoPerfil);
                    HttpContext.Session.SetString("Idsede", usuarioperfil.empleadoperfil.empresasede.IdEmpresaSede.ToString());
                    HttpContext.Session.SetString("NombreSede", usuarioperfil.empleadoperfil.empresasede.NombreSede);
                    HttpContext.Session.SetString("IdEmpresa", usuarioperfil.empleadoperfil.empresasede.empresa.IdEmpresa.ToString());
                    HttpContext.Session.SetString("IdEmpresaPadre", usuarioperfil.empleadoperfil.empresasede.empresa.IdEmpresaPadre.ToString());
                    HttpContext.Session.SetString("NombreEmpresa", usuarioperfil.empleadoperfil.empresasede.empresa.NombreEmpresa);
                    HttpContext.Session.SetString("RucEmpresa", usuarioperfil.empleadoperfil.empresasede.empresa.persona.NumeroDocumento);
                    HttpContext.Session.SetString("NombreCompletoEmpresa", usuarioperfil.empleadoperfil.empresasede.empresa.persona.NombreCompleto);
                    return RedirectToAction("ActualizarMenu", "Usuario");
                }

            }
            return View();
        }
        //[ValidarUsuario]
        [HttpGet]
        public IActionResult IndexUsuario()
        {
            var conpermiso = MatrixUtilitarios.Utilitario.TienePermisoControladorAccion(HttpContext.Request.RouteValues["controller"].ToString(), HttpContext.Request.RouteValues["action"].ToString()); if (conpermiso == -1) { return RedirectToAction("Login", "Usuario"); }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ListarUsuario(IDataTablesRequest requestModel)
        {
            int EsInstitucion = Convert.ToInt32(Request.Form["optEsInstitucionBusqueda"]);
            int Bloqueado = Convert.ToInt32(Request.Form["optBloqueadoBusqueda"]);
            Seguridad.ListaUsuario listausuario = new Seguridad.ListaUsuario();
            Seguridad.Usuario usuario = new Seguridad.Usuario();
            int numeroPagina = (requestModel.Start / requestModel.Length) + 1;
            try
            {

                listausuario = await iusuario.ListarUsuarioAsync(Bloqueado, EsInstitucion, Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")), null, null, numeroPagina, requestModel.Length, requestModel.Search.Value);


                if (listausuario.mensaje.CodigoMensaje > 0)
                {
                    return RedirectToAction("PaginaError", new { DescripcionError = listausuario.mensaje.DescripcionMensaje });
                }
                var totalCount = listausuario.paginacion.TotalRegistros;
                var data = listausuario.lista.Select(lq => new
                {
                    IdUsuario = lq.IdUsuario,
                    Persona = lq.persona.NombreCompleto,
                    CatalogoTipoUsuario = lq.catalogotipousuario.Descripcion,
                    IdCatalogoTipoUsuario = lq.catalogotipousuario.IdCatalogo,
                    Logueo = lq.Logueo,
                    Clave = lq.Clave,
                    Bloqueado = (lq.Bloqueado == true ? "Si" : "No"),
                    EsInstitucion = (lq.EsInstitucion == true ? "Si" : "No"),
                    Email = lq.Email,
                    RutaArchivoFoto = lq.RutaArchivoFoto,
                    lq.CantidadPerfil,
                    lq.CantidadToken,
                }).ToList();

                //var response = DataTablesResponse.Create(requestModel, totalCount, totalCount, data); 
                //return Json(response, true);
                // return Json(new DataTablesResponse(requestModel.Draw, data, totalCount, totalCount), JsonRequestBehavior.AllowGet);
                var jsonData = new { draw = requestModel.Draw, recordsFiltered = totalCount, recordsTotal = totalCount, data = data }; return Ok(jsonData);

            }
            catch (Exception ex)
            {
                return RedirectToAction("PaginaError", new { DescripcionError = "Hubo un error [" + ex.Message + "]" });
            }
        }
        [HttpPost]
        public async Task<IActionResult> RegistroUsuario()
        {
            //MatrixUtilitarios.Utilitario.EnviarEmail("hlucas@outlook.com", "prueba", "prueba", "");
            int IdUsuario = Convert.ToInt32(Request.Form["IdUsuario"]);
            try
            {
                Seguridad.Usuario usuario = new Seguridad.Usuario();
                if (IdUsuario > 0)
                {
                    usuario = await iusuario.ObtenerUsuarioAsync(IdUsuario);
                    usuario.Clave = usuario.Clave;
                }
                return View(usuario);
            }
            catch (Exception ex)
            {
                return RedirectToAction("PaginaError", new { DescripcionError = "Hubo un error [" + ex.Message + "]" });
            }
        }

        [HttpPost]
		public IActionResult Login2()
		{
			string Logue = Request.Form["Logueo"];
			string clave = Request.Form["Clave"];


			string ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Dirección IP no disponible";
			var UsuarioLogueado = new Seguridad.Usuario();
			//VALIDAR A TODO USUARIO Q ENTRE POR EL LOGIN
			if (!AutenticaLDAP(Logue, clave))
			{
				//SI NO EXISTE EN EL LDAP, ES POR DOS MOTIVOS, QUE NO INGRESO SU CLAVE CORECTAMENTE O QUE NO ESTA REGISTRADO EN EL LDAP
				//AHORA SE BUSCARÁ EN EL SISTEMA SI EXISTE
				UsuarioLogueado = iusuario.AutenticarUsuario(Logue, MatrixUtilitarios.Utilitario.Encriptar(clave));
			}
			else
			{
				//SI EXISTE EN EL LDAP E INGRESO CORRECTAMENTE SUS CREDENCIALES
				//BUSCAREMOS SUS DATOS DE USUARIO
				UsuarioLogueado = iusuario.ObtenerDatosUsuarioAutenticadoLdapNoAsincrono(Logue);
				if (UsuarioLogueado.IdUsuario == 0)
				{
					//NO EXISTEN EN EL SISTEMA, ENTONCES ENVIAR MENSAJE QUE NO EXISTE EN SISTEMA
					UsuarioLogueado.mensaje.CodigoMensaje = 1;
					UsuarioLogueado.mensaje.DescripcionMensaje = "Usuario no está registrado en el Sistema, comuiniquese con el Administrador";
					return Json(UsuarioLogueado.mensaje);
				}
			}

			// var UsuarioLogueado = iusuario.AutenticarUsuario(Logueo, MatrixUtilitarios.Utilitario.Encriptar(Clave));

			var rutaarchivo = "SinFotoH.jpg";
			if (UsuarioLogueado.RutaArchivoFoto == null)
			{
				if (!UsuarioLogueado.persona.Sexo)
				{
					rutaarchivo = "SinFotoM.jpg";
				}
			}
			else
			{
				rutaarchivo = UsuarioLogueado.RutaArchivoFoto;
			}
			UsuarioLogueado.RutaArchivoFoto = rutaarchivo;
			UsuarioLogueado.Clave = "";

			try
			{
				return View(UsuarioLogueado);
			}
			catch (Exception ex)
			{
				return RedirectToAction("PaginaError", new { DescripcionError = "Hubo un error [" + ex.Message + "]" });
			}
		}
		//public async Task<IActionResult> Login2()
		//{
		//    string Logueo = Request.Form["Logueo"];
		//    string Clave = Request.Form["Clave"];

		//    string ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Dirección IP no disponible";
		//    var UsuarioLogueado = await iusuario.AutenticarUsuarioAsync(Logueo, MatrixUtilitarios.Utilitario.Encriptar(Clave), ipAddress);
		//    UsuarioLogueado.Clave = "";

		//    try
		//    {
		//        return View(UsuarioLogueado);
		//    }
		//    catch (Exception ex)
		//    {
		//        return RedirectToAction("PaginaError", new { DescripcionError = "Hubo un error [" + ex.Message + "]" });
		//    }
		//}

		[HttpPost]
        public async Task<IActionResult> RegistroUsuarioCuenta()
        {
            int IdUsuario = Convert.ToInt32(HttpContext.Session.GetString("IdUsuario"));
            Seguridad.Usuario usuario = new Seguridad.Usuario();
            try
            {
                if (IdUsuario > 0)
                {
                    usuario = await iusuario.ObtenerUsuarioAsync(IdUsuario);
                }
                return View(usuario);
            }
            catch (Exception ex)
            {
                usuario.mensaje.CodigoMensaje = 1;
                usuario.mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA AL GUARDAR, COMUNIQUESE CON EL ADMINISTRADOR DEL SISTEMA";
                usuario.mensaje.DescripcionMensajeSistema = ex.Message;
                return Json(usuario);

            }
        }
        [HttpPost]
        public async Task<IActionResult> GuardarRegistroUsuario()
        {

            //bool EnviarEmail = false;

            int IdUsuario = Convert.ToInt32(Request.Form["txtRegIdUsuario"]);
            //if (IdUsuario == 0) { EnviarEmail = true; }
            int IdPersona = Convert.ToInt32(Request.Form["cmbPersona"]);
            int IdCatalogoTipoUsuario = Convert.ToInt32(Request.Form["cmbCatalogoTipoUsuario"]);
            string Logueo = Request.Form["txtLogueo"];
            string Clave = MatrixUtilitarios.Utilitario.Encriptar(Request.Form["txtClave"]);

            bool Bloqueado = Convert.ToBoolean(Convert.ToInt32(Request.Form["optBloqueado"]));
            bool EsInstitucion = Convert.ToBoolean(Convert.ToInt32(Request.Form["optEsInstitucion"]));
            string Email = Request.Form["txtEmail"];
            IFormFile file = null;
            string RutaArchivoFoto = "";
            string NombreArchivoRutaArchivoFoto = Request.Form["txtNombreRutaArchivoFoto"];
            if (HttpContext.Request.Form.Files.Count > 0)
            {
                file = HttpContext.Request.Form.Files[0];
                RutaArchivoFoto = string.Format("{0:ddMMyyyyHHmmss}{1}", DateTime.Now, Path.GetExtension(file.FileName));
            }
            else
            {
                RutaArchivoFoto = NombreArchivoRutaArchivoFoto;
            }


            Seguridad.Usuario usuario = new Seguridad.Usuario();
            try
            {
                usuario = await iusuario.GuardaUsuarioAsync(
                IdUsuario,
                IdPersona,
                IdCatalogoTipoUsuario,
                Logueo,
                Clave,
                Bloqueado,
                EsInstitucion,
                Email,
                RutaArchivoFoto,
                Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario"))
                );

                if (usuario.mensaje.CodigoMensaje == 0)
                {
                    var path = "";
                    if (HttpContext.Request.Form.Files.Count > 0)
                    {
                        if (!Directory.Exists(ConfigurationManager.AppSettings["RutaImagenes"]))
                        {
                            Directory.CreateDirectory(ConfigurationManager.AppSettings["RutaImagenes"]);
                        }
                        path = Path.Combine(ConfigurationManager.AppSettings["RutaImagenes"], RutaArchivoFoto);
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }
                    }
                }

                //if (usuario.mensaje.CodigoMensaje == 0 && EnviarEmail == true)
                //{
                //    var mensaje = await EnviarEmailUsuario(usuario.IdUsuario,Password);
                //    usuario.mensaje.DescripcionMensaje = usuario.mensaje.DescripcionMensaje + ", " + mensaje.DescripcionMensaje;
                //}
                return Json(usuario);
            }
            catch (Exception ex)
            {
                usuario.mensaje.CodigoMensaje = 1;
                usuario.mensaje.DescripcionMensaje = "Sucedió un error: " + ex.Message;
                return Json(usuario);
            }
        }
        [HttpPost]
        public async Task<IActionResult> GuardarFotoUsuario()
        {
            int IdUsuario = Convert.ToInt32(Request.Form["txtRegIdUsuario"]);
            IFormFile file = null;
            string RutaArchivoFoto = "";
            string NombreArchivoRutaArchivoFoto = Request.Form["txtNombreRutaArchivoFoto"];
            if (HttpContext.Request.Form.Files.Count > 0)
            {
                file = HttpContext.Request.Form.Files[0];
                RutaArchivoFoto = string.Format("{0:ddMMyyyyHHmmss}{1}", DateTime.Now, Path.GetExtension(file.FileName));
            }
            else
            {
                RutaArchivoFoto = NombreArchivoRutaArchivoFoto;
            }
            Seguridad.Usuario usuario = new Seguridad.Usuario();
            try
            {

                usuario = await iusuario.GuardarFotoUsuarioAsync(
                                    IdUsuario,
                                    RutaArchivoFoto,
                                    Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario"))
            );

                if (usuario.mensaje.CodigoMensaje == 0)
                {
                    var path = "";
                    if (HttpContext.Request.Form.Files.Count > 0)
                    {
                        if (!Directory.Exists(ConfigurationManager.AppSettings["RutaImagenes"]))
                        {
                            Directory.CreateDirectory(ConfigurationManager.AppSettings["RutaImagenes"]);
                        }
                        path = Path.Combine(ConfigurationManager.AppSettings["RutaImagenes"], RutaArchivoFoto);
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }
                    }
                }
                HttpContext.Session.SetString("RutaFotoUsuario", RutaArchivoFoto);
                usuario.RutaArchivoFoto = RutaArchivoFoto;
                return Json(usuario);
            }
            catch (Exception ex)
            {
                usuario.mensaje.CodigoMensaje = 0;
                usuario.mensaje.DescripcionMensaje = "Sucedión un error al intentar guardar la foto, verificar console";
                usuario.mensaje.DescripcionMensajeSistema = ex.Message;
                return Json(usuario);
            }
        }
        [HttpPost]
        public async Task<IActionResult> EliminarUsuario()
        {
            int IdUsuario = Convert.ToInt32(Request.Form["IdUsuario"]);

            Seguridad.Usuario usuario = new Seguridad.Usuario();
            Utilitarios.Mensaje mensaje = new Utilitarios.Mensaje();
            try
            {

                mensaje = await iusuario.EliminarUsuarioAsync(IdUsuario, Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));
                return Json(mensaje);
            }
            catch (Exception ex)
            {
                return RedirectToAction("PaginaError", new { DescripcionError = "Hubo un error [" + ex.Message + "]" });
            }
        }
        public IActionResult CerrarSesion()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
        [HttpPost]
        public async Task<IActionResult> ListarUsuarioPorAutoComplete()
        {

            string NombreUsuario = Request.Form["search"];
            var listaempleadoperfil = new Seguridad.ListaUsuario();

            try
            {
                listaempleadoperfil = await iusuario.ListarUsuarioPorAutoComplete(Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")), NombreUsuario);
                if (listaempleadoperfil.mensaje.CodigoMensaje > 0)
                {
                    return RedirectToAction("PaginaError", new { DescripcionError = listaempleadoperfil.mensaje.DescripcionMensaje });
                }
                var data = listaempleadoperfil.lista.Select(lq => new
                {

                    text = lq.persona.NombreCompleto,
                    id = lq.IdUsuario
                }).ToList();

                return Json(new { items = data });
            }
            catch (Exception ex)
            {
                return RedirectToAction("PaginaError", new { DescripcionError = "Hubo un error [" + ex.Message + "]" });
            }
        }
    }

}
public class CaptchConfig
{
    public int[] captcha_numbers { get; set; }
    public string captcha_value { get; set; }
    public string current_captcha_codificado { get; set; }

}

public class Parametro
{
    public string Parametro1 { get; set; }
    public string Parametro2 { get; set; }
    public string Parametro3 { get; set; }
    public string Parametro4 { get; set; }
}

public class Permiso
{
    public string Controlador { get; set; }
    public string Vista { get; set; }
    public bool Ver { get; set; }
    public bool Nuevo { get; set; }
    public bool Editar { get; set; }
    public bool Guardar { get; set; }
    public bool Eliminar { get; set; }
    public bool Imprimir { get; set; }
    public bool Exportar { get; set; }
    public bool VistaPrevia { get; set; }
    public bool Consultar { get; set; }
}
public static class ListaPermiso
{
    public static List<Permiso> listapermiso { get; set; } = new List<Permiso>();
}








