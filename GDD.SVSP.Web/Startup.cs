using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GDD.SVSP.Web.Startup))]
namespace GDD.SVSP.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
