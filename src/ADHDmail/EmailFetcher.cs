using ADHDmail.API;
using ADHDmail.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ADHDmail
{
    public static class EmailFetcher
    {
        private static Timer _timer;

        public static void SetTimer(IEmailApi emailApi, Func<List<Email>, bool> callback)
        {
            // read from file
            double intervalInMilliseconds = 5000;

            _timer = new Timer();
            _timer.Interval = intervalInMilliseconds;
            _timer.Elapsed += delegate
            {
                var filterConfigFile = new IgnoreFiltersConfigFile();
                var filters = filterConfigFile.GetFilters();
                var query = new GmailQuery(filters);
                var response = emailApi.GetEmails(query);
                if (!callback(response))
                {
                    _timer.Stop();
                }
            };
        }

        public static void Start()
        {
            _timer.Start();
        }

        public static void Stop()
        {
            _timer.Stop();
        }

        public static void ChangeInterval(double intervalInMilliseconds)
        {
            _timer.Interval = intervalInMilliseconds;
        }
    }
}
