using System;
using System.IO;

namespace ADHDmail.Config
{
    /// <summary>
    /// The abstract base class for all config files. 
    /// </summary>
    public abstract class ConfigFile
    {
        private readonly string _appDataFolderPath;
        private const string _appNameFolder = "ADHDmail";

        /// <summary>
        /// The path and name of the <see cref="ConfigFile"/>.
        /// </summary>
        public abstract string FullPath { get; set; }

        private bool _exists { get; set; }
        /// <summary>
        /// Determines whether the specified file exists.
        /// </summary>
        public bool Exists
        {
            get => _exists;
            set => _exists = FileExists(FullPath);
        }

        internal ConfigFile()
        {
            _appDataFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        }

        private bool FileExists(string path)
        {
            try
            {
                return File.Exists(path);
            }
            catch (Exception ex)
            {
                LogWriter.Write($"Could not check if the path {path} exists. {ex.GetType()}: \"{ex.Message}\"");
                throw;
            }
        }

        /// <summary>
        /// Combines the <see cref="_appDataFolderPath"/>, <see cref="_appNameFolder"/>, and <paramref name="fileName"/> 
        /// to make the full file path.
        /// </summary>
        /// <param name="fileName">The name of the file.</param>
        public string GetFullPath(string fileName)
        {
            return Path.Combine(_appDataFolderPath, _appNameFolder, fileName);
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
