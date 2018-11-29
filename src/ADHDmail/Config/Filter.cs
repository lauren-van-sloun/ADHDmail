using System;
using System.Collections.Generic;

namespace ADHDmail.Config
{
    /// <summary>
    /// Represents a filter to apply to a message based on the part of the message to 
    /// filter and the value to filter by.
    /// </summary>
    public class Filter : IEquatable<Filter>
    {
        /// <summary>
        /// Represents the part of the message to filter.
        /// </summary>
        public FilterOption FilterOption { get; set; }

        /// <summary>
        /// Represents the value to filter by.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Filter"/> class.
        /// </summary>
        /// <param name="filterOption">The filter to apply.</param>
        /// <param name="value">The value to filter by. Not needed for certain filters such 
        /// as <see cref="FilterOption.HasAttachment"/>, <see cref="FilterOption.Unread"/>, etc.</param>
        public Filter(FilterOption filterOption, string value = "")
        {
            this.FilterOption = filterOption;
            this.Value = value;
        }

        private readonly Dictionary<FilterOption, string> _filterValues =
            new Dictionary<FilterOption, string>()
        {
            { FilterOption.From, "from:<>" },
            { FilterOption.To, "to:<>" },
            { FilterOption.Subject, "subject:<>" },
            { FilterOption.Label, "label:<>" },
            { FilterOption.HasAttachment, "has:attachment" },
            { FilterOption.HasFilename, "filename:<>" },
            { FilterOption.ContainsWord, "<>" },
            { FilterOption.AllFolders, "is:anywhere" },
            { FilterOption.Starred, "is:starred" },
            { FilterOption.Unread, "is:unread" },
            { FilterOption.Read, "is:read" },
            { FilterOption.After, "after:<>" },
            { FilterOption.Before, "before:<>" },
            { FilterOption.DeliveredTo, "deliveredto:<>" },
            { FilterOption.LargerThan, "larger:<>" },
            { FilterOption.SmallerThan, "smaller:<>" },
            { FilterOption.MatchesWordExactly, "+<>" }
        };

        /// <summary>
        /// Returns the filter as a string formatted for use in an API query.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.IsNullOrWhiteSpace(Value)
                ? _filterValues[FilterOption]
                : _filterValues[FilterOption].Replace("<>", Value);
        }

        /// <summary>
        /// Determines whether the specified <see cref="Filter"/> instances are considered equal
        /// in terms of their <see cref="FilterOption"/> selection and <see cref="Value"/>'s value.
        /// </summary>
        /// <param name="obj">The other <see cref="Filter"/> to compare to.</param>
        /// <returns>Returns true if the <see cref="Filter"/>s are equal, otherwise false.</returns>
        public override bool Equals(object obj)
        {
            var other = obj as Filter;
            return other == null ? false : this.FilterOption == other.FilterOption &&
                                           this.Value == other.Value;
        }

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>Returns the base class's hash code.</returns>
        public override int GetHashCode()
        {
            return FilterOption.GetHashCode() ^ Value.GetHashCode();
        }

        bool IEquatable<Filter>.Equals(Filter other)
        {
            return this.Equals(other);
        }
    }
}
