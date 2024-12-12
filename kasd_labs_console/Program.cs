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
            MyArrayDeque<string> myArrayDeque = new MyArrayDeque<string>();

            var dir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);

            string inputFile = $"{Path.Combine(dir.Parent.Parent.FullName, "input.txt")}";
            string sortedFile = $"{Path.Combine(dir.Parent.Parent.FullName,"sorted.txt")}";
            string line;

            using (StreamReader reader = new StreamReader(inputFile, encoding: Encoding.UTF8))
            {
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    if (SpacesCount(line) > 0)
                    {
                        if (!myArrayDeque.IsEmpty())
                        {
                            string first = myArrayDeque.GetFirst();
                            if (DigitsCount(first) < DigitsCount(line))
                            {
                                myArrayDeque.AddLast(line);
                            }
                            else
                            {
                                myArrayDeque.AddFirst(line);
                            }
                        }
                        else
                        {
                            myArrayDeque.Add(line);
                        }
                    }
                }
            }
            Console.Write("Enter spances count:");
            int n = Convert.ToInt32(Console.ReadLine());
            using (StreamWriter writer = new StreamWriter(sortedFile, false, encoding: Encoding.UTF8))
            {
                MyArrayDeque<string> result = new MyArrayDeque<string>();
                while (!myArrayDeque.IsEmpty())
                {

                    line = myArrayDeque.RemoveFirst();
                    writer.WriteLine(line);
                    if (SpacesCount(line) < n) result.AddFirst(line);
                }
                myArrayDeque = result;
            }
            Console.WriteLine($"deque after removing stirngs, that contain greater {n} spaces");

            var len = myArrayDeque.Size();
            for (int i = 0; i < len; i++)
            {
                Console.WriteLine(myArrayDeque.Pop());
            }
        }
        static int DigitsCount(string str)
        {
            int result = 0;
            foreach (var symbol in str)
            {
                if (Char.IsDigit(symbol)) result++;
            }
            return result;
        }
        static int SpacesCount(string str)
        {
            int result = 0;
            foreach (var symbol in str)
            {
                if (symbol == ' ') result++;
            }
            return result;
        }
    }
}
        
