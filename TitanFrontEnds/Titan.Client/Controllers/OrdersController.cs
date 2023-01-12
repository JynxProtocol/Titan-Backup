using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;
using Titan.Client.Functions;
using TitanAPIConnection;

namespace Titan.Client.Controllers
{
    public class OrdersController : Controller
    {
        internal TitanAPI TitanAPI { get; private set; }
        public IEnumerable<SelectListItem> Customers { get; private set; }
        public IEnumerable<SelectListItem> Colours { get; private set; }
        public IEnumerable<SelectListItem> Warranties { get; private set; }
        public IEnumerable<SelectListItem> LeadTimes { get; private set; }
        public IEnumerable<SelectListItem> DeliveryTerms { get; private set; }
        public IEnumerable<SelectListItem> DespatchMethods { get; private set; }

        public OrdersController(TitanAPI titanAPI)
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
            DeliveryTerms = TitanAPI.GetDeliveryTermsListAsync().Result
                .ToDropDown();
            DespatchMethods = TitanAPI.GetDespatchMethodListAsync().Result
                .ToDropDown();
        }

        public async Task<ActionResult> Index()
        {
            var Orders = await TitanAPI.ListOrdersToApproveAsync();

            return View(Orders);
        }

        public async Task<ActionResult> Complete()
        {
            var Orders = await TitanAPI.ListCompletedOrdersAsync();

            return View(Orders);
        }

        [Route("[controller]/{OrderID}/View")]
        public async Task<ActionResult> ViewOrder(int OrderID)
        {
            ViewBag.CustomerOptions = Customers;
            ViewBag.DeliveryTermsOptions = DeliveryTerms;
            ViewBag.DespatchMethodOptions = DespatchMethods;

            var Order = await TitanAPI.GetOrderByIDAsync(OrderID);

            return View(Order);
        }

        [Route("[controller]/BookInParts/Contracts/{ConID}")]
        public async Task<ActionResult> BookInParts(int ConID)
        {
            var Parts = await TitanAPI.GetOrderPartsSuggestionsAsync(ConID);

            ViewBag.ConID = ConID;

            return View(Parts);
        }

        [HttpPost]
        [Route("[controller]/BookInParts/Contracts/{ConID}")]
        public async Task<ActionResult> BookInParts(int ConID, string DeliveryRef, string GRN,
            List<ReceivedPart> Parts)
        {
            var Order = await TitanAPI.BookInPartsAsync(ConID, DeliveryRef, GRN, Parts);

            return RedirectToAction(nameof(ViewOrder), new { Order.OrderID });
        }

        [Route("[controller]/{OrderID}/GenerateSOR")]
        public async Task<ActionResult> GenerateSOR(int OrderID)
        {
            var Order = await TitanAPI.GetOrderByIDAsync(OrderID);

            ViewBag.CustomerOptions = Customers;
            ViewBag.DeliveryTermsOptions = DeliveryTerms;
            ViewBag.DespatchMethodOptions = DespatchMethods;

            return View(Order);
        }

        [HttpPost]
        [Route("[controller]/{OrderID}/GenerateSOR")]
        public async Task<ActionResult> ConfirmGenerateSOR(int OrderID)
        {
            await TitanAPI.ApproveAndGenerateSORForOrderAsync(OrderID);

            return RedirectToAction(nameof(ViewOrder), new { OrderID });
        }

        [Route("[controller]/{OrderID}/Edit")]
        public async Task<ActionResult> Edit(int OrderID)
        {
            var Order = await TitanAPI.GetOrderByIDAsync(OrderID);

            ViewBag.CustomerOptions = Customers;
            ViewBag.DeliveryTermsOptions = DeliveryTerms;
            ViewBag.DespatchMethodOptions = DespatchMethods;
            ViewBag.ColourOptions = Colours;
            ViewBag.WarrantyOptions = Warranties;
            ViewBag.LeadTimeOptions = LeadTimes;

            return View(Order);
        }

        [HttpPost]
        [Route("[controller]/{OrderID}/Edit")]
        public async Task<ActionResult> Edit(int OrderID, OrderDTO Order)
        {
            await TitanAPI.UpdateOrderAsync(OrderID, Order);

            return RedirectToAction(nameof(ViewOrder), new { OrderID });
            //return RedirectToAction(nameof(Index));
        }
    }
}
