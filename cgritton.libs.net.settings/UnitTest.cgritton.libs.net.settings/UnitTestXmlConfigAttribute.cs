using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using cgritton.libs.net.settings.xml.configuration;

namespace UnitTest.cgritton.libs.net.settings
{
    [TestClass]
    public class UnitTestXmlConfigAttribute
    {
        const string r = "root_element";
        const string s = "setings_element";

        [TestMethod]
        public void XmlConfigAttributeRootElement()
        {

            var attrib = new XmlConfigAttribute(r, s);

            Assert.AreEqual(attrib.RootElement, r);

        }
        [TestMethod]
        public void XmlConfigAttributeSettingsElement()
        {

            var attrib = new XmlConfigAttribute(r, s);

            Assert.AreEqual(attrib.SettingsElement, s);

        }

    }
}
