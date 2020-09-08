using System;
using System.Collections.Generic;
using System.Text;

namespace Shared
{
    public class FilterDto
    {
        public class FilterObj
        {
            public int Field { get; set; }
            public string Value { get; set; }

            public FilterObj(int filter, string value)
            {
                Field = filter;
                Value = value;
            }
        }
    }
}
