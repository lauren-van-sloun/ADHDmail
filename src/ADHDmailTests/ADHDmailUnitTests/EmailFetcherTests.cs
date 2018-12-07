using ADHDmail;
using ADHDmailTests.API;
using ADHDmailTests.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ADHDmailTests
{
    public class EmailFetcherTests
    {
        [Fact]
        public void SetTimerTest()
        {
            var api = new MockApi();
            var emailFetcher = new EmailFetcher(api, new MockQueryScheduleConfigFile());
            emailFetcher.SetTimer(callback: CheckStatus);
            emailFetcher.Start();
            Thread.Sleep(6000);
            emailFetcher.Stop();
            Assert.True(_invokeCount > 0);
        }

        private int _invokeCount = 0;

        public bool CheckStatus(List<Email> emails)
        {
            _invokeCount++;
            return _invokeCount < 1;
        }
        // SetTimer
        // Start
        // Stop
        // ChangeInterval
    }
}
