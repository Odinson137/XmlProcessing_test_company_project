using System.Xml.Serialization;
using FileParser.Models;

[XmlRoot("CombinedOvenStatus")]
public class CombinedOvenStatus : CombinedStatus
{
    [XmlElement("IsBusy")]
    public bool IsBusy { get; set; }

    [XmlElement("IsReady")]
    public bool IsReady { get; set; }

    [XmlElement("IsError")]
    public bool IsError { get; set; }

    [XmlElement("KeyLock")]
    public bool KeyLock { get; set; }

    [XmlElement("UseTemperatureControl")]
    public bool UseTemperatureControl { get; set; }

    [XmlElement("OvenOn")]
    public bool OvenOn { get; set; }

    [XmlElement("Temperature_Actual")]
    public double Temperature_Actual { get; set; }

    [XmlElement("Temperature_Room")]
    public double Temperature_Room { get; set; }

    [XmlElement("MaximumTemperatureLimit")]
    public int MaximumTemperatureLimit { get; set; }

    [XmlElement("Valve_Position")]
    public int Valve_Position { get; set; }

    [XmlElement("Valve_Rotations")]
    public int Valve_Rotations { get; set; }

    [XmlElement("Buzzer")]
    public bool Buzzer { get; set; }
}