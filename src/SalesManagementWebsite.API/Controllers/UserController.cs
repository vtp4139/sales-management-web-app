using Microsoft.AspNetCore.Mvc;
using SalesManagementWebsite.API.Services.UserServices;
using SalesManagementWebsite.Contracts.Dtos.User;

namespace SalesManagementWebsite.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        public IUserService _userService { get; set; }

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Route("/login")]
        [HttpPost]
        public async Task<UserOuputDto> Login(UserLoginDto userLoginDto)
        {
            return await _userService.Login(userLoginDto);
        }

        [Route("/register")]
        [HttpPost]
        public async Task<UserOuputDto> Register(UserRegisterDto userRegisterDto)
        {
            return await _userService.Register(userRegisterDto);
        }
    }
}
