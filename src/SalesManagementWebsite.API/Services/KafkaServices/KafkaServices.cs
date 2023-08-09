using Confluent.Kafka;
using SalesManagementWebsite.Contracts.Kafka;
using SalesManagementWebsite.Domain.UnitOfWork;
using System.Text.Json;

namespace SalesManagementWebsite.API.Services.KafkaServices
{
    public class KafkaServices : IKafkaServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;

        public KafkaServices(IUnitOfWork unitOfWork, ILogger<KafkaServices> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task ProduceAsync(string topic)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = "localhost:9092",
            };

            using (var producer = new ProducerBuilder<Null, string>(config).Build())
            {
                var result = await producer.ProduceAsync(topic, new Message<Null, string> { Value = "a log message" });
            }
            await Task.CompletedTask;
        }

        public async Task Consume(string topic)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = "localhost:9092",
                GroupId = "vtp1",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using (var consumer = new ConsumerBuilder<Ignore, string>(config).Build())
            {
                consumer.Subscribe(topic);

                while (true)
                {
                    var consumeResult = consumer.Consume();

                    _logger.LogInformation($"Message received from {consumeResult.TopicPartitionOffset}:{consumeResult.Message.Value}");
                }
                consumer.Close();
            }
            await Task.CompletedTask;
        }

        //Send all current data to Kakfa
        public async Task SendDataItemToKafka()
        {
            try
            {
                var listItem = _unitOfWork.ItemRepository.GetAll();

                var config = new ProducerConfig
                {
                    BootstrapServers = "localhost:9092",
                };

                using (var producer = new ProducerBuilder<Null, string>(config).Build())
                {
                    foreach (var item in listItem)
                    {
                        await producer.ProduceAsync(KafkaTopics.ItemTopic, new Message<Null, string> { Value = JsonSerializer.Serialize(item) });
                    }
                }

                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                throw ex;
            }           
        }
    }
}
