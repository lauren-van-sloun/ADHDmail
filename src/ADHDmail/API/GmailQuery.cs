using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADHDmail.API
{
    internal class GmailQuery
    {
        private readonly Dictionary<GmailFilterOption, string> _queryFilters = 
            new Dictionary<GmailFilterOption, string>()
        {
            { GmailFilterOption.From, "from:<>" },
            { GmailFilterOption.To, "to:<>" },
            { GmailFilterOption.Subject, "subject:<>" },
            { GmailFilterOption.Label, "label:<>" },
            { GmailFilterOption.HasAttachment, "has:attachment" },
            { GmailFilterOption.HasFilename, "filename:<>" },
            { GmailFilterOption.ContainsWord, "<>" },
            { GmailFilterOption.AllFolders, "is:anywhere" },
            { GmailFilterOption.Starred, "is:starred" },
            { GmailFilterOption.Unread, "is:unread" },
            { GmailFilterOption.Read, "is:read" },
            { GmailFilterOption.After, "after:<>" },
            { GmailFilterOption.Before, "before:<>" },
            { GmailFilterOption.DeliveredTo, "deliveredto:<>" },
            { GmailFilterOption.LargerThan, "larger:<>" },
            { GmailFilterOption.SmallerThan, "smaller:<>" },
            { GmailFilterOption.MatchesWordExactly, "+<>" }
        };

        public List<GmailFilter> QueryFilters { get; set; }
        public string Query { get; private set; }

        public GmailQuery(List<GmailFilter> queryFilters)
        {
            this.QueryFilters = queryFilters;
            Query = ConstructQuery();
        }

        private string ConstructQuery()
        {
            var queryBuilder = new StringBuilder();

            for (int i = 0; i < QueryFilters.Count; i++)
            {
                var key = QueryFilters[i].Filter;
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
