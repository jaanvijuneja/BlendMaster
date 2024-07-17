using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Entities;
using WebApplication2.Models;

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

        [HttpGet]
        public IActionResult RecipeDetail(Guid id)
        {
            Recipe recipe = _context.Recipe.FirstOrDefault(r => r.RecipeId == id);

            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe);
        }

        public IActionResult OngoingOrders()
        {
            List<OngoingOrdersViewModel> models = new List<OngoingOrdersViewModel>();

            List<CustomerOrder> orders = _context.CustomerOrder
                .Include(o => o.OrderDetails)
                .OrderBy(o => o.OrderStatus)
                .ToList();

            foreach (var order in orders)
            {
                var listOfProducts = order.OrderDetails
                    .Join(_context.Product,
                        od => od.ProductId,
                        p => p.ProductId,
                        (od, p) => p.ProductName)
                    .ToList();

                models.Add(new OngoingOrdersViewModel()
                {
                    OrderId = order.OrderId,
                    CreatedDate = order.CreatedDate,
                    OrderStatus = order.OrderStatus,
                    Products = listOfProducts
                });
            }

            return View(models);
        }

        public IActionResult CloseOrder()
        {
            return View();
        }
    }
}
