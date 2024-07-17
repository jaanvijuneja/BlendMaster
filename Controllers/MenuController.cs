using Microsoft.AspNetCore.Mvc;
using WebApplication2.Entities;
using WebApplication2.Models;
using WebApplication2.Tools;

namespace WebApplication2.Controllers
{
    public class MenuController : Controller
    {
        private readonly TestDbContext _context;

        public MenuController(TestDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(Guid categoryId)
        {
            List<Category> categories = _context.Category.ToList();
            var products = _context.Product.Where(p => p.CategoryId == categoryId).ToList();

            if (!products.Any())
            {
                products = _context.Product.Where(p => p.CategoryId == _context.Category.FirstOrDefault().CategoryId).ToList();
            }

            MenuViewModel viewModel = new MenuViewModel() { Categories = categories, Products = products };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult AddToCart(Guid productId)
        {
            var Product = _context.Product.Find(productId);
            var cart = HttpContext.Session.GetObject<List<CartItem>>("Cart") ?? new List<CartItem>();
            var existingItem = cart.FirstOrDefault(i => i.ProductId == productId);
            if (existingItem == null)
            {
                cart.Add(new CartItem
                {
                    ProductId = productId,
                    ProductName = Product.ProductName,
                    Price = Product.Price,
                    Quantity = 1
                });
            }

            HttpContext.Session.SetObject("Cart", cart);
            return RedirectToAction("Index");
        }
    }
}
