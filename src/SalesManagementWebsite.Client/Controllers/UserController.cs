using Microsoft.AspNetCore.Mvc;

namespace SalesManagementWebsite.Client.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UserInfo()
        {
            return View();
        }
    }
}
