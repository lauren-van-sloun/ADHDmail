using ADHDmail.API;

namespace ADHDmail
{
    class Program
    {
        public static void Main(string[] args)
        {
            // use thunderbird
            var api = new GmailApi();
            api.GetEmails();
        }
    }
}
