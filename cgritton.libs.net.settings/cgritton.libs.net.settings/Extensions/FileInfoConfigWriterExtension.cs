// ***********************************************************************
// Assembly         : cgritton.libs.net.settings
// Author           : cgritton-jhinc
// Created          : 07-31-2017
//
// Last Modified By : cgritton-jhinc
// Last Modified On : 07-31-2017
// ***********************************************************************
// <copyright file="FileInfoConfigWriterExtension.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using cgritton.libs.net.settings;

namespace System
{
    /// <summary>
    /// Class FileInfoConfigWriterExtension.
    /// </summary>
    public static class FileInfoConfigWriterExtension
    {
        /// <summary>
        /// To the XML file writer.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>IAppConfigurations.</returns>
        public static IAppConfigurations ToXmlFileWriter(this System.IO.FileInfo source)
        {
            //return our default xml file writer
            return new CommonAppXmlFileWriter(source);
        }

        /// <summary>
        /// To the XML file writer.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="rootelement">The rootelement.</param>
        /// <param name="settingselement">The settingselement.</param>
        /// <returns>IAppConfigurations.</returns>
        public static IAppConfigurations ToXmlFileWriter(this System.IO.FileInfo source, string rootelement, string settingselement)
        {
            //return our default xml file writer
            return new CommonAppXmlFileWriter(source, rootelement, settingselement);
        }

    }
}
