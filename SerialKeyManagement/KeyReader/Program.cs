using System;
using System.Configuration;
using System.IO;
using KeyEngine;

namespace KeyReader
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var encryptedKey = File.ReadAllText(ConfigurationManager.AppSettings["KeyFile"]);

            var encrytorType = ConfigurationManager.AppSettings["EncryptorType"];
            var encryptor = (IEncryptor)Activator.CreateInstance(Type.GetType(encrytorType) ?? typeof(Base64Encryptor));
            var key = encryptor.Decrypt(encryptedKey);

            var encoder = new XmlParameterEncoder();
            var parameters = encoder.Decode(key);

            foreach (var kvp in parameters)
            {
                Console.WriteLine("{0}:\t{1}", kvp.Key, kvp.Value);
            }

            Console.ReadLine();
        }
    }
}