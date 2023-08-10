namespace SalesManagementWebsite.Core.Services.KafkaServices
{
    public interface IKafkaServices
    {
        Task ProduceAsync(string topic);
        Task Consume(string topic);
        Task SendDataItemToKafka();
    }
}
