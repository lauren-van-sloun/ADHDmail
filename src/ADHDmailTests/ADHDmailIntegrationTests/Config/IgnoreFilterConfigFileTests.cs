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

        private static void TestTearDown()
        {
            _filterConfigFile.Clear();
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
                TestTearDown();
            }
        }

        // give better name to distinguish between this and the other data set
        public static IEnumerable<object[]> FiltersAndValues =>
            new List<object[]>
            {
                new object[] { new Filter(FilterOption.AllFolders), "[{\"FilterOption\":7,\"Value\":\"\"}]" },
                new object[] { new Filter(FilterOption.ContainsWord, "Test"), "[{\"FilterOption\":6,\"Value\":\"Test\"}]"},    
            };

        [Theory]
        [MemberData(nameof(FiltersAndValues))]
        public static void AppendSingularTests(Filter input, string expectedFileContent)
        {
            try
            {
                _filterConfigFile.Append(input);
                Assert.True(File.ReadAllText(_filterConfigFile.FullPath).Contains(expectedFileContent));
            }
            finally
            {
                TestTearDown();
            }
        }

        [Theory]
        [MemberData(nameof(FiltersAndValues))]
        public static void ContainsTest(Filter input, string serializedFilter)
        {
            try
            {
                _filterConfigFile.Append(input);
                Assert.True(File.ReadLines(_filterConfigFile.FullPath).Any(line => line.Contains(serializedFilter)));
            }
            finally
            {
                TestTearDown();
            }
        }

        [Fact]
        public static void ClearTest()
        {
            File.WriteAllText(_filterConfigFile.FullPath, "Sample text to ensure the file has content");
            _filterConfigFile.Clear();
            var fileIsEmpty = new FileInfo(_filterConfigFile.FullPath).Length == 0;
            Assert.True(fileIsEmpty);
        }

        public static IEnumerable<object[]> PathsAndExceptions =>
          new List<object[]>
          {
                new object[] { _filterConfigFile.FullPath.Replace("ADHDmail", "NonexistantDirectory"), typeof(DirectoryNotFoundException) },
                new object[] { _filterConfigFile.FullPath.Replace("IgnoreFiltersTest.json", "NonexistantFile.json"), typeof(FileNotFoundException) }
          };

        [Theory]
        [MemberData(nameof(PathsAndExceptions))]
        public static void ClearTest_ExpectedExceptions(string path, Type expectedExceptionType)
        {
            try
            {
                _filterConfigFile.FullPath = path;
                _filterConfigFile.Clear();
            }
            catch (Exception ex)
            {
                Assert.True(ex.GetType() == expectedExceptionType);
            }
            finally
            {
                SetConfigPathForTesting();
            }
        }

        [Theory]
        public static void RemoveTest()
        {
            throw new NotImplementedException();
        }

        [Theory]
        public static void GetFilters()
        {
            throw new NotImplementedException();
        }
    }
}
