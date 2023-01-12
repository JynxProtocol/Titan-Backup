using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TitanAPIConnection;

namespace Titan.Client.Controllers
{
    public class LookupController : Controller
    {
        internal TitanAPI TitanAPI { get; private set; }

        public LookupController(TitanAPI titanAPI)
        {
            TitanAPI = titanAPI;
        }

        public async Task<IActionResult> GetRepairableInformation(string TopLevelCode)
        {
            return Json(await TitanAPI.GetRepairableInformationAsync(TopLevelCode));
        }

        public async Task<IActionResult> GetPartCodeInformation(string Code)
        {
            return Json(await TitanAPI.GetPartCodeInformationAsync(Code));
        }

        public async Task<IActionResult> AutocompleteStockCode(string Code)
        {
            return Json(await TitanAPI.AutocompleteStockCodeAsync(Code));
        }

        public async Task<IActionResult> AutocompleteWorksOrderNumber(string WONumber)
        {
            return Json(await TitanAPI.AutocompleteWorksOrderNumberAsync(WONumber));
        }
    }
}
