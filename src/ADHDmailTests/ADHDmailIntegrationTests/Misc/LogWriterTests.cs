using ADHDmail;
using System;
using System.IO;
using Xunit;

namespace ADHDmailIntegrationTests.Misc
{
    public class LogWriterTests
    {
        [Fact]
        public void WriteTest()
        {
            try
            {
                var logPath = Path.Combine(GlobalValues.LocalAppDataPath, GlobalValues.ApplicationName, "ErrorLog.txt");
                var fileSizeBefore = new FileInfo(logPath).Length;
                var testMessage = "This is a test entry. " + DateTime.Now;

                LogWriter.Write(testMessage);

                var fileSizeAfter = new FileInfo(logPath).Length;
                Assert.True(fileSizeBefore < fileSizeAfter && File.ReadAllText(logPath).Contains(testMessage));
            }
            finally
            {
                LogWriter.RemoveLastEntry();
            }
        }
    }
}
