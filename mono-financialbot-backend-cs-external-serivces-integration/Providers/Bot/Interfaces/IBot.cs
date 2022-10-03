

namespace mono_financialbot_backend_cs_external_serivces.Providers.Bot
{
    public interface IBot
    {
        StockModel GetStockQuote(RabbitMQMessage message);
    }
}
