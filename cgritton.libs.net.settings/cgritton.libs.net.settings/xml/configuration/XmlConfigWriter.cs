// ***********************************************************************
// Assembly         : cgritton.libs.net.settings
// Author           : cgritton-jhinc
// Created          : 07-31-2017
//
// Last Modified By : cgritton-jhinc
// Last Modified On : 07-31-2017
// ***********************************************************************
// <copyright file="XmlConfigWriter.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

/// <summary>
/// The configuration namespace.
/// </summary>
namespace cgritton.libs.net.settings.xml.configuration
{
    /// <summary>
    /// Class XmlConfigWriter.
    /// </summary>
    [ConfigAttribute("configuration", "settings")]
    public abstract class XmlConfigWriter : IAppConfigurations
    {
        /// <summary>
        /// The source
        /// </summary>
        private System.IO.FileInfo source = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="xmlconfigwriter" /> class.
        /// </summary>
        public XmlConfigWriter(System.IO.FileInfo settingsfile)
        {
            //default constructor
            if (settingsfile == null)
            {
                throw new System.InvalidOperationException("You must supply a settings file even if the file does not currently exist.");
            }
            source = settingsfile;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="xmlconfigwriter" /> class.
        /// </summary>
        /// <param name="rootelement">The rootelement.</param>
        /// <param name="settingselement">The settingselement.</param>
        /// <remarks>Overrides settings that are applied in the class xmlconfigattribute attribute.</remarks>
        public XmlConfigWriter(System.IO.FileInfo settingsfile, string rootelement, string settingselement) : this(settingsfile)
        {
            _rootelem = rootelement;
            _settingselem = settingselement;
        }

        /// <summary>
        /// The rootelem
        /// </summary>
        private string _rootelem = string.Empty;
        /// <summary>
        /// The settingselem
        /// </summary>
        private string _settingselem = string.Empty;

        /// <summary>
        /// Gets the getrootname.
        /// </summary>
        /// <value>The getrootname.</value>
        private string getrootname
        {
            get
            {
                if (!string.IsNullOrEmpty(_rootelem))
                {
                    return _rootelem.SanitizeForXml(); 
                }
                System.Reflection.MemberInfo info = GetType();
                object[] attributes = info.GetCustomAttributes(true);
                for (int i = 0; i < attributes.Length; i++)
                {
                    if (attributes[i] is ConfigAttributeAttribute)
                    {
                        ConfigAttributeAttribute thisatt = (ConfigAttributeAttribute)attributes[i];
                        _rootelem = thisatt.RootElement;
                        return _rootelem.SanitizeForXml(); ;
                    }
                }

                return "configuration";
            }
        }

        /// <summary>
        /// Gets the getsettingsname.
        /// </summary>
        /// <value>The getsettingsname.</value>
        private string getsettingsname
        {
            get
            {
                if (!string.IsNullOrEmpty(_settingselem))
                {
                    return _settingselem.SanitizeForXml();
                }
                System.Reflection.MemberInfo info = GetType();
                object[] attributes = info.GetCustomAttributes(true);
                for (int i = 0; i < attributes.Length; i++)
                {
                    if (attributes[i] is ConfigAttributeAttribute)
                    {
                        ConfigAttributeAttribute thisatt = (ConfigAttributeAttribute)attributes[i];
                        _settingselem = thisatt.SettingsElement;
                        return _settingselem.SanitizeForXml();
                    }
                }

                return "settings";
            }
        }

        /// <summary>
        /// Writes the value.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <param name="value">The value.</param>
        public void WriteValue(string field, string value)
        {
            try
            {
                //create a base document
                System.Xml.XmlDocument xmldoc = new System.Xml.XmlDocument();

                if (source.Exists)
                {
                    try
                    {
                        //load existing file
                        xmldoc.Load(source.FullName);
                    }
                    catch (Exception e)
                    {
                        System.Diagnostics.Trace.TraceError(e.Message + "\n" + e.StackTrace);
                        //load default xml
                        xmldoc.LoadXml(@"<!--You should use caution when editing manually. Elements and attributes are case sensitive.--><" + getrootname + "><" + getsettingsname + " /></" + getrootname + ">");
                    }
                }
                else
                {
                    //load default xml
                    xmldoc.LoadXml(@"<!--You should use caution when editing manually. Elements and attributes are case sensitive.--><" + getrootname + "><" + getsettingsname + " /></" + getrootname + ">");
                }

                //get root element
                System.Xml.XmlElement root = xmldoc.DocumentElement;
                //find the settings element
                System.Xml.XmlNode settings = root.SelectSingleNode(@"/" + getrootname + "/" + getsettingsname);

                //verify we have the node created
                if (settings == null)
                {

                    //first get root node if it exists
                    System.Xml.XmlNode ds = root.SelectSingleNode(@"/" + getrootname);
                    if (ds == null)
                    {

                        //create document backup in case user modified incorrectly
                        string newname = source.FullName.Replace(source.Extension, ".bak");
                        int rc = 1;
                        do
                        {
                            //find new file name
                            if (System.IO.File.Exists(newname))
                            {
                                //create incremented new name
                                newname = source.FullName.Replace(source.Extension, "_copy(" + rc.ToString() + ").bak");
                            }

                            rc++;

                        } while (System.IO.File.Exists(newname));

                        source.CopyTo(newname);

                        //invalid settngs, start fresh
                        xmldoc.LoadXml(@"<" + getrootname + "><" + getsettingsname + " /></" + getrootname + ">");
                        //get root element
                        root = xmldoc.DocumentElement;
                        //create comment node
                        System.Xml.XmlComment comment = xmldoc.CreateComment("You should use caution when editing manually. Elements and attributes are case sensitive.");
                        xmldoc.InsertBefore(comment, root);
                        //get now existing root element
                        ds = root.SelectSingleNode(@"/" + getrootname);
                    }
                    //create new settings node and append
                    System.Xml.XmlElement newSettings = xmldoc.CreateElement(getsettingsname);
                    ds.AppendChild(newSettings);
                    //now re-intialize settings node since we now have added it
                    settings = root.SelectSingleNode(@"/" + getrootname + "/" + getsettingsname);
                }

                //find the current node if it exists
                System.Xml.XmlNode curNode = settings.SelectSingleNode("add[@name = '" + field + "']");

                //create new add element
                System.Xml.XmlElement newNode = xmldoc.CreateElement("add");
                //assign attribute values
                newNode.SetAttribute("name", field);
                newNode.SetAttribute("value", value);

                if (curNode != null)
                {
                    //since we found it replace existing with new value
                    settings.ReplaceChild(newNode, curNode);
                }
                else
                {
                    //we didn't find it so add it
                    settings.AppendChild(newNode);
                }

                //save xml document to file
                xmldoc.Save(source.FullName);
                source.Refresh();

            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.TraceError(e.Message + "\n" + e.StackTrace);
                throw e;
            }

        }


        /// <summary>
        /// Writes the value.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="section">The section.</param>
        /// <param name="field">The field.</param>
        /// <param name="value">The value.</param>
        public void WriteValue(string section, string field, string value)
        {
            try
            {
                //create a base document
                System.Xml.XmlDocument xmldoc = new System.Xml.XmlDocument();

                if (source.Exists)
                {
                    try
                    {
                        //load existing file
                        xmldoc.Load(source.FullName);
                    }
                    catch (Exception e)
                    {
                        System.Diagnostics.Trace.TraceError(e.Message + "\n" + e.StackTrace);
                        //load default xml
                        xmldoc.LoadXml(@"<!--You should use caution when editing manually. Elements and attributes are case sensitive.--><" + getrootname + "><" + getsettingsname + " /></" + getrootname + ">");
                    }
                }
                else
                {
                    //load default xml
                    xmldoc.LoadXml(@"<!--You should use caution when editing manually. Elements and attributes are case sensitive.--><" + getrootname + "><" + getsettingsname + " /></" + getrootname + ">");
                }

                //get root element
                System.Xml.XmlElement root = xmldoc.DocumentElement;
                //find the settings element
                System.Xml.XmlNode settings = root.SelectSingleNode(@"/" + getrootname + "/" + getsettingsname);

                //verify we have the node created
                if (settings == null)
                {

                    //first get root node if it exists
                    System.Xml.XmlNode ds = root.SelectSingleNode(@"/" + getrootname);
                    if (ds == null)
                    {

                        //create document backup in case user modified incorrectly
                        string newname = source.FullName.Replace(source.Extension, ".bak");
                        int rc = 1;
                        do
                        {
                            //find new file name
                            if (System.IO.File.Exists(newname))
                            {
                                //create incremented new name
                                newname = source.FullName.Replace(source.Extension, "_copy(" + rc.ToString() + ").bak");
                            }

                            rc++;

                        } while (System.IO.File.Exists(newname));

                        source.CopyTo(newname);

                        //invalid settngs, start fresh
                        xmldoc.LoadXml(@"<" + getrootname + "><" + getsettingsname + " /></" + getrootname + ">");
                        //get root element
                        root = xmldoc.DocumentElement;
                        //create comment node
                        System.Xml.XmlComment comment = xmldoc.CreateComment("You should use caution when editing manually. Elements and attributes are case sensitive.");
                        xmldoc.InsertBefore(comment, root);
                        //get now existing root element
                        ds = root.SelectSingleNode(@"/" + getrootname);
                    }
                    //create new settings node and append
                    System.Xml.XmlElement newSettings = xmldoc.CreateElement(getsettingsname);
                    ds.AppendChild(newSettings);
                    //now re-intialize settings node since we now have added it
                    settings = root.SelectSingleNode(@"/" + getrootname + "/" + getsettingsname);
                }

                //find the section element
                System.Xml.XmlNode cursection = settings.SelectSingleNode(section);

                if (cursection == null)
                {
                    cursection = xmldoc.CreateElement(section);
                    settings.AppendChild(cursection);
                }

                //find the current node if it exists
                System.Xml.XmlNode curNode = cursection.SelectSingleNode("add[@name = '" + field + "']");

                //create new add element
                System.Xml.XmlElement newNode = xmldoc.CreateElement("add");
                //assign attribute values
                newNode.SetAttribute("name", field);
                newNode.SetAttribute("value", value);

                if (curNode != null)
                {
                    //since we found it replace existing with new value
                    cursection.ReplaceChild(newNode, curNode);
                }
                else
                {
                    //we didn't find it so add it
                    cursection.AppendChild(newNode);
                }

                //save xml document to file
                xmldoc.Save(source.FullName);
                source.Refresh();
            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.TraceError(e.Message + "\n" + e.StackTrace);
                throw e;
            }

        }



        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <returns>System.String.</returns>
        public string GetValue(string field)
        {
            string defaultval = String.Empty;
            try
            {
                //create a base document
                System.Xml.XmlDocument xmldoc = new System.Xml.XmlDocument();

                if (source.Exists)
                {
                    //load existing file
                    xmldoc.Load(source.FullName);

                    //get root element
                    System.Xml.XmlElement root = xmldoc.DocumentElement;

                    //find the settings element
                    System.Xml.XmlNode settings = root.SelectSingleNode(@"/" + getrootname + "/" + getsettingsname);

                    if (settings != null)
                    {
                        //find the current node if it exists
                        System.Xml.XmlNode curNode = settings.SelectSingleNode("add[@name = '" + field + "']");
                        if (curNode != null)
                        {
                            System.Xml.XmlNode attNode = curNode.SelectSingleNode("@value");
                            if (attNode != null)
                            {
                                defaultval = attNode.Value;
                            }
                        }
                    }

                }

            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.TraceError(e.Message + "\n" + e.StackTrace);
                //throw e;
            }
            return defaultval;
        }


        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="section">The section.</param>
        /// <param name="field">The field.</param>
        /// <returns>System.String.</returns>
        public string GetValue(string section, string field)
        {
            string defaultval = String.Empty;
            try
            {
                //create a base document
                System.Xml.XmlDocument xmldoc = new System.Xml.XmlDocument();

                if (source.Exists)
                {
                    //load existing file
                    xmldoc.Load(source.FullName);

                    //get root element
                    System.Xml.XmlElement root = xmldoc.DocumentElement;

                    //find the settings element
                    System.Xml.XmlNode settings = root.SelectSingleNode(@"/" + getrootname + "/" + getsettingsname + "/" + section);

                    if (settings != null)
                    {
                        //find the current node if it exists
                        System.Xml.XmlNode curNode = settings.SelectSingleNode("add[@name = '" + field + "']");
                        if (curNode != null)
                        {
                            System.Xml.XmlNode attNode = curNode.SelectSingleNode("@value");
                            if (attNode != null)
                            {
                                defaultval = attNode.Value;
                            }
                        }
                    }

                }

            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.TraceError(e.Message + "\n" + e.StackTrace);
                //throw e;
            }
            return defaultval;
        }

        /// <summary>
        /// Deletes the section.
        /// </summary>
        /// <param name="section">The section.</param>
        public void DeleteSection(string section)
        {
            try
            {
                //create a base document
                System.Xml.XmlDocument xmldoc = new System.Xml.XmlDocument();

                if (source.Exists)
                {
                    //load existing file
                    xmldoc.Load(source.FullName);

                    //get root element
                    System.Xml.XmlElement root = xmldoc.DocumentElement;

                    //find the settings element
                    System.Xml.XmlNode settings = root.SelectSingleNode(@"/" + getrootname + "/" + getsettingsname + "/" + section);

                    if (settings != null)
                    {
                        settings.ParentNode.RemoveChild(settings);
                        xmldoc.Save(source.FullName);
                        source.Refresh();
                    }

                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.TraceError(e.Message + "\n" + e.StackTrace);
                throw e;
            }
        }

        /// <summary>
        /// Gets the section keys.
        /// </summary>
        /// <param name="section">The section.</param>
        /// <returns>List&lt;System.String&gt;.</returns>
        public List<string> GetSectionKeys(string section)
        {
            List<string> keys = new List<string>();
            try
            {
                //create a base document
                System.Xml.XmlDocument xmldoc = new System.Xml.XmlDocument();

                if (source.Exists)
                {
                    //load existing file
                    xmldoc.Load(source.FullName);

                    //get root element
                    System.Xml.XmlElement root = xmldoc.DocumentElement;

                    //find the settings element
                    System.Xml.XmlNode settings = root.SelectSingleNode(@"/" + getrootname + "/" + getsettingsname + "/" + section);

                    if (settings != null)
                    {
                        //find the current node if it exists
                        System.Xml.XmlNodeList curNodes = settings.SelectNodes("add/@name");
                        if (curNodes != null)
                        {
                            foreach (System.Xml.XmlNode curNode in curNodes)
                            {
                                keys.Add(curNode.Value);
                            }
                        }
                    }

                }

            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.TraceError(e.Message + "\n" + e.StackTrace);
                throw e;
            }
            return keys;
        }

        /// <summary>
        /// Gets the sections.
        /// </summary>
        /// <returns>List&lt;System.String&gt;.</returns>
        public List<string> GetSections()
        {
            List<string> _sections = new List<string>();
            try
            {
                //create a base document
                System.Xml.XmlDocument xmldoc = new System.Xml.XmlDocument();

                if (source.Exists)
                {
                    //load existing file
                    xmldoc.Load(source.FullName);

                    //get root element
                    System.Xml.XmlElement root = xmldoc.DocumentElement;

                    //find the settings element
                    System.Xml.XmlNode settings = root.SelectSingleNode(@"/" + getrootname + "/" + getsettingsname);

                    if (settings != null)
                    {
                        //iterate child nodes to get sections from settings node
                        foreach (System.Xml.XmlNode child in settings.ChildNodes)
                        {
                            //add to list
                            _sections.Add(child.Name);
                        }
                    }

                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.TraceError(e.Message + "\n" + e.StackTrace);
                throw e;
            }
            //return sections
            return _sections;
        }

    }
}
