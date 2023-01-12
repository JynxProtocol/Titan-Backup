using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Titan.ReportEngine.Controllers;
using Titan.ReportEngine.Data;

namespace Titan.ReportEngine.DataSources
{
    public class OrderAcknowledgement : IDataSource
    {
        static internal int RoundFigures = 2;

        internal DateTime clientOrderDate;
        internal DateTime sabreOrderDate;
        internal int terms;

        internal decimal TaxRate { get; }
        internal decimal totalNet => Items.Sum(item => item.total);
        internal decimal totalGross => totalNet * (TaxRate + 1);
        internal decimal totalVAT => totalGross - totalNet;

        public Address DeliveryAddress { get; set; }
        public Address InvoiceAddress { get; set; }
        public string SabreOrderRef { get; set; }
        public string SabreOrderDate
        {
            get => string.Format(ReportController.Culture, "{0:dd/MM/yyyy}", sabreOrderDate);
        }
        public string ClientOrderRef { get; set; }
        public string ClientOrderDate
        {
            get
            {
                if (clientOrderDate == DateTime.MinValue)
                {
                    return "";
                }

                return string.Format(ReportController.Culture, "{0:dd/MM/yyyy}", clientOrderDate);
            }
        }
        public string CustomerAccountNumber { get; set; }

        public string TotalNet
        {
            get => string.Format(ReportController.Culture, "{0:C}", totalNet);
        }
        public string TotalVAT
        {
            get => string.Format(ReportController.Culture, "{0:C}", totalVAT);
        }
        public string TotalGross
        {
            get => string.Format(ReportController.Culture, "{0:C}", totalGross);
        }

        public string Terms { get => string.Format(ReportController.Culture, "{0:N0}", terms); }
        public string DeliveryMethod { get; set; }

        public List<OrderAckItem> Items { get; set; } = new();

        public class OrderAckItem
        {
            internal decimal price;
            internal decimal total => price * Quantity;

            public string Code { get; set; }
            public string Description { get; set; }
            public string Warrantry { get; set; }
            public string Colour { get; set; }
            public int Quantity { get; set; }
            public string Price
            {
                get => string.Format(ReportController.Culture, "{0:C}", price);
            }
            public string Total
            {
                get => string.Format(ReportController.Culture, "{0:C}", total);
            }
        }

        public OrderAcknowledgement(string id)
        {
            var Titan = new TitanContext();
            var Sage = new SAGEContext();

            var CONID = int.Parse(id);

            var Contract = Titan.ContractHeaders
                .Where(con => con.ConID == CONID).Single();
            var Invoice = Sage.SLCustomerLocations
                .Where(myin => myin.SLCustomerAccountID == Contract.conCusID).Single();
            var Account = Sage.SLCustomerAccounts
                .Where(myac => myac.SLCustomerAccountID == Contract.conCusID).Single();

            RoundFigures = Titan.Settings.Select(Rnd => Rnd.RndValue).Single() ?? RoundFigures;

            DeliveryAddress = new Address
            {
                PostalName = Contract.PostalName,
                AddressLine1 = Contract.AddressLine1,
                AddressLine2 = Contract.AddressLine2,
                AddressLine3 = Contract.AddressLine3,
                AddressLine4 = Contract.AddressLine4,
                PostCode = Contract.PostCode
            };

            InvoiceAddress = new Address
            {
                PostalName = Account.CustomerAccountName,
                AddressLine1 = Invoice.AddressLine1,
                AddressLine2 = Invoice.AddressLine2,
                AddressLine3 = Invoice.AddressLine3,
                AddressLine4 = Invoice.AddressLine4,
                PostCode = Invoice.PostCode
            };

            SabreOrderRef = "TSOR" + Contract.ConID.ToString("D10");
            sabreOrderDate = Contract.DateCreated ?? DateTime.MinValue;

            ClientOrderRef = Contract.CustomerOrderNumber;
            clientOrderDate = Contract.CustomerOrderDate ?? DateTime.MinValue;

            CustomerAccountNumber = Account.CustomerAccountNumber;

            DeliveryMethod = Contract.DeliveryTerms;
            terms = Account.PaymentTermsInDays;

            TaxRate = Sage.SLCustomerAccounts
                .Include(account => account.DefaultSYSTaxRate)
                .Where(account => account.SLCustomerAccountID == Contract.conCusID)
                .Select(account => account.DefaultSYSTaxRate.TaxRate / 100)
                .Single();

            Items = Titan.ContractDetails.Where(con => con.ConID == CONID)
                 .Select(detail => new OrderAckItem
                 {
                     Code = detail.StockCode,
                     Description = detail.Description,
                     Warrantry = detail.Warranty,
                     Colour = detail.Colour,
                     Quantity = detail.TotalQty ?? 0,
                     price = detail.UnitPrice ?? 0
                 })
                 .ToList();
        }
    }
}
