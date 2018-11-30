using ADHDmail.Config;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace ADHDmail
{
    /// <summary>
    /// Holds extension methods for added functionality.
    /// </summary>
    public static class Extensions
    {
        private static readonly char[] InvalidPathChars = Path.GetInvalidPathChars();

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
            return text.IndexOfAny(InvalidPathChars) >= 0;
        }

        /// <summary>
        /// Converts a specified value to a <see cref="DateTime"/> value.
        /// </summary>
        /// <param name="date">The date to be parsed.</param>
        /// <returns>Returns the parsed date if able to be parsed, otherwise DateTime.MinValue.</returns>
        public static DateTime ToDateTime(this string date)
        {
            if (string.IsNullOrWhiteSpace(date))
                return new DateTime();

            DateTime.TryParse(date, out var result);

            bool parsingFailed = result == DateTime.MinValue;
            if (parsingFailed)
            {
                var googleDateRegex = new Regex("[^+]*");
                DateTime.TryParse(googleDateRegex.Match(date).Value, out result);
            }

            return result;
        }

        /// <summary>
        /// Deserializes a JSON string into a List of <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of object to deserialize to.</typeparam>
        /// <param name="serializedJSONString">The serialized JSON string to parse.</param>
        /// <returns>Returns a deserialized List of <typeparamref name="T"/>.</returns>
        public static List<T> Deserialize<T>(this string serializedJSONString)
        {
            return JsonConvert.DeserializeObject<List<T>>(serializedJSONString);
        }

        /// <summary>
        /// Determines if the file specified is empty.
        /// </summary>
        /// <param name="path">The full path of the file.</param>
        /// <returns>Returns true if the file is empty, otherwise false.</returns>
        public static bool IsEmptyFile(this string path)
        {
            return new FileInfo(path).Length == 0;
        }

        /// <summary>
        /// Reads a file's attributes to determine whether it is encrypted.
        /// </summary>
        /// <param name="path">The full path of the file.</param>
        /// <returns>Returns true if the file is encrypted, otherwise false.</returns>
        public static bool IsEncrypted(this string path)
        {
            var fileAttributes = File.GetAttributes(path);
            return (fileAttributes & FileAttributes.Encrypted) == FileAttributes.Encrypted;
        }
    }
}