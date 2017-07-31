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
    [XmlConfig("cgritton.libs.net.settings.common-app", "system")]
    public class CommonAppWriter : XmlConfigWriter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommonAppWriter"/> class.
        /// </summary>
        public CommonAppWriter()
        {

        }
    }
}
