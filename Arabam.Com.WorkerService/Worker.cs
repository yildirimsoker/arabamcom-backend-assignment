
using Arabam.Com.Application.AdvertVisits.Commands.CreateAdvertVisit;
using MediatR;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Arabam.Com.WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        ConnectionFactory _factory;
        IConnection _conn;
        IModel _channel;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IConfiguration _configuration;
        private readonly int port;
        private readonly string hostName;
        private readonly string password;
        private readonly string userName;
        private readonly string queue;


        public Worker(ILogger<Worker> logger, IConfiguration configuration, IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
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

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                var consumer = new EventingBasicConsumer(_channel);

                consumer.Received += async (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    string message = Encoding.UTF8.GetString(body);

                    if (!string.IsNullOrEmpty(message))
                    {
                        CreateAdvertVisitCommand? createAdvertVisitCommand = JsonConvert.DeserializeObject<CreateAdvertVisitCommand>(message);

                        if (createAdvertVisitCommand != null)
                        {
                            await mediator.Send(createAdvertVisitCommand);
                        }
                    }
                };

                _channel.BasicConsume(queue: queue,
                                        autoAck: true,
                                        consumer: consumer);

                await Task.Delay(1000, stoppingToken);

            }
        }
    }
}