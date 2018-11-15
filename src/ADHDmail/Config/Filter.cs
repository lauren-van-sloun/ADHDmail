namespace ADHDmail.Config
{
    /// <summary>
    /// Represents a filter to apply to a message based on the part of the message to 
    /// filter and the value to filter by.
    /// </summary>
    public class Filter
    {
        /// <summary>
        /// Represents the part of the message to filter.
        /// </summary>
        public FilterOption FilterOption { get; set; }

        /// <summary>
        /// Represents the value to filter by.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Filter"/> class.
        /// </summary>
        /// <param name="filterOption">The filter to apply.</param>
        /// <param name="value">The value to filter by. Not needed for certain filters such 
        /// as <see cref="FilterOption.HasAttachment"/>, <see cref="FilterOption.Unread"/>, etc.</param>
        public Filter(FilterOption filterOption, string value = "")
        {
            this.FilterOption = filterOption;
            this.Value = value;
        }
    }
}
