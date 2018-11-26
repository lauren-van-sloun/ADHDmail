using System;
using System.IO;
using System.Reflection;

namespace ADHDmail
{
    internal static class GlobalValues
    {
        public const string ApplicationName = "ADHDmail";
        public static string LocalAppDataPath { get; }
        public static string AppDataPath { get; }

        static GlobalValues()
        {
            LocalAppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            AppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        }
    }
}
