using Sage.Accounting.Stock;
using Sage.Api.Data;
using Sage.Api.Functions;
using Sage.ObjectStore;
using Sicon.API.Sage200.Objects.Common;
using Sicon.Sage200.WorksOrder.Objects;
using Sicon.Sage200.WorksOrder.Objects.Coordinators;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using StockItem = Sage.Accounting.Stock.StockItem;
using Warehouse = Sage.Accounting.Stock.Warehouse;
using WOLineCreator = Sicon.Sage200.WorksOrder.Coordinators.
    UIWorksOrderLineCreationCoordinator;
using WOLineToCreate = Sicon.Sage200.WorksOrder.Coordinators.
    UIWorksOrderLineCreationCoordinator.CreateWorksOrderLineObject;

namespace Sage.Api.Controllers
{
    [RoutePrefix("api/WorksOrder")]
    public class WorksOrderController : ApiController
    {
        public SabreLive SageDB = new SabreLive();
        public TitanEntities Titan = new TitanEntities();

        /// <summary>
        /// Adds an AWK to a Works Order, adding and allocating parts for each fault.
        /// </summary>
        /// <param name="AWN">The ID of the AWK to add to the WO.</param>
        [HttpPost]
        [Route("AddAWKToWO", Name = nameof(AddAWKToWO))]
        [SwaggerResponse(System.Net.HttpStatusCode.OK)]
        [SwaggerResponse(400, "Could not find AWK or linked WorksOrder")]
        [SwaggerResponse(404, "Could not find bin for an AWK line. " +
            "This could be because there is not enough stock to allocate")]
        [SwaggerResponse(409, "Line added incorrectly")]
        [SwaggerResponse(422, "Could not add an AWK line to the WorksOrder")]
        public IHttpActionResult AddAWKToWO(int AWN)
        {
            if (!TryGetAWKInfo(AWN, out AWKHeader AWK, out List<AWKDetail> AWKDetails,
                out SiWorksOrder WorksOrder))
            {
                return BadRequest();
            }

            // get main warehouse to issue parts from
            Warehouse MainWarehouse = new Warehouses()[Titan.StockSetting().MainWarehouse];

            // add each awk to the works order
            foreach (var Item in AWKDetails
                .Where(detail => detail.RequiredQty != null)
                .Where(detail => detail.RequiredQty > 0))
            {
                if (!TryGetStockItem(Item.StockCode, out StockItem StockItem))
                {
                    return StatusCode((System.Net.HttpStatusCode)422);
                }

                try
                {
                    var AddedLine = WOLineCreator.CreateWorksOrderLine(
                        new WOLineToCreate(WorksOrder, Enums.LineType.Component, StockItem,
                            (decimal)Item.RequiredQty)
                        {
                            LineComment = $"Additional work required: {Item.WorkRequired} " +
                                $"\nfor fault {Item.Fault}",
                            Warehouse = MainWarehouse,

                            FutureBuyPrice =
                                new Global().GetCostOfStockItem(StockItem, out string reason),
                            ReasonForFutureBuyPrice = reason
                        }
                    );

                    if (AddedLine == null)
                    {
                        return Conflict();
                    }

                    AddToHistory(WorksOrder, StockItem.Code, (decimal)Item.RequiredQty,
                        GetReasonForChange());

                    // allocate each works order line
                    // try to allocate from main warehouse
                    if (!Functions.WorksOrder.TryAllocateStockForLine(AddedLine,
                            Titan.StockSetting().MainWarehouse))
                    {
                        // if we can't allocate from main, try to allocate from sabre stock
                        if (!Functions.WorksOrder.TryAllocateStockForLine(AddedLine,
                            Titan.StockSetting().SabreStockWarehouse))
                        {
                            // if we cannot allocate from either, just skip this line
                            continue;
                        }
                    }
                    // otherwise, sucessfully allocated
                }
                catch (Exception)
                {
                    return StatusCode((System.Net.HttpStatusCode)422);
                }
            }

            return Ok();
        }

        /// <summary>
        /// The purpose of this method is to retrieve information about an AWK given its number, 
        /// including the header information, the list of details, and the associated works order. 
        /// If the requested AWK cannot be found, 
        /// the method returns <see langword="false"/>, using the TryParse pattern
        /// </summary>
        private bool TryGetAWKInfo(int AWN, out AWKHeader AWK, out List<AWKDetail> AWKDetails,
            out SiWorksOrder WorksOrder)
        {
            var AWKHeader = Titan.AWKHeaders.Where(awk => awk.ID == AWN).FirstOrDefault();

            AWK = AWKHeader;
            AWKDetails = Titan.AWKDetails
                .Where(detail => detail.DID == AWKHeader.ID)
                .ToList();

            if (AWKHeader == null)
            {
                WorksOrder = null;
                return false;
            }

            var WorksOrders = new SiWorksOrders();
            WorksOrders.Find(new ObjectStore.Query()
            {
                Filters = {
                        new Filter(SiWorksOrder.FIELD_WONUMBER, AWK.WorksOrderNumber)
                    }
            });
            WorksOrder = WorksOrders.First;

            if (WorksOrder == null)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// If the requested stock item is found, the method outputs the StockItem object and returns 
        /// <see langword="true"/>, otherwise <see langword="false"/>, using the TryParse pattern.
        /// </summary>
        private bool TryGetStockItem(string StockCode, out StockItem StockItem)
        {
            try
            {
                StockItem = new StockItems()[StockCode];
                return true;
            }
            catch (Exception)
            {
                StockItem = null;
                return false;
            }
        }

        private string GetReasonForChange()
        {
            var reasons = new SiWorksOrderLineChangeReasons()
            {
                Query = {
                    Filters = {
                        new Filter(SiWorksOrderLineChangeReason.FIELD_DEFAULT, true)
                    }
                }
            };
            reasons.Find();

            return reasons.First?.Reason ?? "";
        }

        private void AddToHistory(SiWorksOrder WorksOrder, string StockCode,
            decimal Quantity, string reasonForChange)
        {
            string qtyFormat =
                ProductSettings<Sicon.Sage200.WorksOrder.Settings>.Instance.QuantityFormat;

            HistoryCoordinator.AddHistory(
                WorksOrder,
                Enums.HistoryType.ComponentLineAdded,
                $"Code: {StockCode} Amount: {Decimal.Zero.ToString(qtyFormat)}",
                $"Code: {StockCode} Amount: {Quantity.ToString(qtyFormat)} " +
                    $"Reason: {reasonForChange}",
                (long)WorksOrder.PrimaryKey.GetDbKey()
            );
        }

    }
}
