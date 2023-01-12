using Balbarak.WeasyPrint;
using HandlebarsDotNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security;
using Titan.ReportEngine.Data;
using Titan.ReportEngine.DataSources;
using Titan.ReportEngine.Functions;
using Titan.ReportEngine.Models.Reports;

namespace Titan.ReportEngine
{
    /// <summary>
    /// A class representing a report being generated. To generate a report, 
    /// the class uses a templates directory and filesystem, Handlebars, and a list of datasources.
    /// Most of the methods use the fluent syntax which makes chaining operations easier
    /// </summary>
    public class Report
    {
        // current report datasources
        private Dictionary<string, object> DataSources = new();
        public bool AreDataSourcesInitialised { get; private set; }

        private static string BasePath = AppDomain.CurrentDomain.BaseDirectory;
        private static DirectoryInfo TemplatesDir;
        private IHandlebars HandleBars;
        private static TemplatesFileSystem TemplatesFileSystem;

        // datasources included in this assembly
        private static string DataSourcesNamespace =
            Assembly.GetExecutingAssembly().GetName().Name + ".DataSources";
        private static IEnumerable<Type> DataSourcesAvailable =
            Assembly.GetExecutingAssembly().GetTypes().Where(type =>
                type.Namespace == DataSourcesNamespace &&
                type.GetInterfaces().Contains(typeof(IDataSource)));

        public string TemplateFile { get; private set; }

        public string HTML { get; set; }
        public bool IsHTMLGenerated { get; private set; }

        private MemoryStream PDF { get; set; }
        public bool IsPDFGenerated { get; private set; }

        public string ReportName { get; private set; }
        public string ReportId { get; private set; }

        /// <summary>
        /// Initialise the report to use the given Handlebars instance, 
        /// templates directory, and filesystem. Also initialises the PDF stream and a helper for 
        /// accessing the templates directory inside templates (used for including images)
        /// </summary>
        /// <param name="_HandleBars">The Handlebars instance to use to generate templates</param>
        /// <param name="_TemplatesDir">The directory to search (recursively) for templates</param>
        /// <param name="_TemplatesFileSystem">The filesystem interface to use</param>
        public Report(IHandlebars _HandleBars,
            DirectoryInfo _TemplatesDir, TemplatesFileSystem _TemplatesFileSystem)
        {
            HandleBars = _HandleBars;
            TemplatesDir = _TemplatesDir;
            TemplatesFileSystem = _TemplatesFileSystem;
            PDF = new MemoryStream();

            HandleBars.RegisterHelper("TemplatesDir", (writer, context, parameters) =>
            {
                writer.Write($"{TemplatesDir.FullName.Replace("\\", "/")}");
            });
        }

        /// <summary>
        /// Creates a report from a template name
        /// </summary>
        /// <param name="templateName">The name of the template/report to generate</param>
        public Report FromName(string templateName)
        {
            IsHTMLGenerated = false;

            var TemplateFiles = TemplatesDir
                .GetFiles($"{templateName}.*", SearchOption.AllDirectories);

            if (TemplateFiles.Count() == 0)
            {
                throw new FileNotFoundException(
                    $"Cannot find file for template {templateName}");
            }
            else if (TemplateFiles.Count() > 1)
            {
                throw new AmbiguousMatchException(
                    $"Found multiple files for template {templateName}:\n\t"
                    + string.Join("\n\t", TemplateFiles.Select(f => $"{f.FullName}"))
                    );
            }

            TemplateFile = TemplateFiles.Single().FullName;
            ReportName = templateName;

            return this;
        }

        /// <summary>
        /// Reads the report template and loads the datasources required by it
        /// </summary>
        /// <param name="id">The id passed to the report, if any. 
        /// This is used to pass information to the datasource</param>
        public Report LoadDatasources(string id)
        {
            var TemplateHtml = TemplatesFileSystem.GetFileContent(TemplateFile);

            // get all datasources used by this report
            var DataSourcesUsed = Regexes.FindDataSource_SelectName.Matches(TemplateHtml)
                .Select(match => match.Groups[1].Value);

            // for each datasource, load the data by creating an instance
            // and add it to the dictionary of avaliable datasources
            foreach (string DataSourceUsedString in DataSourcesUsed)
            {
                var DataSourceUsed = DataSourcesAvailable
                    .Where(x => x.Name == DataSourceUsedString).SingleOrDefault();

                if (DataSourceUsed == null)
                {
                    throw new ArgumentNullException(
                        $"Could not find datasource \"{DataSourceUsedString}\"");
                }

                // load data by calling the constructor on the datasource
                if (string.IsNullOrWhiteSpace(id))
                {
                    DataSources[DataSourceUsedString] =
                        Activator.CreateInstance(DataSourceUsed);
                }
                else
                {
                    try
                    {
                        DataSources[DataSourceUsedString] =
                            Activator.CreateInstance(DataSourceUsed, id);
                    }
                    catch (MissingMethodException ex)
                    {

                        throw new ArgumentException(
                            $"Could not load datasource \"" + DataSourceUsedString +
                                "\" because it does not accept an id",
                            ex);
                    }
                }
            }

            AreDataSourcesInitialised = true;
            ReportId = id;

            return this;
        }

        /// <summary>
        /// Replaces the template with actual data, loaded from datasources, 
        /// and generates HTML as a result
        /// </summary>
        public Report FillData()
        {
            if (!AreDataSourcesInitialised)
            {
                throw new System.InvalidOperationException(
                    "Cannot fill data without having initialised datasources");
            }

            var Template = HandleBars.CompileView(TemplateFile);

            HTML = Template(DataSources);

            IsHTMLGenerated = true;

            return this;
        }

        /// <summary>
        /// Renders generated HTML to a PDF, using WeasyPrint. The way the PDF is layed out 
        /// can be configured using CSS @page styles. Most reports include styles that 
        /// add headers and footers to the PDF by including the ReportStyles
        /// </summary>
        public Report GeneratePDF()
        {
            if (!IsHTMLGenerated)
            {
                throw new System.InvalidOperationException(
                    "Cannot generate PDF without having generated HTML");
            }

            var HtmlToConvert = HTML;

            var TraceWriter = new ConsoleTraceWriter();

            using (WeasyPrintClient client = new WeasyPrintClient(TraceWriter))
            {
                PDF.Seek(0, SeekOrigin.Begin);
                PDF.Write(client.GeneratePdf(HtmlToConvert));
            }

            PDF.Seek(0, SeekOrigin.Begin);
            IsPDFGenerated = true;

            return this;
        }

        /// <summary>
        /// Saves the generated HTML to a file, overwriting if necessary 
        /// </summary>
        /// <param name="filePath">The file to save to</param>
        public Report SaveHTMLAs(string filePath)
        {
            if (!IsHTMLGenerated)
            {
                throw new System.InvalidOperationException(
                    "Cannot save HTML without having generated HTML");
            }

            CheckIfValidDirectory(filePath);

            File.WriteAllText(filePath, HTML);

            return this;
        }

        /// <summary>
        /// Saves the generated PDF to a file, overwriting if necessary 
        /// </summary>
        /// <param name="filePath">The file to save to</param>
        public Report SavePDFAs(string filePath)
        {
            if (!IsPDFGenerated)
            {
                throw new System.InvalidOperationException(
                    "Cannot save PDF without having generated PDF");
            }

            CheckIfValidDirectory(filePath);

            using (var file = File.Create(filePath))
            {
                PDF.Seek(0, SeekOrigin.Begin);
                PDF.CopyTo(file);
            };

            return this;
        }

        /// <summary>
        /// Determines if the given directory exists and is writable as the current user
        /// </summary>
        /// <param name="filePath"></param>
        /// <exception cref="System.ArgumentException"></exception>
        private void CheckIfValidDirectory(string filePath)
        {
            try
            {
                var file = new FileInfo(filePath);

                var dir = Path.GetDirectoryName(filePath);

                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                if (!DirFunctions.DirectoryHasPermission(
                    dir, System.Security.AccessControl.FileSystemRights.CreateFiles)
                    || !DirFunctions.DirectoryHasPermission(
                    dir, System.Security.AccessControl.FileSystemRights.WriteData))
                {
                    throw new SecurityException($"Cannot access \"{dir}\" for writing report");
                }
            }
            catch (Exception ex)
            {
                throw new System.ArgumentException(
                    $"Filepath \"{filePath}\" is not valid", ex);
            }
        }

        /// <summary>
        /// Saves the generated PDF to the database, adding a new header if necessary, 
        /// otherwise incrementing the current version. 
        /// </summary>
        /// <param name="Username">The user to record as having saved the document</param>
        public Report SavePDFToDB(string Username)
        {
            if (!IsPDFGenerated)
            {
                throw new System.InvalidOperationException(
                    "Cannot save PDF without having generated PDF");
            }

            var Reports = new ReportsContext();

            var ReportDocumentHeader = Reports.ReportDocumentHeaders
                .Where(report => report.ReportName == ReportName &&
                    (report.ReportId == ReportId))
                .SingleOrDefault();

            if (ReportDocumentHeader == null)
            {
                ReportDocumentHeader = new ReportDocumentHeader()
                {
                    ReportName = ReportName,
                    ReportId = ReportId,
                    DateFirstGenerated = DateTime.Now,
                    LastGeneratedBy = Username,
                    DateLastGenerated = DateTime.Now
                };

                Reports.ReportDocumentHeaders.Add(ReportDocumentHeader);
                Reports.SaveChanges();

                ReportDocumentHeader = Reports.ReportDocumentHeaders
                    .Where(report => report.ReportName == ReportName &&
                        (string.IsNullOrWhiteSpace(ReportId) || report.ReportId == ReportId))
                    .Single();
            }

            PDF.Seek(0, SeekOrigin.Begin);

            var NextVersion = (ReportDocumentHeader.LatestVersion ?? 0) + 1;

            ReportDocumentHeader.ReportDocuments.Add(new ReportDocument()
            {
                DateGenerated = DateTime.Now,
                Report = PDF.ToArray(),
                Version = NextVersion
            });

            ReportDocumentHeader.DateLastGenerated = DateTime.Now;
            ReportDocumentHeader.LatestVersion = NextVersion;

            Reports.SaveChanges();

            return this;
        }

        public string ReturnHTML()
        {
            if (!IsHTMLGenerated)
            {
                throw new System.InvalidOperationException(
                    "Cannot return HTML without having generated HTML");
            }
            return HTML;
        }

        public MemoryStream ReturnPDF()
        {
            if (!IsPDFGenerated)
            {
                throw new System.InvalidOperationException(
                    "Cannot return PDF without having generated PDF");
            }

            PDF.Seek(0, SeekOrigin.Begin);
            return PDF;
        }
    }

    internal class ConsoleTraceWriter : ITraceWriter
    {
        public void Info(string message)
        {
            Console.WriteLine(message);
        }

        public void Verbose(string message)
        {

        }
    }
}
