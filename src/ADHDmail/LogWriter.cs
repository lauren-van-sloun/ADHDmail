using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace ADHDmail
{
    /// <summary>
    /// A simple log file writer.
    /// <para>
    /// The default log file location is in the LocalAppData folder.
    /// </para>
    /// </summary>
    public static class LogWriter
    {
        private static string _logPath;

        static LogWriter()
        {
            SetLogPath();
        }

        private static void SetLogPath()
        {
            var fileName = "ErrorLog.txt";
            _logPath = Path.Combine(GlobalValues.LocalAppDataPath, GlobalValues.ApplicationName, fileName);
        }

        /// <summary>
        /// Appends text to an existing log file, or to a new one if the file does not exist.
        /// </summary>
        /// <param name="message">The message to record in the log.</param>
        /// <param name="callerName">An optional parameter that automatically obtains the method or property 
        /// name of the caller to the method.</param>
        /// <param name="sourceFilePath">An optional parameter that automatically obtains the full path of the 
        /// source file that contains the caller. This is the file path at compile time.</param>
        /// <param name="sourceLineNumber">An optional parameter that automatically obtains the line number in 
        /// the source file at which the method is called.</param>
        public static void Write(string message, 
                                 [CallerMemberName] string callerName = "", 
                                 [CallerFilePath] string sourceFilePath = "",
                                 [CallerLineNumber] int sourceLineNumber = 0)
        {
            try
            {
                CreateDirectoryIfItDoesntExist();

                using (var writer = File.AppendText(_logPath))
                {
                    writer.Write("\r\nLog Entry : ");
                    writer.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
                    writer.WriteLine("  :");
                    writer.WriteLine($"Message: {message}");
                    writer.WriteLine($"CallerMemberName: {callerName}.");
                    writer.WriteLine($"Source file path: {sourceFilePath}");
                    writer.WriteLine($"Source line number: {sourceLineNumber}");
                    writer.WriteLine("");
                    writer.WriteLine("-------------------------------");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not create or add text to error log in {_logPath}. {ex.GetType()}: \"{ex.Message}\"");
            }
        }

        /// <summary>
        /// Removes the last entry in the log.
        /// </summary>
        public static void RemoveLastEntry()
        {
            try
            {
                var logEntry = new Regex("[^-]+-+\\r*\\n*$");
                var textFromFile = File.ReadAllText(_logPath);
                var matches = logEntry.Matches(textFromFile);
                if (matches.Count == 0)
                    return;
                var lastEntry = matches[matches.Count - 1];
                var indexOfLastEntry = textFromFile.LastIndexOf(lastEntry.Value);
                var updatedFileContents = textFromFile.Remove(indexOfLastEntry, lastEntry.Length)
                                                      .Insert(indexOfLastEntry, string.Empty);
                File.WriteAllText(_logPath, updatedFileContents);
            }
            catch (Exception ex)
            {
                var message = $"Could not remove last entry from error log in {_logPath}. {ex.GetType()}: \"{ex.Message}\"";
                Write(message);
                throw new Exception(message);
            }
        }

        private static void CreateDirectoryIfItDoesntExist()
        {
            var fileInfo = new FileInfo(_logPath);
            fileInfo.Directory.Create();
        }
    }
}
