using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BdDataApi.Startup))]
namespace BdDataApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
