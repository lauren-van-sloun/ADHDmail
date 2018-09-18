using System;
using System.IO;

namespace ADHDmail.Config
{
    /// <summary>
    /// Represents the config file for Gmail's OAuth credentials.
    /// <para>This file should be encrypted to secure the user's credentials.
    /// </para>
    /// </summary>
    public class GmailOAuthConfig : ConfigFile
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
        /// Retrieves the file's location in the AppData folder.
        /// </summary>
        /// <remarks>
        /// If the file does not exist, 
        /// </remarks>
        /// <returns>
        /// If the filepath exists, returns the full path. Otherwise returns an emtpy string.
        /// </returns>
        /// <exception cref="FileNotFoundException">Thrown when one .
        /// </exception>
        /// <exception cref="UnauthorizedAccessException">Thrown when one .
        /// </exception>

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
