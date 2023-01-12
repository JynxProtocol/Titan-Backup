using Microsoft.AspNetCore.Mvc;

namespace Titan.Stock.Controllers
{
    public class IssueController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
