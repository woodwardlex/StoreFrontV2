using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StoreFrontV2.UI.MVC.Startup))]
namespace StoreFrontV2.UI.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
