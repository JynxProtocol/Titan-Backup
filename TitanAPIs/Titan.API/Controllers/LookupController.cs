using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using Titan.API.Models;
using Titan.API.Models.AWK;
using Titan.API.Models.Lookup;
using Titan.Filters;

namespace Titan.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class LookupController : Controller
    {
        internal TitanContext Titan { get; private set; }
        internal SAGEContext Sage { get; private set; }
        internal static string ID { get; private set; }

        public LookupController(TitanContext titan, SAGEContext sage)
        {
            Titan = titan;
            Sage = sage;
            ID = System.Guid.NewGuid().ToString();
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} " +
                $"creates controller {ID} with context {Titan.ContextId}, " +
                $"new context {titan.ContextId}");
        }


        /// <response code="200">Succesfully returns a list of colour options</response>
        [HttpGet]
        [Route("Colours")]
        public List<DropdownOption> GetColourList()
        {
            return Titan.LookUpColours
                .OrderBy(Colour => Colour.LookupText)
                .Select(Colour => new DropdownOption()
                {
                    Value = Colour.LookupText,
                    Text = Colour.LookupText
                })
                .ToList();
        }

        /// <response code="200">Succesfully returns a list of SalesTypes options</response>
        [HttpGet]
        [Route("SalesType")]
        public List<DropdownOption> GetSalesTypeList()
        {
            return Titan.LookUpAWKSalesTypes
                .OrderBy(SalesType => SalesType.LookupText)
                .Select(SalesType => new DropdownOption()
                {
                    Value = SalesType.LookupText,
                    Text = SalesType.LookupText
                })
                .ToList();
        }

        /// <response code="200">Succesfully returns a list of warranty options</response>
        [HttpGet]
        [Route("WarrantyTypes")]
        public List<DropdownOption> GetWarrantyList()
        {
            return Titan.LookUpWarranties
                .OrderBy(Warranty => Warranty.LookupText)
                .Select(Warranty => new DropdownOption()
                {
                    Value = Warranty.LookupText,
                    Text = Warranty.LookupText
                })
                .ToList();
        }

        /// <response code="200">Succesfully returns a list of lead time options</response>
        [HttpGet]
        [Route("LeadTimes")]
        public List<DropdownOption> GetLeadTimesList()
        {
            return Titan.LookUpLeadTimes
                .OrderBy(LeadTime => LeadTime.LookupText)
                .Select(LeadTime => new DropdownOption()
                {
                    Value = LeadTime.LookupText,
                    Text = LeadTime.LookupText + " days"
                })
                .ToList();
        }

        /// <response code="200">Succesfully returns a list of work required options</response>
        [HttpGet]
        [Route("WorkRequiredOptions")]
        public List<DropdownOption> GetWorkRequiredOptionsList()
        {
            return Titan.LookUpWorkRequireds
                .OrderBy(WorkRequired => WorkRequired.LookupText)
                .Select(WorkRequired => new DropdownOption()
                {
                    Value = WorkRequired.LookupText,
                    Text = WorkRequired.LookupText
                })
                .ToList();
        }

        /// <response code="200">Succesfully returns a list of despatch method options</response>
        [HttpGet]
        [Route("DespatchMethods")]
        public List<DropdownOption> GetDespatchMethodList()
        {
            return Titan.LookUpDespatchMethods
                .OrderBy(DespatchMethod => DespatchMethod.LookupText)
                .Select(DespatchMethod => new DropdownOption()
                {
                    Value = DespatchMethod.LookupText,
                    Text = DespatchMethod.LookupText
                })
                .ToList();
        }

        /// <response code="200">Succesfully returns a list of delivery terms options</response>
        [HttpGet]
        [Route("DeliveryTerms")]
        public List<DropdownOption> GetDeliveryTermsList()
        {
            return Titan.LookUpDeliveryTerms
                .OrderBy(DeliveryTerms => DeliveryTerms.LookupText)
                .Select(DeliveryTerms => new DropdownOption()
                {
                    Value = DeliveryTerms.LookupText,
                    Text = DeliveryTerms.LookupText
                })
                .ToList();
        }

        /// <response code="200">Succesfully returns a list of customer options</response>
        [HttpGet]
        [Route("Customers")]
        [Feature("ViewCustomers")]
        public List<DropdownOption> GetCustomersList()
        {
            return Sage.SLCustomerAccounts
                .OrderBy(Customer => Customer.CustomerAccountName)
                .Select(Customer => new DropdownOption()
                {
                    Value = Customer.SLCustomerAccountID.ToString(),
                    Text = Customer.CustomerAccountName
                })
                .ToList();
        }

        /// <response code="200">Succesfully returns a list of product group options</response>
        [HttpGet]
        [Route("ProductGroups")]
        public List<DropdownOption> GetProductGroups()
        {
            return Sage.ProductGroups
                .OrderBy(ProductGroup => ProductGroup.Code)
                .Select(Customer => new DropdownOption()
                {
                    Value = Customer.Code,
                    Text = Customer.Code
                })
                .ToList();
        }

        /// <response code="200">Succesfully returns information about a rapairable</response>
        /// <response code="404">Could not find BOM referenced by top-level code</response>
        /// <response code="409">The referenced BOM does not have a specific
        /// repairable associated with it</response>
        [HttpGet]
        [Route("Parts/{TopLevelCode}/Repairable")]
        public ActionResult<PartCodeInformation> GetRepairableInformation(string TopLevelCode)
        {
            // codes often have slashes - decode them since MVC doesn't
            TopLevelCode = HttpUtility.UrlDecode(TopLevelCode);

            var BOMForThisRepairable = Sage.BomHeaders
                .Include(BOH => BOH.BomComponents)
                .Where(BomHeader => BomHeader.BomReference == TopLevelCode)
                .SingleOrDefault();

            if (BOMForThisRepairable == null)
            {
                return NotFound();
            }

            // fetch the end result, i.e. the repaired item
            var RepairedStockItem = Sage.StockItems
                .Single(Item => BOMForThisRepairable.BomReference == Item.Code);

            var BOMItemCodes = BOMForThisRepairable.BomComponents
                .Select(BomItem => BomItem.StockCode)
                .ToList();

            var BOMItems = Sage.StockItems
                .Where(Item => BOMItemCodes.Contains(Item.Code));

            var RepairablesProductGroupID = Sage.ProductGroups
                .Where(PG => PG.Code == "REPAIRABLES")
                .Single()
                .ProductGroupID;

            // fetch the item sabre recieves, i.e. the repairable item
            var RepairableStockItems = BOMItems
                .Where(Item => Item.ProductGroupID == RepairablesProductGroupID)
                .ToList();

            if (RepairableStockItems.Count != 1)
            {
                return Conflict();
            }

            var RepairableStockItem = RepairableStockItems.First();

            return new PartCodeInformation()
            {
                Description = BOMForThisRepairable.Description,
                StockCode = RepairedStockItem.Code,
                StockID = (int)RepairedStockItem.ItemID,
                RepairableCode = RepairableStockItem.Code
            };
        }

        /// <response code="200">Succesfully returns part by Code</response>
        /// <response code="404">Could not find the part referenced by the code</response>
        [HttpGet]
        [Route("Parts/{Code}")]
        public PartCodeInformation GetPartCodeInformation(string Code)
        {
            // codes often have slashes - decode them since MVC doesn't
            Code = HttpUtility.UrlDecode(Code);

            return Sage.StockItems
                .Where(stk => stk.Code == Code)
                .Select(stk => new PartCodeInformation()
                {
                    Description = stk.Name,
                    StockCode = stk.Code,
                    StockID = (int)stk.ItemID
                })
                .Single();
        }

        /// <response code="200">Returns stock code options for the partial stock code</response>
        [HttpGet]
        [Route("Parts/{StockCode}/Autocomplete")]
        public ActionResult<List<string>> AutocompleteStockCode(string StockCode)
        {
            // codes often have slashes - decode them since MVC doesn't
            StockCode = HttpUtility.UrlDecode(StockCode);

            return Sage.StockItems
                .Where(StockItem => StockItem.Code.Contains(StockCode))
                .Select(StockItem => StockItem.Code)
                .Take(20)
                .ToList();
        }

        /// <response code="200">Succesfully lists AWK fault options</response>
        [HttpGet]
        [Route("AWKFaults")]
        public IEnumerable<LookUpAWKFault> ListAWKFaults()
        {
            return Titan.LookUpAWKFaults.ToList();
        }

        /// <response code="200">Succesfully returns an AWK fault option</response>
        /// <response code="404">Could not find AWK fault</response>
        [HttpGet]
        [Route("AWKFaults/{id}")]
        public ActionResult<LookUpAWKFault> GetAWKFault(int id)
        {
            var AWKFault = Titan.LookUpAWKFaults
                .SingleOrDefault(AWKF => AWKF.id == id);

            if (AWKFault == null)
            {
                return NotFound();
            }

            return AWKFault;
        }

        /// <response code="200">Succesfully added and returned 
        /// an AWK fault option</response>
        [HttpPost]
        [Route("AWKFaults/Add/{FaultText}")]
        public ActionResult<LookUpAWKFault> AddAWKFault(string FaultText)
        {
            var NewFault = Titan.LookUpAWKFaults.Add(new LookUpAWKFault
            {
                LookupText = FaultText
            }).Entity;

            Titan.SaveChanges();

            return Ok(NewFault);
        }

        /// <response code="200">Succesfully updated an AWK fault option</response>
        /// <response code="404">Could not find AWK fault</response>
        [HttpPut]
        [Route("AWKFaults/{id}/FaultText/{FaultText}")]
        public ActionResult<LookUpAWKFault> EditAWKFault(int id,
            string FaultText)
        {
            var AWKFault = Titan.LookUpAWKFaults
                .SingleOrDefault(AWKF => AWKF.id == id);

            if (AWKFault == null)
            {
                return NotFound();
            }

            AWKFault.LookupText = FaultText;

            Titan.SaveChanges();

            return Ok(AWKFault);
        }

        /// <response code="200">Succesfully deleted AWK fault option</response>
        /// <response code="404">Could not find AWK fault</response>
        [HttpDelete]
        [Route("AWKFaults/{id}")]
        public ActionResult DeleteAWKFault(int id)
        {
            var AWKFault = Titan.LookUpAWKFaults
                .SingleOrDefault(AWKF => AWKF.id == id);

            if (AWKFault == null)
            {
                return NotFound();
            }

            Titan.LookUpAWKFaults.Remove(AWKFault);

            Titan.SaveChanges();

            return Ok();
        }

        /// <response code="200">Succesfully lists AWK work required options</response>
        [HttpGet]
        [Route("AWKWorkRequireds")]
        public IEnumerable<LookUpAWKWorkRequired> ListAWKWorkRequireds()
        {
            return Titan.LookUpAWKWorkRequireds.ToList();
        }

        /// <response code="200">Succesfully returns an AWK work required option</response>
        /// <response code="404">Could not find AWK work required</response>
        [HttpGet]
        [Route("AWKWorkRequireds/{id}")]
        public ActionResult<LookUpAWKWorkRequired> GetAWKWorkRequired(int id)
        {
            var AWKWorkRequired = Titan.LookUpAWKWorkRequireds
                .SingleOrDefault(AWKWR => AWKWR.Id == id);

            if (AWKWorkRequired == null)
            {
                return NotFound();
            }

            return AWKWorkRequired;
        }

        /// <response code="200">Succesfully added and returned 
        /// an AWK work required option</response>
        [HttpPost]
        [Route("AWKWorkRequireds/Add/{WorkRequiredText}")]
        public ActionResult<LookUpAWKWorkRequired> AddAWKWorkRequired(string WorkRequiredText)
        {
            var NewWorkRequired = Titan.LookUpAWKWorkRequireds.Add(new LookUpAWKWorkRequired
            {
                LookupText = WorkRequiredText
            }).Entity;

            Titan.SaveChanges();

            return Ok(NewWorkRequired);
        }

        /// <response code="200">Succesfully updated an AWK work required option</response>
        /// <response code="404">Could not find AWK work required</response>
        [HttpPut]
        [Route("AWKWorkRequireds/{id}/WorkRequiredText/{WorkRequiredText}")]
        public ActionResult<LookUpAWKWorkRequired> EditAWKWorkRequired(int id,
            string WorkRequiredText)
        {
            var AWKWorkRequired = Titan.LookUpAWKWorkRequireds
                .SingleOrDefault(AWKWR => AWKWR.Id == id);

            if (AWKWorkRequired == null)
            {
                return NotFound();
            }

            AWKWorkRequired.LookupText = WorkRequiredText;

            Titan.SaveChanges();

            return Ok(AWKWorkRequired);
        }

        /// <response code="200">Succesfully deleted AWK work required option</response>
        /// <response code="404">Could not find AWK work required</response>
        [HttpDelete]
        [Route("AWKWorkRequireds/{id}")]
        public ActionResult DeleteAWKWorkRequired(int id)
        {
            var AWKWorkRequired = Titan.LookUpAWKWorkRequireds
                .SingleOrDefault(AWKWR => AWKWR.Id == id);

            if (AWKWorkRequired == null)
            {
                return NotFound();
            }

            Titan.LookUpAWKWorkRequireds.Remove(AWKWorkRequired);

            Titan.SaveChanges();

            return Ok();
        }

        /// <response code="200">Returns WONumber options for the partial WONumber</response>
        [HttpGet]
        [Route("WorksOrders/{WONumber}/Autocomplete")]
        public ActionResult<List<string>> AutocompleteWorksOrderNumber(string WONumber)
        {
            return Sage.SiWorksOrders
                .Where(WO => WO.WOStatus != "Canceled")
                .Where(WO => WO.WOStatus != "Completed")
                .Where(WO => WO.WONumber.Contains(WONumber))
                .Select(WO => WO.WONumber)
                .OrderBy(WONumber => WONumber)
                .Take(20)
                .ToList();
        }

    }
}
