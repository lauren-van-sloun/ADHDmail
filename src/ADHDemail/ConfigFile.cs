using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADHDemail
{
    public class ConfigFile
    {
        public readonly string ConfigPath;

        public ConfigFile()
        {
            try
            {
                string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string fullPath = Path.Combine(appDataPath, "ADHDemail", "GmailOAuth.json");
                ConfigPath = File.Exists(fullPath) ? fullPath : string.Empty;
            }
            catch (UnauthorizedAccessException)
            {

            }
        }

        // Encrypts a file so that only the account used to encrypt the file can decrypt it.
        public static void Encrypt(string filepath)
        {
            try
            {
                File.Encrypt(filepath);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void Decrypt(string filepath)
        {
            File.Decrypt(filepath);
        }
    }
}
