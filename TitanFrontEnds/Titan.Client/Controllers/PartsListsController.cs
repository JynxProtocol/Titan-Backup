using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;
using Titan.Client.Functions;
using TitanAPIConnection;

namespace Titan.Client.Controllers
{
    public class PartsListsController : Controller
    {
        internal TitanAPI TitanAPI { get; private set; }
        public IEnumerable<SelectListItem> ProductGroups { get; private set; }

        public PartsListsController(TitanAPI titanAPI)
        {
            TitanAPI = titanAPI;
            ProductGroups = TitanAPI.GetProductGroupsAsync().Result
                .ToDropDown();
        }

        public async Task<ActionResult> Index()
        {
            var partslist = await TitanAPI.ListPartsListsAsync();

            return View(partslist);
        }

        [Route("[controller]/Add")]
        public async Task<ActionResult> AddPartsList()
        {
            ViewBag.ProductGroupOptions = ProductGroups;

            return View();
        }

        [HttpPost]
        [Route("[controller]/Add")]
        public async Task<ActionResult> AddPartsList(string Title, string ProductGroup)
        {
            var PLHID = await TitanAPI.AddPartsListAsync(Title, ProductGroup);

            return RedirectToAction(nameof(UpdatePartsList), new { PLHID });
        }

        [HttpPost]
        [Route("[controller]/{PLHID}/Delete")]
        public async Task<ActionResult> DeletePartsList(int PLHID)
        {
            await TitanAPI.DeletePartsListAsync(PLHID);

            return RedirectToAction(nameof(Index));
        }

        [Route("[controller]/{PLHID}/Update")]
        public async Task<ActionResult> UpdatePartsList(int PLHID)
        {
            ViewBag.ProductGroupOptions = ProductGroups;

            var partslist = await TitanAPI.GetPartsListAsync(PLHID);

            return View(partslist);
        }

        [HttpPost]
        [Route("[controller]/{PLHID}/Update")]
        public async Task<ActionResult> UpdatePartsList(int PLHID, string Title,
            string ProductGroup)
        {
            await TitanAPI.UpdatePartsListAsync(PLHID, Title, ProductGroup);

            return RedirectToAction(nameof(Index));
        }

        [Route("[controller]/{PLHID}/Parts/{PLDID}/Edit")]
        public async Task<ActionResult> UpdatePart(int PLHID, int PLDID)
        {
            var part = await TitanAPI.GetPartsListPartAsync(PLHID, PLDID);

            return View(part);
        }

        [HttpPost]
        [Route("[controller]/{PLHID}/Parts/{PLDID}/Edit")]
        public async Task<ActionResult> UpdatePart(int PLHID, int PLDID,
            PartsListItem PartsListItem)
        {
            await TitanAPI.UpdatePartsListPartAsync(PLHID, PLDID, PartsListItem);

            return RedirectToAction(nameof(UpdatePartsList), new { PLHID });
        }

        [Route("[controller]/{PLHID}/Parts/Add")]
        public async Task<ActionResult> AddPart(int PLHID)
        {
            return View(new PartsListItem
            {
                Plhid = PLHID
            });
        }

        [HttpPost]
        [Route("[controller]/{PLHID}/Parts/Add")]
        public async Task<ActionResult> AddPart(int PLHID, PartsListItem PartsListItem)
        {
            await TitanAPI.AddPartToPartsListAsync(PLHID, PartsListItem);

            return RedirectToAction(nameof(UpdatePartsList), new { PLHID });
        }

        [HttpPost]
        [Route("[controller]/{PLHID}/Parts/{PLDID}/Remove")]
        public async Task<ActionResult> DeletePart(int PLHID, int PLDID)
        {
            await TitanAPI.RemovePartFromPartsListAsync(PLHID, PLDID);

            return RedirectToAction(nameof(UpdatePartsList), new { PLHID });
        }

        [HttpPost]
        [Route("[controller]/{FPLHID}/Clone")]
        public async Task<ActionResult> ClonePartsList(int FPLHID)
        {
            var PLHID = await TitanAPI.ClonePartsListAsync(FPLHID);

            return RedirectToAction(nameof(UpdatePartsList), new { PLHID });
        }

        [Route("[controller]/{PLHID}/Cats/Add")]
        public async Task<ActionResult> AddCat(int PLHID)
        {
            return View(new PartsListCatItem
            {
                Plhid = PLHID
            });
        }

        [HttpPost]
        [Route("[controller]/{PLHID}/Cats/Add")]
        public async Task<ActionResult> AddCat(int PLHID, string CatNumber, string Description)
        {
            await TitanAPI.AddCatToPartsListAsync(PLHID, CatNumber, Description);

            return RedirectToAction(nameof(UpdatePartsList), new { PLHID });
        }

        [Route("[controller]/{PLHID}/Cats/{CATID}/Edit")]
        public async Task<ActionResult> EditCat(int PLHID, int CATID)
        {
            var Cat = await TitanAPI.GetCatAsync(PLHID, CATID);

            return View(Cat);
        }

        [HttpPost]
        [Route("[controller]/{PLHID}/Cats/{CATID}/Edit")]
        public async Task<ActionResult> EditCat(int PLHID, int CATID, PartsListCatItem partsListCat)
        {
            await TitanAPI.UpdateCatAsync(PLHID, CATID, partsListCat);

            return RedirectToAction(nameof(UpdatePartsList), new { PLHID });
        }

        [HttpPost]
        [Route("[controller]/{PLHID}/Cats/{CATID}/Remove")]
        public async Task<ActionResult> RemoveCat(int PLHID, int CATID)
        {
            await TitanAPI.RemoveCatAsync(PLHID, CATID);

            return RedirectToAction(nameof(UpdatePartsList), new { PLHID });
        }
    }
}
