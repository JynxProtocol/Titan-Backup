using HandlebarsDotNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Titan.ReportEngine.Functions
{
    /// <summary>
    /// Modified template file system, which overrides several default methods, 
    /// as well as implementing some conflict resolution logic. It also uses a recursive 
    /// approach to finding files, searching the files closest to the current file first, 
    /// then proceeding up through directories
    /// </summary>
    public class TemplatesFileSystem : ViewEngineFileSystem
    {
        private DirectoryInfo TemplatesDir;

        public TemplatesFileSystem(DirectoryInfo templatesDir)
        {
            TemplatesDir = templatesDir;
        }

        public override bool FileExists(string filePath)
        {
            var DirectoryPath = Path.GetDirectoryName(filePath);
            var FileName = Path.GetFileName(filePath);

            if (!Directory.Exists(DirectoryPath))
            {
                return false;
            }

            return Directory.GetFiles(DirectoryPath, FileName, SearchOption.TopDirectoryOnly).Any();
        }

        public override string GetFileContent(string filename)
        {
            if (!FileExists(filename))
            {
                return null;
            }

            return File.ReadAllText(filename);
        }

        protected override string CombinePath(string dir, string otherFileName)
        {
            return Path.Join(dir, otherFileName);
        }

        public string FindTemplateByName(string templateName, string currentPath = null)
        {
            var Templates = TemplatesDir.GetFiles($"{templateName}.*", SearchOption.AllDirectories);

            if (Templates.Count() == 0)
            {
                throw new FileNotFoundException(
                    $"Cannot find file for template \"{templateName}\"");
            }
            else if (Templates.Count() == 1)
            {
                return Templates.Single().FullName;
            }
            else
            {
                return ResolveTemplateConflict(Templates, currentPath, templateName);
            }
        }

        private string ResolveTemplateConflict(
            FileInfo[] Templates, string currentPath, string templateName)
        {
            Func<string, string> MoveUpOneDirectory =
                (string dir) => new DirectoryInfo(dir).Parent.FullName;

            Func<string, bool> IsInTemplatesDir =
                (string dir) => new DirectoryInfo(dir).FullName.Contains(TemplatesDir.FullName);

            if (currentPath != null)
            {
                // at each step, go up one directory from the current template until there are none left
                var filePath = new FileInfo(currentPath).DirectoryName;
                for (; IsInTemplatesDir(filePath); filePath = MoveUpOneDirectory(filePath))
                {
                    var TemplatesAtThisStep = Templates
                        .Where(x => x.DirectoryName.Contains(filePath));

                    if (TemplatesAtThisStep.Count() == 0)
                    {

                    }
                    else if (TemplatesAtThisStep.Count() == 1)
                    {
                        return TemplatesAtThisStep.Single().FullName;
                    }
                    else
                    {
                        // sort templates by order that we want them
                        // prefer .hbs files, and files in ~/partial, in that order
                        var SortedTemplates = TemplatesAtThisStep
                            .GroupBy(f => new
                            {
                                IsHBS = f.Extension == ".hbs",
                                IsInPartialDirectory = f.Directory.Name == "partials"
                            })
                            .OrderByDescending(x => x.Key.IsHBS)
                            .ThenByDescending(x => x.Key.IsInPartialDirectory);

                        var BestMatches = SortedTemplates.First();

                        if (BestMatches.Count() > 1)
                        {
                            return AmbiguousMatch(templateName,
                                    BestMatches.Select(f => f.FullName));
                        }
                        else
                        {
                            return BestMatches.Single().FullName;
                        }
                    }
                }
            }

            return AmbiguousMatch(templateName, Templates.Select(f => f.FullName));
        }

        private string AmbiguousMatch(string templateName, IEnumerable<string> Matches)
        {
#if DEBUG
            var Debug = true;
#else
            var Debug = false;
#endif

            throw new System.Reflection.AmbiguousMatchException(
                $"Found multiple files for template \"{templateName}\""
                + (Debug ? ":\n\t" + string.Join("\n\t", Matches) : "")
                );
        }

        public string ReadTemplateByName(string templateName)
        {
            return GetFileContent(FindTemplateByName(templateName));
        }
    }
}
