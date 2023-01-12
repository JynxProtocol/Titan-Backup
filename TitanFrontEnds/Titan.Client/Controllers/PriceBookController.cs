using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TitanAPIConnection;

namespace Titan.Client.Controllers
{
    public class PriceBookController : Controller
    {
        internal TitanAPI TitanAPI { get; private set; }

        public PriceBookController(TitanAPI titanAPI)
        {
            TitanAPI = titanAPI;
        }

        public async Task<ActionResult> Index()
        {
            var pricebook = await TitanAPI.ListAWKStockPricesAsync();

            return View(pricebook);
        }

        [HttpPost]
        [Route("[controller]/{StkID}/Price/Update")]
        public async Task<ActionResult> Update(int StkID, float Price)
        {
            await TitanAPI.SetAWKStockPriceAsync(StkID, Price);

            return NoContent();
        }
    }
}
