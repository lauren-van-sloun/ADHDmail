using System.Collections.Generic;

namespace ADHDmail.API
{
    interface IEmailApi
    {
        List<Email> GetEmails(Query query);
        Email GetEmail(string id);
    }
}