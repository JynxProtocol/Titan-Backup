using DateTimeExtentions;
using Sage.Accounting.Common;
using Sage.Accounting.Exceptions;
using Sage.Accounting.Reporting;
using Sage.Accounting.SOP;
using Sage.Accounting.Stock;
using Sage.Accounting.Stock.Views;
using Sage.Api.Data;
using System;
using System.Linq;

namespace Sage.Api.Functions
{
    internal static class SalesOrder
    {
        public static TitanEntities Titan = new TitanEntities();

        public static DateTime Today = Sage.Common.Clock.Today;

        private static string GetDocumentNumber(this SOPLedger SalesLedger)
        {
            if (!SalesLedger.Setting.AutoGenOrderReturnNos)
            {
                throw new NotImplementedException();
            }
            else
            {
                return TemporaryDocumentNumberManager.GetNextTemporaryDocumentNumber();
            }
        }

        private static void SetDates(this SOPStandardItemLine SalesOrderLine,
            DateTime RequestedDeliveryDate, DateTime PromisedDeliveryDate)
        {
            SalesOrderLine.RequestedDeliveryDate = RequestedDeliveryDate;
            SalesOrderLine.PromisedDeliveryDate = PromisedDeliveryDate;
        }

        /// <summary>
        /// The purpose of this method is to set the analysis codes of a given sales order line to the given values.
        /// The analysis codes are set using reflection, with the first code being set to the value of the first argument,
        /// the second code being set to the value of the second argument, and so on.
        /// If there are more arguments than analysis codes, the extra arguments are ignored.
        /// If there are fewer arguments than analysis codes, the remaining codes are not modified.
        /// </summary>
        /// <param name="SalesOrderLine">The sales order line to set the analysis codes for.</param>
        /// <param name="Codes">The values to set the analysis codes to, in order.</param>
        private static void SetAnalysisCodes(this SOPStandardItemLine SalesOrderLine,
            params string[] Codes)
        {
            // iterate through arguments, getting arg and index
            foreach (var (Code, Index) in Codes.Select((Code, Index) => (Code, Index + 1)))
            {
                // set the nth analysis code to the nth argument using reflection
                SalesOrderLine.GetType()
                    .GetProperty($"AnalysisCode{Index}")
                    .SetValue(SalesOrderLine, Code);
            }
        }

        public static long AddSalesOrderLine(this SOPOrderReturn SalesOrder,
            string GRN, DateTime? DateReceived, string PickingListComment,
            string StockCode, string Description, int QtyReceived, decimal UnitPrice,
            int LeadTime, int DeliveryDays, string WorkRequired, string Warranty, string Colour,
            string DespatchMethod, string DeliveryTerms, bool RecordCancelledOrdLines)
        {

            Sage.Accounting.Stock.StockItems stockItems = new Sage.Accounting.Stock.StockItems();
            Sage.Accounting.Stock.StockItem StockItem = stockItems[StockCode];

            if (StockItem == null)
            {
                throw new NotImplementedException();
            }

            var WarehouseStockItemViews = new WarehouseStockItemViews();

            WarehouseStockItemViews.Query.Filters.Add(
                new ObjectStore.Filter(WarehouseStockItemView.FIELD_ITEM, StockItem.Item));

            WarehouseStockItemViews.Query.Filters.Add(
                new ObjectStore.Filter("[Warehouse].[Name]", Titan.StockSetting().MainWarehouse));

            WarehouseStockItemViews.Query.Find();

            // find WarehouseItemView
            var WarehouseItemView =
                WarehouseStockItemViews
                .Cast<WarehouseStockItemView>()
                .Single();

            if (WarehouseItemView == null)
            {
                throw new NotImplementedException();
            }

            var SalesOrderLine = new SOPStandardItemLine();

            try
            {
                SalesOrderLine.SOPOrderReturn = SalesOrder;
                SalesOrderLine.Item = StockItem;
                SalesOrderLine.WarehouseItem = WarehouseItemView.WarehouseItem;
                SalesOrderLine.UseDescription = true;
                SalesOrderLine.ItemDescription = Description;

                if (SalesOrder.SOPUserPermission.OverrideFulfilmentMethod)
                {
                    SalesOrderLine.FulfilmentMethod =
                        SOPOrderFulfilmentMethodEnum.EnumFulfilmentFromStock;
                }

                // Add allowable warnings for not enough stock
                Sage.Accounting.Application.AllowableWarnings.Add(SalesOrderLine,
                    typeof(TCNotEnoughStockException));

                Sage.Accounting.Application.AllowableWarnings.Add(SalesOrderLine,
                    typeof(StockItemHoldingInsufficientException));

                Sage.Accounting.Application.AllowableWarnings.Add(SalesOrderLine,
                    typeof(StockItemHoldingInsufficientReduceQuantityException));

                // If the SOP ledger is configured to allocate on order entry setting
                // the LineQuantity will also set the ToAllocateQuantity property
                SalesOrderLine.LineQuantity = QtyReceived;

                // Make sure user has permission to set price and discount
                if (!SalesOrder.SOPUserPermission.OverridePricesDiscounts)
                {
                    throw new NotImplementedException();
                }

                // Add the allowable warning for Ex20319Exception so that the
                // UnitSellingPrice can be set without requesting confirmation
                Sage.Accounting.Application.AllowableWarnings.Add(SalesOrderLine,
                    typeof(Ex20319Exception));

                // Set the new price
                SalesOrderLine.UnitSellingPrice = UnitPrice;

                // Add the allowable warning for Ex20320Exception so that the
                // UnitDiscountPercent can be set without requesting confirmation
                Sage.Accounting.Application.AllowableWarnings.Add(SalesOrderLine,
                    typeof(Ex20320Exception));

                // Set discount
                SalesOrderLine.UnitDiscountPercent = 0M;

                // Add comments to be printed on the picking list
                SalesOrderLine.PickingListComment = PickingListComment;

                SalesOrder.Lines.Add(SalesOrderLine);

                SalesOrderLine.SetDates(Today.AddWorkDays(LeadTime),
                    Today.AddWorkDays(LeadTime - DeliveryDays));

                SalesOrderLine.SetAnalysisCodes(
                    /*A01*/ WorkRequired,
                    /*A02*/ Warranty,
                    /*A03*/ Colour,
                    /*A04*/ "", // Customer Claim Number
                    /*A05*/ "", // Reason for Rejection
                    /*A06*/ $"{LeadTime} Working days", // Turn Around time
                    /*A07*/ DespatchMethod,
                    /*A08*/ DeliveryTerms,
                    /*A09*/ "",
                    /*A10*/ "",
                    /*A11*/ "",
                    /*A12*/ "",
                    /*A13*/ "",
                    /*A14*/ "",
                    /*A15*/ GRN,
                    /*A16*/ DateReceived.ToString()
                );

                SalesOrderLine.Post(RecordCancelledOrdLines);

                Titan.SaveChanges();

                return SalesOrderLine.SOPOrderReturnLine;
            }
            finally
            {
                Sage.Accounting.Application.AllowableWarnings.DeleteAll(SalesOrderLine);
            }
        }

        public static void AddCommentLine(this SOPOrderReturn SalesOrder, string ItemDescription,
            decimal? UnitPrice = null)
        {
            var CommentLine = new SOPCommentLine
            {
                // set the order
                SOPOrderReturn = SalesOrder,
                // set the comment and specify to print on order acknowledgement and invoice
                ItemDescription = ItemDescription,
                ShowOnCustomerDocs = true
            };

            if (UnitPrice is decimal)
            {
                CommentLine.UnitSellingPrice = (decimal)UnitPrice;
            }

            // Add the line to the order
            SalesOrder.Lines.Add(CommentLine);

            // Save the line
            CommentLine.Post();
        }

        public static void SetDeliveryAddress(this SOPOrder SalesOrder,
            string PostalName,
            string AddressLine1,
            string AddressLine2,
            string City,
            string County,
            string PostCode)
        {
            // TODO: logging
            //DataLogger.LogData("AmmendDeliveryAddress", "Attempting to Ammend Delivery Address", 1);
            //DataLogger.LogData("AmmendDeliveryAddress", "Order is Casualty", 1);

            SalesOrder.UseInvoiceAddress = false;

            SalesOrder.SOPDocDelAddress.PostalName = PostalName;
            SalesOrder.SOPDocDelAddress.AddressLine1 = AddressLine1;
            SalesOrder.SOPDocDelAddress.AddressLine2 = AddressLine2;
            SalesOrder.SOPDocDelAddress.City = City;
            SalesOrder.SOPDocDelAddress.County = County;
            SalesOrder.SOPDocDelAddress.PostCode = PostCode;
        }

        public static SOPOrder Create(SOPLedger SalesLedger,
            Accounting.SalesLedger.Customer Customer, int LeadTime, int DeliveryDays,
            string CustomerDocumentNo)
        {
            var Today = Sage.Common.Clock.Today;

            // create new sales order
            var SalesOrder = new SOPOrder();

            SalesOrder.EntryType = SOPOrderEntryTypeEnum.EnumSOPOrderEntryTypeFull;
            SalesOrder.DocumentNo = SalesLedger.GetDocumentNumber();

            // Set customer and reference
            SalesOrder.Customer = Customer;
            SalesOrder.CustomerDocumentNo = CustomerDocumentNo;

            // Set document and delivery dates
            SalesOrder.DocumentDate = Today;
            SalesOrder.RequestedDeliveryDate = Today.AddWorkDays(LeadTime);
            SalesOrder.PromisedDeliveryDate = Today.AddWorkDays(LeadTime - DeliveryDays);

            SalesOrder.Update();

            return SalesOrder;
        }

        public static void Acknowledge(this SOPOrder SalesOrder, SOPLedger SalesLedger)
        {
            if (!SalesLedger.Setting.PrintOrderAcknowledges)
            {
                throw new NotImplementedException();
            }

            SOPPrintOrderAckCoordinator Coordinator = new SOPPrintOrderAckCoordinator();
            Coordinator.ClearCriteria();

            ActiveLock ActiveLock = new ActiveLock();

            try
            {
                Coordinator.PopulateOrderAcks();

                Report Report = Sage.Accounting.Application.ReportingService.CreateReport();

                Report.Criteria.Clear();
                Report.Criteria.Add(new Criterion()
                {
                    Name = ReportingConstants.KEY_SOPOrderReturn_SOPOrderReturnID,
                    Values = Coordinator.OrderAckItems
                        .Cast<SOPPrintOrderAckItem>()
                        .Select(o => o.OrderID)
                        .ToArray()
                });

                Report.FileName = "#SOPOrderAcknowledgement";
                Report.OutputMode = OutputModeEnum.Spooler;

                Report.Run();

                Coordinator.UpdatePrintStatus();
            }
            // TODO: log
            catch (Ex20383Exception onHoldException)
            {
                System.Diagnostics.Debug.WriteLine(onHoldException);
            }
            catch (Ex20245Exception confirmReprintException)
            {
                Console.WriteLine(confirmReprintException);
            }
            catch (Ex20666Exception noItemsException)
            {
                System.Diagnostics.Debug.WriteLine(noItemsException);
            }
            catch (Exception ex)
            {
                //DataLogger.LogData("AckSOR", "Error " + ex.Message, 1);
            }

            SalesOrder.Dispose();
            SalesOrder = null;
            Coordinator.Dispose();
            Coordinator = null;
            ActiveLock.Dispose();
            ActiveLock = null;
        }
    }
}
