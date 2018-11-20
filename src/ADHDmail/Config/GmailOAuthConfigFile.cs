namespace ADHDmail.Config
{
    /// <summary>
    /// Represents the config file for Gmail's OAuth credentials.
    /// <para>This file should be encrypted to secure the user's credentials.
    /// </para>
    /// </summary>
    public class GmailOAuthConfigFile : ConfigFile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GmailOAuthConfigFile"/> class.
        /// </summary>
        public GmailOAuthConfigFile()
        {
            FullPath = GetFullPath("GmailOAuth.json"); 
        }
    }
}
