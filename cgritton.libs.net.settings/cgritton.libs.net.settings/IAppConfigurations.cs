// ***********************************************************************
// Assembly         : cgritton.libs.net.settings
// Author           : cgritton-jhinc
// Created          : 07-31-2017
//
// Last Modified By : cgritton-jhinc
// Last Modified On : 07-31-2017
// ***********************************************************************
// <copyright file="IConfigurations.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;

namespace cgritton.libs.net.settings
{
    /// <summary>
    /// Interface IConfigurations
    /// </summary>
    public interface IAppConfigurations
    {

        /// <summary>
        /// Writes the value.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <param name="value">The value.</param>
        void WriteValue(string field, string value);
        /// <summary>
        /// Writes the value.
        /// </summary>
        /// <param name="section">The section.</param>
        /// <param name="field">The field.</param>
        /// <param name="value">The value.</param>
        void WriteValue(string section, string field, string value);
        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <returns>System.String.</returns>
        string GetValue(string field);
        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="section">The section.</param>
        /// <param name="field">The field.</param>
        /// <returns>System.String.</returns>
        string GetValue(string section, string field);
        /// <summary>
        /// Deletes the section.
        /// </summary>
        /// <param name="section">The section.</param>
        void DeleteSection(string section);
        /// <summary>
        /// Gets the section keys.
        /// </summary>
        /// <param name="section">The section.</param>
        /// <returns>List&lt;System.String&gt;.</returns>
        List<string> GetSectionKeys(string section);
        /// <summary>
        /// Gets the sections.
        /// </summary>
        /// <returns>List&lt;System.String&gt;.</returns>
        List<string> GetSections();

    }
}
