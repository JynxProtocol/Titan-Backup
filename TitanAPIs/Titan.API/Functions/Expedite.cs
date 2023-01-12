using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Threading;
using Titan.API.Models;

namespace Titan.API.Functions
{
    public class Expedite
    {
        public Expedite(IServiceScope serviceScope)
        {
            Configuration = serviceScope.ServiceProvider.GetService<IConfiguration>();
            Sage = serviceScope.ServiceProvider.GetService<SAGEContext>();
            Titan = serviceScope.ServiceProvider.GetService<TitanContext>();
            SmtpClient = serviceScope.ServiceProvider.GetService<SmtpClient>();
        }

        private static string CurrentStep;
        private static string Report;

        private IConfiguration Configuration { get; set; }
        private SAGEContext Sage { get; set; }
        private TitanContext Titan { get; set; }

        private SmtpClient SmtpClient { get; set; }

        private string Status
        {
            set
            {
                Titan.NewExpedites.Single().Status = value;
                Titan.SaveChanges();
            }
        }
        private string Progress
        {
            set
            {
                Titan.NewExpedites.Single().Progress = value;
                Titan.SaveChanges();
            }
        }
        private DateTime LastRunTime
        {
            set
            {
                Titan.NewExpedites.Single().LastRunTime = value;
                Titan.SaveChanges();
            }
        }
        private DateTime LastErrorTime
        {
            set
            {
                Titan.NewExpedites.Single().LastErrorTime = value;
                Titan.SaveChanges();
            }
        }
        private string LastErrorMessage
        {
            set
            {
                Titan.NewExpedites.Single().LastErrorMessage = value;
                Titan.SaveChanges();
            }
        }
        private string LastError
        {
            set
            {
                Titan.NewExpedites.Single().LastError = value;
                Titan.SaveChanges();
            }
        }
        private string SkippedRecords
        {
            set
            {
                Titan.NewExpedites.Single().SkippedRecords = value;
                Titan.SaveChanges();
            }
        }

        public static int Horizion { get; private set; } = 36;

        public static bool UseHorizon { get; private set; } = false;

        // the headings/properties to include on the main report
        private static PropertyInfo[] ReportDataLineProperties =
            typeof(ReportDataLine).GetProperties();

        private static List<string> ReportHeadings = ReportDataLineProperties
            .Select(property => property.Name)
            .Concat(new List<string>
            {
                "New Delivery Date",
                "Date Replied",
                "Comments"
            })
            .ToList();

        // the headings/properties to include on the supplier reports
        private static PropertyInfo[] SupplierDataLineProperties =
            typeof(SupplierDataLine).GetProperties();

        private static List<string> SupplierHeadings = SupplierDataLineProperties
            .Select(property => property.Name)
            .Concat(new List<string>
            {
                "New Delivery Date"
            })
            .ToList();

        public void RunExpedite()
        {
            try
            {
                // ############################################################################
                Status = "Setting up";
                Progress = "0%";
                // ############################################################################

                // read email body
                var SupplierReportEmailBody = System.IO.File.ReadAllText(
                    AppDomain.CurrentDomain.BaseDirectory + Configuration["BodyLocation"]);

                if (string.IsNullOrWhiteSpace(SupplierReportEmailBody))
                {
                    throw new ArgumentNullException(nameof(SupplierReportEmailBody),
                        $"Could not get a valid email body from " +
                        $"{AppDomain.CurrentDomain.BaseDirectory + Configuration["BodyLocation"]}");
                }

                // ############################################################################
                Status = "Processing main report";
                // ############################################################################

                //// create a performanceLogDetails, starting a stopwatch
                //// and create another to time the main report
                //PerformanceLogDetails performanceLogDetails = new(HttpContextAccessor.HttpContext)
                //{
                //    Message = "Expedite report took {ElapsedMilliseconds}ms to complete"
                //};
                var reportStopwatch = Stopwatch.StartNew();

                // group expedite data by account
                Status = "Getting data (slow)";
                Progress = "";
                var AccountGroups = GetValidExpediteData()
                    .GroupBy(expediteItem => expediteItem.SupplierAccountNumber);
                Status = "Processing main report";

                // generate and save the main report
                ExcelPackage ExpediteReport = GenerateMainReport(AccountGroups);
                SaveMainReport(ExpediteReport);
                reportStopwatch.Stop();

                // ############################################################################
                Status = "Generating emails";
                // ############################################################################

                // create a new stopwatch to time generating supplier reports
                var supplierReportsStopwatch = Stopwatch.StartNew();

                // generate emails to be sent to suppliers
                var SupplierEmails = GenerateSupplierEmails(
                    AccountGroups, SupplierReportEmailBody);

                supplierReportsStopwatch.Stop();

                // ############################################################################
                Status = "Sending emails";
                // ############################################################################

                // create a new stopwatch to time sending emails
                var sendEmailsStopwatch = Stopwatch.StartNew();

                // Rate limit based on a maximum number of emails per minute
                // Rate limit does not necesarily need to be an integer
                int RateLimitPerMinute = 20;
                double WaitTimeBetween = (double)60 / RateLimitPerMinute;

                // connect to SMTP server
                using SmtpClient MailClient = SmtpClient;

                // send emails to suppliers
                SendEmailsWithRateLimit(SupplierEmails, WaitTimeBetween, MailClient);

                sendEmailsStopwatch.Stop();

                // ############################################################################
                Status = "Resting";
                // ############################################################################

                LastRunTime = DateTime.Now;
                Progress = "0%";

                // let the user know of unprocessable entries
                if (!string.IsNullOrWhiteSpace(Report))
                {
                    SkippedRecords = Report;
                }
                else
                {
                    Report = null;
                }

                //// we are done, so stop the overall performance timer
                //performanceLogDetails.StopTracking();
                //performanceLogDetails.Message = performanceLogDetails.Message
                //    .Replace("{ElapsedMilliseconds}",
                //        performanceLogDetails.ElapsedMilliseconds.ToString());

                //// add extra details
                //performanceLogDetails.AdditionalInfo = new Dictionary<string, object>
                //    {
                //        { "Time taken for main report",
                //            $"{reportStopwatch.ElapsedMilliseconds}ms" },
                //        { "Time taken to generate supplier reports",
                //            $"{supplierReportsStopwatch.ElapsedMilliseconds}ms" },
                //        { "Time taken to send emails",
                //            $"{sendEmailsStopwatch.ElapsedMilliseconds}ms" }
                //    };

                //// if expedite takes too long, escalate the log level 
                //if (performanceLogDetails.ElapsedMilliseconds < 300 * 1000)
                //{
                //    Performance.Log(performanceLogDetails);
                //}
                //else if (performanceLogDetails.ElapsedMilliseconds < 500 * 1000)
                //{
                //    Performance.LogWarning(performanceLogDetails);
                //}
                //else
                //{
                //    Performance.LogError(performanceLogDetails);
                //}
            }
            catch (Exception e)
            {
                // ensure no errors with logging prevent error from being thrown
                try
                {
                    // record exception
                    Status = "ERRORED";

                    Func<string, int, string> Truncate = (string text, int length) =>
                        (text.Length > length ? text.Substring(0, length) : text);

                    LastErrorTime = DateTime.Now;
                    LastErrorMessage = Truncate(e.Message, 254);
                    LastError = Truncate(CurrentStep + "\n" + e.StackTrace, 7999);
                }
                catch
                {

                }

                throw;
            }
        }

        private IQueryable<ExpediteDataDTO> GetDirtyExpediteData()
        {
            CurrentStep = "Getting data";

            IQueryable<ExpediteDataDTO> UnfilteredExpediteData = (
                from RequestedDeliveryDate in Sage.RequestedDeliveryDates
                join POPOrderReturnLine in Sage.POPOrderReturnLines
                on RequestedDeliveryDate.POPOrderReturnLineID
                    equals POPOrderReturnLine.POPOrderReturnLineID

                join POPOrderReturn in Sage.POPOrderReturns
                on POPOrderReturnLine.POPOrderReturnID equals POPOrderReturn.POPOrderReturnID

                join PLSupplierAccount in Sage.PLSupplierAccounts
                on POPOrderReturn.SupplierID equals PLSupplierAccount.PLSupplierAccountID

                join PLSupplierContact in Sage.PLSupplierContacts
                on PLSupplierAccount.PLSupplierAccountID
                    equals PLSupplierContact.PLSupplierAccountID

                join PLSupplierContactValue in Sage.PLSupplierContactValues
                on PLSupplierContact.PLSupplierContactID
                    equals PLSupplierContactValue.PLSupplierContactID

                join PLSupplierContactRole in Sage.PLSupplierContactRoles
                on PLSupplierContact.PLSupplierContactID
                    equals PLSupplierContactRole.PLSupplierContactID

                join SYSTraderContactRole in Sage.SYSTraderContactRoles
                on PLSupplierContactRole.SYSTraderContactRoleID
                    equals SYSTraderContactRole.SYSTraderContactRoleID

                where (
                    (POPOrderReturn.SpareText1 != "TRUE") &&
                    (PLSupplierAccount.SpareText1 != "TRUE") &&
                    (PLSupplierContactValue.SYSContactTypeID == 2) &&
                    (POPOrderReturn.AuthorisationStatusID == 2) &&
                    !(POPOrderReturnLine.ConfirmationIntentTypeID == 2)
                )

                select new ExpediteDataDTO
                {
                    SupplierAccountName = PLSupplierAccount.SupplierAccountName,
                    SupplierAccountNumber = PLSupplierAccount.SupplierAccountNumber,
                    ContactName = PLSupplierContact.ContactName,
                    DocumentNo = POPOrderReturn.DocumentNo,
                    OnOrderQuantity = POPOrderReturnLine.LineQuantity,
                    ItemCode = POPOrderReturnLine.ItemCode,
                    ItemDescription = POPOrderReturnLine.ItemDescription,
                    DeliveryDate = RequestedDeliveryDate.DeliveryDate ?? DateTime.UnixEpoch,
                    ContactValue = PLSupplierContactValue.ContactValue,
                    PO_Role = SYSTraderContactRole.Role,
                    DocumentCreatedBy = POPOrderReturn.DocumentCreatedBy,
                    POPOrderReturnLineID = RequestedDeliveryDate.POPOrderReturnLineID,
                    RecievedQuantity = POPOrderReturnLine.ReceiptReturnQuantity,
                    SupplierPartRef = POPOrderReturnLine.SupplierPartRef,
                });

            // apply filters that do not require other tables
            IQueryable<ExpediteDataDTO> DirtyExpediteData = UnfilteredExpediteData
                .Where(expediteItem => expediteItem.RecievedQuantity < expediteItem.OnOrderQuantity)
                .Where(expediteItem =>
                    expediteItem.PO_Role == "SendPLOrderTo" || expediteItem.PO_Role == "Expedite")
                .Where(expediteItem => !expediteItem.ItemDescription.StartsWith("Carriage"))
                .Where(expediteItem => !expediteItem.ItemDescription.Contains("tooling"));

            // add horizon, otherwise select all dates
            if (UseHorizon)
            {
                DirtyExpediteData = DirtyExpediteData
                    .Where(expediteItem =>
                        expediteItem.DeliveryDate < DateTime.Now.AddDays(Horizion * 7));
            }

            //Debug.WriteLine($"\n{DirtyExpediteData.ToQueryString()}");

            return DirtyExpediteData;//.AsNoTracking();
        }

        private IEnumerable<ExpediteDataDTO> GetValidExpediteData()
        {
            CurrentStep = "Filtering data";

            Func<string, bool> IsContactValueValid =
                (string contactValue) =>
                    EmailValidation.StrongValidationRegex.Matches(contactValue).Count > 0;

            // This method executes the SQL query, now processing happens clientside

            // group by unique key POPOrderReturnLineID
            IEnumerable<IGrouping<long, ExpediteDataDTO>> GroupedExpediteData =
                GetDirtyExpediteData()
                .ToList()
                .GroupBy(expediteItem => expediteItem.POPOrderReturnLineID)
                ;

            // any item with an expedite role and a valid contact value we want to use preferably
            IEnumerable<ExpediteDataDTO> ValidExpedite = GroupedExpediteData
                .Where(groupItem =>
                    groupItem.Any(expediteItem => expediteItem.PO_Role == "Expedite"))
                .Where(groupItem =>
                    groupItem.Any(expediteItem =>
                        IsContactValueValid(expediteItem.ContactValue)))
                .Select(groupItem =>
                    groupItem.First(expediteItem =>
                        IsContactValueValid(expediteItem.ContactValue)))
                ;

            // if an item with an expedite role does not have a valid contact value,
            // we will not report this and instead throw an error
            IEnumerable<ExpediteDataDTO> InvalidExpedite = GroupedExpediteData // log this
                .Where(groupItem =>
                    groupItem.Any(expediteItem => expediteItem.PO_Role == "Expedite"))
                .Where(groupItem =>
                    groupItem.All(expediteItem =>
                        !IsContactValueValid(expediteItem.ContactValue)))
                .Select(groupItem =>
                    groupItem.First())//x.Key)
                ;

            // if an item has no expedite role, use the SendPLOrderTo role
            IEnumerable<ExpediteDataDTO> ValidPO = GroupedExpediteData
                .Where(groupItem =>
                    !groupItem.Any(expediteItem => expediteItem.PO_Role == "Expedite"))
                .Where(groupItem =>
                    groupItem.Any(expediteItem =>
                        IsContactValueValid(expediteItem.ContactValue)))
                .Select(groupItem =>
                    groupItem.First(expediteItem =>
                        IsContactValueValid(expediteItem.ContactValue)))
                ;

            // if an item has no expedite role and also has not valid contact value,
            // we will not report this and instead throw an error
            IEnumerable<ExpediteDataDTO> InvalidPO = GroupedExpediteData // maybe log this
                .Where(groupItem =>
                    !groupItem.Any(expediteItem => expediteItem.PO_Role == "Expedite"))
                .Where(groupItem =>
                    groupItem.All(expediteItem =>
                        !IsContactValueValid(expediteItem.ContactValue)))
                .Select(groupItem =>
                    groupItem.First())//x.Key)
                ;

            // all the items with a valid contact value can be processed
            IEnumerable<ExpediteDataDTO> ValidReportData = ValidExpedite.Concat(ValidPO);

            // select all valid email addresses
            ValidReportData = ValidReportData.Select(expediteData =>
            {
                expediteData.ValidContactAddresses =
                    string.Join(",",
                        EmailValidation.StrongValidationRegex
                            .Matches(expediteData.ContactValue)
                            .Select(match => match.Value));
                return expediteData;
            });

            // everything else must be logged
            IEnumerable<ExpediteDataDTO> InvalidReportData = InvalidExpedite.Concat(InvalidPO);

            // log to DB and debug if there are failures 
            if (InvalidReportData.Any())
            {
                Report =
                    $"{InvalidReportData.Count()} expedite items were skipped:\n" +
                    string.Join("\n",
                        InvalidReportData
                        .OrderBy(data => data.SupplierAccountNumber)
                        .Select(data =>
                            $"POPOrderReturnLine: {data.POPOrderReturnLineID,10} | " +
                            $"Account: {data.SupplierAccountNumber,8} | " +
                            $"Contact: {data.ContactName,15} | " +
                            $"Email: {data.ContactValue} |"
                            ));

                Debug.WriteLine(Report);
                //Titan.LogData("NewExpedite", ExpediteReport, 1);
            }

            return ValidReportData;
        }

        private void SaveMainReport(ExcelPackage ExpediteReport)
        {
            Status = "Saving main report";
            CurrentStep = $"Saving report";

            // get the location to save the report to
            string SavePath = Configuration["ExpediteReportSavePath"];

            if (Configuration["ExpediteReportUseBaseDirectory"] == "true")
            {
                SavePath = AppDomain.CurrentDomain.BaseDirectory;
            }

            // main report is done, so save, dispose and stop the timer
            ExpediteReport.SaveAs(new FileInfo(SavePath + "Expedite Report.xlsx"));
            ExpediteReport.Dispose();
        }

        private ExcelPackage GenerateMainReport(
            IEnumerable<IGrouping<string, ExpediteDataDTO>> AccountGroups)
        {
            Progress = "0%";

            // create new excel document for the main report
            ExcelPackage ExpediteReport = new();
            ExcelWorksheet ReportWorksheet =
                ExpediteReport.Workbook.Worksheets.Add("Expedite Report");

            // add headings
            int ReportY = 2;
            int ReportX = 1;
            foreach (string heading in ReportHeadings)
            {
                ReportWorksheet.Cells[1, ReportX].Value = heading;
                ReportX++;
            }

            var noGroups = AccountGroups.Count();
            var noGroupsProcessed = 0;
            foreach (IGrouping<string, ExpediteDataDTO> AccountExpedite in AccountGroups)
            {
                CurrentStep = $"Main report: {AccountExpedite.Key}";
                var ReportDataLines = AccountExpedite
                    .Select(expediteItem => new ReportDataLine(expediteItem));

                // add this suppliers expedite data to the main report
                foreach (ReportDataLine reportDataLine in ReportDataLines)
                {
                    CurrentStep = $"Generating report: {AccountExpedite.Key} Line {ReportY - 1}";
                    WriteReportDataLine(ReportY, reportDataLine, ReportWorksheet);
                    ReportY++;
                }

                // update progress
                var prog = (int)Math.Floor((noGroupsProcessed / (double)noGroups) * 100);
                Progress = $"{prog}%";
                noGroupsProcessed++;
            }
            Progress = $"100%";

            // autofit columns so document is readable
            ReportWorksheet.Cells[ReportWorksheet.Dimension.Address].AutoFitColumns();

            return ExpediteReport;
        }

        private void SendEmailsWithRateLimit(List<MailMessage> Emails,
            double WaitTimeBetween, SmtpClient MailClient)
        {
            Progress = "0%";

            // preset rate limit time to start immediately  
            DateTime StartSendTime = DateTime.Now.AddSeconds(-(WaitTimeBetween + 1));

            // send each message and then dispose (important)
            var noEmails = Emails.Count();
            var noEmailsProcessed = 0;
            foreach (MailMessage mailMessage in Emails)
            {
                CurrentStep = $"Sending email: {mailMessage.To} {mailMessage.Subject}";

                // NOTE: the order of these two blocks is important, checking the start time
                // right before setting it encompases the entire loop in the timer

                // if we took less than the rate limit time to get ready to send another mail, wait the difference
                var Diff = StartSendTime.AddSeconds(WaitTimeBetween) - DateTime.Now;
                if (Diff > TimeSpan.Zero) { Thread.Sleep((int)Diff.TotalMilliseconds); }

                // record the time we started sending the mail then send
                StartSendTime = DateTime.Now;

                MailClient.Send(mailMessage);

                // dispose all attachment streams
                mailMessage.Attachments.ToList().ForEach(x => x.ContentStream.Dispose());
                mailMessage.Dispose();

                // update progress
                var prog = (int)Math.Floor((noEmailsProcessed / (double)noEmails) * 100);
                Progress = $"{prog}%";
                noEmailsProcessed++;
            }

            Progress = $"100%";

            MailClient.Dispose();
        }

        private List<MailMessage> GenerateSupplierEmails(
            IEnumerable<IGrouping<string, ExpediteDataDTO>> AccountGroups, string Body)
        {
            Progress = "0%";

            // list of emails to be sent
            var SupplierEmails = new List<MailMessage>();

            var noGroups = AccountGroups.Count();
            var noGroupsProcessed = 0;
            foreach (IGrouping<string, ExpediteDataDTO> AccountExpedite in AccountGroups)
            {
                var SupplierDataLines = AccountExpedite
                    .Select(expediteItem => new SupplierDataLine(expediteItem));

                // add individual supplier report email to list of emails to send
                SupplierEmails.Add(GenerateSupplierEmail(
                            AccountExpedite.Key,
                            SupplierDataLines,
                            AccountExpedite.First().ValidContactAddresses,
                            Body
                        ));

                // update progress
                var prog = (int)Math.Floor((noGroupsProcessed / (double)noGroups) * 100);
                Progress = $"{prog}%";
                noGroupsProcessed++;
            }

            Progress = $"100%";

            return SupplierEmails;
        }

        private MailMessage GenerateSupplierEmail(string AccountNumber,
            IEnumerable<SupplierDataLine> SupplierDataLines, string ContactAddresses, string Body)
        {
            CurrentStep = $"Generating email: {ContactAddresses} {AccountNumber}";
            // create new email, add sender, subject and body
            MailMessage message = new()
            {
                From = new MailAddress(Configuration["MailFromAddress"]),
                Subject = Configuration["MailSubject"].Replace("{AccountNumber}", AccountNumber),
                IsBodyHtml = true,
                Body = Body
            };

            // add recepient(s)
            message.To.Add(ContactAddresses);

            // export excel file in memory (instead of saving it to disk then immediately deleting)
            MemoryStream ExcelReportStream = new();
            var SupplierReport = GenerateSupplierReport(AccountNumber, SupplierDataLines);
            SupplierReport.SaveAs(ExcelReportStream);
            SupplierReport.Dispose();

            CurrentStep = $"Generating email: {ContactAddresses} {AccountNumber}";

            // add exported report as attachment
            ExcelReportStream.Position = 0;
            string AttachmentFilename = $"Supplier Report for account {AccountNumber}.xlsx";
            message.Attachments.Add(new Attachment(ExcelReportStream, AttachmentFilename));

            return message;
        }

        private ExcelPackage GenerateSupplierReport(string AccountNumber,
            IEnumerable<SupplierDataLine> SupplierDataLines)
        {
            CurrentStep = $"Generating report: {AccountNumber}";
            // create new excel document for the current supplier report
            ExcelPackage SupplierReport = new();
            ExcelWorksheet SupplierWorksheet =
                SupplierReport.Workbook.Worksheets.Add("Supplier Report");

            // add headings
            int SupplierX = 1;
            foreach (string heading in SupplierHeadings)
            {
                SupplierWorksheet.Cells[1, SupplierX].Value = heading;
                SupplierX++;
            }

            // add supplier's data to thier report
            int SupplierY = 2;
            foreach (SupplierDataLine supplierDataLine in SupplierDataLines)
            {
                CurrentStep = $"Generating report: {AccountNumber} Line {SupplierY - 1}";
                WriteSupplierDataLine(SupplierY, supplierDataLine, SupplierWorksheet);
                SupplierY++;
            }

            // autofit columns so document is readable
            SupplierWorksheet.Cells[SupplierWorksheet.Dimension.Address].AutoFitColumns();

            return SupplierReport;
        }

        private void WriteReportDataLine(int ReportY, ReportDataLine reportDataLine,
            ExcelWorksheet ReportWorksheet)
        {
            for (int ReportX = 1; ReportX <= ReportDataLineProperties.Length; ReportX++)
            {
                var currentProperty = ReportDataLineProperties[ReportX - 1];

                if (currentProperty.PropertyType == typeof(DateTime))
                {
                    ReportWorksheet.Cells[ReportY, ReportX].Value =
                        ((DateTime)currentProperty.GetValue(reportDataLine))
                        .ToString("dd/MM/yyyy")
                        .Replace(".", "");
                }
                else
                {
                    ReportWorksheet.Cells[ReportY, ReportX].Value =
                        currentProperty.GetValue(reportDataLine)?.ToString() ?? "";
                }
            }
        }

        private void WriteSupplierDataLine(int SupplierY, SupplierDataLine supplierDataLine,
            ExcelWorksheet SupplierWorksheet)
        {
            for (int SupplierX = 1; SupplierX <= SupplierDataLineProperties.Length; SupplierX++)
            {
                var currentProperty = SupplierDataLineProperties[SupplierX - 1];

                if (currentProperty.PropertyType == typeof(DateTime))
                {
                    SupplierWorksheet.Cells[SupplierY, SupplierX].Value =
                        ((DateTime)currentProperty.GetValue(supplierDataLine))
                        .ToString("dd/MM/yyyy")
                        .Replace(".", "");
                }
                else
                {
                    SupplierWorksheet.Cells[SupplierY, SupplierX].Value =
                        currentProperty.GetValue(supplierDataLine)?.ToString() ?? "";
                }
            }
        }
    }
}
