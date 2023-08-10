using Frontend.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace Frontend.Controllers
{
    public class LoginController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7083/api");
        private readonly HttpClient _client;

        private readonly IHttpContextAccessor _contextAccessor;

        public LoginController(IHttpContextAccessor httpContextAccessor)
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
            _contextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            if (string.IsNullOrEmpty(viewModel.Email) ||
                string.IsNullOrEmpty(viewModel.Password))
            {
                ModelState.AddModelError(string.Empty, "All fields are required.");
                return View("Index", viewModel);
            }
            
            string data = JsonConvert.SerializeObject(viewModel);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/Accounts/Login", content).Result;

            if (response.IsSuccessStatusCode)
            {
                string userIdString = await response.Content.ReadAsStringAsync();
                if (int.TryParse(userIdString, out int userId))
                {
                    _contextAccessor.HttpContext.Session.SetInt32("UserId", userId);
                    return RedirectToAction("Index", "Products");
                }
            }
            else if(!response.IsSuccessStatusCode) 
            {
                ModelState.AddModelError(string.Empty, "Incorrect details");
            }

            return View("Index", viewModel);
        }
    }
}
