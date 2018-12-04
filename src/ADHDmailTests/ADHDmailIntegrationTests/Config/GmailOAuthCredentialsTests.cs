using ADHDmail;
using ADHDmail.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ADHDmailIntegrationTests.Config
{
    public class GmailOAuthCredentialsTests
    {
        public static IEnumerable<object[]> GmailOAuthCredentialsAndTheirExpectedPath =>
          new List<object[]>
          {
                new object[]
                {
                    new GmailOAuthCredentials(),
                    Path.Combine(GlobalValues.AppDataPath, GlobalValues.ApplicationName, "GmailOAuth.json")
                },
                new object[]
                {
                    new GmailOAuthCredentials("TestFileName.json"),
                    Path.Combine(GlobalValues.AppDataPath, GlobalValues.ApplicationName, "TestFileName.json")
                },
          };

        [Theory]
        [MemberData(nameof(GmailOAuthCredentialsAndTheirExpectedPath))]
        public void ConstructorTest(GmailOAuthCredentials gmailOAuthCredentials, string expectedPath)
        {
            Assert.Equal(gmailOAuthCredentials.FullPath, expectedPath);
        }
    }
}
