using Microsoft.AspNetCore.Mvc;
using SalesManagementWebsite.API.Services.User;
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

        [HttpPost]
        public async Task Register(UserRegisterDto userRegisterDto)
        {
            //Test user.name and user.email
            await _userService.Register(userRegisterDto);
        }
    }
}
