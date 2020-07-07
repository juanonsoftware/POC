using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KeyEngine;
using System.Collections.Generic;

namespace KeyValidatorUnitTestProject
{
    [TestClass]
    public class KeyEncoderUnitTest
    {
        [TestMethod]
        public void CanEncodeDictionaryWithOneItem()
        {
            var encoder = new XmlParameterEncoder();

            var text = encoder.Encode(new Dictionary<string, string>
            {
                { "Key1","ONE" }
            });

            Assert.IsNotNull(text);
            Assert.IsTrue(text.Length > 0);
        }

        [TestMethod]
        public void CanDecodeFromXmlToDictionaryContainingOneItem()
        {
            const string text = "<?xml version=\"1.0\"?>\r\n<ArrayOfItem xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">\r\n  <Item>\r\n    <Key xsi:type=\"xsd:string\">Key1</Key>\r\n    <Value xsi:type=\"xsd:string\">ONE</Value>\r\n  </Item>\r\n</ArrayOfItem>";

            var encoder = new XmlParameterEncoder();
            var dict = encoder.Decode(text);

            Assert.AreEqual(1, dict.Count);
            Assert.AreEqual("ONE", dict["Key1"]);
        }
    }
}
