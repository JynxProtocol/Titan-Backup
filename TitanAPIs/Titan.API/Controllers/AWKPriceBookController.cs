using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Titan.API.Models.AWK;

namespace Titan.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class AWKPriceBookController : Controller
    {
        internal TitanContext Titan { get; private set; }

        internal DateTime Now = DateTime.Now;

        public AWKPriceBookController(TitanContext titan)
        {
            Titan = titan;
        }

        /// <response code="200">Succesfully returns a list of stock prices</response>
        [HttpGet]
        [Route("Prices")]
        public List<AWKItemPrice> ListAWKStockPrices()
        {
            return Titan.AWKStockHeaders
                .Select(hd => new AWKItemPrice
                {
                    StkID = hd.StkID,
                    PartNumber = hd.StockCode,
                    Description = hd.Description,
                    Price = hd.Price ?? 0,
                    DateLastUpdated = hd.DateLastUpdated,
                })
                .ToList();
        }

        /// <response code="200">Succesfully returns stock price</response>
        [HttpGet]
        [Route("{StockID}/Price")]
        public AWKItemPrice GetAWKStockPrice(int StockID)
        {

            return Titan.AWKStockHeaders
                .Where(hd => hd.StkID == StockID)
                .Select(hd => new AWKItemPrice
                {
                    StkID = hd.StkID,
                    PartNumber = hd.StockCode,
                    Description = hd.Description,
                    Price = hd.Price ?? 0
                })
                .Single();
        }

        /// <response code="200">Succesfully updated stock price</response>
        [HttpPost]
        [Route("{StockID}/Price")]
        public IActionResult SetAWKStockPrice(int StockID, decimal NewPrice)
        {
            var Header = Titan.AWKStockHeaders
                .Where(con => con.StkID == StockID)
                .FirstOrDefault();

            Header.Price = NewPrice;

            Header.LastUpdatedBy = HttpContext.GetUserDisplayName();
            Header.DateLastUpdated = Now;

            Titan.SaveChanges();

            return Ok();
        }
    }
}
