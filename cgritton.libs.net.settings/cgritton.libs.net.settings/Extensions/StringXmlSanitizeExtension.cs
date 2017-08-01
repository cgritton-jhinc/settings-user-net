using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace System
{
    public static class StringXmlSanitizeExtension
    {
        public static string SanitizeForXml(this string source)
        {
            StringBuilder sb = new StringBuilder();

            foreach (char character in source)
            {
                //only allow alpha/numeric (not sure if others are allowed but this is what I am allowing)
                if ((Convert.ToInt32(character) >= 48 & Convert.ToInt32(character) <= 57) |
               (Convert.ToInt32(character) >= 65 & Convert.ToInt32(character) <= 90) |
               (Convert.ToInt32(character) >= 97 & Convert.ToInt32(character) <= 122))
                {
                    sb.Append(character);
                }
                else
                {
                    sb.Append("__x00" + Convert.ToInt32(character).ToString("x") + "_");
                }
            }

            return sb.ToString();
        }

        public static string FromXmlSanitzedString(this string source)
        {

            StringBuilder sb = new StringBuilder();

            Regex regex = new Regex("(?<REPLACE>__x00(?<HEX>[0-9A-Fa-f]*)_)");

            MatchCollection matches = regex.Matches(source);

            int index = 0;

            //iterate matches and look for success and find named groups
            foreach (Match m in matches)
            {
                if (m.Success)
                {
                    string replace = m.Groups["REPLACE"].Value;
                    string hex = m.Groups["HEX"].Value;

                    if (index < m.Groups["REPLACE"].Index)
                    {
                        //append in string in between groups
                        sb.Append(source.Substring(index, m.Groups["REPLACE"].Index - index));
                    }

                    //append ascii value for hex
                    int outhex;
                    if (int.TryParse(hex, Globalization.NumberStyles.HexNumber, null, out outhex))
                    {
                        sb.Append((char)outhex);
                    }
                    //increment index position
                    index = (m.Groups["REPLACE"].Index + m.Groups["REPLACE"].Length);

              }
            }

            if (index < source.Length - 1)
            {
                sb.Append(source.Substring(index, source.Length - index));
            }

            return sb.ToString();
        }
    }
}
