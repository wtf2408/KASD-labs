using System.Collections.Generic;
using System;

namespace kasd_labs_console
{
    class Node<T>
    {
        public T value;
        public Node<T> next;
        public Node<T> prev;
        public Node()
        {
            value = default;
            next = null;
            prev = null;
        }
        public Node(T value, Node<T> next, Node<T> prev)
        {
            this.value = value;
            this.next = next;
            this.prev = prev;
        }
    }
    public class MyHashMap<K, V>
    {
        
        private MyLinkedList<Pair<K, V>>[] table;
        private int size;
        private double loadFactor;
        public MyHashMap()
        {
            table = new MyLinkedList<Pair<K, V>>[16];
            for (int i = 0; i < 16; i++)
                table[i] = new MyLinkedList<Pair<K, V>>();
            size = 0;
            loadFactor = 0.75;
        }
        public MyHashMap(int initialCapacity)
        {
            if (initialCapacity <= 0)
                throw new ArgumentException("Initial capacity");
            table = new MyLinkedList<Pair<K, V>>[initialCapacity];
            for (int i = 0; i < initialCapacity; i++)
                table[i] = new MyLinkedList<Pair<K, V>>();
            size = 0;
            loadFactor = 0.75;
        }
        public MyHashMap(int initialCapacity, double loadFactor)
        {
            if (initialCapacity <= 0)
                throw new ArgumentException("Initial capacity");
            if (loadFactor <= 0 || 1 <= loadFactor)
                throw new ArgumentException("Load factor");
            table = new MyLinkedList<Pair<K, V>>[initialCapacity];
            for (int i = 0; i < initialCapacity; i++)
                table[i] = new MyLinkedList<Pair<K, V>>();
            size = 0;
            this.loadFactor = 0.75;
        }
        private int GetHashIndex(object obj)
        {
            return Math.Abs(obj.GetHashCode()) % table.Length;
        }
        private int GetNewHashIndex(object obj, int module)
        {
            return Math.Abs(obj.GetHashCode()) % module;
        }
        public void Print()
        {
            for (int i = 0; i < table.Length; i++)
                if (table[i].Size() != 0)
                {
                    for (int j = 0; j < table[i].Size(); j++)
                        Console.Write("(" + table[i].Get(j).key + ": " +
                            table[i].Get(j).value + ") ");
                    Console.Write("\n");
                }
        }
        public void Clear()
        {
            table = new MyLinkedList<Pair<K, V>>[16];
            for (int i = 0; i < 16; i++)
                table[i] = new MyLinkedList<Pair<K, V>>();
            size = 0;
        }
        public bool ContainsKey(object key)
        {
            int index = GetHashIndex(key);
            if (table[index].Size() == 0)
                return false;
            Node<Pair<K, V>> p = table[index].GetFirstNode();
            while (p != null)
            {
                if (Equals(p.value.key, key))
                    return true;
                p = p.next;
            }
            return false;
        }
        public bool ContainsValue(object value)
        {
            for (int i = 0; i < table.Length; i++)
            {
                if (table[i].Size() == 0)
                    continue;
                Node<Pair<K, V>> p = table[i].GetFirstNode();
                while (p != null)
                {
                    if (Equals(p.value.value, value))
                        return true;
                    p = p.next;
                }
            }
            return false;
        }
        public MyHashMap<Pair<K, V>, byte> EntrySet()
        {
            MyHashMap<Pair<K, V>, byte> set = new MyHashMap<Pair<K, V>, byte>();
            for (int i = 0; i < table.Length; i++)
                for (int j = 0; j < table[i].Size(); j++)
                    set.Put(table[i].Get(j), 0);
            return set;
        }
        public V Get(object key)
        {
            int index = GetHashIndex(key);
            if (table[index].Size() == 0)
                return default;
            Node<Pair<K, V>> p = table[index].GetFirstNode();
            while (p != null)
            {
                if (Equals(p.value.key, key))
                    return p.value.value;
                p = p.next;
            }
            return default;
        }
        public bool IsEmpty()
        {
            return size == 0;
        }
        public MyHashMap<K, byte> KeySet()
        {
            MyHashMap<K, byte> set = new MyHashMap<K, byte>();
            for (int i = 0; i < table.Length; i++)
                for (int j = 0; j < table[i].Size(); j++)
                    set.Put(table[i].Get(j).key, 0);
            return set;
        }
        private void Resize()
        {
            MyLinkedList<Pair<K, V>>[] newTable =
                new MyLinkedList<Pair<K, V>>[table.Length * 2];
            for (int i = 0; i < table.Length * 2; i++)
                newTable[i] = new MyLinkedList<Pair<K, V>>();
            int index;
            for (int i = 0; i < table.Length; i++)
            {
                if (table[i].Size() == 0)
                    continue;
                index = GetNewHashIndex
                    (table[i].GetFirstNode().value.key, newTable.Length);
                newTable[index] = table[i];
                table[i] = new MyLinkedList<Pair<K, V>>();
            }
            table = newTable;
        }
        public void Put(K key, V value)
        {
            if ((double)size / table.Length > loadFactor)
                Resize();
            int index = GetHashIndex(key);
            if (table[index].Size() == 0)
            {
                table[index] = new MyLinkedList<Pair<K, V>>();
                Pair<K, V> pair = new Pair<K, V>(key, value);
                table[index].Add(pair);
                size++;
                return;
            }
            Node<Pair<K, V>> p = table[index].GetFirstNode();
            while (p != null)
            {
                if (Equals(p.value.key, key))
                {
                    p.value.value = value;
                    size++;
                    return;
                }
                p = p.next;
            }
            Pair<K, V> newPair = new Pair<K, V>(key, value);
            table[index].Add(newPair);
            size++;
        }
        public void Remove(object key)
        {
            int index = GetHashIndex(key);
            if (table[index].Size() == 0)
                return;
            Node<Pair<K, V>> p = table[index].GetFirstNode();
            while (p != null)
            {
                if (Equals(p.value.key, key))
                {
                    Pair<K, V> pair =
                        new Pair<K, V>((K)key, p.value.value);
                    table[index].Remove(pair);
                    size--;
                    return;
                }
                p = p.next;
            }
        }
        public int Size()
        {
            return size;
        }
    }
    public class Pair<T1, T2>
    {
        public T1 key;
        public T2 value;
        public Pair(T1 key, T2 value)
        {
            this.key = key;
            this.value = value;
        }
        public override string ToString()
        {
            return "(" + key.ToString() + "; " +
                value.ToString() + ")";
        }
        public override bool Equals(object pair)
        {
            return key.Equals(((Pair<T1, T2>)pair).key);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
    class MyLinkedList<T>
    {

        private Node<T> first;
        private Node<T> last;
        private int size;
        public MyLinkedList()
        {
            first = null;
            last = null;
            size = 0;
        }
        public MyLinkedList(T[] A)
        {
            if (A.Length == 0)
            {
                first = null;
                last = null;
                size = 0;
                return;
            }
            if (A.Length == 1)
            {
                Node<T> node = new Node<T>(A[0], null, null);
                first = node;
                last = node;
                size = 1;
                return;
            }
            Node<T> begin = new Node<T>(A[0], null, null);
            Node<T> p = begin;
            Node<T> q = new Node<T>();
            Node<T> end;
            int i = 1;
            while (i < A.Length)
            {
                q = new Node<T>(A[i], null, p);
                p.next = q;
                p = q;
                i++;
            }
            end = q;
            first = begin;
            last = end;
            size = A.Length;
        }
        public void Print()
        {
            Node<T> p = first;
            while (p != null)
            {
                Console.Write(p.value + " ");
                p = p.next;
            }
            Console.Write('\n');
        }
        public void Add(T element)
        {
            Node<T> node = new Node<T>(element, null, last);
            if (size == 0)
            {
                first = last = node;
                size++;
                return;
            }
            last.next = node;
            last = node;
            size++;
        }
        public void AddAll(T[] array)
        {
            for (int i = 0; i < array.Length; i++)
                Add(array[i]);
        }
        public void Clear()
        {
            first = null;
            last = null;
            size = 0;
        }
        public bool Contains(object obj)
        {
            Node<T> p = first;
            while (p != null)
            {
                if (Equals(p.value, obj))
                    return true;
                p = p.next;
            }
            return false;
        }
        public bool ContainsAll(T[] array)
        {
            for (int i = 0; i < array.Length; i++)
                if (!Contains(array[i]))
                    return false;
            return true;
        }
        public bool IsEmpty()
        {
            return size == 0;
        }
        public void Remove(object obj)
        {
            if (first == null)
                return;
            Node<T> p = first.next;
            if (p != null)
                while (p.next != null)
                {
                    if (p.value.Equals(obj))
                    {
                        p.prev.next = p.next;
                        p.next.prev = p.prev;
                        size--;
                    }
                    p = p.next;
                }
            if (first.value.Equals(obj))
            {
                first = first.next;
                if (first != null)
                    first.prev = null;
                size--;
            }
            if (last.value.Equals(obj))
            {
                last = last.prev;
                if (last != null)
                    last.next = null;
                size--;
            }
            if (first == null || last == null)
                first = last = null;
        }
        public void RemoveAll(T[] array)
        {
            for (int i = 0; i < array.Length; i++)
                Remove(array[i]);
        }
        public void RetainAll(T[] array)
        {
            Node<T> p = first;
            bool flag;
            while (p != null)
            {
                flag = false;
                for (int i = 0; i < array.Length && !flag; i++)
                    if (Equals(array[i], p.value))
                        flag = true;
                if (!flag)
                    Remove(p.value);
                p = p.next;
            }
        }
        public int Size()
        {
            return size;
        }
        public T[] ToArray()
        {
            T[] array = new T[size];
            Node<T> p = first;
            int i = 0;
            while (p != null)
            {
                array[i] = p.value;
                i++;
                p = p.next;
            }
            return array;
        }
        public void ToArray(ref T[] array)
        {
            if (array == null)
            {
                array = ToArray();
                return;
            }
            Node<T> p = first;
            int i = 0;
            if (array.Length == size)
            {
                while (p != null)
                {
                    array[i] = p.value;
                    i++;
                    p = p.next;
                }
                return;
            }
            array = new T[size];
            while (p != null)
            {
                array[i] = p.value;
                i++;
                p = p.next;
            }
        }
        public void Add(int index, T element)
        {
            if (index < 0 || index > size)
                throw new ArgumentOutOfRangeException("Index");
            if (index == 0)
            {
                Node<T> node = new Node<T>(element, first, null);
                if (first != null)
                    first.prev = node;
                first = node;
                if (last == null)
                    last = first;
                size++;
                return;
            }
            if (index == size)
            {
                Add(element);
                return;
            }
            Node<T> p = first;
            for (int i = 0; i < index; i++)
                p = p.next;
            Node<T> newNode = new Node<T>(element, p, p.prev);
            p.prev.next = newNode;
            p.prev = newNode;
            size++;
        }
        public void AddAll(int index, T[] array)
        {
            if (index < 0 || index > size)
                throw new ArgumentOutOfRangeException("Index");
            for (int i = array.Length - 1; i >= 0; i--)
                Add(index, array[i]);
        }
        public T Get(int index)
        {
            if (index < 0 || index > size - 1)
                throw new ArgumentOutOfRangeException("Index");
            Node<T> p = first;
            for (int i = 0; i < index; i++)
                p = p.next;
            return p.value;
        }
        public int IndexOf(object obj)
        {
            Node<T> p = first;
            int index = 0;
            while (p != null)
            {
                if (Equals(p.value, obj))
                    return index;
                index++;
                p = p.next;
            }
            return -1;
        }
        public int LastIndexOf(object obj)
        {
            Node<T> p = last;
            int index = size - 1;
            while (p != null)
            {
                if (Equals(p.value, obj))
                    return index;
                index--;
                p = p.prev;
            }
            return -1;
        }
        public T RemoveAt(int index)
        {
            if (index < 0 || index > size - 1)
                throw new ArgumentOutOfRangeException("Index");
            if (index == 0)
            {
                T element = first.value;
                first = first.next;
                if (first != null)
                    first.prev = null;
                if (first == null)
                    last = null;
                size--;
                return element;
            }
            if (index == size - 1)
            {
                T element = last.value;
                last = last.prev;
                if (last != null)
                    last.next = null;
                if (last == null)
                    first = null;
                size--;
                return element;
            }
            Node<T> p = first;
            for (int i = 0; i < index; i++)
                p = p.next;
            T value = p.value;
            p.prev.next = p.next;
            p.next.prev = p.prev;
            size--;
            return value;
        }
        public void Set(int index, T element)
        {
            if (index < 0 || index > size - 1)
                throw new ArgumentOutOfRangeException("Index");
            Node<T> p = first;
            for (int i = 0; i < index; i++)
                p = p.next;
            p.value = element;
        }
        public MyLinkedList<T> SubList(int begin, int end)
        {
            if (begin < 0 || begin > size - 1)
                throw new ArgumentOutOfRangeException("BeginIndex");
            if (end < 0 || end > size)
                throw new ArgumentOutOfRangeException("EndIndex");
            MyLinkedList<T> list = new MyLinkedList<T>();
            Node<T> p = first;
            for (int i = 0; i < begin; i++)
                p = p.next;
            for (int i = 0; i < end - begin; i++)
            {
                list.Add(p.value);
                p = p.next;
            }
            return list;
        }
        public T Element()
        {
            if (size == 0)
                throw new ArgumentOutOfRangeException("ListIsEmpty");
            return Get(0);
        }
        private int Amount(object obj)
        {
            int amount = 0;
            Node<T> p = first;
            while (p != null)
            {
                if (Equals(p.value, obj))
                    amount++;
                p = p.next;
            }
            return amount;
        }
        public bool Offer(T element)
        {
            int oldAmount = Amount(element);
            Add(element);
            int newAmount = Amount(element);
            return oldAmount != newAmount;
        }
        public T Peek()
        {
            if (size == 0)
                return default;
            return Get(0);
        }
        public T Poll()
        {
            if (size == 0)
                return default;
            T element = Get(0);
            RemoveAt(0);
            return element;
        }
        public void AddFirst(T element)
        {
            Add(0, element);
        }
        public void AddLast(T element)
        {
            Add(element);
        }
        public T GetFirst()
        {
            return Get(0);
        }
        public Node<T> GetFirstNode()
        {
            return first;
        }
        public T GetLast()
        {
            return Get(Size() - 1);
        }
        public bool OfferFirst(T element)
        {
            int oldAmount = Amount(element);
            Add(0, element);
            int newAmount = Amount(element);
            return oldAmount != newAmount;
        }
        public bool OfferLast(T element)
        {
            return Offer(element);
        }
        public T Pop()
        {
            if (size == 0)
                throw new ArgumentOutOfRangeException("ListIsEmpty");
            return Poll();
        }
        public void Push(T element)
        {
            AddFirst(element);
        }
        public T PeekFirst()
        {
            if (size == 0)
                return default;
            return GetFirst();
        }
        public T PeekLast()
        {
            if (size == 0)
                return default;
            return GetLast();
        }
        public T PollFirst()
        {
            return Poll();
        }
        public T PollLast()
        {
            if (size == 0)
                return default;
            T element = Get(Size() - 1);
            RemoveAt(Size() - 1);
            return element;
        }
        public T RemoveLast()
        {
            return RemoveAt(Size() - 1);
        }
        public T RemoveFirst()
        {
            return RemoveAt(0);
        }
        public bool RemoveFirstOccurrence(object obj)
        {
            int index = IndexOf(obj);
            if (index == -1)
                return false;
            RemoveAt(index);
            return true;
        }
        public bool RemoveLastOccurrence(object obj)
        {
            int index = LastIndexOf(obj);
            if (index == -1)
                return false;
            RemoveAt(index);
            return true;
        }
    }
    
}