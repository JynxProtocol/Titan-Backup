using System.Collections.Generic;
using System.Linq;
using Titan.ReportEngine.Controllers;
using Titan.ReportEngine.Data;

namespace Titan.ReportEngine.DataSources
{
    public class Stock : IDataSource
    {
        internal decimal totalValueOfStock => Parts.Sum(part => part.totalValue);

        public int OngoingAllocations { get; set; }
        public string TotalValueOfStock
        {
            get => string.Format(ReportController.Culture, "{0:C}", totalValueOfStock);
        }
        public int PartsLow { get; set; }
        public List<StockPart> Parts { get; set; }

        public class StockPart
        {
            internal decimal unitCost;
            internal decimal totalValue => unitCost * NumberInStock;

            public string Code { get; set; }
            public string Description { get; set; }
            public string UnitCost
            {
                get => string.Format(ReportController.Culture, "{0:C}", unitCost);
            }
            public decimal NumberInStock { get; set; }
            public string TotalValue
            {
                get => string.Format(ReportController.Culture, "{0:C}", totalValue);
            }
        }

        public Stock()
        {
            OngoingAllocations = 110;
            PartsLow = 55;
            Parts = new List<Stock.StockPart>()
                .Append(new Stock.StockPart()
                {
                    Code = "1400-100-002",
                    Description = "Turboencabulator vane",
                    NumberInStock = 7,
                    unitCost = 45.28M
                })
                .Append(new Stock.StockPart()
                {
                    Code = "1400-100-003",
                    Description = "Turboencabulator casing",
                    NumberInStock = 3,
                    unitCost = 70.00M
                })
                .Append(new Stock.StockPart()
                {
                    Code = "1400-100-002",
                    Description = "Turboencabulator base plate",
                    NumberInStock = 21,
                    unitCost = 27.33M
                })
                .Append(new Stock.StockPart()
                {
                    Code = "1400-100-002",
                    Description = "Turboencabulator panametric fan",
                    NumberInStock = 8,
                    unitCost = 21.00M
                })
                .ToList();
        }

        public Stock(string id)
        {
            var Sage = new SAGEContext();

            var SageCategory = Sage.ProductGroups
                .Where(group => group.Code == id)
                .Single();

            var SageStockInCategory = Sage.StockItems
                .Where(item => item.ProductGroupID == SageCategory.ProductGroupID)
                .ToList();

            Parts = new List<Stock.StockPart>();

            foreach (var Item in SageStockInCategory)
            {
                var Bins = Sage.BinItems
                    .Where(BinItem => BinItem.ItemID == Item.ItemID)
                    .ToList();

                var AllocationsAgainstItem = (int)Bins.Sum(binItem =>
                    binItem.QuantityAllocatedBOM +
                    binItem.QuantityAllocatedSOP +
                    binItem.QuantityAllocatedStock);

                if (AllocationsAgainstItem > 0)
                {
                    OngoingAllocations++;
                }

                Parts.Add(new StockPart()
                {
                    Code = Item.Code,
                    Description = Item.Name,
                    NumberInStock = Bins.Sum(binItem =>
                        ((binItem.ConfirmedQtyInStock + binItem.UnconfirmedQtyInStock) -
                            AllocationsAgainstItem)),
                    unitCost = (int)(Item.AverageBuyingPrice)
                });
            }
        }
    }
}
