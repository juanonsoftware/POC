using System;
using System.Collections;
using System.Globalization;
using System.Resources;
using System.Web.Compilation;

namespace CustomResourceProviders
{
    internal class CustomResourceProvider : IResourceProvider
    {
        string _virtualPath;
        string _className;

        public CustomResourceProvider(string virtualPath, string classname)
        {
            _virtualPath = virtualPath;
            _className = classname;
        }

        private IDictionary GetResourceCache(string culturename)
        {
            return (IDictionary)
                System.Web.HttpContext.Current.Cache[culturename];
        }

        object IResourceProvider.GetObject
            (string resourceKey, CultureInfo culture)
        {
            object value;

            string cultureName = null;
            if (culture != null)
            {
                cultureName = culture.Name;
            }
            else
            {
                cultureName = CultureInfo.CurrentUICulture.Name;
            }

            value = GetResourceCache(cultureName)[resourceKey];
            if (value == null)
            {
                value = GetResourceCache(null)[resourceKey];
            }
            return value;
        }


        IResourceReader IResourceProvider.ResourceReader
        {
            get
            {
                string cultureName = null;
                CultureInfo currentUICulture = CultureInfo.CurrentUICulture;
                if (!String.Equals(currentUICulture.Name,
                    CultureInfo.InstalledUICulture.Name))
                {
                    cultureName = currentUICulture.Name;
                }

                return new CustomResourceReader
                    (GetResourceCache(cultureName));
            }
        }
    }
}