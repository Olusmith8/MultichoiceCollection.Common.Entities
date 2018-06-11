using System.Collections.Generic;
using Hangfire.Dashboard;

namespace MultichoiceCollection.Presentation.Services
{
    public class HanfireRestrictiveAuthorizationFilter: IDashboardAuthorizationFilter
    {
        
        public bool Authorize(DashboardContext context)
        {
            // In case you need an OWIN context, use the next line,
            // `OwinContext` class is the part of the `Microsoft.Owin` package.
            //var context = new OwinContext(owinEnvironment);
            // Allow all authenticated users to see the Dashboard (potentially dangerous).
            // return context.Authentication.User.Identity.IsAuthenticated;

            /*
             FOR DEBUG PURPOSES RETURN TRUE
             */
            return true;

        }
    }
}