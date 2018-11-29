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
    public static class IgnoreFilterConfigFileTests
    {
        private static IgnoreFiltersConfigFile _filterConfigFile = new IgnoreFiltersConfigFile();

        public static IEnumerable<object[]> SampleFilterListsAndSerializedValues =>
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
                }
            };

        public static IEnumerable<object[]> SampleFiltersAndSerializedValues =>
            new List<object[]>
            {
                new object[] { new Filter(FilterOption.AllFolders), "[{\"FilterOption\":7,\"Value\":\"\"}]" },
                new object[] { new Filter(FilterOption.ContainsWord, "Test"), "[{\"FilterOption\":6,\"Value\":\"Test\"}]" }
            };

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

        [Theory]
        [MemberData(nameof(SampleFiltersAndSerializedValues))]
        public static void GetFiltersTest_WhenFileOnlyHasOneFilter(Filter filter, string serializedFilter)
        {
            try
            {
                File.WriteAllText(_filterConfigFile.FullPath, serializedFilter);
                var filterFromFile = _filterConfigFile.GetFilters();
                Assert.Equal(filter, filterFromFile.First());
            }
            finally
            {
                _filterConfigFile.Clear();
            }
        }

        [Theory]
        [MemberData(nameof(SampleFilterListsAndSerializedValues))]
        public static void GetFiltersTest_WhenFileHasMultipleFilters(List<Filter> filters, string serializedFilter)
        {
            try
            {
                File.WriteAllText(_filterConfigFile.FullPath, serializedFilter);
                var filtersFromFile = _filterConfigFile.GetFilters();
                Assert.Equal(filters[0], filtersFromFile[0]);
            }
            finally
            {
                _filterConfigFile.Clear();
            }
        }

        [Theory]
        [MemberData(nameof(SampleFiltersAndSerializedValues))]
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

        [Theory]
        [MemberData(nameof(SampleFiltersAndSerializedValues))]
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
        [MemberData(nameof(SampleFilterListsAndSerializedValues))]
        public static void AppendMultipleTests(List<Filter> filters, string expectedFileContent)
        {
            try
            {
                _filterConfigFile.Append(filters);
                Assert.True(File.ReadAllText(_filterConfigFile.FullPath).Contains(expectedFileContent));
            }
            finally
            {
                _filterConfigFile.Clear();
            }
        }

        [Fact]
        public static void AppendMultipleTest_DoesNotAddDuplicatesWhenGivenListWithDuplicates()
        {
            try
            {
                var filter = new Filter(FilterOption.HasAttachment);
                var duplicateFilters = new List<Filter>
                {
                    filter,
                    filter
                };
                var expectedFileContent = "[{\"FilterOption\":4,\"Value\":\"\"}]";

                _filterConfigFile.Append(duplicateFilters);

                Assert.True(File.ReadAllText(_filterConfigFile.FullPath).Contains(expectedFileContent));
            }
            finally
            {
                _filterConfigFile.Clear();
            }
        }

        [Fact]
        public static void AppendMultipleTest_DoesNotAddDuplicatesWhenFilterIsAlreadyInFile()
        {
            throw new NotImplementedException();
        }

        [Theory]
        [MemberData(nameof(SampleFiltersAndSerializedValues))]
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

        public static IEnumerable<object[]> SampleFiltersAndExpectedFileContent =>
            new List<object[]>
            {
                new object[] 
                {
                    new Filter(FilterOption.AllFolders),
                    new Filter(FilterOption.DeliveredTo, "Nobody"),
                    "[{\"FilterOption\":7,\"Value\":\"\"}]"
                },
                new object[] 
                {
                    new Filter(FilterOption.ContainsWord, "Test"),
                    new Filter(FilterOption.Read),
                    "[{\"FilterOption\":6,\"Value\":\"Test\"}]"
                }
            };

        [Theory]
        [MemberData(nameof(SampleFiltersAndExpectedFileContent))]
        public static void RemoveTest_WhenFilterIsNotInFile(Filter filterInFile,
                                                            Filter filterToRemove,
                                                            string expectedFileContent)
        {
            try
            {
                _filterConfigFile.Append(filterInFile);
                _filterConfigFile.Remove(filterToRemove);
                Assert.True(File.ReadAllText(_filterConfigFile.FullPath).Contains(expectedFileContent));
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
    }
}
