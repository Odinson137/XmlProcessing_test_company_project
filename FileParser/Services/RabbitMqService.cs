using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace FileParser.Services;

public class RabbitMqService
{
    private readonly ILogger<RabbitMqService> _logger;
    private readonly IConfiguration _configuration;
    public RabbitMqService()
    {
        ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddConsole();
            builder.AddDebug();
        });

        _logger = loggerFactory.CreateLogger<RabbitMqService>();
        
        var currentDirectory = Directory.GetCurrentDirectory();
        var appsettingsPath = Path.Combine(currentDirectory, "..", "..", "..", "..", "appsettings.json");
        
        _configuration = new ConfigurationBuilder().AddJsonFile(appsettingsPath).Build();
    }
    public void SendMessage<T>(T obj)
    {
        var content = JsonConvert.SerializeObject(obj);
        try
        {
            SendMessage(content);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Ошибка при отправке данных в RabbtiMq: {ex.Message}");
            throw new Exception("Stop server");
        }
        
    }

    void SendMessage(string message)
    {
        var factory = new ConnectionFactory() { HostName = _configuration["RabbitMQ:HostName"] }; 
        using var connection = factory.CreateConnection();
        using (var channel = connection.CreateChannel())
        {
            channel.QueueDeclare(queue: _configuration["RabbitMQ:QueueName"],
                durable: false,
                exclusive: false, 
                autoDelete: false,
                arguments: null);

            var body = Encoding.UTF8.GetBytes(message);
            
            channel.BasicPublish(exchange: "",
                routingKey: _configuration["RabbitMQ:QueueName"],
                body: body);
        };
        
    }
}