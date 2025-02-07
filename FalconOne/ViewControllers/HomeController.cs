using Microsoft.AspNetCore.Mvc;

namespace FalconOne.API.ViewControllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        public IActionResult Index()
        {
            return View("~/Views/Index.cshtml");
        }
    }
}
