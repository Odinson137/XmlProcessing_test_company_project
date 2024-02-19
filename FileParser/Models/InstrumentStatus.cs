using System.Xml.Serialization;

namespace FileParser.Models;

[XmlRoot("InstrumentStatus")]
public class InstrumentStatus
{
    [XmlElement("PackageID")]
    public string PackageID { get; set; }

    [XmlElement("DeviceStatus")]
    public DeviceStatus[] DeviceStatus { get; set; }
}