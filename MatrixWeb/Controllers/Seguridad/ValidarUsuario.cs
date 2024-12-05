using System;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using System.Web.Routing;

namespace MatrixWeb.Controllers
{
    public class ValidarUsuario : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            int IdUsuario = 0;
            IdUsuario = Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario"));
            if (IdUsuario == 0)
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.HttpContext.Response.StatusCode = 403;

                }
                else
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary {
                                                                        { "Controller", "Usuario" },
                                                                        { "Action", "Login" }
                    }
                                                                     );
                }
            }
            base.OnActionExecuting(filterContext);
        }
    }
    //public class ValidarUsuario : ActionFilterAttribute
    //{

    //    public override void OnActionExecuting(ActionExecutingContext filterContext)
    //    {

    //        if (HttpContext.Current.HttpContext.Session.GetString("IdUsuario") == null)
    //        {

    //            if (filterContext.HttpContext.Request.IsAjaxRequest())
    //            {
    //                UrlHelper url = new UrlHelper(HttpContext.Current.Request.RequestContext);
    //                filterContext.Result = new JavaScriptResult() { Script = "<script>alert('Su session se ha terminado'); window.location.href = '" + url.Action("Login", "Ingreso") + "';</script>" };
    //            }
    //            else
    //            {
    //                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary {
    //                                                                    { "Controller", "Usuario" },
    //                                                                    { "Action", "Login" }
    //                                                                }
    //                                                                 );
    //            }
    //        }
    //        base.OnActionExecuting(filterContext);

    //    }

    //}
}