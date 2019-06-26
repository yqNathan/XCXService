using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GDD.Admin.Web.Startup))]
namespace GDD.Admin.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
