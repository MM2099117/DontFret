using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DFShop.Startup))]
namespace DFShop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
