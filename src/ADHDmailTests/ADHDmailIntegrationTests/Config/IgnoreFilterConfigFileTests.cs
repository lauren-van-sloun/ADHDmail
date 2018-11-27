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

        private void SetConfigPathForTesting()
        {
            filterConfigFile.FullPath = Path.Combine(
                    GlobalValues.AppDataPath,
                    GlobalValues.ApplicationName,
                    "IgnoreFiltersTest.json");
        }
        
        public static IEnumerable<object[]> TestFiltersAndTheirSerializedValues =>
            new List<object[]>
            {
                new object[] 
                {
                    new List<Filter>()
                    {
                        new Filter(FilterOption.AllFolders),
                        new Filter(FilterOption.ContainsWord, "Test")
                    },
                    "{\"FilterOption\":7,\"Value\":\"\"}{\"FilterOption\":6,\"Value\":\"Test\"}"
                }
            };

        [Theory]
        [MemberData(nameof(TestFiltersAndTheirSerializedValues))]
        public void AppendTests(List<Filter> input, string expectedFileContent)
        {
            SetConfigPathForTesting();

            try
            {
                // act
                filterConfigFile.Append(input);

                // check
                bool fileContainsFilters = false;
                if (File.ReadLines(filterConfigFile.FullPath).Any(line => line.Contains(expectedFileContent)))
                    fileContainsFilters = true;
                // assert
                Assert.True(fileContainsFilters);
            }
            catch (Exception)
            {
                Assert.True(false);
            }
            finally
            {
                filterConfigFile.Remove(input);
            }
        }


        // Test these
        // ctor sets FullPath field on base class
        // Append successfully appends filter(s)
        // Clear clears the file
        // GetFilters successfully reads and returns Filters
    }
}
