using Azure;
using Frontend.Interfaces;
using Frontend.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace Frontend.Controllers
{
    public class OrdersController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7083/api");
        private readonly HttpClient _client;
        private readonly ICartService _cartService;
        public OrdersController(ICartService cartService)
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
            _cartService = cartService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue)
            {
                ModelState.AddModelError(string.Empty, "User ID not found.");
                return RedirectToAction("Index", "Products");
            }

            var cartItems = _cartService.GetCartItems();
            if (cartItems == null || cartItems.Count == 0)
            {
                ModelState.AddModelError(string.Empty, "Cart is empty.");
                return RedirectToAction("Index", "Products");
            }

            var orderDetails = new OrderViewModel
            {
                UserID = userId.Value,
                CartItems = cartItems,
                TotalPrice = cartItems.Sum(item => item.Price * item.Quantity)
            };

            // Serialize the order details to JSON and store in TempData
            TempData["OrderDetails"] = JsonConvert.SerializeObject(orderDetails);

            return View(orderDetails);
        }


        [HttpPost]
        public IActionResult CompleteOrder(OrderViewModel order)
        {
            int userId = HttpContext.Session.GetInt32("UserId") ?? 0;
            var cartItems = _cartService.GetCartItems();


            order.UserID = userId;
            order.Address = order.Address;
            order.PostalCode = order.PostalCode;
            order.TotalPrice = cartItems.Sum(item => item.Price * item.Quantity);
            order.CartItems = cartItems;

            string data = JsonConvert.SerializeObject(order);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/Orders/Create", content).Result;

            if (response.IsSuccessStatusCode)
            {
                _cartService.ClearCart();
                return RedirectToAction("Index", "Products");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "An error occurred while placing the order.");
                return View("Index", order);
            }
        }

        public IActionResult Completed() 
        {
            return View();
        }

    }
}
