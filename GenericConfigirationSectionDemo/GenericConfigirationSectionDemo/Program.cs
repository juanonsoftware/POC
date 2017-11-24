using System;
using System.Collections.Generic;
using System.Configuration;

namespace GenericConfigirationSectionDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //var setting = BuildSampleSettings();

            //var xml = setting.Serialize(new XmlSerializationStrategy());
            //File.WriteAllText("Servers.xml", xml);

            var config = ConfigurationManager.GetSection("serviceSettings") as DynamicXmlSection;
            if (config != null)
            {
                Console.WriteLine(config.InnerXml);
            }

            Console.ReadLine();
        }

        private static ServerSetting BuildSampleSettings()
        {
            return new ServerSetting()
            {
                ServiceKey = "ABCD-EFGS1234545667-XYZ",

                Servers = new List<Server>()
                {
                    new Server()
                    {
                        Name = @"Server1\SQL2K5",
                        Tag = "PROD",
                        Note = "Production server",
                        ConnectionString = @"Server=Server1\SQL2K5;Database=DB01;Uid=sa;Pwd=sa"
                    },
                    new Server()
                    {
                        Name = @"Server2\SQL2K8",
                        Tag = "PROD",
                        Note = "PROD server for next release",
                        ConnectionString = @"Server=Server2\SQL2K8;Database=DB01;Uid=sa;Pwd=sa"
                    },
                }
            };
        }
    }
}
