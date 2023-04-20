using Newtonsoft.Json;
using SalesManagementWebsite.Client.Services.Intefaces;
using SalesManagementWebsite.Contracts.Dtos.Response;
using SalesManagementWebsite.Contracts.Dtos.User;
using System.Text;

namespace SalesManagementWebsite.Client.Services.API
{
    public class UserService : IUserService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public UserService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<ResponseHandle<UserOuputDto>> Login(UserLoginDto userLoginDto)
        {
            var client = _httpClientFactory.CreateClient();

            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(userLoginDto),
                Encoding.UTF8, "application/json");

            var response = await client.PostAsync(_configuration["BackendUrl:Default"] + "/api/user/login", httpContent);

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ResponseHandle<UserOuputDto>>();
        }

        public Task<ResponseHandle<UserOuputDto>> Register(UserRegisterDto userRegisterDto)
        {
            throw new NotImplementedException();
        }
    }
}
