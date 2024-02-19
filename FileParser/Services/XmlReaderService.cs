using System.Net;
using System.Xml.Serialization;
using FileParser.Dto;
using FileParser.Models;
using Microsoft.Extensions.Logging;

namespace FileParser.Services;

public class XmlReaderService
{
    private const string url = "../../../wwwroot/status.xml";

    private static readonly ILogger<XmlReaderService> _logger;
    static XmlReaderService()
    {
        ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddConsole();
            builder.AddDebug();
        });

        _logger = loggerFactory.CreateLogger<XmlReaderService>();
    }
    
    public static ModuleStateDto[] ReadingXml()
    {
        try
        {
            var mainFile = DeserializeFromXml<InstrumentStatus>();

            ModuleStateDto[] status =
            {
                new()
                {
                    ModuleCategoryID = mainFile.DeviceStatus[0].ModuleCategoryID,
                    ModuleState = DeserializeFromXml<CombinedSamplerStatus>(mainFile.DeviceStatus[0].RapidControlStatus)
                        .ModuleState,
                },
                new()
                {
                    ModuleCategoryID = mainFile.DeviceStatus[1].ModuleCategoryID,
                    ModuleState = DeserializeFromXml<CombinedPumpStatus>(mainFile.DeviceStatus[1].RapidControlStatus)
                        .ModuleState,
                },
                new()
                {
                    ModuleCategoryID = mainFile.DeviceStatus[2].ModuleCategoryID,
                    ModuleState = DeserializeFromXml<CombinedOvenStatus>(mainFile.DeviceStatus[2].RapidControlStatus)
                        .ModuleState,
                }
            };

            return status;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Ошибка при десерилизации: {ex.Message}");
        }

        throw new Exception("Stop server");
    } 
    
    static T DeserializeFromXml<T>()
    {
        var serializer = new XmlSerializer(typeof(T));
        using (var fileStream = new FileStream(url, FileMode.Open))
        {
            return (T)serializer.Deserialize(fileStream);
        }
    }
    
    static T DeserializeFromXml<T>(string xmlString)
    {
        var serializer = new XmlSerializer(typeof(T));
        using (var stringReader = new StringReader(xmlString))
        {
            return (T)serializer.Deserialize(stringReader);
        }
    }
}