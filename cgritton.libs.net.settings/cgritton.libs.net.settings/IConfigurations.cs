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
    public interface IConfigurations
    {

        /// <summary>
        /// Writes the value.
        /// </summary>
        /// <param name="output">The output.</param>
        /// <param name="field">The field.</param>
        /// <param name="value">The value.</param>
        void WriteValue(System.IO.FileInfo output, string field, string value);
        /// <summary>
        /// Writes the value.
        /// </summary>
        /// <param name="output">The output.</param>
        /// <param name="section">The section.</param>
        /// <param name="field">The field.</param>
        /// <param name="value">The value.</param>
        void WriteValue(System.IO.FileInfo output, string section, string field, string value);
        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="field">The field.</param>
        /// <returns>System.String.</returns>
        string GetValue(System.IO.FileInfo input, string field);
        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="section">The section.</param>
        /// <param name="field">The field.</param>
        /// <returns>System.String.</returns>
        string GetValue(System.IO.FileInfo input, string section, string field);
        /// <summary>
        /// Deletes the section.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="section">The section.</param>
        void DeleteSection(System.IO.FileInfo input, string section);
        /// <summary>
        /// Gets the section keys.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="section">The section.</param>
        /// <returns>List&lt;System.String&gt;.</returns>
        List<string> GetSectionKeys(System.IO.FileInfo input, string section);
        /// <summary>
        /// Gets the sections.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>List&lt;System.String&gt;.</returns>
        List<string> GetSections(System.IO.FileInfo input);

    }
}
