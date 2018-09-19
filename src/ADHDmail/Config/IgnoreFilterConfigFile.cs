using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADHDmail.Config
{
    /// <summary>
    /// Represents the config file for the ignoring specific fields and values of a message.
    /// </summary>
    public class IgnoreFiltersConfigFile : ConfigFile
    {
        private readonly string fullPath;
        private readonly bool _exists;

        /// <summary>
        /// Initializes a new instance of the <see cref="IgnoreFiltersConfigFile"/> class.
        /// </summary>
        public IgnoreFiltersConfigFile()
        {
            fullPath = GetIgnoredSendersConfigPath();
            _exists = Exists(fullPath);
        }

        private string GetIgnoredSendersConfigPath()
        {
            return Path.Combine(base._appDataPath, "ADHDemail", "IgnoreFilters.json");
        }

        // Depending on how many times I need to call this method, I might be able to get rid of it
        // and inline the File.Exists() call assuming the try-catch won't clutter up the calling code
        private bool Exists(string path)
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
        /// Creates an <see cref="IgnoreFiltersConfigFile"/> file in the user's AppData folder if it does not already exist.
        /// </summary>
        public void Create()
        {
            if (!this._exists)
                File.Create(fullPath);
        }

        /// <summary>
        /// Opens a file, appends the specified <see cref="Filter"/> to the file, and then closes the file. If the file does 
        /// not exist, this method creates a file, writes the specified <see cref="Filter"/> to the file, then closes the file.
        /// </summary>
        /// <param name="filter">Represents a filter to apply to a message based on the part of the message to filter and the value to filter by.</param>
        /// <exception cref="IOException">An I/O error occurred while opening the file.</exception>
        /// <exception cref="UnauthorizedAccessException">Thrown when given a read-only filepath, when the operation is not supported on the current platform, 
        /// or the caller does not have the required permission.</exception>
        /// <exception cref="System.Security.SecurityException">Thrown when the caller does not have the required permission.</exception>
        public void Append(Filter filter)
        {
            File.AppendAllText(fullPath, JsonConvert.SerializeObject(filter));
        }

        /// <summary>
        /// Opens a file, appends the specified <see cref="Filter"/>s to the file, and then closes the file. If the file does 
        /// not exist, this method creates a file, writes the specified <see cref="Filter"/>s to the file, then closes the file.
        /// </summary>
        /// <param name="filters">Represents filters to apply to a message based on the part of the message to filter and the value to filter by.</param>
        /// <exception cref="IOException">An I/O error occurred while opening the file.</exception>
        /// <exception cref="UnauthorizedAccessException">Thrown when given a read-only filepath, when the operation is not supported on the current platform, 
        /// or the caller does not have the required permission.</exception>
        /// <exception cref="System.Security.SecurityException">Thrown when the caller does not have the required permission.</exception>
        public void Append(List<Filter> filters)
        {
            filters.ForEach(f => Append(f));
        }

        /// <summary>
        /// Clears the contents of the file.
        /// </summary>
        public void Clear()
        {
            File.WriteAllText(fullPath, string.Empty);
        }

        /// <summary>
        /// Reads the file and returns its contents.
        /// </summary>
        /// <returns>A list of <see cref="Filter"/> objects that were saved in the file.</returns>
        public List<Filter> LoadJson()
        {
            using (StreamReader reader = File.OpenText(fullPath))
            {
                string json = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<List<Filter>>(json);
            }
        }
    }
}
