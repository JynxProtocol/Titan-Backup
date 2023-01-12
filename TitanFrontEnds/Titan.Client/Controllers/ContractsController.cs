using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;
using Titan.Client.Functions;
using TitanAPIConnection;

namespace Titan.Client.Controllers
{
    public class ContractsController : Controller
    {
        internal TitanAPI TitanAPI { get; private set; }

        public IEnumerable<SelectListItem> Customers { get; private set; }
        public IEnumerable<SelectListItem> Colours { get; private set; }
        public IEnumerable<SelectListItem> Warranties { get; private set; }
        public IEnumerable<SelectListItem> LeadTimes { get; private set; }
        public IEnumerable<SelectListItem> WorkRequireds { get; private set; }
        public IEnumerable<SelectListItem> DeliveryTerms { get; private set; }
        public IEnumerable<SelectListItem> DespatchMethods { get; private set; }

        public ContractsController(TitanAPI titanAPI)
        {
            TitanAPI = titanAPI;

            Customers = TitanAPI.GetCustomersListAsync().Result
                .ToDropDown();
            Colours = TitanAPI.GetColourListAsync().Result
                .ToDropDown();
            Warranties = TitanAPI.GetWarrantyListAsync().Result
                .ToDropDown();
            LeadTimes = TitanAPI.GetLeadTimesListAsync().Result
                .ToDropDown();
            WorkRequireds = TitanAPI.GetWorkRequiredOptionsListAsync().Result
                .ToDropDown();
            DeliveryTerms = TitanAPI.GetDeliveryTermsListAsync().Result
                .ToDropDown();
            DespatchMethods = TitanAPI.GetDespatchMethodListAsync().Result
                .ToDropDown();
        }

        public async Task<ActionResult> Index()
        {
            var Contracts = await TitanAPI.ListOpenContractsAsync();

            return View(Contracts);
        }

        public async Task<ActionResult> Closed()
        {
            var Contracts = await TitanAPI.ListClosedContractsAsync();

            return View(Contracts);
        }

        public async Task<ActionResult> CreateContract()
        {
            ViewBag.CustomerOptions = Customers;
            ViewBag.DeliveryTermsOptions = DeliveryTerms;
            ViewBag.DespatchMethodOptions = DespatchMethods;

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateContract(Contract NewContract)
        {
            await TitanAPI.CreateContractAsync(NewContract);

            return RedirectToAction(nameof(Index));
        }

        [Route("[controller]/{ConID}/Edit")]
        public async Task<ActionResult> EditContract(int ConID)
        {
            var Contract = await TitanAPI.GetContractAsync(ConID);

            ViewBag.CustomerOptions = Customers;
            ViewBag.ColourOptions = Colours;
            ViewBag.WarrantyOptions = Warranties;
            ViewBag.LeadTimeOptions = LeadTimes;
            ViewBag.WorkRequiredOptions = WorkRequireds;
            ViewBag.DeliveryTermsOptions = DeliveryTerms;
            ViewBag.DespatchMethodOptions = DespatchMethods;

            return View(Contract);
        }

        [HttpPost]
        [Route("[controller]/{ConID}/Edit")]
        public async Task<ActionResult> EditContract(int ConID, Contract NewContract)
        {
            await TitanAPI.UpdateContractAsync(ConID, NewContract);

            return RedirectToAction(nameof(Index));
        }

        [Route("[controller]/{ConID:int}")]
        public async Task<ActionResult> ViewContract(int ConID)
        {
            ViewBag.CustomerOptions = Customers;
            ViewBag.ColourOptions = Colours;
            ViewBag.WarrantyOptions = Warranties;
            ViewBag.LeadTimeOptions = LeadTimes;
            ViewBag.WorkRequiredOptions = WorkRequireds;
            ViewBag.DeliveryTermsOptions = DeliveryTerms;
            ViewBag.DespatchMethodOptions = DespatchMethods;

            var ContractPart = await TitanAPI.GetContractAsync(ConID);

            return View(ContractPart);
        }

        [HttpPost]
        [Route("[controller]/{ConID}/Acknowledge")]
        public async Task<ActionResult> AcknowledgeContract(int ConID)
        {
            await TitanAPI.AcknowledgeContractAsync(ConID);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [Route("[controller]/{ConID}/Deactivate")]
        public async Task<ActionResult> DeactivateContract(int ConID)
        {
            await TitanAPI.DeacivateContractAsync(ConID);

            return RedirectToAction(nameof(Index));
        }

        [Route("[controller]/{ConID}/Details/Add")]
        public async Task<ActionResult> AddContractPart(int ConID)
        {
            ViewBag.ColourOptions = Colours;
            ViewBag.WarrantyOptions = Warranties;
            ViewBag.LeadTimeOptions = LeadTimes;
            ViewBag.WorkRequiredOptions = WorkRequireds;

            return View(new ContractItem
            {
                ConID = ConID
            });
        }

        [HttpPost]
        [Route("[controller]/{ConID}/Details/Add")]
        public async Task<ActionResult> AddContractPart(int ConID, ContractItem ContractPart)
        {
            await TitanAPI.AddItemToContractAsync(ConID, ContractPart);

            return RedirectToAction(nameof(EditContract), new { ConID });
        }

        [Route("[controller]/{ConID}/Details/{AltTo}/AddAlt")]
        public async Task<ActionResult> AddAlternativePart(int ConID, int AltTo)
        {
            ViewBag.ColourOptions = Colours;
            ViewBag.WarrantyOptions = Warranties;
            ViewBag.LeadTimeOptions = LeadTimes;
            ViewBag.WorkRequiredOptions = WorkRequireds;

            var Parent = await TitanAPI.GetContractItemAsync(ConID, AltTo);

            return View(new ContractItem
            {
                ConID = ConID,
                AltPart = 1,
                AltTo = AltTo,
                Parent = Parent.StockCode,

                // we don't expect to recieve alt parts
                TotalQty = 0,
                QtyRequired = 0,

                // copy details from main part
                Colour = Parent.Colour,
                DeliveryTerms = Parent.DeliveryTerms,
                DespatchMethod = Parent.DespatchMethod,
                LeadTime = Parent.LeadTime,
                Warranty = Parent.Warranty,
                WorkRequired = Parent.WorkRequired,
                UnitPrice = Parent.UnitPrice,
                IsSerialised = Parent.IsSerialised,
                SpecialInstruction = Parent.SpecialInstruction,
                QuotationNumber = Parent.QuotationNumber,
            });
        }

        [HttpPost]
        [Route("[controller]/{ConID}/Details/{AltTo}/AddAlt")]
        public async Task<ActionResult> AddAlternativePart(int ConID,
            ContractItem ContractPart)
        {
            await TitanAPI.AddAltPartToContractAsync(ConID, (int)ContractPart.AltTo, ContractPart);

            return RedirectToAction(nameof(EditContract), new { ConID });
        }

        [Route("[controller]/{ConID}/Details/{ConDetID}/Edit")]
        public async Task<ActionResult> UpdateContractPart(int ConID, int ConDetID)
        {
            ViewBag.ColourOptions = Colours;
            ViewBag.WarrantyOptions = Warranties;
            ViewBag.LeadTimeOptions = LeadTimes;
            ViewBag.WorkRequiredOptions = WorkRequireds;

            var ContractPart = await TitanAPI.GetContractItemAsync(ConID, ConDetID);

            return View(ContractPart);
        }

        [HttpPost]
        [Route("[controller]/{ConID}/Details/{ConDetID}/Edit")]
        public async Task<ActionResult> UpdateContractPart(int ConID, int ConDetID,
            ContractItem ContractPart)
        {
            await TitanAPI.UpdateContractItemAsync(ConID, ConDetID, ContractPart);

            return RedirectToAction(nameof(EditContract), new { ConID });
        }

        [Route("[controller]/{ConID}/Details/{ConDetID}/Remove")]
        public async Task<ActionResult> RemoveContractPart(int ConID, int ConDetID)
        {
            ViewBag.ColourOptions = Colours;
            ViewBag.WarrantyOptions = Warranties;
            ViewBag.LeadTimeOptions = LeadTimes;
            ViewBag.WorkRequiredOptions = WorkRequireds;

            var ContractPart = await TitanAPI.GetContractItemAsync(ConID, ConDetID);

            return View(ContractPart);
        }

        [HttpPost]
        [Route("[controller]/{ConID}/Details/{ConDetID}/Remove")]
        public async Task<ActionResult> ConfirmRemoveContractPart(int ConID, int ConDetID)
        {
            await TitanAPI.RemoveItemFromContractAsync(ConID, ConDetID);

            return RedirectToAction(nameof(EditContract), new { ConID });
        }

        [Route("[controller]/{ConID}/Documents/Add")]
        public async Task<ActionResult> AddDocument(int ConID)
        {
            return View(ConID);
        }

        [HttpPost]
        [Route("[controller]/{ConID}/Documents/Add")]
        public async Task<ActionResult> AddDocument(int ConID, [FromForm] IFormFile Document)
        {
            if (Document == null)
            {
                throw new System.ArgumentNullException(nameof(Document), "Please select a file");
            }

            await TitanAPI.AddDocumentAsync(ConID,
                new FileParameter(
                    Document.OpenReadStream(),
                    Document.FileName,
                    Document.ContentType
                    )
                );

            return RedirectToAction(nameof(EditContract), new { ConID });
        }

        [HttpPost]
        [Route("[controller]/BalanceFromSORs")]
        public async Task<ActionResult> BalanceFromSORs()
        {
            await TitanAPI.BalanceContractsFromSORsAsync();

            return RedirectToAction(nameof(Index));
        }

        [Route("[controller]/Customers")]
        public async Task<ActionResult> ListCustomers()
        {
            var Contracts = await TitanAPI.ListOpenContractsAsync();

            return View(Contracts);
        }
    }
}
