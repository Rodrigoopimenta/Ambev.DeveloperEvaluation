using Ambev.DeveloperEvaluation.Application.Common.Messaging;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Infrastructure.Messaging
{
    public class RabbitMqMessagePublisher : IMessagePublisher
    {
        private readonly ConnectionFactory _factory;

        public RabbitMqMessagePublisher(IConfiguration configuration)
        {
            _factory = new ConnectionFactory
            {
                HostName = configuration["RabbitMQ:Host"] ?? "localhost",
                UserName = configuration["RabbitMQ:User"] ?? "guest",
                Password = configuration["RabbitMQ:Password"] ?? "guest"
            };
        }

        public Task PublishAsync<T>(string queueName, T message)
        {
            using var connection = _factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false);

            var json = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(json);

            channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);

            return Task.CompletedTask;
        }
    }
}
