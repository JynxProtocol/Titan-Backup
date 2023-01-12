using Microsoft.AspNetCore.Mvc;
using SageAPIConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Titan.API.Models;
using Titan.API.Models.DGRN;
using Titan.API.Models.Sales;
using Titan.Filters;
using static Titan.API.Functions.Stock;

namespace Titan.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class OrdersController : Controller
    {
        internal TitanContext Titan { get; private set; }
        internal SAGEContext Sage { get; private set; }
        internal SageAPI SageAPI { get; private set; }

        internal DateTime Now = DateTime.Now;

        public OrdersController(TitanContext titan, SAGEContext sage,
            SageAPI sageAPI)
        {
            Titan = titan;
            Sage = sage;
            SageAPI = sageAPI;
        }

        /// <response code="200">Returns the list of unauthorised Orders</response>
        [HttpGet]
        [Route("ToAuthorise")]
        public ActionResult<List<OrderDTO>> ListOrdersToApprove()
        {
            return Titan.OrderHeaders
                .OrderByDescending(Order => Order.OrdID)
                .Where(Order => Order.OrdIsAuthorised != 1)
                .ToList()
                .Select(Order => Order.ToOrderDTO(Titan))
                .ToList();
        }

        /// <response code="200">Returns the list of completed Orders</response>
        [HttpGet]
        [Route("Complete")]
        public ActionResult<List<OrderDTO>> ListCompletedOrders()
        {
            return Titan.OrderHeaders
                .OrderByDescending(Order => Order.OrdID)
                .Where(Order => !string.IsNullOrWhiteSpace(Order.SOR))
                .ToList()
                .Select(Order => Order.ToOrderDTO(Titan))
                .ToList();
        }

        /// <response code="200">Returns the Order</response>
        /// <response code="404">Could not find the Order</response>
        [HttpGet]
        [Route("{OrderID}")]
        public ActionResult<OrderDTO> GetOrderByID(int OrderID)
        {
            var Order = Titan.OrderHeaders
                .SingleOrDefault(Order => Order.OrdID == OrderID)
                .ToOrderDTO(Titan);

            if (Order == null)
            {
                return NotFound();
            }

            return Order;
        }

        /// <response code="200">Succesfully order</response>
        /// <response code="400">Cannot update order that is authorised</response>
        /// <response code="404">Could not find the Order</response>
        [HttpPost]
        [Route("{OrderID}")]
        [Feature("ModifyOrder")]
        public ActionResult UpdateOrder(int OrderID, OrderDTO orderDTO)
        {
            var Order = Titan.OrderHeaders
                .SingleOrDefault(Order => Order.OrdID == OrderID);

            if (Order == null)
            {
                return NotFound();
            }

            if (Order.OrdIsAuthorised == 1)
            {
                return BadRequest();
            }

            Order.CusOrderNumber = orderDTO.CusOrderNumber;
            Order.OrdCustomerRef = orderDTO.OrdCustomerRef;
            Order.OrdSpecialInstruction = orderDTO.OrdSpecialInstruction;
            Order.PostalName = orderDTO.PostalName;
            Order.AddressLine1 = orderDTO.AddressLine1;
            Order.AddressLine2 = orderDTO.AddressLine2;
            Order.AddressLine3 = orderDTO.AddressLine3;
            Order.AddressLine4 = orderDTO.AddressLine4;
            Order.City = orderDTO.City;
            Order.County = orderDTO.County;
            Order.PostCode = orderDTO.PostCode;
            Order.DeliveryTerms = orderDTO.DeliveryTerms;
            Order.DespatchMethod = orderDTO.DespatchMethod;
            Order.GRN = orderDTO.GRN;

            foreach (OrderDetailDTO orderDetailDTO in orderDTO.OrderDetails)
            {
                var OrderDetail = Titan.OrderDetails
                    .Single(detail => detail.DetID == orderDetailDTO.OrderDetailID);

                OrderDetail.Colour = orderDetailDTO.Colour;
                OrderDetail.Warranty = orderDetailDTO.Warranty;
                OrderDetail.LeadTime = orderDetailDTO.LeadTime;
                OrderDetail.QtyReceived = orderDetailDTO.QtyReceived;
                OrderDetail.UnitPrice = orderDetailDTO.UnitPrice;
                OrderDetail.SerialNumber = orderDetailDTO.SerialNumber;
            }

            Titan.SaveChanges();

            return Ok();
        }

        /// <response code="200">Returns the Order detail</response>
        /// <response code="404">Could not find Order or detail</response>
        [HttpGet]
        [Route("{OrderID}/Details/{OrderDetailID}")]
        public ActionResult<OrderDetailDTO> GetOrderDetail(int OrderID, int OrderDetailID)
        {
            var Order = Titan.OrderHeaders
                .Where(Order => Order.OrdID == OrderID)
                .SingleOrDefault()
                .ToOrderDTO(Titan);

            if (Order == null)
            {
                return NotFound();
            }

            var Detail = Order.OrderDetails
                .SingleOrDefault(Detail => Detail.OrderDetailID == OrderDetailID);

            if (Detail == null)
            {
                return NotFound();
            }

            return Ok(Detail);
        }

        /// <response code="200">Returns succesfully added Order detail</response>
        /// <response code="404">Could not find Order</response>
        /// <response code="400">Could not find stock code</response>
        [HttpPost]
        [Route("{OrderID}/Details/Add")]
        [Feature("ModifyOrder")]
        public ActionResult<OrderDetailDTO> AddOrderDetail(int OrderID,
            OrderDetailDTO OrderDetailDTO)
        {
            var Order = Titan.OrderHeaders
                .SingleOrDefault(Order => Order.OrdID == OrderID);

            if (Order == null)
            {
                return NotFound();
            }

            var DetailToAdd = new OrderDetail()
            {
                OrdID = Order.OrdID,
                DateCreated = Now,
                CreatedBy = HttpContext.GetUserDisplayName(),

                QtyReceived = OrderDetailDTO.QtyReceived,
                StockCode = OrderDetailDTO.StockCode,
                Description = OrderDetailDTO.Description,
                Warranty = OrderDetailDTO.Warranty,
                WorkRequired = OrderDetailDTO.WorkRequired,
                LeadTime = OrderDetailDTO.LeadTime,
                DirtyStockCode = OrderDetailDTO.DirtyStockCode,
                DespatchMethod = OrderDetailDTO.DespatchMethod,
                DeliveryTerms = OrderDetailDTO.DeliveryTerms,
                DeliveryDays = (OrderDetailDTO.DespatchMethod == "Carrier") ? 1 : 0,

                Colour = OrderDetailDTO.Colour,
                SerialNumber = OrderDetailDTO.SerialNumber,
                SpecialInstruction = OrderDetailDTO.SpecialInstruction,
                UnitPrice = OrderDetailDTO.UnitPrice
            };

            Titan.OrderDetails.Add(DetailToAdd);

            try
            {
                CopyStockToTitanIfNotThere(new StockHeader
                {
                    DirtyStockCode = OrderDetailDTO.DirtyStockCode,
                    StockCode = OrderDetailDTO.StockCode
                }, Titan, Sage);
            }
            catch (Exception)
            {
                return BadRequest();
            }

            Titan.SaveChanges();

            return Ok(DetailToAdd.ToOrderDetailDTO());
        }

        /// <response code="200">Returns succesfully updated Order detail</response>
        /// <response code="400">Could not find stock code</response>
        /// <response code="404">Could not find Order or detail to update</response>
        [HttpPut]
        [Route("{OrderID}/Details/{OrderDetailID}")]
        [Feature("ModifyOrder")]
        public ActionResult<OrderDetailDTO> UpdateOrderDetail(int OrderID, int OrderDetailID,
            OrderDetailDTO OrderDetailDTO)
        {
            var Order = Titan.OrderHeaders
                .SingleOrDefault(Order => Order.OrdID == OrderID);

            if (Order == null)
            {
                return NotFound();
            }

            var DetailToUpdate = Titan.OrderDetails
                .Where(det => det.DetID == OrderDetailID)
                .SingleOrDefault();

            if (DetailToUpdate == null)
            {
                return NotFound();
            }

            DetailToUpdate.QtyReceived = OrderDetailDTO.QtyReceived;
            DetailToUpdate.StockCode = OrderDetailDTO.StockCode;
            DetailToUpdate.Description = OrderDetailDTO.Description;
            DetailToUpdate.Warranty = OrderDetailDTO.Warranty;
            DetailToUpdate.WorkRequired = OrderDetailDTO.WorkRequired;
            DetailToUpdate.LeadTime = OrderDetailDTO.LeadTime;
            DetailToUpdate.DirtyStockCode = OrderDetailDTO.DirtyStockCode;
            DetailToUpdate.DespatchMethod = OrderDetailDTO.DespatchMethod;
            DetailToUpdate.DeliveryTerms = OrderDetailDTO.DeliveryTerms;
            DetailToUpdate.DeliveryDays = (OrderDetailDTO.DespatchMethod == "Carrier") ? 1 : 0;

            DetailToUpdate.Colour = OrderDetailDTO.Colour;
            DetailToUpdate.SerialNumber = OrderDetailDTO.SerialNumber;
            DetailToUpdate.SpecialInstruction = OrderDetailDTO.SpecialInstruction;
            DetailToUpdate.UnitPrice = OrderDetailDTO.UnitPrice;

            try
            {
                CopyStockToTitanIfNotThere(new StockHeader
                {
                    DirtyStockCode = OrderDetailDTO.DirtyStockCode,
                    StockCode = OrderDetailDTO.StockCode
                }, Titan, Sage);
            }
            catch (Exception)
            {
                return BadRequest();
            }

            Titan.SaveChanges();

            return Ok(DetailToUpdate.ToOrderDetailDTO());
        }

        /// <response code="200">Returns the Order detail</response>
        /// <response code="404">Could not find contract</response>
        [HttpGet]
        [Route("{ConID}/PartsToBookIn")]
        public ActionResult<List<ReceivedPart>> GetOrderPartsSuggestions(int ConID)
        {
            var Contract = Titan.ContractHeaders
                .SingleOrDefault(Contract => Contract.ConID == ConID);

            if (Contract == null)
            {
                return NotFound();
            }

            return Titan.ContractDetails
                .Where(Detail => Detail.ConID == ConID)
                .Select(Detail => new ReceivedPart
                {
                    StockCode = Detail.StockCode,
                    Quantity = 0,
                    Description = Detail.Description
                })
                .ToList();
        }

        /// <response code="200">Returns succesfully added Order</response>
        /// <response code="400">Could not find contract to book in against</response>
        /// <response code="409">Cannot book in a blank GRN</response>
        /// <response code="422">Could not process a recieved part</response>
        [HttpPost]
        [Route("BookInParts")]
        [Feature("BookInParts")]
        public ActionResult<OrderDTO> BookInParts(List<ReceivedPart> Parts, int ConID,
            string DeliveryRef, string? GRN)
        // GRN is used currently, we might move away from this in future
        {
            var Contract = Titan.ContractHeaders.Where(con => con.ConID == ConID).FirstOrDefault();

            if (Contract == null)
            {
                return BadRequest();
            }

            int OrderID;

            try
            {
                var OrderHeaderToAdd = new OrderHeader()
                {
                    CreatedBy = HttpContext.GetUserDisplayName(),
                    DateCreated = Now,

                    ConID = Contract.ConID,
                    PostalName = Contract.PostalName,
                    AddressLine1 = Contract.AddressLine1,
                    AddressLine2 = Contract.AddressLine2,
                    AddressLine3 = Contract.AddressLine3,
                    AddressLine4 = Contract.AddressLine4,
                    City = Contract.City,
                    County = Contract.County,
                    PostCode = Contract.PostCode,
                    DeliveryTerms = Contract.DeliveryTerms,
                    DespatchMethod = Contract.DespatchMethod,

                    OrdIsAuthorised = 0,
                    DateReceived = Now,

                    CusID = (int?)Contract.conCusID,
                    CustomerName = Contract.CustomerName,
                    OrdCustomerRef = DeliveryRef,
                    CusOrderNumber = Contract.CustomerOrderNumber,
                    BatchCount = Contract.DeliveriesReceived,

                    //OrdIsCasualty = IsCasualty ? 1 : 0,
                    GRN = GRN
                };

                var OrderHeader = Titan.OrderHeaders.Add(OrderHeaderToAdd).Entity;
                Titan.SaveChanges();
                OrderID = OrderHeader.OrdID;

                // do not allow booking in nothing
                if (Parts.Sum(part => part.Quantity) == 0)
                {
                    return Conflict();
                }

                foreach (ReceivedPart part in Parts)
                {
                    if (part.Quantity == 0)
                    {
                        // skip empty lines
                        continue;
                    }

                    var LinkedContractDetail = Titan.ContractDetails
                        .Where(det => det.ConID == ConID && det.StockCode == part.StockCode)
                        .Single();

                    var IsSerialised = LinkedContractDetail.IsSerialised == 1;

                    // if serialised, add a seperate line for each single part
                    if (IsSerialised)
                    {
                        for (int i = part.Quantity; i > 0; i--)
                        {
                            Titan.OrderDetails.Add(new OrderDetail
                            {
                                OrdID = OrderHeader.OrdID,
                                DateCreated = Now,
                                CreatedBy = HttpContext.GetUserDisplayName(),

                                ConDetID = LinkedContractDetail.ConDetID,

                                LeadTime = LinkedContractDetail.LeadTime,
                                DirtyStockCode = LinkedContractDetail.DirtyStockCode,
                                StockCode = LinkedContractDetail.StockCode,
                                Description = LinkedContractDetail.Description,
                                WorkRequired = LinkedContractDetail.WorkRequired,
                                DeliveryTerms = LinkedContractDetail.DeliveryTerms,
                                DespatchMethod = LinkedContractDetail.DespatchMethod,
                                DeliveryDays =
                                    (LinkedContractDetail.DespatchMethod == "Pallet Network")
                                        ? 1 : 0,

                                Colour = LinkedContractDetail.Colour,
                                Warranty = LinkedContractDetail.Warranty,
                                UnitPrice = LinkedContractDetail.UnitPrice,
                                SpecialInstruction = LinkedContractDetail.SpecialInstruction,

                                QtyReceived = 1,
                                ExpectedQty = LinkedContractDetail.QtyRequired
                            });
                        }
                    }
                    else
                    {
                        {
                            // otherwise add all at once
                            Titan.OrderDetails.Add(new OrderDetail
                            {
                                OrdID = OrderHeader.OrdID,
                                DateCreated = Now,
                                CreatedBy = HttpContext.GetUserDisplayName(),

                                ConDetID = LinkedContractDetail.ConDetID,

                                LeadTime = LinkedContractDetail.LeadTime,
                                DirtyStockCode = LinkedContractDetail.DirtyStockCode,
                                StockCode = LinkedContractDetail.StockCode,
                                Description = LinkedContractDetail.Description,
                                WorkRequired = LinkedContractDetail.WorkRequired,
                                DeliveryTerms = LinkedContractDetail.DeliveryTerms,
                                DespatchMethod = LinkedContractDetail.DespatchMethod,
                                DeliveryDays =
                                    (LinkedContractDetail.DespatchMethod == "Pallet Network")
                                        ? 1 : 0,

                                Colour = LinkedContractDetail.Colour,
                                Warranty = LinkedContractDetail.Warranty,
                                UnitPrice = LinkedContractDetail.UnitPrice,
                                SpecialInstruction = LinkedContractDetail.SpecialInstruction,

                                QtyReceived = part.Quantity,
                                ExpectedQty = LinkedContractDetail.QtyRequired
                            });
                        }
                    }
                }
            }
            catch
            {
                // we haven't written to the DB so no rollback needed
                // revert local changes that failed
                Titan.ChangeTracker.Clear();
                return UnprocessableEntity();
            }

            Contract.DeliveriesReceived += 1;

            Titan.SaveChanges();
            return Ok(Titan.OrderHeaders
                    .Where(Order => Order.OrdID == OrderID)
                    .Single()
                    .ToOrderDTO(Titan)
                );
        }

        /// <response code="200">Returns succesfully created SOR number</response>
        /// <response code="400">Cannot generate SOR for Order without customer</response>
        /// <response code="404">Could not find Order to create SOR from</response>
        /// <response code="422">Could not process order line</response>
        [HttpPost]
        [Route("{OrderID}/GenerateSOR")]
        [Feature("CreateOrdersInSage")]
        public async Task<ActionResult> ApproveAndGenerateSORForOrder(int OrderID)
        {
            var Order = Titan.OrderHeaders
                .SingleOrDefault(Order => Order.OrdID == OrderID)
                .ToOrderDTO(Titan);

            if (Order == null)
            {
                return NotFound();
            }

            var OrderCustomer = Titan.Customers
                .Where(Customer => Customer.CusID == Order.CusID)
                .SingleOrDefault();

            if (OrderCustomer == null)
            {
                return BadRequest();
            }

            if (Order.OrderDetails.Any(Line => Line.UnitPrice == null))
            {
                return UnprocessableEntity();
            }

            // send to sage
            await SageAPI.CreateSalesOrderAsync(Order.OrderID);

            var CompletedOrder = Titan.OrderHeaders
                .SingleOrDefault(Order => Order.OrdID == OrderID);

            CompletedOrder.OrdIsAuthorised = 1;
            CompletedOrder.ApprovedBy = HttpContext.GetUserDisplayName();

            Titan.SaveChanges();

            return Ok();
        }
    }
}
