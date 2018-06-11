using System;
using System.Web;
using System.Web.Mvc;
using MultichoiceCollection.Common.Entities.Base;
using MultichoiceCollection.Presentation.Services;

namespace MultichoiceCollection.Presentation.Attributes
{
    /// <summary>
    /// CustomAuthorizeAttribute is used to control the access to different parts of the app
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// Processes HTTP requests that fail authorization.
        /// </summary>
        /// <param name="filterContext">Encapsulates the information for using <see cref="T:System.Web.Mvc.AuthorizeAttribute"/>. The <paramref name="filterContext"/> object contains the controller, HTTP context, request context, action result, and route data.</param>
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)

        {
             var accessToken = filterContext.HttpContext.Request.Cookies[CookieConstantService.ACCESS_TOKEN];

             if (accessToken != null)
             {
                 return;
             }
             filterContext.HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                 filterContext.HttpContext.Response.RedirectPermanent("/Account/Login");

            base.HandleUnauthorizedRequest(filterContext);
        }

    }
}