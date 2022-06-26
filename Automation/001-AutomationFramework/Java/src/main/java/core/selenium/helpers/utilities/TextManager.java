using System;
using System.Linq;
using System.Text.RegularExpressions;
using SeleniumCore.Enums;

namespace SeleniumCore.Helpers.Utilities
{
    public static class TextManager
    {
        public static string GetTextInRange(string text, string start, string end)
        {
            Func<string, string> escapeRegexChars = (value) =>
            {
                return value.Replace(".", @"\.")
                            .Replace("(", @"\(")
                            .Replace(")", @"\)")
                            .Replace("$", @"\$")
                            .Replace("+", @"\+")
                            .Replace("{s}", @"[\n\r\s]+");
            };

            start = escapeRegexChars(start);
            end = escapeRegexChars(end);

            string regexPattern = "(?s){0}(.*?){1}";
            string searchPattern = string.Format(regexPattern, start, end);

            MatchCollection matches = Regex.Matches(text, searchPattern, RegexOptions.IgnoreCase);

            if (matches.Count == 0)
            {
                return null;
            }

            return matches.Cast<Match>().Select(m => m.Value).FirstOrDefault();
        }

        public static string ExtractValue(string text, string searchString, bool useExactRegexPattern = false)
        {
            if (useExactRegexPattern)
            {
                return ExtractValueByRegexPattern(text, searchString);
            }

            return ExtractValue(text, searchString);
        }

        public static string ExtractValueByRegexPattern(string text, string searchString)
        {
            MatchCollection matches = Regex.Matches(text, searchString, RegexOptions.IgnoreCase);

            if (matches.Count == 0)
            {
                return null;
            }

            string value = matches.Cast<Match>().Select(m => m.Groups.OfType<Group>().Any(x => x.Name == "value") ? m.Groups["value"].Value : m.Value).FirstOrDefault();

            return value;
        }

        public static bool IsMatching(string text, string searchString)
        {
            return Regex.Matches(text, searchString, RegexOptions.IgnoreCase).Count > 0;
        }

        public static string ExtractValue(string text, string searchString)
        {
            string[] splitToken = new string[] { };
            string searchPattern = searchString;

            searchPattern = searchPattern
                .Replace(".", @"\.")
                .Replace("(", @"\(")
                .Replace(")", @"\)")
                .Replace("$", @"\$")
                .Replace("+", @"\+");

            if (searchPattern.Contains("|"))
            {
                splitToken = new string[] { "|" };
                searchPattern = searchPattern.Split('|')[0];
            }
            if (searchPattern.Contains("{vn}"))
            {
                splitToken = new string[] { "{vn}" };
                searchPattern = searchPattern.Replace("{vn}", @"[\s\S]+");
            }
            else if (searchPattern.Contains("{v}"))
            {
                splitToken = new string[] { "{v}" };
                searchPattern = searchPattern.Replace("{v}", @"(.*?)");
            }
            else if (searchPattern.Contains("{d}"))
            {
                splitToken = new string[] { "{d}" };
                searchPattern = searchPattern.Replace("{d}", @"[-+]?([0-9,]*\.[0-9]+|[0-9]+)");
            }

            searchPattern = searchPattern
                .Replace("{n}", @"[\r\n]+")
                .Replace("{a}", @"(.*?)")
                .Replace("{s}", @"[\n\r\s]+")
                .Replace("{d}", @"[,0-9]+(\.[0-9][0-9]?)?");

            MatchCollection matches = Regex.Matches(text, searchPattern, RegexOptions.IgnoreCase);

            if (matches.Count == 0)
            {
                return null;
            }

            string value = matches.Cast<Match>().Select(m => m.Value).FirstOrDefault();

            value = ReplaceText(TextReplacement.FromBeginning, value, searchString.Split(splitToken, StringSplitOptions.None)[0], string.Empty);
            value = ReplaceText(TextReplacement.FromEnding, value, searchString.Split(splitToken, StringSplitOptions.None)[1], string.Empty);
            value = Regex.Replace(value, @"\r\n?|\n", " ").Trim();

            return value;
        }

        public static string ReplaceText(TextReplacement mode, string source, string find, string replace)
        {
            if (find.Contains("{"))
            {
                find = find
                    .Replace(".", @"\.")
                    .Replace("(", @"\(")
                    .Replace(")", @"\)")
                    .Replace("$", @"\$")
                    .Replace("+", @"\+")
                    .Replace("{n}", @"[\r\n]")
                    .Replace("{a}", @"(.*?)")
                    .Replace("{s}", @"[\n\r\s]+")
                    .Replace("{l}", @"\S+(\w+)\W*$");

                source = Regex.Replace(source, find, string.Empty, RegexOptions.IgnoreCase);
            }

            int place = -1;
            if (mode == TextReplacement.FromBeginning)
            {
                place = source.IndexOf(find);
            }
            else if (mode == TextReplacement.FromEnding)
            {
                place = source.LastIndexOf(find);
            }

            if (place == -1)
            {
                return source;
            }

            return source.Remove(place, find.Length).Insert(place, replace);
        }

        public static string RemoveSpecialCharacters(string text, string pattern = null)
        {
            return pattern == null ? Regex.Replace(text ?? string.Empty, "[^0-9.,]", string.Empty)
                                   : Regex.Replace(text ?? string.Empty, string.Format("{0}", pattern), string.Empty);
        }
    }
}
