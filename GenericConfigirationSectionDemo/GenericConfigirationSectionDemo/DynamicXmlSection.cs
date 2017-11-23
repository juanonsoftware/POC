using System.Configuration;
using System.Xml;

namespace GenericConfigirationSectionDemo
{
    public class DynamicXmlSection : ConfigurationSection
    {
        public string InnerXml { get; set; }

        protected override void DeserializeElement(XmlReader reader, bool serializeCollectionKey)
        {
            InnerXml = reader.ReadInnerXml();
            base.DeserializeElement(reader, serializeCollectionKey);
        }
    }
}