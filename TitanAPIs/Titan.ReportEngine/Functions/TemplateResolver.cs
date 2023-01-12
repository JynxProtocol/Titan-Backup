using HandlebarsDotNet;
using System;

namespace Titan.ReportEngine.Functions
{
    /// <summary>
    /// Modified partial template resolver, used to find handlebars partial templates 
    /// using the same mechanism as non-partial templates, which is defined in the custom 
    /// <seealso cref="TemplatesFileSystem"/>, as opposed to the less intelligent default method, 
    /// which finds the closest file in the partials directory and that ends in .hbs
    /// </summary>
    // minor differences from https://github.com/Handlebars-Net/Handlebars.Net/blob/master/source/Handlebars/FileSystemPartialTemplateResolver.cs
    public class TemplateResolver : IPartialTemplateResolver
    {
        public bool TryRegisterPartial(IHandlebars env, string partialName, string templatePath)
        {
            if (env == null)
            {
                throw new ArgumentNullException(nameof(env));
            }

            var handlebarsTemplateRegistrations = env.Configuration as IHandlebarsTemplateRegistrations;
            if (handlebarsTemplateRegistrations?.FileSystem == null || templatePath == null || partialName == null)
            {
                return false;
            }

            // code changes here
            string partialPath;

            partialPath = (handlebarsTemplateRegistrations.FileSystem as TemplatesFileSystem)
                .FindTemplateByName(partialName, templatePath);

            if (partialPath == null)
            {
                // Failed to find partial in filesystem
                return false;
            }
            // end code changes

            if (partialPath != null)
            {
                var compiled = env
                    .CompileView(partialPath);

                handlebarsTemplateRegistrations.RegisteredTemplates.AddOrReplace(partialName, (writer, o, data) =>
                {
                    writer.Write(compiled(o, data));
                });

                return true;
            }
            else
            {
                // Failed to find partial in filesystem
                return false;
            }
        }
    }
}
