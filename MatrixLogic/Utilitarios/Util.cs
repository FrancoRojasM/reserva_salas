using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Utilitarios
{
    
    public class Util

    {
        public static string ClaveEncripcion = "RasputinColoradoBlanco";
        //public List<T> DataTableAListaObjetos<T>(DataTable dt)
        //{
        //    List<string> columns = new List<string>();
        //    foreach (DataColumn dc in dt.Columns)
        //    {
        //        columns.Add(dc.ColumnName);
        //    }

        //    var fields = typeof(T).GetFields();
        //    var properties = typeof(T).GetProperties();

        //    List<T> lst = new List<T>();

        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        var ob = Activator.CreateInstance<T>();

        //        foreach (var fieldInfo in fields)
        //        {
        //            if (columns.Contains(fieldInfo.Name))
        //            {
        //                fieldInfo.SetValue(ob, dr[fieldInfo.Name]);
        //            }
        //        }

        //        foreach (var propertyInfo in properties)
        //        {
        //            if (columns.Contains(propertyInfo.Name))
        //            {
        //                propertyInfo.SetValue(ob, dr[propertyInfo.Name]);
        //            }
        //        }

        //        lst.Add(ob);
        //    }

        //    return lst;
        //}
        //public T DataTableAObjeto<T>(DataTable dt)
        //{
        //    DataRow dr = dt.Rows[0];

        //    // Get all columns' name
        //    List<string> columns = new List<string>();
        //    foreach (DataColumn dc in dt.Columns)
        //    {
        //        columns.Add(dc.ColumnName);
        //    }

        //    // Create object
        //    var ob = Activator.CreateInstance<T>();

        //    // Get all fields
        //    var fields = typeof(T).GetFields();
        //    foreach (var fieldInfo in fields)
        //    {
        //        if (columns.Contains(fieldInfo.Name))
        //        {
        //            // Fill the data into the field
        //            fieldInfo.SetValue(ob, dr[fieldInfo.Name]);
        //        }
        //    }

        //    // Get all properties
        //    var properties = typeof(T).GetProperties();
        //    foreach (var propertyInfo in properties)
        //    {
        //        if (columns.Contains(propertyInfo.Name))
        //        {
        //            // Fill the data into the property
        //            //propertyInfo.SetValue(ob, dr[propertyInfo.Name]);
        //            if (dr[propertyInfo.Name] != DBNull.Value)
        //            {
        //                propertyInfo.SetValue(ob, dr[propertyInfo.Name]);
        //            }
        //            else
        //            {
        //                string tipo = propertyInfo.PropertyType.Name;
        //                propertyInfo.SetValue(ob, ObtenerValorDefaultPorTipo(tipo));
        //            }

        //        }
        //    }

        //    return ob;
        //}
        private object ObtenerValorDefaultPorTipo(string tipo)
        {
            object valor = null;
            switch (tipo)
            {
                case "Int32": valor =null; break;
                case "DateTime": valor = null; break;      
            }
            return valor;

        }

        public string Colorado(string cadena)
        {
            string clave = "RasputinColoradoBlanco";
            byte[] llave; //Arreglo donde guardaremos la llave para el cifrado 3DES.

            byte[] arreglo = UTF8Encoding.UTF8.GetBytes(cadena); //Arreglo donde guardaremos la cadena descifrada.

            // Ciframos utilizando el Algoritmo MD5.
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            llave = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(clave));
            md5.Clear();

            //Ciframos utilizando el Algoritmo 3DES.
            TripleDESCryptoServiceProvider tripledes = new TripleDESCryptoServiceProvider();
            tripledes.Key = llave;
            tripledes.Mode = CipherMode.ECB;
            tripledes.Padding = PaddingMode.PKCS7;
            ICryptoTransform convertir = tripledes.CreateEncryptor(); // Iniciamos la conversión de la cadena
            byte[] resultado = convertir.TransformFinalBlock(arreglo, 0, arreglo.Length); //Arreglo de bytes donde guardaremos la cadena cifrada.
            tripledes.Clear();

            return Convert.ToBase64String(resultado, 0, resultado.Length); // Convertimos la cadena y la regresamos.
        }
        public string DesColorado(string cadena)
        {

            string clave = "RasputinColoradoBlanco";
            byte[] llave;

            byte[] arreglo = Convert.FromBase64String(cadena); // Arreglo donde guardaremos la cadena descovertida.

            // Ciframos utilizando el Algoritmo MD5.
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            llave = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(clave));
            md5.Clear();

            //Ciframos utilizando el Algoritmo 3DES.
            TripleDESCryptoServiceProvider tripledes = new TripleDESCryptoServiceProvider();
            tripledes.Key = llave;
            tripledes.Mode = CipherMode.ECB;
            tripledes.Padding = PaddingMode.PKCS7;
            ICryptoTransform convertir = tripledes.CreateDecryptor();
            byte[] resultado = convertir.TransformFinalBlock(arreglo, 0, arreglo.Length);
            tripledes.Clear();

            string cadena_descifrada = UTF8Encoding.UTF8.GetString(resultado); // Obtenemos la cadena
            return cadena_descifrada; // Devolvemos la cadena
        }
        public string EnviarCorreoValido(string destinatarios, string copiados, string copiadosocultos, string mensaje, string asunto, string pathArchivo)
        {
            try
            {
                string userName = "siispronis@pronis.gob.pe";  // ConfigurationManager.AppSettings["CuentaEnvioEmail"];
                string password = "eeNjtHtvdMuE7Pek";  // ConfigurationManager.AppSettings["PasswordEnvioEmail"];
                //ServicePointManager.Expect100Continue = true;
                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;

                if (destinatarios != "")
                {
                    MailMessage msg = new MailMessage();
                    foreach (var email in destinatarios.Split(new[] { ';' }))
                    {
                        if (!ComprobarFormatoEmail(email)) { return "El formato del correo " + email + ", no es correcto"; }
                        msg.To.Add(new MailAddress(email));
                    }

                    if (copiados != "")
                    {
                        foreach (var email in copiados.Split(new[] { ';' }))
                        {
                            if (!ComprobarFormatoEmail(email)) { return "El formato del correo " + email + ", no es correcto"; }
                            msg.CC.Add(new MailAddress(email));
                        }
                    }

                    if (copiadosocultos != "")
                    {
                        foreach (var email in copiadosocultos.Split(new[] { ';' }))
                        {
                            if (!ComprobarFormatoEmail(email)) { return "El formato del correo " + email + ", no es correcto"; }
                            msg.Bcc.Add(new MailAddress(email));
                        }
                    }



                    msg.From = new MailAddress(userName);
                    msg.Subject = asunto;
                    msg.Body = mensaje;
                    msg.IsBodyHtml = true;
                    if (pathArchivo != "")
                    {
                        msg.Attachments.Add(new Attachment(pathArchivo));
                    }

                    SmtpClient client = new SmtpClient();
                    client.Host = "smtp.office365.com";//servidor de correo local

                    client.EnableSsl = true;
                    client.Credentials = new System.Net.NetworkCredential(userName, password);

                    client.Port = 587;
                    client.Send(msg);

                    return "OK";
                }
                else
                {
                    return "CORREO VACIO";
                }



            }
            catch (Exception ex)
            {
                return ex.Message;
            }


        }
        public static bool ComprobarFormatoEmail(string seMailAComprobar)
        {
            String sFormato;
            sFormato = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(seMailAComprobar, sFormato))
            {
                if (Regex.Replace(seMailAComprobar, sFormato, String.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public static string Encriptar(string cadena)
        {
            try
            {
                byte[] llave;
                byte[] arreglo = UTF8Encoding.UTF8.GetBytes(cadena);
                MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                llave = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(ClaveEncripcion));
                md5.Clear();
                TripleDESCryptoServiceProvider tripledes = new TripleDESCryptoServiceProvider();
                tripledes.Key = llave;
                tripledes.Mode = CipherMode.ECB;
                tripledes.Padding = PaddingMode.PKCS7;
                ICryptoTransform convertir = tripledes.CreateEncryptor();
                byte[] resultado = convertir.TransformFinalBlock(arreglo, 0, arreglo.Length);
                tripledes.Clear();
                return Convert.ToBase64String(resultado, 0, resultado.Length);
            }
            catch
            {
                return "";
            }
        }
    }


}
