using System;
using System.IO;

namespace ADHDmail.Config
{
    /// <summary>
    /// Represents the config file for Gmail's OAuth credentials.
    /// <para>This file should be encrypted to secure the user's credentials.
    /// </para>
    /// </summary>
    public class GmailOAuthConfigFile : ConfigFile
    {
        private readonly string fullPath;

        /// <summary>
        /// Initializes a new instance of the <see cref="GmailOAuthConfigFile"/> class.
        /// </summary>
        public GmailOAuthConfigFile()
        {
            fullPath = GetGmailOAuthConfigPath();
        }

        private string GetGmailOAuthConfigPath()
        {
            try
            {
                string fullPath = Path.Combine(base._appDataPath, "ADHDemail", "GmailOAuth.json");
                if (!File.Exists(fullPath)) throw new FileNotFoundException();
                return fullPath;
            }
            catch(FileNotFoundException ex)
            {
                // May change this behavior (hence the duplication) - tbd
                LogWriter.Write($"Could not get the Gmail OAuth config file path. {ex.GetType()}: \"{ex.Message}\"");
                return string.Empty;
            }
            catch (UnauthorizedAccessException ex)
            {
                LogWriter.Write($"Could not get the Gmail OAuth config file path. {ex.GetType()}: \"{ex.Message}\"");
                return string.Empty;
            }
        }
    }
}
