using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using SalesManagementWebsite.Client.Services.Intefaces;
using SalesManagementWebsite.Contracts.Dtos.User;
using System.Security.Claims;

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
            //if user is login, redirect to home page
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
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

            //Add claim to gen authentication
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, res.Data.UserName),
                new Claim("FullName", res.Data.Name),
                new Claim("Token", res.Data.Token),
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
                ExpiresUtc = DateTime.UtcNow.AddMinutes(10),
            };

            await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), authProperties);

            return RedirectToAction("Index", "Home");
        }
    }
}
