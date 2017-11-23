using Rabbit.SerializationMaster;
using Rabbit.SerializationMaster.JsonNet;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;

namespace SerializeDynamicToJson
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = CreateObjects().ToList();

            var json = list.Serialize(new JsonSerializationStrategy());
            File.WriteAllText("objects.json", json);

            var deserialised = json.Deserialize<dynamic>(new JsonSerializationStrategy());

            //var xml = list.Serialize(new XmlSerializationStrategy());
        }

        static IEnumerable<dynamic> CreateObjects()
        {
            dynamic obj1 = new ExpandoObject();
            obj1.Id = 1;
            obj1.Name = "Server1";
            obj1.Tag = "PROD";
            obj1.Note = string.Empty;

            dynamic obj2 = new ExpandoObject();
            obj2.Id = 1;
            obj2.Name = "Server2";
            obj2.Tag = "PROD";
            obj2.Note = string.Empty;

            dynamic obj3 = new ExpandoObject();
            obj3.Id = 3;
            obj3.Name = "Server1";
            obj3.Tag = "PROD";
            obj3.Note = "Main server";
            obj3.Location = "US";
            obj3.Info = new
            {
                RAM = 8,
                CPU = 18
            };

            return new[] { obj1, obj2, obj3 };
        }
    }
}
