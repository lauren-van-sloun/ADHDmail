using System;
using System.IO;
using System.Text.RegularExpressions;

namespace ADHDmail
{
    /// <summary>
    /// Holds extension methods for added functionality.
    /// </summary>
    public static class Extensions
    {
        private static readonly char[] _invalidPathChars = Path.GetInvalidPathChars();

        /// <summary>
        /// Determines whether a string is a valid path based on the length and character content.
        /// </summary>
        /// <param name="path">The path to be checked.</param>
        /// <returns>Returns true if the string is a valid path, false if not.</returns>
        public static bool IsValidPath(this string path)
        {
            const int MaxPath = 260;
            return (!path.ContainsInvalidPathChar() 
                && path.Length <= MaxPath
                && !string.IsNullOrWhiteSpace(path));
        }

        private static bool ContainsInvalidPathChar(this string text)
        {
            return text.IndexOfAny(_invalidPathChars) >= 0;
        }

        /// <summary>
        /// Converts a specified value to a <see cref="DateTime"/> value.
        /// </summary>
        /// <param name="date">The date to be parsed.</param>
        /// <returns>Returns the parsed date if able to be parsed, otherwise DateTime.MinValue.</returns>
        public static DateTime ToDateTime(this string date)
        {
            var result = new DateTime();
            if (!string.IsNullOrWhiteSpace(date))
                DateTime.TryParse(date, out result);

            if (result == DateTime.MinValue)
            {
                var googleDateRegex = new Regex("[^+]*");
                DateTime.TryParse(googleDateRegex.Match(date).Value, out result);
            }

            return result;
        }
    }
}