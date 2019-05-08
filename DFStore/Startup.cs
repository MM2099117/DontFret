using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DFStore.Startup))]
namespace DFStore
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
