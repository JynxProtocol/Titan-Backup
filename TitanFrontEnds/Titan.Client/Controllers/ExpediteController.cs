using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TitanAPIConnection;

namespace Titan.Client.Controllers
{
    public class ExpediteController : Controller
    {
        internal TitanAPI TitanAPI { get; private set; }

        public ExpediteController(TitanAPI titanAPI)
        {
            TitanAPI = titanAPI;
        }

        // GET: NewExpedite
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var ExpediteStatus = await TitanAPI.GetExpediteStatusAsync();

            return View(ExpediteStatus);
        }

        [HttpGet]
        public async Task<JsonResult> Status()
        {
            var ExpediteStatus = await TitanAPI.GetExpediteStatusAsync();

            return Json(ExpediteStatus);
        }

        [HttpPost]
        public async Task<ActionResult> Run()
        {
            await TitanAPI.StartExpediteAsync();

            return NoContent();
        }
    }
}