using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kasd_labs_console
{
    public class Entry<TK, TV>
    {
        public TK Key { get; set; }
        public TV Value { get; set; }
        public Entry<TK, TV> Next { get; set; }

        public Entry(TK key, TV value)
        {
            Key = key;
            Value = value;
            Next = null;
        }
    }
}
