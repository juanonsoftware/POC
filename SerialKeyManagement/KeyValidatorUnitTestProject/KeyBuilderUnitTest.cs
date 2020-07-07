using System;
using KeyEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KeyValidatorUnitTestProject
{
    [TestClass]
    public class KeyBuilderUnitTest
    {
        [TestMethod]
        public void CanInitializeKeyBuilder()
        {
            var builder = new KeyBuilder().Initialize("appnam.com", DateTime.Today.AddDays(10));
            var encoder = new XmlParameterEncoder();

            var key = builder.Build(encoder);
        }


    }
}