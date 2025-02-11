using Newtonsoft.Json;
using SalesManagementWebsite.Client.Paths;
using SalesManagementWebsite.Client.Services.Intefaces;
using SalesManagementWebsite.Contracts.Dtos.Response;
using SalesManagementWebsite.Contracts.Dtos.User;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;

namespace SalesManagementWebsite.Client.Services.API
{
    public class UserService : IUserService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        public UserService(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }
      
        public async Task<ResponseHandle<UserOuputDto>> Login(UserLoginDto userLoginDto)
        {
            var client = _httpClientFactory.CreateClient();

            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(userLoginDto),
                Encoding.UTF8, "application/json");

            var path = $"{_configuration["BackendUrl:Default"]}{InternalAPIs.LOGIN}";

            var response = await client.PostAsync(path, httpContent);

            response.EnsureSuccessStatusCode();

            var output = await response.Content.ReadFromJsonAsync<ResponseHandle<UserOuputDto>>();

            return output != null ? output : null;
        }

        public Task<ResponseHandle<UserOuputDto>> Register(UserRegisterDto userRegisterDto)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseHandle<UserOuputDto>> GetUser()
        {
            //Get info user to get
            var token = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Token")?.Value;
            var userName = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

            var response = await client.GetAsync(_configuration["BackendUrl:Default"] + "/api/users/" + userName);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ResponseHandle<UserOuputDto>>();
        }


    }
}
