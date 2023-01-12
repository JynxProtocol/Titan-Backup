using HandlebarsDotNet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using Titan.ReportEngine.Data;
using Titan.ReportEngine.Functions;
using Titan.ReportEngine.Models.Reports;

namespace Titan.ReportEngine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : Controller
    {
        public readonly ReportsContext Reports;
        // culture used to format replaced values
        public static CultureInfo Culture = new CultureInfo("en-GB");

        private static string BasePath = AppDomain.CurrentDomain.BaseDirectory;
        private static DirectoryInfo TemplatesDir =
            new DirectoryInfo(Path.Combine(BasePath, "Templates"));

        // handlebars and template management 
        private static TemplatesFileSystem TemplatesFileSystem =
            new TemplatesFileSystem(TemplatesDir);

        private static TemplateResolver TemplateResolver =
            new TemplateResolver();

        private IHandlebars HandleBars = Handlebars.CreateSharedEnvironment(
            new HandlebarsConfiguration
            {
                PartialTemplateResolver = TemplateResolver,
                FileSystem = TemplatesFileSystem
            });

        internal ILogger<ReportController> logger { get; private set; }

        public ReportController(ReportsContext reports, ILogger<ReportController> _logger)
        {
            Reports = reports;
            logger = _logger;

            HandleBars.RegisterHelper("inc", (writer, context, parameters) =>
            {
                writer.WriteSafeString($"{(int)parameters[0] + 1}");
            });
        }

        /// <response code="200">Returns the generated report</response>
        /// <response code="400">Error generating report</response>
        /// <response code="404">Could not find report</response>
        [HttpGet]
        [Route("Generate/{ReportName}")]
        [SwaggerResponse(200, "application/pdf", Type = typeof(FileContentResult))]
        [Produces("application/pdf")]
        public ActionResult Generate(string ReportName, string? id = null)
        {
            logger.LogInformation("Generating Report");
            // load configuration for this report from db
            var reportConfig = Reports.ReportConfigs
                .Where(report => report.Name == ReportName).SingleOrDefault();

            if (reportConfig == null)
            {
                return NotFound();
            }

            // the actions to perform for the report, and what, if anything, to return
            var ReportActions = Reports.ReportActions
                .Where(reportAction => reportAction.ReportName == reportConfig.Name).ToList();
            var ReturnAction = Reports.ReturnActions
                .Where(returnAction => returnAction.Id == reportConfig.ReturnActionId)
                .SingleOrDefault();

            // if there is nothing to do, return an error
            if (ReturnAction == null && ReportActions.Count() == 0)
            {
                BadRequest();
            }

            var Report = new Report(HandleBars, TemplatesDir, TemplatesFileSystem);

            // do all of the actions that don't need to return something, in the correct order
            foreach (var ReportAction in ReportActions)
            {
                // settings for this action specific to this report
                var ReportActionSettings = Reports.ReportActionSettings.Where(setting =>
                    setting.ReportName == ReportName && setting.ActionId == ReportAction.ActionId);

                // what action we will be doing
                var Action = Reports.FreeActions.Single(action =>
                    action.Id == ReportAction.ActionId);

                // settings for this action in general
                var ActionSettings = Reports.ActionSettings.Where(setting =>
                    setting.Id == Action.Id);

                ReportActionSetting FilePathSetting;

                switch (Action.Name)
                {
                    case "GenerateHTML":
                        Report.FromName(ReportName).LoadDatasources(id).FillData();
                        break;
                    case "GeneratePDF":
                        Report.GeneratePDF();
                        break;
                    case "SaveHTMLAsFile":
                        FilePathSetting =
                            ReportActionSettings.Single(s => s.Name == "FilePath");

                        Report.SaveHTMLAs(FilePathSetting.Value.Replace("{id}", id ?? ""));
                        break;
                    case "SavePDFAsFile":
                        FilePathSetting =
                            ReportActionSettings.Single(s => s.Name == "FilePath");

                        Report.SavePDFAs(FilePathSetting.Value.Replace("{id}", id ?? ""));
                        break;
                    case "SavePDFToDB":
                        Report.SavePDFToDB(HttpContext?.User?.Identity?.Name);
                        break;
                    case "EmailPDFTo":
                        break;
                    default:
                        throw new NotImplementedException($"Unknown action \"{Action.Name}\"");
                }
            }

            if (ReturnAction == null)
            {
                return Ok();
            }
            else
            {
                return ReturnAction.Name switch
                {
                    "HTML" =>
                        Content(Report.ReturnHTML(),
                            Microsoft.Net.Http.Headers.MediaTypeHeaderValue.Parse("text/html")),
                    "PDF" =>
                        File(Report.ReturnPDF(), "application/pdf",
                            $"{ReportName}{(string.IsNullOrWhiteSpace(id) ? "" : id)}.pdf"),
                    "NOTHING" =>
                        Ok(),
                    _ =>
                        BadRequest(),
                };
            }

        }

        /// <response code="200">Returns the fetched report</response>
        /// <response code="404">Could not find saved report</response>
        [HttpGet]
        [SwaggerResponse(200, "application/pdf", Type = typeof(FileContentResult))]
        [Produces("application/pdf")]
        [Route("FromDB/{ReportName}")]
        public ActionResult FromDBByName(string ReportName)
        {
            var Report = GetReportFromDB(ReportName, null, null);

            if (Report != null)
            {
                return File(Report, "application/pdf",
                    $"{ReportName}.pdf");
            }
            else
            {
                return NotFound();
            }
        }

        /// <response code="200">Returns the fetched report</response>
        /// <response code="404">Could not find saved report</response>
        [HttpGet]
        [SwaggerResponse(200, "application/pdf", Type = typeof(FileContentResult))]
        [Produces("application/pdf")]
        [Route("FromDB/{ReportName}/V{version:int}")]
        public ActionResult FromDBByNameAndVersion(string ReportName, int version)
        {
            var Report = GetReportFromDB(ReportName, null, version);

            if (Report != null)
            {
                return File(Report, "application/pdf",
                    $"{ReportName}.pdf");
            }
            else
            {
                return NotFound();
            }
        }

        /// <response code="200">Returns the fetched report</response>
        /// <response code="404">Could not find saved report</response>
        [HttpGet]
        [SwaggerResponse(200, "application/pdf", Type = typeof(FileContentResult))]
        [Produces("application/pdf")]
        [Route("FromDB/{ReportName}/{id}")]
        public ActionResult FromDBByNameAndID(string ReportName, string id)
        {
            var Report = GetReportFromDB(ReportName, id, null);

            if (Report != null)
            {
                return File(Report, "application/pdf",
                    $"{ReportName}{id}.pdf");
            }
            else
            {
                return NotFound();
            }
        }

        /// <response code="200">Returns the fetched report</response>
        /// <response code="404">Could not find saved report</response>
        [HttpGet]
        [SwaggerResponse(200, "application/pdf", Type = typeof(FileContentResult))]
        [Produces("application/pdf")]
        [Route("FromDB/{ReportName}/{id}/V{version:int}")]
        public ActionResult FromDBByNameIDAndVersion(string ReportName, string id, int version)
        {
            var Report = GetReportFromDB(ReportName, id, version);

            if (Report != null)
            {
                return File(Report, "application/pdf",
                    $"{ReportName}{id}v{version}.pdf");
            }
            else
            {
                return NotFound();
            }
        }

        private byte[] GetReportFromDB(string ReportName, string id = null, int? version = null)
        {
            var Reports = new ReportsContext();

            ReportDocumentHeader ReportDocumentHeader;

            if (string.IsNullOrWhiteSpace(id))
            {
                ReportDocumentHeader = Reports.ReportDocumentHeaders
                    .Where(report => report.ReportName == ReportName &&
                        (report.ReportId == null))
                    .SingleOrDefault();
            }
            else
            {
                ReportDocumentHeader = Reports.ReportDocumentHeaders
                    .Where(report => report.ReportName == ReportName &&
                        (report.ReportId == id))
                    .SingleOrDefault();
            }

            if (ReportDocumentHeader == null)
            {
                return null;
            }

            ReportDocument ReportDocument;

            if (version == null)
            {
                ReportDocument = ReportDocumentHeader.ReportDocuments
                    .OrderByDescending(doc => doc.Version)
                    .FirstOrDefault();
            }
            else
            {
                ReportDocument = ReportDocumentHeader.ReportDocuments
                    .Where(doc => doc.Version == version)
                    .SingleOrDefault();
            }

            if (ReportDocument == null)
            {
                return null;
            }
            else
            {
                return ReportDocument.Report;
            }
        }

        public class TemplateRequestDTO
        {
            public string Template { get; set; }
            public dynamic Data { get; set; }
        }

        [HttpPost]
        [Route("Template")]
        [Produces("application/json")]
        public ActionResult<string> Template(TemplateRequestDTO templateRequest)
        {
            try
            {
                var Template = templateRequest.Template;
                dynamic Data = templateRequest.Data;

                var Result = HandleBars.Compile(Template)(Data);
                return Ok(Result);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
