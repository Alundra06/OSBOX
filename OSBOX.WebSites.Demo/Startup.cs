using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OSBOX.WebSites.Demo.Startup))]
namespace OSBOX.WebSites.Demo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
