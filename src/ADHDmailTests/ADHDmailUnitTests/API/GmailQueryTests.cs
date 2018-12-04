using ADHDmail.API;
using ADHDmail.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ADHDmailTests.API
{
    public class GmailQueryTests
    {
        public static IEnumerable<object[]> GmailQueriesAndTheirStringValues =>
           new List<object[]>
           {
                new object[] 
                {
                    new GmailQuery(),
                    string.Empty
                },
                new object[] 
                {
                    new GmailQuery(new List<Filter>()),
                    string.Empty
                },
                new object[] 
                {
                    new GmailQuery(new List<Filter>()
                    {
                        new Filter(FilterOption.ContainsWord, "Test")
                    }),
                    "Test"
                },
                new object[] 
                {
                    new GmailQuery(new List<Filter>()
                    {
                        new Filter(FilterOption.HasAttachment),
                        new Filter(FilterOption.LargerThan, "1")
                    }),
                    "has:attachment larger:1"
                }
           };

        [Theory]
        [MemberData(nameof(GmailQueriesAndTheirStringValues))]
        public void ConstructorTest(GmailQuery gmailQuery, string expectedQueryString)
        {
            Assert.Equal(gmailQuery.ToString(), expectedQueryString);
        }
    }
}
