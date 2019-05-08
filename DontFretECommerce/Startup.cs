using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DontFretECommerce.Startup))]
namespace DontFretECommerce
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
