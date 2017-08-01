// ***********************************************************************
// Assembly         : cgritton.libs.net.settings
// Author           : cgritton-jhinc
// Created          : 07-31-2017
//
// Last Modified By : cgritton-jhinc
// Last Modified On : 07-31-2017
// ***********************************************************************
// <copyright file="CommonAppWriter.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using cgritton.libs.net.settings.xml.configuration;

namespace cgritton.libs.net.settings
{
    /// <summary>
    /// Class CommonAppWriter.
    /// </summary>
    /// <seealso cref="cgritton.libs.net.settings.xml.configuration.XmlConfigWriter" />
    [ConfigAttribute("cgritton.libs.net.settings.common-app", "system")]
    public class CommonAppXmlFileWriter : XmlConfigWriter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommonAppXmlFileWriter"/> class.
        /// </summary>
        public CommonAppXmlFileWriter(System.IO.FileInfo settingsfile) : base(settingsfile)
        {
          
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommonAppXmlFileWriter"/> class.
        /// </summary>
        /// <param name="settingsfile">The settingsfile.</param>
        /// <param name="rootelement">The rootelement.</param>
        /// <param name="settingselement">The settingselement.</param>
        /// <remarks>Overrides settings that are applied in the class xmlconfigattribute attribute.</remarks>
        public CommonAppXmlFileWriter(System.IO.FileInfo settingsfile, string rootelement, string settingselement) : base(settingsfile, rootelement, settingselement)
        {

        }

    }
}
