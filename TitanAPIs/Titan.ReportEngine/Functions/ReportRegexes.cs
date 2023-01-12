using System.Text.RegularExpressions;

namespace Titan.ReportEngine.Functions
{
    // Abandon all hope ye who debug here
    internal static class Regexes
    {
        internal static Regex FindDataSource_SelectName =
            new("{{#with ?(.*?)}}");
        /*
         * {{#with ? # find the opening tag, ignoring a space
         *    (.*?)  ### select the contents of the tag (the datasource name)
         * }}        # find the closing tag
         */

        internal static Regex FindSubTemplate_SelectName =
            new("{{ ?> ?(.*?)}}");
        /*
         * {{ ?> ? # find the opening tag, ignoring any spaces
         *    (.*?)  ### select the contents of the tag (the subtemplate name)
         * }}        # find the closing tag
         */
    }
}
