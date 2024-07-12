using Microsoft.AspNetCore.Mvc;
using WebApplication2.Entities;
using WebApplication2.Models;
using WebApplication2.Tools;

namespace WebApplication2.Controllers
{
    public class CartController : Controller
    {
        private readonly TestDbContext _context;

        public CartController(TestDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var cart = HttpContext.Session.GetObject<List<CartItem>>("Cart");
            return View(cart);
        }

        [HttpPost]
        public IActionResult UpdateQuantity(CartItem item)
        {
            var cart = HttpContext.Session.GetObject<List<CartItem>>("Cart");
            var itemToUpdate = cart.FirstOrDefault(i => i.ProductId == item.ProductId);

            if (itemToUpdate != null)
            {
                itemToUpdate.Quantity = item.Quantity;
                HttpContext.Session.SetObject("Cart", cart);
            }

            return RedirectToAction("Index", "Cart");
        }

        [HttpPost]
        public IActionResult RemoveItem(CartItem item)
        {
            var cart = HttpContext.Session.GetObject<List<CartItem>>("Cart");
            var itemToRemove = cart.FirstOrDefault(i => i.ProductId == item.ProductId);

            if (itemToRemove != null)
            {
                cart.Remove(itemToRemove);
                HttpContext.Session.SetObject("Cart", cart);
            }

            return RedirectToAction("Index", "Cart");
        }

        public IActionResult Checkout() 
        {
            Guid orderId = Guid.NewGuid();
            decimal total = 0;
            var cart = HttpContext.Session.GetObject<List<CartItem>>("Cart");

            foreach (var cartItem in cart)
            {
                OrderDetail detail = new OrderDetail();

                detail.OrderDetailId = Guid.NewGuid();
                detail.OrderId = orderId;
                detail.ProductId = cartItem.ProductId;
                detail.UnitPrice = cartItem.Price;
                detail.Quantity = cartItem.Quantity;

                _context.OrderDetail.Add(detail);

                total += detail.UnitPrice * detail.Quantity;
            }

            CustomerOrder order = new CustomerOrder()
            {
                CreatedDate = DateTime.Now,
                OrderId = orderId,
                Total = total
            };

            _context.CustomerOrder.Add(order);
            _context.SaveChanges();

            HttpContext.Session.Clear();
            HttpContext.Session.Remove("Cart");

            return View();
        }
    }
}
