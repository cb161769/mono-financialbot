namespace mono_financialbot_backend_cs_external_serivces.Providers.Bot.Services
{
    public class BotMessageReciever : BackgroundService, IRabbitMQReciever
    {
        private readonly string _hostname;
        private readonly string _listenToQueueName;
        private readonly string _username;
        private readonly string _password;
        private readonly IHubContext<ChatService> _chat;
        private IConnection _connection;
        private IModel _channel;
        private ILogger<BotMessageReciever> _logger;
        public Task HandleMessage(RabbitMQMessage message)
        {
            throw new NotImplementedException();
        }

        public void InitializeListeners()
        {
            throw new NotImplementedException();
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            throw new NotImplementedException();
        }
    }
}
