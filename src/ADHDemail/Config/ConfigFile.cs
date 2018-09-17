using System;
using System.IO;

namespace ADHDemail.Config
{
    /// <summary>
    /// The abstract base class for all config files. 
    /// <para>Provides default implementation of file encryption and decryption 
    /// as well as common config file locations.
    /// </para>
    /// </summary>
    public abstract class ConfigFile
    {
        /// <summary>
        /// Represents the path for the AppData folder.
        /// </summary>
        public readonly string _appDataPath;

        internal ConfigFile()
        {
            _appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        }

        /// <summary>
        /// Encrypts a file so that only the account used to encrypt the file can decrypt it.
        /// </summary>
        /// <param name="path"> A path that describes a file to encrypt.</param>
        public void Encrypt(string path)
        {
            try
            {
                File.Encrypt(path);
            }
            catch (Exception ex)
            {
                LogWriter.Write($"Could not encrypt {path}. {ex.GetType()}: \"{ex.Message}\"");
            }
        }

        /// <summary>
        /// Decrypts a file that was encrypted by the current account using the Encrypt(String) method.
        /// </summary>
        /// <param name="path"> A path that describes a file to decrypt.</param>
        public void Decrypt(string path)
        {
            try
            {
                File.Decrypt(path);
            }
            catch (Exception ex)
            {
                LogWriter.Write($"Could not decrypt {path}. {ex.GetType()}: \"{ex.Message}\"");
            }
        }
    }
}
