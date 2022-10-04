namespace mono_financialbot_backend_cs_external_serivces.Providers.Bot.Services
{
    public class BotMessageReciever : BackgroundService, IRabbitMQReciever
    {
        private readonly string _hostname;
        private readonly string _listenToQueueName;
        private readonly string _username;
        private readonly string _password;
        private readonly IBot _bot;
        private readonly IRabbitMQMessageSender _sender;
        private IConnection _connection;
        private IModel _channel;
        private ILogger<BotMessageReciever> _logger;
        public BotMessageReciever(IOptions<RabbitMQConfiguration> rabbitMqOptions, IBot bot, IRabbitMQMessageSender sender)
        {
            _hostname = rabbitMqOptions.Value.HostName;
            _password = rabbitMqOptions.Value.Password;
            _username = rabbitMqOptions.Value.UserName;
            _listenToQueueName = rabbitMqOptions.Value.ListenToQueueName;
            _bot = bot;
            _sender = sender;
            InitializeListeners();
        }
        public Task HandleMessage(RabbitMQMessage message)
        {
            try
            {
                StockModel quote = _bot.GetStockQuote(message);

                if (quote != null)
                {
                    var msg = GetStockMessage(quote);
                    msg.group = message.group;
                    _sender.SendMessage(msg);
                }
                return null;
               
            }
            catch (Exception)
            {
                _sender.SendMessage(new RabbitMQMessage
                {
                    username = "#BOT",
                    sendedDateUtc = DateTime.Now,
                    Message = $"Could not get stock quote.",
                    group = message.group
                });
                throw;
            }
        }
        private RabbitMQMessage GetStockMessage(StockModel quote)
        {
            return new RabbitMQMessage
            {
                username = "#BOT",
                sendedDateUtc = DateTime.Now,
                Message = $"{quote.Symbol} quote is ${quote.Close} per share"
            };
        }

        public void InitializeListeners()
        {
            var factory = new ConnectionFactory
            {
                HostName = _hostname,
                UserName = _username,
                Password = _password
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: _listenToQueueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
            _logger.LogInformation("connected to rabbitMQ successfully");
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                var clientMessage = JsonConvert.DeserializeObject<RabbitMQMessage>(content);

                HandleMessage(clientMessage);

                _channel.BasicAck(ea.DeliveryTag, false);
            };

   

            _channel.BasicConsume(_listenToQueueName, false, consumer);

            return Task.CompletedTask;
        }
    }
}
