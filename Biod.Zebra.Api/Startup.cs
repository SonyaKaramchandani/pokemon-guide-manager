using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Biod.Zebra.Api.Startup))]
namespace Biod.Zebra.Api
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
