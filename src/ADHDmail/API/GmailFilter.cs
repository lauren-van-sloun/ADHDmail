using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADHDmail.API
{
    class GmailFilter
    {
        public GmailFilterOption Filter { get; set; }
        public string Value { get; set; }

        public GmailFilter(GmailFilterOption filter, string value = "")
        {
            this.Filter = filter;
            this.Value = value;
        }
    }
}
