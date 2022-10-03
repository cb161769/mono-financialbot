

namespace mono_financialbot_backend_cs_external_serivces.Providers.RabbitMQ.Services
{
    public class RabbitMQService : IRabbitMQMessageSender
    {
        private readonly string _hostname;
        private readonly string _queueName;
        private readonly string _password;
        private readonly string _username;
        private  IConnection _connection;
        private readonly ILogger<RabbitMQService> _logger;
        public RabbitMQService(IOptions<RabbitMQConfiguration> options, ILogger<RabbitMQService> logger)
        {
            _hostname = options.Value.HostName;
            _queueName = options.Value.QueueName;
            _password = options.Value.Password;
            _username = options.Value.UserName;
            _logger = logger;
            CreateConnection();
        }

        public bool ConnectionExists()
        {
            try
            {
                if (_connection.IsOpen)
                {
                    _logger.LogInformation("rabbitMQ connection is still open and usable, proceed to return true ");
                    return true;
                }
                else
                {
                    _logger.LogWarning("the rabbitMQ connection is not open, proceed to establish a new rabbitMQ connection");
                    CreateConnection();
                }
                return _connection.IsOpen;
            }
            catch (Exception e)
            {
                _logger.LogError("an internal error has occurred wile checking if the connection was available, error", e);
                throw new Exception(e.Message);
            }
        }

        public void CreateConnection()
        {
            try
            {
                _logger.LogInformation("initializing ConnectionFactory class to instance a RabbitMQ connection");
                var connection = new ConnectionFactory{
                    HostName = _hostname,
                    UserName = _username,
                    Password = _password
                };
                _connection = connection.CreateConnection();
                _logger.LogInformation("rabbit MQ connection succesfully estabished, details:",connection);

            }
            catch (Exception e)
            {
                _logger.LogError("an internal error has occured wile estabilishg rabbitMQ connection, details:", e.ToString());
            }
        }

        public void SendMessage(RabbitMQMessage message)
        {
            try
            {
                _logger.LogInformation("start operation to send a rabbitMQ message {message}");
                if (ConnectionExists())
                {
                    _logger.LogInformation("rabbitMQ connection is open, proceed to send a rabbitMQ message");
                    using (var channel = _connection.CreateModel())
                    {

                        channel.QueueDeclare(queue: _queueName,durable:false, exclusive:false,autoDelete:true);
                        _logger.LogInformation("rabbitMQ queue declared, starting preparations to send rabbit MQ message trough the declared rabbitMQ queue");
                        var json = JsonConvert.SerializeObject(message);
                        var MessageBody = Encoding.UTF8.GetBytes(json);

                        channel.BasicPublish(exchange: "", routingKey: _queueName, basicProperties: null, body: MessageBody);

                    }
                }
                else
                {
                    _logger.LogWarning("rabbitMQ connection could not open, is it not possible to send the rabbitMQ message");
                }
            }
            catch (Exception e)
            {

                _logger.LogError("an internal error has occured wile sending  arabbitMQ message, details:", e.ToString());
            }
        }
    }
}
