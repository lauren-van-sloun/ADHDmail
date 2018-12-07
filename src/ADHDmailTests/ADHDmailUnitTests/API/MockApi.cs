using ADHDmail;
using ADHDmail.API;
using System;
using System.Collections.Generic;

namespace ADHDmailTests.API
{
    internal class MockApi : IEmailApi
    {
        public Email GetEmail(string id)
        {
            return new Email()
            {
                Account = "Account name",
                Body = "",
                Id = id,
                SendersEmail = "Sender's email",
                SendersName = "Sender's name",
                Subject = "Subject",
                TimeReceived = DateTime.Now
            };
        }

        public List<Email> GetEmails(Query query)
        {
            return new List<Email>
            {
                new Email()
                {
                    Account = "Account name",
                    Body = "Sample body",
                    Id = "1",
                    SendersEmail = "Sender@domain.com",
                    SendersName = "Bob Martin",
                    Subject = "This is a subject line",
                    TimeReceived = DateTime.Now
                },
                new Email()
                {
                    Account = "Other account name",
                    Body = "Sample body #2",
                    Id = "2",
                    SendersEmail = "Sender@domain2.com",
                    SendersName = "Joe Satriani",
                    Subject = "Sample subject line",
                    TimeReceived = DateTime.Now
                }
            };
        }
    }
}
