using kasd_labs_console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kasd_labs_console
{
    class MyPriorityQueue<T> where T : IComparable<T>
    {
        public class MyItr : MyIterator<T>
        {
            public MyItr(MyPriorityQueue<T> map)
            {
                this.set = map.ToArray().ToList();
            }
            private List<T> set;
            private int index = -1;
            T cursor;
            public bool HasNext()
            {
                return index < set.Count - 1;
            }

            public T Next()
            {
                cursor = set[++index];
                return cursor;
            }

            public void Remove()
            {
                set.Remove(cursor);
            }
        }
        private T[] queue;
        private int size;
        public int Size { get => size; set => size = value; }
        private Comparer<T> comparator;
        public Comparer<T> Comparator
        {
            get => comparator;
            set => comparator = value;
        }
        public MyPriorityQueue()
        {
            queue = new T[10];
            size = 0;
            comparator = Comparer<T>.Default;
        }
        public MyPriorityQueue(T[] mass)
        {
            size = mass.Length;
            queue = new T[size];

            for (int i = 0; i < size; i++)
            {
                queue[i] = mass[i];
            }
            comparator = Comparer<T>.Default;
            Rebuild();
        }
        public MyPriorityQueue(int init_size)
        {
            queue = new T[init_size];
            size = 0;
            comparator = Comparer<T>.Default;
        }
        public MyPriorityQueue(int init_size, Comparer<T> comp)
        {
            queue = new T[init_size];
            size = 0;
            this.comparator = comp;
        }
        public MyPriorityQueue(MyPriorityQueue<T> q)
        {
            queue = q.ToArray();
            size = q.Size;
            comparator = q.Comparator;
        }
        public object this[int index]
        {
            get { return queue[index]; }
        }
        public MyItr Iterator() => new MyItr(this);
        private void Rebuild()
        {
            MyHeap<T> my_heap = new MyHeap<T>(queue);
            for (int i = 0; i < size; i++)
            {
                queue[i] = my_heap[i];
            }
        }
        public void Add(T elem)
        {
            if (size < queue.Length)
            {
                queue[size] = elem;
                size++;
                Rebuild();
                return;
            }

            int new_size;
            if (queue.Length < 64)
            {
                new_size = queue.Length + 2;
            }
            else
            {
                new_size = (int)(queue.Length * 1.5);
            }

            T[] newQueue = new T[new_size];
            for (int i = 0; i < size; i++)
            {
                newQueue[i] = queue[i];
            }

            newQueue[size] = elem;
            queue = newQueue;
            size++;

            Rebuild();
        }
        public void AddAll(T[] mass)
        {
            for (int i = 0; i < mass.Length; i++)
            {
                Add(mass[i]);
            }
        }
        public void Clear()
        {
            size = 0;
        }
        public bool Contains(object o)
        {
            for (int i = 0; i < size; i++)
            {
                if (comparator.Compare((T)o, queue[i]) == 0)
                {
                    return true;
                }
            }

            return false;
        }
        public bool ContainsAll(T[] mass)
        {
            bool check;
            for (int i = 0; i < mass.Length; i++)
            {
                check = false;
                for (int j = 0; j < size; j++)
                {
                    if (comparator.Compare(mass[i], queue[j]) == 0)
                    {
                        check = true;
                    }
                }

                if (!check) { return false; }
            }

            return true;
        }
        public bool IsEmpty()
        {
            return size == 0;
        }
        public void Remove(object o)
        {
            for (int i = 0; i < size; i++)
            {
                if (comparator.Compare((T)o, queue[i]) == 0)
                {
                    for (int j = i; j < size - 1; j++)
                    {
                        queue[j] = queue[j + 1];
                    }
                    i--;
                    size--;
                }
            }

            Rebuild();
        }
        public void RemoveAll(T[] mass)
        {
            for (int i = 0; i < mass.Length; i++)
                Remove(mass[i]);
        }
        public void ReatainAll(T[] mass)
        {
            bool check;
            for (int i = 0; i < size; i++)
            {
                check = false;
                for (int j = 0; j < mass.Length; j++)
                {
                    if (comparator.Compare(mass[i], queue[j]) == 0)
                    {
                        check = true;
                    }
                }

                if (!check)
                {
                    Remove(mass[i]);
                }
            }

            Rebuild();
        }


        public T[] ToArray()
        {
            T[] mass = new T[size];
            for (int i = 0; i < size; i++) { mass[i] = queue[i]; }
            return mass;
        }
        public void ToArray(ref T[] mass)
        {
            if (mass == null)
            {
                mass = ToArray();
                return;
            }
            if (mass.Length == size)
            {
                for (int i = 0; i < size; i++)
                {
                    mass[i] = queue[i];
                }
                return;
            }
            mass = new T[size];
            for (int i = 0; i < size; i++)
            {
                mass[i] = queue[i];
            }
        }
        public T Element()
        {
            if (size == 0)
            {
                throw new ArgumentOutOfRangeException("index");
            }

            return queue[0];
        }
        private int Amount(T o)
        {
            int amount = 0;
            for (int i = 0; i < size; i++)
            {
                if (comparator.Compare(o, queue[i]) == 0)
                {
                    amount++;
                }
            }

            return amount;
        }
        public bool Offer(T o)
        {
            int old_amount = Amount(o);
            Add(o);

            int new_amount = Amount(o);
            if (old_amount != new_amount)
            {
                return true;
            }

            return false;
        }
        public T Peek()
        {
            if (size == 0)
            {
                return default;
            }

            return queue[0];
        }
        public T Poll()
        {
            if (size == 0) { return default; }
            T elem = queue[0];
            for (int i = 0; i < size - 1; i++)
            {
                queue[i] = queue[i + 1];
            }
            size--;
            Rebuild();

            return elem;
        }
    }
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
            set
            {
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
            if (i == 0) Console.Write($"{elementData[i]} ");
            if (2 * i + 1 > elementCount - 1) return;

            Console.Write($"{elementData[2 * i + 1]} {elementData[2 * i + 1]} ");
            Print(2 * i + 1);
            Print(2 * i + 2);
        }
    }
}
