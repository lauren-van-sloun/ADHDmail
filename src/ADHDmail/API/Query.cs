using ADHDmail.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADHDmail.API
{
    /// <summary>
    /// The abstract, base class that represents a query used to retrieve messages from an email API.
    /// </summary>
    public abstract class Query
    {
        /// <summary>
        /// Represents the fully constructed query for filtering use in querying email APIs. 
        /// </summary>
        protected string RawQuery;

        /// <summary>
        /// Sets the <see cref="RawQuery"/> to <see cref="string.Empty"/> which represents a query 
        /// with no filtering. This will construct a query that will return all emails.
        /// </summary>
        protected Query()
        {
            RawQuery = "";
        }

        /// <summary>
        /// Constructs the <see cref="RawQuery"/> to contain the filters supplied.
        /// </summary>
        /// <param name="queryFilters">Represents the filters to apply to the query.</param>
        protected Query(List<Filter> queryFilters)
        {
            RawQuery = ConstructQuery(queryFilters);
        }

        /// <summary>
        /// Parses the <see cref="Filter"/>s provided into a query string that is usable 
        /// in an API query.
        /// </summary>
        /// <param name="queryFilters">Represents the filters to apply to the query.</param>
        /// <returns>Returns the fully constructed query.</returns>
        protected abstract string ConstructQuery(List<Filter> queryFilters);

        /// <summary>
        /// Represents the fully constructed query for use in the <see cref="GmailApi.GetMessage(string)"/> 
        /// method. 
        /// <para>If this returns <see cref="string.Empty"/>, then no filtering is applied to the query.</para>
        /// </summary>
        /// <returns>Returns the fully constructed query.</returns>
        public override string ToString() => RawQuery;
    }
}
