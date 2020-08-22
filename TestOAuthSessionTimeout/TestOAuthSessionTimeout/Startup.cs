using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TestOAuthSessionTimeout.Startup))]
namespace TestOAuthSessionTimeout
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
