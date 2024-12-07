using System;
using System.Collections.Generic;
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
            int[] arr = {5, 9, 12, 3, 0};
            MyHeap<int> heap = new MyHeap<int>(arr);

            var max = heap.Max();
            Console.WriteLine($"Max : {max}");

            Console.WriteLine("\nHeap:");
            heap.Print();



            heap.PopMax();
            Console.WriteLine("\nHeap after poping:");
            heap.Print();

            var otherArr = new int[] { 6, 9, 1};
            var otherHeap = new MyHeap<int>(otherArr);

            heap.Merge(otherHeap);

            Console.WriteLine("\nHeap after merge");
            heap.Print();

        }
        
    }
}
