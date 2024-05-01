using Business.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(string usernameOrEmail, string password)
        {
            bool result = await _authService.LoginAsync(usernameOrEmail, password);
            if(result)
            {
                return RedirectToAction("GetAllUsers", "Users");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _authService.Logout();
            return RedirectToAction("Login");
        }
    }
}
