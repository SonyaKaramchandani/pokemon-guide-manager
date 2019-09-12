using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Biod.Surveillance.Startup))]
namespace Biod.Surveillance
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
