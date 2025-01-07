using BlogApp.Core.Interfaces;
using BlogApp.Dto.Login;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAuthService _authService;

        public LoginController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (User.Identity?.IsAuthenticated == true) 
            {
                return RedirectToAction("Index", "Blog");
            }

            return View("Index", new LoginDto());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", loginDto);
            }
     
            var (success, message) = await _authService.LoginAsync(loginDto);

            if (success)
            {
                return RedirectToAction("Index", "Blog"); 
            }

            ViewBag.ErrorMessage = message;
            return View("Index", loginDto);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _authService.LogoutAsync();
            return RedirectToAction("Index", "Login");
        }
    }
   
}
