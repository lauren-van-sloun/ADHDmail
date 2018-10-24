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
