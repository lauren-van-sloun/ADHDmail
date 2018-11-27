using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;

namespace ADHDmail.Config
{
    /// <summary>
    /// Represents the config file for the ignoring specific fields and values of a message.
    /// </summary>
    public class IgnoreFiltersConfigFile : ConfigFile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IgnoreFiltersConfigFile"/> class.
        /// </summary>
        public IgnoreFiltersConfigFile()
        {
            FullPath = GetFullPath("IgnoreFilters.json");
        }

        /// <summary>
        /// Determines whether the file contains a specified <see cref="Filter"/>.
        /// </summary>
        /// <param name="filter">Represents a filter to apply to a message based on the part of the message to filter and the value to filter by.</param>
        /// <returns>Returns true if the <see cref="Filter"/> exists within the file, otherwise false.</returns>
        public bool Contains(Filter filter)
        {
            var serializedFilter = JsonConvert.SerializeObject(filter);
            return LoadFile().Contains(serializedFilter);
        }

        /// <summary>
        /// Opens a file, appends the specified <see cref="Filter"/> to the file, and then closes the file. If the file does 
        /// not exist, this method creates a file, writes the specified <see cref="Filter"/> to the file, then closes the file.
        /// </summary>
        /// <param name="filter">Represents a filter to apply to a message based on the part of the message to filter and the value to filter by.</param>
        /// <exception cref="IOException">Thrown when an I/O error occurrs while opening the file.</exception>
        /// <exception cref="UnauthorizedAccessException">Thrown when given a read-only filepath, when the operation is not supported on the current platform, 
        /// or the caller does not have the required permission.</exception>
        /// <exception cref="SecurityException">Thrown when the caller does not have the required permission.</exception>
        public void Append(Filter filter)
        {
            Append(new List<Filter>() { filter });
        }

        /// <summary>
        /// Opens a file, appends the specified <see cref="Filter"/>s to the file, and then closes the file. If the file does 
        /// not exist, this method creates a file, writes the specified <see cref="Filter"/>s to the file, then closes the file.
        /// </summary>
        /// <param name="filters">Represents filters to apply to a message based on the part of the message to filter and the value to filter by.</param>
        /// <exception cref="IOException">Thrown when an I/O error occurrs while opening the file.</exception>
        /// <exception cref="UnauthorizedAccessException">Thrown when given a read-only filepath, when the operation is not supported on the current platform, 
        /// or the caller does not have the required permission.</exception>
        /// <exception cref="SecurityException">Thrown when the caller does not have the required permission.</exception>
        public void Append(List<Filter> filters)
        {
            var fileContents = LoadFile();

            var serializedFiltersToAdd = new List<string>();
            foreach (var filter in filters)
            {
                var serializedFilter = JsonConvert.SerializeObject(filter);
                if (!fileContents.Contains(serializedFilter))
                    serializedFiltersToAdd.Add(serializedFilter);
            }

            var combinedSerializedFilters = string.Join("", serializedFiltersToAdd);
            File.AppendAllText(FullPath, combinedSerializedFilters);
        }

        /// <summary>
        /// Opens a file, removes the specified <see cref="Filter"/> from the file, and then closes the file.
        /// </summary>
        /// <param name="filter">Represents the filter to apply to a message based on the part of the message to filter and the value to filter by.</param>
        /// <exception cref="IOException">Thrown when an I/O error occurrs while opening the file.</exception>
        /// <exception cref="UnauthorizedAccessException">Thrown when given a read-only filepath, when the operation is not supported on the current platform, 
        /// or the caller does not have the required permission.</exception>
        /// <exception cref="SecurityException">Thrown when the caller does not have the required permission.</exception>
        public void Remove(Filter filter)
        {
            Remove(new List<Filter>() { filter });
        }

        /// <summary>
        /// Opens a file, removes the specified <see cref="Filter"/>s from the file, and then closes the file.
        /// </summary>
        /// <param name="filters">Represents filters to apply to a message based on the part of the message to filter and the value to filter by.</param>
        /// <exception cref="IOException">Thrown when an I/O error occurrs while opening the file.</exception>
        /// <exception cref="UnauthorizedAccessException">Thrown when given a read-only filepath, when the operation is not supported on the current platform, 
        /// or the caller does not have the required permission.</exception>
        /// <exception cref="SecurityException">Thrown when the caller does not have the required permission.</exception>
        public void Remove(List<Filter> filters)
        {
            // Figure out how I want to handle the case when the file does not exist

            var serializedFilters = new List<string>();
            filters.ForEach(f => serializedFilters.Add(JsonConvert.SerializeObject(f)));

            string fileContent = LoadFile();
            serializedFilters.ForEach(f => fileContent.Replace(f, string.Empty));
            File.WriteAllText(FullPath, fileContent);
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
        public List<Filter> GetFilters()
        {
            if (!Exists)
            {
                var message = $"Failed to retrieve filters from path: {FullPath}. File does not exist.";
                LogWriter.Write(message);
                throw new FileNotFoundException(message);
            }

            using (var reader = File.OpenText(FullPath))
            {
                var json = reader.ReadToEnd();
                List<Filter> result = JsonConvert.DeserializeObject<List<Filter>>(json);
                return result ?? new List<Filter>();
            }
        }

        private string LoadFile()
        {
            return File.ReadAllText(FullPath);
        }
    }
}
