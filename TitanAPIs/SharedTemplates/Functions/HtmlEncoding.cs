using Ganss.XSS;
using System.Text;
using System.Web;

namespace Titan.Functions
{
    public static class HtmlEncoding
    {
        private static HtmlSanitizer HtmlSanitizer = new HtmlSanitizer();
        private static UnicodeEncoding Unicode = new UnicodeEncoding();

        public static string Decode(string input)
        {
            return HtmlSanitizer.Sanitize(
                HttpUtility.HtmlDecode(
                    System.Text.RegularExpressions.Regex.Unescape(
                        HttpUtility.HtmlDecode(input)
                        )
                    )
                );
        }
    }
}
