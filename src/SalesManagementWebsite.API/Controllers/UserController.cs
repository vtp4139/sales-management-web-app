using Microsoft.AspNetCore.Mvc;
using SalesManagementWebsite.API.Services.UserServices;
using SalesManagementWebsite.Contracts.Dtos.Response;
using SalesManagementWebsite.Contracts.Dtos.User;

namespace SalesManagementWebsite.API.Controllers
{
    [Route("api/user")]
    [ApiController] 
    public class UserController : Controller
    {
        public IUserService _userService { get; set; }

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<ResponseHandle<UserOuputDto>> Login(UserLoginDto userLoginDto)
        {
            return await _userService.Login(userLoginDto);
        }

        [HttpPost("register")]
        public async Task<ResponseHandle<UserOuputDto>> Register(UserRegisterDto userRegisterDto)
        {
            return await _userService.Register(userRegisterDto);
        }
    }
}
