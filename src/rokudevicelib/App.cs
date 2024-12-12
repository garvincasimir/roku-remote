using System.Xml.Serialization;
using System.Collections.Generic;

namespace RokuDeviceLib;
    
public class App
{
    [XmlAttribute("id")]
    public string Id {get;set;}

    [XmlText]
    public string Name {get;set;}

    [XmlAttribute("type")]
    public string Type {get;set;}

    [XmlAttribute("version")]
    public string Version{get;set;}
}

[XmlRoot("apps")]
public class DeviceAppWrapper
{
    [XmlElement("app")]
    public List<App> Apps {get;set;}

}
