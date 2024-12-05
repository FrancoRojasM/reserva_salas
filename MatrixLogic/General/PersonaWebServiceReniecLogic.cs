using General;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Web;

namespace MatrixLogic.General
{
    public class PersonaWebServiceReniecLogic
    {
        private Persona persona;
        private ListaPersona lista;

        public PersonaWebServiceReniecLogic(){
            persona = new Persona();
            lista = new ListaPersona();
        }

        //public async Task<Persona> ObtenerDatosPersonaReniec( string NumeroDocumento) {

            //try
            //{
            //    ServicioReniec.ReniecClient reniec = new ServicioReniec.ReniecClient();
            //    ServicioReniec.EPersonaR resultado = await reniec.getPersonaAsync(NumeroDocumento);

            //    if (resultado.PRENOM_INSCRITO!=null)
            //    {
            //        persona.Nombres = resultado.PRENOM_INSCRITO;
            //        persona.ApellidoPaterno = resultado.AP_PRIMER;
            //        persona.ApellidoMaterno = resultado.AP_SEGUNDO;
            //        persona.FechaNacimiento = resultado.FECHA_NACIMIENTO;

            //        persona.Departamento = resultado.DEPARTAMENTO;
            //        persona.Provincia = resultado.PROVINCIA;
            //        persona.Distrito = resultado.DISTRITO;
            //        persona.IdGenero = Convert.ToInt32(resultado.DE_GENERO);
                    
            //        if (resultado.FECHA_NACIMIENTO != null)
            //        {                    
            //        string dia = resultado.FECHA_NACIMIENTO.Substring(6, 2);
            //        string mes = resultado.FECHA_NACIMIENTO.Substring(4, 2);
            //        string anio = resultado.FECHA_NACIMIENTO.Substring(0, 4);
            //        string NuevaFechaNacimiento = dia + "/" + mes + "/" + anio;
            //        int Edad = DateTime.Now.Year - DateTime.Parse(NuevaFechaNacimiento).Year;
            //         persona.Edad = Edad;

            //        }

                   
            //    }

              //return persona;
            //}
            //catch (Exception ex)
            //{
            //    persona.mensaje.CodigoMensaje = 1;
            //    persona.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ObtenerDatosPersonaReniec], VERIFICAR CONSOLA";
            //    persona.mensaje.DescripcionMensajeSistema = ex.Message;
            //    return persona;

            //}
                            
        //}


        public Persona ObtenerDatosPersonaReniecV2(string NumeroDocumento)
        {
            try
            {
                string validarRespuesta = "";     
                string myUrl = "http://sairc.fissal.gob.pe/Consulta/ObtenerInformacionPersona_Vista?NroDoc=" + NumeroDocumento + "&key=RTEYUKDMLMA";
                HttpWebRequest myWebRequest = (HttpWebRequest)WebRequest.Create(myUrl);
                myWebRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:23.0) Gecko/20100101 Firefox/23.0";
                myWebRequest.Credentials = CredentialCache.DefaultCredentials;
                myWebRequest.Proxy = null;
                HttpWebResponse myHttpWebResponse =  (HttpWebResponse)myWebRequest.GetResponse();
                Stream myStream =  myHttpWebResponse.GetResponseStream();
                Encoding encode = System.Text.Encoding.GetEncoding("ISO-8859-9");
                StreamReader myStreamReader = new StreamReader(myStream, encode);
                string _WebSource = myStreamReader.ReadToEnd();
               
                XElement table;
                table = XElement.Parse(_WebSource);
               string[] values = table.Descendants("td").Select(td => td.Value).ToArray();

                validarRespuesta= values[1].Replace("\n", string.Empty).Trim().ToString();
                if (validarRespuesta == "0")
                {
                    persona.Nombres = values[15].Replace("\n", string.Empty).Trim().ToString();
                    persona.ApellidoPaterno = values[9].Replace("\n", string.Empty).Trim().ToString();
                    persona.ApellidoMaterno = values[11].Replace("\n", string.Empty).Trim().ToString();
                    persona.FechaNacimiento = values[39].Replace("\n", string.Empty).Trim().ToString();

                    persona.Departamento = values[31].Replace("\n", string.Empty).Trim().ToString();
                    persona.Provincia = values[33].Replace("\n", string.Empty).Trim().ToString();
                    persona.Distrito = values[35].Replace("\n", string.Empty).Trim().ToString();
                    persona.IdGenero = Convert.ToInt32(values[37].Replace("\n", string.Empty).Trim().ToString());

                    if (persona.FechaNacimiento != null)
                    {
                        string dia = persona.FechaNacimiento.Substring(6, 2);
                        string mes = persona.FechaNacimiento.Substring(4, 2);
                        string anio = persona.FechaNacimiento.Substring(0, 4);
                        string NuevaFechaNacimiento = dia + "/" + mes + "/" + anio;
                        int Edad = DateTime.Now.Year - DateTime.Parse(NuevaFechaNacimiento).Year;
                        persona.Edad = Edad;
                    }
                }
                else if (validarRespuesta == "99")
                {
                    persona.mensaje.CodigoMensaje = 99;
                    persona.mensaje.DescripcionMensaje = values[3].Replace("\n", string.Empty).Trim().ToString();
                }
                else {
                    persona.mensaje.CodigoMensaje = 1;
                    persona.mensaje.DescripcionMensaje = "No hay conexión";
                }

                return persona;
            }
            catch (Exception ex)
            {
                persona.mensaje.CodigoMensaje = 1;
                persona.mensaje.DescripcionMensaje = "SUCEDIÓ UN ERROR EN LA CAPA LOGICA [ObtenerDatosPersonaReniec], VERIFICAR CONSOLA";
                persona.mensaje.DescripcionMensajeSistema = ex.Message;
                return persona;

            }

        }

    }
}
