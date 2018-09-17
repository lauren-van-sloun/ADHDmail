using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace ADHDemail
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
        /// <exception cref="ArgumentException">Thrown when given a string with one or more invalid path characters.</exception>
        public static string LogPath
        {
            get { return _logPath; }
            set
            {
                // create other extension methods that verify path and make it into one call here
                if (!value.IsValidPath())
                    throw new ArgumentException("The LogPath provided contains an invalid path character.");
                _logPath = value;
            }
        }

        // moving to Extensions
        //private static bool IsValidPath(string path)
        //{
        //    const int MaxPath = 260;
        //    return (path.ContainsInvalidPathChar() || path.Length > MaxPath) ? false : true;
        //}

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
