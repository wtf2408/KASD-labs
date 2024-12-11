using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace kasd_labs_console
{
    struct RequestInfo
    {
        public double time;
        public int priority;
        public int number;
        public int step;
        public RequestInfo(double time, int priority, int number, int step)
        {
            this.time = time;
            this.priority = priority;
            this.number = number;
            this.step = step;
        }
    }
}
