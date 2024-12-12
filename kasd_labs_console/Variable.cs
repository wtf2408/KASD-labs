using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kasd_labs_console
{
    internal class Variable
    {
        public Type Type { get; }
        public string Value { get; }

        public Variable(Type type, string value)
        {
            Type = type;
            Value = value;
        }
    }
}
