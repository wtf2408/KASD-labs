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
}
