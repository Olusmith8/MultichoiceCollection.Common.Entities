using Hangfire;
using Microsoft.Owin;
using Microsoft.Owin.Security.DataProtection;
using MultichoiceCollection.Common.Entities;
using MultichoiceCollection.Common.Shared;
using MultichoiceCollection.Presentation;
using MultichoiceCollection.Presentation.Services;
using Owin;

[assembly: OwinStartup(typeof(Startup))]
namespace MultichoiceCollection.Presentation
{
    public partial class Startup
    {
        internal static IDataProtectionProvider DataProtectionProvider { get; private set; }
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            DataProtectionProvider = app.GetDataProtectionProvider();
           // GlobalConfiguration.Configuration.UseSqlServerStorage(ConnectionStrings.BaseConnectionString);
            //app.UseHangfireServer();
            //app.UseHangfireDashboard("/hangsidedashboard", new DashboardOptions
            //{
            //    AuthorizationFilters = new[] { new HanfireRestrictiveAuthorizationFilter() }
            //});
        }
    }
}
/*internal static IDataProtectionProvider DataProtectionProvider { get; private set; }

    public void ConfigureAuth(IAppBuilder app)
    {
        DataProtectionProvider = app.GetDataProtectionProvider();
        // other stuff.
    }*/
