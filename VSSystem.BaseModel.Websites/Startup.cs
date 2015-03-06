using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(scheduleMVC.Startup))]
namespace scheduleMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
