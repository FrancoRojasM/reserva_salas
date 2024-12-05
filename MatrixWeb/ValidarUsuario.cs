using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatrixWeb
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.AspNetCore.Routing;
    public class ValidarUsuario : AuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {
            //your judgement to if user has logged in
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                //redirect to Account/Login
                filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(
                            new
                            {
                                controller = "Usuario",
                                action = "Login",
                                returnUrl = filterContext.HttpContext.Request.Path.ToUriComponent()
                            }));
            }

        }
    }

    //public class ValidarUsuario : ActionFilterAttribute
    //{
    //    public override void OnActionExecuting(ActionExecutingContext filterContext)
    //    {
    //        int IdUsuario = 0;
    //        //IdUsuario = Convert.ToInt32(HttpContext.Session.GetString("IdUsuario") == null ? 0 : HttpContext.Session.GetString("IdUsuario"));
    //        //if (IdUsuario == 0)
    //        //{
    //        //    if (filterContext.HttpContext.Request.IsHttps.is ())
    //        //    {
    //        //        filterContext.HttpContext.Response.StatusCode = 403;

    //        //    }
    //        //    else
    //        //    {
    //        //        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary {
    //        //                                                            { "Controller", "Usuario" },
    //        //                                                            { "Action", "Login" }
    //        //        }
    //        //                                                         );
    //        //    }
    //        //}
    //        base.OnActionExecuting(filterContext);
    //    }
    //}
}
