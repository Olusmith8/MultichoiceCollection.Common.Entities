using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using MultichoiceCollection.Common.Entities.Enum;
using MultichoiceCollection.Common.Shared.UnitOfWork;
using MultichoiceCollection.Models.Repositories.Context;
using MultichoiceCollection.Presentation.Models;
using MultichoiceCollection.Presentation.Services;
//using MultichoiceCollection.Services.Unity;

namespace MultichoiceCollection.Presentation.Controllers
{
    public class BaseController : Controller
    {
        protected UserModel UserInfo { get; set; }

        /// <summary>
        /// AuthenticationManager to manage user authentications
        /// </summary>
        protected IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        /// <summary>
        /// SignInManager to manage user sign in
        /// </summary>
        protected ApplicationSignInManager SignInManager
        {
            get
            {
                return HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
        }
        /// <summary>
        /// UserManager to manage user information
        /// </summary>
        protected ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }
        /// <summary>
        /// UserManager to manage user information
        /// </summary>
        protected RoleManager<IdentityRole> RoleManager
        {
            get
            {
                return new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new AppDbContext()));
            }
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var accessToken = Request.Cookies[CookieConstantService.ACCESS_TOKEN];
            var clientAppName = Request.Cookies[CookieConstantService.CLIENT_APP_NAME];
            var expirationDate = Request.Cookies[CookieConstantService.EXPIRATION_DATE];
            var userInfo = new UserModel
            {
                accessToken = accessToken?.Value,
                clientAppName = clientAppName?.Value,
                expirationDate = expirationDate?.Value
            };
            TempData["UserInfo"] = userInfo;
            this.UserInfo = userInfo;
        }
        /// <summary>
        /// A global notification for users
        /// </summary>
        /// <param name="message">Message to display</param>
        /// <param name="alertType">Alert type to use</param>
        public void ShowMessage(string message, AlertType alertType)
        {
            TempData[SharedService.GetGlobalMessageKey] = new NotificationViewModel(message, alertType);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                UserManager?.Dispose();
                SignInManager?.Dispose();
               // UnitOfWork?.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}