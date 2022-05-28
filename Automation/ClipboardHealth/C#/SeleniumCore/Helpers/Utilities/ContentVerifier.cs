using System;
using System.IO;
using System.Xml;
using Newtonsoft.Json.Linq;

namespace SeleniumCore.Helpers.Utilities
{
    public static class ContentVerifier
    {
        public static bool IsFileInXMLFormat(string filePath)
        {
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(filePath);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsStringInXMLFormat(string content)
        {
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(content);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsFileInJSONFormat(string filePath)
        {
            string content = File.ReadAllText(filePath);

            return IsThisValidJSON(content.Trim());
        }

        public static bool IsStringInJSONFormat(string content)
        {
            return IsThisValidJSON(content.Trim());
        }

        private static bool IsThisValidJSON(string content)
        {
            Func<bool> IsWellFormed = () =>
            {
                try
                {
                    JToken.Parse(content);
                    return true;
                }
                catch
                {
                    return false;
                }
            };

            return (content.StartsWith("{") && content.EndsWith("}")
                    || content.StartsWith("[") && content.EndsWith("]"))
                    && IsWellFormed();
        }
    }
}
