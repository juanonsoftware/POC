using KeyEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KeyValidatorUnitTestProject
{
    [TestClass]
    public class Base64EncryptorUnitTest
    {
        [TestMethod]
        public void CanEncode()
        {
            var encoder = new Base64Encryptor();

            var text = encoder.Encrypt("ABCD");

            Assert.AreEqual("QQBCAEMARAA=", text);
        }

        [TestMethod]
        public void CanDecode()
        {
            var encoder = new Base64Encryptor();

            var text = encoder.Decrypt("QQBCAEMARAA=");

            Assert.AreEqual("ABCD", text);
        }
    }
}