using ADHDmail.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADHDmailTests.Config
{
    public class MockQueryScheduleConfigFile : QueryScheduleConfigFile
    {
        private const double fiveSecondsInMilliseconds = 5000;
        private const double oneHourInMilliseconds = 3600000;

        public MockQueryScheduleConfigFile()
        {
            // providing a default value
            UpdateFrequency(50);
        }

        // hide the base class implementation with the new keyword so I'm not accessing file dependencies
    }
}
