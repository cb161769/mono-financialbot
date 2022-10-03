namespace mono_financialbot_backend_cs_external_serivces.Providers.RabbitMQ.Interfaces
{
    public interface IRabbitMQReciever
    {

        void InitializeListeners();
        Task HandleMessage(RabbitMQMessage message);
    }
}
