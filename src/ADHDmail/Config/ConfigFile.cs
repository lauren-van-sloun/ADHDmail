using System;
using System.IO;

namespace ADHDmail.Config
{
    /// <summary>
    /// The abstract base class for all config files. 
    /// </summary>
    public abstract class ConfigFile
    {
        /// <summary>
        /// The path and name of the <see cref="ConfigFile"/>.
        /// </summary>
        public string FullPath { get; set; }

        /// <summary>
        /// Determines whether the <see cref="ConfigFile"/> exists.
        /// </summary>
        public bool Exists => File.Exists(FullPath);

        /// <summary>
        /// Combines the <see cref="GlobalValues.AppDataPath"/>, <see cref="GlobalValues.ApplicationName"/>, 
        /// and <paramref name="fileName"/> to make the full file path.
        /// </summary>
        /// <param name="fileName">The name of the file.</param>
        public string GetFullPath(string fileName)
        {
            return Path.Combine(GlobalValues.AppDataPath, GlobalValues.ApplicationName, fileName);
        }

        /// <summary>
        /// Creates a <see cref="ConfigFile"/> file in the <see cref="FullPath"/> if it does not already exist.
        /// </summary>
        public void Create()
        {
            if (!Exists)
                File.Create(FullPath);
        }

        /// <summary>
        /// Encrypts a file so that only the account used to encrypt the file can decrypt it.
        /// </summary>
        public void Encrypt()
        {
            try
            {
                File.Encrypt(FullPath);
            }
            catch (Exception ex)
            {
                LogWriter.Write($"Could not encrypt {FullPath}. {ex.GetType()}: \"{ex.Message}\"");
            }
        }

        /// <summary>
        /// Decrypts a file that was encrypted by the current account using the Encrypt(String) method.
        /// </summary>
        public void Decrypt()
        {
            try
            {
                File.Decrypt(FullPath);
            }
            catch (Exception ex)
            {
                LogWriter.Write($"Could not decrypt {FullPath}. {ex.GetType()}: \"{ex.Message}\"");
            }
        }
    }
}
