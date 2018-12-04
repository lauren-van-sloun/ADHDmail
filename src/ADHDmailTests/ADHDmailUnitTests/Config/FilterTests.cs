using ADHDmail.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ADHDmailTests.Config
{
    public class FilterTests
    {
        public static IEnumerable<object[]> FiltersAndTheirExpectedStringValues =>
           new List<object[]>
           {
                new object[]
                {
                    new Filter(FilterOption.Subject, "Test subject"),
                    "subject:Test subject"
                },
                new object[]
                {
                    new Filter(FilterOption.Starred),
                    "is:starred"
                },new object[]
                {
                    new Filter(FilterOption.To, "Recipient"),
                    "to:Recipient"
                },
           };

        [Theory]
        [MemberData(nameof(FiltersAndTheirExpectedStringValues))]
        public void ToStringTest(Filter filter, string expectedStringValue)
        {
            Assert.Equal(filter.ToString(), expectedStringValue);
        }

        public static IEnumerable<object[]> FiltersAndTheirEquality =>
            new List<object[]>
            {
                new object[]
                {   new Filter(FilterOption.Label, "Test Label"),
                    new Filter(FilterOption.Label, "Test Label"),
                    true
                },
                new object[]
                {
                    new Filter(FilterOption.Starred),
                    new Filter(FilterOption.Starred),
                    true
                },
                new object[]
                {
                    new Filter(FilterOption.DeliveredTo, "Test value"),
                    new Filter(FilterOption.DeliveredTo, "test value"),
                    false
                },
                new object[]
                {
                    new Filter(FilterOption.MatchesWordExactly, "Test"),
                    new Filter(FilterOption.ContainsWord, "Test"),
                    false
                }
            };

        [Theory]
        [MemberData(nameof(FiltersAndTheirEquality))]
        public void EqualsTest(Filter first, Filter second, bool expectedEquality)
        {
            Assert.True(first.Equals(second) == expectedEquality);
        }
    }
}
