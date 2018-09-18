using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADHDmail
{
    public static class Extensions
    {
        /// <summary>
        /// Determines whether a string is a valid path based on the length and character content.
        /// </summary>
        /// <param name="path">The path to be checked.</param>
        /// <returns>Returns true if the string is a valid path, false if not.</returns>
        public static bool IsValidPath(this string path)
        {
            const int MaxPath = 260;
            return (path.ContainsInvalidPathChar() || path.Length > MaxPath) ? false : true;
        }

        private static bool ContainsInvalidPathChar(this string text)
        {
            return text.IndexOfAny(_invalidPathChars) >= 0;
        }

        private static readonly char[] _invalidPathChars = Path.GetInvalidPathChars();
    }
}
