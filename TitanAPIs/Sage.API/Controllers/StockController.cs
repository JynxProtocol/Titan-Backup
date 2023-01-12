using Sage.Accounting.Stock;
using Sage.Accounting.Stock.Views;
using Sage.Api.Data;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using Titan.Models.StockManagement;
using Titan.Models.StockTake;

namespace Sage.Api.Controllers
{
    [RoutePrefix("api/Stock")]
    public class StockController : ApiController
    {
        public SabreLive SageDB = new SabreLive();

        // using the object store transaction doesn't seem to do anything
        private static bool UseObjectStoreTransaction = false;

        /// <summary>
        /// This function encodes an input string to a JSON string representation, 
        /// escaping any HTML. It first encodes any special characters as a HTML representation, 
        /// then encodes the result as a javaScript string, e.g. escaping quotes. 
        /// Additionally, an XSS test string is added, which should be filtered out by 
        /// correct validation on clients. If it is not, there is an XSS vulnerability via Sage, 
        /// as well as by modifying API responses.
        /// </summary>
        /// <param name="str">The string to encode for transport</param>
        private static string ClientEncode(string str)
        {
            var XSSTest = "<script>alert(\"XSS security vulnerability in Sage.API. " +
                "Report to IT ASAP. Do not click OK\");</script>";
            return HttpUtility.JavaScriptStringEncode(HttpUtility.HtmlEncode(str + XSSTest));
        }

        [HttpGet]
        [Route("Exists", Name = nameof(DoesStockExist))]
        [SwaggerResponse(System.Net.HttpStatusCode.OK, Type = typeof(bool))]
        public bool DoesStockExist(string Code)
        {
            if (SageDB.StockItems.Any(StockItem => StockItem.Code == Code))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Retrieves the details for a given stock item, including the item's code, name,
        /// description, quantity, and the locations where it is stored.
        /// </summary>
        /// <param name="Code">The code of the stock item to retrieve details for.</param>
        /// <returns>
        /// If the stock item with the given code is found, returns a StockDetailsDTO object
        /// containing the item's details. Otherwise, returns a NotFound result.
        /// </returns>
        [HttpGet]
        [Route("Details", Name = nameof(StockDetails))]
        [SwaggerResponse(System.Net.HttpStatusCode.OK, Type = typeof(StockDetailsDTO))]
        [SwaggerResponse(System.Net.HttpStatusCode.NotFound, "Could not find item with given code")]
        public IHttpActionResult StockDetails(string Code)
        {
            try
            {
                var StockDetails = (from stockItem in SageDB.StockItems
                                    where stockItem.Code == Code

                                    select new
                                    {
                                        stockItem.Code,
                                        Amount = stockItem.FreeStockQuantity,
                                        stockItem.Name,
                                        KanbanQuantity = stockItem.AnalysisCode3,
                                        stockItem.Description
                                    }).FirstOrDefault();

                var BinLocations = (from binItem in SageDB.BinItems
                                    where binItem.StockItem.Code == Code
                                    && !(binItem.BinName.Contains("uns"))
                                    && !(binItem.BinName == "zoar")
                                    select binItem).AsEnumerable()
                                    .Select(binItem => new
                                    {
                                        Location = binItem.BinName,
                                        Amount = binItem.CalculateStockInBin()
                                    }).ToList();

                var internalAreas = (from internalArea in SageDB.InternalAreas
                                     select internalArea.InternalAreaName
                                     ).ToList();

                if (StockDetails == null || BinLocations.Count == 0)
                {
                    return NotFound();
                }
                else
                {
                    decimal MaxAmount = decimal.MinValue;
                    BinLocationDTO BestLocation = null;
                    List<BinLocationDTO> Locations = new List<BinLocationDTO>();

                    foreach (var binLocation in BinLocations)
                    {
                        var Location = new BinLocationDTO()
                        {
                            Name = binLocation.Location,
                            Amount = binLocation.Amount
                        };

                        Locations.Add(Location);

                        if (binLocation.Amount > MaxAmount)
                        {
                            MaxAmount = binLocation.Amount;
                            BestLocation = Location;
                        }
                    }


                    bool IsKanban =
                        int.TryParse(StockDetails.KanbanQuantity, out int KanbanQuantity);

                    var isEnoughStockToIssue = (MaxAmount >= KanbanQuantity);

                    var StockDetail = new StockDetailsDTO
                    {
                        Code = StockDetails.Code,
                        Description = StockDetails.Description,
                        KanbanQuantity = KanbanQuantity,
                        Quantity = isEnoughStockToIssue ? KanbanQuantity : MaxAmount,
                        IsEnoughStockToIssue = isEnoughStockToIssue,
                        Name = StockDetails.Name,
                        BestLocation = BestLocation,
                        Locations = Locations,
                        InternalAreas = internalAreas,
                        IsKanban = IsKanban
                    };

                    return Json(StockDetail);
                }
            }
            catch (Exception ex)
            {
                //do something here or create a global exception handler for owin
                Console.WriteLine(ex.GetType().FullName);
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        [HttpGet]
        [Route("Warehouse/{Warehouse}/Details", Name = nameof(StockDetailsByWarehouse))]
        [SwaggerResponse(System.Net.HttpStatusCode.OK, Type = typeof(StockDetailsDTO))]
        [SwaggerResponse(System.Net.HttpStatusCode.NotFound)]
        public IHttpActionResult StockDetailsByWarehouse(string Code, string Warehouse)
        {
            try
            {
                var StockDetails = (from stockItem in SageDB.StockItems
                                    join warehouseItem in SageDB.WarehouseItems
                                    on stockItem.ItemID equals warehouseItem.ItemID
                                    join warehouse in SageDB.Warehouses
                                    on warehouseItem.WarehouseID equals warehouse.WarehouseID
                                    join binItem in SageDB.BinItems
                                    on stockItem.ItemID equals binItem.ItemID
                                    where stockItem.Code == Code
                                    && binItem.WarehouseItemID == warehouseItem.WarehouseItemID
                                    && ((Warehouse == null) || (warehouse.Name == Warehouse))
                                    && !(binItem.BinName.Contains("uns"))
                                    && !(binItem.BinName == "zoar")

                                    select new
                                    {
                                        stockItem.Code,
                                        Amount = stockItem.FreeStockQuantity,
                                        stockItem.Name,
                                        KanbanQuantity = stockItem.AnalysisCode3,
                                        stockItem.Description
                                    }).FirstOrDefault();

                var BinLocations = (from binItem in SageDB.BinItems
                                    join warehouseItem in SageDB.WarehouseItems
                                    on binItem.WarehouseItemID equals warehouseItem.WarehouseItemID
                                    join warehouse in SageDB.Warehouses
                                    on warehouseItem.WarehouseID equals warehouse.WarehouseID
                                    where binItem.StockItem.Code == Code
                                    && ((Warehouse == null) || (warehouse.Name == Warehouse))
                                    && !(binItem.BinName.Contains("uns"))
                                    && !(binItem.BinName == "zoar")
                                    select binItem).AsEnumerable()
                                    .Select(binItem => new
                                    {
                                        Location = binItem.BinName,
                                        Amount = binItem.CalculateStockInBin()
                                    }).ToList();

                var internalAreas = (from internalArea in SageDB.InternalAreas
                                     select internalArea.InternalAreaName
                                     ).ToList();

                if (StockDetails == null || BinLocations.Count == 0)
                {
                    return NotFound();
                }
                else
                {
                    decimal MaxAmount = decimal.MinValue;
                    BinLocationDTO BestLocation = null;
                    List<BinLocationDTO> Locations = new List<BinLocationDTO>();

                    foreach (var binLocation in BinLocations)
                    {
                        var Location = new BinLocationDTO()
                        {
                            Name = binLocation.Location,
                            Amount = binLocation.Amount
                        };
                        Locations.Add(Location);

                        if (binLocation.Amount > MaxAmount)
                        {
                            MaxAmount = binLocation.Amount;
                            BestLocation = Location;
                        }
                    }


                    bool IsKanban =
                        int.TryParse(StockDetails.KanbanQuantity, out int KanbanQuantity);

                    var isEnoughStockToIssue = (MaxAmount >= KanbanQuantity);

                    var StockDetail = new StockDetailsDTO
                    {
                        Code = StockDetails.Code,
                        Description = StockDetails.Description,
                        KanbanQuantity = KanbanQuantity,
                        Quantity = isEnoughStockToIssue ? KanbanQuantity : MaxAmount,
                        IsEnoughStockToIssue = isEnoughStockToIssue,
                        Name = StockDetails.Name,
                        BestLocation = BestLocation,
                        Locations = Locations,
                        InternalAreas = internalAreas,
                        IsKanban = IsKanban
                    };

                    return Json(StockDetail);
                }
            }
            catch (Exception ex)
            {
                //do something here or create a global exception handler for owin
                Console.WriteLine(ex.GetType().FullName);
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        private static Accounting.Stock.StockItem GetStockItem(string Code)
        {
            // Get the stock item for the movement.
            StockItems stockItems = new StockItems();
            return stockItems[Code];
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public static bool TryGetBinItemWarehouseView(Accounting.Stock.StockItem stockItem,
            string Warehouse, string BinName, decimal StockNeeded,
            out BinItemWarehouseView BinItemWarehouseView)
        {
            ObjectStore.Query query = new ObjectStore.Query();

            // Find the warehouse and bin
            query.Filters.Add(new ObjectStore.Filter(
                BinItemWarehouseView.FIELD_ITEM,
                stockItem.PrimaryKey.GetDbKey()));

            query.Filters.Add(new ObjectStore.Filter(
                BinItemWarehouseView.FIELD_NAME,
                Warehouse));

            // unspecified bin names should be *unspecified* in the query
            if (BinName != "Unspecified")
            {
                query.Filters.Add(new ObjectStore.Filter(
                    BinItemWarehouseView.FIELD_BINNAME,
                    BinName));
            }

            BinItemWarehouseViews binItemWarehouseViews =
                new BinItemWarehouseViews();

            binItemWarehouseViews.Find(query);

            // select all bins that fit the warehouse, stock item and bin name specified
            var BinOptions = binItemWarehouseViews.Cast<BinItemWarehouseView>();

            // go through bins by descending priority
            for (int Priority = 9; Priority > 0; Priority--)
            {
                // the current options for priority
                var PriorityOptions = BinOptions
                    .Where(BIWV => BIWV.BinItem.AllocationPriority == Priority);

                // select only bins that have enough stock to fulfil the action
                var OptionsWithEnoughStock = PriorityOptions
                    .Where(BIWV => BIWV.BinItem.CalculateStockInBin() >= StockNeeded);

                // if there are no options, next priority
                if (OptionsWithEnoughStock.Count() == 0)
                {
                    continue;
                }
                else if (OptionsWithEnoughStock.Count() == 1)
                {
                    // if there is one, return it
                    BinItemWarehouseView = OptionsWithEnoughStock.Single();
                    return true;
                }
                else
                {
                    // otherwise, tiebreak by amount of stock, then by name
                    BinItemWarehouseView = OptionsWithEnoughStock
                        .OrderBy(BIWV => BIWV.BinItem.CalculateStockInBin())
                        .ThenBy(BIWV => BIWV.BinItem.BinName)
                        .First();
                    return true;
                }
            }

            // if we have eliminated all priorities, there is no bin fitting our requirements
            BinItemWarehouseView = null;
            return false;
        }

        private static void SetSecondaryActivity<T>(T activity,
            SecondaryActivityTypeEnum secondaryActivityType)
        {
            // Use Reflection to overide the Secondary Activity Property
            PropertyInfo pi = activity.GetType()
                .GetProperty("SecondaryActivityTypeProtected",
                    BindingFlags.Instance | BindingFlags.NonPublic);

            if (pi != null)
            {
                pi.SetValue(activity, secondaryActivityType);
            }
        }

        // POST: Stock/Add
        [HttpPost]
        [Route("Add", Name = nameof(AddStock))]
        [SwaggerResponse(System.Net.HttpStatusCode.OK, Type = typeof(StockResponseDTO))]
        public IHttpActionResult AddStock(string Code, [FromBody] AddStockDTO addStockDTO)
        {
            return Json(AddStockNonTraceable(Code, addStockDTO));
        }

        // POST: Stock/Add
        [HttpPost]
        [SwaggerResponse(System.Net.HttpStatusCode.OK, Type = typeof(StockResponseDTO))]
        [Route("Add/Stocktake", Name = nameof(AddStockStocktake))]
        public IHttpActionResult AddStockStocktake(string Code, [FromBody] AddStockDTO addStockDTO)
        {
            return Json(AddStockNonTraceable(Code, addStockDTO,
                SecondaryActivityTypeEnum.EnumSecondaryActivityTypeStockTakeAdjustmentIn));
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public static StockResponseDTO AddStockNonTraceable(string Code, AddStockDTO addStockDTO,
            SecondaryActivityTypeEnum secondaryActivityType =
            SecondaryActivityTypeEnum.EnumSecondaryActivityTypeDiscoveryGoodsIn)
        {
            // Create a new instance of the movement transaction.
            StockDiscoveryGoodsInMovementActivity stockDiscoveryGoodsInMovementActivity =
                new StockDiscoveryGoodsInMovementActivity();

            try
            {
                // Get the stock item for the movement.
                Accounting.Stock.StockItem stockItem = GetStockItem(Code);

                //Set Allowable Warnings
                Accounting.Application.AllowableWarnings.Add(stockDiscoveryGoodsInMovementActivity,
                    typeof(Accounting.Exceptions.StockItemHoldingExceedsMaximumException));

                var BinName = ClientEncode(addStockDTO.BinName);
                var Warehouse = ClientEncode(addStockDTO.Warehouse);
                var StockItemName = ClientEncode(stockItem.Name);
                var StockCode = ClientEncode(Code);

                if (!TryGetBinItemWarehouseView(stockItem, addStockDTO.Warehouse,
                    addStockDTO.BinName, 0, out BinItemWarehouseView BinItemWarehouseView))
                {
                    return (new StockResponseDTO()
                    {
                        Changed = false,
                        Success = false,
                        Message = "<h3>" +
                        $"Could not find bin <code>{BinName}</code> in warehouse '{Warehouse}'" +
                        $" for stock <code>{StockItemName} ({StockCode})</code>"
                        + "</h3>"
                    });
                }

                // Set the movement stock item.
                stockDiscoveryGoodsInMovementActivity.StockItem = stockItem;

                // Set the bin to which the item quantity is to be added.
                stockDiscoveryGoodsInMovementActivity.Location = BinItemWarehouseView.BinItem;

                // Set the number of items involved in the movement.
                stockDiscoveryGoodsInMovementActivity.Quantity = addStockDTO.Quantity;

                // Optional: set movement date.
                stockDiscoveryGoodsInMovementActivity.ActivityDate = Common.Clock.Today;

                // Optional: set references.
                stockDiscoveryGoodsInMovementActivity.Reference = addStockDTO.Ref;
                stockDiscoveryGoodsInMovementActivity.SecondReference = addStockDTO.SecondRef;

                // set secondary activity through reflection
                SetSecondaryActivity(stockDiscoveryGoodsInMovementActivity, secondaryActivityType);

                // using the object store transaction doesn't seem to do anything
                if (UseObjectStoreTransaction)
                {
                    // Save the stock addition to the database.
                    using (ObjectStore.PersistentObjectTransaction persistentObjectTransaction =
                                new ObjectStore.PersistentObjectTransaction())
                    {
                        stockDiscoveryGoodsInMovementActivity.Update();

                        persistentObjectTransaction.Commit();
                    }
                }
                else
                {
                    // Save the stock addition to the database.
                    stockDiscoveryGoodsInMovementActivity.Update();
                }

                return (new StockResponseDTO()
                {
                    Changed = true,
                    Success = true,
                    Message = "<h3>" +
                    $"Successfully added {addStockDTO.Quantity} of stock <code>{StockItemName} " +
                    $"({StockCode})</code> to bin <code>{BinName}</code>"
                    + "</h3>"
                });
            }
            catch (Exception ex)
            {
                stockDiscoveryGoodsInMovementActivity.DiscardChanges();

                return (new StockResponseDTO()
                {
                    Changed = true,
                    Success = false,
                    Message = "<h3>" +
                    $"Failed to add {addStockDTO.Quantity} stock of code " +
                    $"<code>{ClientEncode(Code)}</code> due to an error of type " +
                    $"<code>{ClientEncode(ex.GetType().FullName)}</code>"
                    + "</h3>"
                });
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public static TraceableStockResponseDTO AddStockTraceable(string Code,
            AddStockDTO addStockDTO,
            SecondaryActivityTypeEnum secondaryActivityType =
            SecondaryActivityTypeEnum.EnumSecondaryActivityTypeDiscoveryGoodsIn)
        {
            // Create a new instance of the movement transaction.
            StockDiscoveryGoodsInMovementActivity stockDiscoveryGoodsInMovementActivity =
                new StockDiscoveryGoodsInMovementActivity();

            try
            {
                // Get the stock item for the movement.
                Accounting.Stock.StockItem stockItem = GetStockItem(Code);

                //Set Allowable Warnings
                Accounting.Application.AllowableWarnings.Add(stockDiscoveryGoodsInMovementActivity,
                    typeof(Accounting.Exceptions.StockItemHoldingExceedsMaximumException));

                var BinName = ClientEncode(addStockDTO.BinName);
                var Warehouse = ClientEncode(addStockDTO.Warehouse);
                var StockItemName = ClientEncode(stockItem.Name);
                var StockCode = ClientEncode(Code);

                if (!stockItem.RecordNosOnGoodsReceived)
                {
                    return (new TraceableStockResponseDTO()
                    {
                        Changed = false,
                        Success = false,
                        Message = "<h3>" +
                        $"Cannot add traceable stock for stock " +
                        $"<code>{StockItemName} ({StockCode})</code> " +
                        $"because recording numbers is not enabled on goods recieved"
                        + "</h3>"
                    });
                }

                if (!TryGetBinItemWarehouseView(stockItem, addStockDTO.Warehouse,
                    addStockDTO.BinName, 0, out BinItemWarehouseView BinItemWarehouseView))
                {
                    return (new TraceableStockResponseDTO()
                    {
                        Changed = false,
                        Success = false,
                        Message = "<h3>" +
                        $"Could not find bin <code>{BinName}</code> in warehouse '{Warehouse}'" +
                        $" for stock <code>{StockItemName} ({StockCode})</code>"
                        + "</h3>"
                    });
                }

                // Set the movement stock item.
                stockDiscoveryGoodsInMovementActivity.StockItem = stockItem;

                // Set the bin to which the item quantity is to be added.
                stockDiscoveryGoodsInMovementActivity.Location = BinItemWarehouseView.BinItem;

                // Set the number of items involved in the movement.
                stockDiscoveryGoodsInMovementActivity.Quantity = addStockDTO.Quantity;

                // Optional: set movement date.
                stockDiscoveryGoodsInMovementActivity.ActivityDate = Common.Clock.Today;

                // Optional: set references.
                stockDiscoveryGoodsInMovementActivity.Reference = addStockDTO.Ref;
                stockDiscoveryGoodsInMovementActivity.SecondReference = addStockDTO.SecondRef;

                // set secondary activity through reflection
                SetSecondaryActivity(stockDiscoveryGoodsInMovementActivity, secondaryActivityType);

                // Add serial items
                var BatchNo = AutoGenerateSerialItems(stockDiscoveryGoodsInMovementActivity);

                // using the object store transaction doesn't seem to do anything
                if (UseObjectStoreTransaction)
                {
                    // Save the stock addition to the database.
                    using (ObjectStore.PersistentObjectTransaction persistentObjectTransaction =
                                new ObjectStore.PersistentObjectTransaction())
                    {
                        stockDiscoveryGoodsInMovementActivity.Update();

                        persistentObjectTransaction.Commit();
                    }
                }
                else
                {
                    // Save the stock addition to the database.
                    stockDiscoveryGoodsInMovementActivity.Update();
                }

                return (new TraceableStockResponseDTO()
                {
                    Changed = true,
                    Success = true,
                    BatchNo = BatchNo,
                    Message = "<h3>" +
                    $"Successfully added {addStockDTO.Quantity} of stock <code>{StockItemName} " +
                    $"({StockCode})</code> to bin <code>{BinName}</code>"
                    + "</h3>"
                });
            }
            catch (Exception ex)
            {
                stockDiscoveryGoodsInMovementActivity.DiscardChanges();

                return (new TraceableStockResponseDTO()
                {
                    Changed = true,
                    Success = false,
                    Message = "<h3>" +
                    $"Failed to add {addStockDTO.Quantity} stock of code " +
                    $"<code>{ClientEncode(Code)}</code> due to an error of type " +
                    $"<code>{ClientEncode(ex.GetType().FullName)}</code>"
                    + "</h3>"
                });
            }
        }

        private static string AutoGenerateSerialItems(
            StockDiscoveryGoodsInMovementActivity activity)
        {
            // Get the traceable adjustment to record the new serial items
            TraceableGoodsInAdjustment
                traceableGoodsInAdjustment =
                (TraceableGoodsInAdjustment)
                activity.TraceableAdjustment;

            // Generate serial numbers starting with 10003
            traceableGoodsInAdjustment.AutoGenerateNumbers(10003, activity.Quantity);

            // Create attributes for the serial items created
            CreateDefaultAttributes(traceableGoodsInAdjustment);

            // Call confirm to validate the number of serial items against
            // the quantity added to stock
            traceableGoodsInAdjustment.Confirm();

            return traceableGoodsInAdjustment.AdjustmentItems.First.IdentificationNo;
        }

        private static void CreateDefaultAttributes(TraceableGoodsInAdjustment adjustment)
        {
            foreach (TraceableAdjustmentItem traceableAdjustmentItem in
                adjustment.AdjustmentItems)
            {
                // Copy and create attributes for the traceable item from the compulsory attributes
                // of the product group that the stock item is linked to.
                // The final 'false' parameter tells the coordinator to add any missing attributes
                // when Update() is called
                STKTraceItemBatchAttributeCoordinator traceItemBatchAttributeCoordinator =
                new STKTraceItemBatchAttributeCoordinator(adjustment.StockItem,
                    traceableAdjustmentItem.TraceableItem, traceableAdjustmentItem.IdentificationNo, false);

                traceItemBatchAttributeCoordinator.Update();

                // If you wish to make sure the attributes have been correctly populated call
                // the method ValidateAttributes.The method throws Ex21228Exception when one
                // or more compulsory attributes have blank values or when no attribute
                // has been created for the traceable item when we have compulsory attributes
                // defined for the product group that the stock item is linked to.
                TraceableItem traceableItem = new TraceableItem();
                traceableItem.Read(traceItemBatchAttributeCoordinator.TraceableItemID);

                traceableItem.ValidateAttributes();
            }
        }

        [HttpPost]
        [Route("Issue", Name = nameof(IssueStock))]
        [SwaggerResponse(System.Net.HttpStatusCode.OK, Type = typeof(StockResponseDTO))]
        public IHttpActionResult IssueStock(string Code, [FromBody] IssueStockDTO issueStockDTO)
        {
            return Json(IssueStockNonTraceable(Code, issueStockDTO));
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public static StockResponseDTO IssueStockNonTraceable(string Code, IssueStockDTO issueStockDTO)
        {
            // Create a new StockInternalGoodsOutMovementActivity instance
            StockInternalGoodsOutMovementActivity stockInternalGoodsOutMovementActivity =
                new StockInternalGoodsOutMovementActivity();

            try
            {
                // Get the stock item for the movement.
                Accounting.Stock.StockItem stockItem = GetStockItem(Code);

                // Get the internal area that the stock item is to be issued to.
                InternalAreas internalAreas = new InternalAreas();
                Accounting.Stock.InternalArea internalArea =
                    internalAreas[issueStockDTO.InternalArea];

                // Add to the allowable warning that the stock will
                // go below the minimum and reorder and levels
                Accounting.Application.AllowableWarnings.Add(stockInternalGoodsOutMovementActivity,
                  typeof(Accounting.Exceptions.StockItemHoldingBelowMinimumLevelException));
                Accounting.Application.AllowableWarnings.Add(stockInternalGoodsOutMovementActivity,
                  typeof(Accounting.Exceptions.StockItemHoldingBelowReorderLevelException));

                var BinName = ClientEncode(issueStockDTO.BinName);
                var Warehouse = ClientEncode(issueStockDTO.Warehouse);
                var StockItemName = ClientEncode(stockItem.Name);
                var StockCode = ClientEncode(Code);
                var InternalArea = ClientEncode(issueStockDTO.InternalArea);

                if (!TryGetBinItemWarehouseView(stockItem, issueStockDTO.Warehouse,
                    issueStockDTO.BinName, issueStockDTO.Quantity,
                    out BinItemWarehouseView BinItemWarehouseView))
                {
                    return (new StockResponseDTO()
                    {
                        Changed = false,
                        Success = false,
                        Message = "<h3>" +
                        $"Could not find bin <code>{BinName}</code> in warehouse '{Warehouse}' " +
                        $"with {issueStockDTO.Quantity} stock " +

                        $"for stock <code>{StockItemName} ({StockCode})</code>"
                        + "</h3>"
                    });
                }

                // Set the movement stock item.
                stockInternalGoodsOutMovementActivity.StockItem = stockItem;

                // Set the bin to which the item quantity is to be added.
                stockInternalGoodsOutMovementActivity.Location = BinItemWarehouseView.BinItem;

                // Set the number of items involved in the movement.
                stockInternalGoodsOutMovementActivity.Quantity = issueStockDTO.Quantity;

                // Optional: set movement date.
                stockInternalGoodsOutMovementActivity.ActivityDate = Common.Clock.Today;

                // Optional: set references.
                stockInternalGoodsOutMovementActivity.Reference = issueStockDTO.Ref;
                stockInternalGoodsOutMovementActivity.SecondReference = issueStockDTO.SecondRef;

                // Set the internal location receiving the stock item.
                stockInternalGoodsOutMovementActivity.Recipient = internalArea;

                // Save the stock addition to the database.
                stockInternalGoodsOutMovementActivity.Update();

                return (new StockResponseDTO()
                {
                    Changed = true,
                    Success = true,
                    Message = "<h3>" +
                    $"Successfully issued {issueStockDTO.Quantity} of stock <code>{StockItemName}" +
                    $" ({StockCode})</code> from bin <code>{BinName}</code> to {InternalArea}"
                    + "</h3>"
                });
            }
            catch (Exception ex)
            {
                stockInternalGoodsOutMovementActivity.DiscardChanges();

                return (new StockResponseDTO()
                {
                    Changed = true,
                    Success = false,
                    Message = "<h3>" +
                    $"Failed to issue {issueStockDTO.Quantity} stock of code " +
                    $"<code>{ClientEncode(Code)}</code> due to an error of type " +
                    $"<code>{ClientEncode(ex.GetType().FullName)}</code>"
                    + "</h3>"
                });
            }
        }

        [HttpPost]
        [Route("WriteOff", Name = nameof(WriteOffStock))]
        [SwaggerResponse(System.Net.HttpStatusCode.OK, Type = typeof(StockResponseDTO))]
        public IHttpActionResult WriteOffStock(string Code,
            [FromBody] WriteOffStockDTO writeOffStockDTO)
        {
            return Json(WriteOffStockNonTraceable(Code, writeOffStockDTO));
        }

        [HttpPost]
        [Route("WriteOff/Stocktake", Name = nameof(WriteOffStockStocktake))]
        [SwaggerResponse(System.Net.HttpStatusCode.OK, Type = typeof(StockResponseDTO))]
        public IHttpActionResult WriteOffStockStocktake(string Code,
            [FromBody] WriteOffStockDTO writeOffStockDTO)
        {
            return Json(WriteOffStockNonTraceable(Code, writeOffStockDTO,
                SecondaryActivityTypeEnum.EnumSecondaryActivityTypeStockTakeAdjustmentOut));
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public static StockResponseDTO WriteOffStockNonTraceable(string Code,
            WriteOffStockDTO writeOffStockDTO,
            SecondaryActivityTypeEnum secondaryActivityType =
            SecondaryActivityTypeEnum.EnumSecondaryActivityTypeInternalGoodsOut)
        {
            // Create a new StockInternalGoodsOutMovementActivity instance
            StockWriteOffMovementActivity stockWriteOffMovementActivity =
                new StockWriteOffMovementActivity();

            try
            {
                // Get the stock item for the movement.
                Accounting.Stock.StockItem stockItem = GetStockItem(Code);

                var BinName = ClientEncode(writeOffStockDTO.BinName);
                var Warehouse = ClientEncode(writeOffStockDTO.Warehouse);
                var StockItemName = ClientEncode(stockItem.Name);
                var StockCode = ClientEncode(Code);

                if (!TryGetBinItemWarehouseView(stockItem, writeOffStockDTO.Warehouse,
                    writeOffStockDTO.BinName, writeOffStockDTO.Quantity,
                    out BinItemWarehouseView BinItemWarehouseView))
                {
                    return (new StockResponseDTO()
                    {
                        Changed = false,
                        Success = false,
                        Message = "<h3>" +
                        $"Could not find bin <code>{BinName}</code> in warehouse '{Warehouse}'" +
                        $"with {writeOffStockDTO.Quantity} stock " +
                        $" for stock <code>{StockItemName} ({StockCode})</code>"
                        + "</h3>"
                    });
                }

                // Get the write-off category
                WriteOffCategories writeOffCategories =
                    new WriteOffCategories();

                writeOffCategories.Query.Filters.Add(new ObjectStore.Filter(
                    WriteOffCategory.FIELD_NAME, "written-off"));

                WriteOffCategory writeOffCategory = writeOffCategories.First;

                // Set the movement stock item.
                stockWriteOffMovementActivity.StockItem = stockItem;

                // Set the bin to which the item quantity is to be added.
                stockWriteOffMovementActivity.Location = BinItemWarehouseView.BinItem;

                // Set the number of items involved in the movement.
                stockWriteOffMovementActivity.Quantity = writeOffStockDTO.Quantity;

                stockWriteOffMovementActivity.Recipient = writeOffCategory;

                // Optional: set movement date.
                stockWriteOffMovementActivity.ActivityDate = Common.Clock.Today;

                // Optional: set references.
                stockWriteOffMovementActivity.Reference = writeOffStockDTO.Ref;
                stockWriteOffMovementActivity.SecondReference = writeOffStockDTO.SecondRef;

                // set secondary activity through reflection
                SetSecondaryActivity(stockWriteOffMovementActivity, secondaryActivityType);

                // using the object store transaction doesn't seem to do anything
                if (UseObjectStoreTransaction)
                {
                    // Save the stock addition to the database.
                    using (ObjectStore.PersistentObjectTransaction persistentObjectTransaction =
                                new ObjectStore.PersistentObjectTransaction())
                    {
                        stockWriteOffMovementActivity.Update();

                        persistentObjectTransaction.Commit();
                    }
                }
                else
                {
                    // Save the stock addition to the database.
                    stockWriteOffMovementActivity.Update();
                }

                return (new StockResponseDTO()
                {
                    Changed = true,
                    Success = true,
                    Message = "<h3>" +
                    $"Successfully wrote off {writeOffStockDTO.Quantity} of stock " +
                    $"<code>{StockItemName} ({StockCode})</code> from bin " +
                    $"<code>{BinName}</code> to {writeOffCategory}"
                    + "</h3>"
                });
            }
            catch (Exception ex)
            {
                stockWriteOffMovementActivity.DiscardChanges();

                return (new StockResponseDTO()
                {
                    Changed = true,
                    Success = false,
                    Message = "<h3>" +
                    $"Failed to write off {writeOffStockDTO.Quantity} stock of code " +
                    $"<code>{ClientEncode(Code)}</code> due to an error of type " +
                    $"<code>{ClientEncode(ex.GetType().FullName)}</code>"
                    + "</h3>"
                });
            }
        }

        [HttpPost]
        [Route("{id}/ApplyStocktakeAdjustments", Name = nameof(AdjustStockMultiple))]
        [SwaggerResponse(System.Net.HttpStatusCode.OK, Type = typeof(AdjustResponseReportDTO))]
        public IHttpActionResult AdjustStockMultiple(int id,
            [FromBody] List<StockAdjustDTO> stockAdjustDTOs)
        {
            var Report = new List<AdjustResponseDTO>();

            foreach (StockAdjustDTO stockAdjustDTO in stockAdjustDTOs)
            {
                var Result = AdjustStock(stockAdjustDTO);
                Report.Add(new AdjustResponseDTO()
                {
                    Code = stockAdjustDTO.Code,
                    Bin = stockAdjustDTO.BinName,
                    Success = Result.Success,
                    Message = Result.Message
                });
            }

            var Success = !Report.Any(Adjustment => !Adjustment.Success);

            return Json(new AdjustResponseReportDTO() { Report = Report, Success = Success });
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public static StockResponseDTO AdjustStock(StockAdjustDTO stockAdjustDTO)
        {
            try
            {
                // Get the stock item for the movement.
                Accounting.Stock.StockItem stockItem = GetStockItem(stockAdjustDTO.Code);

                var BinName = ClientEncode(stockAdjustDTO.BinName);
                var Warehouse = ClientEncode(stockAdjustDTO.Warehouse);
                var StockItemName = ClientEncode(stockItem.Name);
                var StockCode = ClientEncode(stockAdjustDTO.Code);
                var BeforeQty = ClientEncode(stockAdjustDTO.SnapshotSageQuantity.ToString());
                var AfterQty = ClientEncode(stockAdjustDTO.ResultSageQuantity.ToString());

                if (!TryGetBinItemWarehouseView(stockItem, stockAdjustDTO.Warehouse,
                    stockAdjustDTO.BinName, 0, out BinItemWarehouseView BinItemWarehouseView))
                {
                    return (new StockResponseDTO()
                    {
                        Changed = false,
                        Success = false,
                        Message = "<h3>" +
                        $"Could not find bin <code>{BinName}</code> in warehouse '{Warehouse}'" +
                        $" for stock <code>{StockItemName} ({StockCode})</code>"
                        + "</h3>"
                    });
                }

                var CurrentStockInSage = BinItemWarehouseView.BinItem.CalculateStockInBin();
                var ChangeIsPositive = (stockAdjustDTO.Change > 0);
                var ChangeIsNegative = (stockAdjustDTO.Change < 0);
                var ChangeWillBringStockBelowZero =
                    (CurrentStockInSage + stockAdjustDTO.Change < 0);

                if (ChangeIsPositive)
                {
                    // Stock is higher than sage thinks
                    return AddStockNonTraceable(stockAdjustDTO.Code, new AddStockDTO
                    {
                        Quantity = stockAdjustDTO.Change,
                        BinName = stockAdjustDTO.BinName,
                        Warehouse = stockAdjustDTO.Warehouse,
                        Ref = stockAdjustDTO.Ref,
                        SecondRef = stockAdjustDTO.SecondRef
                    }, SecondaryActivityTypeEnum.EnumSecondaryActivityTypeStockTakeAdjustmentIn);
                }
                else if (ChangeIsNegative && !ChangeWillBringStockBelowZero)
                {
                    // Stock is lower than sage thinks
                    return WriteOffStockNonTraceable(stockAdjustDTO.Code, new WriteOffStockDTO
                    {
                        Quantity = -stockAdjustDTO.Change,
                        BinName = stockAdjustDTO.BinName,
                        Warehouse = stockAdjustDTO.Warehouse,
                        Ref = stockAdjustDTO.Ref,
                        SecondRef = stockAdjustDTO.SecondRef
                    }, SecondaryActivityTypeEnum.EnumSecondaryActivityTypeStockTakeAdjustmentOut);
                }
                else if (ChangeWillBringStockBelowZero && CurrentStockInSage != 0)
                {
                    // Stock is higher than sage thinks, but the correction would go below zero
                    // So write off all remaining stock, unless its already zero
                    return WriteOffStockNonTraceable(stockAdjustDTO.Code, new WriteOffStockDTO
                    {
                        Quantity = CurrentStockInSage,
                        BinName = stockAdjustDTO.BinName,
                        Warehouse = stockAdjustDTO.Warehouse,
                        Ref = stockAdjustDTO.Ref,
                        SecondRef = stockAdjustDTO.SecondRef
                    }, SecondaryActivityTypeEnum.EnumSecondaryActivityTypeStockTakeAdjustmentOut);
                }
                else
                {
                    // No changes needed, either Change is zero or Change would bring
                    // stock below zero and stock is already zero
                    return new StockResponseDTO()
                    {
                        Changed = false,
                        Success = true,
                        Message = "<h3>" +
                        $"Stock <code>{StockItemName} ({StockCode})</code> in bin " +
                        $"<code>{BinName}</code> is unchanged at {BeforeQty}"
                        + "</h3>"
                    };
                }
            }
            catch (Exception ex)
            {
                return new StockResponseDTO()
                {
                    Changed = true,
                    Success = false,
                    Message = "<h3>" +
                    $"Failed to adjust stock of code " +
                    $"<code>{ClientEncode(stockAdjustDTO.Code)}</code> due to an error of type " +
                    $"<code>{ClientEncode(ex.GetType().FullName)}</code>" +
                    "</h3>"
                };
            }
        }

        [HttpPut]
        [Route("Correct", Name = nameof(CorrectStock))]
        [SwaggerResponse(System.Net.HttpStatusCode.OK, Type = typeof(StockResponseDTO))]
        public IHttpActionResult CorrectStock(string Code,
            [FromBody] StockCorrectDTO stockCorrectDTO)
        {
            return Json(CorrectStockNonTraceable(Code, stockCorrectDTO));
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public static StockResponseDTO CorrectStockNonTraceable(string Code,
            StockCorrectDTO stockCorrectDTO)
        {
            try
            {
                // Get the stock item for the movement.
                Accounting.Stock.StockItem stockItem = GetStockItem(Code);

                var BinName = ClientEncode(stockCorrectDTO.BinName);
                var Warehouse = ClientEncode(stockCorrectDTO.Warehouse);
                var StockItemName = ClientEncode(stockItem.Name);
                var StockCode = ClientEncode(Code);

                if (!TryGetBinItemWarehouseView(stockItem, stockCorrectDTO.Warehouse,
                    stockCorrectDTO.BinName, 0, out BinItemWarehouseView BinItemWarehouseView))
                {
                    return (new StockResponseDTO()
                    {
                        Changed = false,
                        Success = false,
                        Message = "<h3>" +
                        $"Could not find bin <code>{BinName}</code> in warehouse '{Warehouse}'" +
                        $" for stock <code>{StockItemName} ({StockCode})</code>"
                        + "</h3>"
                    });
                }

                var StockInSage = BinItemWarehouseView.BinItem.CalculateStockInBin();
                var StockDifference = Math.Abs(stockCorrectDTO.Amount - StockInSage);
                StockResponseDTO result;

                if (stockCorrectDTO.Amount > StockInSage)
                {
                    // Stock is higher than sage thinks
                    result = AddStockNonTraceable(Code, new AddStockDTO
                    {
                        Quantity = StockDifference,
                        BinName = stockCorrectDTO.BinName,
                        Warehouse = stockCorrectDTO.Warehouse,
                        Ref = stockCorrectDTO.Ref,
                        SecondRef = stockCorrectDTO.SecondRef
                    });
                }
                else if (stockCorrectDTO.Amount < StockInSage)
                {
                    // Stock is lower than sage thinks
                    result = IssueStockNonTraceable(Code, new IssueStockDTO
                    {
                        Quantity = StockDifference,
                        BinName = stockCorrectDTO.BinName,
                        Warehouse = stockCorrectDTO.Warehouse,
                        Ref = stockCorrectDTO.Ref,
                        SecondRef = stockCorrectDTO.SecondRef,
                        InternalArea = stockCorrectDTO.InternalArea
                    });
                }
                else
                {
                    return (new StockResponseDTO()
                    {
                        Changed = false,
                        Success = true,
                        Message = "<h3>" +
                        $"Stock <code>{StockItemName} ({StockCode})</code> in bin " +
                        $"<code>{BinName}</code> is unchanged at {stockCorrectDTO.Amount}"
                        + "</h3>"
                    });
                }

                if (result.Success)
                {
                    return (new StockResponseDTO()
                    {
                        Changed = true,
                        Success = true,
                        Message = "<h3>" +
                       $"Corrected stock <code>{StockItemName} ({StockCode})</code> in bin " +
                       $"<code>{BinName}</code> from {StockInSage} to {stockCorrectDTO.Amount}"
                       + "</h3>"
                    });
                }
                else
                {
                    return result;
                }
            }
            catch (Exception ex)
            {
                return (new StockResponseDTO()
                {
                    Changed = true,
                    Success = false,
                    Message = "<h3>" +
                    $"Failed to correct stock of code <code>{ClientEncode(Code)}</code> due " +
                    $"to an error of type <code>{ClientEncode(ex.GetType().FullName)}</code>" +
                    "</h3>"
                });
            }
        }

        [HttpGet]
        [Route("ItemPrice", Name = nameof(PriceOfStock))]
        [SwaggerResponse(System.Net.HttpStatusCode.OK, Type = typeof(decimal))]
        public decimal? PriceOfStock(string Code)
        {
            return SageDB.StockItems
                .FirstOrDefault(StockItem => StockItem.Code == Code)?.AverageBuyingPrice;
        }

    }

    internal static class BinItemExtensions
    {
        public static decimal CalculateStockInBin(this Accounting.Stock.BinItem bin)
        {
            return CalculateStock(bin);
        }

        public static decimal CalculateStockInBin(this Data.BinItem bin)
        {
            return CalculateStock(bin);
        }

        private static Func<dynamic, decimal> CalculateStock = (dynamic bin) =>
            ((bin.ConfirmedQtyInStock + bin.UnconfirmedQtyInStock) -
            (bin.QuantityAllocatedBOM + bin.QuantityAllocatedSOP + bin.QuantityAllocatedStock));
    }
}
