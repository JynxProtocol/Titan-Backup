using Ganss.XSS;
using System.Text.RegularExpressions;
using System.Web;

namespace Titan.Functions
{
    public static class HtmlEncoding
    {
        /// <summary>
        /// This function decodes an input string that is given in the form of a JSON string 
        /// representation of HTML. It first decodes any Html/Url encoding, then any unicode
        /// escape sequences, then Html decodes it once again. Finally, the HTML is sanitised 
        /// using the given HtmlSanitizer.
        /// </summary>
        /// <param name="HtmlSanitizer">The HtmlSanitizer used to sanitise HTML</param>
        /// <param name="input">The input string, a JSON encoded HTML string</param>
        public static string Decode(this HtmlSanitizer HtmlSanitizer, string input)
        {
            // First pass to decode any html structures.
            var decoded = HttpUtility.HtmlDecode(input);

            // Second pass to decode any escaped unicode chars.
            decoded = DecodeUnicode(decoded);

            // Third pass to decode any html revealed by decoding unicode.
            // Important to prevent XSS via unicode encoding.
            decoded = HttpUtility.HtmlDecode(decoded);

            // Final pass, removes non-strutural or style-based HTML elements such as scripts.
            var sanitised = HtmlSanitizer.Sanitize(decoded);

            return sanitised;
        }

        /// <summary>
        /// Replaces unicode escapes in the input with the corresponding unicode characters.
        /// </summary>
        public static string DecodeUnicode(string input)
        {
            var FindUnicodeEscapes = new Regex(@"\\u[0-9a-fA-F]{4}");

            // Use Regex.Unescape to unescape the matched escape sequences.
            return FindUnicodeEscapes.Replace(input, m => Regex.Unescape(m.Value));
        }
    }
}
