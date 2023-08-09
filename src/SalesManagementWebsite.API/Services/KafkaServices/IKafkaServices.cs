namespace SalesManagementWebsite.API.Services.KafkaServices
{
    public interface IKafkaServices
    {
        Task ProduceAsync(string topic);
        Task Consume(string topic);
        Task SendDataItemToKafka();
    }
}
