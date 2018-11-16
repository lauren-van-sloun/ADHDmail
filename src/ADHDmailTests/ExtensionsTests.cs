//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Xunit;
//using ADHDmail;

//namespace ADHDmailTests
//{
//    public class ExtensionsTests
//    {
//        [Theory]
//        [InlineData(@"c:\temp\MyTest.txt")]
//        public void IsValidPath_Test_ValidInput(string path)
//        {
//            Assert.True(path.IsValidPath());
//        }

//        [Theory]
//        [InlineData("")]
//        [InlineData(">")]
//        [InlineData("|")]
//        [InlineData("\"")]
//        [InlineData("This is a string that exceeds 260 characters. This is a string that " +
//            "exceeds 260 characters. This is a string that exceeds 260 characters. This is " +
//            "a string that exceeds 260 characters. This is a string that exceeds 260 characters. " +
//            "This is a string that exceeds 260 characters.")]
//        public void IsValidPath_Test_InvalidInput(string path)
//        {
//            Assert.False(path.IsValidPath());
//        }

//        private const string _gmailDateTimeFormatExample = "Tue, 13 Nov 2018 22:01:48 + 0000(UTC)";
//        public static IEnumerable<object[]> ValidDateTimes =>
//            new List<object[]>
//            {
//                new object[] { _gmailDateTimeFormatExample, new DateTime(2018, 11, 13, 22, 01, 48) },
//            };

//        public static IEnumerable<object[]> InvalidDateTimes =>
//            new List<object[]>
//            {
//                new object[] { "Invalid date time", DateTime.MinValue },
//                new object[] { "", DateTime.MinValue },
//                new object[] { "     ", DateTime.MinValue }
//            };

//        [Theory]
//        [MemberData(nameof(ValidDateTimes))]
//        [MemberData(nameof(InvalidDateTimes))]
//        public void ToDateTime_Test_ValidInput(string input, DateTime expectedOutput)
//        {
//            Assert.Equal(expectedOutput, input.ToDateTime());
//        }
//    }
//}
