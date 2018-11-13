using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace ADHDmail
{
    /// <summary>
    /// A simple log file writer.
    /// <para>
    /// The default log file location is the LocalAppData folder.
    /// </para>
    /// </summary>
    public static class LogWriter
    {
        private static string _logPath;
        /// <summary>
        /// The full path to the log file.
        /// </summary>
        /// <exception 
        /// cref="ArgumentException">Thrown when given a string with one or more invalid path characters.
        /// </exception>
        public static string LogPath
        {
            get => _logPath;
            set
            {
                if (!value.IsValidPath())
                    throw new ArgumentException("The LogPath provided is not a valid filepath.");
                _logPath = value;
            }
        }

        static LogWriter()
        {
            _logPath = GetLogPath();
        }

        private static string GetLogPath()
        {
            var localAppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var fileName = "ADHDemailLog.txt";
            return Path.Combine(localAppDataPath, fileName);
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
    }
}
