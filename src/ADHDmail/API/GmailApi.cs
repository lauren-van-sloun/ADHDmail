using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using GmailMessage = Google.Apis.Gmail.v1.Data.Message;

namespace ADHDmail.API
{
    /// <summary>
    /// The Gmail implementation of <see cref="IEmailApi"/>.
    /// <para>
    /// Uses the <a href="https://developers.google.com/api-client-library/dotnet/apis/gmail/v1">Gmail API</a> 
    /// to query emails. 
    /// </para>
    /// </summary>
    public class GmailApi : IEmailApi
    {
        private GmailService _gmailService;

        // User's email address. The special value "me" can be used to indicate the authenticated user.
        private const string userId = "me";

        private UserCredential _credential;

        /// <summary>
        /// Initializes a new instance of the <see cref="GmailApi"/> class.
        /// </summary>
        public GmailApi()
        {
            _credential = GetCredential(
                Path.Combine(
                    GlobalValues.AppDataPath,
                    GlobalValues.ApplicationName,
                    "GmailOAuth.json"));
            PopulateService();
        }

        private UserCredential GetCredential(string path)
        {
            string[] scopes = { GmailService.Scope.GmailReadonly };

            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                string credPath = "token.json";

                return GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            }
        }

        private void PopulateService()
        {
            string fullName = Assembly.GetEntryAssembly().Location;
            string applicationName = Path.GetFileNameWithoutExtension(fullName);

            _gmailService = new GmailService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = _credential,
                ApplicationName = applicationName
            });
        }

        /// <summary>
        /// Retrieves a list of <see cref="Email"/>s from the user's mailbox matching the query.
        /// </summary>
        /// <param name="query">Represents a query to retrieve messages from an email API.</param>
        List<Email> IEmailApi.GetEmails(Query query)
        {
            return this.GetEmails((GmailQuery)query);
        }

        /// <summary>
        /// Retrieves a list of <see cref="Email"/>s from the user's mailbox matching the query.
        /// </summary>
        /// <param name="query">Represents a query to retrieve messages from the Gmail API.</param>
        public List<Email> GetEmails(GmailQuery query)
        {
            var emails = new List<Email>();

            try
            {
                var gmailMessages = ListMessages(query.ToString());

                if (gmailMessages != null && gmailMessages.Count > 0)
                {
                    foreach (var message in gmailMessages)
                    {
                        var gmailMessage = GetMessage(message.Id);
                        emails.Add(ConvertToEmail(gmailMessage));
                    }
                }
            }
            catch (Exception ex)
            {
                LogWriter.Write($"Failed to retrieve emails. {ex.GetType()}: \"{ex.Message}\"");
            }
            return emails;
        }

        /// <summary>
        /// List all Messages of the user's mailbox matching the query.
        /// </summary>
        /// <param name="query">String used to filter Messages returned. By default, 
        /// returns the full email message data with body content parsed in the payload field.</param>
        private List<GmailMessage> ListMessages(string query = "")
        {
            var result = new List<GmailMessage>();
            UsersResource.MessagesResource.ListRequest request = _gmailService.Users.Messages.List(userId);
            request.Q = query;

            do
            {
                // might not need this try-catch block since it is nested in another
                // try-catch block that is in the calling method
                try
                {
                    ListMessagesResponse response = request.Execute();
                    if (response.Messages == null)
                        return null;
                    result.AddRange(response.Messages);
                    request.PageToken = response.NextPageToken;
                }
                catch (Exception ex)
                {
                    LogWriter.Write($"Could not retrieve messages from Gmail. {ex.GetType()}: \"{ex.Message}\"");
                }
            }
            while (!string.IsNullOrEmpty(request.PageToken));

            return result;
        }

        /// <summary>
        /// Retrieve a message by ID.
        /// </summary>
        /// <param name="messageId">The ID of the message to retrieve.</param>
        private GmailMessage GetMessage(string messageId)
        {
            try
            {
                return _gmailService.Users.Messages.Get(userId, messageId).Execute();
            }
            catch (Exception ex)
            {
                LogWriter.Write($"Failed to retrieve the gmail message with an id of {messageId}. " +
                    $"{ex.GetType()}: \"{ex.Message}\"");
                return null;
            }
        }

        /// <summary>
        /// Retrieves an email by ID.
        /// </summary>
        /// <param name="emailId">The ID of the email to retrieve.</param>
        public Email GetEmail(string emailId)
        {
            var gmailMessage = GetMessage(emailId);
            return ConvertToEmail(gmailMessage);
        }

        private Email ConvertToEmail(GmailMessage gmailMessage)
        {
            var body = GetBody(gmailMessage.Payload.Parts);

            var email = new Email
            {
                Id = gmailMessage.Id,
                Account = "Gmail",
                Body = body
            };

            ExtractDataFromHeader(ref email, gmailMessage);
            return email;
        }

        private string GetBody(IList<MessagePart> parts)
        {
            foreach (MessagePart part in parts)
            {
                if (part.Body != null)
                {
                    if (part.MimeType == "text/html" || part.MimeType == "text/plain")
                        return Decode(part.Body.Data);
                }
            }
            return GetBody(parts[0].Parts);
        }

        private string Decode(string body)
        {
            string codedBody = body.Replace("-", "+");
            codedBody = codedBody.Replace("_", "/");
            byte[] convertedBody = Convert.FromBase64String(codedBody);
            return Encoding.UTF8.GetString(convertedBody);
        }

        private void ExtractDataFromHeader(ref Email email, GmailMessage message)
        {
            foreach (var header in message.Payload.Headers)
            {
                switch (header.Name)
                {
                    case "Date":
                        email.TimeReceived = header.Value.ToDateTime();
                        break;
                    case "From":
                        PopulateSenderData(email, header.Value);
                        break;
                    case "Subject":
                        email.Subject = header.Value;
                        break;
                }
            }
        }

        private void PopulateSenderData(Email email, string fromHeader)
        {
            Regex sendersNameRegex = new Regex(@"[^<]*");
            Regex sendersEmailRegex = new Regex(@"<(.+)>");
            email.SendersName = sendersNameRegex.Match(fromHeader).Value.Trim();
            email.SendersEmail = sendersEmailRegex.Match(fromHeader).Groups[1].Value;
        }
    }
}
