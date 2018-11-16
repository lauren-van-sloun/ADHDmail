using System.Collections.Generic;

namespace ADHDmail.API
{
    /// <summary>
    /// Uses an email API with readonly access to query emails. 
    /// </summary>
    public interface IEmailApi
    {
        /// <summary>
        /// Gets a list of emails from the user's mailbox.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        List<Email> GetEmails(Query query);

        /// <summary>
        /// Gets a specified email from the user's mailbox.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Email GetEmail(string id);
    }
}