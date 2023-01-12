using Ganss.XSS;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Titan.Functions;
using TitanAPIConnection;

namespace Titan.Stock.Controllers
{
    public class KanbanController : Controller
    {
        public KanbanController(TitanAPI titanAPI)
        {
            TitanAPI = titanAPI;
        }

        private TitanAPI TitanAPI { get; set; }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Details(string Code)
        {
            var StockDetails = await TitanAPI.StockDetailsAsync(Code);

            if (StockDetails.IsKanban ?? false)
            {
                return View(StockDetails);
            }
            else
            {
                return View("Index", new StockResponseDTO()
                {
                    Message = "<h3 class='text-danger'>Item is not kanban</h3>"
                });
            }
        }


        [HttpPost]
        public async Task<ActionResult> Issue([FromForm] string Code,
            [FromForm] IssueStockDTO issueDetails, [FromServices] HtmlSanitizer htmlSanitizer)
        {
            var IssueStock = await TitanAPI.IssueStockAsync(Code, issueDetails);

            // Removes dangerous html attributes
            IssueStock.Message = htmlSanitizer.Decode(IssueStock.Message);

            return View("Index", IssueStock);
        }
    }
}
