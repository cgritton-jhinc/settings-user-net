using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using cgritton.libs.net.settings.xml.configuration;
using cgritton.libs.net.settings;

namespace UnitTest.cgritton.libs.net.settings
{
    [TestClass]
    public class UnitTestXmlConfigWriter
    {
        string testDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\cgritton.libs.net.settings.unittest";

        [TestInitialize()]
        public void InitTestDirectory()
        {           
            if (!System.IO.Directory.Exists(testDir)) System.IO.Directory.CreateDirectory(testDir);
        }

        [TestCleanup()]
        public void DestroyTestDirectory()
        {
           if (System.IO.Directory.Exists(testDir)) System.IO.Directory.Delete(testDir, true);
        }

        [TestMethod]
        public void WriteValueNoSection()
        {
            //set variables
            string field = "my-app-field";
            string value = "my-app-value";
            System.IO.FileInfo file = new System.IO.FileInfo(testDir + @"\global-app.config");

            //get writer
            IConfigurations configs = new CommonAppWriter();

            //write our value
            configs.WriteValue(file, field, value);

            //retrieve our value
            string result = configs.GetValue(file, field);

            Assert.AreEqual(value, result);
        }

    }
}
