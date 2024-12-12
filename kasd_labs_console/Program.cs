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
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var hashMap = new MyHashMap<string, int>();

            hashMap.Put("item", 1);
            hashMap.Put("biba", 2);
            hashMap.Put("boba", 3);

            //hashMap.ContainsValue(3);
            //hashMap.ContainsKey("imem 3");
            //var a = hashMap.Get("item 3");
            //Console.WriteLine(a);

            var col = hashMap.EntrySet();
            foreach (var item in col)
            {
                Console.WriteLine($"{item.Key} {item.Value}");
            }
        }

    }
}
        
