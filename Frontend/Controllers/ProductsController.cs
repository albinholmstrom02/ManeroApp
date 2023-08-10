using Frontend.Interfaces;
using Frontend.ViewModels;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Json;

namespace Frontend.Controllers
{
    public class ProductsController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7083/api");
        private readonly HttpClient _client;

        private readonly ICartService _cartService;


        public ProductsController(ICartService cartService)
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
            _cartService = cartService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<ProductsViewModel> productslist = new List<ProductsViewModel>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/products/Get").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                productslist = JsonConvert.DeserializeObject<List<ProductsViewModel>>(data);
            }


            return View(productslist);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            ProductsViewModel model = new ProductsViewModel();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + $"/products/GetById?id={id}").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<ProductsViewModel>(data);
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int id, int quantity)
        {
            ProductsViewModel model = new ProductsViewModel();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + $"/products/GetById?id={id}").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<ProductsViewModel>(data);
                _cartService.AddToCart(model, quantity);
            }

            return RedirectToAction("Index");
        }
    }
}
