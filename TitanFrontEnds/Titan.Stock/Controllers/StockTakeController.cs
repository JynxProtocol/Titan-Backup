using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Web;
using TitanAPIConnection;

namespace Titan.Stock.Controllers
{
    public class StockTakeController : Controller
    {
        public StockTakeController(TitanAPI titanAPI)
        {
            TitanAPI = titanAPI;
        }

        private TitanAPI TitanAPI { get; set; }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult List()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> EnterAmount(string Code)
        {
            var Locations = await TitanAPI.GetLocationsOfItemInStockTakeAsync(Code);

            if (Locations.Locations.Count == 0)
            {
                return View(nameof(this.Index), new StockResponseDTO
                {
                    Message = $"Item code <code>{HttpUtility.HtmlEncode(Code)}</code> is not in any active stocktakes"
                });
            }
            else
            {
                return View(Locations);
            }
        }

        [HttpPost]
        public async Task<ActionResult> SubmitAmount(string Code, decimal Amount, string Location)
        {
            var SubmitAmount = await TitanAPI.StocktakeRecordValueAsync(Code, Amount.ToString(), Location);

            return View(nameof(this.Index), SubmitAmount);
        }
    }
}
