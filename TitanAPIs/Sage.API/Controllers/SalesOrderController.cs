using Sage.Accounting.Exceptions;
using Sage.Accounting.SalesLedger;
using Sage.Accounting.SOP;
using Sage.Api.Data;
using Sage.Api.Functions;
using Sicon.Sage200.WorksOrder.Objects;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Titan.Models.StockManagement;

namespace Sage.Api.Controllers
{
    [RoutePrefix("api/SalesOrder")]
    public class SalesOrderController : ApiController
    {
        public SabreLive SageDB = new SabreLive();
        public TitanEntities Titan = new TitanEntities();

        /// <summary>
        /// AddAWKLineToSalesOrder adds a line to a sales order with the 
        /// given SalesOrderId, Description, and UnitPrice.
        /// </summary>
        /// <param name="SalesOrderId">The ID of the sales order to which the line will be added.</param>
        /// <param name="Description">The description of the line to add to the sales order.</param>
        /// <param name="UnitPrice">The unit price of the line to add to the sales order.</param>
        [HttpPost]
        [Route("{SalesOrderId}/AddAWKLine", Name = nameof(AddAWKLineToSalesOrder))]
        [SwaggerResponse(System.Net.HttpStatusCode.OK)]
        [SwaggerResponse(System.Net.HttpStatusCode.NotFound, "Could not find sales order")]
        public IHttpActionResult AddAWKLineToSalesOrder(string SalesOrderId,
            [FromUri] string Description, [FromUri] decimal UnitPrice)
        {
            // Retrieve the sales order with the given ID,
            // padding it with zeros on the left if necessary
            var SalesOrder = GetSalesOrder(SalesOrderId.PadLeft(10, '0'));

            if (SalesOrder == null)
            {
                return NotFound();
            }

            // Add a line to the sales order with the given Description and UnitPrice
            SalesOrder.AddCommentLine(Description, UnitPrice);

            return Ok();
        }

        /// <summary>
        /// The CreateSalesOrderAsync method is used to create a new sales order from an existing 
        /// order with the given OrderID. The method returns a 404 response if the customer 
        /// or contract associated with the order is not found, a 400 Bad Request response 
        /// if the sales order cannot be saved, and a 200 OK response if the sales order 
        /// is successfully created.
        /// 
        /// Next, it creates a works order for each item on the sales order, 
        /// adds stock for the repairable item associated with the order line, 
        /// and tries to allocate stock for the works order. If any errors are encountered,
        /// the method returns a 409 Conflict response.
        /// </summary>
        /// <param name="OrderID">The order from which to create a sales order and works order</param>
        [HttpPost]
        [Route("CreateFromOrder", Name = nameof(CreateSalesOrderAsync))]
        [SwaggerResponse(System.Net.HttpStatusCode.OK)]
        [SwaggerResponse(400, "Could not create sales order")]
        [SwaggerResponse(404, "Could not find linked contract or customer")]
        [SwaggerResponse(409, "Could not create WorksOrder")]
        [SwaggerResponse(422, "Could not add an Order line")]
        public async Task<IHttpActionResult> CreateSalesOrderAsync(int OrderID)
        {
            bool ApplyBestDiscount = true;
            bool OverrideOnHold = true;
            bool ConfirmLines = true;
            var ShouldAddComment = Titan.Settings.Single().CommentLine == 1;

            var Order = Titan.OrderHeaders
                .FirstOrDefault(Option => Option.OrdID == OrderID);

            var OrderDetails = Titan.OrderDetails
                .Where(Option => Option.OrdID == OrderID)
                .ToList();

            var Contract = Titan.ContractHeaders
                .Where(contract => contract.ConID == Order.ConID)
                .SingleOrDefault();

            var CustomerCode = Titan.Customers
                .Where(cus => cus.CusID == Order.CusID)
                .Single()
                .CustomerAccountNumber;

            var Customer = CustomerFactory.Factory.Fetch(CustomerCode);

            if (Customer == null || Contract == null)
            {
                return NotFound();
            }

            var LeadTime = OrderDetails.Max(detail => detail.LeadTime) ?? 10;
            var DeliveryDays = OrderDetails.Max(detail => detail.DeliveryDays) ?? 0;

            var SalesLedger = SOPLedger.CreateNew();

            var SalesOrder = Functions.SalesOrder.Create(SalesLedger, Customer, LeadTime,
                DeliveryDays, Order.CusOrderNumber);

            SalesOrder.SetDeliveryAddress(
                Order.PostalName,
                Order.AddressLine1,
                Order.AddressLine2,
                Order.City,
                Order.County,
                Order.PostCode);

            // Add allowable warning for negative values on free text lines
            Sage.Accounting.Application.AllowableWarnings
                .Add(SalesOrder, typeof(Ex21190Exception));

            foreach (var OrderLine in OrderDetails)
            {
                if (!(OrderLine.UnitPrice is decimal UnitPrice))
                {
                    return StatusCode((System.Net.HttpStatusCode)422);
                }

                var LineComment = GetDocumentComments(OrderLine, Contract?.ContractName, Order.GRN);

                var LineLeadTime = OrderLine.LeadTime ?? 10;
                var LineDeliveryDays = OrderLine.DeliveryDays ?? 0;

                var SOPOrderReturnLine = SalesOrder.AddSalesOrderLine(
                    Order.GRN,
                    Order.DateReceived,
                    LineComment,
                    OrderLine.StockCode,
                    OrderLine.Description,
                    OrderLine.QtyReceived ?? 0,
                    UnitPrice,
                    LineLeadTime,
                    LineDeliveryDays,
                    OrderLine.WorkRequired,
                    OrderLine.Warranty,
                    OrderLine.Colour,
                    OrderLine.DespatchMethod,
                    OrderLine.DeliveryTerms,
                    SalesLedger.Setting.RecordCancelledOrdLines);

                OrderLine.DetSODID = SOPOrderReturnLine;

                // TODO: standardise console logs
                Console.WriteLine($"Processed order line | DetId {OrderLine.DetID} | " +
                    $"SOPOrderReturnLine {SOPOrderReturnLine} | ");
            }

            if (ShouldAddComment && !string.IsNullOrWhiteSpace(Order.DeliveryTerms))
            {
                SalesOrder.AddCommentLine(Order.DeliveryTerms);
            }

            // Delete allowable warning for negative values
            Sage.Accounting.Application.AllowableWarnings
                .Delete(SalesOrder, typeof(Ex21190Exception));

            try
            {
                // Validate the order
                SalesOrder.Confirm(ApplyBestDiscount, OverrideOnHold, ConfirmLines);

                // Save the order to the database.
                SalesOrder.Post(ApplyBestDiscount, OverrideOnHold);

                var ShouldAcknowledgeOrder = Titan.Settings.First().OrderAck == 1;

                if (ShouldAcknowledgeOrder)
                {
                    SalesOrder.Acknowledge(SalesLedger);
                }

                Order.SOR = SalesOrder.DocumentNo;
                Titan.SaveChanges();

                // copy PO to new directory
                var PODir = Titan.Settings.Select(doc => doc.POStorage).Single();
                var POPrefix = Titan.Settings.Select(doc => doc.POPrefix).Single();

                try
                {
                    Directory.CreateDirectory(PODir);

                    var Source = PODir + POPrefix + Order.ConID;
                    var DestDir = PODir + SalesOrder.DocumentNo;

                    foreach (var SourceFile in Directory.GetFiles(Source))
                    {
                        var DestFile = Path.Combine(DestDir, Path.GetFileName(SourceFile));
                        File.Copy(SourceFile, DestFile, true);
                    }
                }
                catch
                {
                    // TODO: log
                }
            }
            catch (Exception)
            {
                // If the validation of the order fails we need to
                // remove it and its lines - since these have already been saved.
                SalesOrder.Cancel();

                return BadRequest();
            }

            try
            {
                // Create WorksOrder for each order line
                // Sales Order DocumentNo is used to generate the Works OrderNumber using the
                // "-" and a number from 1 to x
                foreach (var (OrderLine, Index) in OrderDetails
                    .Select((line, index) => (line, index + 1)))
                {
                    var DocNumber = SalesOrder.DocumentNo.TrimStart('0') + $"-{Index:D2}";

                    var WorksOrder = await Functions.WorksOrder.Create(
                        OrderLine.StockCode,
                        (int)OrderLine.QtyReceived,
                        (long)OrderLine.DetSODID,
                        DocNumber,
                        Titan.StockSetting().MainWarehouse
                    );

                    var Line = Titan.OrderDetails
                        .Where(ID => ID.DetID == OrderLine.DetID)
                        .FirstOrDefault();

                    Line.WONumber = DocNumber;

                    Titan.SaveChanges();

                    //Add repairables to stock
                    var AddStockResult = StockController.AddStockTraceable(OrderLine.DirtyStockCode,
                        new AddStockDTO
                        {
                            Quantity = (decimal)OrderLine.QtyReceived,
                            BinName = "Unspecified",
                            Warehouse = Titan.StockSetting().MainRepairablesWarehouse,
                            Ref = Line.WONumber,
                            SecondRef = "Booking in"
                        }
                    );

                    if (!AddStockResult.Success)
                    {
                        return StatusCode((System.Net.HttpStatusCode)422);
                    }

                    SiWorksOrderLine RepairableLine = Sicon.Sage200.WorksOrder.Global.
                        LoadComponents(WorksOrder, null, out _, false)
                        .Cast<SiWorksOrderLine>()
                        .FirstOrDefault(cl => cl.Item.Code == OrderLine.DirtyStockCode);

                    // allocate stock for the added line
                    if (!Functions.WorksOrder
                        .TryAllocateStockForLine(RepairableLine,
                            Titan.StockSetting().MainRepairablesWarehouse, AddStockResult.BatchNo))
                    {
                        // if we cannot allocate, just skip this line
                        continue;
                    }
                }
            }
            catch
            {
                return Conflict();
            }

            return Ok();
        }

        /// <summary>
        /// Generates a string containing comments for an order line, using the contract and GRN.
        /// </summary>
        private string GetDocumentComments(OrderDetail OrderLine,
            string ContractName, string GRN)
        {
            // Check whether the contract name, serial number, and special instructions
            // are not null or empty
            var HasContractName = !string.IsNullOrWhiteSpace(ContractName);
            var HasSerialNumber = OrderLine.SerialNumber != null;
            var HasSpecialInstructions = OrderLine.SpecialInstruction != null;

            // Create a new list of strings to hold the comments
            var Comments = new List<string>();

            // Add the GRN number as a comment
            Comments.Add($"GRN: {GRN}");

            // If the contract name is not null or empty, add it as a comment
            if (HasContractName) { Comments.Add($"Contract Name: {ContractName}"); }

            // If the serial number is not null, add it as a comment
            if (HasSerialNumber) { Comments.Add($"SN: {OrderLine.SerialNumber}"); }

            // If the special instructions are not null, add them as a comment
            if (HasSpecialInstructions)
            {
                Comments.Add($"Special Instructions: {OrderLine.SpecialInstruction}");
            }

            // Return a string containing all the comments, separated by a space character
            return string.Join(" ", Comments);
        }

        private SOPOrder GetSalesOrder(string SOR)
        {
            // get all sopOrders then filter for the one we want
            SOPOrders sopOrders = new SOPOrders();
            sopOrders.Query.Filters.Add(new Sage.ObjectStore.Filter(
                SOPOrder.FIELD_DOCUMENTNO, SOR));
            sopOrders.Find();

            return sopOrders.First;
        }
    }
}
