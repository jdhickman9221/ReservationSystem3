using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AxelHarveyStudio.UI.MVC.Startup))]
namespace AxelHarveyStudio.UI.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
