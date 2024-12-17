using kasd_labs_console;
using System.Collections.Generic;
using System;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;

namespace kasd_labs_console
{
    public class MyHashSet<T> 
    {
        private const int DEFAULT_CAPACITY = 16;
        private const float DEFAULT_LOAD_FACTOR = 0.75f;
        private readonly object fictiveObject = new object();
        private MyHashMap<T, object> map;
        private int initialCapacity;
        private float loadFactor;

        public MyHashSet() : this(DEFAULT_CAPACITY, DEFAULT_LOAD_FACTOR) { }

        public MyHashSet(T[] array) : this(DEFAULT_CAPACITY, DEFAULT_LOAD_FACTOR)
        {
            AddAll(array);
        }

        public MyHashSet(int initialCapacity) : this(initialCapacity, DEFAULT_LOAD_FACTOR) { }

        public MyHashSet(int initialCapacity, float loadFactor)
        {
            if (initialCapacity < 0) throw new ArgumentOutOfRangeException(nameof(initialCapacity));
            if (loadFactor <= 0) throw new ArgumentOutOfRangeException(nameof(loadFactor));

            this.initialCapacity = initialCapacity;
            this.loadFactor = loadFactor;
            map = new MyHashMap<T, object>(initialCapacity);
        }

        public void Add(T element)
        {
            if (!map.ContainsKey(element))
            {
                map.Put(element, fictiveObject);
            }
        }
        public void AddAll(T[] array)
        {
            foreach (T element in array)
            {
                Add(element);
            }
        }

        public void Clear()
        {
            map.Clear();
            initialCapacity = DEFAULT_CAPACITY;
            loadFactor = DEFAULT_LOAD_FACTOR;
        }

        public bool Contains(object obj)
        {
            return map.ContainsKey((T)obj);
        }

        public bool ContainsAll(T[] arr)
        {
            bool containsAll = true;
            for (int i = 0; containsAll && i < arr.Length; i++)
            {
                containsAll &= map.ContainsKey(arr[i]);
            }
            return containsAll;
        }

        public bool IsEmpty()
        {
            return map.IsEmpty();
        }

        public void Remove(object obj)
        {
            map.Remove((T)obj);
        }

        public void RemoveAll(T[] arr)
        {
            foreach (T element in arr)
            {
                map.Remove(element);
            }
        }

        public void RetainAll(T[] arr)
        {
            foreach (T element in map.KeySet())
            {
                if (!arr.Contains(element))
                {
                    map.Remove(element);
                }
            }
        }

        public int Size() => map.Size();

        public T[] ToArray()
        {
            HashSet<T> set = new HashSet<T>();
            set = map.KeySet();
            T[] arr = new T[set.Count];
            int i = 0;
            foreach (T element in set)
            {
                arr[i++] = element;
            }

            return arr;
        }

        public T[] ToArray(T[] a)
        {
            if (a.Length < Size())
            {
                a = new T[Size()];
            }
            HashSet<T> set = new HashSet<T>();
            set = map.KeySet();
            a = set.ToArray();
            return a;
        }
    }
}