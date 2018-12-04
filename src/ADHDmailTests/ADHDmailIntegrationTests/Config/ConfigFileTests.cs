using ADHDmail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ADHDmailIntegrationTests.Config
{
    public class ConfigFileTests
    {
        private readonly TestConfigFile _configFile = new TestConfigFile();

        [Fact]
        public void ExistsTest()
        {
            Assert.True(File.Exists(_configFile.FullPath));
        }

        [Fact]
        public void GetFullPathTest()
        {
            Assert.True(_configFile.FullPath.IsValidPath());
        }

        [Fact]
        public void CreateTest()
        {
            _configFile.Create();
            Assert.True(File.Exists(_configFile.FullPath));
        }

        [Fact]
        public void EncryptTest()
        {
            _configFile.Encrypt();
            Assert.True(_configFile.FullPath.IsEncrypted());
        }

        [Fact]
        public void DecryptTest()
        {
            _configFile.Decrypt();
            Assert.False(_configFile.FullPath.IsEncrypted());
        }

    }
}
