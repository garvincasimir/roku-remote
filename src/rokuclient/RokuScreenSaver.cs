using System.Xml.Serialization;

namespace RokuDeviceLib
{
    public class RokuScreenSaver
    {
        [XmlAttribute("id")]
        public string Id {get;set;}

        [XmlAttribute("type")]
        public string Type {get;set;}

        [XmlAttribute("version")]
        public string Version {get;set;}

        [XmlText]
        public string Name {get;set;}
    }
}