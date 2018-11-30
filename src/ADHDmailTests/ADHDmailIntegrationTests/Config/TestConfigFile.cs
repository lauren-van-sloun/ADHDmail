using ADHDmail.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADHDmailIntegrationTests.Config
{
    internal class TestConfigFile : ConfigFile
    {
        public TestConfigFile(string fileName = "TestConfig.json")
        {
            FullPath = GetFullPath(fileName);
            if (!File.Exists(FullPath))
                Create();
        }
    }
}
