using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADHDmail.API
{
    internal class GmailQuery
    {
        private Dictionary<GmailQueryFilterOption, string> _queryFilters = 
            new Dictionary<GmailQueryFilterOption, string>()
        {
            { GmailQueryFilterOption.From, "from:<>" },
            { GmailQueryFilterOption.To, "to:<>" },
            { GmailQueryFilterOption.Subject, "subject:<>" },
            { GmailQueryFilterOption.Label, "label:<>" },
            { GmailQueryFilterOption.HasAttachment, "has:attachment" },
            { GmailQueryFilterOption.HasFilename, "filename:<>" },
            { GmailQueryFilterOption.Contains, "<>" },
            { GmailQueryFilterOption.AllFolders, "is:anywhere" },
            { GmailQueryFilterOption.Starred, "is:starred" },
            { GmailQueryFilterOption.Unread, "is:unread" },
            { GmailQueryFilterOption.Read, "is:read" },
            { GmailQueryFilterOption.After, "after:<>" },
            { GmailQueryFilterOption.Before, "before:<>" },
            { GmailQueryFilterOption.DeliveredTo, "deliveredto:<>" },
            { GmailQueryFilterOption.LargerThan, "larger:<>" },
            { GmailQueryFilterOption.SmallerThan, "smaller:<>" },
            { GmailQueryFilterOption.MatchesWordExactly, "+<>" }
        };

        public List<GmailQueryFilter> QueryFilters { get; set; }
        public string Query { get; private set; }

        public GmailQuery(List<GmailQueryFilter> queryFilters)
        {
            this.QueryFilters = queryFilters;
            Query = ConstructQuery();
        }

        private string ConstructQuery()
        {
            var queryBuilder = new StringBuilder();

            for (int i = 0; i < QueryFilters.Count - 1; i++)
            {
                var key = QueryFilters[i].Filter;
                var value = QueryFilters[i].Value;
                if (string.IsNullOrWhiteSpace(value))
                    queryBuilder.Append(_queryFilters[key]);
                else
                    queryBuilder.Append(_queryFilters[key].Replace("<>", value));
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
