using Microsoft.AspNetCore.Mvc;
using WebApplication2.Entities;

namespace WebApplication2.Controllers
{
    public class AdminController : Controller
    {
        private readonly TestDbContext _context;

        public AdminController(TestDbContext context)
        {
            _context = context;
        }

        public IActionResult Recipes()
        {
            List<Recipe> recipes = _context.Recipe.ToList();
            return View(recipes);
        }
    }
}
