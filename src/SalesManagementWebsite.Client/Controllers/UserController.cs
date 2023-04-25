using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalesManagementWebsite.Client.Services.Intefaces;

namespace SalesManagementWebsite.Client.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> UserInfo()
        {
            var user = await _userService.GetUser();
            return View(user.Data);
        }
    }
}
