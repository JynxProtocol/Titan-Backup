//Not Yet Implmented


//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Net;
//using System.Security.Cryptography;
//using System.Threading.Tasks;
//using Titan.Client.Functions;
//using TitanAPIConnection;

//namespace Titan.Client.Controllers
//{
//    public class EngineeringInstructionsController : Controller
//    {
//        internal TitanAPI TitanAPI { get; private set; }

//        public EngineeringInstructionsController(TitanAPI titanAPI)
//        {
//            TitanAPI = titanAPI;
//        }

//        public ActionResult Index()
//        {
//            return View(TitanAPI.ListEIsAsync().Result);
//        }


//        [HttpPost]
//        public ActionResult AutoComplete(string EISearch)
//        {
//            var Results = TitanAPI.AutoCompleteEIAsync(EISearch).Result;

//            return Json(Results);
//        }



//        [HttpPost]
//        public async Task<ActionResult> Create(string EIRef)
//        {
//            await TitanAPI.CreateEIAsync(EIRef);

//            return RedirectToAction(nameof(Index));
//        }

//        public ActionResult EditSubInstructions(string EIRef)
//        {
//            return View(TitanAPI.GetEIAsync(EIRef).Result);
//        }



//    }
//}