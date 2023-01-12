using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Titan.Models;
using Titan.Models.ExtendedStock;

namespace Titan.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ExtendedStockController : ControllerBase
    {
        public ExtendedStockController(SAGEContext sage)
        {
            Sage = sage;
        }

        private readonly SAGEContext Sage = new();

        // GET: ExtendedStock
        /// <response code="200">Returns a page of extended stock results</response>
        [HttpPost]
        public ActionResult<PagedList<ExtendedStockLookupView>> GetExtendedStock(
            [FromBody] PagedListParameters extendedStockParameters)
        {
            IQueryable<ExtendedStockLookupView> collection =
                (from StockItem in Sage.StockItems
                 join StockItemX in Sage.StockItemXes
                 on StockItem.ItemID equals StockItemX.StockItemXID
                 join ProductGroup in Sage.ProductGroups
                 on StockItem.ProductGroupID equals ProductGroup.ProductGroupID
                 select new ExtendedStockLookupView
                 {
                     ItemID = StockItem.ItemID,
                     Code = StockItem.Code,
                     Description = StockItem.Name,
                     LookupID = ((int)Math.Round(StockItemX.SabreSpareNumber1)),
                     ProductGroup = ProductGroup.Description
                 });

            collection = collection.Where(ExtendedStockItem =>
                ExtendedStockItem.ProductGroup.StartsWith("FG"));
            collection = collection.Where(ExtendedStockItem => (
                ExtendedStockItem.ProductGroup.Contains(extendedStockParameters.SearchTerm) ||
                ExtendedStockItem.Code.Contains(extendedStockParameters.SearchTerm) ||
                ExtendedStockItem.Description.Contains(extendedStockParameters.SearchTerm) ||
                ExtendedStockItem.ItemID.ToString().Contains(extendedStockParameters.SearchTerm) ||
                ExtendedStockItem.LookupID.ToString().Contains(extendedStockParameters.SearchTerm))
            );

            collection = collection.OrderBy(ExtendedStockItem => ExtendedStockItem.Code);

            PagedList<ExtendedStockLookupView> pagedList =
                PagedList<ExtendedStockLookupView>.Create(
                    collection,
                    extendedStockParameters.CurrentPage,
                    extendedStockParameters.PageSize,
                    extendedStockParameters.SearchTerm
                );
            return pagedList;
        }

        // PUT: ExtendedStock/{id}/LookupId
        /// <response code="200">Successfully set lookup id</response>
        /// <response code="404">The stockitem could not be found</response>
        [HttpPut]
        [Route("{id}/LookupID")]
        public ActionResult SetExtendedStockLookupId(int id, [FromBody] int LookupID)
        {
            try
            {
                var stockItem = Sage.StockItems
                    .FirstOrDefault(StockItem => StockItem.ItemID == id);
                if (stockItem == null)
                {
                    return NotFound();
                }

                var StockItemX = Sage.StockItemXes
                    .FirstOrDefault(StockItemX => StockItemX.StockItemXID == stockItem.ItemID);
                if (StockItemX == null)
                {
                    return NotFound();
                }

                StockItemX.SabreSpareNumber1 = LookupID;

                Sage.SaveChanges();

            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                throw;
            }

            return Ok();
        }
    }
}
