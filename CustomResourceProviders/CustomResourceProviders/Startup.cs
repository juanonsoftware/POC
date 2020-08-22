using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CustomResourceProviders.Startup))]
namespace CustomResourceProviders
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
