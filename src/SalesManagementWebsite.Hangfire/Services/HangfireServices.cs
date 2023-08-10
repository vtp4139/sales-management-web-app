using SalesManagementWebsite.Contracts.Dtos.Order;
using SalesManagementWebsite.Contracts.Dtos.Response;

namespace SalesManagementWebsite.Hangfire.Services
{
    public class HangfireServices : IHangfireServices
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public HangfireServices(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<HangfireServices> logger)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<ResponseHandle<OrderListOutputDto>> GetOrders()
        {
            var client = _httpClientFactory.CreateClient();

            var response = await client.GetAsync(_configuration["SMCore:Default"] + "/api/order/get-all-orders");

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ResponseHandle<OrderListOutputDto>>();

            if (result == null)
            {
                _logger.LogError("Result is null!", result);
            }
            else
                _logger.LogInformation("Orders List: ", result.Data);
            
            return result;
        }
    }
}
