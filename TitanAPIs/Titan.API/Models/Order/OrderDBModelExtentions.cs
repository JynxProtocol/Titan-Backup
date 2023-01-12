using System.Linq;

namespace Titan.API.Models.DGRN
{
    public static class OrderDBModelExtentions
    {
        public static OrderDTO ToOrderDTO(this OrderHeader Order, TitanContext Titan)
        {
            var OrderDetails = Titan.OrderDetails
                .Where(Detail => Detail.OrdID == Order.OrdID)
                .Select(Detail => new OrderDetailDTO
                {
                    OrderDetailID = Detail.DetID,
                    OrderID = (int)Detail.OrdID,
                    ConDetID = Detail.ConDetID,
                    ExpectedQty = Detail.ExpectedQty,
                    CreatedBy = Detail.CreatedBy,
                    DateCreated = Detail.DateCreated,
                    DetSODID = Detail.DetSODID,
                    WONumber = Detail.WONumber,
                    DirtyStockCode = Detail.DirtyStockCode,
                    StockCode = Detail.StockCode,
                    Description = Detail.Description,
                    WorkRequired = Detail.WorkRequired,
                    Colour = Detail.Colour,
                    Warranty = Detail.Warranty,
                    LeadTime = Detail.LeadTime,
                    DeliveryTerms = Detail.DeliveryTerms,
                    DespatchMethod = Detail.DespatchMethod,
                    DeliveryDays = Detail.DeliveryDays,
                    QtyReceived = Detail.QtyReceived,
                    UnitPrice = Detail.UnitPrice,
                    SpecialInstruction = Detail.SpecialInstruction,
                    SerialNumber = Detail.SerialNumber,
                })
                .ToList();

            return new OrderDTO
            {
                OrderID = Order.OrdID,
                ConID = Order.ConID,
                DateCreated = Order.DateCreated,
                CreatedBy = Order.CreatedBy,
                DateReceived = Order.DateReceived,
                CusOrderNumber = Order.CusOrderNumber,
                CustomerName = Order.CustomerName,
                CusID = Order.CusID,
                OrdCustomerRef = Order.OrdCustomerRef,
                OrdSpecialInstruction = Order.OrdSpecialInstruction,
                PostalName = Order.PostalName,
                AddressLine1 = Order.AddressLine1,
                AddressLine2 = Order.AddressLine2,
                AddressLine3 = Order.AddressLine3,
                AddressLine4 = Order.AddressLine4,
                City = Order.City,
                County = Order.County,
                PostCode = Order.PostCode,
                DeliveryTerms = Order.DeliveryTerms,
                DespatchMethod = Order.DespatchMethod,
                Approved = Order.OrdIsAuthorised == 1,
                ApprovedBy = Order.ApprovedBy,
                SOR = Order.SOR,
                GRN = Order.GRN,

                OrderDetails = OrderDetails
            };
        }

        public static OrderDetailDTO ToOrderDetailDTO(this OrderDetail Detail)
        {
            return new OrderDetailDTO
            {
                OrderDetailID = Detail.DetID,
                OrderID = (int)Detail.OrdID,
                ConDetID = Detail.ConDetID,
                ExpectedQty = Detail.ExpectedQty,
                CreatedBy = Detail.CreatedBy,
                DateCreated = Detail.DateCreated,
                DetSODID = Detail.DetSODID,
                WONumber = Detail.WONumber,
                DirtyStockCode = Detail.DirtyStockCode,
                StockCode = Detail.StockCode,
                Description = Detail.Description,
                WorkRequired = Detail.WorkRequired,
                Colour = Detail.Colour,
                Warranty = Detail.Warranty,
                LeadTime = Detail.LeadTime,
                DeliveryTerms = Detail.DeliveryTerms,
                DespatchMethod = Detail.DespatchMethod,
                DeliveryDays = Detail.DeliveryDays,
                QtyReceived = Detail.QtyReceived,
                UnitPrice = Detail.UnitPrice,
                SpecialInstruction = Detail.SpecialInstruction,
                SerialNumber = Detail.SerialNumber,
            };
        }
    }
}
