using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace UtgKata.Lib
{
    public static class RegExHelper
    {
        public const string UkPostCodePattern = @"([Gg][Ii][Rr] 0[Aa]{2})|((([A-Za-z][0-9]{1,2})|(([A-Za-z][A-Ha-hJ-Yj-y][0-9]{1,2})|(([A-Za-z][0-9][A-Za-z])|([A-Za-z][A-Ha-hJ-Yj-y][0-9][A-Za-z]?))))\s?[0-9][A-Za-z]{2})";

        public static bool IsRegExValid(string pattern, string input)
        {
            var re = new Regex(pattern, RegexOptions.Compiled);

            return re.IsMatch(input);
        }
    }
}
