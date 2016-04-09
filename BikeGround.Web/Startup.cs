using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BikeGround.Web.Startup))]
namespace BikeGround.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}