namespace mono_financialbot_backend_cs_external_serivces.Providers.RabbitMQ.Models
{
    /// <summary>
    /// this class references to an RabbitMQ configuration
    /// </summary>
    public class RabbitMQConfiguration
    {
        /// <summary>
        /// this property refers to the HostName
        /// </summary>
        public string? HostName { get; set; }
        /// <summary>
        /// this property refers to the rabbitMQ QueueName
        /// </summary>
        public string? QueueName { get; set; }
        public string? Password { get; set; }
        public string UserName { get; set; }

    }
}
