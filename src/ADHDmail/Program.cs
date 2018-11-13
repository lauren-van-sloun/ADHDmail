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
            var queryFilters = new List<GmailFilter>()
            {
                new GmailFilter(GmailFilterOption.Unread),
                new GmailFilter(GmailFilterOption.LargerThan, "1"),
                new GmailFilter(GmailFilterOption.From, "GitHub")
            };
            var query = new GmailQuery(queryFilters);
            var unreadEmails = api.GetEmails(query);

            unreadEmails.ForEach(e => 
                Console.WriteLine($"Email ID: {e.Id} Time received: {e.TimeReceived}. Subject: {e.Subject}"));
        }
    }
}
