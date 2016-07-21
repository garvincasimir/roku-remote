using System.Xml.Serialization;

namespace RokuDeviceLib
{
    [XmlRoot("active-app")]
    public class RokuActiveApp
    {
        [XmlElement("app")]
        public RokuApp App {get;set;}

        [XmlElement("screensaver")]
        public RokuScreenSaver ScreenSaver {get;set;}
    }
}