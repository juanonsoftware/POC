using System;
using System.Text;

namespace KeyEngine
{
    public class Base64Encryptor : IEncryptor
    {
        public string Encrypt(string plaintext)
        {
            return Convert.ToBase64String(Encoding.Unicode.GetBytes(plaintext));
        }

        public string Decrypt(string base64Text)
        {
            return Encoding.Unicode.GetString(Convert.FromBase64String(base64Text));
        }
    }
}