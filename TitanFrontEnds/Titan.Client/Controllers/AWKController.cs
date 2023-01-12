using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Titan.Client.Functions;
using TitanAPIConnection;

namespace Titan.Client.Controllers
{
    public class AWKController : Controller
    {
        internal TitanAPI TitanAPI { get; private set; }
        public IEnumerable<SelectListItem> Faults { get; private set; }
        public IEnumerable<SelectListItem> WorkRequireds { get; private set; }
        public IEnumerable<SelectListItem> SalesTypes { get; private set; }

        public AWKController(TitanAPI titanAPI)
        {
            TitanAPI = titanAPI;
            Faults = TitanAPI.ListAWKFaultsAsync().Result.ToDropDown();
            WorkRequireds = TitanAPI.ListAWKWorkRequiredsAsync().Result.ToDropDown();
            SalesTypes = TitanAPI.GetSalesTypeListAsync().Result.ToDropDown();
        }

        [Route("[controller]/Closed")]
        public async Task<ActionResult> Closed()
        {
            var awks = await TitanAPI.ListClosedAWKAsync();

            return View(awks);
        }

        [Route("[controller]/ToApprove")]
        public async Task<ActionResult> ToApprove()
        {
            var AWKsToApprove = await TitanAPI.ListAWKToApproveAsync();

            return View(AWKsToApprove);
        }

        [Route("[controller]/ToQuote")]
        public async Task<ActionResult> ToQuote()
        {
            var AWKsToQuote = await TitanAPI.ListAWKToQuoteAsync();

            return View(AWKsToQuote);
        }

        [Route("[controller]/ToAuthorise")]
        public async Task<ActionResult> ToAuthorise()
        {
            var AWKsToAuthorise = await TitanAPI.ListAWKToAuthoriseAsync();

            return View(AWKsToAuthorise);
        }

        [Route("[controller]/RaiseAWKOnWO")]
        public IActionResult RaiseAWKOnWO()
        {
            return View();
        }

        [HttpPost]
        [Route("[controller]/RaiseAWKOnWO")]
        public async Task<ActionResult> Raise(string WO)
        {
            var WOInfo = await TitanAPI.GetWOInfoAsync(WO);

            ViewBag.Faults = Faults;
            return View(WOInfo);
        }

        [HttpPost]
        [Route("[controller]/RaiseAWKOnWO/{WO}")]
        public async Task<ActionResult> Raise(string WO, List<PartsListItem> PartsListItems)
        {
            await TitanAPI.RaiseAWKAsync(WO, PartsListItems);

            return RedirectToAction(nameof(ToApprove));
        }

        [Route("[controller]/{AWN}/Approve")]
        public async Task<ActionResult> Approve(int AWN)
        {
            var AWK = await TitanAPI.GetAWKToApproveAsync(AWN);

            return View(AWK);
        }

        [HttpPost]
        [Route("[controller]/{AWN}/Approve")]
        public async Task<ActionResult> Approve(int AWN, int NumberOfConsolidatedAffectedItems)
        {
            if (!ModelState.IsValid)
            {
                var AWK = await TitanAPI.GetAWKToApproveAsync(AWN);

                AWK.NumberOfConsolidatedAffectedItems = NumberOfConsolidatedAffectedItems;

                return View(AWK);
            }

            await TitanAPI.ApproveAWKAsync(AWN, NumberOfConsolidatedAffectedItems);

            return RedirectToAction(nameof(ToQuote));
        }

        [Route("[controller]/{AWN}/Quote")]
        public async Task<ActionResult> Quote(int AWN)
        {
            var AWK = await TitanAPI.GetAWKToQuoteAsync(AWN);
            ViewBag.SalesTypes = SalesTypes;

            return View(AWK);
        }

        [HttpPost]
        [Route("[controller]/{AWN}/Quote")]
        public async Task<ActionResult> Quote(int AWN, AWHeader AWKToQuote)
        {
            await TitanAPI.QuoteAWKAsync(AWN, AWKToQuote);

            return RedirectToAction(nameof(ToAuthorise));
        }

        [HttpPost]
        [Route("[controller]/{AWN}/Authorise")]
        public async Task<ActionResult> Authorise(int AWN)
        {
            await TitanAPI.AWKAuthoriseAsync(AWN);

            return RedirectToAction(nameof(ToAuthorise));
        }

        [HttpPost]
        [Route("[controller]/{AWN}/Cancel")]
        public async Task<ActionResult> Cancel(int AWN)
        {
            await TitanAPI.CancelAWKAsync(AWN);

            return RedirectToAction(nameof(ToQuote));
        }

        [HttpPost]
        [Route("[controller]/{AWN}/RevertToQuote")]
        public async Task<ActionResult> RevertToQuote(int AWN)
        {
            await TitanAPI.RevertToQuoteAsync(AWN);

            return RedirectToAction(nameof(ToQuote));
        }

        [Route("[controller]/{AWN}/View")]
        public async Task<ActionResult> ViewClosed(int AWN)
        {
            var AWK = await TitanAPI.GetClosedAWKAsync(AWN);

            ViewBag.SalesTypes = SalesTypes;

            return View(AWK);
        }

        [Route("[controller]/{AWN}/Images/Add")]
        public async Task<ActionResult> AddImage(int AWN)
        {
            return View(AWN);
        }

        [HttpPost]
        [Route("[controller]/{AWN}/Images/Add")]
        public async Task<ActionResult> AddImage(int AWN, [FromForm] IFormFile Image)
        {
            await TitanAPI.AddAWKImageAsync(AWN,
                new FileParameter(
                    Image.OpenReadStream(),
                    Image.FileName,
                    Image.ContentType
                ));

            return RedirectToAction(nameof(Approve), new { AWN });
        }

        [Route("[controller]/{AWN}/Images/Add/Camera")]
        public async Task<ActionResult> Camera(int AWN)
        {
            return View(AWN);
        }

        [HttpPost]
        [Route("[controller]/{AWN}/Images/Add/Camera")]
        public async Task<ActionResult> Camera(int AWN, string Image)
        {
            var ImageBase64 = Image.Split(',')[1];
            byte[] ImageBytes = Convert.FromBase64String(ImageBase64);

            await TitanAPI.AddAWKImageAsync(AWN,
                new FileParameter(
                    new MemoryStream(ImageBytes),
                    $"AWK_{AWN}_{DateTime.Now}.png",
                    "image/png"
                ));

            return RedirectToAction(nameof(Approve), new { AWN });
        }

        [Route("[controller]/{AWN}/Images/{ImageID}")]
        public async Task<ActionResult> Image(int AWN, int ImageID)
        {
            var Image = await TitanAPI.GetAWKImageAsync(ImageID);

            return File(Image, "image/png");
        }

        [Route("[controller]/{AWN}/Details/{AWDID}/Edit")]
        public async Task<ActionResult> EditDetail(int AWN, int AWDID, string ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;

            ViewBag.Faults = Faults;
            ViewBag.WorkRequireds = WorkRequireds;

            var AWKDetail = await TitanAPI.GetAWKDetailAsync(AWDID);

            return View(AWKDetail);
        }

        [HttpPost]
        [Route("[controller]/{AWN}/Details/{AWDID}/Edit")]
        public async Task<ActionResult> EditDetail(int AWN, AWDetail AWKDetail, string ReturnUrl)
        {
            await TitanAPI.ModifyAWKDetailAsync(AWKDetail);

            return this.RedirectToLocal(ReturnUrl);
        }


        public async Task<ActionResult> FaultsList()
        {
            var Faults = await TitanAPI.ListAWKFaultsAsync();

            return View(Faults);
        }


        public async Task<ActionResult> EditFault(int Id)
        {

            var Fault = await TitanAPI.GetAWKFaultAsync(Id);

            return View(Fault);
        }

        [HttpPost]
        public async Task<ActionResult> EditFault(int Id, string LookupText)
        {
            var Fault = await TitanAPI.EditAWKFaultAsync(Id, LookupText);

            return RedirectToAction(nameof(FaultsList));
        }



        public async Task<ActionResult> AddFault()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddFault(string LookupText)
        {
            await TitanAPI.AddAWKFaultAsync(LookupText);

            return RedirectToAction(nameof(FaultsList));
        }


        [HttpPost]
        public async Task<ActionResult> DeleteFault(int Id)
        {
            await TitanAPI.DeleteAWKFaultAsync(Id);

            return RedirectToAction(nameof(FaultsList));
        }

        public async Task<ActionResult> AWKWorkRequiredList()
        {
            var Work = await TitanAPI.ListAWKWorkRequiredsAsync();

            return View(Work);
        }


        public async Task<ActionResult> EditAWKWorkRequired(int Id)
        {

            var Work = await TitanAPI.GetAWKWorkRequiredAsync(Id);

            return View(Work);
        }

        [HttpPost]
        public async Task<ActionResult> EditAWKWorkRequired(int Id, string LookupText)
        {
            var Work = await TitanAPI.EditAWKWorkRequiredAsync(Id, LookupText);

            return RedirectToAction(nameof(AWKWorkRequiredList));
        }



        public async Task<ActionResult> AddAWKWorkRequired()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddAWKWorkRequired(string LookupText)
        {
            await TitanAPI.AddAWKWorkRequiredAsync(LookupText);

            return RedirectToAction(nameof(AWKWorkRequiredList));
        }


        [HttpPost]
        public async Task<ActionResult> DeleteAWKWorkRequired(int Id)
        {
            await TitanAPI.DeleteAWKWorkRequiredAsync(Id);

            return RedirectToAction(nameof(AWKWorkRequiredList));
        }

    }
}
