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
        static void Main(string[] args)
        {
            var deque = new MyArrayDeque<int>();
            int[] plainArray = Enumerable.Range(1, 20).ToArray();

            deque.AddAll(plainArray);
            deque.Add(21);
            deque.Add(22);
            Console.WriteLine($"Первый эл.: {deque.GetFirst()}");
            Console.WriteLine($"Последний эл.: {deque.GetLast()}");

            Console.WriteLine($"Попный эл.: {deque.Pop()}");
            Console.WriteLine($"Первый эл.: {deque.GetFirst()}");
            Console.WriteLine($"Последний эл.: {deque.GetLast()}");
        }
    }
}
        
