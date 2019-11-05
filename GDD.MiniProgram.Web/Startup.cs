using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GDD.MiniProgram.Web.Startup))]
namespace GDD.MiniProgram.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
