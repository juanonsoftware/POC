using System.Collections.Generic;

namespace GenericConfigirationSectionDemo
{
    public class ServerSetting
    {
        public string ServiceKey { get; set; }

        public List<Server> Servers { get; set; }
    }
}