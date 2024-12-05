using Microsoft.AspNetCore.Http;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;
using MatrixService;

using System.Text;

namespace MatrixWeb.Controllers
{
    //[ValidarUsuario]
    public class MenuController : Controller
    {

        List<Menu> menuLista = new List<Menu>();
        private readonly ISvcPerfilOpcion iperfilopcion;
        public MenuController(ISvcPerfilOpcion _iperfilopcion, ISvcPerfil _iperfil)
        {
            iperfilopcion = _iperfilopcion;
        }
      
        [HttpPost]
        public IActionResult InicializarMenu()
        {
            string NuevoMenuHtml = string.Empty;
            NuevoMenuHtml = CrearMenu();
            return Json(NuevoMenuHtml);
        }
        private string CrearMenu()
        {
            Seguridad.ListaPerfilOpcion listaperfilopcion = new Seguridad.ListaPerfilOpcion();
            Seguridad.PerfilOpcion perfilopcion = new Seguridad.PerfilOpcion();
          
                listaperfilopcion = iperfilopcion.ListarMenuPorUsuario( Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == "null" ? 0 : HttpContext.Session.GetString("IdUsuario")), Convert.ToInt32(HttpContext.Session.GetString("IdEmpleadoPerfil") == null ? 0 : HttpContext.Session.GetString("IdEmpleadoPerfil")));
           
            string menuPrincipal = "";
         
            var listaModulo = listaperfilopcion.lista.GroupBy(x => new { x.opcion.modulo.NombreModulo, x.opcion.modulo.RutaImagenModulo })
                    .Select(y => new Sistema.Modulo
                    {
                        NombreModulo = y.Key.NombreModulo,
                        RutaImagenModulo = y.Key.RutaImagenModulo
                    }
                    );
            foreach (var modulo in listaModulo)
            {
                menuPrincipal = menuPrincipal + "<li class='with-sub compact-hide'><a href='#' class='waves-effect  waves-light'><span class='s-caret'><i class='fa fa-angle-down'></i></span><span class='s-icon'><i class='" + modulo.RutaImagenModulo + "'></i></span><span class='s-text'>" + modulo.NombreModulo + "</span></a> <ul>";
                foreach (var opcionPadre in listaperfilopcion.lista.Where(x => x.opcion.IdOpcionPadre == 0 && (x.opcion.modulo.NombreModulo == modulo.NombreModulo)))
                {
                    if (opcionPadre.opcion.catalogotipoopcion.IdCatalogo == 7)
                    {
                        var relativeUrl = Url.Action(opcionPadre.opcion.Accion, opcionPadre.opcion.Controlador) + opcionPadre.opcion.Parametros;
                        string link1 = "<a href='" + relativeUrl + "'><span class='s-icon'><i class='" + opcionPadre.opcion.RutaImagenOpcion + "'></i></span> " + opcionPadre.opcion.NombreOpcion + "</a>";
                        menuPrincipal = menuPrincipal + " <li>" + link1 + "</li>";
                        //string x = Request.Url.Authority;
                        //string x1 = Request.Url.Authority;

                    }
                    else
                    {
                        menuPrincipal = menuPrincipal + "<li class='with-sub'><a href='#'><span class='s-icon'><i class='" + opcionPadre.opcion.RutaImagenOpcion + "'></i></span><span class='s-text'> " + opcionPadre.opcion.NombreOpcion + " </span><span class='s-caret'><i class='fa fa-angle-down'></i></span></a><ul>";
                        foreach (var opcionHijo in listaperfilopcion.lista.Where(x => x.opcion.IdOpcionPadre == opcionPadre.opcion.IdOpcion).OrderBy(x => x.opcion.OrdenOpcion))
                        {
                            if (opcionHijo.opcion.catalogotipoopcion.IdCatalogo == 7)
                            {
                                var relativeUrl1 = Url.Action(opcionHijo.opcion.Accion, opcionHijo.opcion.Controlador) + opcionHijo.opcion.Parametros;
                                string link2 = "<a href='" + relativeUrl1 + "'>" + opcionHijo.opcion.NombreOpcion + "</a>";
                                menuPrincipal = menuPrincipal + " <li>" + link2 + "</li>";
                            }
                        }
                        menuPrincipal = menuPrincipal + "</ul></li>";
                    }
                }
                menuPrincipal = menuPrincipal + "</ul></li>";
            }
            return menuPrincipal;
        }        
    }


    public class Menu
    {
        private int menuPai = 0;

        public int MenuPai
        {
            get { return menuPai; }
            set { menuPai = value; }
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Link { get; set; }


        public Menu(int _id, string _itemNome, string _itemLink, int _menuPai)
        {
            Id = _id;
            Nome = _itemNome;
            Link = _itemLink;
            MenuPai = _menuPai;
        }
    }
}









		