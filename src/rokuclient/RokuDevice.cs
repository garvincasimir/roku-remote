using System.Xml.Serialization;

namespace RokuDeviceLib
{
    [XmlRoot("device-info")]
    public class RokuDevice
    {
        [XmlElement("model-name")]
        public string ModelName {get;set;}

        [XmlElement("serial-number")]
        public string SerialNumber {get;set;}

        public string Endpoint {get;set;}
    }
}