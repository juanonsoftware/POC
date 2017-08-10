
namespace GdNet.IdentitySrv.Migrations.ScopeConfiguration
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<IdentityServer3.EntityFramework.ScopeConfigurationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\ScopeConfiguration";
        }

        protected override void Seed(IdentityServer3.EntityFramework.ScopeConfigurationDbContext context)
        {
            //  This method will be called after migrating to the latest version.
        }
    }
}
