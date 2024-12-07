using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace kasd_labs_console
{
    class MyHeap<T> where T : IComparable<T>
    {
        private T[] elementData;
        private int elementCount;
        public MyHeap(T[] array)
        {
            elementCount = array.Length;
            elementData = new T[elementCount];
            for (int i = 0; i < elementCount; i++) { elementData[i] = array[i]; }
            Rebuild();
        }
        private void Rebuild()
        {
            for (int i = elementCount - 1; i >= 0; i--)
            {
                int left, right;
                int parent = i;
                bool flag;
                do
                {
                    flag = false;
                    left = 2 * i + 1;
                    right = 2 * i + 2;
                    if (left < elementCount && elementData[left].CompareTo(elementData[parent]) > 0)
                    {
                        parent = left;
                    }
                    if (right < elementCount && elementData[right].CompareTo(elementData[parent]) > 0)
                    {
                        parent = right;
                    }
                    if (parent != i)
                    {
                        var temp = elementData[i];
                        elementData[i] = elementData[parent];
                        elementData[parent] = temp;
                        i = parent;
                        flag = true;
                    }
                }
                while (flag);
            }
        }
        public T Max() => elementData[0];
        
        public T PopMax()
        {
            T max = Max();
            for (int i = 0; i < elementCount - 1; i++)
                elementData[i] = elementData[i + 1];

            elementCount--;
            Rebuild();
            return max;
        }

        public T this[int index]
        {
            get => elementData[index];
            set {
                elementData[index] = value;
                Rebuild();
            }
        }
        public void Add(T element)
        {
            if (elementCount < elementData.Length)
            {
                elementData[elementCount] = element;
                elementCount++;
                Rebuild();
                return;
            }
            T[] newElementData = new T[2 * elementData.Length + 1];
            for (int i = 0; i < elementCount; i++)
                newElementData[i] = elementData[i];

            newElementData[elementCount] = element;
            elementData = newElementData;
            elementCount++;
            Rebuild();
        }
        public int Size { get => elementCount; } 
        public void Merge(MyHeap<T> myHeap)
        {
            T[] newElementData = new T[elementCount + myHeap.Size];
            for (int i = 0; i < elementCount; i++) { newElementData[i] = elementData[i]; }

            for (int i = 0; i < myHeap.Size; i++) { newElementData[elementCount + i] = myHeap[i]; }

            elementData = newElementData;
            elementCount += myHeap.Size;
            Rebuild();
        }

        public void Print(int i = 0)
        {
            if (i  == 0) Console.Write($"{elementData[i]} ");
            if (2 * i + 1 > elementCount - 1) return;

            Console.Write($"{elementData[2*i+1]} {elementData[2 * i + 1]} ");
            Print(2*i+1);
            Print(2*i+2);
        }
    }
}

