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
            int[] mass = { 7, 3, 9, 2, 0, 18 };
            MyPriorityQueue<int> firstQueue = new MyPriorityQueue<int>(mass);
            Console.WriteLine("First Queue now:");
            for (int i = 0; i < firstQueue.Size; i++) { Console.Write(firstQueue[i] + " "); }
            Console.WriteLine();


            Console.WriteLine($"Poll operation: {firstQueue.Poll()}");
            Console.WriteLine("After Poll operation:");
            for (int i = 0; i < firstQueue.Size; i++) { Console.Write(firstQueue[i] + " "); }
            Console.WriteLine();

            int[] mass2 = { 9, 3, 8, 2, 19 };
            MyPriorityQueue<int> SecondQueue = new MyPriorityQueue<int>(mass2);
            Console.WriteLine("Second Queue:");
            for (int i = 0; i < SecondQueue.Size; i++) { Console.Write(firstQueue[i] + " "); }


            MyPriorityQueue<int> ThirdQueue = new MyPriorityQueue<int>(SecondQueue);
            Console.WriteLine("\nThird Queue (copy second queue):");
            for (int i = 0; i < ThirdQueue.Size; i++) { Console.Write(firstQueue[i] + " "); }


            Console.ReadLine();

        }
        
    }
}
