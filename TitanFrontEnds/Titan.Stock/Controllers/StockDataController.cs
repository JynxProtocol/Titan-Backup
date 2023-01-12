using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TitanAPIConnection;

namespace Titan.Stock.Controllers
{
    public class StockDataController : Controller
    {
        public StockDataController(TitanAPI titanAPI)
        {
            TitanAPI = titanAPI;
        }

        private TitanAPI TitanAPI { get; set; }

        [HttpPost]
        public async Task<JsonResult> Exists([FromForm] string Code)
        {
            if (string.IsNullOrWhiteSpace(Code))
            {
                return Json(false);
            }

            var StockExists = await TitanAPI.StockExistsAsync(Code);

            return Json(StockExists);
        }
    }
}
