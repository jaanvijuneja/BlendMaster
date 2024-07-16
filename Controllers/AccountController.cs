using Microsoft.AspNetCore.Mvc;
using WebApplication2.Entities;
using WebApplication2.Models;
using WebApplication2.Services;

namespace WebApplication2.Controllers
{
    public class AccountController : Controller
    {
        private readonly TokenService _tokenService;
        private readonly TestDbContext _testDbContext;

        public AccountController(TokenService tokenService, TestDbContext testDbContext)
        {
            _tokenService = tokenService;
            _testDbContext = testDbContext;
        }

        [HttpGet]
        public IActionResult Login()
        {
            LoginModel model = new LoginModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = _testDbContext.User
                .FirstOrDefault(u => u.Name == model.Username);

            if (user != null && BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
            {
                string token = _tokenService.GenerateToken(model.Username);

                TempData["UserName"] = model.Username;
                TempData["Token"] = token;

                return RedirectToAction("LoginSuccess");
            }
            else
            {
                return Unauthorized();
            }
        }

        public IActionResult LoginSuccess()
        {
            return View();
        }

        public IActionResult Logout()
        {
            return RedirectToAction("LogoutSuccess");
        }

        public IActionResult LogoutSuccess()
        {
            return View();
        }
    }
}
