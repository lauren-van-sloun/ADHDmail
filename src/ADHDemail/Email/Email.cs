using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADHDemail.Email
{
    /// <summary>
    /// The abstract base class for all email objects. 
    /// </summary>
    public abstract class Email
    {
        #region Headers
        /// <summary>
        /// A description of the topic of the message.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// The sender's email address.
        /// </summary>
        public string Sender { get; set; }

        /// <summary>
        /// The date and time the message was received.
        /// </summary>
        public DateTime TimeReceived { get; set; }
        #endregion

        #region Body
        /// <summary>
        /// The main text content of the email.
        /// </summary>
        public string Body { get; set; }
        #endregion
    }
}
