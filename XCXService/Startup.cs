using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(XCXService.Startup))]
namespace XCXService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
