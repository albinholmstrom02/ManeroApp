using Frontend.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Frontend.Controllers.WelcomeSlides
{
    public class SecondSlideController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
