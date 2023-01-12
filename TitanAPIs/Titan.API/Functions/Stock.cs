using System.Linq;
using Titan.API.Models;

namespace Titan.API.Functions
{
    public static class Stock
    {
        public static void CopyStockToTitanIfNotThere(StockHeader Stock,
            TitanContext Titan, SAGEContext Sage)
        {
            if (Stock.StkID != default)
            {
                if (!Titan.StockHeaders.Any(stk => stk.StkID == Stock.StkID))
                {
                    var SageStock = Sage.StockItems
                        .Where(stk => stk.ItemID == Stock.StkID)
                        .Single();

                    Titan.StockHeaders.Add(new StockHeader
                    {
                        StkID = Stock.StkID,
                        DirtyStockCode = Stock.DirtyStockCode,
                        StockCode = Stock.StockCode,
                        Description = SageStock.Name
                    });
                }
            }
            else
            {
                if (!Titan.StockHeaders.Any(stk => stk.StockCode == Stock.StockCode))
                {
                    var SageStock = Sage.StockItems
                        .Where(stk => stk.Code == Stock.StockCode)
                        .Single();

                    Titan.StockHeaders.Add(new StockHeader
                    {
                        StkID = (int)SageStock.ItemID,
                        DirtyStockCode = Stock.DirtyStockCode,
                        StockCode = Stock.StockCode,
                        Description = SageStock.Name
                    });
                }
            }
        }

        public static AWKStockHeader CopyAWKStockToTitanIfNotThere(int? SageStkID,
            string StockCode, TitanContext Titan, SAGEContext Sage)
        {
            if (SageStkID != default)
            {
                if (!Titan.AWKStockHeaders.Any(stk => stk.StkID == SageStkID))
                {
                    var SageStock = Sage.StockItems
                        .Where(stk => stk.ItemID == SageStkID)
                        .Single();

                    Titan.AWKStockHeaders.Add(new AWKStockHeader
                    {
                        StkID = (int)SageStkID,
                        StockCode = StockCode,
                        Description = SageStock.Name
                    });
                }
            }
            else
            {
                if (!Titan.AWKStockHeaders.Any(stk => stk.StockCode == StockCode))
                {
                    var SageStock = Sage.StockItems
                        .Where(stk => stk.Code == StockCode)
                        .Single();

                    Titan.AWKStockHeaders.Add(new AWKStockHeader
                    {
                        StkID = (int)SageStock.ItemID,
                        StockCode = StockCode,
                        Description = SageStock.Name
                    });
                }
            }

            return Titan.AWKStockHeaders.Single(stk => stk.StockCode == StockCode);
        }
    }
}
