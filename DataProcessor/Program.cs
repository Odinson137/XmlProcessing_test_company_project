using System.Text;
using DataProcessor.Dto;
using DataProcessor.Services;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

// initial
var databaseManager = new DatabaseManager();
databaseManager.CreateDatabaseAndTable();

var currentDirectory = Directory.GetCurrentDirectory();
var appsettingsPath = Path.Combine(currentDirectory, "..", "..", "..", "..", "appsettings.json");
        
var configuration = new ConfigurationBuilder().AddJsonFile(appsettingsPath).Build();

// rabbit mq
var factory = new ConnectionFactory() { HostName = configuration["RabbitMQ:HostName"] };
using var connection = factory.CreateConnection();
using var channel = connection.CreateChannel();

channel.QueueDeclare(queue: configuration["RabbitMQ:QueueName"],
    durable: false,
    exclusive: false, 
    autoDelete: false,
    arguments: null);

var consumer = new EventingBasicConsumer(channel);


// waiting
consumer.Received += (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine($"Received message: {message}");

    var modelStates = JsonConvert.DeserializeObject<ModuleStateDto[]>(message);

    foreach (var state in modelStates)
    {
        databaseManager.InsertOrUpdateModuleState(state);
    }
};

channel.BasicConsume(queue: configuration["RabbitMQ:QueueName"], autoAck: true, consumer: consumer);

Console.WriteLine("Нажмите любую клавишу для выхода");
Console.ReadLine();