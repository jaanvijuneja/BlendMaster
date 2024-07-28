using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplication2.Entities;
using WebApplication2.Models;
using WebApplication2.Services;
using WebApplication2.Tools;

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

        [HttpPost]
        public IActionResult ValidateToken([FromBody] TokenRequest request)
        {
            if (ValidateJwtToken(request.Token, out var principal))
            {
                // Create a session for the admin
                HttpContext.Session.SetObject("AdminSession", "true");
                return Ok(new { success = true });
            }
            else
            {
                return Unauthorized(new { success = false });
            }
        }

        public IActionResult LoginSuccess()
        {
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.SetObject("AdminSession", "false");
            return RedirectToAction("LogoutSuccess");
        }

        public IActionResult LogoutSuccess()
        {
            return View();
        }

        private bool ValidateJwtToken(string token, out ClaimsPrincipal principal)
        {
            principal = null;
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes("A4m3Dd2UBehUbSe95CIFyNI6ZJyHkV+a9H1dkElCALk=");

                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };

                principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }

    public class TokenRequest
    {
        public string? Token { get; set; }
    }
}
