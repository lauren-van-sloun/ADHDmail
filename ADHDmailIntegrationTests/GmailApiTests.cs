using ADHDmail;
using ADHDmail.API;
using ADHDmail.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ADHDmailIntegrationTests
{
    public class GmailApiTests
    {
        public readonly IEmailApi GmailApi = new GmailApi();

        public static IEnumerable<object[]> Queries =>
            new List<object[]>
            {
                new object[] { new GmailQuery(new List<Filter>
                {
                    new Filter(FilterOption.Unread),
                    new Filter(FilterOption.LargerThan, "1"),
                    new Filter(FilterOption.From, "GitHub")
                }), 1 },
                new object[] { new GmailQuery() , 10 }
            };

        [Theory]
        [MemberData(nameof(Queries))]
        public void GetEmailsTest(GmailQuery input, int numOfRetrievedEmails)
        {
            Assert.True(GmailApi.GetEmails(input).Count >= numOfRetrievedEmails);
        }

        public static IEnumerable<object[]> Messages =>
            new List<object[]>
                {
                    new object[] { "message ID goes here", new Email()
                }


        public static List<string> messageIds

        // Have this use messageIds as an input. They will be received from GmailApi.GetEmails() 
        [Theory]
        [MemberData(nameof(Messages))]
        public void GetEmailTest()
        {

        }
    }
}
