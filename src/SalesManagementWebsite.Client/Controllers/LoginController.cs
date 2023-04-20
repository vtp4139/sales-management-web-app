using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using SalesManagementWebsite.Client.Services.Intefaces;
using SalesManagementWebsite.Contracts.Dtos.User;

namespace SalesManagementWebsite.Client.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserService _userService;
        private readonly IToastNotification _toastNotification;

        public LoginController(IUserService userService, IToastNotification toastNotification)
        {
            _userService = userService;
            _toastNotification = toastNotification;
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
            
            if (!res.IsSuccess)
            {
                _toastNotification.AddErrorToastMessage("Tên đăng nhập hoặc mật khẩu không khả dụng!");
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
