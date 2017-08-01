using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using cgritton.libs.net.settings.xml.configuration;
using cgritton.libs.net.settings;

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

            var attrib = new ConfigAttributeAttribute(r, s);

            Assert.AreEqual(attrib.RootElement, r);

        }
        [TestMethod]
        public void XmlConfigAttributeSettingsElement()
        {

            var attrib = new ConfigAttributeAttribute(r, s);

            Assert.AreEqual(attrib.SettingsElement, s);

        }

        [TestMethod]
        public void SanitizationForXml()
        {
            string xmlfail = "my_corrupted=xml value";

            string xmlresolved = xmlfail.SanitizeForXml();

            Assert.AreNotEqual(xmlfail, xmlresolved);

        }

        [TestMethod]
        public void SanitizationForXmlResolved()
        {
            string xmlfail = "my_corrupted=xml value";
            string xmlresolved = xmlfail.SanitizeForXml();
            string resolved = xmlresolved.FromXmlSanitzedString();

            Assert.AreEqual(xmlfail, resolved);

        }
    }
}
