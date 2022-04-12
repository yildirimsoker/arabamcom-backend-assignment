using Arabam.Com.Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System.Text;

namespace Arabam.Com.Infrastructure.RabbitMQ
{
    public class MessageService : IMessageService
    {
        ConnectionFactory _factory;
        IConnection _conn;
        IModel _channel;
        private readonly IConfiguration _configuration;
        private readonly int port;
        private readonly string hostName;
        private readonly string password;
        private readonly string userName;
        private readonly string queue;

        public MessageService(IConfiguration configuration)
        {
            _configuration = configuration;
             port = Convert.ToInt32(_configuration["RabbitMQ:Port"]);
             hostName = _configuration["RabbitMQ:HostName"];
             userName = configuration["RabbitMQ:UserName"];
             password = configuration["RabbitMQ:Password"];
             queue = configuration["RabbitMQ:Queue"];

            _factory = new ConnectionFactory() { HostName = hostName, Port = port };
            _factory.UserName = userName;
            _factory.Password = password;
            _conn = _factory.CreateConnection();
            _channel = _conn.CreateModel();
            _channel.QueueDeclare(queue: queue,
                                    durable: false,
                                    exclusive: false,
                                    autoDelete: false,
                                    arguments: null);
        }
        public bool Enqueue(string messageString)
        {
            var body = Encoding.UTF8.GetBytes(messageString);
            _channel.BasicPublish(exchange: "",
                                routingKey: queue,
                                basicProperties: null,
                                body: body);
            return true;
        }
    }
}
