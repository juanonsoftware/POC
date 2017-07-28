using Rabbit.Foundation.Configuration;
using Rabbit.IOC;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace ElmahMvc.Global
{
    public class ServicesModule : ModuleBase, IPackage
    {
        public void RegisterServices(Container container)
        {
            container.RegisterSingleton<IConfiguration, EnvironmentAwareAppSettingsConfiguration>();
        }
    }
}