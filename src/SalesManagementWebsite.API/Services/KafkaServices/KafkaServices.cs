using Confluent.Kafka;

namespace SalesManagementWebsite.API.Services.KafkaServices
{
    public class KafkaServices : IKafkaServices
    {
        public KafkaServices() { }

        public async Task ProduceAsync(string topic)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = "",
                SecurityProtocol = SecurityProtocol.SaslSsl,
                SaslMechanism = SaslMechanism.Plain,
                SaslUsername = "",
                SaslPassword = "",  
            };

            using (var producer = new ProducerBuilder<Null, string>(config).Build())
            {
                var result = await producer.ProduceAsync(topic, new Message<Null, string> { Value = "a log message"});
            }
            await Task.CompletedTask;
        }
    }
}
