
using System.Text.RegularExpressions;

namespace SimpleJsonParser
{
    static class StringUtils
    {
        private const string jsonWhitespacePattern = @"\n|\t|\r| ";

        private static Regex JsonWhitespaceRegex
        { get; set; }

        static StringUtils()
        {
            JsonWhitespaceRegex = new Regex(jsonWhitespacePattern);
        }

        public static string StripLeadingJsonWhitespace(
            string jsonFragment
        ) {
            while (
                (jsonFragment.Length > 0)
                && (JsonWhitespaceRegex.IsMatch(
                    GetFirstCharacter(
                        jsonFragment
                    )
                ))
            ) {
                jsonFragment = StripFirstCharacter(
                    jsonFragment
                );
            }
            return jsonFragment;
        }

        public static string StripTrailingJsonWhitespace(
            string jsonFragment
        ) {
            while (
                (jsonFragment.Length > 0)
                && (JsonWhitespaceRegex.IsMatch(
                    GetLastCharacter(
                        jsonFragment
                    )
                ))
            ) {
                jsonFragment = StripLastCharacter(
                    jsonFragment
                );
            }
            return jsonFragment;
        }

        public static string GetFirstCharacter(
            string toGet
        ) {
            return toGet.Substring(0, 1);
        }

        public static string StripFirstCharacter(
            string toStrip
        ) {
            return toStrip.Substring(1);
        }

        static string GetLastCharacter(
            string toGet
        ) {
            return toGet.Substring(
                toGet.Length - 1
            );
        }

        static string StripLastCharacter(
            string toStrip
        ) {
            return toStrip.Substring(
                0, 
                toStrip.Length - 1
            );
        }
    }
}