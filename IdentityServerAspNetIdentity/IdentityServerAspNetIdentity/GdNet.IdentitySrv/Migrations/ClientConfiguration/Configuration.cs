
namespace GdNet.IdentitySrv.Migrations.ClientConfiguration
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<IdentityServer3.EntityFramework.ClientConfigurationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\ClientConfiguration";
        }

        protected override void Seed(IdentityServer3.EntityFramework.ClientConfigurationDbContext context)
        {
            //  This method will be called after migrating to the latest version.
        }
    }
}
