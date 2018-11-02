using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using GmailMessage = Google.Apis.Gmail.v1.Data.Message;

namespace ADHDmail.API
{
    /// <summary>
    /// The Gmail implementation of IEmailApi.
    /// <para>Uses the <a href="https://developers.google.com/api-client-library/dotnet/apis/gmail/v1">Gmail API</a> to query emails. 
    /// </para>
    /// </summary>
    public class GmailApi : IEmailApi
    {
        private UserCredential _credential;
        /// <summary>
        /// Initializes a new instance of the <see cref="GmailApi"/> class.
        /// </summary>
        public GmailApi()
        {
           _credential = GetCredential(@"C:\Users\Ashie\AppData\Roaming\ADHDmail\GmailOAuth.json");
        }

        internal UserCredential GetCredential(string path)
        {
            // If modifying these scopes, delete your previously saved credentials
            // at ~/.credentials/gmail-dotnet-quickstart.json
            string[] scopes = { GmailService.Scope.GmailReadonly };

            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                string credPath = "token.json";

                Console.WriteLine("Credential file saved to: " + credPath);

                return GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            }
        }

        /// <summary>
        /// List all Messages of the user's mailbox matching the query.
        /// </summary>
        /// <param name="service">Gmail API service instance.</param>
        /// <param name="userId">User's email address. The special value "me"
        /// can be used to indicate the authenticated user.</param>
        /// <param name="query">String used to filter Messages returned.</param>
        public static List<GmailMessage> ListMessages(
            GmailService service, string userId, string query)
        {
            var result = new List<GmailMessage>();
            UsersResource.MessagesResource.ListRequest request = service.Users.Messages.List(userId);
            request.Q = query;

            do
            {
                try
                {
                    ListMessagesResponse response = request.Execute();
                    result.AddRange(response.Messages);
                    request.PageToken = response.NextPageToken;
                }
                catch (Exception e)
                {
                    Console.WriteLine("An error occurred: " + e.Message);
                }
            }
            while (!string.IsNullOrEmpty(request.PageToken));

            return result;
        }

        // code modified from SO
        // https://stackoverflow.com/questions/36448193/how-to-retrieve-my-gmail-messages-using-gmail-api
        internal List<Email.Email> GetEmails()
        {
            var emails = new List<Email.Email>();
            string fullName = Assembly.GetEntryAssembly().Location;
            string applicationName = Path.GetFileNameWithoutExtension(fullName);

            try
            {
                var gmailService = new GmailService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = _credential,
                    ApplicationName = applicationName
                });

                // The user's email address. The special value "me" can be used to indicate the authenticated user.
                const string userId = "me";
                var gmailMessages = ListMessages(gmailService, userId, /*"is:unread"*/ "");
                
                if (gmailMessages != null && gmailMessages.Count > 0)
                {
                    foreach (var message in gmailMessages)
                    {
                        var gmailMessage = GetMessage(gmailService, userId, message.Id);
                        // BODY IS NULL, work on this next
                        var body = gmailMessage.Payload.Body?.Data;
                        var convertedBody = ConvertCharactersFromBase64(body);

                        var emailToAdd = new Email.Email
                        {
                            Account = "Gmail",
                            Body = convertedBody
                        };

                        ExtractDataFromHeader(ref emailToAdd, gmailMessage);
                        emails.Add(emailToAdd);
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to get messages!");
            }
            return emails;
        }

        private void ExtractDataFromHeader(ref Email.Email email, GmailMessage message)
        {
            // loop through the headers to get the rest of the fields I need...
            foreach (var header in message.Payload.Headers)
            {
                switch (header.Name)
                {
                    case "Date":
                        email.TimeReceived = ConvertToDateTime(header.Value);
                        break;
                    case "From": // or this could be sender's name?
                        email.SendersEmail = header.Value;
                        break;
                    case "Subject":
                        email.Subject = header.Value;
                        break;
                    default:
                        break;
                }
            }
        }

        // Make these names more descriptive and maybe write xml doc for this method instead of explaining below
        // need to replace some characters as the data for the email's body is base64
        private string ConvertCharactersFromBase64(string body)
        {
            string codedBody = body.Replace("-", "+");
            codedBody = codedBody.Replace("_", "/");
            byte[] data = Convert.FromBase64String(codedBody);
            body = Encoding.UTF8.GetString(data);
            return body;
        }

        // Move this method somewhere else
        private DateTime ConvertToDateTime(string date)
        {
            var result = new DateTime();
            if (!string.IsNullOrWhiteSpace(date))
                DateTime.TryParse(date, out result);
            return result;
        }

        /// <summary>
        /// Retrieve a Message by ID.
        /// </summary>
        /// <param name="service">Gmail API service instance.</param>
        /// <param name="userId">User's email address. The special value "me"
        /// can be used to indicate the authenticated user.</param>
        /// <param name="messageId">ID of Message to retrieve.</param>
        public static GmailMessage GetMessage(GmailService service, string userId, string messageId)
        {
            try
            {
                return service.Users.Messages.Get(userId, messageId).Execute();
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: " + e.Message);
            }

            return null;
        }
    }
}
