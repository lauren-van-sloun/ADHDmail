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

        // first attempt
        public void GetMessages()
        {
            string fullName = Assembly.GetEntryAssembly().Location;
            string applicationName = Path.GetFileNameWithoutExtension(fullName);

            try
            {
                // Create Gmail API service.
                var service = new GmailService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = _credential,
                    ApplicationName = applicationName,
                });

                // Define parameters of request.;
                UsersResource.MessagesResource.ListRequest request = service.Users.Messages.List("me");

                // List messages.
                IList<Google.Apis.Gmail.v1.Data.Message> messages = request.Execute().Messages;
                Console.WriteLine("Messages:");
                if (messages != null && messages.Count > 0)
                {
                    foreach (var message in messages)
                    {
                        Console.WriteLine();
                        Console.WriteLine(message.Payload.Body.ToString());
                        Console.WriteLine(message.Raw);
                        Console.WriteLine(message.Snippet);
                    }
                }
                else
                {
                    Console.WriteLine("No messages found.");
                }
                Console.Read();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.WriteLine(ex.Message);

            }
        }

        // code modified from SO
        // https://stackoverflow.com/questions/36448193/how-to-retrieve-my-gmail-messages-using-gmail-api
        internal async Task GetEmails()
        {
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

                var emailListRequest = gmailService.Users.Messages.List(userId);
                emailListRequest.LabelIds = "INBOX";
                emailListRequest.IncludeSpamTrash = false;
                emailListRequest.Q = "is:unread"; //this was added because I only wanted unread emails...

                //get our emails 
                // This line is crashing the app
                var emailListResponse = await emailListRequest.ExecuteAsync();

                if (emailListResponse?.Messages != null)
                {
                    // loop through each email and get what fields I want...
                    foreach (var email in emailListResponse.Messages)
                    {
                        var emailInfoRequest = gmailService.Users.Messages.Get("EMAIL ADDRESS HERE", email.Id);
                        // make another request for that email id...
                        var emailInfoResponse = await emailInfoRequest.ExecuteAsync();

                        if (emailInfoResponse != null)
                        {
                            string from = "";
                            string date = "";
                            string subject = "";
                            string body = "";
                            // loop through the headers and get the fields I need...
                            foreach (var mParts in emailInfoResponse.Payload.Headers)
                            {
                                switch (mParts.Name)
                                {
                                    case "Date":
                                        date = mParts.Value;
                                        break;
                                    case "From":
                                        from = mParts.Value;
                                        break;
                                    case "Subject":
                                        subject = mParts.Value;
                                        break;
                                }

                                if (date == "" || from == "")
                                    continue;
                                if (emailInfoResponse.Payload.Parts == null && emailInfoResponse.Payload.Body != null)
                                {
                                    body = emailInfoResponse.Payload.Body.Data;
                                    Console.WriteLine(body);
                                }
                                else
                                    body = GetNestedParts(emailInfoResponse.Payload.Parts, "");
                                // need to replace some characters as the data for the email's body is base64
                                string codedBody = body.Replace("-", "+");
                                codedBody = codedBody.Replace("_", "/");
                                byte[] data = Convert.FromBase64String(codedBody);
                                body = Encoding.UTF8.GetString(data);

                                // now I have the data I want....
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to get messages!");
            }
        }

        static string GetNestedParts(IList<MessagePart> part, string curr)
        {
            string str = curr;
            if (part == null)
            {
                return str;
            }

            foreach (var parts in part)
            {
                if (parts.Parts == null)
                {
                    if (parts.Body != null && parts.Body.Data != null)
                        str += parts.Body.Data;
                }
                else
                    return GetNestedParts(parts.Parts, str);
            }

            return str;
        }
    }
}
