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
    // order these tests to match the method order that is in IgnorefilterConfigFile.cs
    public static class IgnoreFilterConfigFileTests
    {
        private static IgnoreFiltersConfigFile _filterConfigFile = new IgnoreFiltersConfigFile();

        static IgnoreFilterConfigFileTests()
        {
            SetConfigPathForTesting();
        }

        private static void SetConfigPathForTesting()
        {
            _filterConfigFile.FullPath = Path.Combine(
                    GlobalValues.AppDataPath,
                    GlobalValues.ApplicationName,
                    "IgnoreFiltersTest.json");
        }

        [Fact]
        public static void ConstructorTest()
        {
            Assert.True(_filterConfigFile.FullPath.IsValidPath());
        }

        // give better name to distinguish between this and the other data set
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
                    "[{\"FilterOption\":7,\"Value\":\"\"},{\"FilterOption\":6,\"Value\":\"Test\"}]"
                },
            };

        [Theory]
        [MemberData(nameof(TestFiltersAndTheirSerializedValues))]
        public static void AppendMultipleTests(List<Filter> input, string expectedFileContent)
        {
            try
            {
                _filterConfigFile.Append(input);
                Assert.True(File.ReadAllText(_filterConfigFile.FullPath).Contains(expectedFileContent));
            }
            finally
            {
                _filterConfigFile.Clear();
            }
        }

        // give better name to distinguish between this and the other data set
        public static IEnumerable<object[]> SampleFiltersAndValues =>
            new List<object[]>
            {
                new object[] { new Filter(FilterOption.AllFolders), "[{\"FilterOption\":7,\"Value\":\"\"}]" },
                new object[] { new Filter(FilterOption.ContainsWord, "Test"), "[{\"FilterOption\":6,\"Value\":\"Test\"}]"},    
            };

        [Theory]
        [MemberData(nameof(SampleFiltersAndValues))]
        public static void AppendSingularTests(Filter input, string expectedFileContent)
        {
            try
            {
                _filterConfigFile.Append(input);
                Assert.True(File.ReadAllText(_filterConfigFile.FullPath).Contains(expectedFileContent));
            }
            finally
            {
                _filterConfigFile.Clear();
            }
        }

        [Theory]
        [MemberData(nameof(SampleFiltersAndValues))]
        public static void ContainsTest(Filter input, string serializedFilter)
        {
            try
            {
                _filterConfigFile.Append(input);
                Assert.True(File.ReadLines(_filterConfigFile.FullPath).Any(line => line.Contains(serializedFilter)));
            }
            finally
            {
                _filterConfigFile.Clear();
            }
        }

        [Fact]
        public static void ClearTest()
        {
            File.WriteAllText(_filterConfigFile.FullPath, "Sample text to ensure the file has content");
            _filterConfigFile.Clear();
            Assert.True(_filterConfigFile.FullPath.IsEmptyFile());
        }

        

        [Theory]
        [MemberData(nameof(SampleFiltersAndValues))]
        public static void RemoveTest(Filter filter, string serializedFilter)
        {
            try
            {
                File.WriteAllText(_filterConfigFile.FullPath, serializedFilter);
                _filterConfigFile.Remove(filter);
                Assert.True(_filterConfigFile.FullPath.IsEmptyFile());
            }
            finally
            {
                _filterConfigFile.Clear();
            }
        }

        [Theory]
        public static void GetFilters()
        {
            throw new NotImplementedException();
        }
    }
}
