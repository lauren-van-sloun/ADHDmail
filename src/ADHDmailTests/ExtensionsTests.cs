using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ADHDmailTests
{
    [TestFixture]
    public class ExtensionsTests
    {
        [Test]
        public void IsValidPath_Test_ValidInput()
        {

        }

        //public static bool IsValidPath(this string path)
        //{
        //    const int MaxPath = 260;
        //    return (!path.ContainsInvalidPathChar() && path.Length <= MaxPath);
        //}

        [Test]
        public void ContainsInvalidPathChar_Test_ValidInput()
        {

        }

        //private static bool ContainsInvalidPathChar(this string text)
        //{
        //    return text.IndexOfAny(_invalidPathChars) >= 0;
        //}

        [Test]
        public void ToDateTime_Test_ValidInput()
        {

        }
        //public static DateTime ToDateTime(this string date)
        //{
        //    var result = new DateTime();
        //    if (!string.IsNullOrWhiteSpace(date))
        //        DateTime.TryParse(date, out result);
        //    return result;
        //}
    }
}
