using FileParser.Services;
using Microsoft.Extensions.Logging;

ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddConsole();
    builder.AddDebug();
});

var logger = loggerFactory.CreateLogger<Program>();

var xmlReadingThread = new Thread(ReadXmlInBackground);
xmlReadingThread.Start();

void ReadXmlInBackground()
{
    logger.LogInformation("Начало работы программы");
    while (true)
    {
        logger.LogInformation("Начало новой фазы цикла");
        var moduleStatesDto = XmlReaderService.ReadingXml();

        logger.LogInformation("Чтение из файла прошло успешно");
        ChangeRandomModuleState.ChangeStatus(moduleStatesDto);

        logger.LogInformation("Успешно изменены типы");
        var rabbitMqService = new RabbitMqService();
        rabbitMqService.SendMessage(moduleStatesDto);

        logger.LogInformation("Данные в бд успешно изменены");
        Thread.Sleep(1000);
    }
}
