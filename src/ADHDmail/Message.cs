using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADHDmail
{
    /// <summary>
    /// The abstract base class for all message objects. 
    /// </summary>
    public abstract class Message
    {
        /// <summary>
        /// The recipient's account that the message belongs to.
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// The date and time the message was received.
        /// </summary>
        public DateTime TimeReceived { get; set; }

        /// <summary>
        /// The name of the person who sent the message.
        /// </summary>
        public string SendersName { get; set; }

        /// <summary>
        /// The main text content of the message.
        /// </summary>
        public string Body { get; set; }
    }
}
