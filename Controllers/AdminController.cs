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
            List<Recipe> recipes = _context.Recipe.OrderBy(r => r.Status).ToList();
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

        [HttpPost]
        public IActionResult RecipeDetail(Recipe model)
        {
            Recipe recipe = _context.Recipe.Find(model.RecipeId);

            if (recipe == null)
            {
                return NotFound();
            }

            try
            {
                recipe.Status = model.Status;
                _context.Recipe.Update(recipe);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                // Log the exception (ex)
                return StatusCode(500, "Internal server error.");
            }

            return RedirectToAction("RecipeDetail", new { recipe.RecipeId });
        }

        public IActionResult OngoingOrders()
        {
            List<OngoingOrdersViewModel> models = new List<OngoingOrdersViewModel>();

            List<CustomerOrder> orders = _context.CustomerOrder
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .OrderByDescending(o => o.CreatedDate)
                .ToList();

            foreach (var order in orders)
            {
                var orderItems = order.OrderDetails
                    .Join(_context.Product,
                        od => od.ProductId,
                        p => p.ProductId,
                        (od, p) => new { p.ProductName, od.Quantity })
                    .ToDictionary(
                        x => x.ProductName,
                        x => x.Quantity);

                models.Add(new OngoingOrdersViewModel()
                {
                    OrderId = order.OrderId,
                    CreatedDate = order.CreatedDate,
                    OrderStatus = order.OrderStatus,
                    TableId = order.TableId,
                    OrderItems = orderItems
                });
            }

            return View(models);
        }

        public IActionResult CompleteOrder(Guid id)
        {
            CustomerOrder order = _context.CustomerOrder.Find(id);

            if (order == null) 
            { 
                return View("Error");
            }

            order.OrderStatus = OrderStatusType.Completed;

            _context.CustomerOrder.Update(order);
            _context.SaveChanges();

            return RedirectToAction("OngoingOrders");
        }

        public IActionResult CancelOrder(Guid id)
        {
            CustomerOrder order = _context.CustomerOrder.Find(id);

            if (order == null)
            {
                return View("Error");
            }

            order.OrderStatus = OrderStatusType.Cancelled;

            _context.CustomerOrder.Update(order);
            _context.SaveChanges();

            return RedirectToAction("OngoingOrders");
        }

        public IActionResult CloseOrder()
        {
            return View();
        }
    }
}
