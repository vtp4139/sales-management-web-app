using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalesManagementWebsite.Core.Services.UserServices;
using SalesManagementWebsite.Contracts.Dtos.Response;
using SalesManagementWebsite.Contracts.Dtos.User;

namespace SalesManagementWebsite.Core.Controllers
{
    //[Authorize]
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

        [AllowAnonymous]
        [HttpPost("register")]
        public async ValueTask<ResponseHandle<UserOuputDto>> Register(UserRegisterDto userRegisterDto)
        {
            return await _userService.Register(userRegisterDto);
        }

        [HttpGet("get-user-by-username/{userName}")]
        public async ValueTask<ResponseHandle<UserOuputDto>> GetUserByUserName(string userName)
        {
            return await _userService.GetUserByUserName(userName);
        }

        [HttpGet("get-all-users")]
        public async ValueTask<ResponseHandle<UsersListOuputDto>> GetAllUsers()
        {
            return await _userService.GetAllUsers();
        }

        [HttpPut("update-user")]
        public async ValueTask<ResponseHandle<UserOuputDto>> UpdateUser(UserInputDto userInputDto)
        {
            return await _userService.UpdateUser(userInputDto);
        }

        [HttpPut("change-status-user")]
        public async ValueTask<ResponseHandle<UserOuputDto>> ChangeStatusUser(UserStatusInputDto userInputDto)
        {
            return await _userService.ChangeStatusUser(userInputDto);
        }
    }
}
