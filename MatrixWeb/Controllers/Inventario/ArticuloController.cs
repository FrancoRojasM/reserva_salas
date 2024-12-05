using DataTables.AspNet.Core;
using Microsoft.AspNetCore.Http;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using MatrixService;
using System.Threading.Tasks;
using Inventario;
using System.IO;
using Utilitarios;
using System.Collections.Generic;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;
using Newtonsoft.Json;
using General;

namespace MatrixWeb.Controllers
{
    public class ArticuloController : Controller
    {
        private ISvcArticulo iarticulo;
        private ISvcEmpleado iempleado;
        private ISvcUsuarioPerfil iusuarioperfil;
        public ArticuloController(ISvcArticulo _iarticulo, ISvcEmpleado _iempleado, ISvcUsuarioPerfil _iusuarioperfil)
        {
            iarticulo = _iarticulo;
            iempleado = _iempleado;
            iusuarioperfil = _iusuarioperfil;
        }

        [HttpGet]
        public async Task<IActionResult> IndexArticulo()
        {
            int accesoReportesSeguimiento =  await iusuarioperfil.ValidarAccesoUsuarioPerfilAsync(Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")), 22);
            int accesoReportesGenerales = await iusuarioperfil.ValidarAccesoUsuarioPerfilAsync(Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")), 23);
            ViewBag.ReportesSeguimiento = accesoReportesSeguimiento;
            ViewBag.ReportesGenerales = accesoReportesGenerales;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ListarArticulo(IDataTablesRequest requestModel)
        {

            ListaArticulo listaarticulo = new Inventario.ListaArticulo();
            Articulo articulo = new Inventario.Articulo();
            int numeroPagina = (requestModel.Start / requestModel.Length) + 1;
            try
            {
                listaarticulo = await iarticulo.ListarArticuloAsync( Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")), null, null, numeroPagina, requestModel.Length, requestModel.Search.Value);
                
                if (listaarticulo.mensaje.CodigoMensaje > 0)
                {
                    return RedirectToAction("PaginaError", new { DescripcionError = listaarticulo.mensaje.DescripcionMensaje });
                }
                var totalCount = listaarticulo.paginacion.TotalRegistros;
                var data = listaarticulo.lista.Select(lq => new
                {
                    Id = lq.Id,
                    Codigo_Barra = lq.Codigo_Barra,
                    ItemCode = lq.ItemCode,
                    Ubicacion_Region = lq.Ubicacion_Region,
                    Ubicacion_Sede = lq.Ubicacion_Sede,
                    Ubicacion_Area = lq.Ubicacion_Area,
                    Ubicacion_Sub_Area = lq.Ubicacion_Sub_Area,
                    Piso = lq.Piso,
                    ItemName = lq.ItemName,
                    Detalle = lq.Detalle,
                    Marca = lq.Marca,
                    Modelo = lq.Modelo,
                    Serie = lq.Serie,
                    Material = lq.Material,
                    Medida = lq.Medida,
                    Color = lq.Color,
                    Estado = lq.Estado,
                    Condicion_Uso = lq.Condicion_Uso,
                    Usuario = lq.Usuario,
                    Documento = lq.Documento,
                    Cargo = lq.Cargo,
                    Gerencia = lq.Gerencia,
                    GroupName = lq.GroupName,
                    EstadoConta = lq.EstadoConta,
                    Periodo = lq.Periodo,
                    Tipo_Inventario = lq.Tipo_Inventario,
                    Tipo_Asignacion = lq.Tipo_Asignacion,
                    Sucursal = lq.Sucursal,
                    Image1 = lq.Image1,
                    Image2 = lq.Image2,
                    Image3 = lq.Image3,
                    Image4 = lq.Image4,
                    US_Registro = lq.US_Registro,
                    FE_CREA = lq.FE_CREA,
                }).ToList();

                var jsonData = new { draw = requestModel.Draw, recordsFiltered = totalCount, recordsTotal = totalCount, data = data }; return Ok(jsonData);
            }
            catch (Exception ex)
            {
                return RedirectToAction("PaginaError", new { DescripcionError = "Hubo un error [" + ex.Message + "]" });
            }
        }

        [HttpPost]
        public IActionResult RegistroNuevoArticulo()
        {
            string NombreModal = Request.Form["NombreModal"].ToString();
            ViewBag.NombreModal = NombreModal;
            Articulo articulo = new Inventario.Articulo();
            try
            {
                return View(articulo);
            }
            catch (Exception ex)
            {
                articulo.mensaje.CodigoMensaje = 1;
                articulo.mensaje.DescripcionMensaje = ex.Message;
                return View(articulo);
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> RegistroArticulo()
        {

            int Id = Convert.ToInt32(Request.Form["Id"]);
            var sucursales = await iarticulo.ObtenerSucursalesAsync();

            var sucursalSeleccionada = HttpContext.Session.GetString("Sucursal");
            var ubicacion = HttpContext.Session.GetString("Ubicacion");
            ViewBag.Sucursales = sucursales;
            ViewBag.SucursalSeleccionada = sucursalSeleccionada;
            try
            {
                Articulo articulo = new Inventario.Articulo();
                List<string> imagenes = new List<string>();

                string region = string.Empty;
                string sede = string.Empty;

                if (!string.IsNullOrEmpty(ubicacion) && ubicacion.Contains("-"))
                {
                    var partesUbicacion = ubicacion.Split('-');
                    if (partesUbicacion.Length == 2)
                    {
                        region = partesUbicacion[0];
                        sede = partesUbicacion[1];
                    }
                }

                if (Id > 0)
                {
                    articulo = await iarticulo.ObtenerArticuloAsync(Id);
                    
                    if (!string.IsNullOrEmpty(articulo.Image1)) imagenes.Add(articulo.Image1);
                    if (!string.IsNullOrEmpty(articulo.Image2)) imagenes.Add(articulo.Image2);
                    if (!string.IsNullOrEmpty(articulo.Image3)) imagenes.Add(articulo.Image3);
                    if (!string.IsNullOrEmpty(articulo.Image4)) imagenes.Add(articulo.Image4);

                    ViewBag.ImagenesCargadas = imagenes;
                    
                } 
                else
                {
                    articulo.Periodo = DateTime.Now.ToString("yyyy");
                }
                ViewBag.region = region;
                ViewBag.sede = sede;
                return View(articulo);
            }
            catch (Exception ex)
            {
                return RedirectToAction("PaginaError", new { DescripcionError = "Hubo un error [" + ex.Message + "]" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> BuscarCodigo()
        {

            string Id = Request.Form["Id"];
            var sucursales = await iarticulo.ObtenerSucursalesAsync();

            var sucursalSeleccionada = HttpContext.Session.GetString("Sucursal");
            var ubicacion = HttpContext.Session.GetString("Ubicacion");
            ViewBag.Sucursales = sucursales;
            ViewBag.SucursalSeleccionada = sucursalSeleccionada;
            try
            {
                Articulo articulo = new Inventario.Articulo();
                List<string> imagenes = new List<string>();

                string region = string.Empty;
                string sede = string.Empty;

                if (!string.IsNullOrEmpty(ubicacion) && ubicacion.Contains("-"))
                {
                    var partesUbicacion = ubicacion.Split('-');
                    if (partesUbicacion.Length == 2)
                    {
                        region = partesUbicacion[0];
                        sede = partesUbicacion[1];
                    }
                }

                articulo = await iarticulo.ObtenerArticuloPorCodeAsync(Id);

                if (!string.IsNullOrEmpty(articulo.Image1)) imagenes.Add(articulo.Image1);
                if (!string.IsNullOrEmpty(articulo.Image2)) imagenes.Add(articulo.Image2);
                if (!string.IsNullOrEmpty(articulo.Image3)) imagenes.Add(articulo.Image3);
                if (!string.IsNullOrEmpty(articulo.Image4)) imagenes.Add(articulo.Image4);

                ViewBag.ImagenesCargadas = imagenes;
                ViewBag.region = region;
                ViewBag.sede = sede;

                return View(articulo);
            }
            catch (Exception ex)
            {
                return RedirectToAction("PaginaError", new { DescripcionError = "Hubo un error [" + ex.Message + "]" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> GuardarRegistroArticulo()
        {
            int Id = Convert.ToInt32(Request.Form["txtRegId"]);
            string Codigo_Barra = Request.Form["txtCodigoBarra"];
            string ItemCode = Request.Form["txtItemCode"];
            string Ubicacion_Region = Request.Form["txtUbicacionRegion"];
            string Ubicacion_Sede = Request.Form["txtUbicacionSede"];
            string Ubicacion_Area = Request.Form["txtUbicacionArea"];
            string Ubicacion_Sub_Area = Request.Form["txtUbicacionSubArea"];
            string Piso = Request.Form["txtPiso"];
            string ItemName = Request.Form["txtItemName"];
            string Detalle = Request.Form["txtDetalle"];
            string Marca = Request.Form["txtMarca"];
            string Modelo = Request.Form["txtModelo"];
            string Serie = Request.Form["txtSerie"];
            string Material = Request.Form["txtMaterial"];
            string Medida = Request.Form["txtMedida"];
            string Color = Request.Form["txtColor"];
            string Estado = Request.Form["optEstado"];
            string Condicion_Uso = Request.Form["optCondicionUso"];
            string Usuario = Request.Form["txtUsuario"];
            string Documento = Request.Form["optDocumento"];
            string Cargo = Request.Form["txtCargo"];
            string Gerencia = Request.Form["txtGerencia"];
            string Periodo = Request.Form["txtPeriodo"];
            string TipoInventario = Request.Form["optTipoInventario"];
            string TipoAsignacion = Request.Form["optTipoAsignacion"];
            int Sucursal = Convert.ToInt32(Request.Form["optSucursal"]);

            string imagenesGuardadasJson = Request.Form["imagenesGuardadas"];
            List<string> imagenesGuardadas = JsonConvert.DeserializeObject<List<string>>(imagenesGuardadasJson) ?? new List<string>();

            Articulo articulo = new Inventario.Articulo();

            try
            {
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "inventario_imagenes");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                string image1 = null;
                string image2 = null;
                string image3 = null;
                string image4 = null;

                for (int i = 0; i < imagenesGuardadas.Count && i < 4; i++)
                {
                    switch (i)
                    {
                        case 0:
                            image1 = imagenesGuardadas[i];
                            break;
                        case 1:
                            image2 = imagenesGuardadas[i];
                            break;
                        case 2:
                            image3 = imagenesGuardadas[i];
                            break;
                        case 3:
                            image4 = imagenesGuardadas[i];
                            break;
                    }
                }

                for (int i = 0; i < Request.Form.Files.Count && imagenesGuardadas.Count + i < 4; i++)
                {
                    IFormFile foto = Request.Form.Files[i];
                    if (foto.Length > 0)
                    {
                        string fileName = Codigo_Barra + "_" + Guid.NewGuid().ToString() + Path.GetExtension(foto.FileName);
                        string filePath = Path.Combine(uploadsFolder, fileName);

                        using (var image = Image.Load(foto.OpenReadStream()))
                        {
                            image.Mutate(x => x.Resize(new ResizeOptions
                            {
                                Mode = ResizeMode.Max,
                                Size = new Size(1024, 1024)
                            }));

                            await image.SaveAsync(filePath, new JpegEncoder
                            {
                                Quality = 70
                            });
                        }

                        switch (imagenesGuardadas.Count + i)
                        {
                            case 0:
                                image1 = fileName;
                                break;
                            case 1:
                                image2 = fileName;
                                break;
                            case 2:
                                image3 = fileName;
                                break;
                            case 3:
                                image4 = fileName;
                                break;
                        }
                    }
                }

                articulo = await iarticulo.GuardaArticuloAsync(Id, Codigo_Barra, ItemCode, Ubicacion_Region, Ubicacion_Sede, Ubicacion_Area, Ubicacion_Sub_Area, Piso, ItemName, Detalle, Marca, Modelo, Serie, Material,
                                                                Medida, Color, Estado, Condicion_Uso, Usuario, Documento, Cargo, Gerencia, Periodo, TipoInventario, TipoAsignacion, Sucursal, image1, image2, image3, image4, Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") ?? "0"));

                return Json(articulo);
            }
            catch (Exception ex)
            {
                articulo.mensaje.CodigoMensaje = 1;
                articulo.mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA AL GUARDAR, COMUNIQUESE CON EL ADMINISTRADOR DEL SISTEMA";
                articulo.mensaje.DescripcionMensajeSistema = ex.Message;
                return Json(articulo);
            }
        }


        [HttpPost]
        public async Task<IActionResult> EliminarArticulo()
        {
            int Id = Convert.ToInt32(Request.Form["Id"]);

            Articulo articulo = new Inventario.Articulo();
            Utilitarios.Mensaje mensaje = new Utilitarios.Mensaje();
            try
            {
                mensaje = await iarticulo.EliminarArticuloAsync( Id, Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));

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
        public async Task<IActionResult> LiberarCorrelativo()
        {
            string Id = Request.Form["Id"];
            int correlativoActual = int.Parse(Id.ToString().Split('-').Last().Trim());
            Mensaje mensaje = new Utilitarios.Mensaje();
            try
            {
                mensaje = await iarticulo.LiberarCorrelativoAsync(correlativoActual);

                return Json(mensaje);
            }
            catch (Exception ex)
            {
                mensaje.CodigoMensaje = 1;
                mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA AL LIBERAR EL CORRELATIVO, COMUNIQUESE CON EL ADMINISTRADOR DEL SISTEMA";
                mensaje.DescripcionMensajeSistema = ex.Message;
                return Json(mensaje);
            }
        }

        [HttpPost]
        public async Task<IActionResult> ReservarCorrelativo()
        {
            Mensaje mensaje = new Utilitarios.Mensaje();
            try
            {
                string correlativo = await iarticulo.ReservarCorrelativoAsync();

                return Json(new { success = true, correlativo = correlativo });
            }
            catch (Exception ex)
            {
                mensaje.CodigoMensaje = 1;
                mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA AL CREAR EL CORRELATIVO, COMUNIQUESE CON EL ADMINISTRADOR DEL SISTEMA";
                mensaje.DescripcionMensajeSistema = ex.Message;
                return Json(mensaje);
            }
        }

        [HttpPost]
        public async Task<IActionResult> BuscarCampo(string query, string field)
        {
            Mensaje mensaje = new Utilitarios.Mensaje();
            try
            {
                var resultados = await iarticulo.BuscarCampoAsync(query, field);

                return Json(resultados);
            }
            catch (Exception ex)
            {
                mensaje.CodigoMensaje = 1;
                mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA AL BUSCAR EL CAMPO, COMUNIQUESE CON EL ADMINISTRADOR DEL SISTEMA";
                mensaje.DescripcionMensajeSistema = ex.Message;
                return Json(mensaje);
            }
        }

        [HttpGet]
        public async Task<ActionResult> DescargarReporteExcel()
        {
            string Titulo = "REPORTE DE INVENTARIO";
            try
            {
                var dt = await iarticulo.DescargarReporteExcel(Convert.ToInt32(HttpContext.Session.GetString("IdUsuario")));
                var stream = new MemoryStream();
                OfficeOpenXml.ExcelPackage pck = new OfficeOpenXml.ExcelPackage(stream);


                dt.Tables[0].TableName = "Reporte";
                dt.Tables[0].ExtendedProperties.Add("Titulo", Titulo);

                pck = MatrixUtilitarios.Utilitario.ExportarReporteV2(dt);
                stream.Position = 0;

                string excelName = "ReporteInventario" + DateTime.Now.ToString("ddMMyyyyHHmmss") + ".xlsx";
                return File(pck.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);

            }
            catch (Exception ex)
            {
                return RedirectToAction("PaginaError", new { DescripcionError = "Hubo un error [" + ex.Message + "]" });
            }
        }

        [HttpGet]
        public async Task<ActionResult> DescargarReporteCodigosExcel()
        {
            string Titulo = "REPORTE DE INVENTARIO";
            try
            {
                var dt = await iarticulo.DescargarReporteCodigosExcel(Convert.ToInt32(HttpContext.Session.GetString("IdUsuario")));
                var stream = new MemoryStream();
                OfficeOpenXml.ExcelPackage pck = new OfficeOpenXml.ExcelPackage(stream);


                dt.Tables[0].TableName = "Reporte";
                dt.Tables[0].ExtendedProperties.Add("Titulo", Titulo);

                pck = MatrixUtilitarios.Utilitario.ExportarReporteV2(dt);
                stream.Position = 0;

                string excelName = "ReporteCodigosDeBarra" + DateTime.Now.ToString("ddMMyyyyHHmmss") + ".xlsx";
                return File(pck.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);

            }
            catch (Exception ex)
            {
                return RedirectToAction("PaginaError", new { DescripcionError = "Hubo un error [" + ex.Message + "]" });
            }
        }

        [HttpGet]
        public async Task<ActionResult> DescargarReporteCodigosQRExcel()
        {
            string Titulo = "REPORTE DE INVENTARIO";
            try
            {
                var dt = await iarticulo.DescargarReporteCodigosQRExcel(Convert.ToInt32(HttpContext.Session.GetString("IdUsuario")));
                var stream = new MemoryStream();
                OfficeOpenXml.ExcelPackage pck = new OfficeOpenXml.ExcelPackage(stream);


                dt.Tables[0].TableName = "Reporte";
                dt.Tables[0].ExtendedProperties.Add("Titulo", Titulo);

                pck = MatrixUtilitarios.Utilitario.ExportarReporteV2(dt);
                stream.Position = 0;

                string excelName = "ReporteCodigosQR" + DateTime.Now.ToString("ddMMyyyyHHmmss") + ".xlsx";
                return File(pck.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);

            }
            catch (Exception ex)
            {
                return RedirectToAction("PaginaError", new { DescripcionError = "Hubo un error [" + ex.Message + "]" });
            }
        }

        [HttpGet]
        public async Task<ActionResult> DescargarReporteBienesExcel()
        {
            string Titulo = "REPORTE DE INVENTARIO";
            try
            {
                var dt = await iarticulo.DescargarReporteBienesExcel(Convert.ToInt32(HttpContext.Session.GetString("IdUsuario")));
                var stream = new MemoryStream();
                OfficeOpenXml.ExcelPackage pck = new OfficeOpenXml.ExcelPackage(stream);


                dt.Tables[0].TableName = "Reporte";
                dt.Tables[0].ExtendedProperties.Add("Titulo", Titulo);

                pck = MatrixUtilitarios.Utilitario.ExportarReporteV2(dt);
                stream.Position = 0;

                string excelName = "ReporteDeBienesFaltantes" + DateTime.Now.ToString("ddMMyyyyHHmmss") + ".xlsx";
                return File(pck.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);

            }
            catch (Exception ex)
            {
                return RedirectToAction("PaginaError", new { DescripcionError = "Hubo un error [" + ex.Message + "]" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> ListarDocumentos()
        {
            string search = Request.Form["search"];
            Mensaje mensaje = new Utilitarios.Mensaje();
            try
            {
                var documentos = await iempleado.ObtenerDocumentosAsync(search);

                if (documentos.lista != null && documentos.lista.Any())
                {
                    return Json(new
                    {
                        items = documentos.lista.Select(d => new
                        {
                            id = d.Documento,
                            text = $"{d.Documento} - {d.Nombre}"
                        })
                    });
                }
                else
                {
                    mensaje.CodigoMensaje = 1;
                    mensaje.DescripcionMensaje = "No se encontraron documentos.";
                    return Json(mensaje);
                }
            }
            catch (Exception ex)
            {
                mensaje.CodigoMensaje = 1;
                mensaje.DescripcionMensaje = "Sucedió un problema al obtener los documentos.";
                mensaje.DescripcionMensajeSistema = ex.Message;
                return Json(mensaje);
            }
        }

        [HttpPost]
        public async Task<IActionResult> ObtenerCargo()
        {
            string search = Request.Form["search"];
            Mensaje mensaje = new Utilitarios.Mensaje();
            try
            {
                var documentos = await iempleado.ObtenerDocumentosAsync(search);

                if (documentos.lista != null && documentos.lista.Any())
                {
                    return Json(documentos.lista.Select(d => new {
                        cargo = d.Cargo,
                        nombre = d.Nombre
                    }));
                }
                else
                {
                    mensaje.CodigoMensaje = 1;
                    mensaje.DescripcionMensaje = "No se encontró información.";
                    return Json(mensaje);
                }
            }
            catch (Exception ex)
            {
                mensaje.CodigoMensaje = 1;
                mensaje.DescripcionMensaje = "Sucedió un problema al obtener los datos.";
                mensaje.DescripcionMensajeSistema = ex.Message;
                return Json(mensaje);
            }
        }

    }
    
}









