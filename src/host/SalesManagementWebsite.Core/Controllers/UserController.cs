using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalesManagementWebsite.Core.Services.UserServices;
using SalesManagementWebsite.Contracts.Dtos.Response;
using SalesManagementWebsite.Contracts.Dtos.User;

namespace SalesManagementWebsite.Core.Controllers
{
    [Authorize]
    [Route("api/user")]
    [ApiController] 
    public class UserController : Controller, IUserService
    {
        private IUserService _userService { get; set; }

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async ValueTask<ResponseHandle<UserOuputDto>> Login(UserLoginDto userLoginDto)
        {
            return await _userService.Login(userLoginDto);
        }

        [Authorize(Roles = "administration")]
        [HttpPost("register")]
        public async ValueTask<ResponseHandle<UserOuputDto>> Register(UserRegisterDto userRegisterDto)
        {
            return await _userService.Register(userRegisterDto);
        }

        [HttpGet("{userName}")]
        public async ValueTask<ResponseHandle<UserOuputDto>> GetUserByUserName(string userName)
        {
            return await _userService.GetUserByUserName(userName);
        }

        [HttpGet]
        public async ValueTask<ResponseHandle<UsersListOuputDto>> GetAllUsers()
        {
            return await _userService.GetAllUsers();
        }

        [HttpPut("{userName}")]
        public async ValueTask<ResponseHandle<UserOuputDto>> UpdateUser(string userName, UserInputDto userInputDto)
        {
            return await _userService.UpdateUser(userName, userInputDto);
        }

        [HttpPut("{userName}/status")]
        public async ValueTask<ResponseHandle<UserOuputDto>> ChangeStatusUser(string userName, UserStatusInputDto userInputDto)
        {
            return await _userService.ChangeStatusUser(userName, userInputDto);
        }
    }
}
