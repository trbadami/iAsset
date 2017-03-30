using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(iAsset.Web.UI.Startup))]
namespace iAsset.Web.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
