using Frontend.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace Frontend.Controllers
{
    public class SignUpController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7083/api");
        private readonly HttpClient _client;

        public SignUpController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(AccountsViewModel viewModel)
        {
            if (string.IsNullOrEmpty(viewModel.Name) ||
                string.IsNullOrEmpty(viewModel.Email) ||
                string.IsNullOrEmpty(viewModel.Password))
            {
                ModelState.AddModelError(string.Empty, "All fields are required.");
                return View("Index", viewModel); 
            }

            string data = JsonConvert.SerializeObject(viewModel);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/Accounts/SignUp", content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Login");
            }
            return View("Index", viewModel);
        }

    }
}
