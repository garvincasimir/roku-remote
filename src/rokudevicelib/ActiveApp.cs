using System.Xml.Serialization;

namespace RokuDeviceLib;

[XmlRoot("active-app")]
public class ActiveApp
{
    [XmlElement("app")]
    public App App {get;set;}

    [XmlElement("screensaver")]
    public ScreenSaver ScreenSaver {get;set;}
}
