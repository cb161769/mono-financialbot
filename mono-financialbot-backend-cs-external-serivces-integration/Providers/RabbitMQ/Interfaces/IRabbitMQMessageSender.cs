

namespace mono_financialbot_backend_cs_external_serivces.Providers.RabbitMQ.Interfaces
{
    public interface IRabbitMQMessageSender
    {
        void SendMessage(RabbitMQMessage message);
        void CreateConnection();
        bool ConnectionExists();
    }
}
