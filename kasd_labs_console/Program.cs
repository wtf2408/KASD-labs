﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kasd_labs_console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var list = new MyArrayList<int>(new int[] { 1, 3, 5});
            list.Add(1);
            list[0] = 2;
        }
    }
}
