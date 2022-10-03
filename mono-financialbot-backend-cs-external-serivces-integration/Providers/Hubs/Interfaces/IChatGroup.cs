namespace mono_financialbot_backend_cs_external_serivces.Providers.Hubs.Interfaces
{
    public interface IChatGroup
    {
        void AddToCurrentMessage(RabbitMQMessage message);
        Task SendMessage(RabbitMQMessage message);
        Task Add(string group);
        void Save(RabbitMQMessage message);
        Task Disconnect(string group);

    }
}
