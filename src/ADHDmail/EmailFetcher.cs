using ADHDmail.API;
using ADHDmail.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADHDmail
{
    public class EmailFetcherUsingTimers
    {
        private System.Timers.Timer _timer;

        public EmailFetcherUsingTimers(double intervalInMilliseconds)
        {
            _timer = new System.Timers.Timer();
            _timer.Interval = intervalInMilliseconds;
            _timer.Elapsed += PrintEmails;
        }

        public void Start()
        {
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }

        public void ChangeInterval(double intervalInMilliseconds)
        {
            _timer.Interval = intervalInMilliseconds;
        }

        public static void PrintEmails(object sender, System.Timers.ElapsedEventArgs e)
        {
            IEmailApi api = new GmailApi();

            var filterConfigFile = new IgnoreFiltersConfigFile();
            filterConfigFile.Clear();
            filterConfigFile.Append(new List<Filter>()
            {
                new Filter(FilterOption.AllFolders),
                new Filter(FilterOption.Read)
            });

            var filters = filterConfigFile.GetFilters();
            var query = new GmailQuery(filters);
            var emails = api.GetEmails(query);

            emails.ForEach(email =>
                Console.WriteLine($"Email ID: {email.Id} Time received: {email.TimeReceived}. " +
                                  $"Subject: {email.Subject} Time: {email.TimeReceived}"));

            Console.WriteLine("-----------------------");
        }
    }

    public class EmailFetcherUsingThreading
    {
        // set a minimum and maximum interval
        private System.Threading.Timer _timer;

        public EmailFetcherUsingThreading(double intervalInSeconds)
        {
            Console.WriteLine($"Starting timer with callback every {intervalInSeconds} second(s).");
            _timer = new System.Threading.Timer(state => PrintEmails(new GmailApi()), 
                                                null, 
                                                TimeSpan.FromSeconds(intervalInSeconds), 
                                                TimeSpan.FromSeconds(intervalInSeconds));
            System.Threading.Thread.Sleep(16000);
            Console.WriteLine("Changing timer to callback every 2 seconds.");
            ChangeInterval(2);
            System.Threading.Thread.Sleep(9000);
            Stop();
            Console.WriteLine("Done. Press ENTER");
            Console.ReadLine();
        }

        public void Start()
        {

        }

        public void Stop()
        {
            _timer.Change(-1, -1);
        }

        public void ChangeInterval(double intervalInSeconds)
        {
            _timer.Change(TimeSpan.FromSeconds(intervalInSeconds), TimeSpan.FromSeconds(intervalInSeconds));
        }

        public static void PrintEmails(IEmailApi api)
        {
            var filterConfigFile = new IgnoreFiltersConfigFile();
            filterConfigFile.Clear();
            filterConfigFile.Append(new List<Filter>()
            {
                new Filter(FilterOption.AllFolders),
                new Filter(FilterOption.Read)
            });

            var filters = filterConfigFile.GetFilters();
            var query = new GmailQuery(filters);
            var emails = api.GetEmails(query);

            emails.ForEach(email =>
                Console.WriteLine($"Email ID: {email.Id} Time received: {email.TimeReceived}. " +
                                  $"Subject: {email.Subject} Time: {email.TimeReceived}"));

            Console.WriteLine("-----------------------");
        }

        // dispose?
    }
}
