// <copyright file="RegExHelper.cs" company="ajhudson">
// Copyright (c) ajhudson. All rights reserved.
// </copyright>

namespace UtgKata.Lib
{
    using System.Text.RegularExpressions;

    /// <summary>
    /// Regular expression helper containing convenience methods.
    /// </summary>
    public static class RegExHelper
    {
        /// <summary>
        /// The uk post code pattern.
        /// </summary>
        public const string UkPostCodePattern = @"([Gg][Ii][Rr] 0[Aa]{2})|((([A-Za-z][0-9]{1,2})|(([A-Za-z][A-Ha-hJ-Yj-y][0-9]{1,2})|(([A-Za-z][0-9][A-Za-z])|([A-Za-z][A-Ha-hJ-Yj-y][0-9][A-Za-z]?))))\s?[0-9][A-Za-z]{2})";

        /// <summary>
        /// Determines whether [is reg ex valid] [the specified pattern].
        /// </summary>
        /// <param name="pattern">The pattern.</param>
        /// <param name="input">The input.</param>
        /// <returns>
        ///   <c>true</c> if [is reg ex valid] [the specified pattern]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsRegExValid(string pattern, string input)
        {
            var re = new Regex(pattern, RegexOptions.Compiled);

            return re.IsMatch(input);
        }
    }
}
