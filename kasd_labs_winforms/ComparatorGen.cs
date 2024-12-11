using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kasd_labs_winforms
{
    public static class ComparatorGen
    {
        public static Comparer<T> GetComparer<T>()
        {
             return Comparer<T>.Default;
        }
    }

    public class StringComparer : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            return x.Length - y.Length;
        }
    }
}
