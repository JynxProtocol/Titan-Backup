using Ganss.XSS;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Titan.Functions;
using TitanAPIConnection;

namespace Titan.Stock.Controllers
{
    public class StockCorrectController : Controller
    {
        public StockCorrectController(TitanAPI titanAPI)
        {
            TitanAPI = titanAPI;
        }

        private TitanAPI TitanAPI { get; set; }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> StockCorrect(string Code)
        {
            var StockDetails = await TitanAPI.StockDetailsAsync(Code);

            return View(StockDetails);
        }


        [HttpPost]
        public async Task<ActionResult> StockCorrect([FromForm] string Code,
            [FromForm] StockCorrectDTO stockCorrect, [FromServices] HtmlSanitizer htmlSanitizer)
        {
            var CorrectStock = await TitanAPI.CorrectStockAsync(Code, stockCorrect);

            // Removes dangerous html attributes
            CorrectStock.Message = htmlSanitizer.Decode(CorrectStock.Message);

            return View("~/Views/Kanban/Index.cshtml", new StockResponseDTO()
            {
                Message = CorrectStock.Message
            });
        }
    }
}
