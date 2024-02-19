using System.Xml.Serialization;
using FileParser.Models;

[XmlRoot("CombinedPumpStatus")]
public class CombinedPumpStatus : CombinedStatus
{
    [XmlElement("IsBusy")]
    public bool IsBusy { get; set; }

    [XmlElement("IsReady")]
    public bool IsReady { get; set; }

    [XmlElement("IsError")]
    public bool IsError { get; set; }

    [XmlElement("KeyLock")]
    public bool KeyLock { get; set; }

    [XmlElement("Mode")]
    public string Mode { get; set; }

    [XmlElement("Flow")]
    public int Flow { get; set; }

    [XmlElement("PercentB")]
    public int PercentB { get; set; }

    [XmlElement("PercentC")]
    public int PercentC { get; set; }

    [XmlElement("PercentD")]
    public int PercentD { get; set; }

    [XmlElement("MinimumPressureLimit")]
    public int MinimumPressureLimit { get; set; }

    [XmlElement("MaximumPressureLimit")]
    public double MaximumPressureLimit { get; set; }

    [XmlElement("Pressure")]
    public int Pressure { get; set; }

    [XmlElement("PumpOn")]
    public bool PumpOn { get; set; }

    [XmlElement("Channel")]
    public int Channel { get; set; }
}