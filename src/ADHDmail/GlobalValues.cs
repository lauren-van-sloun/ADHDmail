using System;
using System.IO;
using System.Reflection;

namespace ADHDmail
{
    internal static class GlobalValues
    {
        public static string ApplicationName { get; }
        public static string LocalAppDataPath { get; }
        public static string AppDataPath { get; }

        static GlobalValues()
        {
            var fullName = Assembly.GetEntryAssembly().Location;
            ApplicationName = Path.GetFileNameWithoutExtension(fullName);
            LocalAppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            AppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        }
    }
}
