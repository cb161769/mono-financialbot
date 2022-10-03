


namespace mono_financialbot_backend_cs_external_serivces.Providers.RabbitMQ.Services
{
    public class RabbitMQRecieverService :  BackgroundService, IRabbitMQReciever
    {
        private readonly string _hostname;
        private readonly string _listenToQueueName;
        private readonly string _username;
        private readonly string _password;
        private readonly IHubContext<ChatService> _chat;
        private IConnection _connection;
        private IModel _channel;
        private ILogger<RabbitMQRecieverService> _logger;
        public RabbitMQRecieverService(IOptions<RabbitMQConfiguration> options, IHubContext<ChatService> chat,ILogger<RabbitMQRecieverService> logger)
        {
            _hostname = options.Value.HostName;
            _listenToQueueName = options.Value.ListenToQueueName;
            _password = options.Value.Password;
            _username = options.Value.UserName;
            _logger = logger;
            _chat = chat;
            InitializeListeners();
        }
        public async Task HandleMessage(RabbitMQMessage message)
        {
            try
            {
                _logger.LogInformation("start process to handle rabbitMQ messages");
                await _chat.Clients.All.SendAsync(Events.NewMessage, message);
                _logger.LogInformation("message sent  to rabbitMQ succesfully");

            }
            catch (Exception e)
            {
                _logger.LogError("an internal error has occurred wile handle rabbitMQ event message, error", e);
                throw new Exception(e.Message);

            }
        }

        public void InitializeListeners()
        {
            try
            {
                _logger.LogInformation("start process to initialize rabbitMQ listeners");
                var connection = new ConnectionFactory
                {

                    HostName = _hostname,
                    UserName = _username,
                    Password = _password

                };
                _connection = connection.CreateConnection();
                _logger.LogInformation("rabbitMQ connection succesfully established");
                _channel = _connection.CreateModel();
                _logger.LogInformation("rabbitMQ channel succesfully created");
                _channel.QueueDeclare(queue: _listenToQueueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
                _logger.LogInformation("rabbitMQ queue succesfully declared");

            }
            catch (Exception e )
            {
                _logger.LogError("an internal error has occurred wile initialiing rabbitMQ event listeners, error", e);
                throw new Exception(e.Message);
            }
        }

        protected override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("start process to Execute Background service");

                cancellationToken.ThrowIfCancellationRequested();
                var consumer = new EventingBasicConsumer(_channel);
                _logger.LogInformation("instance of rabbitMQ channel consumer succesful");
                consumer.Received += async (channel, payload) =>
               {
                   _logger.LogInformation("event chained, extracting content");
                   var content = Encoding.UTF8.GetString(payload.Body.ToArray());

                   var clientMessage = JsonConvert.DeserializeObject<RabbitMQMessage>(content);

                   _logger.LogInformation("event chained, extraction was succesfull, proceed to invoke handleMessage method");
                   await HandleMessage(clientMessage);
                   _logger.LogInformation("event chained, message susccesfully accomplished");
                   _channel.BasicAck(payload.DeliveryTag, false);
                   _logger.LogInformation("event chained, message susccesfully sent and acknowledged");

               };
                _channel.BasicConsume(_listenToQueueName, false, consumer);

                return Task.CompletedTask;
            }
            catch (Exception e)
            {

                _logger.LogError("an internal error has occurred wile initialiing rabbitMQ event listeners, error", e);
                throw new Exception(e.Message);
            }
    
        }
    }
}
