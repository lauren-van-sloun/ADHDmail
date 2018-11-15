using ADHDmail.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADHDmail.API
{
    internal class GmailQuery
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

        public List<Filter> QueryFilters { get; set; }
        public string Query { get; private set; }

        public GmailQuery(List<Filter> queryFilters)
        {
            this.QueryFilters = queryFilters;
            Query = ConstructQuery();
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

        public override string ToString()
        {
            return Query;
        }
    }
}
