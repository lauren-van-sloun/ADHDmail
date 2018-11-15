using ADHDmail.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADHDmail.API
{
    // Potentially change this to just Query and not Gmail query depending on
    // what query filters look like on other APIs

    /// <summary>
    /// Represents a query to retrieve messages from the Gmail API.
    /// </summary>
    public class GmailQuery
    {
        private readonly Dictionary<FilterOption, string> _queryFilters = 
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
        public List<Filter> QueryFilters { get; set; }
        private readonly string _query;

        /// <summary>
        /// Initializes a new instance of the <see cref="GmailQuery"/> class with filters 
        /// to apply to the query.
        /// </summary>
        /// <param name="queryFilters">Represents the filters to apply to the query.</param>
        public GmailQuery(List<Filter> queryFilters)
        {
            this.QueryFilters = queryFilters;
            _query = ConstructQuery();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GmailQuery"/> class with no filtering. 
        /// This will make a request for all emails.
        /// </summary>
        public GmailQuery()
        {
            _query = "";
        }

        private string ConstructQuery()
        {
            var queryBuilder = new StringBuilder();

            for (int i = 0; i < QueryFilters.Count; i++)
            {
                var key = QueryFilters[i].FilterOption;
                var value = QueryFilters[i].Value;
                if (string.IsNullOrWhiteSpace(value))
                    queryBuilder.Append(_queryFilters[key]);
                else
                    queryBuilder.Append(_queryFilters[key].Replace("<>", value));
                if (i != QueryFilters.Count - 1)
                    queryBuilder.Append(' ');
            }

            return queryBuilder.ToString();
        }

        /// <summary>
        /// Represents the fully constructed query for use in the <see cref="GmailApi.GetMessage(string)"/> 
        /// method. 
        /// <para>If this returns <see cref="string.Empty"/>, then no filtering is applied to the query.</para>
        /// </summary>
        /// <returns>Returns the fully constructed query.</returns>
        public override string ToString() => _query;
    }
}
