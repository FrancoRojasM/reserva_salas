using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using OfficeOpenXml;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Seguridad;
using MatrixWeb;
using OfficeOpenXml.Style;
using System.Drawing;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace MatrixUtilitarios
{
	public static class Utilitario
	{
		private static IHttpContextAccessor _httpContextAccessor;
		public static void Configure(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}
		internal static byte[] ComputeSha256(this byte[] value)
		{
			using (SHA256 sha256Hash = SHA256.Create())
				return sha256Hash.ComputeHash(value);
		}
		internal static byte[] ComputeSha256(this string value) => ComputeSha256(Encoding.UTF8.GetBytes(value));
		internal static byte[] ComputeMD5(this byte[] value)
		{
			using (MD5 md5 = MD5.Create())
				return md5.ComputeHash(value);
		}
		internal static byte[] ComputeMD5(this string value) => ComputeMD5(Encoding.UTF8.GetBytes(value));
		internal static string CryptoJS_MD5(string Password)
		{
			var passwordMd5 = ComputeMD5(Password);
			return BitConverter.ToString(passwordMd5).Replace("-", "").ToLower();
		}
		internal static byte[] CombineByteArray(byte[] first, byte[] second)
		{
			// Hex encode (with lowercase letters) and Utf8 encode
			string hex = ByteArrayToString(first).ToLower();
			first = Encoding.UTF8.GetBytes(hex);

			byte[] bytes = new byte[first.Length + second.Length];
			Buffer.BlockCopy(first, 0, bytes, 0, first.Length);
			Buffer.BlockCopy(second, 0, bytes, first.Length, second.Length);
			return bytes;
		}
		public static string ByteArrayToString(byte[] ba)
		{
			StringBuilder hex = new StringBuilder(ba.Length * 2);
			foreach (byte b in ba)
				hex.AppendFormat("{0:x2}", b);
			return hex.ToString();
		}

		public static int TienePermisoControladorAccion(string Controlador, string Accion)
		{
			var IdUsuario = Convert.ToInt32(_httpContextAccessor.HttpContext.Session.GetString("IdUsuario"));
			if (IdUsuario == 0)
			{
				return -1;
			}
            List <PerfilOpcion> lista = new List<PerfilOpcion>();
			// Obtener el valor de una variable de sesión
			lista = _httpContextAccessor.HttpContext.Session.GetObject<List<PerfilOpcion>>("ListaPermiso");

			// Establecer un valor en la sesión
			// _httpContextAccessor.HttpContext.Session.SetString("MiVariableDeSesion", "NuevoValor");


			// lista = HttpContext.Session.GetObject<List<Permiso>>("ListaPermiso");//ListaPermiso.listapermiso;
			int conpermiso = 0;
			if (lista != null)
			{
				foreach (PerfilOpcion item in lista)
				{
					if (item.opcion.Controlador == Controlador && item.opcion.Accion == Accion)
					{
						conpermiso = 1;
					}
				}
			}
			return conpermiso;
		}
		public static string shuffle(string input)
		{
			var q = from c in input.ToCharArray()
					orderby Guid.NewGuid()
					select c;
			string s = string.Empty;
			foreach (var r in q)
				s += r;
			return s;
		}
		public static string Base64Encode(string plainText)
		{
			var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
			return System.Convert.ToBase64String(plainTextBytes);
		}
		public static string Base64Decode(string base64EncodedData)
		{
			var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
			return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
		}
        //public static string GenerateQrCode(string url, string alt = "QR code", int height = 500, int width = 500, int margin = 0)
        //{
        //    var qrWriter = new BarcodeWriter();
        //    qrWriter.Format = BarcodeFormat.QR_CODE;
        //    qrWriter.Options = new EncodingOptions() { Height = height, Width = width, Margin = margin };

        //    using (var q = qrWriter.Write(url))
        //    {
        //        using (var ms = new MemoryStream())
        //        {
        //            q.Save(ms, ImageFormat.Png);
        //            var img = new TagBuilder("img");
        //            img.Attributes.Add("src", String.Format("data:image/png;base64,{0}", Convert.ToBase64String(ms.ToArray())));
        //            img.Attributes.Add("alt", alt);
        //            return img.ToString();
        //        }
        //    }
        //}
        //public static string GenerateQrCode64Bits(string url, int height = 500, int width = 500, int margin = 0)
        //{
        //    var qrWriter = new BarcodeWriter();
        //    qrWriter.Format = BarcodeFormat.QR_CODE;
        //    qrWriter.Options = new EncodingOptions() { Height = height, Width = width, Margin = margin };

        //    using (var q = qrWriter.Write(url))
        //    {
        //        using (var ms = new MemoryStream())
        //        {
        //            q.Save(ms, ImageFormat.Png);
        //            var img = new TagBuilder("img");
        //            return String.Format("data:image/png;base64,{0}", Convert.ToBase64String(ms.ToArray()));
        //        }
        //    }
        //}
        public static ExcelPackage ExportarReporteV2(DataSet ListaTablas, List<string> ListaTitulos = null, List<bool> ListaTablaConTotal = null, List<bool> ListaTablaConNumeracion = null)
        {

            ExcelPackage pck = new ExcelPackage();

            for (int k = 0; k < ListaTablas.Tables.Count; k++)
            {
                ExcelWorksheet ws = null;
                if (ListaTitulos != null)
                {
                    ws = pck.Workbook.Worksheets.Add(ListaTitulos[k].ToString());
                }
                else
                {
                    ws = pck.Workbook.Worksheets.Add(ListaTablas.Tables[k].TableName.ToString());
                }
                DataTable dt = ListaTablas.Tables[k].Copy();


             //   var image = Image.FromFile(ConfigurationManager.AppSettings["RutaImagenesLogoEntidad"]);

                // Insertar la imagen en una celda específica
                var picture = ws.Drawings.AddPicture("nombre_uniqo", ConfigurationManager.AppSettings["RutaImagenesLogoEntidadReporteExcel"]);
                picture.SetPosition(0, 0, 0, 0);

                // Ajustar el tamaño de la imagen si es necesario
                picture.SetSize(350, 55);


                ws.Cells["A1"].Value = "";
                ws.Cells["A1"].Style.Font.Bold = true;
                ws.Cells["A4"].Value = "REPORTE";
                ws.Cells["A4"].Style.Font.Bold = true;
                if (ListaTitulos != null)
                {
                    ws.Cells["B4"].Value = ListaTitulos[k].ToString();
                }
                else
                {
                    ws.Cells["B4"].Value = ListaTablas.Tables[k].ExtendedProperties["Titulo"].ToString();
                }


                ws.Cells["A5"].Value = "FECHA REPORTE";
                ws.Cells["A5"].Style.Font.Bold = true;
                ws.Cells["B5"].Value = string.Format("{0:dd MMMM yyyy} , {0:H: mm tt}", DateTimeOffset.Now);
                ws.Cells["A1:B5"].Style.Font.Size = 8;

                ws.Cells["A4:A5"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                ws.Cells["A4:A5"].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#630460")));
                ws.Cells["A4:B5"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                ws.Cells["A4:B5"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                ws.Cells["A4:B5"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                ws.Cells["A4:B5"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                ws.Cells["A4:B5"].Style.Font.Bold = true;
                ws.Cells["A4:B5"].Style.Font.Name = "Arial";
                ws.Cells["A4:B5"].Style.Font.Size = 8;
                ws.Cells["A4:A5"].Style.Font.Color.SetColor(ColorTranslator.FromHtml(string.Format("#ffffff")));

                // 26
                string CeldaC = "";
                char base64String = (char)65;
                char base64StringCuadro = (char)65;

                int f = 1;
                int g = 1;
                int h = 1;
                int numeracion = 0;
                if (ListaTablaConNumeracion != null)
                {
                    if (ListaTablaConNumeracion[k])
                    {
                        ws.Cells["A7"].Value = "N°";
                        numeracion = 1;
                    }
                }

                for (int i = 0; i < dt.Columns.Count; i++)
                {

                    if (i < (26 - numeracion))
                    {
                        char dato = (char)(64 + i + 1 + numeracion);
                        CeldaC = dato.ToString();
                        ws.Cells[dato + "7"].Value = dt.Columns[i].ColumnName.ToString();
                    }
                    else
                    {
                        if (i < (52 - numeracion))
                        {
                            char dato2 = (char)(64 + f);
                            CeldaC = "A" + dato2.ToString();
                            ws.Cells[CeldaC + "7"].Value = dt.Columns[i].ColumnName.ToString();
                            f = f + 1;
                        }
                        else
                        {
                            if (i < (78 - numeracion))
                            {
                                char dato2 = (char)(64 + g);
                                CeldaC = "B" + dato2.ToString();
                                ws.Cells[CeldaC + "7"].Value = dt.Columns[i].ColumnName.ToString();
                                g = g + 1;
                            }
                            else
                            {
                                char dato2 = (char)(64 + h);
                                CeldaC = "C" + dato2.ToString();
                                ws.Cells[CeldaC + "7"].Value = dt.Columns[i].ColumnName.ToString();
                                h = h + 1;
                            }
                        }

                    }

                }
                ws.Cells["A7:" + CeldaC + "7"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                ws.Cells["A7:" + CeldaC + "7"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                ws.Cells["A7:" + CeldaC + "7"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                ws.Cells["A7:" + CeldaC + "7"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                ws.Cells["A7:" + CeldaC + "7"].Style.Font.Size = 8;

                ws.Cells["A7:" + CeldaC + "7"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                ws.Cells["A7:" + CeldaC + "7"].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("#630460")));
                ws.Cells["A7:" + CeldaC + "7"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                ws.Cells["A7:" + CeldaC + "7"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                ws.Cells["A7:" + CeldaC + "7"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                ws.Cells["A7:" + CeldaC + "7"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                ws.Cells["A7:" + CeldaC + "7"].Style.Font.Bold = true;
                ws.Cells["A7:" + CeldaC + "7"].Style.Font.Name = "Arial";
                ws.Cells["A7:" + CeldaC + "7"].Style.Font.Size = 8;
                ws.Cells["A7:" + CeldaC + "7"].Style.Font.Color.SetColor(ColorTranslator.FromHtml(string.Format("#ffffff")));

                int row = 8;
                int rowCuadro = 7;
                int columana = 0;
                int rowR = 8;

                //CREANDO LOS REGISTROS
                int fila = 0;
                int filaA = 1;
                int filaB = 1;
                int filaC = 1;
                int PasoAA = 0;
                int PasoBB = 0;
                int PasoCC = 0;
                string Celda = "";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    filaA = 1;
                    filaB = 1;
                    filaC = 1;
                    if (ListaTablaConTotal != null)
                    {
                        if (ListaTablaConTotal[k])
                        {
                            if (dt.Rows.Count - 1 == i)
                            {
                                if (numeracion == 1)
                                {
                                    ws.Cells["A" + "" + (row + i)].Value = "";
                                }
                            }
                            else
                            {
                                if (numeracion == 1)
                                {
                                    ws.Cells["A" + "" + (row + i)].Value = i + 1;
                                }
                            }
                        }
                        else
                        {
                            if (numeracion == 1)
                            {
                                ws.Cells["A" + "" + (row + i)].Value = i + 1;
                            }
                        }
                    }
                    else
                    {
                        if (numeracion == 1)
                        {
                            ws.Cells["A" + "" + (row + i)].Value = i + 1;
                        }
                    }


                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (j < 26 - numeracion)
                        {
                            base64String = (char)(64 + j + 1 + numeracion);
                            Celda = base64String.ToString();
                            if (dt.Columns[j].DataType == typeof(DateTime))
                            {
                                ws.Cells[Celda + "" + (row + i)].Style.Numberformat.Format = "dd/mm/yyyy";
                                ws.Cells[Celda + "" + (row + i)].Value = dt.Rows[i].ItemArray[j];
                            }
                            else
                            {
                                if (ListaTablaConTotal != null)
                                {
                                    if (ListaTablaConTotal[k])
                                    {
                                        if (dt.Rows.Count - 1 == i)
                                        {
                                            ws.Cells[Celda + "" + (row + i)].Style.Font.Bold = true;
                                        }
                                    }
                                }
                                ws.Cells[Celda + "" + (row + i)].Value = dt.Rows[i].ItemArray[j];
                            }
                            fila = fila + 1;
                        }
                        else
                        {
                            if (j < 52 - numeracion)
                            {
                                PasoAA = 1;
                                base64String = (char)(64 + filaA);
                                Celda = "A" + base64String.ToString();
                                if (dt.Columns[j].DataType == typeof(DateTime))
                                {
                                    ws.Cells[Celda + "" + (row + i)].Style.Numberformat.Format = "dd/mm/yyyy";
                                    ws.Cells[Celda + "" + (row + i)].Value = dt.Rows[i].ItemArray[j];
                                }
                                else
                                {
                                    if (ListaTablaConTotal != null)
                                    {
                                        if (ListaTablaConTotal[k])
                                        {
                                            if (dt.Rows.Count - 1 == i)
                                            {
                                                ws.Cells[Celda + "" + (row + i)].Style.Font.Bold = true;
                                            }
                                        }
                                    }
                                    ws.Cells[Celda + "" + (row + i)].Value = dt.Rows[i].ItemArray[j];
                                }
                                filaA = filaA + 1;
                            }
                            else
                            {
                                if (j < 78 - numeracion)
                                {
                                    PasoBB = 1;
                                    base64String = (char)(64 + filaB);
                                    Celda = "B" + base64String.ToString();
                                    if (dt.Columns[j].DataType == typeof(DateTime))
                                    {
                                        ws.Cells[Celda + "" + (row + i)].Style.Numberformat.Format = "dd/mm/yyyy";
                                        ws.Cells[Celda + "" + (row + i)].Value = dt.Rows[i].ItemArray[j];
                                    }
                                    else
                                    {
                                        if (ListaTablaConTotal != null)
                                        {
                                            if (ListaTablaConTotal[k])
                                            {
                                                if (dt.Rows.Count - 1 == i)
                                                {
                                                    ws.Cells[Celda + "" + (row + i)].Style.Font.Bold = true;
                                                }
                                            }
                                        }
                                        ws.Cells[Celda + "" + (row + i)].Value = dt.Rows[i].ItemArray[j];
                                    }
                                    filaB = filaB + 1;
                                }
                                else
                                {
                                    PasoCC = 1;
                                    base64String = (char)(64 + filaC);
                                    Celda = "C" + base64String.ToString();
                                    if (dt.Columns[j].DataType == typeof(DateTime))
                                    {
                                        ws.Cells[Celda + "" + (row + i)].Style.Numberformat.Format = "dd/mm/yyyy";
                                        ws.Cells[Celda + "" + (row + i)].Value = dt.Rows[i].ItemArray[j];
                                    }
                                    else
                                    {
                                        if (ListaTablaConTotal != null)
                                        {
                                            if (ListaTablaConTotal[k])
                                            {
                                                if (dt.Rows.Count - 1 == i)
                                                {
                                                    ws.Cells[Celda + "" + (row + i)].Style.Font.Bold = true;
                                                }
                                            }
                                        }
                                        ws.Cells[Celda + "" + (row + i)].Value = dt.Rows[i].ItemArray[j];
                                    }
                                    filaC = filaC + 1;
                                }
                            }
                        }
                        columana = j + 1;
                        rowR = row + i;
                    }


                    ws.Cells["A" + rowR + ":" + Celda + "" + rowR].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    ws.Cells["A" + rowR + ":" + Celda + "" + rowR].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    ws.Cells["A" + rowR + ":" + Celda + "" + rowR].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    ws.Cells["A" + rowR + ":" + Celda + "" + rowR].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    ws.Cells["A" + rowR + ":" + Celda + "" + rowR].Style.Font.Size = 8;
                }



                ws.Cells["A1:AZ1000"].AutoFitColumns();
            }

            return pck;
        }
        public static ExcelPackage ExportarReporte(DataSet ListaTablas)
		{
			try
			{
				ExcelPackage pck = new ExcelPackage();

				for (int k = 0; k < ListaTablas.Tables.Count; k++)
				{

					ExcelWorksheet ws = pck.Workbook.Worksheets.Add(ListaTablas.Tables[k].TableName.ToString());
					DataTable dt = ListaTablas.Tables[k].Copy();

					char LetraInicial = (char)(65);
					string celdaLetras = LetraInicial + "";
					string LetraNueva = "";
					int n = 1;
					int m = 1;
					for (int i = 1; i <= dt.Columns.Count; i++)
					{
						if (i % 27 == 0)
						{
							LetraNueva = (char)(64 + n) + "";
							n++;
							m = 1;
						}
						celdaLetras = LetraNueva + (char)(64 + m);
						ws.Cells[celdaLetras + "1"].Value = dt.Columns[i - 1].ColumnName.ToString();
						ws.Cells[celdaLetras + "1"].Style.Font.Bold = true;
						ws.Cells[celdaLetras + "1"].Style.Font.Size = 7;
						m++;
					}
					int row = 2;
					// int rowCuadro = 5;
					int columana = 0;
					int rowR = 1;

					//CREANDO LOS REGISTROS
					for (int i = 0; i < dt.Rows.Count; i++)
					{
						char LetraInicialF = (char)(65);
						string celdaLetrasF = LetraInicialF + "";
						string LetraNuevaF = "";
						int nF = 1;
						int mF = 1;
						for (int j = 1; j <= dt.Columns.Count; j++)
						{
							if (j % 27 == 0)
							{
								LetraNuevaF = (char)(64 + nF) + "";
								nF++;
								mF = 1;
							}
							celdaLetrasF = LetraNuevaF + (char)(64 + mF);
							if (dt.Columns[j - 1].DataType == typeof(DateTime))
							{
								ws.Cells[celdaLetrasF + (row + i)].Style.Numberformat.Format = "dd/mm/yyyy";
								ws.Cells[celdaLetrasF + (row + i)].Value = dt.Rows[i].ItemArray[j - 1];
							}
							else
							{
								if (dt.Columns[j - 1].DataType == typeof(decimal))
								{
									ws.Cells[celdaLetrasF + (row + i)].Style.Numberformat.Format = "#,##0.00";
									ws.Cells[celdaLetrasF + (row + i)].Value = dt.Rows[i].ItemArray[j - 1];
								}
								else
								{
									ws.Cells[celdaLetrasF + (row + i)].Value = dt.Rows[i].ItemArray[j - 1];
								}
							}
							ws.Cells[celdaLetrasF + (row + i)].Style.Font.Bold = false;
							ws.Cells[celdaLetrasF + (row + i)].Style.Font.Size = 7;
							columana = j + 1;
							rowR = row + i;
							mF++;
						}

					}

					//ws.Cells["A1:AZ1000"].AutoFitColumns();
				}

				return pck;
			}
			catch (Exception ex)
			{
				string x = ex.Message;
				return null;
			}
		}
		public static int[] Shuffle(int[] values)
		{
			var n = values.Count();
			var rnd = new Random();
			for (int i = n - 1; i > 0; i--)
			{
				var j = rnd.Next(0, i);
				var temp = values[i];
				values[i] = values[j];
				values[j] = temp;
			}
			return values;
		}
      
		public static string Encriptar(string cadena)
		{
			try
			{
				byte[] llave;
				byte[] arreglo = UTF8Encoding.UTF8.GetBytes(cadena);
				MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
				llave = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(MatrixUtilitarios.DatosGlobales.ClaveEncripcion));
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

		public static string desEncriptar(string cadena)
		{
			try
			{
				byte[] llave;
				byte[] arreglo = Convert.FromBase64String(cadena);
				MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
				llave = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(MatrixUtilitarios.DatosGlobales.ClaveEncripcion));
				md5.Clear();
				TripleDESCryptoServiceProvider tripledes = new TripleDESCryptoServiceProvider();
				tripledes.Key = llave;
				tripledes.Mode = CipherMode.ECB;
				tripledes.Padding = PaddingMode.PKCS7;
				ICryptoTransform convertir = tripledes.CreateDecryptor();
				byte[] resultado = convertir.TransformFinalBlock(arreglo, 0, arreglo.Length);
				tripledes.Clear();
				string cadena_descifrada = UTF8Encoding.UTF8.GetString(resultado);
				return cadena_descifrada;
			}
			catch (Exception ex)
			{
				var x = ex.Message;
				return "";
			}
		}
		
        public static string Left(string param, int length)
		{
			string result = param.Substring(0, length);
			return result;
		}
		public static string Right(string param, int length)
		{
			string result = param.Substring(param.Length - length, length);
			return result;
		}

		public static string Mid(string param, int startIndex, int length)
		{
			string result = param.Substring(startIndex, length);
			return result;
		}

		public static string Mid(string param, int startIndex)
		{
			string result = param.Substring(startIndex);
			return result;
		}
		public static bool ValidarEmail(string email)
		{
			string expresion;
			expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
			if (Regex.IsMatch(email, expresion))
			{
				if (Regex.Replace(email, expresion, String.Empty).Length == 0)
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

		public static void EnviarEmail(string destinatarios, string mensaje, string asunto, string pathArchivo, string destinatariosCopia = "")
		{
			string userName = ConfigurationManager.AppSettings["CuentaEnvioEmail"];
			string password = ConfigurationManager.AppSettings["PasswordEnvioEmail"];
			string Dominio = ConfigurationManager.AppSettings["Dominio"];

			SmtpClient client = new SmtpClient();
			client.UseDefaultCredentials = false;
			client.Credentials = new System.Net.NetworkCredential(userName, password, Dominio);
			client.Port = Convert.ToInt32(ConfigurationManager.AppSettings["PuertoServidorEmail"]);
			client.Host = ConfigurationManager.AppSettings["ServidorEmail"];
			client.DeliveryMethod = SmtpDeliveryMethod.Network;
			client.EnableSsl = true;
			
			MailMessage mail = new MailMessage();
			mail.From = new MailAddress(userName, "");
			MailMessage msg = new MailMessage();
			msg.From = new MailAddress(userName);
			foreach (var email in destinatarios.Split(new[] { ';' }))
			{
				if (email != "")
				{
					msg.To.Add(new MailAddress(email));
				}
			}
			if (destinatariosCopia != "")
			{
				foreach (var email in destinatariosCopia.Split(new[] { ';' }))
				{
					if (email != "")
					{
						msg.CC.Add(new MailAddress(email));
					}
				}
			}

			msg.Subject = asunto;
			msg.Body = mensaje;
			msg.Sender = new MailAddress(userName);
			msg.IsBodyHtml = true;
			if (pathArchivo != "")
			{
				msg.Attachments.Add(new Attachment(pathArchivo));
			}
			msg.Subject = asunto;
			msg.Body = mensaje;

			client.Send(msg);


		}
        public static int EnviarEmailConfirmacion(string destinatarios, string mensaje, string asunto, string pathArchivo, string destinatariosCopia = "")
        {
			try {

            string userName = ConfigurationManager.AppSettings["CuentaEnvioEmail"];
            string password = ConfigurationManager.AppSettings["PasswordEnvioEmail"];
            string Dominio = ConfigurationManager.AppSettings["Dominio"];

            SmtpClient client = new SmtpClient();
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(userName, password, Dominio);
            client.Port = Convert.ToInt32(ConfigurationManager.AppSettings["PuertoServidorEmail"]);
            client.Host = ConfigurationManager.AppSettings["ServidorEmail"];
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = true;

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(userName, "");
            MailMessage msg = new MailMessage();
            msg.From = new MailAddress(userName);
            foreach (var email in destinatarios.Split(new[] { ';' }))
            {
                if (email != "")
                {
                    msg.To.Add(new MailAddress(email));
                }
            }
            if (destinatariosCopia != "")
            {
                foreach (var email in destinatariosCopia.Split(new[] { ';' }))
                {
                    if (email != "")
                    {
                        msg.CC.Add(new MailAddress(email));
                    }
                }
            }

            msg.Subject = asunto;
            msg.Body = mensaje;
            msg.Sender = new MailAddress(userName);
            msg.IsBodyHtml = true;
            if (pathArchivo != "")
            {
                msg.Attachments.Add(new Attachment(pathArchivo));
            }
            msg.Subject = asunto;
            msg.Body = mensaje;

            client.Send(msg);

				return 1;
            }
            catch (IOException e) {
                return 0;

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

		public static string stringLineal(List<string> texto, string caracter = ",")
		{
			string cadena = "";
			if (texto != null)
			{
				foreach (var item in texto)
				{
					cadena += item + caracter;
				}

				if (cadena.Length > 0)
				{
					cadena = cadena.Substring(0, cadena.Length - 1);
				}
			}
			return cadena;
		}

		public static string ConvertirHaciaNumeroOrdinal(int numero)
		{

			string strNumeroOrdinal = "";
			String[] Unidad = { "", "primera", "segunda", "tercera",
			"cuarta", "quinta", "sexta", "septima", "octava",
			"novena" };
			String[] Decena = { "", "decimo", "vigesimo", "trigesimo",
			"cuadragesimo", "quincuagesimo", "sexagesimo",
			"septuagesimo", "octogesimo", "nonagesimo" };
			String[] Centena = {"", "centesimo", "ducentesimo",
			"tricentesimo", " cuadringentesimo", " quingentesimo",
			" sexcentesimo", " septingentesimo", " octingentesimo",
			" noningentesimo"};

			if (numero > 0)
			{
				int u = numero % 10;
				int d = (numero / 10) % 10;
				int c = numero / 100;

				if (numero >= 100)
				{
					strNumeroOrdinal = Centena[c] + " " + Decena[d] + " " + Unidad[u];
				}
				else
				{
					if (numero >= 10)
					{
						strNumeroOrdinal = Decena[d] + " " + Unidad[u];
					}
					else
					{
						strNumeroOrdinal = Unidad[numero];
					}
				}


			}
			return strNumeroOrdinal;
		}

		public static bool ValidarFecha(object inValue)
		{
			bool bValid;
			try
			{
				DateTime myDT = DateTime.Parse(inValue.ToString());
				bValid = true;
			}
			catch
			{
				bValid = false;
			}
			return bValid;
		}

		public static string GetRandomCodigo(int length)
		{
			const string chars = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

			StringBuilder sb = new StringBuilder();
			Random rnd = new Random();

			for (int i = 0; i < length; i++)
			{
				int index = rnd.Next(chars.Length);
				sb.Append(chars[index]);
			}

			return sb.ToString();
		}
		public static bool ValidarCaracterEspecial(string value)
		{
			value = value.ToLower();

			if (value.IndexOf('#') != -1) { return false; }
			else if (value.IndexOf('!') != -1) { return false; }
			else if (value.IndexOf('$') != -1) { return false; }
			else if (value.IndexOf('%') != -1) { return false; }
			else if (value.IndexOf('/') != -1) { return false; }
			else if (value.IndexOf('=') != -1) { return false; }
			else if (value.IndexOf('?') != -1) { return false; }
			else if (value.IndexOf('¡') != -1) { return false; }
			else if (value.IndexOf('¿') != -1) { return false; }
			else if (value.IndexOf("'") != -1) { return false; }
			else if (value.IndexOf('´') != -1) { return false; }
			else if (value.IndexOf('*') != -1) { return false; }
			else if (value.IndexOf('{') != -1) { return false; }
			else if (value.IndexOf('}') != -1) { return false; }
			else if (value.IndexOf('+') != -1) { return false; }
			else if (value.IndexOf('"') != -1) { return false; }
			else if (value.IndexOf('”') != -1) { return false; }
			else if (value.IndexOf('“') != -1) { return false; }
			else if (value.IndexOf('|') != -1) { return false; }
			else { return true; }
		}
	}


}
