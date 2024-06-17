using BlendMaster.Services;
using BlendMaster.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using BlendMaster.Helpers;
using BlendMaster.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq;

namespace BlendMaster.Controllers
{
    public class HomeController : Controller
    {
        private readonly WineService _wineService;
        private readonly CartService _cartService;
        private readonly TableService _tableService;
        private readonly CategoryService _categoriesService;

        public HomeController(WineService wineService, CartService cartService, TableService tableService, CategoryService categoryService)
        {
            _wineService = wineService;
            _cartService = cartService;
            _tableService = tableService;
            _categoriesService = categoryService;
        }

        [HttpGet]
        public IActionResult SelectTable()
        {
            var tables = _tableService.GetAllTables();
            return View(tables);
        }

        [HttpPost]
        public IActionResult SelectTable(int tableId)
        {
            var table = _tableService.GetTableById(tableId);
            if (table == null)
            {
                return RedirectToAction("Error");
            }
            HttpContext.Session.SetInt32("TableId", tableId);
            Console.WriteLine($"TableId set to: {tableId}");
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Index()
        {
            var tableId = HttpContext.Session.GetInt32("TableId");
            Console.WriteLine($"TableId retrieved: {tableId}");
            if (tableId == null)
            {
                return RedirectToAction("Error", "Home");
            }

            var cart = HttpContext.Session.GetObjectFromJson<CartViewModel>("Cart") ?? new CartViewModel { TableId = tableId.Value };
            cart.TableId = tableId.Value;
            ViewBag.Cart = cart;

            ViewBag.TableId = tableId;
            ViewBag.Categories = _categoriesService.GetAllCategories();
            var wines = _wineService.GetAllWines();
            return View(wines);
        }

        [HttpPost]
        public IActionResult AddToCart(int wineId)
        {
            var wine = _wineService.GetWineById(wineId);
            if (wine == null)
            {
                return RedirectToAction("Error");
            }
            var cart = HttpContext.Session.GetObjectFromJson<CartViewModel>("Cart") ?? new CartViewModel();
            cart.TableId = HttpContext.Session.GetInt32("TableId") ?? 0;
            var cartItem = cart.Items.FirstOrDefault(i => i.WineId == wineId);
            if (cartItem == null)
            {
                cart.Items.Add(new CartItemViewModel
                {
                    WineId = wine.WineId,
                    WineName = wine.WineName,
                    Price = wine.Price,
                    Quantity = 1
                });
            }
            else
            {
                cartItem.Quantity++;
            }
            cart.TotalPrice = cart.Items.Sum(i => i.Quantity * i.Price);
            HttpContext.Session.SetObjectAsJson("Cart", cart);
            Console.WriteLine($"TableId in AddToCart: {cart.TableId}");
            var quantity = cart.Items.FirstOrDefault(i => i.WineId == wineId)?.Quantity ?? 0;
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult RemoveFromCart(int wineId)
        {
            var cart = HttpContext.Session.GetObjectFromJson<CartViewModel>("Cart");
            if (cart == null)
            {
                return RedirectToAction("Error");
            }
            cart.TableId = HttpContext.Session.GetInt32("TableId") ?? 0;
            Console.WriteLine($"TableId before removing item: {cart.TableId}");
            var cartItem = cart.Items.FirstOrDefault(i => i.WineId == wineId);
            if (cartItem != null)
            {
                if (cartItem.Quantity > 1)
                {
                    cartItem.Quantity--;
                }
                else
                {
                    cart.Items.Remove(cartItem);
                }
            }
            cart.TotalPrice = cart.Items.Sum(i => i.Quantity * i.Price);
            HttpContext.Session.SetObjectAsJson("Cart", cart);
            Console.WriteLine($"TableId in RemoveFromCart: {cart.TableId}");
            var quantity = cart.Items.FirstOrDefault(i => i.WineId == wineId)?.Quantity ?? 0;
            return RedirectToAction("Index");
        }

        public IActionResult Cart()
        {
            var cart = HttpContext.Session.GetObjectFromJson<CartViewModel>("Cart") ?? new CartViewModel();
            cart.TableId = HttpContext.Session.GetInt32("TableId") ?? 0;
            Console.WriteLine($"TableId in Cart: {cart.TableId}");
            return View(cart);
        }

        [HttpPost]
        public IActionResult Checkout()
        {
            var cart = HttpContext.Session.GetObjectFromJson<CartViewModel>("Cart");
            cart.TableId = HttpContext.Session.GetInt32("TableId") ?? 0;
            Console.WriteLine($"TableId in Checkout: {cart.TableId}");
            if (cart != null)
            {
                var order = _cartService.CheckOut(cart);
                HttpContext.Session.Remove("Cart");
                return RedirectToAction("OrderConfirmation", new { orderId = order.OrderId });
            }
            return RedirectToAction("Index");
        }

        public IActionResult OrderConfirmation(int orderId)
        {
            return View(orderId);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
