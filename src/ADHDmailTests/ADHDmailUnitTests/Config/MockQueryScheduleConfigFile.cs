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
        public MockQueryScheduleConfigFile()
        {
            // providing a default value
            QueryFrequencyInMilliseconds = 50;
        }
    }
}
