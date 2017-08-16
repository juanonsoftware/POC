using Resources.Concrete;
using System.Configuration;
using System.IO;
using System.Web;

namespace CustomerBasedMultiLanguages.Resources
{
    public partial class GlobalResources
    {
        private static XmlResourceProvider resourceProvider
        {
            get
            {
                return new XmlResourceProvider(GetXmlFile());
            }
        }

        private static string GetXmlFile()
        {
            var customerId = GetCustomerId();

            if (!string.IsNullOrWhiteSpace(customerId))
            {
                var xml = HttpContext.Current.Server.MapPath(string.Format(@"~/bin\App_Data\Resources\{0}\GlobalResources.xml", customerId));
                if (File.Exists(xml))
                {
                    return xml;
                }
            }

            return HttpContext.Current.Server.MapPath(@"~/bin\App_Data\Resources\GlobalResources.xml");
        }

        private static string GetCustomerId()
        {
            return ConfigurationManager.AppSettings["CustomerId"];
        }
    }
}
