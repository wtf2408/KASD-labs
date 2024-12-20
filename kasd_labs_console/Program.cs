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
            var a = Enumerable.Range(1, 10).ToArray();
           
            MyTreeSet<int> treeSet = new MyTreeSet<int>();
            treeSet.AddAll(a);
            MyHashSet<int> hashSet = new MyHashSet<int>(a);
            MyPriorityQueue<int> priorityQueue = new MyPriorityQueue<int>(a);
            MyArrayDeque<int> arrayDeque = new MyArrayDeque<int>(a);

            Console.WriteLine("treeSet: ");
            PrintCollection(treeSet.Iterator());

            Console.WriteLine("hashSet: ");
            PrintCollection(hashSet.Iterator());

            Console.WriteLine("priorityQueue: ");
            PrintCollection(priorityQueue.Iterator());

            Console.WriteLine("arrayDeque: ");
            PrintCollection(arrayDeque.Iterator());

            Console.WriteLine("Нажми для продолжения"); Console.ReadKey(); Console.Clear();

            MyArrayList<int> arrayList = new MyArrayList<int>(a);
            MyVector<int> vector = new MyVector<int>(a);
            MyLinkedList<int> linkedList = new MyLinkedList<int>(a);

            Console.WriteLine("arrayList: ");
            PrintCollection(arrayList.ListIterator());

            Console.WriteLine("arrayList: ");
            PrintCollection(vector.ListIterator());

            Console.WriteLine("arrayList: ");
            PrintCollection(linkedList.ListIterator());

        }

        static void PrintCollection<T>(MyIterator<T> myIterator)
        {
            while (myIterator.HasNext())
            {
                Console.Write($"{myIterator.Next()} ");
            }
            Console.WriteLine();
        }
    }
}
        
