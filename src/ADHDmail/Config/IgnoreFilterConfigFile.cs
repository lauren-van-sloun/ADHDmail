using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace ADHDmail.Config
{
    /// <summary>
    /// Represents the config file for the ignoring specific fields and values of a message.
    /// </summary>
    public class IgnoreFiltersConfigFile : ConfigFile
    {
        /// <summary>
        /// The path and name of the <see cref="IgnoreFiltersConfigFile"/>.
        /// </summary>
        public override string FullPath { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="IgnoreFiltersConfigFile"/> class.
        /// </summary>
        public IgnoreFiltersConfigFile()
        {
            FullPath = GetFullPath("IgnoreFilters.json");
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
            File.AppendAllText(FullPath, JsonConvert.SerializeObject(filter));
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
            File.WriteAllText(FullPath, string.Empty);
        }

        /// <summary>
        /// Reads the file and returns its contents.
        /// </summary>
        /// <returns>A list of <see cref="Filter"/> objects that were saved in the file. 
        /// Returns null if the <see cref="IgnoreFiltersConfigFile"/> does not exist.</returns>
        public List<Filter> LoadJson()
        {
            if (!Exists)
                return null;

            using (var reader = File.OpenText(FullPath))
            {
                var json = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<List<Filter>>(json);
            }
        }
    }
}
