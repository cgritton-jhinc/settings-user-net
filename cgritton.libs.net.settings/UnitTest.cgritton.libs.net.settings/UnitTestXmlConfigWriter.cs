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
           //if (System.IO.Directory.Exists(testDir)) System.IO.Directory.Delete(testDir, true);
        }

        [TestMethod]
        public void WriteValueNoSectionExtension()
        {
            //set variables
            string field = "my-app-field";
            string value = "my-app-value";
            System.IO.FileInfo file = new System.IO.FileInfo(testDir + @"\global-app.config");

            //get writer
            IAppConfigurations configs = file.ToXmlFileWriter();

            //write our value
            configs.WriteValue(field, value);

            //retrieve our value
            string result = configs.GetValue(field);

            Assert.AreEqual(value, result);
        }

        [TestMethod]
        public void WriteValueNoSectionOverrideExtension()
        {
            //set variables
            string field = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
            string value = System.Reflection.MethodBase.GetCurrentMethod().Name;
            System.IO.FileInfo file = new System.IO.FileInfo(testDir + @"\local-app.config");

            //get writer
            IAppConfigurations configs = file.ToXmlFileWriter(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Namespace, @"unit-test");

            //write our value
            configs.WriteValue(field, value);

            //retrieve our value
            string result = configs.GetValue(field);

            Assert.AreEqual(value, result);
        }

    }
}
