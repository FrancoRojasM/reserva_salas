
using DataTables.AspNet.Core;
using Microsoft.AspNetCore.Http;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using MatrixService;
using System.Threading.Tasks;
using RecursoHumano;
using Utilitarios;


namespace MatrixWeb.Controllers
{
	//[ValidarUsuario]
	public class EmpleadoController : Controller
	{
		private ISvcEmpleado iempleado;
		
		public EmpleadoController(ISvcEmpleado _iempleado)
		{
			iempleado = _iempleado;
			//empleado = new Empleado();
		}
		[HttpGet]
		public IActionResult Directorio()
		{
			var conpermiso = MatrixUtilitarios.Utilitario.TienePermisoControladorAccion(HttpContext.Request.RouteValues["controller"].ToString(), HttpContext.Request.RouteValues["action"].ToString()); if (conpermiso == -1) { return RedirectToAction("Login", "Usuario"); }
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> ListarDirectorio(IDataTablesRequest requestModel)
		{
			int IdArea = Convert.ToInt32(Request.Form["IdArea"]);
			int IdCargo = Convert.ToInt32(Request.Form["IdCargo"]);
			string NombreCompleto = Request.Form["NombreCompleto"].ToString();
			RecursoHumano.ListaEmpleado listaempleado = new RecursoHumano.ListaEmpleado();

			try
			{
				listaempleado = await iempleado.ListarDirectorio(Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")), IdArea, IdCargo, NombreCompleto);
				if (listaempleado.mensaje.CodigoMensaje > 0)
				{
					return RedirectToAction("PaginaError", new { DescripcionError = listaempleado.mensaje.DescripcionMensaje });
				}
				var data = listaempleado.lista.Select(lq => new
				{
					lq.IdEmpleado,
					lq.persona.NombreCompleto,
					lq.persona.Celular,
					lq.persona.Email,
					lq.persona.FechaNacimiento,
					lq.persona.TelefonoFijo,
					lq.persona.RutaArchivoFoto
				}).ToList();
				return Ok(data);
			}
			catch (Exception ex)
			{
				return RedirectToAction("PaginaError", new { DescripcionError = "Hubo un error [" + ex.Message + "]" });
			}
		}
		[HttpGet]
		public IActionResult IndexEmpleado()
		{
			var conpermiso = MatrixUtilitarios.Utilitario.TienePermisoControladorAccion(HttpContext.Request.RouteValues["controller"].ToString(), HttpContext.Request.RouteValues["action"].ToString()); if (conpermiso == -1) { return RedirectToAction("Login", "Usuario"); }
			return View();
		}
		[HttpGet]
		public IActionResult ReporteEmpleado()
		{
			ViewBag.IdUsuario = Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario"));
			ViewBag.Clave = Convert.ToString(HttpContext.Session.GetString("Clave") == null ? 0 : HttpContext.Session.GetString("Clave"));
			var conpermiso = MatrixUtilitarios.Utilitario.TienePermisoControladorAccion(HttpContext.Request.RouteValues["controller"].ToString(), HttpContext.Request.RouteValues["action"].ToString()); if (conpermiso == -1) { return RedirectToAction("Login", "Usuario"); }
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> ListarEmpleado(IDataTablesRequest requestModel)
		{
			ListaEmpleado listaempleado = new ListaEmpleado();
			int IdEmpresaPadre = Convert.ToInt32(HttpContext.Session.GetString("IdEmpresaPadre").ToString());
			int numeroPagina = (requestModel.Start / requestModel.Length) + 1;
			try
			{
				listaempleado = await iempleado.ListarEmpleadoAsync(Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")), null, null, numeroPagina, requestModel.Length, requestModel.Search.Value);
				if (listaempleado.mensaje.CodigoMensaje > 0)
				{
					return RedirectToAction("PaginaError", new { DescripcionError = listaempleado.mensaje.DescripcionMensaje });
				}
				var totalCount = listaempleado.paginacion.TotalRegistros;
				var data = listaempleado.lista.Select(lq => new
				{
					lq.IdEmpleado,
					Persona = lq.persona.NombreCompleto,
					Activo = (lq.Activo == true ? "Si" : "No"),
					CatalogoEstadoEmpleado = lq.catalogoestadoempleado.Descripcion,
					CatalogoTipoEmpleado = lq.catalogotipoempleado.Descripcion

				}).ToList();

				var jsonData = new { draw = requestModel.Draw, recordsFiltered = totalCount, recordsTotal = totalCount, data = data }; return Ok(jsonData);
			}
			catch (Exception ex)
			{
				return RedirectToAction("PaginaError", new { DescripcionError = "Hubo un error [" + ex.Message + "]" });
			}
		}
		[HttpPost]
		public async Task<IActionResult> ListarComboEmpleado()
		{
			var listaempleado = await iempleado.ListarComboEmpleado(Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));
			try
			{
				var data = listaempleado.lista.Select(lq => new
				{
					IdEmpleado = lq.IdEmpleado,
					NombreEmpleado = lq.persona.NombreCompleto
				}).ToList();

				return Json(listaempleado);
			}
			catch (Exception ex)
			{
				listaempleado.mensaje.CodigoMensaje = 1;
				listaempleado.mensaje.DescripcionMensaje = "Sucedió un error en la Vista: " + HttpContext.Request.RouteValues["action"].ToString() + " del controlador: " + HttpContext.Request.RouteValues["controller"].ToString();
				listaempleado.mensaje.DescripcionMensajeSistema = ex.Message;
				return Json(listaempleado);
			}
		}
		[HttpPost]
		public async Task<IActionResult> RegistroEmpleado()
		{
			var empleado = new Empleado();
			int IdEmpleado = Convert.ToInt32(Request.Form["IdEmpleado"]);
			try
			{
				if (IdEmpleado > 0)
				{
					empleado = await iempleado.ObtenerEmpleadoAsync(IdEmpleado, Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));
				}
				return View(empleado);
			}
			catch (Exception ex)
			{
				return RedirectToAction("PaginaError", new { DescripcionError = "Hubo un error [" + ex.Message + "]" });
			}
		}
		[HttpPost]
		public async Task<IActionResult> GuardarRegistroEmpleado()
		{

			int IdEmpleado = Convert.ToInt32(Request.Form["txtRegIdEmpleado"]);
			int IdEmpresaPadre = Convert.ToInt32(HttpContext.Session.GetString("IdEmpresaPadre").ToString());
			int IdPersona = Convert.ToInt32(Request.Form["cmbPersona"]);
			bool Activo = Convert.ToBoolean(Convert.ToInt32(Request.Form["optActivo"]));
			int IdCatalogoEstadoEmpleado = Convert.ToInt32(Request.Form["cmbCatalogoEstadoEmpleado"]);
			int IdCatalogoTipoEmpleado = Convert.ToInt32(Request.Form["cmbCatalogoTipoEmpleado"]);
			var empleado = new Empleado();
			try
			{

				empleado = await iempleado.GuardaEmpleadoAsync(

									IdEmpleado,
											IdPersona,
											IdEmpresaPadre,
											Activo,
											IdCatalogoTipoEmpleado,
											IdCatalogoEstadoEmpleado,
									Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));

				return Json(empleado);
			}
			catch (Exception ex)
			{
				empleado.mensaje.CodigoMensaje = 1;
				empleado.mensaje.DescripcionMensaje = "SUCEDIÓ UN PROBLEMA AL GUARDAR, COMUNIQUESE CON EL ADMINISTRADOR DEL SISTEMA";
				empleado.mensaje.DescripcionMensajeSistema = ex.Message;
				return Json(empleado);
			}
		}
		[HttpPost]
		public async Task<IActionResult> EliminarEmpleado()
		{
			int IdEmpleado = Convert.ToInt32(Request.Form["IdEmpleado"]);
			Utilitarios.Mensaje mensaje = new Utilitarios.Mensaje();
			try
			{
				mensaje = await iempleado.EliminarEmpleadoAsync(IdEmpleado, Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));
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
		public async Task<IActionResult> ListarEmpleadoCumpleanios()
		{
			ListaEmpleado listaempleado = new ListaEmpleado();

			try
			{
				listaempleado = await iempleado.ListarEmpleadoCumpleanios(Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));
				if (listaempleado.mensaje.CodigoMensaje > 0)
				{
					return RedirectToAction("PaginaError", new { DescripcionError = listaempleado.mensaje.DescripcionMensaje });
				}

				//var data = listaempleado.lista.Select(lq => new
				//{
				//    lq.IdEmpleado,
				//    Persona = lq.persona.NombreCompleto,
				//    Activo = (lq.Activo == true ? "Si" : "No"),
				//    CatalogoEstadoEmpleado = lq.catalogoestadoempleado.Descripcion,
				//    CatalogoTipoEmpleado = lq.catalogotipoempleado.Descripcion

				//}).ToList();

				return Json(new { data = listaempleado });
			}
			catch (Exception ex)
			{
				return RedirectToAction("PaginaError", new { DescripcionError = "Hubo un error [" + ex.Message + "]" });
			}
		}
		[HttpPost]
		public async Task<IActionResult> ListarComboEmpleado2()
		{
			var lista = await iempleado.ListarComboEmpleado2(Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario")));
			try
			{
				var data = lista.lista.Select(lq => new
				{
					IdEmpleado = lq.IdEmpleado,
					NombreEmpleado = lq.persona.NombreCompleto
				}).ToList();

				return Json(data);
			}
			catch (Exception ex)
			{
				lista.mensaje.CodigoMensaje = 1;
				lista.mensaje.DescripcionMensaje = "Sucedió un error en la Vista: " + HttpContext.Request.RouteValues["action"].ToString() + " del controlador: " + HttpContext.Request.RouteValues["controller"].ToString();
				lista.mensaje.DescripcionMensajeSistema = ex.Message;
				return Json(lista);
			}
		}
    }
}