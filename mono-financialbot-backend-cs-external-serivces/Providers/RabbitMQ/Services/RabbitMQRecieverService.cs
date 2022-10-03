

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
            _listenToQueueName = options.Value.QueueName;
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

            }
            catch (Exception e)
            {
                _logger.LogError("an internal error has occurred wile handle rabbitMQ event message, error", e);

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
            }
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {

            }
            catch (Exception e)
            {

                throw;
            }
            throw new NotImplementedException();
        }
    }
}
