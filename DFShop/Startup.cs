using Microsoft.Owin;
using Owin;
using DFShop.Models;
using DFShop.Migrations;

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
