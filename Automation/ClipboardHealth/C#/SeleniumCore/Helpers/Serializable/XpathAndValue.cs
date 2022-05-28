using System;
using System.Xml.Serialization;

namespace SeleniumCore.Helpers.Serializable
{
    [Serializable]
    public class XpathAndValue
    {
        #region Properties
        [XmlAttribute("xpath")]
        public string Xpath { get; set; }

        [XmlAttribute("value")]
        public string Value { get; set; }
        #endregion
    }
}
