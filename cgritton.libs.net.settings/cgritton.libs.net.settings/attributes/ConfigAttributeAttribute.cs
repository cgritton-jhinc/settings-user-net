// ***********************************************************************
// Assembly         : cgritton.libs.net.settings
// Author           : cgritton-jhinc
// Created          : 07-31-2017
//
// Last Modified By : cgritton-jhinc
// Last Modified On : 07-31-2017
// ***********************************************************************
// <copyright file="ConfigAttributeAttribute.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Text.RegularExpressions;

/// <summary>
/// The configuration namespace.
/// </summary>
namespace cgritton.libs.net.settings
{
    /// <summary>
    /// Class XmlConfigAttribute.
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [System.AttributeUsage(AttributeTargets.Class)]
    public class ConfigAttributeAttribute : System.Attribute
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="xmlconfigattribute" /> class.
        /// </summary>
        /// <param name="rootelem">The root element.</param>
        /// <param name="settingselem">The settings element.</param>
        public ConfigAttributeAttribute(string rootelem, string settingselem)
        {
            root = rootelem;
            settings = settingselem;
        }

        /// <summary>
        /// The root
        /// </summary>
        private string root = "cgritton.libs.net.settings";
        /// <summary>
        /// The settings
        /// </summary>
        private string settings = "global";

        /// <summary>
        /// Gets the root element.
        /// </summary>
        /// <value>The root element.</value>
        public string RootElement
        {
            get
            {
                return root;
            }
        }

        /// <summary>
        /// Gets the settings element.
        /// </summary>
        /// <value>The settings element.</value>
        public string SettingsElement
        {
            get
            {
                return settings;
            }
        }

    }
}
