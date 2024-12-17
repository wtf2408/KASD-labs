using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using kasd_labs_console;



namespace kasd_labs_console
{
    class Program
    {
        static void Main()
        {
            MyTreeMap<int, string> treeMap = new MyTreeMap<int, string>();
            
            foreach (var item in Enumerable.Range(1, 10))
            {
                treeMap.Put(item*2, $"Value {item*2}");
            }

            foreach (var item in Enumerable.Range(1, 10))
            {
                treeMap.Put(item, $"Value {item}");
            }
            
            
        }

        
    }
}
        
