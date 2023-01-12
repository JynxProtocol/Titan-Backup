using Sage.Accounting.Stock;
using Sage.Accounting.Stock.Views;
using Sage.Api.Controllers;
using Sage.Manufacturing.BusinessObjects.BillOfMaterials.Factories;
using Sicon.API.Sage200.Objects.Models;
using Sicon.Sage200.WorksOrder.Objects;
using Sicon.Sage200.WorksOrder.Objects.Coordinators;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BomRecord = Sage.Manufacturing.BusinessObjects.BillOfMaterials.BomRecord;
using WorksOrderUpdate = Sicon.Sage200.WorksOrder.WorksOrderUpdate;

namespace Sage.Api.Functions
{
    internal static class WorksOrder
    {
        public static DateTime Today = Sage.Common.Clock.Today;

        public static async Task<SiWorksOrder> Create(string StockCode, int BuildQuantity,
            long ReturnLineID, string DocNo, string Warehouse)
        {
            // default variables to pass to the new WO
            var WODefaults = new
            {
                Status = "New",
                SubAssemblyNumber = 0M,
                JobCostingJobID = 0L,
                JobCostingChildID = 0L,

                WorkShop = "",
                ParentWOLineID = (string)null,
                UltimateParentID = 0L,
                SeparateWOForEachSubassembly = new bool?(),
                ExpandBOMs = new bool?(),
                FromWebAPI = false,
                SpareText1 = "",
                SpareText2 = "",
                SpareText3 = "",
                SpareText4 = "",
                SpareText5 = "",
            };

            // Get the latest BOM for the return part
            BomRecord BOM =
                BomRecordFactory.Factory.FetchLatestActive(StockCode);

            // main variables passed to new WO
            string WorksOrderNumber = DocNo;
            DateTime DueDate = DateTime.Today.AddDays(5);

            // Use the main warehouse to allocate to/from
            Warehouse MainWarehouse = new Warehouses()[Warehouse];
            // construct a name for the WO
            string WorksOrderName = $"{BOM.Reference} - {BOM.Description}";

            // Now create our new WO
            // GenerateNewWorksOrder has several overloads - you may want to investigate these.
            // [Increase] just picked this one as it seemed reasonable:
            SiWorksOrder NewWorksOrder = await new WorksOrderUpdate().GenerateNewWorksOrder(
                BOM,
                MainWarehouse,
                MainWarehouse,
                WorksOrderName,
                WODefaults.Status,
                BuildQuantity,
                WorksOrderNumber,
                WODefaults.SubAssemblyNumber,
                WODefaults.JobCostingJobID,
                WODefaults.JobCostingChildID,
                WODefaults.SpareText1,
                WODefaults.SpareText2,
                // not sure why SpareText3 is missing here
                WODefaults.SpareText4,
                WODefaults.SpareText5,
                ReturnLineID,
                DueDate,
                WODefaults.WorkShop,
                WODefaults.ParentWOLineID,
                WODefaults.UltimateParentID,
                WODefaults.SeparateWOForEachSubassembly,
                WODefaults.ExpandBOMs,
                WODefaults.FromWebAPI
            );

            return NewWorksOrder;
        }

        /// <summary>
        /// The purpose of this method is to allocate stock for a given works order line, 
        /// using the specified warehouse and batch number. If the specified batch number 
        /// is not null, the method attempts to allocate stock only from items with that batch 
        /// number. Otherwise, the method does not consider the batch number when allocating stock.
        /// If the allocation is successful, the method returns <see langword="true"/>, 
        /// otherwise the method returns <see langword="false"/>, using the TryParse pattern.
        /// </summary>
        /// <param name="WorksOrderLine">The works order line to allocate stock for.</param>
        /// <param name="Warehouse">The warehouse to allocate stock from.</param>
        /// <param name="BatchNo">The batch number to allocate stock from, 
        /// or null if the batch number should not be considered.</param>
        /// <returns>true if the allocation is successful, false otherwise.</returns>
        public static bool TryAllocateStockForLine(SiWorksOrderLine WorksOrderLine,
            string Warehouse, string BatchNo = null)
        {
            // fetch the bin to allocate from
            if (!StockController.TryGetBinItemWarehouseView(WorksOrderLine.Item, Warehouse,
                "Unspecified", WorksOrderLine.Quantity,
                out BinItemWarehouseView binItemWarehouseView))
            {
                // if we cannot allocate, just skip this line
                return false;
            }

            // if we are given a batch number, select only items with that batch number
            // otherwise, we don't care
            List<TraceableBinItemQuantity> FilterByBatchNumber = null;
            if (BatchNo != null)
            {
                FilterByBatchNumber = ConstructBatchNoFilter(binItemWarehouseView.BinItem,
                    WorksOrderLine.Quantity, BatchNo);
            }

            // allocate stock for the line
            var results = AllocationCoordinator.AllocateWorksOrderLine(WorksOrderLine,
                WorksOrderLine.Quantity, binItemWarehouseView.BinItem, FilterByBatchNumber);

            return true;
        }

        private static List<TraceableBinItemQuantity> ConstructBatchNoFilter(
            BinItem binItem, decimal requiredQty, string BatchNo)
        {
            return new List<TraceableBinItemQuantity>()
            {
                new TraceableBinItemQuantity(BatchNo, requiredQty, binItem.BinItem)
            };
        }
    }
}
