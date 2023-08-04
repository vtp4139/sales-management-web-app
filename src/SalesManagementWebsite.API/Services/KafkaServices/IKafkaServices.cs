namespace SalesManagementWebsite.API.Services.KafkaServices
{
    public interface IKafkaServices
    {
        Task ProduceAsync(string topic);
    }
}
