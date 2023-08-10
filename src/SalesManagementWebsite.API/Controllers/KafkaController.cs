using Microsoft.AspNetCore.Mvc;
using SalesManagementWebsite.Core.Services.KafkaServices;

namespace SalesManagementWebsite.Core.Controllers
{
    [Route("api/kafka")]
    [ApiController]
    public class KafkaController : Controller, IKafkaServices
    {
        private IKafkaServices _kafkaServices;

        public KafkaController(IKafkaServices kafkaServices)
        {
            _kafkaServices = kafkaServices;
        }      

        [HttpGet("test-produce-message")]
        public async Task ProduceAsync(string topic)
        {
            await _kafkaServices.ProduceAsync(topic);
        }

        [HttpGet("test-consume-message")]
        public async Task Consume(string topic)
        {
            await _kafkaServices.Consume(topic);
        }


        [HttpGet("send-data-item-to-kafka")]
        public async Task SendDataItemToKafka()
        {
            await _kafkaServices.SendDataItemToKafka();
        }
    }
}
