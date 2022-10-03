namespace mono_financialbot_backend_cs_external_serivces.Providers.RabbitMQ.Models
{
    public class RabbitMQMessage
    {
        public string? username { get; set; }

        public DateTime sendedDateUtc { get; set; }

        public string? Message { get; set; }

        public string group { get; set; } = "Global";
    }
}
