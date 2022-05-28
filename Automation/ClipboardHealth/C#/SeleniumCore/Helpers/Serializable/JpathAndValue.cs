using System;
using System.Xml.Serialization;

namespace SeleniumCore.Helpers.Serializable
{
    [Serializable]
    public class JpathAndValue
    {
        #region Properties
        [XmlAttribute("jpath")]
        public string Jpath { get; set; }

        [XmlAttribute("value")]
        public string Value { get; set; }
            #endregion
}
}
