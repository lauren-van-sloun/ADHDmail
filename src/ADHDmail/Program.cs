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
            var api = new API.GmailApi();
            api.GetEmails();
        }
    }
}
