using System.Collections.Generic;

namespace ADHDmail.API
{
    public interface IEmailApi
    {
        List<Email> GetEmails(Query query);
        Email GetEmail(string id);
    }
}