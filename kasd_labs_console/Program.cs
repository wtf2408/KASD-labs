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
            var list = new MyLinkedList<int>();

            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add(2, 4);
            list.Print();

            int[] array = { 4, 5, 6, 7, 8, 9, 10 };
            list.AddAll(2, array);

            list.AddLast(11);
            list.AddLast(12);
            list.Print();
            Console.WriteLine(list.RemoveLastOccurrence(11));
            list.Print();
        }
        
    }
}
        
