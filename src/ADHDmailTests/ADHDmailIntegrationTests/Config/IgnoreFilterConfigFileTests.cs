using ADHDmail;
using ADHDmail.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ADHDmailIntegrationTests.Config
{
    public class IgnoreFilterConfigFileTests
    {
        IgnoreFiltersConfigFile filterConfigFile = new IgnoreFiltersConfigFile();

        [Fact]
        public void AppendTests()
        {
            filterConfigFile.FullPath = Path.Combine(
                    GlobalValues.AppDataPath,
                    GlobalValues.ApplicationName,
                    "IgnoreFiltersTest.json");

            Filter filter = new Filter(FilterOption.AllFolders);
            var filters = new List<Filter>()
            {
                new Filter(FilterOption.AllFolders),
                new Filter(FilterOption.ContainsWord, "Test")
            };

            try
            {
                filterConfigFile.Append(filters);

                var expectedText = "{\"FilterOption\":7,\"Value\":\"\"}{\"FilterOption\":6,\"Value\":\"Test\"}";

                bool fileContainsFilters = false;
                if (File.ReadLines(filterConfigFile.FullPath).Any(line => line.Contains(expectedText)))
                    fileContainsFilters = true;

                Assert.True(fileContainsFilters);
            }
            catch (Exception)
            {
                Assert.True(false);
            }
            finally
            {
                filterConfigFile.Remove(filters);
            }
        }


        // Test these
        // ctor sets FullPath field on base class
        // Append successfully appends filter(s)
        // Clear clears the file
        // GetFilters successfully reads and returns Filters
    }
}
