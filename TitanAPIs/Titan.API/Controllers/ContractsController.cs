using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using ReportEngineConnection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Titan.API.Models;
using Titan.API.Models.Sales;
using Titan.Filters;
using static Titan.API.Functions.Customer;
using static Titan.API.Functions.Stock;

namespace Titan.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ContractsController : Controller
    {
        internal TitanContext Titan { get; private set; }
        internal SAGEContext Sage { get; private set; }
        internal ReportEngine ReportEngine { get; private set; }
        internal SmtpClient SmtpClient { get; private set; }

        internal DateTime Now = DateTime.Now;

        public ContractsController(TitanContext titan, SAGEContext sage, ReportEngine reportEngine,
            SmtpClient smtpClient)
        {
            Titan = titan;
            Sage = sage;
            ReportEngine = reportEngine;
            SmtpClient = smtpClient;
        }

        /// <response code="200">Returns the list of Open Sales Contracts</response>
        [HttpGet]
        [Route("Open")]
        [Feature("ViewContract")]
        public ActionResult<List<ContractOverview>> ListOpenContracts()
        {
            var Contracts = Titan.ContractHeaders
                .Where(Contract => Contract.Active == 1)
                .Select(Contract => new ContractOverview
                {
                    conID = Contract.ConID,
                    ContractName = Contract.ContractName,
                    CustomerName = Contract.CustomerName,
                    CustomerOrderNumber = Contract.CustomerOrderNumber,
                    DeliveriesReceived = Contract.DeliveriesReceived ?? 0,
                    Active = Contract.Active ?? 0,
                    Acknowledged = Contract.Acknowledged ?? 0,
                    DateCreated = (DateTime)Contract.DateCreated
                })
                .ToList();

            foreach (var Contract in Contracts)
            {
                var ContractTotalValue = Titan.ContractDetails
                    .Where(Detail => Detail.ConID == Contract.conID)
                    .Sum(ContractLine => (ContractLine.TotalQty * ContractLine.UnitPrice) ?? 0);

                var ContractRemainingValue = Titan.ContractDetails
                    .Where(Detail => Detail.ConID == Contract.conID)
                    .Sum(ContractLine => (
                        (ContractLine.TotalQty - ContractLine.TotalQtyReceived)
                            * ContractLine.UnitPrice) ?? 0);

                Contract.TotalValue = ContractTotalValue;
                Contract.RemainingValue = ContractRemainingValue;
            }

            return Ok(Contracts);
        }

        /// <response code="200">Returns the list of Open Sales Contracts</response>
        [HttpGet]
        [Route("Closed")]
        [Feature("ViewContract")]
        public ActionResult<List<ContractOverview>> ListClosedContracts()
        {
            var Contracts = Titan.ContractHeaders
                .Where(Contract => Contract.Active == 0)
                .Select(Contract => new ContractOverview
                {
                    conID = Contract.ConID,
                    ContractName = Contract.ContractName,
                    CustomerName = Contract.CustomerName,
                    CustomerOrderNumber = Contract.CustomerOrderNumber,
                    DeliveriesReceived = Contract.DeliveriesReceived ?? 0,
                    Active = Contract.Active ?? 0,
                    Acknowledged = Contract.Acknowledged ?? 0,
                    DateCreated = (DateTime)Contract.DateCreated
                })
                .ToList();

            foreach (var Contract in Contracts)
            {
                var ContractTotalValue = Titan.ContractDetails
                    .Where(Detail => Detail.ConID == Contract.conID)
                    .Sum(ContractLine => (ContractLine.TotalQty * ContractLine.UnitPrice) ?? 0);

                var ContractRemainingValue = Titan.ContractDetails
                    .Where(Detail => Detail.ConID == Contract.conID)
                    .Sum(ContractLine => (
                        (ContractLine.TotalQty - ContractLine.TotalQtyReceived)
                            * ContractLine.UnitPrice) ?? 0);

                Contract.TotalValue = ContractTotalValue;
                Contract.RemainingValue = ContractRemainingValue;
            }

            return Ok(Contracts);
        }

        /// <response code="200">Returns informaton about Contract</response>
        [HttpGet]
        [Route("{conID}")]
        [Feature("ViewContract")]
        public ActionResult<Contract> GetContract(int conID)
        {
            var mycontract =
                (from cus in Titan.Customers
                 join con in Titan.ContractHeaders on cus.CusID equals con.conCusID
                 where con.ConID == conID
                 select new Models.Sales.Contract
                 {
                     ConID = con.ConID,
                     ContractName = con.ContractName,
                     ConCusID = cus.CusID,
                     CustomerName = cus.CustomerName,
                     CustomerOrderNumber = con.CustomerOrderNumber,
                     CustomerOrderDate = (DateTime)con.CustomerOrderDate,
                     PostalName = con.PostalName,
                     AddressLine1 = con.AddressLine1,
                     AddressLine2 = con.AddressLine2,
                     AddressLine3 = con.AddressLine3,
                     AddressLine4 = con.AddressLine4,
                     City = con.City,
                     County = con.County,
                     Postcode = con.PostCode,
                     BookingInInstructions = con.BookingInInstructions,
                     IsCasualty = con.IsCasualty ?? false,
                     DeliveryTerms = con.DeliveryTerms,
                     DespatchMethod = con.DespatchMethod
                 })
                .SingleOrDefault();

            if (mycontract == null)
            {
                return NotFound();
            }

            List<Models.Sales.ContractItem> myItems = Titan.ContractDetails
                .Where(con => con.ConID == conID).Select(det => new Models.Sales.ContractItem
                {
                    Colour = det.Colour,
                    DeliveryDays = det.DeliveryDays,
                    DeliveryTerms = det.DeliveryTerms,
                    DespatchMethod = det.DespatchMethod,
                    StockCode = det.StockCode,
                    Description = det.Description,
                    ConDetID = det.ConDetID,
                    LeadTime = det.LeadTime,
                    ConID = det.ConID,
                    QtyRequired = det.QtyRequired ?? 0,
                    Warranty = det.Warranty,
                    WorkRequired = det.WorkRequired,
                    UnitPrice = det.UnitPrice ?? 0,
                    WONumber = det.WONumber,
                    DirtyStockCode = det.DirtyStockCode,
                    SageStkID = det.SageStkID,
                    IsSerialised = det.IsSerialised ?? 0,
                    TotalQty = det.TotalQty,
                    TotalQtyReceived = det.TotalQtyReceived,
                    QtyOutstanding = (det.TotalQty ?? 0) - (det.TotalQtyReceived ?? 0),
                    AltPart = det.AlternativePart,
                    AltTo = det.APTConID,
                    Parent = det.ParentPart
                }
           ).ToList();

            mycontract.ContractItems = myItems;

            List<Models.Sales.ContractDocuments> myDocs = Titan.Documents
                .Where(tik => tik.ConID == conID).Select(doc => new Models.Sales.ContractDocuments
                {
                    DocID = doc.DocID,
                    DocumentName = doc.DocumentName,
                    FilePath = doc.FilePath
                }).ToList();

            mycontract.ContractDocuments = myDocs
                .OrderByDescending(ContractDocument => ContractDocument.DocumentName).ToList();

            return Ok(mycontract);
        }

        /// <response code="200">Succesfully created contract</response>
        /// <response code="400">Cannot create contract without a valid customer</response>
        [HttpPost]
        [Route("Create")]
        [Feature("AddContract")]
        public ActionResult CreateContract(Models.Sales.Contract myContract)
        {
            var Customer = CopyCustomerToTitanIfNotThere((int)myContract.ConCusID,
                    Titan, Sage);

            if (Customer == null)
            {
                return BadRequest();
            }

            Titan.ContractHeaders.Add(new ContractHeader
            {
                ContractName = myContract.ContractName,
                CustomerOrderNumber = myContract.CustomerOrderNumber,
                CustomerOrderDate = myContract.CustomerOrderDate,

                conCusID = Customer.CusID,
                CustomerName = Customer.CustomerName,
                Active = 1,
                DeliveriesReceived = 0,

                PostalName = myContract.PostalName,
                AddressLine1 = myContract.AddressLine1,
                AddressLine2 = myContract.AddressLine2,
                AddressLine3 = myContract.AddressLine3,
                AddressLine4 = myContract.AddressLine4,
                City = myContract.City,
                County = myContract.County,
                PostCode = myContract.Postcode,
                BookingInInstructions = myContract.BookingInInstructions,
                IsCasualty = myContract.IsCasualty,
                DeliveryTerms = myContract.DeliveryTerms,
                DespatchMethod = myContract.DespatchMethod,

                CreatedBy = HttpContext.GetUserDisplayName(),
                DateCreated = System.DateTime.Now
            });

            Titan.SaveChanges();

            return Ok();
        }

        /// <response code="200">Succesfully updated contract</response>
        /// <response code="404">Cannot find contract to update</response>
        /// <response code="400">Cannot update a contract to have an invalid customer</response>
        [HttpPost]
        [Route("{conID}/Update")]
        [Feature("ModifyContract")]
        public ActionResult UpdateContract(int conID, Models.Sales.Contract UpdatedContract)
        {
            var Contract = Titan.ContractHeaders.FirstOrDefault(Con => Con.ConID == conID);

            if (Contract == null)
            {
                return NotFound();
            }

            var Customer = CopyCustomerToTitanIfNotThere((int)UpdatedContract.ConCusID,
                    Titan, Sage);

            if (Customer == null)
            {
                return BadRequest();
            }

            Contract.ContractName = UpdatedContract.ContractName;
            Contract.CustomerOrderNumber = UpdatedContract.CustomerOrderNumber;
            Contract.CustomerOrderDate = UpdatedContract.CustomerOrderDate;

            Contract.PostalName = UpdatedContract.PostalName;
            Contract.AddressLine1 = UpdatedContract.AddressLine1;
            Contract.AddressLine2 = UpdatedContract.AddressLine2;
            Contract.AddressLine3 = UpdatedContract.AddressLine3;
            Contract.AddressLine4 = UpdatedContract.AddressLine4;
            Contract.City = UpdatedContract.City;
            Contract.County = UpdatedContract.County;
            Contract.PostCode = UpdatedContract.Postcode;
            Contract.BookingInInstructions = UpdatedContract.BookingInInstructions;
            Contract.IsCasualty = UpdatedContract.IsCasualty;
            Contract.DeliveryTerms = UpdatedContract.DeliveryTerms;
            Contract.DespatchMethod = UpdatedContract.DespatchMethod;

            Contract.LastUpdatedBy = HttpContext.GetUserDisplayName();
            Contract.DateLastUpdated = System.DateTime.Now;

            Contract.conCusID = Customer.CusID;
            Contract.CustomerName = Customer.CustomerName;

            Titan.SaveChanges();

            return Ok();
        }

        /// <response code="200">Succesfully acknowledged contract</response>
        /// <response code="404">Could not get contact address</response>
        /// <response code="500">Could not add attachments to email</response>
        /// <response code="502">Could not send email</response>
        /// <response code="503">Could not generate required report</response>
        [HttpPost]
        [Route("{conID}/Acknowledge")]
        [Feature("AcknowledgeContract")]
        public async Task<ActionResult> AcknowledgeContract(int conID)
        {
            var Contract = Titan.ContractHeaders.Where(con => con.ConID == conID).Single();
            var Account = Sage.SLCustomerAccounts
                .Where(myac => myac.SLCustomerAccountID == Contract.conCusID).Single();

            var contact = Sage.Titan_SLOrderAckContacts_vws
                .Where(acn => acn.CustomerAccountNumber == Account.CustomerAccountNumber)
                .Single();

            // create memory stream to save PDF
            Stream pdfStream = new MemoryStream();
            string filename;

            // call report engine to generate the Contract Acknowledgement
            try
            {
                var ReportResponse =
                    await ReportEngine.GenerateAsync("OrderAcknowledgement", conID.ToString());

                var ContentDisposition = System.Net.Http.Headers.ContentDispositionHeaderValue
                    .Parse(ReportResponse.Headers[HeaderNames.ContentDisposition].First());

                filename = ContentDisposition.FileNameStar ?? ContentDisposition.FileName;

                pdfStream.Seek(0, SeekOrigin.Begin);
                await ReportResponse.Stream.CopyToAsync(pdfStream);
                ReportResponse.Dispose();
                pdfStream.Seek(0, SeekOrigin.Begin);
            }
            catch
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable);
            }

            string Body;

            try
            {
                Body = await ReportEngine.TemplateAsync(new TemplateRequestDTO
                {
                    Template = Titan.EmailTemplates
                        .Where(template => template.TemplateName == "OrderAck")
                        .Single().Template,
                    Data = new
                    {
                        ContactName = contact.ContactName,
                        CustomerRef = Contract.CustomerOrderNumber,
                        SabreRef = Contract.ConID.ToString("D10")
                    }
                });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable);
            }

            MailAddress EmailAddress;

            if (Titan.Settings.Single().OrderAckEmailDirect ?? false)
            {
                if (contact.ContactValue != null)
                {
                    EmailAddress = new MailAddress(contact.ContactValue);
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                EmailAddress = new MailAddress("customerservices@sabrerail.com");
            }

            var message = new MailMessage();

            message.To.Add(EmailAddress);
            message.Bcc.Add("customerservices@sabrerail.com");
            message.Subject = "Order Acknowledgement";
            message.Body = Body;
            message.Priority = MailPriority.High;
            message.From = new MailAddress("Titan@sabrerail.com");

            try
            {
                message.Attachments.Add(
                    new Attachment(@"C:\Titan\OrderAck\Sabre Terms and Conditions.pdf"));

                message.Attachments.Add(
                    new Attachment(pdfStream, filename));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            message.IsBodyHtml = true;

            var SendMail = true;
            if (SendMail)
            {
                try
                {
                    SmtpClient.Send(message);
                }
                catch (Exception ex)
                {
                    //DataLogger.LogData("Email", "Could Not send mail:" + ex.Message, 1);
                    return StatusCode(StatusCodes.Status502BadGateway);
                }
            }

            //Update the contract
            Contract.Acknowledged = 1;
            Titan.SaveChanges();

            return Ok();
        }

        /// <response code="200">Returns informaton about Contract</response>
        [HttpGet]
        [Route("{conID}/Details/{conDetID}")]
        [Feature("ViewContract")]
        public ActionResult<ContractItem> GetContractItem(int conID, int conDetID)
        {
            return Titan.ContractDetails
                .Where(ord => ord.ConDetID == conDetID)
                .Select(Detail => new ContractItem
                {
                    ConDetID = Detail.ConDetID,
                    ConID = conID,

                    QtyRequired = Detail.QtyRequired ?? 0,
                    StockCode = Detail.StockCode,
                    Description = Detail.Description,
                    Warranty = Detail.Warranty,
                    WorkRequired = Detail.WorkRequired,
                    UnitPrice = Detail.UnitPrice ?? 0,
                    SageStkID = Detail.SageStkID,
                    LeadTime = Detail.LeadTime,
                    DirtyStockCode = Detail.DirtyStockCode,

                    Colour = Detail.Colour,
                    DeliveryDays = Detail.DeliveryDays,
                    DeliveryTerms = Detail.DeliveryTerms,
                    DespatchMethod = Detail.DespatchMethod,
                    WONumber = Detail.WONumber,
                    IsSerialised = Detail.IsSerialised ?? 0,
                    TotalQty = Detail.TotalQty,
                    TotalQtyReceived = Detail.TotalQtyReceived,
                    QtyOutstanding = (Detail.TotalQty - Detail.TotalQtyReceived) ?? 0,
                    AltPart = Detail.AlternativePart,
                    AltTo = Detail.APTConID,
                    Parent = Detail.ParentPart
                })
                .Single();
        }

        /// <response code="200">Succesfully added item to contract</response>
        /// <response code="400">Could not find stock code</response>
        /// <response code="404">Could not find contract</response>
        [HttpPost]
        [Route("{conID}/Details/Add")]
        [Feature("ModifyContract")]
        public ActionResult AddItemToContract(int conID, ContractItem ItemToAdd)
        {
            var Contract = Titan.ContractHeaders.FirstOrDefault(Con => Con.ConID == conID);

            if (Contract == null)
            {
                return NotFound();
            }

            try
            {
                CopyStockToTitanIfNotThere(new StockHeader
                {
                    StkID = (int)ItemToAdd.SageStkID,
                    StockCode = ItemToAdd.StockCode,
                    DirtyStockCode = ItemToAdd.DirtyStockCode
                }, Titan, Sage);
            }
            catch (Exception)
            {
                return BadRequest();
            }

            Titan.ContractDetails.Add(new ContractDetail
            {
                ConID = conID,
                CreatedBy = HttpContext.GetUserDisplayName(),
                DateCreated = Now,

                DespatchMethod = Contract.DespatchMethod,
                DeliveryTerms = Contract.DeliveryTerms,
                DeliveryDays = (Contract.DespatchMethod == "Carrier") ? 1 : 0,

                QtyRequired = ItemToAdd.QtyRequired,
                StockCode = ItemToAdd.StockCode,
                Description = ItemToAdd.Description,
                Warranty = ItemToAdd.Warranty,
                WorkRequired = ItemToAdd.WorkRequired,
                UnitPrice = ItemToAdd.UnitPrice,
                SageStkID = ItemToAdd.SageStkID,
                LeadTime = ItemToAdd.LeadTime,
                DirtyStockCode = ItemToAdd.DirtyStockCode,

                Colour = ItemToAdd.Colour,
                SpecialInstruction = ItemToAdd.SpecialInstruction,
                QuotationNumber = ItemToAdd.QuotationNumber,
                IsSerialised = ItemToAdd.IsSerialised,
                TotalQty = ItemToAdd.TotalQty,

                LastUpdatedBy = HttpContext.GetUserDisplayName(),
                DateLastUpdated = Now,
            });
            Titan.SaveChanges();

            return Ok();
        }

        /// <response code="200">Succesfully removed item from contract</response>
        /// <response code="404">Could not find contract</response>
        /// <response code="409">Could not delete contract detail as there are items 
        /// booked in against it</response>
        [HttpDelete]
        [Route("{conID}/Details/{ConDetID}")]
        [Feature("ModifyContract")]
        public ActionResult RemoveItemFromContract(int conID, int ConDetID)
        {
            var Contract = Titan.ContractHeaders.FirstOrDefault(Con => Con.ConID == conID);

            if (Contract == null)
            {
                return NotFound();
            }

            var Detail = Titan.ContractDetails
                .FirstOrDefault(Detail => Detail.ConDetID == ConDetID);

            if (Detail == null)
            {
                return NotFound();
            }

            if ((Detail.TotalQtyReceived ?? 0) != 0)
            {
                return Conflict();
            }

            Titan.ContractDetails.Remove(Detail);

            Titan.SaveChanges();

            return Ok();
        }

        /// <response code="200">Succesfully updated contract item</response>
        /// <response code="400">Could not find stock code</response>
        /// <response code="404">Could not find contract or contract item</response>
        [HttpPost]
        [Route("{conID}/Details/{ConDetId}/Update")]
        [Feature("ModifyContract")]
        public ActionResult UpdateContractItem(int conID, int ConDetId,
            ContractItem NewItem)
        {
            var Contract = Titan.ContractHeaders.FirstOrDefault(Con => Con.ConID == conID);

            if (Contract == null)
            {
                return NotFound();
            }

            var ItemToUpdate =
                Titan.ContractDetails.FirstOrDefault(det => det.ConDetID == ConDetId);

            if (ItemToUpdate == null)
            {
                return NotFound();
            }

            try
            {
                CopyStockToTitanIfNotThere(new StockHeader
                {
                    StkID = (int)NewItem.SageStkID,
                    StockCode = NewItem.StockCode,
                    DirtyStockCode = NewItem.DirtyStockCode
                }, Titan, Sage);
            }
            catch (Exception)
            {
                return BadRequest();
            }

            ItemToUpdate.DespatchMethod = Contract.DespatchMethod;
            ItemToUpdate.DeliveryTerms = Contract.DeliveryTerms;
            ItemToUpdate.DeliveryDays = (ItemToUpdate.DespatchMethod == "Carrier") ? 1 : 0;

            ItemToUpdate.QtyRequired = NewItem.QtyRequired;
            ItemToUpdate.StockCode = NewItem.StockCode;
            ItemToUpdate.Description = NewItem.Description;
            ItemToUpdate.Warranty = NewItem.Warranty;
            ItemToUpdate.WorkRequired = NewItem.WorkRequired;
            ItemToUpdate.UnitPrice = NewItem.UnitPrice;
            ItemToUpdate.SageStkID = NewItem.SageStkID;
            ItemToUpdate.LeadTime = NewItem.LeadTime;
            ItemToUpdate.DirtyStockCode = NewItem.DirtyStockCode;

            ItemToUpdate.Colour = NewItem.Colour;
            ItemToUpdate.SpecialInstruction = NewItem.SpecialInstruction;
            ItemToUpdate.QuotationNumber = NewItem.QuotationNumber;
            ItemToUpdate.IsSerialised = NewItem.IsSerialised;
            ItemToUpdate.TotalQty = NewItem.TotalQty;

            ItemToUpdate.LastUpdatedBy = HttpContext.GetUserDisplayName();
            ItemToUpdate.DateLastUpdated = Now;
            Titan.SaveChanges();

            return Ok();
        }

        /// <response code="200">Succesfully added altpart to contract</response>
        /// <response code="400">Could not find stock code</response>
        /// <response code="404">Could not find contract</response>
        [HttpPost]
        [Route("{conID}/Details/{ConDetID}/AddAlt")]
        [Feature("ModifyContract")]
        public ActionResult AddAltPartToContract(int conID, int ConDetID,
            ContractItem AltPartToAdd)
        {
            var Contract = Titan.ContractHeaders
                .FirstOrDefault(Con => Con.ConID == conID);

            if (Contract == null)
            {
                return NotFound();
            }

            var ParentPart = Titan.ContractDetails
                .FirstOrDefault(con => con.ConDetID == ConDetID);

            if (ParentPart == null)
            {
                return NotFound();
            }

            try
            {
                CopyStockToTitanIfNotThere(new StockHeader
                {
                    StkID = (int)AltPartToAdd.SageStkID,
                    StockCode = AltPartToAdd.StockCode,
                    DirtyStockCode = AltPartToAdd.DirtyStockCode
                }, Titan, Sage);
            }
            catch (Exception)
            {
                return BadRequest();
            }

            Titan.ContractDetails.Add(new ContractDetail
            {
                ConID = conID,
                CreatedBy = HttpContext.GetUserDisplayName(),
                DateCreated = Now,

                QtyRequired = 0,
                APTConID = ConDetID,
                AlternativePart = 1,
                ParentPart = ParentPart.StockCode,

                DespatchMethod = Contract.DespatchMethod,
                DeliveryTerms = Contract.DeliveryTerms,
                DeliveryDays = (Contract.DespatchMethod == "Carrier") ? 1 : 0,

                StockCode = AltPartToAdd.StockCode,
                Description = AltPartToAdd.Description,
                Warranty = AltPartToAdd.Warranty,
                WorkRequired = AltPartToAdd.WorkRequired,
                UnitPrice = AltPartToAdd.UnitPrice,
                SageStkID = AltPartToAdd.SageStkID,
                LeadTime = AltPartToAdd.LeadTime,
                DirtyStockCode = AltPartToAdd.DirtyStockCode,

                Colour = AltPartToAdd.Colour,
                SpecialInstruction = AltPartToAdd.SpecialInstruction,
                QuotationNumber = AltPartToAdd.QuotationNumber,
                IsSerialised = AltPartToAdd.IsSerialised,
                TotalQty = AltPartToAdd.TotalQty,

                LastUpdatedBy = HttpContext.GetUserDisplayName(),
                DateLastUpdated = Now,
            });

            Titan.SaveChanges();

            return Ok();
        }

        /// <response code="200">Succesfully deactivated contract</response>
        /// <response code="404">Could not find contract</response>
        [HttpPost]
        [Route("{conID}/Deactivate")]
        [Feature("DeactivateContract")]
        public ActionResult DeacivateContract(int conID)
        {
            var Contract = Titan.ContractHeaders
                .FirstOrDefault(Con => Con.ConID == conID);

            if (Contract == null)
            {
                return NotFound();
            }

            Contract.Active = 0;

            Titan.SaveChanges();

            return Ok();
        }

        /// <response code="200">Succesfully addeed document</response>
        /// <response code="409">Could not add docmuent, it already exists</response>
        [HttpPost]
        [Route("{conID}/AddDoc")]
        [Feature("ModifyContract")]
        public async Task<ActionResult> AddDocument(int conID, IFormFile document)
        {
            var dir = Titan.Settings.Select(doc => doc.POStorage).FirstOrDefault();
            var myPrefix = Titan.Settings.Select(pre => pre.POPrefix).FirstOrDefault();
            var DirName = dir + myPrefix + conID;

            string myDocName = Path.GetFileName(document.FileName).ToString();

            //else create

            Directory.CreateDirectory(DirName);

            string path = Path.Combine(DirName, Path.GetFileName(document.FileName));

            using (Stream fileStream = new FileStream(path, FileMode.Create))
            {
                await document.CopyToAsync(fileStream);
            }

            //Add to Database

            Titan.Documents.Add(new Document
            {
                DocumentType = 2,
                ConID = conID,
                DocumentName = Path.GetFileName(document.FileName),
                FilePath = path,
                LastUpdatedBy = HttpContext.GetUserDisplayName(),
                DateLastUpdated = DateTime.Now
            });
            Titan.SaveChanges();

            return Ok();
        }

        /// <response code="200">Successfully updated N contract records from SORs</response>
        [HttpPost]
        [Route("BalanceSORs")]
        [Feature("BalanceContracts")]
        public int BalanceContractsFromSORs()
        {
            List<ContractHeader> Contracts = Titan.ContractHeaders.ToList();

            int NoOfRecordsProcessed = 0;

            foreach (var Contract in Contracts)
            {
                string CusDoc = Contract.CustomerOrderNumber;

                var SOPOrderReturnLines = Sage.SOPOrderReturns
                    .Where(SOPHeader => SOPHeader.CustomerDocumentNo == CusDoc)
                    .Include(SOPHeader => SOPHeader.SOPOrderReturnLines)
                    .SelectMany(Header => Header.SOPOrderReturnLines)
                    .ToList();

                var SummedByItemCode = SOPOrderReturnLines
                    .GroupBy(OrderLine => OrderLine.ItemCode)
                    .Select(GroupItem => new SOPOrderReturnLine
                    {
                        ItemCode = GroupItem.Key,
                        LineQuantity = GroupItem.Sum(OrderLine => OrderLine.LineQuantity)
                    })
                    .ToList();


                foreach (var SummedLine in SummedByItemCode)
                {
                    var LinkedContractDetail = Titan.ContractDetails
                        .Where(itm => itm.ConID == Contract.ConID)
                        .Where(itm => itm.StockCode == SummedLine.ItemCode)
                        .FirstOrDefault();

                    if (LinkedContractDetail != null)
                    {
                        LinkedContractDetail.TotalQtyReceived = (int?)SummedLine.LineQuantity;
                    }
                    else
                    {
                        //do Nothing its a free text Line
                    }

                    NoOfRecordsProcessed++;
                }
            }

            Titan.SaveChanges();

            return NoOfRecordsProcessed;
        }
    }
}