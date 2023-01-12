using Microsoft.AspNetCore.Mvc;
using SageAPIConnection;
using System.Threading.Tasks;
using System.Web;
using Titan.Filters;

namespace Titan.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class StockController : ControllerBase
    {
        internal TitanContext Titan { get; private set; }
        internal SAGEContext Sage { get; private set; }
        private SageAPI SageAPI { get; set; }

        public StockController(TitanContext titan, SAGEContext sage, SageAPI sageAPI)
        {
            Titan = titan;
            Sage = sage;
            SageAPI = sageAPI;
        }

        /// <response code="200">Successfuly returns stock details</response>
        // GET: Stock/5/Details
        [HttpGet]
        [Route("{Code}/Details")]
        [Feature("StockDetails")]
        public async Task<StockDetailsDTO> StockDetails(string Code)
        {
            // codes often have slashes - decode them since MVC doesn't
            Code = HttpUtility.UrlDecode(Code);

            var StockDetails = await SageAPI.StockDetailsAsync(Code);

            return StockDetails;
        }

        /// <response code="200">Successfuly returns stock details</response>
        [HttpGet]
        [Route("Warehouse/{Warehouse}/{Code}/Details")]
        [Feature("WarehouseDetails")]
        public async Task<StockDetailsDTO> StockDetailsByWarehouse(string Code, string Warehouse)
        {
            // codes often have slashes - decode them since MVC doesn't
            Code = HttpUtility.UrlDecode(Code);

            var StockDetails = await SageAPI.StockDetailsByWarehouseAsync(Code, Warehouse);

            return StockDetails;
        }

        // GET: Stock/5/Exists
        /// <response code="200">Returns whether the stock code exists in Sage</response>
        [HttpGet]
        [Route("{Code}/Exists")]
        [Feature("StockExists")]
        public async Task<bool> StockExists(string Code)
        {
            // codes often have slashes - decode them since MVC doesn't
            Code = HttpUtility.UrlDecode(Code);

            var StockExists = await SageAPI.DoesStockExistAsync(Code);

            return StockExists;
        }

        // POST: Stock/5/Issue
        /// <response code="200">Successfully issued stock</response>
        [HttpPost]
        [Route("{Code}/Issue")]
        [Feature("IssueStock")]
        public async Task<StockResponseDTO> IssueStock(string Code,
            [FromBody] IssueStockDTO issueStockDTO)
        {
            // codes often have slashes - decode them since MVC doesn't
            Code = HttpUtility.UrlDecode(Code);

            // if ref is default
            if (issueStockDTO.SecondRef == (new IssueStockDTO()).SecondRef)
            {
                issueStockDTO.SecondRef = HttpContext?.User?.Identity?.Name ?? "Unknown user";
            }

            var IssueStock = await SageAPI.IssueStockAsync(Code, issueStockDTO);

            return IssueStock;
        }

        // POST: Stock/5/Add
        /// <response code="200">Successfully added to stock</response>
        [HttpPost]
        [Route("{Code}/Add")]
        [Feature("AddStock")]
        public async Task<StockResponseDTO> AddStock(string Code,
            [FromBody] AddStockDTO addStockDTO)
        {
            // codes often have slashes - decode them since MVC doesn't
            Code = HttpUtility.UrlDecode(Code);

            // if ref is default
            if (addStockDTO.SecondRef == (new AddStockDTO()).SecondRef)
            {
                addStockDTO.SecondRef = HttpContext?.User?.Identity?.Name ?? "Unknown user";
            }

            var AddStock = await SageAPI.AddStockAsync(Code, addStockDTO);

            return AddStock;
        }

        // POST: Stock/5/Correct
        /// <response code="200">Successfully corrected stock</response>
        [HttpPut]
        [Route("{Code}/Correct")]
        [Feature("CorrectStock")]
        public async Task<StockResponseDTO> CorrectStock(string Code,
            [FromBody] StockCorrectDTO stockCorrectDTO)
        {
            // codes often have slashes - decode them since MVC doesn't
            Code = HttpUtility.UrlDecode(Code);

            // if ref is default
            if (stockCorrectDTO.SecondRef == (new StockCorrectDTO()).SecondRef)
            {
                stockCorrectDTO.SecondRef = HttpContext?.User?.Identity?.Name ?? "Unknown user";
            }

            var StockCorrect = await SageAPI.CorrectStockAsync(Code, stockCorrectDTO);

            return StockCorrect;
        }
    }
}
