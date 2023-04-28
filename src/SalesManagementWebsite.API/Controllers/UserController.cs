using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalesManagementWebsite.API.Services.UserServices;
using SalesManagementWebsite.Contracts.Dtos.Response;
using SalesManagementWebsite.Contracts.Dtos.User;

namespace SalesManagementWebsite.API.Controllers
{
    [Authorize]
    [Route("api/user")]
    [ApiController] 
    public class UserController : Controller, IUserService
    {
        public IUserService _userService { get; set; }

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
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

        [HttpGet("get-user/{userName}")]
        public async Task<ResponseHandle<UserOuputDto>> GetUser(string userName)
        {
            return await _userService.GetUser(userName);
        }

        [HttpGet("get-all-user")]
        public async Task<ResponseHandle<UsersListOuputDto>> GetAllUsers()
        {
            return await _userService.GetAllUsers();
        }
    }
}
