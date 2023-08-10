using Microsoft.AspNetCore.Mvc;

namespace Frontend.Controllers.WelcomeSlides
{
    public class FirstSlideController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
