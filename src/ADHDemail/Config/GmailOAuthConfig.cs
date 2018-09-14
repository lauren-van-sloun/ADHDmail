using System;
using System.IO;

namespace ADHDemail.Config
{
    internal class GmailOAuthConfig : ConfigFile
    {
        private readonly string _gmailOAuthConfigPath;

        /// <summary>
        /// Initializes a new instance of the <see cref="GmailOAuthConfig"/> class.
        /// </summary>
        public GmailOAuthConfig()
        {
            _gmailOAuthConfigPath = GetGmailOAuthConfigPath();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>
        /// 
        /// </returns>
        /// <exception cref="System.UnauthorizedAccessException">Thrown when one .</exception>
        private string GetGmailOAuthConfigPath()
        {
            try
            {
                string fullPath = Path.Combine(base._appDataPath, "ADHDemail", "GmailOAuth.json");
                return File.Exists(fullPath) ? fullPath : string.Empty;
            }
            catch (UnauthorizedAccessException ex)
            {
                LogWriter.Write($"Could not get the Gmail OAuth config file path. {ex.GetType()}: \"{ex.Message}\"");
                return string.Empty;
            }
        }
    }
}
