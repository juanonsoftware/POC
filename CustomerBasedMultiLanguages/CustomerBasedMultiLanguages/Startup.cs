using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CustomerBasedMultiLanguages.Startup))]
namespace CustomerBasedMultiLanguages
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
