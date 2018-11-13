namespace ADHDmail
{
    /// <summary>
    /// Represents a standard email.
    /// </summary>
    public class Email : Message
    {
        /// <summary>
        /// The immutable ID of the message.
        /// </summary>
        public string Id { get; set; }

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
