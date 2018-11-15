namespace ADHDmail.Config
{
    /// <summary>
    /// Search operators used to filter message search results.
    /// </summary>
    public enum FilterOption
    {
        /// <summary>
        /// Specify the sender's name or email address
        /// </summary>
        From,
        /// <summary>
        /// Specify a recipient's name or email address
        /// </summary>
        To,
        /// <summary>
        /// Words in the subject line
        /// </summary>
        Subject,
        /// <summary>
        /// Messages that have a certain label
        /// </summary>
        Label,
        /// <summary>
        /// Messages that have an attachment
        /// </summary>
        HasAttachment,
        /// <summary>
        /// Attachments with a certain name or file type
        /// </summary>
        HasFilename,
        /// <summary>
        /// Search for an exact word or phrase
        /// </summary>
        ContainsWord,
        /// <summary>
        /// Messages in any folder, including Spam and Trash	
        /// </summary>
        AllFolders,
        /// <summary>
        /// Search for messages that are starred
        /// </summary>
        Starred,
        /// <summary>
        /// Search for messages that are unread
        /// </summary>
        Unread,
        /// <summary>
        /// Search for messages that are read
        /// </summary>
        Read,
        /// <summary>
        /// Search for messages sent before a certain time period
        /// </summary>
        After,
        /// <summary>
        /// Search for messages sent after a certain time period
        /// </summary>
        Before,
        /// <summary>
        /// Messages delivered to a certain email address
        /// </summary>
        DeliveredTo,
        /// <summary>
        /// Messages larger than a certain size in bytes
        /// </summary>
        LargerThan,
        /// <summary>
        /// Messages smaller than a certain size in bytes	
        /// </summary>
        SmallerThan,
        /// <summary>
        /// Results that match a word exactly
        /// </summary>
        MatchesWordExactly
    }
}
