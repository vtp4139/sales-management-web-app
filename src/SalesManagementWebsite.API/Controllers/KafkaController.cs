using Microsoft.AspNetCore.Mvc;
using SalesManagementWebsite.API.Services.KafkaServices;

namespace SalesManagementWebsite.API.Controllers
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

        [HttpGet("get-all-orders")]
        public Task ProduceAsync(string topic)
        {
            return _kafkaServices.ProduceAsync(topic);
        }
    }
}
