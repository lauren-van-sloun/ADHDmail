using ADHDmail;
using ADHDmail.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADHDmailTests.Mocks
{
    internal class MockApi : IEmailApi
    {
        public Email GetEmail(string id)
        {
            return new Email() { Id = id };
        }

        public List<Email> GetEmails(Query query)
        {
            return new List<Email>();
        }
    }
}
