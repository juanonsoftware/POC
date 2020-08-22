using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Resources.Utility;
using Resources.Concrete;

namespace ResourceBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new Resources.Utility.ResourceBuilder();
            //string filePath = builder.Create(new DbResourceProvider(@"Data Source=(localdb)\Projects;Initial Catalog=MvcInternationalization;Integrated Security=True;Pooling=False"), 
            //    summaryCulture: "en-us");
            var filePath = builder.Create(new XmlResourceProvider(@"D:\Wip\Practices\Github\POC\aspnet-mvc-internationalization-store-strings-in-database-or-xml\Resources\Resources.xml"),
                filePath: @"D:\Wip\Practices\Github\POC\aspnet-mvc-internationalization-store-strings-in-database-or-xml\MvcInternationalization\Resources.cs");

            Console.WriteLine("Created file {0}", filePath);

        }
    }
}
