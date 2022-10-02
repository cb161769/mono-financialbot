

namespace mono_financialbot_backend_cs_external_serivces.Providers.RabbitMQ.Services
{
    public class RabbitMQService : IRabbitMQMessageSender
    {
        private readonly string _hostname;
        private readonly string _queueName;
        private readonly string _password;
        private readonly string _username;
        private readonly IConnection _connection;
        public RabbitMQService(IOptions<RabbitMQConfiguration> options)
        {
            _hostname = options.Value.HostName;
            _queueName = options.Value.QueueName;
            _password = options.Value.Password;
            _username = options.Value.UserName;
            CreateConnection();
        }

        public bool ConnectionExists()
        {
            throw new NotImplementedException();
        }

        public void CreateConnection()
        {
            try
            {

            }
            catch (Exception e)
            {

                throw;
            }
        }

        public void SendMessage(RabbitMQMessage message)
        {
            throw new NotImplementedException();
        }
    }
}
