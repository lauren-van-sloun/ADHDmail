using ADHDmail.API;
using ADHDmail.Config;
using System;
using System.Collections.Generic;

namespace ADHDmailTests.Mocks
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
