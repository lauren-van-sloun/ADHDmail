using ADHDmail.API;

namespace ADHDmail
{
    class Program
    {
        public static void Main(string[] args)
        {
            var api = new GmailApi();
            var queryFilters = new List<GmailQueryFilter>()
            {
                new GmailQueryFilter(GmailQueryFilterOption.Unread),
                new GmailQueryFilter(GmailQueryFilterOption.LargerThan, "1"),
                new GmailQueryFilter(GmailQueryFilterOption.ContainsWord, "DogFoodCon")
            };
            var query = new GmailQuery(queryFilters);
            var unreadEmails = api.GetEmails(query);

            unreadEmails.ForEach(e => 
                Console.WriteLine($"Email ID: {e.Id} Time received: {e.TimeReceived}. Subject: {e.Subject}"));
        
        }
    }
}
