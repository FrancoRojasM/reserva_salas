using DataTables.AspNet.Core;
using Microsoft.AspNetCore.Http;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using MatrixService;
using System.Threading.Tasks;
using Sanciones;
using System.IO;
using Utilitarios;
using Microsoft.AspNetCore.Authorization;
using General;
using Inventario;

namespace MatrixWeb.Controllers
{
    public class SancionController : Controller
    {
        private ISvcSancion isancion;
        private ISvcUbigeo iubigeo;
        private ISvcCatalogo icatalogo;
        public SancionController(ISvcSancion _isancion, ISvcUbigeo _iubigeo, ISvcCatalogo _icatalogo)
        {
            isancion = _isancion;
            iubigeo = _iubigeo;
            icatalogo = _icatalogo;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Consulta()
        {

            var departamentos = iubigeo.ListarComboDepartamento().lista;
            ViewBag.Departamentos = departamentos;

            var TiposDocumento = await icatalogo.ListarCatalogoComboAsync(1, "Sancion");
            var dataTDocumento = TiposDocumento.lista.Select(a => new Catalogo
            {
                IdCatalogo = a.IdCatalogo,
                Descripcion = a.Descripcion
            }).ToList();
            ViewBag.TiposDocumento = dataTDocumento;

            var ConsejosRegionales = await isancion.ObtenerConsejosRegionalesAsync();
            ViewBag.ConsejosRegionales = ConsejosRegionales;

            var TiposSancion = await icatalogo.ListarCatalogoComboAsync(5, "Sancion");
            var dataTSancion = TiposSancion.lista.Select(b => new Catalogo
            {
                IdCatalogo = b.IdCatalogo,
                Descripcion = b.Descripcion
            }).ToList();
            ViewBag.TiposSancion = dataTSancion;

            return View();
        }


        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> BuscarSanciones([FromForm] SancionSearch searchModel, int pagina = 1, int dimensionPagina = 10)
        {
            try
            {
                ListaSancion listaSancion = await isancion.BuscarSancionesAsync(
                    IdUsuarioAuditoria: 0,
                    CampoOrdenado: null,
                    TipoOrdenacion: null,
                    NumeroPagina: pagina,
                    DimensionPagina: dimensionPagina,
                    BusquedaGeneral: searchModel.NombreCompleto,
                    TipoDocumento: searchModel.TipoDocumento,
                    NroDocumento: searchModel.NroDocumento,
                    NombreCompleto: searchModel.NombreCompleto,
                    ConsejoRegional: searchModel.ConsejoRegional,
                    TipoSancion: searchModel.TipoSancion,
                    Departamento: searchModel.Departamento,
                    Provincia: searchModel.Provincia,
                    Distrito: searchModel.Distrito
                );

                if (listaSancion.mensaje.CodigoMensaje > 0)
                {
                    return BadRequest(listaSancion.mensaje.DescripcionMensaje);
                }

                var jsonData = new
                {
                    sanciones = listaSancion.lista,
                    totalPaginas = (int)Math.Ceiling(listaSancion.paginacion.TotalRegistros / (double)dimensionPagina),
                    paginaActual = pagina,
                    TotalRegistros = listaSancion.paginacion.TotalRegistros
                };

                return Ok(jsonData);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Hubo un error [" + ex.Message + "]" });
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> ObtenerDetalleSancion(int id)
        {
            Sancion sancion = new Sanciones.Sancion();
            sancion = await isancion.ObtenerSancionAsync(id);

            if (sancion == null)
            {
                return NotFound();
            }

            return Json(new
            {
                sancion.TipoDocumento,
                sancion.NroDocumento,
                sancion.NombreCompleto,
                sancion.TipoSancion,
                sancion.ConsejoRegional,
                sancion.CMP,
                sancion.NumeroResolucion,
                sancion.FechaResolucion,
                sancion.FechaRegistroInscripcion,
                sancion.FechaNotificacionMedico,
                sancion.FechaVencimientoSancion,
                sancion.FechaCumplimientoMulta,
                sancion.EstadoSancion,
                sancion.SancionImpuesta,
                sancion.IndicacionNormaVulnerada
            });
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult> DescargarReporteExcelExterno(string TipoDocumento, string NroDocumento, string NombreCompleto, string ConsejoRegional, string TipoSancion, string Departamento, string Provincia, string Distrito)
        {
            string Titulo = "REPORTE DE SANCIONES";
            try
            {
                int IdUsuarioAuditoria = Convert.ToInt32(HttpContext.Session.GetString("IdUsuario"));

                var dt = await isancion.DescargarReporteExcelExterno(IdUsuarioAuditoria, TipoDocumento, NroDocumento, NombreCompleto, ConsejoRegional, TipoSancion, Departamento, Provincia, Distrito);

                var stream = new MemoryStream();
                OfficeOpenXml.ExcelPackage pck = new OfficeOpenXml.ExcelPackage(stream);

                dt.Tables[0].TableName = "Reporte";
                dt.Tables[0].ExtendedProperties.Add("Titulo", Titulo);

                pck = MatrixUtilitarios.Utilitario.ExportarReporteV2(dt);
                stream.Position = 0;

                string excelName = "ReporteSanciones" + DateTime.Now.ToString("ddMMyyyyHHmmss") + ".xlsx";
                return File(pck.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
            }
            catch (Exception ex)
            {
                return RedirectToAction("PaginaError", new { DescripcionError = "Hubo un error [" + ex.Message + "]" });
            }
        }


        [Authorize]
        [HttpGet]
        public IActionResult IndexSancion()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult RegistroNuevoSancion()
        {
            string NombreModal = Request.Form["NombreModal"].ToString();
            ViewBag.NombreModal = NombreModal;
            Sancion sancion = new Sanciones.Sancion();
            try
            {
                return View(sancion);
            }
            catch (Exception ex)
            {
                sancion.mensaje.CodigoMensaje = 1;
                sancion.mensaje.DescripcionMensaje = ex.Message;
                return View(sancion);
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> RegistroSancion()
        {

            int Id = Convert.ToInt32(Request.Form["Id"]);

            var TiposDocumento = await icatalogo.ListarCatalogoComboAsync(1, "Sancion");
            var dataTDocumento = TiposDocumento.lista.Select(a => new Catalogo
            {
                IdCatalogo = a.IdCatalogo,
                Descripcion = a.Descripcion
            }).ToList();
            ViewBag.TiposDocumento = dataTDocumento;

            var ConsejosRegionales = await isancion.ObtenerConsejosRegionalesAsync();
            ViewBag.ConsejosRegionales = ConsejosRegionales;

            var TiposSancion = await icatalogo.ListarCatalogoComboAsync(5, "Sancion");
            var dataTSancion = TiposSancion.lista.Select(b => new Catalogo
            {
                IdCatalogo = b.IdCatalogo,
                Descripcion = b.Descripcion
            }).ToList();
            ViewBag.TiposSancion = dataTSancion;

            try
            {
                Sancion sancion = new Sanciones.Sancion();

                if (Id > 0)
                {
                    sancion = await isancion.ObtenerSancionAsync(Id);
                }

                return View(sancion);
            }
            catch (Exception ex)
            {
                return RedirectToAction("PaginaError", new { DescripcionError = "Hubo un error [" + ex.Message + "]" });
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> BuscarCodigo()
        {

            string Id = Request.Form["Id"];

            var TiposDocumento = await icatalogo.ListarCatalogoComboAsync(1, "Sancion");
            var dataTDocumento = TiposDocumento.lista.Select(a => new Catalogo
            {
                IdCatalogo = a.IdCatalogo,
                Descripcion = a.Descripcion
            }).ToList();
            ViewBag.TiposDocumento = dataTDocumento;

            var ConsejosRegionales = await isancion.ObtenerConsejosRegionalesAsync();
            ViewBag.ConsejosRegionales = ConsejosRegionales;

            var TiposSancion = await icatalogo.ListarCatalogoComboAsync(5, "Sancion");
            var dataTSancion = TiposSancion.lista.Select(b => new Catalogo
            {
                IdCatalogo = b.IdCatalogo,
                Descripcion = b.Descripcion
            }).ToList();
            ViewBag.TiposSancion = dataTSancion;

            try
            {
                Sancion sancion = new Sanciones.Sancion();
                sancion = await isancion.ObtenerSancionPorCodeAsync(Id);
                return View(sancion);
            }
            catch (Exception ex)
            {
                return RedirectToAction("PaginaError", new { DescripcionError = "Hubo un error [" + ex.Message + "]" });
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ListarSancion(IDataTablesRequest requestModel)
        {
            ListaSancion listaSancion = new Sanciones.ListaSancion();
            Sancion sancion = new Sanciones.Sancion();
            int numeroPagina = (requestModel.Start / requestModel.Length) + 1;

            try
            {
                int idUsuario = Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? "0" : HttpContext.Session.GetString("IdUsuario"));
                listaSancion = await isancion.ListarSancionAsync(idUsuario, null, null, numeroPagina, requestModel.Length, requestModel.Search.Value);


                if (listaSancion.mensaje.CodigoMensaje > 0)
                {
                    return RedirectToAction("PaginaError", new { DescripcionError = listaSancion.mensaje.DescripcionMensaje });
                }

                var totalCount = listaSancion.paginacion.TotalRegistros;

                var data = listaSancion.lista.Select(lq => new
                {
                    Id = lq.Id,
                    TipoDocumento = lq.TipoDocumento,
                    NroDocumento = lq.NroDocumento,
                    NombreCompleto = lq.NombreCompleto,
                    TipoSancion = lq.TipoSancion,
                    ConsejoRegional = lq.ConsejoRegional,
                    CMP = lq.CMP,
                    NumeroResolucion = lq.NumeroResolucion,
                    FechaResolucion = lq.FechaResolucion.HasValue ? lq.FechaResolucion.Value.ToString("yyyy-MM-dd") : null,
                    FechaNotificacionMedico = lq.FechaNotificacionMedico.HasValue ? lq.FechaNotificacionMedico.Value.ToString("yyyy-MM-dd") : null,
                    FechaVencimientoSancion = lq.FechaVencimientoSancion.HasValue ? lq.FechaVencimientoSancion.Value.ToString("yyyy-MM-dd") : null,
                    FechaCumplimientoMulta = lq.FechaCumplimientoMulta.HasValue ? lq.FechaCumplimientoMulta.Value.ToString("yyyy-MM-dd") : null,
                    FechaRegistroInscripcion = lq.FechaRegistroInscripcion.HasValue ? lq.FechaRegistroInscripcion.Value.ToString("yyyy-MM-dd") : null,
                    EstadoSancion = lq.EstadoSancion,
                    SancionImpuesta = lq.SancionImpuesta,
                    IndicacionNormaVulnerada = lq.IndicacionNormaVulnerada
                }).ToList();

                var jsonData = new
                {
                    draw = requestModel.Draw,
                    recordsFiltered = totalCount,
                    recordsTotal = totalCount,
                    data = data
                };

                return Ok(jsonData);
            }
            catch (Exception ex)
            {
                return RedirectToAction("PaginaError", new { DescripcionError = "Hubo un error [" + ex.Message + "]" });
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> GuardarRegistroSancion()
        {
            int Id = Convert.ToInt32(Request.Form["txtRegId"]);
            string TipoDocumento = Request.Form["txtTipoDocumento"];
            string NroDocumento = Request.Form["txtNroDocumento"];
            string CMP = Request.Form["txtCMP"];
            string NombreCompleto = Request.Form["txtNombreApellidos"];
            string TipoSancion = Request.Form["txtTipoSancion"];
            string ConsejoRegional = Request.Form["txtConsejoRegional"];
            string NumeroResolucion = Request.Form["txtNumeroResolucion"];
            DateTime? FechaResolucion = string.IsNullOrWhiteSpace(Request.Form["dtFechaResolucion"]) ? (DateTime?)null : Convert.ToDateTime(Request.Form["dtFechaResolucion"]);
            DateTime? FechaNotificacionMedico = string.IsNullOrWhiteSpace(Request.Form["dtFechaNotificacionMedico"]) ? (DateTime?)null : Convert.ToDateTime(Request.Form["dtFechaNotificacionMedico"]);
            DateTime? FechaVencimientoSancion = string.IsNullOrWhiteSpace(Request.Form["dtFechaVencimientoSancion"]) ? (DateTime?)null : Convert.ToDateTime(Request.Form["dtFechaVencimientoSancion"]);
            DateTime? FechaCumplimientoMulta = string.IsNullOrWhiteSpace(Request.Form["dtFechaCumplimientoMulta"]) ? (DateTime?)null : Convert.ToDateTime(Request.Form["dtFechaCumplimientoMulta"]);
            DateTime? FechaRegistroInscripcion = string.IsNullOrWhiteSpace(Request.Form["dtFechaRegistroInscripcion"]) ? (DateTime?)null : Convert.ToDateTime(Request.Form["dtFechaRegistroInscripcion"]);
            string EstadoSancion = Request.Form["txtEstadoSancion"];
            string SancionImpuesta = Request.Form["txtSancionImpuesta"];
            string IndicacionNormaVulnerada = Request.Form["txtIndicacionNormaVulnerada"];

            Sancion sancion = new Sancion();

            try
            {
                sancion = await isancion.GuardaSancionAsync(Id, NombreCompleto, TipoDocumento, NroDocumento, TipoSancion, ConsejoRegional, CMP, NumeroResolucion, FechaResolucion, FechaNotificacionMedico, FechaVencimientoSancion, FechaCumplimientoMulta, FechaRegistroInscripcion, EstadoSancion, SancionImpuesta, IndicacionNormaVulnerada, Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") ?? "0"));

                return Json(sancion);
            }
            catch (Exception ex)
            {
                sancion.mensaje.CodigoMensaje = 1;
                sancion.mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA AL GUARDAR, COMUNÍQUESE CON EL ADMINISTRADOR DEL SISTEMA";
                sancion.mensaje.DescripcionMensajeSistema = ex.Message;
                return Json(sancion);
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> EliminarSancion()
        {
            int Id = Convert.ToInt32(Request.Form["Id"]);

            Sancion sancion = new Sanciones.Sancion();
            Utilitarios.Mensaje mensaje = new Utilitarios.Mensaje();
            try
            {
                mensaje = await isancion.EliminarSancionAsync(Id, Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));

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

        [Authorize]
        [HttpGet]
        public async Task<ActionResult> DescargarReporteExcel()
        {
            string Titulo = "REPORTE DE SANCIONES";
            try
            {
                var dt = await isancion.DescargarReporteExcel(Convert.ToInt32(HttpContext.Session.GetString("IdUsuario")));
                var stream = new MemoryStream();
                OfficeOpenXml.ExcelPackage pck = new OfficeOpenXml.ExcelPackage(stream);


                dt.Tables[0].TableName = "Reporte";
                dt.Tables[0].ExtendedProperties.Add("Titulo", Titulo);

                pck = MatrixUtilitarios.Utilitario.ExportarReporteV2(dt);
                stream.Position = 0;

                string excelName = "ReporteSanciones" + DateTime.Now.ToString("ddMMyyyyHHmmss") + ".xlsx";
                return File(pck.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);

            }
            catch (Exception ex)
            {
                return RedirectToAction("PaginaError", new { DescripcionError = "Hubo un error [" + ex.Message + "]" });
            }
        }



    }

}









