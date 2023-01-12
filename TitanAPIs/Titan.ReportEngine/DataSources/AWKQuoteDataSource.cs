using System;
using System.Collections.Generic;
using System.Linq;
using Titan.ReportEngine.Controllers;
using Titan.ReportEngine.Data;

namespace Titan.ReportEngine.DataSources
{
    public class AWKQuote : IDataSource
    {
        internal DateTime date;
        internal decimal grandTotal => Items.Sum(item => item.total);

        public string Client { get; set; }
        public string ClientContact { get; set; }
        public string SabreRailContact { get; set; }
        public int AWNumber { get; set; }
        public string CustomerOrderNumber { get; set; }
        public string SabreOrderNumber { get; set; }
        public string UnitDescription { get; set; }
        public string CatNumber { get; set; }
        public int NoOfUnits { get; set; }
        public string Date
        {
            get => string.Format(ReportController.Culture, "{0:dd/MM/yyyy}", date);
        }

        public List<AWKItem> Items { get; set; } = new();
        public string GrandTotal
        {
            get => string.Format(ReportController.Culture, "{0:C}", grandTotal);
        }

        public class AWKItem
        {
            internal decimal price;
            internal decimal total => price * Quantity;

            public string Description { get; set; }
            public string PartNumber { get; set; }
            public string Fault { get; set; }
            public string Price
            {
                get => string.Format(ReportController.Culture, "{0:C}", price);
            }
            public int Quantity { get; set; }
            public string Total
            {
                get => string.Format(ReportController.Culture, "{0:C}", total);
            }
        }

        // constructor fills data from database
        public AWKQuote(string id)
        {
            var Titan = new TitanContext();

            var AWKID = int.Parse(id);

            // select the AWK
            var AWK = Titan.AWKHeaders.Where(AWK => AWK.ID == AWKID).Single();

            // fill single datastore values
            Client = AWK.AccountName;
            ClientContact = AWK.ContractName;
            SabreRailContact = AWK.AWKQuotedBy;
            AWNumber = AWK.ID;
            CustomerOrderNumber = AWK.CustOrderNumber;
            SabreOrderNumber = AWK.SOR;
            UnitDescription = AWK.Description;
            CatNumber = AWK.CatNo;
            NoOfUnits = AWK.AWKQty ?? 0;
            date = AWK.AWKQuotedDate ?? DateTime.MinValue;

            // get items from database
            Items = Titan.vw_AWKStockRecords
                .Where(AWKItem =>
                    AWKItem.DID == AWKID && AWKItem.RequiredQty > 0 && AWKItem.ComAccepted != true)
                .Select(AWKItem => new AWKItem
                {
                    Description = AWKItem.Description,
                    Fault = AWKItem.Fault,
                    PartNumber = AWKItem.StockCode,
                    price = AWKItem.RepairCost ?? 0,
                    Quantity = AWKItem.RequiredQty ?? 0
                })
                .ToList();
        }

        // constructor with test data
        public AWKQuote()
        {
            Client = "BOMBARDIER TRANS UK LTD CREWE";
            ClientContact = "Debbie Morris";
            SabreRailContact = "Mark Turner";
            AWNumber = 630;
            CustomerOrderNumber = "4501594906­-U2Y";
            SabreOrderNumber = "0000085739";
            UnitDescription = "Koni 9531 Sec Lateral Damper-Overhaul";
            CatNumber = "0098/071266/R";
            NoOfUnits = 1;
            date = new DateTime(2022, 03, 23);
            Items = new List<AWKItem>()
                .Append(new AWKItem
                {
                    Description = "Rod Oil Seal Nut",
                    PartNumber = "K02­-22­-03­-020­-0",
                    Fault = "Damaged beyond repair",
                    price = 11.5200M,
                    Quantity = 1
                })
                .Append(new AWKItem
                {
                    Description = "Pressure Tube 9542/9534/9531/9963/9543",
                    PartNumber = "K02­-17-­16­-175­-0",
                    Fault = "Excessive wear",
                    price = 28.9400M,
                    Quantity = 1
                })
                .Append(new AWKItem
                {
                    Description = "Oversized Piston",
                    PartNumber = "7001-­012-­901",
                    Fault = "Excessive wear",
                    price = 43.7000M,
                    Quantity = 1
                })
                .ToList();
        }
    }
}
