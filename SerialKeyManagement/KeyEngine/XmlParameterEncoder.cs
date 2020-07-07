using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace KeyEngine
{
    /// <summary>
    ///     Encode a dictionary to xml and decode a xml string to a dictionary
    /// </summary>
    public class XmlParameterEncoder : IParameterEncoder
    {
        public IDictionary<string, string> Decode(string xml)
        {
            using (var reader = new StringReader(xml))
            {
                var xs = new XmlSerializer(typeof (List<Item>));
                var items = (List<Item>) xs.Deserialize(reader);

                return Utils.ToDict<string, string>(items);
            }
        }

        public string Encode(IDictionary<string, string> parameters)
        {
            using (var writer = new StringWriter())
            {
                var items = Utils.ToList(parameters);

                var xs = new XmlSerializer(typeof (List<Item>));
                xs.Serialize(writer, items);

                writer.Flush();

                return writer.GetStringBuilder().ToString();
            }
        }
    }
}