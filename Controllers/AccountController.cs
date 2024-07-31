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
        private readonly IConfiguration _configuration;

        public AccountController(TokenService tokenService, TestDbContext testDbContext, IConfiguration configuration)
        {
            _tokenService = tokenService;
            _testDbContext = testDbContext;
            _configuration = configuration;
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
            Console.WriteLine(request.Token);
            var result = ValidateJwtToken(request.Token);
            Console.WriteLine(result);

            if (result)
            {
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

        private bool ValidateJwtToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JWT_Secret"]);

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false
            };

            try
            {
                Console.WriteLine(token);
                tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);
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

