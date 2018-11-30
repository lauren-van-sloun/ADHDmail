using ADHDmail;
using ADHDmail.API;
using ADHDmail.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ADHDmailIntegrationTests.API
{
    public class GmailApiTests
    {
        private readonly IEmailApi _gmailApi = new GmailApi();

        // Figure out a cleaner way to represent test data
        public static IEnumerable<object[]> Queries =>
            new List<object[]>
            {
                new object[] 
                {
                    new GmailQuery(
                        new List<Filter>
                        {
                            new Filter(FilterOption.LargerThan, "1"),
                        }),
                    1
                },
                new object[] 
                {
                    new GmailQuery(),
                    1
                },
                new object[]
                {
                    new GmailQuery(
                        new List<Filter>
                        {
                            new Filter(FilterOption.Unread),
                            new Filter(FilterOption.Read)
                        }),
                    0
                }
            };

        // This test will currently fail if there are no messages in the Gmail inbox.
        [Theory]
        [MemberData(nameof(Queries))]
        public void GetEmailsTest(GmailQuery input, int numOfRetrievedEmails)
        {
            Assert.True(_gmailApi.GetEmails(input).Count >= numOfRetrievedEmails);
        }


        // This test will currently fail if there are no messages in the Gmail inbox.
        [Fact]
        public void GetEmailTest()
        {
            var messageIds = new List<string>();
            _gmailApi.GetEmails(new GmailQuery()).ForEach(e => messageIds.Add(e.Id));
            Assert.True(messageIds.Count > 0 && _gmailApi.GetEmail(messageIds[0]) != null);           
        }
    }
}
