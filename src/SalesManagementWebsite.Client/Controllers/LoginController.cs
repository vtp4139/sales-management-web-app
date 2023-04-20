using Microsoft.AspNetCore.Mvc;
using SalesManagementWebsite.Client.Services.Intefaces;
using SalesManagementWebsite.Contracts.Dtos.User;

namespace SalesManagementWebsite.Client.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserService _userService;

        public LoginController(IUserService userService)
        {
            _userService = userService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(UserLoginDto userLoginDto)
        {
            var res = await _userService.Login(userLoginDto);
            return View();
        }
    }
}
