using Microsoft.AspNetCore.Mvc;

namespace Titan.Stock.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
