using ADHDmail.API;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADHDmail
{
    class Program
    {
        public static void Main(string[] args)
        {
            var api = new GmailApi();
            var unreadEmails = api.GetEmails(GmailQueryParameter.Unread);

            unreadEmails.ForEach(e => 
                Console.WriteLine($"Time received: {e.TimeReceived}. Subject: {e.Subject}"));
        
        }
    }
}
