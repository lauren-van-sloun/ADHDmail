using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADHDmail.API
{
    class GmailQueryFilter
    {
        public GmailQueryFilterOption Filter { get; set; }
        public string Value { get; set; }

        public GmailQueryFilter(GmailQueryFilterOption filter, string value = "")
        {
            this.Filter = filter;
            this.Value = value;
        }
    }
}
