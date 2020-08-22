using System.Web.Compilation;

namespace CustomResourceProviders
{
    public class CustomResxProviderFactory : ResourceProviderFactory
    {
        public CustomResxProviderFactory()
        {
            
        }

        public override IResourceProvider CreateGlobalResourceProvider(string classname)
        {
            return new CustomResourceProvider(null, classname);
        }
        public override IResourceProvider CreateLocalResourceProvider(string virtualPath)
        {
            return new CustomResourceProvider(virtualPath, null);
        }
    }
}