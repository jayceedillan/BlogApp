using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Web.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
