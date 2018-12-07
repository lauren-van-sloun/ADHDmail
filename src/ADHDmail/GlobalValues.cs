using System;
using System.IO;
using System.Reflection;

namespace ADHDmail
{
    /// <summary>
    /// A static class that holds commonly used or shared variables such as folder paths and names.
    /// </summary>
    public static class GlobalValues
    {
        /// <summary>
        /// The name of the application for use in creating filepaths.
        /// </summary>
        public const string ApplicationName = "ADHDmail";

        /// <summary>
        /// The path to the directory that serves as a common repository for application-specific 
        /// data that is used by the current, non-roaming user.
        /// </summary>
        public static string LocalAppDataPath { get; }

        /// <summary>
        /// The path to the directory that serves as a common repository for application-specific
        /// data for the current roaming user.
        /// </summary>
        public static string AppDataPath { get; }

        static GlobalValues()
        {
            LocalAppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            AppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        }
    }
}
