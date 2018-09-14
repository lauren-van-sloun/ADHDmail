using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace ADHDemail
{
    public static class LogWriter
    {
        private static readonly string _logPath;

        static LogWriter()
        {
            _logPath = GetLogPath();
        }

        private static string GetLogPath()
        {
            string localAppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string fileName = "ADHDemailLog.txt";
            return Path.Combine(localAppDataPath, fileName);
        }

        /// <summary>
        /// Summary here including reference to <paramref name="message"/>, <paramref name="callerName"/>, and other params.
        /// </summary>
        /// <exception cref="System.OverflowException">Thrown when one parameter is max 
        /// and the other is greater than zero.</exception>
        /// See <see cref="Math.Add(int, int)"/> to add integers.
        /// <param name="message">A double precision number.</param>
        /// <param name="callerName">A double precision number.</param>
        public static void Write(string message, 
                                 [CallerMemberName] string callerName = "", 
                                 [CallerFilePath] string sourceFilePath = "",
                                 [CallerLineNumber] int sourceLineNumber = 0)
        {
            try
            {
                using (StreamWriter writer = File.AppendText(_logPath))
                {
                    writer.Write("\r\nLog Entry : ");
                    writer.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
                    writer.WriteLine("  :");
                    writer.WriteLine($"Message: {message}");
                    writer.WriteLine($"CallerMemberName: {callerName}.");
                    writer.WriteLine($"Source file path: {sourceFilePath}");
                    writer.WriteLine($"Source line number: {sourceLineNumber}");
                    writer.WriteLine($"");
                    writer.WriteLine("-------------------------------");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not create or add text to error log in {_logPath}. {ex.GetType()}: \"{ex.Message}\"");
            }
        }
    }
}
