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
    public class EmailFetcher
    {
        private Timer _timer;
        private IEmailApi _emailApi;
        private QueryScheduleConfigFile _queryScheduleConfigFile;

        public EmailFetcher(IEmailApi emailApi, QueryScheduleConfigFile queryScheduleConfigFile)
        {
            _emailApi = emailApi;
            _queryScheduleConfigFile = queryScheduleConfigFile;
        }

        public void SetTimer(Func<List<Email>, bool> callback)
        {
            _timer = new Timer();
            _timer.Interval = _queryScheduleConfigFile.GetFrequency();
            _timer.Elapsed += delegate
            {
                var filterConfigFile = new IgnoreFiltersConfigFile();
                var filters = filterConfigFile.GetFilters();
                var query = new GmailQuery(filters);
                var emails = _emailApi.GetEmails(query);
                if (!callback(emails))
                {
                    _timer.Stop();
                }
            };
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
    }
}
