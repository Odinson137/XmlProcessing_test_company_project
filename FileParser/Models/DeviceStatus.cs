﻿using System.Xml.Serialization;

namespace FileParser.Models;

public class DeviceStatus
{
    [XmlElement("ModuleCategoryID")]
    public string ModuleCategoryID { get; set; }

    [XmlElement("IndexWithinRole")]
    public int IndexWithinRole { get; set; }

    [XmlElement("RapidControlStatus")]
    public string RapidControlStatus { get; set; }
}