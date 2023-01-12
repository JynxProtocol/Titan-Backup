using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using ReportEngineConnection;
using SageAPIConnection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Titan.API.Functions;
using Titan.API.Models;
using Titan.API.Models.AWK;
using Titan.Filters;
using Titan.Functions;

namespace Titan.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class AWKController : Controller
    {
        internal TitanContext Titan { get; private set; }
        internal SAGEContext Sage { get; private set; }
        internal ReportEngine ReportEngine { get; private set; }
        internal SmtpClient SmtpClient { get; private set; }
        internal SageAPI SageAPI { get; private set; }
        internal ILogger<AWKController> logger { get; private set; }

        internal DateTime Now = DateTime.Now;

        public AWKController(TitanContext titan, SAGEContext sage, ReportEngine reportEngine,
            SmtpClient smtpClient, SageAPI sageAPI, ILogger<AWKController> _logger)
        {
            Titan = titan;
            Sage = sage;
            ReportEngine = reportEngine;
            SmtpClient = smtpClient;
            SageAPI = sageAPI;
            logger = _logger;
        }

        /// <response code="200">Returns information about a WO and its parts</response>
        /// <response code="400">Invalid WO number</response>
        /// <response code="404">Could not find BOM reference or linked WO info</response>
        /// <response code="422">Could not find parts list for CatNumber</response>
        [HttpGet]
        [Route("GetWOInfo")]
        public ActionResult<WOInformation> GetWOInfo(string WONumber)
        {
            if (TryGetValidWO(WONumber, out SiWorksOrder WorksOrder))
            {
                if (TryGetWOInfo(WorksOrder, out StockItem WorksOrderReturnItem,
                    out SOPOrderReturn SalesOrder, out SLCustomerAccount Customer))
                {
                    var WOInformation = new WOInformation
                    {
                        WorksOrderNumber = WorksOrder.WONumber,
                        BomReference = WorksOrderReturnItem.Code,
                        BomDescription = WorksOrderReturnItem.Name,
                        QtyRequired = WorksOrder.WOQuantity ?? -1,
                        LinkedTo = WorksOrder.WONumber.Split("-").FirstOrDefault(),
                        CusOrderNumber = SalesOrder.CustomerDocumentNo,
                        AccountName = Customer.CustomerAccountName,
                        AccountNumber = Customer.CustomerAccountNumber
                    };

                    //Get the PLHID for the cat Number (BomReference)
                    var PartsListCat = Titan.PartsListCats
                        .Where(con => con.CatNumber == WOInformation.BomReference)
                        .FirstOrDefault();

                    if (PartsListCat == null)
                    {
                        return UnprocessableEntity();
                    }

                    var PartsListForWO = Titan.PartsListHeaders
                        .Where(con => con.PLHID == PartsListCat.PLHID)
                        .FirstOrDefault();

                    if (PartsListForWO == null)
                    {
                        return UnprocessableEntity();
                    }

                    string ProductGroup = PartsListForWO.ProductGroup;

                    List<PartsListItem> WOItems = Titan.PartsListDetails
                        .Where(part => part.PLHID == PartsListForWO.PLHID)
                        .Where(part => part.Mandatory != true)
                        .Select(det => new PartsListItem
                        {
                            PLDID = det.PLDID,
                            SageStkID = det.SageStkID,
                            PartNumber = det.PartNumber,
                            Description = det.Description,
                            ProductGroup = ProductGroup
                        }).ToList();

                    WOInformation.PartsListItems = WOItems;

                    return WOInformation;
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return BadRequest();
            }
        }

        /// <response code="200">Succesfully raised AWK</response>
        /// <response code="400">Invalid WO number</response>
        /// <response code="404">Could not find BOM reference or linked WO info</response>
        [HttpPost]
        [Route("{WONumber}/RaiseAWK")]
        [Feature("RaiseAWK")]
        public ActionResult RaiseAWK(string WONumber, List<PartsListItem> Parts)
        {
            if (!TryGetValidWO(WONumber, out SiWorksOrder WorksOrder))
            {
                return BadRequest();
            }

            if (!TryGetWOInfo(WorksOrder, out StockItem WorksOrderReturnItem,
                out SOPOrderReturn SalesOrder, out SLCustomerAccount Customer))
            {
                return NotFound();
            }

            var WOInformation = new WOInformation
            {
                WorksOrderNumber = WorksOrder.WONumber,
                BomReference = WorksOrderReturnItem.Code,
                BomDescription = WorksOrderReturnItem.Name,
                QtyRequired = WorksOrder.WOQuantity ?? -1,
                LinkedTo = SORFromWONumber(WorksOrder.WONumber),
                CusOrderNumber = SalesOrder.CustomerDocumentNo,
                AccountName = Customer.CustomerAccountName,
                AccountNumber = Customer.CustomerAccountNumber
            };

            var contact = Sage.Titan_SLAWKContacts_vws
                .Where(acn => acn.CustomerAccountNumber == WOInformation.AccountNumber)
                .FirstOrDefault();

            string awkContactName = "No Contact";
            string awkContactValue = "No Contact";

            if (contact != null)
            {
                awkContactName = contact.ContactName;
                awkContactValue = contact.ContactValue;
            }

            var header = new AWKHeader()
            {
                WorksOrderNumber = WOInformation.WorksOrderNumber,
                AccountName = WOInformation.AccountName,
                AccountNumber = WOInformation.AccountNumber,
                CustOrderNumber = WOInformation.CusOrderNumber,
                CatNo = WOInformation.BomReference,
                Description = WOInformation.BomDescription,
                SOR = WOInformation.LinkedTo,
                Qty = WOInformation.QtyRequired,
                ContractName = awkContactName,
                EMail = awkContactValue,
                SendMail = true,
                Approved = false,
            };

            Titan.AWKHeaders.Add(header);
            Titan.SaveChanges();

            WorksOrder.SpareText5 = "AWK1"; //Additional Work Rasied
            Sage.SaveChanges();

            foreach (var AWKPart in Parts)
            {
                Titan.AWKDetails.Add(new AWKDetail
                {
                    DID = header.ID,
                    StkID = AWKPart.SageStkID,
                    StockCode = AWKPart.PartNumber,
                    Description = AWKPart.Description,
                    RequiredQty = (int?)AWKPart.Qty,
                    Fault = AWKPart.AWKFault,
                    ProductGroup = AWKPart.ProductGroup,

                    SabreStock = (AWKPart.PartNumber == "ZZZ")
                });

                if (AWKPart.Qty > 0)
                {
                    WorksOrder.WOComments = $"AWK on {AWKPart.PartNumber} ({AWKPart.Description})" +
                        $", Qty: {AWKPart.Qty}\n" + WorksOrder.WOComments;
                }
            }

            Sage.SaveChanges();
            Titan.SaveChanges();

            return Ok();
        }

        private string SORFromWONumber(string WONumber)
        {
            return WONumber.Split("-").FirstOrDefault().PadLeft(10, '0');
        }

        /// <response code="200">Returns the list of AWK to approve</response>
        [HttpGet]
        [Route("ToApprove")]
        public List<AWKHeader> ListAWKToApprove()
        {
            logger.LogInformation("Getting AWK to Approve List");

            return Titan.AWKHeaders
                .Where(AWK => AWK.Canceled != true)
                .Where(AWK => AWK.Approved != true)
                .ToList();
        }

        /// <response code="200">Returns the list of AWK to quote</response>
        [HttpGet]
        [Route("ToQuote")]
        public List<AWKHeader> ListAWKToQuote()
        {
            return Titan.AWKHeaders
                .Where(AWK => AWK.Canceled != true)
                .Where(AWK => AWK.Approved == true)
                .Where(AWK => AWK.AWKQuoted != true)
                .ToList();
        }

        /// <response code="200">Returns the list of AWK to authorise</response>
        [HttpGet]
        [Route("ToAuthorise")]
        public List<AWKHeader> ListAWKToAuthorise()
        {
            return Titan.AWKHeaders
                .Where(AWK => AWK.Canceled != true)
                .Where(AWK => AWK.AWKQuoted == true)
                .Where(AWK => AWK.Authorised != true)
                .ToList();
        }

        /// <response code="200">Returns the list of closed AWK</response>
        [HttpGet]
        [Route("Closed")]
        public List<AWKHeader> ListClosedAWK()
        {
            return Titan.AWKHeaders
                .Where(AWK => AWK.Canceled == true || AWK.AWKQuoted == true)
                .ToList();
        }

        /// <response code="200">Returns informaton about AWK</response>
        [HttpGet]
        [Route("{AWN}/Approve")]
        public ActionResult<AWHeader> GetAWKToApprove(int AWN)
        {
            AWHeader myAWK = new();

            myAWK = Titan.AWKHeaders.Where(AWK => AWK.ID == AWN)
                .Select(AWKHeader => new AWHeader
                {
                    ID = AWKHeader.ID,
                    WorksOrderNumber = AWKHeader.WorksOrderNumber,
                    BomReference = AWKHeader.CatNo,
                    BomDescription = AWKHeader.Description,
                    QtyRequired = (int)AWKHeader.Qty,
                    LinkedTo = AWKHeader.SOR,
                    CusOrderNumber = AWKHeader.CustOrderNumber,
                    AccountName = AWKHeader.AccountName
                }).Single();


            bool showAllitems = true;

            if (showAllitems == true)
            {
                List<AWDetail> myitems = (from awd in Titan.vw_AWKStockRecords
                                          where awd.DID == myAWK.ID

                                          select new AWDetail
                                          {
                                              AWDID = awd.AWDID,
                                              ID = awd.DID ?? 0,
                                              StkID = awd.StkID ?? 0,
                                              StockCode = awd.StockCode,
                                              Description = awd.Description,
                                              RequiredQty = awd.RequiredQty ?? 0,
                                              SabreStock = awd.SabreStock ?? false,
                                              Fault = awd.Fault,
                                              WorkRequired = awd.WorkRequired,
                                              RepairDetail = awd.RepairDetail,
                                              UnitPrice = awd.Price ?? 0,
                                              FreeStock = awd.FreeStock ?? 0,
                                              RepairCost = awd.RepairCost ?? 0,
                                              Linetotal = awd.RequiredQty * awd.RepairCost ?? 0,


                                          }).ToList();


                myAWK.AWDetails = myitems.ToList();
            }
            else
            {
                List<AWDetail> myitems = (from awd in Titan.vw_AWKStockRecords
                                          where awd.DID == myAWK.ID && awd.RequiredQty > 0

                                          select new AWDetail
                                          {
                                              AWDID = awd.AWDID,
                                              ID = awd.DID ?? 0,
                                              StkID = awd.StkID ?? 0,
                                              StockCode = awd.StockCode,
                                              Description = awd.Description,
                                              RequiredQty = awd.RequiredQty ?? 0,
                                              SabreStock = awd.SabreStock ?? false,
                                              Fault = awd.Fault,
                                              WorkRequired = awd.WorkRequired,
                                              RepairDetail = awd.RepairDetail,
                                              UnitPrice = awd.Price ?? 0,
                                              FreeStock = awd.FreeStock ?? 0,
                                              RepairCost = awd.RepairCost ?? 0,
                                              Linetotal = awd.RequiredQty * awd.RepairCost ?? 0
                                          }).ToList();


                myAWK.AWDetails = myitems.ToList();
            }

            List<Document> myDocs = Titan.Documents.Where(tik => tik.AWKID == AWN)
                .Select(doc => new Document
                {
                    DocID = doc.DocID,
                    DocumentName = doc.DocumentName,
                    AWKID = doc.AWKID ?? 0
                }).ToList();

            myAWK.Documents = myDocs
                .OrderByDescending(Document => Document.DocumentName)
                .ToList();

            return Ok(myAWK);
        }

        /// <response code="200">Succesfully approved AWK</response>
        /// <response code="400">Cannot approve AWK with a total quantity of zero</response>
        [HttpPost]
        [Route("{AWN}/Approve")]
        [Feature("ApproveAWK")]
        public ActionResult ApproveAWK(int AWN, [Required] int AWKQty)
        {
            var header = Titan.AWKHeaders
                .Where(awk => awk.ID == AWN)
                .FirstOrDefault();

            if (AWKQty <= 0)
            {
                return BadRequest();
            }

            header.AWKQty = AWKQty;

            header.Approved = true;
            header.AWKQuoted = false;
            header.Authorised = false;
            header.ApprovedBy = HttpContext.GetUserDisplayName();
            header.DateApproved = Now;

            var AWKTotalPrice = Titan.AWKDetails.Where(d => d.DID == header.ID)
                .Sum(detail => detail.RequiredQty * detail.RepairCost ?? 0);

            header.TotalPrice = AWKTotalPrice;

            Titan.SaveChanges();

            return Ok();
        }

        /// <response code="200">Returns the AWDetail</response>
        [HttpGet]
        [Route("Details/{AWDID}")]
        public ActionResult<AWDetail> GetAWKDetail(int AWDID)
        {
            return Titan.AWKDetails
               .Where(ord => ord.AWDID == AWDID)
               .Select(det => new AWDetail
               {
                   StockCode = det.StockCode,
                   Description = det.Description,
                   RequiredQty = (int)det.RequiredQty,
                   Fault = det.Fault,
                   WorkRequired = det.WorkRequired,
                   RepairDetail = det.RepairDetail,
                   ID = (int)det.DID,
                   RepairCost = det.RepairCost ?? 0,
                   SabreStock = det.SabreStock ?? false,
                   StkID = det.StkID ?? 0,
                   ComAccepted = det.ComAccepted ?? false,
                   DID = (int)det.DID,
                   AWDID = det.AWDID
               })
               .Single();
        }

        /// <response code="200">Succesfully updated AWK details</response>
        [HttpPost]
        [Route("Details")]
        public ActionResult ModifyAWKDetail(AWDetail myItem)
        {
            AWKDetail myLine = new();
            AWKStockHeader myStock = new();
            AWKHeader myHeader = new();

            myStock = Titan.AWKStockHeaders
                .Where(won => won.StkID == myItem.StkID)
                .FirstOrDefault();

            var did = myItem.DID;

            myHeader = Titan.AWKHeaders
                .Where(stat => stat.ID == myItem.DID)
                .FirstOrDefault();

            try
            {

                myLine = Titan.AWKDetails
                    .Where(det => det.AWDID == myItem.AWDID)
                    .Single();


                myLine.RequiredQty = myItem.RequiredQty;
                myLine.Fault = myItem.Fault;
                bool workChanged = myLine.WorkRequired != myItem.WorkRequired;
                myLine.WorkRequired = myItem.WorkRequired;
                myLine.RepairDetail = myItem.RepairDetail;

                bool Updatefromdetail = true;

                if (myItem.WorkRequired == "Replace parts")
                {
                    // if the user has changed the price, update it in the pricebook
                    // only update if feature-flag is enabled and AWK is approved
                    if (Updatefromdetail && (bool)myHeader.Approved && !workChanged)
                    {
                        myStock.Price = myItem.RepairCost;
                    }

                    // if replacing parts, use the price of the part being replaced
                    myLine.RepairCost = myStock.Price;
                }
                else
                {
                    // else if repairing parts, do not use price
                    myLine.RepairCost = myItem.RepairCost;
                }


                myLine.SabreStock = myItem.SabreStock;


                myLine.ComAccepted = myItem.ComAccepted;

                myLine.LastUpdatedBy = HttpContext.GetUserDisplayName();
                myLine.DateLastUpdated = Now;

                Titan.SaveChanges();
            }
            catch
            {
                //Catch what went wrong here
            }

            //Need to handle if updated from sales or engineering

            var AWKTotalPrice = Titan.AWKDetails.Where(d => d.DID == myHeader.ID)
                .Sum(detail => detail.RequiredQty * detail.RepairCost ?? 0);

            myHeader.TotalPrice = AWKTotalPrice;
            Titan.SaveChanges();

            return Ok();
        }

        /// <response code="200">Returns informaton about AWK</response>
        [HttpGet]
        [Route("{AWN}/Quote")]
        public ActionResult<AWHeader> GetAWKToQuote(int AWN)
        {
            AWHeader myAWK = new();

            myAWK = Titan.AWKHeaders.Where(AWK => AWK.ID == AWN)
                .Select(a => new AWHeader
                {
                    ID = a.ID,
                    WorksOrderNumber = a.WorksOrderNumber,
                    BomReference = a.CatNo,
                    BomDescription = a.Description,
                    QtyRequired = (int)a.Qty,
                    NumberOfConsolidatedAffectedItems = a.AWKQty ?? 0,
                    LinkedTo = a.SOR,
                    CusOrderNumber = a.CustOrderNumber,
                    AccountName = a.AccountName,
                    ContractName = a.ContractName,
                    EMail = a.EMail,
                    EmailMessage = a.EMailMessage,
                    AWKQuoted = (bool)a.AWKQuoted,
                    SendMail = (bool)a.SendMail,
                    AWKSalesType = a.AWKSalesType
                })
                .Single();

            bool showAllitems = false;

            if (showAllitems == true)
            {

                List<AWDetail> myitems = (from awd in Titan.vw_AWKStockRecords
                                          where awd.DID == myAWK.ID

                                          select new AWDetail
                                          {
                                              AWDID = awd.AWDID,
                                              ID = awd.DID ?? 0,
                                              StkID = awd.StkID ?? 0,
                                              StockCode = awd.StockCode,
                                              Description = awd.Description,
                                              RequiredQty = awd.RequiredQty ?? 0,
                                              Fault = awd.Fault,
                                              WorkRequired = awd.WorkRequired,
                                              RepairDetail = awd.RepairDetail,
                                              UnitPrice = awd.Price ?? 0,
                                              FreeStock = awd.FreeStock ?? 0,
                                              FreeSabreStock = awd.FreeSabreStock ?? 0,
                                              QtyonOrder = awd.QuantityOnPOPOrder,
                                              RepairCost = awd.RepairCost ?? 0,
                                              SabreStock = awd.SabreStock ?? false,
                                              ComAccepted = awd.ComAccepted ?? false

                                          }).ToList();


                myAWK.AWDetails = myitems;

            }
            else
            {
                List<AWDetail> myitems = (from awd in Titan.vw_AWKStockRecords
                                          where awd.DID == myAWK.ID && awd.RequiredQty > 0

                                          select new AWDetail
                                          {
                                              AWDID = awd.AWDID,
                                              ID = awd.DID ?? 0,
                                              StkID = awd.StkID ?? 0,
                                              StockCode = awd.StockCode,
                                              Description = awd.Description,
                                              RequiredQty = awd.RequiredQty ?? 0,
                                              Fault = awd.Fault,
                                              WorkRequired = awd.WorkRequired,
                                              RepairDetail = awd.RepairDetail,
                                              UnitPrice = awd.Price ?? 0,
                                              FreeStock = awd.FreeStock ?? 0,
                                              FreeSabreStock = awd.FreeSabreStock ?? 0,
                                              QtyonOrder = awd.QuantityOnPOPOrder,
                                              RepairCost = awd.RepairCost ?? 0,
                                              SabreStock = awd.SabreStock ?? false,
                                              ComAccepted = awd.ComAccepted ?? false

                                          }).ToList();


                myAWK.AWDetails = myitems;
            }

            return myAWK;
        }

        /// <response code="200">Succesfully quoted AWK</response>
        /// <response code="400">Invalid WO number</response>
        /// <response code="404">Could not find BOM reference or linked WO info</response>
        /// <response code="409">Cannot Quote AWK item with zero price</response>
        /// <response code="502">Could not send email</response>
        /// <response code="503">Could not generate required report</response>
        [HttpPost]
        [Route("{AWN}/Quote")]
        [Feature("QuoteAWK")]
        public async Task<ActionResult> QuoteAWK(int AWN, AWHeader myItem)
        {
            var header = Titan.AWKHeaders.Where(awk => awk.ID == AWN)
                .FirstOrDefault();

            var AWKDetails = Titan.AWKDetails.Where(det => det.DID == AWN)
                .ToList();

            var AWKTotalPrice = AWKDetails
                .Select(det => det.RequiredQty * det.RepairCost ?? 0)
                .Sum();

            // TODO: this fails for replacing parts
            // should this check QuotePrice == 0 too?
            if (AWKDetails.Where(det => det.RequiredQty > 0)
                .Any(detail => (detail.RepairCost ?? 0) == 0))
            {
                // do not allow quoting an item for zero price
                return Conflict();
            }

            header.AWKSalesType = myItem.AWKSalesType;
            Titan.SaveChanges();

            if (header.AWKSalesType == null && header.SOR != null)
            {
                await SageAPI.AddAWKLineToSalesOrderAsync(
                    myItem.LinkedTo,
                    $"AW {AWN} REF WO {myItem.WorksOrderNumber} Awaiting Customer Auth",
                    (double)AWKTotalPrice
                );
            }

            header.ContractName = myItem.ContractName;
            header.EMail = myItem.EMail;
            header.EMailMessage = myItem.EmailMessage;

            header.AWKQuoted = true;
            header.AWKQuotedBy = HttpContext.GetUserDisplayName();
            header.AWKQuotedDate = Now;
            Titan.SaveChanges();

            if (!TryGetValidWO(myItem.WorksOrderNumber, out SiWorksOrder WorksOrder))
            {
                return BadRequest();
            }

            if (!TryGetWOInfo(WorksOrder, out StockItem WorksOrderReturnItem,
                out SOPOrderReturn SalesOrder, out SLCustomerAccount Customer))
            {
                return NotFound();
            }

            WorksOrder.SpareText5 = "AWK2"; //Additional Work Rasied
            Sage.SaveChanges();

            // add awk lines to works order
            await SageAPI.AddAWKToWOAsync(AWN);

            var message = new MailMessage();
            try
            {
                // create memory stream to save PDF
                Stream pdfStream = new MemoryStream();
                string filename;

                // call report engine to generate the AKWQuote
                try
                {
                    var ReportResponse =
                        await ReportEngine.GenerateAsync("AWKQuotation", header.ID.ToString());

                    var ContentDisposition = System.Net.Http.Headers.ContentDispositionHeaderValue
                        .Parse(ReportResponse.Headers[HeaderNames.ContentDisposition].First());

                    filename = ContentDisposition.FileNameStar ?? ContentDisposition.FileName;

                    pdfStream.Seek(0, SeekOrigin.Begin);
                    await ReportResponse.Stream.CopyToAsync(pdfStream);
                    ReportResponse.Dispose();
                    pdfStream.Seek(0, SeekOrigin.Begin);
                }
                catch (Exception e)
                {
                    return StatusCode(StatusCodes.Status503ServiceUnavailable, new ErrorViewModel()
                    {
                        Message = e.Message,
                        CorrelationId = ""
                    });
                }

                List<AWKPhoto> Image = Titan.Documents.Where(id => id.AWKID == AWN)
                    .Select(id => new AWKPhoto
                    {
                        DocID = id.DocID,
                        Image = id.AWKImage,
                        DocumentName = id.DocumentName,
                        AWKID = id.AWKID ?? 0,
                        FileType = id.FileType
                    })
                    .ToList();

                foreach (var x in Image)
                {
                    string source = x.Image;
                    string base64 = source.Substring(source.IndexOf(',') + 1);
                    byte[] data = Convert.FromBase64String(base64);

                    message.Attachments.Add(new Attachment(
                            new MemoryStream(data), "AWK-" + x.AWKID + x.FileType)
                        );
                }

                if (myItem.SendMail == true)
                {
                    if (Titan.Settings.Single().AWKEmailDirect ?? false)
                    {
                        message.To.Add(new MailAddress(myItem.EMail));
                        message.Bcc.Add("customerservices@sabrerail.com");
                    }
                    else
                    {
                        message.To.Add(new MailAddress("customerservices@sabrerail.com"));
                    }
                }
                else
                {
                    message.To.Add(new MailAddress("customerservices@sabrerail.com"));
                }

                message.Subject = "AWK Quote";
                message.Body = myItem.EmailMessage;
                message.From = new MailAddress("Titan@sabrerail.com");

                if (Titan.FeatureFlags.ShouldRunFeature("AddAttachmentFromFile"))
                {
                    message.Attachments.Add(new Attachment(@"C:\GeneratedReports\" + filename));
                }

                message.Attachments.Add(new Attachment(pdfStream, filename));

                // send mail (if in test, save to disk or send to test mail server)
                SmtpClient.Send(message);

                //close pdf document
                pdfStream.Close();
                message.Dispose();
            }
            catch (Exception ex)
            {
                message?.Attachments.ToList().ForEach(a => a.Dispose());
                message?.Dispose();

                //DataLogger.LogData("SendQuote", "Error: " + ex.Message.ToString(), 0);
                return StatusCode(StatusCodes.Status502BadGateway);
            }

            return Ok();
        }

        /// <response code="200">Succesfully authorised AWK</response>
        /// <response code="400">Invalid WO number</response>
        /// <response code="404">Could not find BOM reference or linked WO info</response>
        [HttpPost]
        [Route("Authorise")]
        [Feature("AuthoriseAWK")]
        public async Task<ActionResult> AWKAuthorise(int awn)
        {
            var header = new AWKHeader();

            header = Titan.AWKHeaders.Where(awk => awk.ID == awn)
                .FirstOrDefault();

            if (!TryGetValidWO(header.WorksOrderNumber, out SiWorksOrder WorksOrder))
            {
                return BadRequest();
            }

            if (!TryGetWOInfo(WorksOrder, out StockItem WorksOrderReturnItem,
                out SOPOrderReturn SalesOrder, out SLCustomerAccount Customer))
            {
                return NotFound();
            }

            WorksOrder.SpareText5 = ""; //Additional Work Released
            Sage.SaveChanges();

            if (Titan.AWKSettings.Select(x => x.EmailOnApproval).Single())
            {
                var Body = await ReportEngine.TemplateAsync(new TemplateRequestDTO
                {
                    Template = Titan.EmailTemplates
                        .Where(template => template.TemplateName == "AWKAuthorised")
                        .Single().Template,
                    Data = new
                    {
                        AWN = header.ID,
                        WONumber = WorksOrder.WONumber,
                        Items = Titan.AWKDetails
                            .Where(detail => detail.DID == header.ID)
                            .Where(detail => detail.RequiredQty > 0)
                            .Select(detail => new
                            {
                                Description = detail.Description,
                                PartNumber = detail.StockCode,
                                Fault = detail.Fault,
                                Quantity = detail.RequiredQty
                            })
                    }
                });

                SmtpClient.Email(Titan.AWKSettings.Single().EmailAddresses,
                    "AWK Authorised", Body);
            }

            //Update Status
            header.Authorised = true;
            header.AuthorisedBy = HttpContext.GetUserDisplayName();
            header.DateAuthorised = Now;

            Titan.SaveChanges();

            return Ok();
        }

        /// <response code="200">Returns informaton about closed AWK</response>
        [HttpGet]
        [Route("Closed/{AWN}")]
        public ActionResult<AWHeader> GetClosedAWK(int AWN)
        {
            var myAWK = Titan.AWKHeaders
                .Where(AWK => AWK.ID == AWN)
                .Select(AWK => new AWHeader
                {
                    ID = AWK.ID,
                    WorksOrderNumber = AWK.WorksOrderNumber,
                    BomReference = AWK.CatNo,
                    BomDescription = AWK.Description,
                    QtyRequired = (int)AWK.Qty,
                    LinkedTo = AWK.SOR,
                    CusOrderNumber = AWK.CustOrderNumber,
                    AccountName = AWK.AccountName,
                    NumberOfConsolidatedAffectedItems = AWK.AWKQty ?? 0,
                    TotalPrice = AWK.TotalPrice,
                }).Single();

            bool showAllitems = false;

            if (showAllitems == true)
            {
                List<AWDetail> myitems = (from awd in Titan.vw_AWKStockRecords
                                          where awd.DID == myAWK.ID

                                          select new AWDetail
                                          {
                                              AWDID = awd.AWDID,
                                              ID = awd.DID ?? 0,
                                              StkID = awd.StkID ?? 0,
                                              StockCode = awd.StockCode,
                                              Description = awd.Description,
                                              RequiredQty = awd.RequiredQty ?? 0,
                                              SabreStock = awd.SabreStock ?? false,
                                              Fault = awd.Fault,
                                              WorkRequired = awd.WorkRequired,
                                              RepairDetail = awd.RepairDetail,
                                              UnitPrice = awd.Price ?? 0,
                                              FreeStock = awd.FreeStock ?? 0,
                                              RepairCost = awd.RepairCost ?? 0,
                                              Linetotal = awd.RequiredQty * awd.RepairCost ?? 0,


                                          }).ToList();


                myAWK.AWDetails = myitems.ToList();
            }
            else
            {
                List<AWDetail> myitems = (from awd in Titan.vw_AWKStockRecords
                                          where awd.DID == myAWK.ID && awd.RequiredQty > 0

                                          select new AWDetail
                                          {
                                              AWDID = awd.AWDID,
                                              ID = awd.DID ?? 0,
                                              StkID = awd.StkID ?? 0,
                                              StockCode = awd.StockCode,
                                              Description = awd.Description,
                                              RequiredQty = awd.RequiredQty ?? 0,
                                              SabreStock = awd.SabreStock ?? false,
                                              Fault = awd.Fault,
                                              WorkRequired = awd.WorkRequired,
                                              RepairDetail = awd.RepairDetail,
                                              UnitPrice = awd.Price ?? 0,
                                              FreeStock = awd.FreeStock ?? 0,
                                              RepairCost = awd.RepairCost ?? 0,
                                              Linetotal = awd.RequiredQty * awd.RepairCost ?? 0
                                          }).ToList();


                myAWK.AWDetails = myitems.ToList();
            }

            List<Document> myDocs = Titan.Documents
                .Where(tik => tik.AWKID == AWN)
                .Select(doc => new Document
                {
                    DocID = doc.DocID,
                    DocumentName = doc.DocumentName,
                    AWKID = doc.AWKID ?? 0
                }).ToList();

            myAWK.Documents = myDocs.OrderByDescending(Document => Document.DocumentName)
                .ToList();

            return Ok(myAWK);
        }

        /// <response code="200">Succesfully added image</response>
        [HttpPost]
        [Route("Images/Add/{AWKID}")]
        public ActionResult AddAWKImage(int AWKID, IFormFile image)
        {
            var ImageName = image.FileName ?? "AWK:" + AWKID + "-" + Now.ToString() + ".png";

            var ImageStream = image.OpenReadStream();
            ImageStream.Seek(0, SeekOrigin.Begin);

            var ImageBytes = new byte[ImageStream.Length];
            ImageStream.Read(ImageBytes);

            var B64String = Convert.ToBase64String(ImageBytes);

            Titan.Documents.Add(new Document
            {
                AWKImage = B64String,
                DocumentType = 3,
                DateLastUpdated = DateTime.Now,
                AWKID = AWKID,
                DocumentName = ImageName,
                FileType = ".png"
            });

            Titan.SaveChanges();

            return Ok();
        }

        /// <response code="200">Returns the image</response>
        /// <response code="404">Could not find image with id</response>
        [HttpGet]
        [Route("Images/{id}")]
        public ActionResult<byte[]> GetAWKImage(int id)
        {
            var AWKImageDocument = Titan.Documents.Where(doc => doc.DocID == id)
                .SingleOrDefault();

            if (AWKImageDocument == null)
            {
                return NotFound();
            }

            string B64String = AWKImageDocument.AWKImage;
            B64String = B64String.Split(",").Last();
            byte[] ImageData = Convert.FromBase64String(B64String);

            return ImageData;
        }

        /// <response code="200">Succesfully canceled AWK</response>
        [HttpPost]
        [Route("Cancel")]
        [Feature("CancelAWK")]
        public ActionResult CancelAWK(int id)
        {
            var awk = Titan.AWKHeaders.Where(awk => awk.ID == id)
                .FirstOrDefault();

            awk.Canceled = true;

            Titan.SaveChanges();

            return Ok();
        }

        /// <response code="200">Succesfully canceled AWK</response>
        [HttpPost]
        [Route("RevertToQuote")]
        [Feature("RevertAWKToQuote")]
        public ActionResult RevertToQuote(int id)
        {
            var awk = Titan.AWKHeaders.Where(awk => awk.ID == id)
                .FirstOrDefault();

            awk.AWKQuoted = false;
            awk.Authorised = false;

            Titan.SaveChanges();

            return Ok();
        }

        private bool TryGetValidWO(string WONumber, out SiWorksOrder WorksOrder)
        {
            try
            {
                WONumber = GetWONumber(WONumber);
            }
            catch (Exception)
            {
                WorksOrder = null;
                return false;
            }

            WorksOrder = Sage.SiWorksOrders
                .SingleOrDefault(wo => wo.WONumber == WONumber);

            if (WorksOrder == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool TryGetWOInfo(SiWorksOrder WorksOrder, out StockItem WorksOrderReturnItem,
            out SOPOrderReturn SalesOrder, out SLCustomerAccount Customer)
        {
            try
            {
                WorksOrderReturnItem = Sage.StockItems
                .Single(item => item.ItemID == WorksOrder.StockItemID);

                var SalesOrderReturnLine = Sage.SOPOrderReturnLines
                    .Single(returnLine =>
                        returnLine.SOPOrderReturnLineID == WorksOrder.SOPOrderReturnLineId);

                SalesOrder = Sage.SOPOrderReturns
                    .Single(ret =>
                        ret.SOPOrderReturnID == SalesOrderReturnLine.SOPOrderReturnID);

                var CustomerID = SalesOrder.CustomerID;

                Customer = Sage.SLCustomerAccounts
                    .Single(customer =>
                        customer.SLCustomerAccountID == CustomerID);

                return true;
            }
            catch (Exception)
            {
                WorksOrderReturnItem = null;
                SalesOrder = null;
                Customer = null;
                return false;
            }
        }

        private string GetWONumber(string WONumber)
        {
            // normalise WO number

            var onlyNums = new string(WONumber.Where(c => !Char.IsLetter(c)).ToArray());
            return onlyNums.TrimStart('0');

            //// old method
            //var WOSelectNumbers = new Regex(@"(?:WO)?0*(\d+)");
            //int WOI = int.Parse(WOSelectNumbers.Match(WONumber).Groups[1].Value);

            //return $"WO{WOI:0000000000}";
        }
    }
}
