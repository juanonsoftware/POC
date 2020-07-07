using System;
using System.Configuration;
using System.Globalization;
using System.IO;
using KeyEngine;

namespace KeyGenerator
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var builder = new KeyBuilder().Initialize(GetIdentity(), GetExpireDate());
            var key = builder.Build(new XmlParameterEncoder());

            var encrytorType = ConfigurationManager.AppSettings["EncryptorType"];
            var encryptor = (IEncryptor)Activator.CreateInstance(Type.GetType(encrytorType) ?? typeof(Base64Encryptor));
            var emcryptedKey = encryptor.Encrypt(key);

            var fileName = "Key_" + DateTime.Now.ToString("yyyyMMdd_hhmmss") + ".txt";
            File.WriteAllText(fileName, emcryptedKey);

            Console.WriteLine("New key generated and saved to file: " + fileName);
            Console.ReadLine();
        }

        private static string GetIdentity()
        {
            return ConfigurationManager.AppSettings[Constants.Identity];
        }

        private static DateTime GetExpireDate()
        {
            return DateTime.ParseExact(ConfigurationManager.AppSettings[Constants.ExpireDate], "yyyyMMdd",
                CultureInfo.InvariantCulture);
        }
    }
}