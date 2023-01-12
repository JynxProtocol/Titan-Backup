using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SageAPIConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Titan.API.Models;
using Titan.Models;
using Titan.Models.StockTake;

namespace Titan.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class StockTakeController : ControllerBase
    {
        public StockTakeController(SAGEContext sage, TitanContext titan, SageAPI sageAPI)
        {
            Sage = sage;
            Titan = titan;
            SageAPI = sageAPI;
        }

        private SageAPI SageAPI { get; set; }

        private readonly SAGEContext Sage = new();
        private readonly TitanContext Titan = new();

        private readonly string[] ActiveStatuses = { "Selecting items", "Entering values" };
        private readonly string[] CompletedStatuses = { "Completed" };
        private readonly string[] DeletedStatuses = { "Deleted" };


        /// <response code="200">Successfully returns list of warehouses</response>
        [HttpGet]
        [Route("Warehouses")]
        public IEnumerable<string> ListWarehouses()
        {
            return Sage.Warehouses.Select(w => w.Name).ToList();
        }

        /// <response code="200">Returns the stocktake with the given id</response>
        /// <response code="404">Could not find stocktake</response>
        [HttpGet]
        [Route("{id}")]
        public ActionResult<StockTakeInfoDTO> GetStocktake(int id)
        {
            var StockTake = Titan.StockTakeHeaders
                .SingleOrDefault(StockTakeHeader => StockTakeHeader.STKID == id);

            if (StockTake == null)
            {
                return NotFound();
            }

            var StockTakeItems = Titan.StockTakeDetails
                .Where(StockTakeDetail => StockTakeDetail.STKID == id);

            var numberOfItems = StockTakeItems.Count();
            var numberCompleted = StockTakeItems.Where(Item => Item.RecordedValue != null).Count();

            var StockTakeInfo = new StockTakeInfoDTO()
            {
                ID = StockTake.STKID,
                Name = StockTake.Name,
                Status = Titan.StockTakeStatuses
                    .Single(Status => Status.StatusID == StockTake.StatusID).Name,
                CompletedBy = StockTake.CompletedBy,
                CreatedBy = StockTake.CreatedBy,
                DateCompleted = StockTake.DateCompleted,
                DateCreated = StockTake.DateCreated,
                Warehouse = StockTake.Warehouse,
                Completion = $"{numberCompleted}/{numberOfItems}"
            };

            foreach (StockTakeDetail Item in StockTakeItems)
            {

                StockTakeInfo.Items.Add(new StockTakeItemInfoDTO()
                {
                    RecordedBy = Item.RecordedBy,
                    RecordedDate = Item.RecordedDate,
                    Adjustment = Item.Adjustment,
                    AdjustmentValue = Item.AdjustmentValue,
                    Code = Item.PartNumber,
                    Description = Item.Description,
                    FreeStock = Item.FreeStock,
                    Location = Item.Location,
                    RecordedValue = Item.RecordedValue
                });
            }

            return StockTakeInfo;
        }

        /// <response code="200">Returns a list of stocktake items</response>
        /// <response code="404">Could not find stocktake</response>
        [HttpGet]
        [Route("{id}/SelectedItems")]
        public ActionResult<IEnumerable<StockTakeItemsDTO>> StocktakeSelectedItems(int id)
        {
            var StockTake = Titan.StockTakeHeaders
                .SingleOrDefault(StockTakeHeader => StockTakeHeader.STKID == id);

            if (StockTake == null)
            {
                return NotFound();
            }

            List<string> SelectedItems = Titan.StockTakeDetails
                .Where(StockTakeItem => StockTakeItem.STKID == id)
                .Select(StockTakeItem => StockTakeItem.PartNumber)
                .ToList();

            string Warehouse = Titan.StockTakeHeaders
                .Where(StockTake => StockTake.STKID == id)
                .Select(StockTake => StockTake.Warehouse)
                .FirstOrDefault();

            IQueryable<StockTakeItemsDTO> collection =
                (from WarehouseItem in Sage.WarehouseItems
                 join DBWarehouse in Sage.Warehouses
                 on WarehouseItem.WarehouseID equals DBWarehouse.WarehouseID
                 join StockItem in Sage.StockItems
                 on WarehouseItem.ItemID equals StockItem.ItemID
                 join ProductGroup in Sage.ProductGroups
                 on StockItem.ProductGroupID equals ProductGroup.ProductGroupID
                 where SelectedItems.Contains(StockItem.Code.Trim())
                 && (Warehouse == null || DBWarehouse.Name.ToUpper() == Warehouse.Trim().ToUpper())
                 select new StockTakeItemsDTO
                 {
                     Code = StockItem.Code,
                     Description = StockItem.Name,
                     DateOfLastStockCount = WarehouseItem.DateOfLastStockCount,
                     ProductGroup = ProductGroup.Description,
                     QuantityAllocated = (int)Math.Ceiling(
                         WarehouseItem.QuantityAllocatedBOM +
                         WarehouseItem.QuantityAllocatedSOP +
                         WarehouseItem.QuantityAllocatedStock
                         )
                 });

            return collection.ToList();
        }

        /// <response code="200">Returns the created stocktake id</response>
        [HttpPost]
        [Route("{Warehouse}/New")]
        public int CreateStocktake(string Warehouse)
        {
            var DateTimeNow = DateTime.Now;
            var StockTakeName = $"{Warehouse} {DateTimeNow.ToString("G")}";

            var NewStockTake = new StockTakeHeader()
            {
                StatusID = Titan.StockTakeStatuses
                    .Single(Status => Status.Name == "Selecting items").StatusID,
                Warehouse = Warehouse,
                Name = StockTakeName,
                DateCreated = DateTimeNow,
                CreatedBy = HttpContext.GetUserDisplayName()
            };

            Titan.StockTakeHeaders.Add(NewStockTake);
            Titan.SaveChanges();

            return NewStockTake.STKID;
        }

        /// <response code="200">Successfully adds part to stocktake</response>
        /// <response code="404">Could not find stocktake</response>
        /// <response code="500">Could not add part to stocktake</response>
        [HttpPost]
        [Route("{id}/Add")]
        public IActionResult AddToStockTake(string Code, int id)
        {
            var StockTake = Titan.StockTakeHeaders
                .SingleOrDefault(StockTakeHeader => StockTakeHeader.STKID == id);

            if (StockTake == null)
            {
                return NotFound();
            }

            var StockTakesUsingItem = (from StockTakeHeader in Titan.StockTakeHeaders
                                       join StockTakeItem in Titan.StockTakeDetails
                                       on StockTakeHeader.STKID equals StockTakeItem.STKID
                                       join StockTakeStatus in Titan.StockTakeStatuses
                                       on StockTakeHeader.StatusID equals StockTakeStatus.StatusID
                                       where ActiveStatuses.Contains(StockTakeStatus.Name)
                                       && StockTakeItem.PartNumber == Code
                                       select StockTakeHeader.Name);

            // fail if part is already in an active stocktake
            if (StockTakesUsingItem.Count() != 0)
            {
                HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                return new JsonResult(new ErrorViewModel()
                {
                    Message = $"Cannot add part to stocktake because " +
                    $"it is already in use in active stocktakes:" +
                    $" {HttpUtility.HtmlEncode(string.Join(", ", StockTakesUsingItem))}",
                    CorrelationId =
                    System.Diagnostics.Activity.Current?.Id ?? HttpContext?.TraceIdentifier
                });
            }

            if (!TryGetSageInfo(id, Code, out Warehouse Warehouse, out StockItem StockItem,
                out WarehouseItem WarehouseItem, out string ValidationErrorMessage))
            {
                HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                return new JsonResult(new ErrorViewModel()
                {
                    Message = ValidationErrorMessage,
                    CorrelationId =
                    System.Diagnostics.Activity.Current?.Id ?? HttpContext?.TraceIdentifier
                });
            }

            // get all locations of items in the warehouse
            IQueryable<StockTakeDetail> ItemLocations = Sage.BinItems
                .Where(BinItem => BinItem.ItemID == StockItem.ItemID)
                .Where(BinItem => BinItem.WarehouseItemID == WarehouseItem.WarehouseItemID)
                .Select(BinItem => new StockTakeDetail
                {
                    STKID = StockTake.STKID,
                    PartNumber = StockItem.Code,
                    Location = BinItem.BinName,
                    Description = StockItem.Name,
                    FreeStock = BinItem.ConfirmedQtyInStock + BinItem.UnconfirmedQtyInStock
                });

            try
            {
                Titan.StockTakeDetails.AddRange(ItemLocations);
                Titan.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.WriteLine(ex.Message);
                throw;
            }

        }

        /// <response code="200">Successfully removed part from stocktake</response>
        /// <response code="404">Could not find stocktake</response>
        /// <response code="500">Could not remove part from stocktake</response>
        [HttpDelete]
        [Route("{id}/Remove")]
        public IActionResult RemoveFromStockTake(string Code, int id)
        {
            var StockTake = Titan.StockTakeHeaders
                .Where(StockTake => StockTake.STKID == id).Single();

            if (StockTake == null)
            {
                return NotFound();
            }

            if (!TryGetSageInfo(id, Code, out Warehouse Warehouse, out StockItem StockItem,
                out WarehouseItem WarehouseItem, out string ValidationErrorMessage))
            {
                HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError; ;
                return new JsonResult(new ErrorViewModel()
                {
                    Message = ValidationErrorMessage,
                    CorrelationId =
                    System.Diagnostics.Activity.Current?.Id ?? HttpContext?.TraceIdentifier
                });
            }

            // get all locations of items in Titan
            IQueryable<StockTakeDetail> ItemLocations = Titan.StockTakeDetails
                .Where(StockTakeItem => StockTakeItem.PartNumber == Code)
                .Where(StockTakeItem => StockTakeItem.STKID == StockTake.STKID);

            Titan.StockTakeDetails.RemoveRange(ItemLocations);
            Titan.SaveChanges();
            return Ok();
        }

        /// <response code="200">Successfully returns a list of items in a warehouse</response>
        [HttpPost]
        [Route("{Warehouse}/Items")]
        public PagedList<StockTakeItemsDTO> GetWarehouseItems(string Warehouse,
            PagedListParameters pagedListParameters)
        {
            IQueryable<StockTakeItemsDTO> collection =
                (from WarehouseItem in Sage.WarehouseItems
                 join DBWarehouse in Sage.Warehouses
                 on WarehouseItem.WarehouseID equals DBWarehouse.WarehouseID
                 join StockItem in Sage.StockItems
                 on WarehouseItem.ItemID equals StockItem.ItemID
                 join ProductGroup in Sage.ProductGroups
                 on StockItem.ProductGroupID equals ProductGroup.ProductGroupID
                 where DBWarehouse.Name.ToUpper() == Warehouse.Trim().ToUpper()
                 && !ProductGroup.Code.StartsWith("FG")
                 select new StockTakeItemsDTO
                 {
                     Code = StockItem.Code,
                     Description = StockItem.Name,
                     DateOfLastStockCount = WarehouseItem.DateOfLastStockCount,
                     ProductGroup = ProductGroup.Description,
                     QuantityAllocated = (int)Math.Ceiling(
                         WarehouseItem.QuantityAllocatedBOM +
                         WarehouseItem.QuantityAllocatedSOP +
                         WarehouseItem.QuantityAllocatedStock
                         )
                 });

            collection = collection
                .Where(StockTakeItem =>
                    !(StockTakeItem.Code.EndsWith("/WIP") || StockTakeItem.Code.EndsWith("/T") ||
                        StockTakeItem.Code.EndsWith("/SC") || StockTakeItem.Code.EndsWith("/PT") ||
                        StockTakeItem.Code.EndsWith("/PT"))
                );
            collection = collection
                .Where(StockTakeItem => !(StockTakeItem.Code.StartsWith("REP")));
            collection = collection
                .Where(StockTakeItem => !(StockTakeItem.ProductGroup == "REPAIRABLE ITEMS"));

            collection = collection.Where(StockTakeItem => (
                StockTakeItem.ProductGroup.Contains(pagedListParameters.SearchTerm) ||
                StockTakeItem.Code.Contains(pagedListParameters.SearchTerm) ||
                StockTakeItem.Description.Contains(pagedListParameters.SearchTerm)));

            collection = collection.OrderBy(StockTakeItem => StockTakeItem.DateOfLastStockCount);


            PagedList<StockTakeItemsDTO> List = PagedList<StockTakeItemsDTO>.Create(
                collection, pagedListParameters.CurrentPage, pagedListParameters.PageSize,
                pagedListParameters.SearchTerm);

            return List;
        }

        /// <response code="200">Successfully submitted stocktake</response>
        /// <response code="404">Could not find stocktake</response>
        [HttpPost]
        [Route("Submit")]
        public IActionResult SubmitStockTake(SubmitStockTakeDTO submitStockTake)
        {
            if (Sage.Warehouses
                .Where(Warehouse => Warehouse.Name == submitStockTake.Warehouse).Count() != 1)
            {
                HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                return new JsonResult(new ErrorViewModel()
                {
                    Message = $"Warehouse name is ambiguous, matches " +
                    $"{Sage.Warehouses.Where(Warehouse => Warehouse.Name == submitStockTake.Warehouse).Count()} results",
                    CorrelationId =
                        System.Diagnostics.Activity.Current?.Id ?? HttpContext?.TraceIdentifier
                });
            }
            Warehouse Warehouse = Sage.Warehouses
                .Where(Warehouse => Warehouse.Name == submitStockTake.Warehouse).Single();

            var StockTake = Titan.StockTakeHeaders
                .Where(StockTake => StockTake.STKID == submitStockTake.ID).Single();

            if (StockTake == null)
            {
                return NotFound();
            }

            StockTake.Name = submitStockTake.Name;
            StockTake.Warehouse = Warehouse.Name;
            StockTake.DateCreated = DateTime.Now;

            StockTake.StatusID = Titan.StockTakeStatuses
                .First(Status => Status.Name == "Entering values").StatusID;
            Titan.SaveChanges();

            return Ok();
        }

        /// <response code="200">Successfully deleted stocktake</response>
        /// <response code="404">Could not find stocktake</response>
        [HttpDelete]
        [Route("{id}/Delete")]
        public IActionResult DeleteStockTake(int id)
        {
            var StockTake = Titan.StockTakeHeaders
                .Where(StockTake => StockTake.STKID == id).Single();

            if (StockTake == null)
            {
                return NotFound();
            }

            StockTake.StatusID = Titan.StockTakeStatuses
                .First(Status => Status.Name == "Deleted").StatusID;
            Titan.SaveChanges();

            return Ok();
        }

        /// <response code="200">Successfully removes all items from stocktake</response>
        [HttpDelete]
        [Route("{id}/Reset/{Warehouse}")]
        public IActionResult ResetStockTake(string Warehouse, int id)
        {
            Titan.StockTakeDetails.RemoveRange(
                Titan.StockTakeDetails.Where(StockTakeItem => StockTakeItem.STKID == id));
            Titan.StockTakeHeaders
                .Where(StockTake => StockTake.STKID == id)
                .ToList()
                .ForEach(StockTake => StockTake.Warehouse = Warehouse);
            Titan.SaveChanges();
            return Ok();
        }

        /// <response code="200">Successfully returns a list of stocktakes,
        /// and their status</response>
        [HttpGet]
        [Route("List")]
        public StockTakeListDTO ListStockTakes()
        {
            List<StockTakeDTO> StockTakes = (from StockTake in Titan.StockTakeHeaders
                                             join Status in Titan.StockTakeStatuses
                                             on StockTake.StatusID equals Status.StatusID

                                             select new StockTakeDTO
                                             {
                                                 ID = StockTake.STKID,
                                                 Name = StockTake.Name,
                                                 Status = Status.Name,
                                                 CompletedBy = StockTake.CompletedBy,
                                                 CreatedBy = StockTake.CreatedBy,
                                                 DateCompleted = StockTake.DateCompleted,
                                                 DateCreated = StockTake.DateCreated,
                                                 Warehouse = StockTake.Warehouse
                                             }).ToList();

            List<StockTakeDTO> Active = new();
            List<StockTakeDTO> Completed = new();
            List<StockTakeDTO> Deleted = new();

            foreach (StockTakeDTO StockTake in StockTakes)
            {
                var StockTakeItems = Titan.StockTakeDetails
                    .Where(StockTakeItem => StockTakeItem.STKID == StockTake.ID);
                var numberOfItems = StockTakeItems.Count();
                var numberCompleted = StockTakeItems
                    .Where(StockTakeItem => StockTakeItem.RecordedValue != null).Count();

                StockTake.Completion = $"{numberCompleted}/{numberOfItems}";

                if (ActiveStatuses.Contains(StockTake.Status))
                {
                    Active.Add(StockTake);
                }
                else if (CompletedStatuses.Contains(StockTake.Status))
                {
                    Completed.Add(StockTake);
                }
                else if (DeletedStatuses.Contains(StockTake.Status))
                {
                    Deleted.Add(StockTake);
                }
            }

            return new StockTakeListDTO()
            {
                Active = Active,
                Complete = Completed,
                Deleted = Deleted
            };
        }

        /// <response code="200">Successfully returns the generated adjustments</response>
        /// <response code="404">Could not find stocktake</response>
        /// <response code="422">Could not generate adjustments because a value is missing</response>
        [HttpGet]
        [Route("{id}/Adjust")]
        public ActionResult<IEnumerable<StockAdjustDTO>> GetStocktakeAdjustments(int id)
        {
            var StockTake = Titan.StockTakeHeaders
                .SingleOrDefault(StockTake => StockTake.STKID == id);
            if (StockTake == null)
            {
                return NotFound();
            }

            var StockTakeStatus = Titan.StockTakeStatuses
                .Single(Status => Status.StatusID == StockTake.StatusID).Name;
            if (StockTakeStatus != "Entering values")
            {
                HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                return new JsonResult(new ErrorViewModel()
                {
                    Message = $"Only an active socktake in the Entering values state can apply " +
                    $"adjustments. This stocktake is in the {StockTakeStatus} state",
                    CorrelationId =
                    System.Diagnostics.Activity.Current?.Id ?? HttpContext?.TraceIdentifier
                });
            }

            var StockTakeItems = Titan.StockTakeDetails
                .Where(StockTakeItem => StockTakeItem.STKID == id).ToList();

            var Adjustments = new List<StockAdjustDTO>();
            StockTakeDetail StockCheckItem = null;

            try
            {
                foreach (StockTakeDetail Item in StockTakeItems)
                {
                    StockCheckItem = Item;

                    if (!TryGetSageInfo(id, Item.PartNumber, out Warehouse Warehouse,
                        out StockItem StockItem, out WarehouseItem WarehouseItem,
                        out string ValidationErrorMessage))
                    {
                        HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                        return new JsonResult(new ErrorViewModel()
                        {
                            Message = ValidationErrorMessage,
                            CorrelationId =
                            System.Diagnostics.Activity.Current?.Id ?? HttpContext?.TraceIdentifier
                        });
                    }

                    var RecordedValue = (decimal)Item.RecordedValue;

                    var CurrentSageQuantity = Sage.BinItems
                            .Where(BinItem => BinItem.ItemID == StockItem.ItemID)
                            .Where(BinItem =>
                                BinItem.WarehouseItemID == WarehouseItem.WarehouseItemID)
                            .Where(BinItem => BinItem.BinName == Item.Location)
                            .Select(BinItem =>
                                BinItem.ConfirmedQtyInStock + BinItem.UnconfirmedQtyInStock)
                            .Single();

                    Adjustments.Add(new StockAdjustDTO
                    {
                        CurrentSageQuantity = ((double?)CurrentSageQuantity),
                        SnapshotSageQuantity = (double?)Item.FreeStock,//CurrentSageQuantity,
                        ResultSageQuantity = (double?)RecordedValue,
                        Change = (double?)(RecordedValue - Item.FreeStock),
                        Code = Item.PartNumber,
                        BinName = Item.Location,
                        Warehouse = Warehouse.Name
                    });
                }
            }
            catch (Exception)
            {
                return UnprocessableEntity();
            }

            return Adjustments;
        }

        /// <response code="200">Successfully returns a report of what adjustments were applied</response>
        /// <response code="404">Could not find stocktake</response>
        [HttpPost]
        [Route("{id}/ApplyStocktakeAdjustments")]
        public async Task<ActionResult<AdjustResponseReportDTO>> ApplyStocktakeAdjustments(int id,
            List<StockAdjustDTO> stockAdjusts)
        {
            if (stockAdjusts == null)
            {
                stockAdjusts = new List<StockAdjustDTO>();
            }

            var StockTake = Titan.StockTakeHeaders.Single(StockTake => StockTake.STKID == id);

            if (StockTake == null)
            {
                return NotFound();
            }

            var StockTakeStatus = Titan.StockTakeStatuses
                .Single(Status => Status.StatusID == StockTake.StatusID).Name;
            if (StockTakeStatus != "Entering values")
            {
                HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                return new JsonResult(new ErrorViewModel()
                {
                    Message = $"Only an active socktake in the Entering values state can apply " +
                    $"adjustments. This stocktake is in the {StockTakeStatus} state",
                    CorrelationId =
                        System.Diagnostics.Activity.Current?.Id ?? HttpContext?.TraceIdentifier
                });
            }

            var AdjustmentReport = await SageAPI.AdjustStockMultipleAsync(id, stockAdjusts);

            if (AdjustmentReport.Success ?? false)
            {
                try
                {
                    var DateTimeNow = DateTime.Now;

                    // complete stocktake
                    StockTake.StatusID = Titan.StockTakeStatuses
                        .Single(Status => Status.Name == "Completed").StatusID;
                    StockTake.DateCompleted = DateTimeNow;
                    StockTake.CompletedBy = HttpContext.GetUserDisplayName();

                    Titan.SaveChanges();
                    AdjustmentReport.StockTakeClosed = true;
                }
                catch (Exception)
                {
                    AdjustmentReport.StockTakeClosed = false;
                }
            }

            return Ok(AdjustmentReport);
        }

        /// <response code="200">Successfully returns a list of bins to check for an item</response>
        [HttpGet]
        [Route("{Code}/LocationsInActiveStockTake")]
        public async Task<ActionResult<LocationDetailsDTO>>
            GetLocationsOfItemInStockTake(string Code)
        {
            // codes often have slashes - decode them since MVC doesn't
            Code = HttpUtility.UrlDecode(Code);

            var Details = await SageAPI.StockDetailsAsync(Code);

            var Locations = (from StockTakeHeader in Titan.StockTakeHeaders
                             join StockTakeDetail in Titan.StockTakeDetails
                             on StockTakeHeader.STKID equals StockTakeDetail.STKID
                             join StockTakeStatus in Titan.StockTakeStatuses
                             on StockTakeHeader.StatusID equals StockTakeStatus.StatusID
                             where StockTakeStatus.Name == "Entering values"
                             && StockTakeDetail.PartNumber == Code
                             select StockTakeDetail.Location).ToList();

            return new LocationDetailsDTO()
            {
                Code = Details.Code,
                Name = Details.Name,
                Locations = Locations
            };
        }

        /// <response code="200">Successfully records part count in stocktake</response>
        [HttpPut]
        [Route("{Code}/RecordValue/{Location}/{Amount}")]
        public async Task<ActionResult<StockResponseDTO>> StocktakeRecordValue(string Code,
            string Amount, string Location)
        {
            // codes often have slashes - decode them since MVC doesn't
            Code = HttpUtility.UrlDecode(Code);

            var ItemDetails = (from StockTakeHeader in Titan.StockTakeHeaders
                               join StockTakeDetail in Titan.StockTakeDetails
                               on StockTakeHeader.STKID equals StockTakeDetail.STKID
                               join StockTakeStatus in Titan.StockTakeStatuses
                               on StockTakeHeader.StatusID equals StockTakeStatus.StatusID
                               where StockTakeStatus.Name == "Entering values"
                               && StockTakeDetail.PartNumber == Code
                               select StockTakeDetail).ToList();

            if (ItemDetails.Select(StockTakeItem => StockTakeItem.STKID).Distinct().Count() > 1)
            {
                HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                return new JsonResult(new ErrorViewModel()
                {
                    Message = $"Cannot enter a value for code {HttpUtility.HtmlEncode(Code)}" +
                    $" because it is in multiple active stocktakes",
                    CorrelationId =
                        System.Diagnostics.Activity.Current?.Id ?? HttpContext?.TraceIdentifier
                });
            }

            var StockTakeID = ItemDetails.Select(StockTakeItem => StockTakeItem.STKID).Single();

            if (ItemDetails.Where(StockTakeItem => StockTakeItem.Location == Location &&
                    StockTakeItem.STKID == StockTakeID).Count() > 1)
            {
                HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                return new JsonResult(new ErrorViewModel()
                {
                    Message = $"Cannot enter a value for code " +
                    $"{HttpUtility.HtmlEncode(Code)} in location " +
                    $"{HttpUtility.HtmlEncode(Location)} because there are multiple records",
                    CorrelationId =
                        System.Diagnostics.Activity.Current?.Id ?? HttpContext?.TraceIdentifier
                });
            }

            if (!decimal.TryParse(Amount, out decimal RecordedValue))
            {
                HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                return new JsonResult(new ErrorViewModel()
                {
                    Message = $"Cannot read value {Amount} as a valid decimal number",
                    CorrelationId =
                        System.Diagnostics.Activity.Current?.Id ?? HttpContext?.TraceIdentifier
                });
            }

            var ItemToUpdate = ItemDetails
                .Where(StockTakeItem => StockTakeItem.Location == Location &&
                    StockTakeItem.STKID == StockTakeID).Single();
            ItemToUpdate.RecordedValue = RecordedValue;
            ItemToUpdate.Adjustment = ItemToUpdate.RecordedValue - ItemToUpdate.FreeStock;
            ItemToUpdate.RecordedDate = DateTime.Now;
            ItemToUpdate.RecordedBy = HttpContext.GetUserDisplayName();

            var ItemPrice = await SageAPI.PriceOfStockAsync(Code);

            ItemToUpdate.AdjustmentValue = ItemToUpdate.Adjustment * (decimal?)ItemPrice;

            Titan.PrintChanges();
            Titan.SaveChanges();

            return Ok(new StockResponseDTO
            {
                Message = $"Recorded {Amount} items of code " +
                $"<code>{HttpUtility.HtmlEncode(Code)}</code> in bin " +
                $"<code>{HttpUtility.HtmlEncode(Location)}</code>"
            });
        }


        /// <summary>
        /// Gets Sage data for a stocktake and part number, checking at each step to make sure given input is valid
        /// </summary>
        /// <param name="id">The stocktake id to fetch variables for</param>
        /// <param name="Code">The Sage part number</param>
        /// <param name="Warehouse">The Sage warehouse the stocktake is against</param>
        /// <param name="StockItem">The Sage StockItem matching the given part number</param>
        /// <param name="WarehouseItem">The Sage WarehouseItem matching the given part number</param>
        /// <param name="ValidationErrorMessage">The error message, if validation fails</param>
        /// <returns>Boolean value indicating success</returns>
        private bool TryGetSageInfo(int id, string Code, out Warehouse Warehouse,
            out StockItem StockItem, out WarehouseItem WarehouseItem,
            out string ValidationErrorMessage)
        {
            Warehouse = null;
            StockItem = null;
            WarehouseItem = null;
            ValidationErrorMessage = null;

            var StockTake = Titan.StockTakeHeaders
                .Where(StockTake => StockTake.STKID == id).Single();

            // find warehouse from stocktake
            if (Sage.Warehouses
                .Where(Warehouse => Warehouse.Name == StockTake.Warehouse).Count() != 1)
            {
                ValidationErrorMessage = $"Warehouse name is ambiguous, " +
                    $"matches {Sage.Warehouses.Where(Warehouse => Warehouse.Name == StockTake.Warehouse).Count()} results";
                return false;
            }
            Warehouse = Sage.Warehouses
                .Where(Warehouse => Warehouse.Name == StockTake.Warehouse).Single();

            // get stockitem from code
            if (Sage.StockItems.Where(StockItem => StockItem.Code == Code).Count() != 1)
            {
                ValidationErrorMessage = $"A selected item code " +
                    $"({HttpUtility.HtmlEncode(Code)}) is ambiguous, " +
                    $"matches {Sage.StockItems.Where(StockItem => StockItem.Code == Code).Count()} results";
                return false;
            }
            StockItem = Sage.StockItems.Where(StockItem => StockItem.Code == Code).Single();


            var WarehouseID = Warehouse.WarehouseID;
            var StockItemId = StockItem.ItemID;

            // get warehouseitem from stockitem and warehouse
            IQueryable<WarehouseItem> WarehouseItems = Sage.WarehouseItems
                .Where(WarehouseItem => WarehouseItem.WarehouseID == WarehouseID)
                .Where(WarehouseItem => WarehouseItem.ItemID == StockItemId);

            if (WarehouseItems.Count() != 1)
            {
                ValidationErrorMessage = $"Item code '{HttpUtility.HtmlEncode(Code)}'" +
                    $" {((WarehouseItems.Count() == 0) ? "cannot be found" : "is ambiguous")} " +
                    $"in warehouse '{HttpUtility.HtmlEncode(Warehouse.Name)}'";
                return false;
            }
            WarehouseItem = WarehouseItems.Single();

            return true;
        }
    }
}