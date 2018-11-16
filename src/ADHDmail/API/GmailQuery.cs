using ADHDmail.Config;
using System.Collections.Generic;
using System.Text;

namespace ADHDmail.API
{
    /// <summary>
    /// Represents a query used to retrieve emails from the Gmail API.
    /// </summary>
    public class GmailQuery : Query
    {
        private readonly Dictionary<FilterOption, string> _queryFilterValues = 
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
        /// Represents the filters to apply to a query.
        /// </summary>
        private List<Filter> _queryFilters { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GmailQuery"/> class with filters 
        /// to apply to the query.
        /// </summary>
        /// <param name="queryFilters">Represents the filters to apply to the query.</param>
        public GmailQuery(List<Filter> queryFilters)
        {
            this._queryFilters = queryFilters;
            RawQuery = ConstructQuery();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GmailQuery"/> class with no filtering. 
        /// This will construct a query that will return all emails.
        /// </summary>
        public GmailQuery() : base()
        { }

        private string ConstructQuery()
        {
            var queryBuilder = new StringBuilder();

            for (int i = 0; i < _queryFilters.Count; i++)
            {
                var key = _queryFilters[i].FilterOption;
                var value = _queryFilters[i].Value;
                if (string.IsNullOrWhiteSpace(value))
                    queryBuilder.Append(_queryFilterValues[key]);
                else
                    queryBuilder.Append(_queryFilterValues[key].Replace("<>", value));
                if (i != _queryFilters.Count - 1)
                    queryBuilder.Append(' ');
            }

            return queryBuilder.ToString();
        }
    }
}