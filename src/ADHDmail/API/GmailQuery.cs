using ADHDmail.Config;
using System.Collections.Generic;
using System.Text;

namespace ADHDmail.API
{
    /// <summary>
    /// Represents a query used to retrieve emails from the Gmail API.
    /// </summary>
    public class GmailQuery : Query
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GmailQuery"/> class with no filtering. 
        /// This will construct a query that will return all emails.
        /// </summary>
        public GmailQuery() : base()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ADHDmail.API.GmailQuery" /> class with filters 
        /// to apply to the query.
        /// </summary>
        /// <param name="queryFilters">Represents the filters to apply to the query.</param>
        public GmailQuery(List<Filter> queryFilters) : base(queryFilters)
        { }

        /// <summary>
        /// Parses the <see cref="Filter"/>s provided into a query string that is usable 
        /// in a <see cref="GmailApi"/> query.
        /// </summary>
        /// <param name="queryFilters">Represents the filters to apply to the query.</param>
        /// <returns>Returns the fully constructed query.</returns>
        protected override string ConstructQuery(List<Filter> queryFilters)
        {
            var queryBuilder = new StringBuilder();

            for (int i = 0; i < queryFilters.Count; i++)
            {
                queryBuilder.Append(queryFilters[i]);
                if (i != queryFilters.Count - 1)
                    queryBuilder.Append(' ');
            }

            return queryBuilder.ToString();
        }
    }
}