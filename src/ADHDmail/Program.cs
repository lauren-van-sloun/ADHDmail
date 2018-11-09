using ADHDmail.API;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADHDmail
{
    class Program
    {
        public static void Main(string[] args)
        {
            var api = new GmailApi();
            var queryFilters = new List<GmailQueryFilter>()
            {
                new GmailQueryFilter(GmailQueryFilterOption.Unread),
                new GmailQueryFilter(GmailQueryFilterOption.LargerThan, "1"),
                // this filter isn't applying, debug needed
                new GmailQueryFilter(GmailQueryFilterOption.Contains, "Dog")
            };
            var query = new GmailQuery(queryFilters);
            var unreadEmails = api.GetEmails(query);

            unreadEmails.ForEach(e => 
                Console.WriteLine($"Time received: {e.TimeReceived}. Subject: {e.Subject}"));
        
        }
    }
}
