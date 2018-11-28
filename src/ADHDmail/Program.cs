using ADHDmail.API;
using ADHDmail.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Timers;

namespace ADHDmail
{
    class Program
    {
        public static void Main(string[] args)
        {
            //var fiveMinutesInMilliseconds = 300000;
            /*
            var fiveSecondsInMilliseconds = 5000;
            Timer timer = new Timer();
            timer.Interval = fiveSecondsInMilliseconds;
            timer.Elapsed += PrintEmails;
            timer.Start();

            Console.ReadKey();
            timer.Stop();
            */
            
            PrintEmails(null, null);
        }

        public static void PrintEmails(object sender, ElapsedEventArgs e)
        {
            IEmailApi api = new GmailApi();

            var filterConfigFile = new IgnoreFiltersConfigFile();
            filterConfigFile.Clear();
            //filterConfigFile.Append(new Filter(FilterOption.AllFolders));
            filterConfigFile.Append(new List<Filter>() { new Filter(FilterOption.AllFolders), new Filter(FilterOption.Read) });

            var filters = filterConfigFile.GetFilters();
            var query = new GmailQuery(filters);
            var emails = api.GetEmails(query);

            emails.ForEach(email =>
                Console.WriteLine($"Email ID: {email.Id} Time received: {email.TimeReceived}. " +
                                  $"Subject: {email.Subject} Time: {email.TimeReceived}"));

            Console.WriteLine("-----------------------");
        }

        // in memory filters to use for temp testing
        public static List<Filter> CreateFilters()
        {
            return new List<Filter>()
            {
                new Filter(FilterOption.Unread),
                new Filter(FilterOption.LargerThan, "1"),
                new Filter(FilterOption.From, "GitHub")
            };
        }
    }
}
