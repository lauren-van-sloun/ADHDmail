using ADHDmail.API;
using ADHDmail.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
//using System.Timers;

namespace ADHDmail
{
    class Program
    {
        public static void Main(string[] args)
        {
            // Approach using System.Timers
            //EmailFetcher fetcher = new EmailFetcher(5000);
            //fetcher.Start();
            //Console.ReadKey();
            //fetcher.Stop();

            // Approach using System.Threading
            EmailFetcherUsingThreading fetcher = new EmailFetcherUsingThreading(8);
            
        }
    }
}
