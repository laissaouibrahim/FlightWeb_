using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FlightWeb.Startup))]
namespace FlightWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
