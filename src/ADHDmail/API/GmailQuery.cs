using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADHDmail.API
{
    internal static class GmailQuery
    {
        private static Dictionary<GmailQueryParameter, string> _queryParameters = 
            new Dictionary<GmailQueryParameter, string>()
        {
            { GmailQueryParameter.From, "from:<>" },
            { GmailQueryParameter.To, "to:<>" },
            { GmailQueryParameter.Subject, "subject:<>" },
            { GmailQueryParameter.Label, "label:<>" },
            { GmailQueryParameter.HasAttachment, "has:attachment" },
            { GmailQueryParameter.HasFilename, "filename:<>" },
            { GmailQueryParameter.Contains, "<>" },
            { GmailQueryParameter.AllFolders, "is:anywhere" },
            { GmailQueryParameter.Starred, "is:starred" },
            { GmailQueryParameter.Unread, "is:unread" },
            { GmailQueryParameter.Read, "is:read" },
            { GmailQueryParameter.After, "after:<>" },
            { GmailQueryParameter.Before, "before:<>" },
            { GmailQueryParameter.DeliveredTo, "deliveredto:<>" },
            { GmailQueryParameter.LargerThan, "larger:<>" },
            { GmailQueryParameter.SmallerThan, "smaller:<>" },
            { GmailQueryParameter.MatchesWordExactly, "+<>" }
        };

        // need to properly implement this and substitute in values for those that take values
        internal static string ConstructQuery(params GmailQueryParameter[] queryParams)
        {
            var queryBuilder = new StringBuilder();

            foreach (var queryParameter in queryParams)
            {
                queryBuilder.Append(_queryParameters[queryParameter]);
            }

            return queryBuilder.ToString();
        }
    }
}
