using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


namespace kasd_labs_console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MyVector<int> vector = new MyVector<int>();
            vector.Add(1);
            vector.Add(2);
            vector.Add(3);

            var a = vector.FirstElement();
            var b = vector.LastElement();
            Console.WriteLine($"{a} {b}");
        }
    }
}
