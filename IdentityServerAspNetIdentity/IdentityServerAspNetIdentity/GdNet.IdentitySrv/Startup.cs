using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Services;
using IdentityServer3.EntityFramework;
using Microsoft.Owin.Security.Google;
using Owin;
using Serilog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using AuthenticationOptions = IdentityServer3.Core.Configuration.AuthenticationOptions;

namespace GdNet.IdentitySrv
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Trace()
                .CreateLogger();

            var serverOptions = new IdentityServerOptions
            {
                SiteName = ConfigurationManager.AppSettings["SiteName"],

                RequireSsl = false,
                SigningCertificate = LoadCertificate(),

                LoggingOptions = new LoggingOptions()
                {
                    EnableHttpLogging = true,
                    EnableKatanaLogging = true,
                },

                Factory = BuildFactory(),

                AuthenticationOptions = new AuthenticationOptions()
                {
                    IdentityProviders = ConfigureIdentityProviders,
                    EnableSignOutPrompt = false,
                    EnablePostSignOutAutoRedirect = true,
                    RequireSignOutPrompt = false,
                    CookieOptions = new CookieOptions()
                    {
                        SlidingExpiration = true,
                    },
                },
            };

            app.UseIdentityServer(serverOptions);
        }

        private IdentityServerServiceFactory BuildFactory()
        {
            var serviceOptions = new EntityFrameworkServiceOptions()
            {
                ConnectionString = "IdSvr3Config"
            };

            var factory = new IdentityServerServiceFactory();
            //    .UseInMemoryUsers(Users.Get());

            factory.RegisterClientStore(serviceOptions);
            factory.RegisterScopeStore(serviceOptions);
            factory.RegisterOperationalServices(serviceOptions);

            factory.UserService = new Registration<IUserService>(UserServiceFactory.Create());

            return factory;
        }

        public static void ConfigureIdentityProviders(IAppBuilder app, string signInAsType)
        {
            var google = new GoogleOAuth2AuthenticationOptions
            {
                AuthenticationType = "Google",
                Caption = "Login with Google",
                SignInAsAuthenticationType = signInAsType,
                ClientId = "",
                ClientSecret = ""
            };

            var scopes = new List<string>
            {
                "openid",
                "profile",
                "email"
            };

            foreach (var scope in scopes.Where(scope => !google.Scope.Contains(scope)))
            {
                google.Scope.Add(scope);
            }

            app.UseGoogleAuthentication(google);
        }

        private X509Certificate2 LoadCertificate()
        {
            return new X509Certificate2(
                string.Format(@"{0}\bin\Configuration\idsrv3test.pfx", AppDomain.CurrentDomain.BaseDirectory), "idsrv3test");
        }

        
    }
}