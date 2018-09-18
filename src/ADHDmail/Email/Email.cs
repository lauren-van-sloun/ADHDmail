using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADHDmail.Email
{
    /// <summary>
    /// The base class for all email objects. 
    /// </summary>
    public class Email : Message
    {
        /// <summary>
        /// A description of the topic of the message.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// The sender's email address.
        /// </summary>
        public string SendersEmail { get; set; }
    }
}
