using System.Xml;
using System.Xml.Linq;

namespace ConsoleRunner.Xml
{
    public class XmlHelper
    {
        public static XmlDocument StringToXml(string xmlString)
        {
            if (!IsValidXml(xmlString))
            {
                throw new Exception("Not a valid XML");
            }

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xmlString);
            return xmlDocument;
        }

        public static string XmlToString(XmlDocument xmlString)
        {
            string output = xmlString.OuterXml;
            return output;
        }

        public static string GetNodeValue(XmlDocument xmlDocument, string nodeName)
        {
            XmlNodeList listOfNodes = GetNodesByName(xmlDocument, nodeName);
            string content = listOfNodes[0].InnerXml;
            return content;
        }

        private static XmlNodeList GetNodesByName(XmlDocument xmlDocument, string nodeName)
        {
            XmlNodeList xmlElements = xmlDocument.GetElementsByTagName(nodeName);
            return xmlElements;
        }

        private static List<XElement> GetGroupOfNodesFromXmlFile(XDocument xml, string nodeName)
        {
            IEnumerable<XElement> xmlElements = xml.Root.Elements();
            List<XElement> listOfNodes = xmlElements.Where(x => x.Name.LocalName.Contains(nodeName)).ToList();
            return listOfNodes;
        }
        private static bool IsValidXml(string xmlString)
        {
            try
            {
                XDocument.Parse(xmlString);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
