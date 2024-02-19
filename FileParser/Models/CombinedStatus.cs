using System.Xml.Serialization;

namespace FileParser.Models;

public abstract class CombinedStatus
{
    [XmlElement("ModuleState")]
    public string ModuleState { get; set; }
}