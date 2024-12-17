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
            MyHashSet<int> set = new MyHashSet<int>();

            set.AddAll(Enumerable.Range(1, 10).ToArray());
            set.Remove(3);
            set.Add(5);
            set.Add(5);


            int[] newArray = set.ToArray();
            for (int i = 0; i < newArray.Length; i++)
                Console.Write(newArray[i] + " ");


        }

        
    }
}
        
