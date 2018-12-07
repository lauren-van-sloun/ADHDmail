using ADHDmail.API;
using ADHDmail.Config;
using System;
using System.Collections.Generic;

namespace ADHDmail
{
    class Program
    {
        public static void Main(string[] args)
        {
            //var emailFetcher = new EmailFetcher(new GmailApi(), new mockConfigFile());
            //emailFetcher.SetTimer(callback: PrintEmails);
            //emailFetcher.Start();
            //Console.ReadKey();
            //emailFetcher.Stop();

            var queryConfig = new QueryScheduleConfigFile();
            queryConfig.UpdateFrequency(30000);
        }

        public static bool PrintEmails(List<Email> emails)
        {
            emails.ForEach(email =>
                Console.WriteLine($"Email ID: {email.Id} Subject: {email.Subject} Time: {email.TimeReceived}"));

            Console.WriteLine("-----------------------");

            // do whatever you want here to handle the response...
            // you can write it out, store in a queue, put in a member, etc.
            // check result to see if it's a certain value...
            // if it should keep going, return true, otherwise return false

            return true;
        }
    }
}
