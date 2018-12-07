using ADHDmail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ADHDmailIntegrationTests.Misc
{
    public class LogWriterTests
    {
        // clean up this test and add more
        [Fact]
        public void WriteTest()
        {
            var logPath = Path.Combine(GlobalValues.LocalAppDataPath, GlobalValues.ApplicationName, "ErrorLog.txt");
            var fileSizeBefore = new FileInfo(logPath).Length;
            LogWriter.Write("This is a test entry.");
            var fileSizeAfter = new FileInfo(logPath).Length;
            Assert.True(fileSizeBefore < fileSizeAfter && File.ReadAllText(logPath).Contains("This is a test entry"));
            LogWriter.RemoveLastEntry();
        }
    }
}
