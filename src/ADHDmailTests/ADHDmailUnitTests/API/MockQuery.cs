using ADHDmail.API;
using ADHDmail;
using System;
using System.Collections.Generic;

namespace ADHDmailTests.API
{
    internal class MockQuery : Query
    {
        public MockQuery() : base()
        { }

        public MockQuery(List<Filter> queryFilters) : base(queryFilters)
        { }

        protected override string ConstructQuery(List<Filter> queryFilters)
        {
            throw new NotImplementedException();
        }
    }
}
