using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Web.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
