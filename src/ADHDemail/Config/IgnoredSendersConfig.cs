using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADHDemail.Config
{
    internal class IgnoredSendersConfig : ConfigFile
    {
        private readonly string _ignoredSendersConfigPath;

        /// <summary>
        /// Initializes a new instance of the <see cref="IgnoredSendersConfig"/> class.
        /// </summary>
        public IgnoredSendersConfig()
        {
            _ignoredSendersConfigPath = GetIgnoredSendersConfigPath();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>
        /// 
        /// </returns>
        /// <exception cref="System.UnauthorizedAccessException">Thrown when one .</exception>
        private string GetIgnoredSendersConfigPath()
        {
            try
            {
                string fullPath = Path.Combine(base._appDataPath, "ADHDemail", "GmailOAuth.json");
                return File.Exists(fullPath) ? fullPath : string.Empty;
            }
            catch (UnauthorizedAccessException ex)
            {
                LogWriter.Write($"Could not get the config file path. {ex.GetType()}: \"{ex.Message}\"");
                return string.Empty;
            }
        }
    }
}
