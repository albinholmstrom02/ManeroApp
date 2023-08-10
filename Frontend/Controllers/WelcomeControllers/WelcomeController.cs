using Microsoft.AspNetCore.Mvc;

namespace Frontend.Controllers.WelcomeSlides
{
    public class WelcomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
