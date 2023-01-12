using Ganss.Xss;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Titan.Client.Functions;
using TitanAPIConnection;

namespace Titan.Client.Controllers
{
    public class StockTakeController : Controller
    {
        internal TitanAPI TitanAPI { get; private set; }

        public StockTakeController(TitanAPI titanAPI)
        {
            TitanAPI = titanAPI;
        }

        public async Task<ActionResult> Details(int id)
        {
            var StockTakeDetails = await TitanAPI.GetStocktakeAsync(id);

            return View(StockTakeDetails);
        }

        public async Task<ActionResult> Create(string Warehouse = "MAIN")
        {
            var NewStockTakeID = await TitanAPI.CreateStocktakeAsync(Warehouse);

            return RedirectToAction(nameof(StockTakeController.SelectItems),
                new { id = NewStockTakeID });
        }

        public async Task<ActionResult> SelectItems(PagedListParameters pagedListParameters,
            int id, string Warehouse = "MAIN")
        {
            var ItemList = await TitanAPI.GetWarehouseItemsAsync(Warehouse, pagedListParameters);

            var WarehouseOptions = await TitanAPI.ListWarehousesAsync();

            ViewBag.StockTakeID = id;

            var StockTakeItems = await TitanAPI.StocktakeSelectedItemsAsync(id);

            ViewBag.SelectedWarehouse = Warehouse;
            ViewBag.Warehouses = WarehouseOptions;
            ViewBag.SelectedItems = StockTakeItems.Select(StockItem => StockItem.Code);

            foreach (string Code in StockTakeItems.Select(StockItem => StockItem.Code))
            {
                if (ItemList.Items.Find(StockItem => StockItem.Code == Code) != null)
                {
                    ItemList.Items.Find(StockItem => StockItem.Code == Code).Selected = true;
                }
            }

            return View(ItemList);
        }

        [HttpPost]
        public async Task<ActionResult> Add(string Code, PagedListParameters pagedListParameters,
            int id, string Warehouse = "MAIN")
        {
            await TitanAPI.AddToStockTakeAsync(Code, id);

            return RedirectToAction(nameof(SelectItems),
                new
                {
                    pagedListParameters.CurrentPage,
                    pagedListParameters.PageSize,
                    pagedListParameters.SearchTerm,
                    Warehouse,
                    id
                });
        }

        [HttpPost]
        public async Task<ActionResult> Remove(string Code, PagedListParameters pagedListParameters,
            int id, string Warehouse = "MAIN")
        {
            await TitanAPI.RemoveFromStockTakeAsync(Code, id);

            return RedirectToAction(nameof(SelectItems),
                new
                {
                    pagedListParameters.CurrentPage,
                    pagedListParameters.PageSize,
                    pagedListParameters.SearchTerm,
                    Warehouse,
                    id
                });
        }

        [HttpGet]
        public async Task<ActionResult> Confirm(string Warehouse, int id)
        {
            var StockTakeItems = await TitanAPI.StocktakeSelectedItemsAsync(id);

            ViewBag.StockTakeID = id;

            ViewBag.StockTakeItems = StockTakeItems;

            var DateTimeNow = DateTime.Now;
            var StockTakeName =
                $"Cycle check {DateTimeNow.ToString("d")} {DateTimeNow.ToString("t")}";

            SubmitStockTakeDTO createStockTakeDTO = new SubmitStockTakeDTO()
            {
                Id = id,
                Warehouse = Warehouse,
                Name = StockTakeName
            };

            return View(createStockTakeDTO);
        }

        [HttpPost]
        public async Task<ActionResult> Submit(SubmitStockTakeDTO submitStockTakeDTO)
        {
            await TitanAPI.SubmitStockTakeAsync(submitStockTakeDTO);

            return RedirectToAction(nameof(StockTakeController.List));
        }

        [HttpPost]
        public async Task<ActionResult> Reset(PagedListParameters pagedListParameters,
            int id, string Warehouse = "MAIN")
        {
            await TitanAPI.ResetStockTakeAsync(Warehouse, id);

            return RedirectToAction(nameof(SelectItems),
                new
                {
                    CurrentPage = 1,
                    pagedListParameters.PageSize,
                    pagedListParameters.SearchTerm,
                    Warehouse,
                    id
                });
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            await TitanAPI.DeleteStockTakeAsync(id);

            return RedirectToAction(nameof(StockTakeController.DeletedList));
        }

        [HttpGet]
        public async Task<ActionResult> List()
        {
            var StockTakes = await TitanAPI.ListStockTakesAsync();

            return View(StockTakes);
        }

        [HttpGet]
        public async Task<ActionResult> CompletedList()
        {
            var StockTakes = await TitanAPI.ListStockTakesAsync();

            return View(StockTakes);
        }

        [HttpGet]
        public async Task<ActionResult> DeletedList()
        {
            var StockTakes = await TitanAPI.ListStockTakesAsync();

            return View(StockTakes);
        }

        private bool IsStockTakeComplete(StockTakeInfoDTO stockTakeInfoDTO)
        {
            try
            {
                return (stockTakeInfoDTO.Completion.Split('/')[0]
                            == stockTakeInfoDTO.Completion.Split('/')[1]);
            }
            catch (Exception)
            {
                return false;
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetAdjustments(int id)
        {
            var StockTakeDetails = await TitanAPI.GetStocktakeAsync(id);

            var Adjustments = await TitanAPI.GetStocktakeAdjustmentsAsync(id);
            ViewBag.StockTakeID = id;

            if (!IsStockTakeComplete(StockTakeDetails))
            {
                ModelState.AddModelError("Items",
                    "Cannot generate adjustments because not all parts have been checked");
                return View(nameof(Details), StockTakeDetails);
            }
            else
            {
                return View(Adjustments);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Adjust(int id, List<StockAdjustDTO> stockAdjusts,
            [FromServices] HtmlSanitizer htmlSanitizer)
        {
            var AdjustmentReport =
                await TitanAPI.ApplyStocktakeAdjustmentsAsync(id, stockAdjusts);

            AdjustmentReport.Report
                .ForEach(reportLine =>
                    reportLine.Message = htmlSanitizer.Decode(reportLine.Message));

            if (await User.UserHasPermission("ViewAdjustmentReport"))
            {
                return View("AdjustmentReport", AdjustmentReport);
            }
            else
            {
                return RedirectToAction(nameof(StockTakeController.CompletedList));
            }
        }
    }
}