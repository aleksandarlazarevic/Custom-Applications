using System.Xml;
using System.Xml.Linq;

namespace ConsoleRunner.Xml
{
    public class XmlTests
    {
        public static void TestConversions() 
        {
            string test = "<body><head>test header</head></body>";
            XmlDocument xml = XmlHelper.StringToXml(test);
            string stringFromXml = XmlHelper.XmlToString(xml);
            string content = XmlHelper.GetNodeValue(xml, "head");

        }
    }
}
