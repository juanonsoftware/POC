using System.Collections.Generic;
using GdNet.Common;

namespace Models
{
    public abstract class DynamicPropertyBase
    {
        protected readonly IDictionary<string, string> Properties = new Dictionary<string, string>();

        public string XmlProperties
        {
            get
            {
                return Properties.ToXmlIgnoreNullOrEmpty();
            }
        }
    }
}