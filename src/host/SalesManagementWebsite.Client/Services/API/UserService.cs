using SalesManagementWebsite.Client.Paths;
using SalesManagementWebsite.Client.Services.Intefaces;
using SalesManagementWebsite.Contracts.Dtos.Response;
using SalesManagementWebsite.Contracts.Dtos.User;
using System.Security.Claims;

namespace SalesManagementWebsite.Client.Services.API
{
    public class UserService : IUserService
    {
        private readonly IApiService _apiService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IApiService apiService, IHttpContextAccessor httpContextAccessor)
        {
            _apiService = apiService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ResponseHandle<UserOuputDto>> Login(UserLoginDto userLoginDto)
        {
            var res = await _apiService.SendRequestAsync<ResponseHandle<UserOuputDto>>(HttpMethod.Post, InternalAPIs.Login, userLoginDto);
            return res;
        }

        public Task<ResponseHandle<UserOuputDto>> Register(UserRegisterDto userRegisterDto)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseHandle<UserOuputDto>> GetUser()
        {
            var userName = _httpContextAccessor?.HttpContext?.User.FindFirst(ClaimTypes.Name)?.Value;
            var res = await _apiService.SendRequestAsync<ResponseHandle<UserOuputDto>>(HttpMethod.Get, string.Format(InternalAPIs.GetUser, userName));
            return res;
        }
    }
}
