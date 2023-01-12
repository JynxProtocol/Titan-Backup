using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Titan.API.Models;
using Titan.API.Models.AWK;
using static Titan.API.Functions.Stock;

namespace Titan.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class PartsListController : Controller
    {
        internal TitanContext Titan { get; private set; }
        internal SAGEContext Sage { get; private set; }
        internal ILogger<PartsListController> logger { get; private set; }

        internal DateTime Now = DateTime.Now;

        public PartsListController(TitanContext titan, SAGEContext sage,
            ILogger<PartsListController> _logger)
        {
            Titan = titan;
            Sage = sage;
            logger = _logger;
        }

        /// <response code="200">Returns the list of parts lists</response>
        [HttpGet]
        [Route("PartsLists")]
        public List<PartsListHeader> ListPartsLists()
        {
            return Titan.PartsListHeaders
                .ToList();
        }

        /// <response code="200">Returns the id of the copied parts list</response>
        [HttpPost]
        [Route("PartsLists/{FPLHID}/Clone")]
        public ActionResult<int> ClonePartsList(int FPLHID)
        {
            PartsList myPartsList = new();

            myPartsList = Titan.PartsListHeaders.Where(hd => hd.PLHID == FPLHID)
                .Select(hd => new PartsList
                {
                    PLHID = hd.PLHID,
                    Title = hd.Title,
                    ProductGroup = hd.ProductGroup
                }).Single();

            var header = new PartsListHeader();

            try
            {
                header.Title = "Copy of Parts List: " + myPartsList.Title;

                header.ProductGroup = myPartsList.ProductGroup;

                header.CreatedBy = HttpContext.GetUserDisplayName();
                header.DateCreated = Now;

                Titan.PartsListHeaders.Add(header);
                Titan.SaveChanges();

                header.PLHID = header.PLHID;

                List<PartsListItem> myItems = Titan.PartsListDetails
                    .Where(itm => itm.PLHID == FPLHID)
                    .Select(det => new PartsListItem
                    {
                        PLDID = det.PLDID,
                        ItemNumber = det.ItemNumber ?? 0,
                        PartNumber = det.PartNumber,
                        Description = det.Description,
                        Mandatory = det.Mandatory ?? false,
                        SageStkID = det.SageStkID,
                        Qty = det.Qty ?? 0,
                        StockUnit = det.StockUnit,
                        IsAlternativePart = det.IsAlternativePart ?? false,
                        AlternativePartTo = det.AlternativePartTo,
                    })
                    .OrderBy(PartListItem => PartListItem.ItemNumber)
                    .ToList();

                myPartsList.PartsListItems = myItems;


                foreach (var itm in myItems)
                {
                    PartsListDetail myLine = new();

                    myLine.PLHID = header.PLHID;

                    myLine.ItemNumber = itm.ItemNumber;
                    myLine.PartNumber = itm.PartNumber;
                    myLine.Description = itm.Description;
                    myLine.Mandatory = itm.Mandatory;
                    myLine.Qty = itm.Qty;
                    myLine.SageStkID = itm.SageStkID;

                    myLine.CreatedBy = HttpContext.GetUserDisplayName();
                    myLine.DateCreated = Now;

                    Titan.PartsListDetails.Add(myLine);
                }

                Titan.SaveChanges();
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
            }

            return header.PLHID;
        }

        /// <response code="200">Returns the parts list</response>
        [HttpGet]
        [Route("PartsLists/{PLHID}")]
        public PartsList GetPartsList(int PLHID)
        {
            var myPartsList = Titan.PartsListHeaders
                .Where(hd => hd.PLHID == PLHID)
                .Select(hd => new PartsList

                {
                    PLHID = hd.PLHID,
                    Title = hd.Title,
                    ProductGroup = hd.ProductGroup
                }).Single();

            List<PartsListItem> myItems = Titan.PartsListDetails
                .Where(itm => itm.PLHID == PLHID)
                .Select(det => new PartsListItem
                {
                    PLHID = det.PLHID ?? myPartsList.PLHID,
                    PLDID = det.PLDID,
                    ItemNumber = det.ItemNumber ?? 0,
                    PartNumber = det.PartNumber,
                    Description = det.Description,
                    Mandatory = det.Mandatory ?? false,
                    Qty = det.Qty ?? 0,
                    StockUnit = det.StockUnit,
                    IsAlternativePart = det.IsAlternativePart ?? false,
                    AlternativePartTo = det.AlternativePartTo,
                }).OrderBy(PartListItem => PartListItem.ItemNumber).ToList();

            List<PartsListCatItem> Cats = Titan.PartsListCats
                .Where(itm => itm.PLHID == PLHID)
                .Select(det => new PartsListCatItem
                {
                    PLHID = det.PLHID ?? myPartsList.PLHID,
                    CATID = det.CATID,
                    CatNumber = det.CatNumber,
                    Description = det.Description,
                }).ToList();

            myPartsList.PartsListCats = Cats;

            myPartsList.PartsListItems = myItems;

            return myPartsList;
        }

        /// <response code="200">Returns the id of the created parts list</response>
        [HttpPost]
        [Route("PartsLists/Add/{ProductGroup}/{Title}")]
        public ActionResult<int> AddPartsList(string ProductGroup, string Title)
        {
            var PLH = new PartsListHeader()
            {
                Title = Title,
                ProductGroup = ProductGroup,
                CreatedBy = HttpContext.GetUserDisplayName(),
                DateCreated = Now,
            };

            Titan.PartsListHeaders.Add(PLH);

            Titan.SaveChanges();

            return Ok(PLH.PLHID);
        }

        /// <response code="200">Succesfully updated parts list</response>
        /// <response code="404">Could not find parts list to update</response>
        [HttpPost]
        [Route("PartsLists/{PLHID}")]
        public ActionResult UpdatePartsList(int PLHID, string Title, string ProductGroup)
        {
            var header = Titan.PartsListHeaders
                .Where(con => con.PLHID == PLHID)
                .FirstOrDefault();

            if (header == null)
            {
                return NotFound();
            }

            header.Title = Title;
            header.ProductGroup = ProductGroup;

            header.LastUpdatedBy = HttpContext.GetUserDisplayName();
            header.DateLastUpdated = Now;

            Titan.SaveChanges();

            return Ok();
        }

        /// <response code="200">Succesfully deleted parts list</response>
        [HttpDelete]
        [Route("PartsLists/{PLHID}")]
        public ActionResult DeletePartsList(int PLHID)
        {

            Titan.PartsListDetails.RemoveRange(Titan.PartsListDetails.Where(x => x.PLHID == PLHID));

            Titan.PartsListHeaders
                .Remove(Titan.PartsListHeaders.Find(PLHID));


            Titan.SaveChanges();

            return Ok();
        }

        /// <response code="200">Returns part</response>
        [HttpGet]
        [Route("PartsLists/{PLHID}/Parts/{PLDID}")]
        public PartsListItem GetPartsListPart(int PLHID, int PLDID)
        {
            var myitem = Titan.PartsListDetails
                .Where(ord => ord.PLDID == PLDID)
                .Select(det => new PartsListItem
                {
                    PartNumber = det.PartNumber,
                    Description = det.Description,
                    ItemNumber = det.ItemNumber ?? 0,

                    PLDID = det.PLDID,

                    PLHID = det.PLHID,
                    Qty = det.Qty ?? 0,
                    Mandatory = det.Mandatory ?? false
                })
                .Single();

            return myitem;
        }

        /// <response code="200">Succesfully added part to list</response>
        /// <response code="400">Could not find stock code</response>
        /// <response code="404">Could not find parts list to add to</response>
        [HttpPost]
        [Route("PartsLists/{PLHID}/Parts/Add")]
        public ActionResult AddPartToPartsList(int PLHID, PartsListItem myItem)
        {
            var header = Titan.PartsListHeaders
                .Where(con => con.PLHID == PLHID)
                .FirstOrDefault();

            if (header == null)
            {
                return NotFound();
            }

            AWKStockHeader Stock;
            try
            {
                Stock = CopyAWKStockToTitanIfNotThere(myItem.SageStkID, myItem.PartNumber,
                    Titan, Sage);
            }
            catch (Exception)
            {
                return BadRequest();
            }

            Titan.PartsListDetails.Add(new PartsListDetail
            {
                PLHID = PLHID,
                CreatedBy = HttpContext.GetUserDisplayName(),
                DateCreated = Now,
                ItemNumber = myItem.ItemNumber,
                Qty = myItem.Qty,
                PartNumber = Stock.StockCode,
                Description = myItem.Description,
                Mandatory = myItem.Mandatory,
                SageStkID = Stock.StkID
            });

            header.LastUpdatedBy = HttpContext.GetUserDisplayName();
            header.DateLastUpdated = Now;

            Titan.SaveChanges();

            return Ok();
        }

        /// <response code="200">Succesfully added part to list</response>
        /// <response code="400">Could not find stock code</response>
        /// <response code="404">Could not find parts list or parts list detail to update</response>
        [HttpPost]
        [Route("PartsLists/{PLHID}/Parts/{PLDID}/Update")]
        public ActionResult UpdatePartsListPart(int PLHID, int PLDID, PartsListItem myItem)
        {
            var header = Titan.PartsListHeaders
                .Where(con => con.PLHID == PLHID)
                .FirstOrDefault();

            if (header == null)
            {
                return NotFound();
            }

            PartsListDetail myLine = Titan.PartsListDetails
                .Where(det => det.PLDID == myItem.PLDID).SingleOrDefault();

            if (myLine == null)
            {
                return NotFound();
            }

            AWKStockHeader Stock;
            try
            {
                Stock = CopyAWKStockToTitanIfNotThere(myItem.SageStkID, myItem.PartNumber,
                    Titan, Sage);
            }
            catch (Exception)
            {
                return BadRequest();
            }

            myLine.ItemNumber = myItem.ItemNumber;
            myLine.Qty = myItem.Qty;
            myLine.PartNumber = Stock.StockCode;
            myLine.Description = myItem.Description;
            myLine.Mandatory = myItem.Mandatory;
            myLine.SageStkID = Stock.StkID;

            header.LastUpdatedBy = HttpContext.GetUserDisplayName();
            header.DateLastUpdated = Now;

            Titan.SaveChanges();

            return Ok();
        }

        /// <response code="200">Succesfully deleted part</response>
        /// <response code="404">Could not find parts list or parts list detail to delete</response>
        [HttpDelete]
        [Route("PartsLists/{PLHID}/Parts/{PLDID}")]
        public ActionResult RemovePartFromPartsList(int PLHID, int PLDID)
        {
            var header = Titan.PartsListHeaders
                .Where(con => con.PLHID == PLHID)
                .FirstOrDefault();

            if (header == null)
            {
                return NotFound();
            }

            PartsListDetail myLine = Titan.PartsListDetails
                .Where(det => det.PLDID == PLDID).SingleOrDefault();

            if (myLine == null)
            {
                return NotFound();
            }

            Titan.PartsListDetails.Remove(myLine);
            Titan.SaveChanges();

            return Ok();
        }

        /// <response code="200">Succesfully returns information about Cat</response>
        [HttpGet]
        [Route("PartsLists/{PLHID}/Cats/{CATID}")]
        public PartsListCatItem GetCat(int PLHID, int CATID)
        {
            return Titan.PartsListCats.Where(ord => ord.CATID == CATID)
                .Select(det => new PartsListCatItem
                {
                    CatNumber = det.CatNumber,
                    Description = det.Description,

                    CATID = det.CATID,

                    PLHID = det.PLHID,
                })
                .Single();
        }

        /// <response code="200">Succesfully added Cat</response>
        [HttpPost]
        [Route("PartsLists/{PLHID}/AddCat/{CatNumber}")]
        public ActionResult AddCatToPartsList(int PLHID, string CatNumber, string Description)
        {
            CatNumber = HttpUtility.UrlDecode(CatNumber);

            var newCat = new Models.PartsListCat()
            {
                PLHID = PLHID,
                CreatedBy = HttpContext.GetUserDisplayName(),
                DateCreated = Now,
                CatNumber = CatNumber,
                Description = Description
            };

            Titan.PartsListCats.Add(newCat);
            Titan.SaveChanges();

            var header = Titan.PartsListHeaders.Where(con => con.PLHID == PLHID).FirstOrDefault();

            header.LastUpdatedBy = HttpContext.GetUserDisplayName();
            header.DateLastUpdated = Now;

            Titan.SaveChanges();

            return Ok();
        }

        /// <response code="200">Succesfully updated parts list Cat</response>
        [HttpPost]
        [Route("PartsLists/{PLHID}/Cats/{CATID}")]
        public ActionResult UpdateCat(int PLHID, int CATID, PartsListCatItem partsListCat)
        {
            //update this entry
            var myLine = Titan.PartsListCats.Where(det => det.CATID == CATID).Single();
            myLine.CatNumber = partsListCat.CatNumber;
            myLine.Description = partsListCat.Description;
            myLine.SageStkID = partsListCat.SageStkID;
            myLine.LastUpdatedBy = HttpContext.GetUserDisplayName();
            myLine.DateLastUpdated = Now;

            Titan.SaveChanges();

            var header = Titan.PartsListHeaders.Where(con => con.PLHID == PLHID).FirstOrDefault();

            header.LastUpdatedBy = HttpContext.GetUserDisplayName();
            header.DateLastUpdated = Now;

            Titan.SaveChanges();

            return Ok();
        }

        /// <response code="200">Succesfully removed parts list Cat</response>
        [HttpPost]
        [Route("PartsLists/{PLHID}/Cats/{CATID}/Remove")]
        public ActionResult RemoveCat(int PLHID, int CATID)
        {
            //update this entry
            var myLine = Titan.PartsListCats.Where(det => det.CATID == CATID).Single();

            Titan.PartsListCats.Remove(myLine);

            Titan.SaveChanges();

            var header = Titan.PartsListHeaders.Where(con => con.PLHID == PLHID).FirstOrDefault();

            header.LastUpdatedBy = HttpContext.GetUserDisplayName();
            header.DateLastUpdated = Now;

            Titan.SaveChanges();

            return Ok();
        }
    }
}
