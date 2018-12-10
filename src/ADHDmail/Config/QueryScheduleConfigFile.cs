using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace ADHDmail.Config
{
    public class QueryScheduleConfigFile : ConfigFile
    {
        private class QuerySchedule
        {
            public QuerySchedule()
            {
                Days = new HashSet<DayOfWeek>();
                Hours = new HashSet<byte>();
                FrequencyInMilliseconds = new double();
            }

            // Restrict these properties' potential values when I code the UI potion of the app
            // and optionally here as well
            public HashSet<DayOfWeek> Days { get; set; }
            public HashSet<byte> Hours { get; set; }
            public double FrequencyInMilliseconds { get; set; }

            public void SetDefaultValues()
            {
                Days.Add(DayOfWeek.Monday);
                Days.Add(DayOfWeek.Tuesday);
                Days.Add(DayOfWeek.Wednesday);
                Days.Add(DayOfWeek.Thursday);
                Days.Add(DayOfWeek.Friday);

                for (byte hour = 8; hour < 17; hour++)
                {
                    Hours.Add(hour);
                }

                FrequencyInMilliseconds = 5000;
            }
        }

        private QuerySchedule _querySchedule;

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryScheduleConfigFile"/> class.
        /// </summary>
        public QueryScheduleConfigFile(string fileName = "QuerySchedule.json")
        {
            FullPath = GetFullPath(fileName);
            GetValuesFromFile();
        }

        private void GetValuesFromFile()
        {
            var fileContents = LoadFile();
            var deserializedSchedule = fileContents.Deserialize<QuerySchedule>();
            if (deserializedSchedule == null)
            {
                _querySchedule = new QuerySchedule();
                _querySchedule.SetDefaultValues();
            }
            else
                _querySchedule = deserializedSchedule;
        }

        public void AddDays(HashSet<DayOfWeek> daysToApplyFilter)
        {
            foreach (var day in daysToApplyFilter)
            {
                _querySchedule.Days.Add(day);
            }

            WriteScheduleToFile();
        }

        public HashSet<DayOfWeek> GetDays()
        {
            return _querySchedule.Days;
        }

        public void AddHours(HashSet<byte> hoursToApplyFilter)
        {
            foreach (var hour in hoursToApplyFilter)
            {
                _querySchedule.Hours.Add(hour);
            }

            WriteScheduleToFile();
        }

        public HashSet<byte> GetHours()
        {
            return _querySchedule.Hours;
        }

        public void UpdateFrequency(double frequencyInMilliseconds)
        {
            if (_querySchedule.FrequencyInMilliseconds == frequencyInMilliseconds)
                return;
            _querySchedule.FrequencyInMilliseconds = frequencyInMilliseconds;

            WriteScheduleToFile();
        }

        public double GetFrequency()
        {
            return _querySchedule.FrequencyInMilliseconds;
        }

        private void WriteScheduleToFile()
        {
            string serializedQuerySchedule = JsonConvert.SerializeObject(_querySchedule);
            File.WriteAllText(FullPath, serializedQuerySchedule);
        }
    }
}
