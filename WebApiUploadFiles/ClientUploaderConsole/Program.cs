using System;
using System.IO;
using System.Net.Http;

namespace ClientUploaderConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new HttpClient();
            var content = new MultipartFormDataContent();

            foreach (var file in Directory.GetFiles("SampleFiles"))
            {
                content.Add(new StreamContent(File.OpenRead(file)), "File", Path.GetFileName(file));
            }

            try
            {
                HttpResponseMessage result = client.PostAsync("http://localhost:58650/api/FileUpload", content).Result;
                var output = result.Content.ReadAsStringAsync().Result;

                Console.WriteLine("Uploaded files: " + output);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                Console.ReadLine();
            }
        }
    }
}
