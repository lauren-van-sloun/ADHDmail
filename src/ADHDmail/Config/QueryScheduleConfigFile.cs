using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADHDmail.Config
{
    public class QueryScheduleConfigFile : ConfigFile
    {
        const double fiveSecondsInMilliseconds = 5000;
        const double oneHourInMilliseconds = 3600000;

        // this range attribute isn't working 
        [Range(fiveSecondsInMilliseconds, oneHourInMilliseconds, ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public double QueryFrequencyInMilliseconds { get; set; }

        public QueryScheduleConfigFile()
        {
            // providing a default value
            QueryFrequencyInMilliseconds = 50;
        }

        public void AddDays(HashSet<DayOfWeek> daysToApplyFilter)
        {
            throw new NotImplementedException();
        }

        public HashSet<DayOfWeek> GetDays()
        {
            throw new NotImplementedException();
        }

        public void AddHours(HashSet<byte> hoursToApplyFilter)
        {
            throw new NotImplementedException();
        }

        public HashSet<byte> GetHours()
        {
            throw new NotImplementedException();
        }

        public void UpdateQueryFrequency(double queryFrequencyInMilliseconds)
        {
            // writes this to the file
            QueryFrequencyInMilliseconds = queryFrequencyInMilliseconds;
        }

        public double GetQueryFrequency()
        {
            return QueryFrequencyInMilliseconds;
        }
    }
}
