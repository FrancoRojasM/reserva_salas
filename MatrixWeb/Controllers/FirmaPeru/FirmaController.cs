
using DataTables.AspNet.Core;
using Microsoft.AspNetCore.Http;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using MatrixService;
using System.Threading.Tasks;
using General;
using DataTables.AspNet.AspNetCore;
using System.Collections.Specialized;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text;
using System.Drawing;
using System.IO;
using System.Configuration;

namespace MatrixWeb.Controllers
{
    public class FirmaController : Controller
    {
        private static string _token; // Almacena el token en una variable estática

        public FirmaController()
        {

        }
        [HttpGet]
        public IActionResult Firmador()
        {


            Random r = new Random();
            string param_token = "";
            for (int i = 0; i < 10; i++)
            {

                //Genera un numero entre 10 y 100 (101 no se incluye)
                param_token = param_token + r.Next(10, 101).ToString();

            }


            var jsonData = new
            {
                param_url = @"https://localhost:44350/Firma/postArguments",
                param_token = param_token,
                document_extension = "pdf"                
            };
            // Convertir a JSON
            var jsonString = JsonConvert.SerializeObject(jsonData);
            // Convertir a base64
            var base64String = Convert.ToBase64String(Encoding.UTF8.GetBytes(jsonString));
            ViewBag.paramBase64 = base64String;
            return View();
        }
        [HttpPost]
        public IActionResult upload()
        {
            string path = "";
            IFormFile Archivo = HttpContext.Request.Form.Files[0];
            string NombreArchivo = "Doc_Fir_Refirma" + "_" + string.Format("{0:ddMMyyyyHHmmss}{1}", DateTime.Now, ".pdf");

            if (HttpContext.Request.Form.Files.Count > 0)
            {
                if (!Directory.Exists(ConfigurationManager.AppSettings["RutaArchivos"]))
                {
                    Directory.CreateDirectory(ConfigurationManager.AppSettings["RutaArchivos"]);
                }
                path = Path.Combine(ConfigurationManager.AppSettings["RutaArchivos"], NombreArchivo);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    Archivo.CopyTo(stream);
                }
            }

            string x = Request.Form["signed_file"];
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> postArguments()
        {
            // Verifica si el token aún no ha sido obtenido o ha expirado
            if (string.IsNullOrEmpty(_token) || IsTokenExpired(_token))
            {
                // Obtiene un nuevo token
                _token = await GetAccessToken();
                //  ViewBag.Token = _token; // Puedes pasar el token a la vista si es necesario
            }
            string param_token = Request.Form["param_token"];
            var jsonData = new
            {
                signatureFormat = "PAdES",
                signatureLevel = "B",
                signaturePackaging = "enveloped",
                documentToSign = @"https://localhost:44350/Content/Contenedor/Archivos/16112023184111.pdf",
                certificateFilter = ".*",
                webTsa = "",
                userTsa = "",
                passwordTsa = "",
                theme = "claro",
                visiblePosition = true,
                contactInfo = "",
                signatureReason = "Soy el autor de este documento",
                bachtOperation = false,
                oneByOne = true,
                signatureStyle = 1,
                imageToStamp = @"https://localhost:44350/Content/Contenedor/Imagenes/iFirma1.png",
                stampTextSize = 10,
                stampWordWrap = 37,
                role = "Analista de servicios",
                stampPage = 1,
                positionx = 50,
                positiony = 20,
                uploadDocumentSigned = @"https://localhost:44350/Firma/upload?id=001",
                certificationSignature = false,
                token = _token
            };
            // Convertir a JSON
            var jsonString = JsonConvert.SerializeObject(jsonData);
            // Convertir a base64
            var base64String = Convert.ToBase64String(Encoding.UTF8.GetBytes(jsonString));
            return Ok(base64String);
        }

        private async Task<string> GetAccessToken()
        {
            string tokenUrl = "https://apps.firmaperu.gob.pe/admin/api/security/generate-token";
            string clientId = "91630riw6TIwNjAxNzY1MjI2MzcWZiqQEA";
            string clientSecret = "eMcB5IgRh93V_I_zBB4icDrmK4i34MmigpU";

            using (HttpClient client = new HttpClient())
            {
                var parameters = new List<KeyValuePair<string, string>>
                {
                new KeyValuePair<string, string>("client_id", clientId),
                new KeyValuePair<string, string>("client_secret", clientSecret)
                };

                var response = await client.PostAsync(tokenUrl, new FormUrlEncodedContent(parameters));

                if (response.IsSuccessStatusCode)
                {
                    string token = await response.Content.ReadAsStringAsync();
                    return token;
                }
                else
                {
                    throw new HttpRequestException($"Error en la solicitud: {response.StatusCode}");
                }
            }
        }

        private bool IsTokenExpired(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

            if (jsonToken == null)
                throw new InvalidOperationException("Invalid token");

            var expDate = jsonToken.ValidTo.ToLocalTime();

            return expDate <= DateTime.Now;
        }
    }
}








