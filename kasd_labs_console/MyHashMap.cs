using System.Collections.Generic;
using System;

namespace kasd_labs_console
{

    public class MyHashMap<TK, TV> where TK : IComparable<TK>

    {
        private const int DEFAULT_CAPACITY = 16;
        private const float DEFAULT_COEFFICENT = 0.75f;


        private Entry<TK, TV>[] _table;
        private int _size;
        private float _loadFactor;
        private int _threshold;

        public MyHashMap() : this(DEFAULT_CAPACITY, DEFAULT_COEFFICENT)
        {
        }

        public MyHashMap(int initialCapacity) : this(initialCapacity, DEFAULT_COEFFICENT)
        {
        }

        public MyHashMap(int initialCapacity, float loadFactor)
        {
            if (initialCapacity < 1) throw new ArgumentOutOfRangeException("initialCapacity");
            if (loadFactor <= 0) throw new ArgumentOutOfRangeException("loadFactor");

            _loadFactor = loadFactor;
            _table = new Entry<TK, TV>[initialCapacity];
            _threshold = (int)(initialCapacity * loadFactor);
            _size = 0;
        }

        private int ToHash(object key)
        {
            if (key != null) return (key.GetHashCode() & 0x7FFFFFFF) % _table.Length;
            else throw new NullReferenceException("Key can't be null");
        }

        private Entry<TK, TV> GetEntry(object key)
        {
            Entry<TK, TV> mappedEntry = _table[ToHash(key)];

            while (mappedEntry != null)
            {
                if (mappedEntry.Key.Equals(key)) return mappedEntry;

                mappedEntry = mappedEntry.Next;
            }

            return null;
        }

        public bool IsEmpty() => _size == 0;

        public TV Get(object key)
        {
            Entry<TK, TV> currentEntry = GetEntry(key);
            if (currentEntry != null) return currentEntry.Value;
            return default;
        }

        public void Clear()
        {
            Array.Clear(_table, 0, _table.Length);
            _size = 0;
        }

        public bool ContainsKey(object key) => GetEntry(key) != null;

        public bool ContainsValue(object value)
        {
            foreach (Entry<TK, TV> entry in _table)
            {
                Entry<TK, TV> currentEntry = entry;
                while (currentEntry != null)
                {
                    if (currentEntry.Value != null && currentEntry.Value.Equals(value)) return true;
                    currentEntry = currentEntry.Next;
                }
            }

            return false;
        }

        public HashSet<TK> KeySet()
        {
            HashSet<TK> hashSet = new HashSet<TK>();
            foreach (Entry<TK, TV> entry in _table)
            {
                Entry<TK, TV> current = entry;
                while (current != null)
                {
                    hashSet.Add(current.Key);
                    current = current.Next;
                }
            }
            return hashSet;
        }

        public HashSet<KeyValuePair<TK, TV>> EntrySet()
        {
            HashSet<KeyValuePair<TK, TV>> hashSet = new HashSet<KeyValuePair<TK, TV>>();
            foreach (Entry<TK, TV> entry in _table)
            {
                Entry<TK, TV> current = entry;
                while (current != null)
                {
                    hashSet.Add(new KeyValuePair<TK, TV>(current.Key, current.Value));
                    current = current.Next;
                }
            }
            return hashSet;
        }

        private void Resize()
        {
            int newCapacity = _table.Length * 2;
            Entry<TK, TV>[] newTable = new Entry<TK, TV>[newCapacity];

            foreach (var entry in _table)
            {
                var current = entry;
                while (current != null)
                {
                    int newIndex = (current.Key.GetHashCode() & 0x7FFFFFFF) % newTable.Length;
                    Entry<TK, TV> next = current.Next;

                    current.Next = newTable[newIndex];
                    newTable[newIndex] = current;

                    current = next;
                }
            }

            _table = newTable;
            _threshold = (int)(newCapacity * _loadFactor);
        }

        public void Put(TK key, TV value)
        {
            if (_size + 1 > _threshold) Resize();
            Entry<TK, TV> currentEntry = _table[ToHash(key)];
            if (currentEntry != null)
            {
                while (currentEntry.Next != null)
                {
                    if (currentEntry.Key.Equals(key))
                    {
                        currentEntry.Value = value;
                        return;
                    }

                    currentEntry = currentEntry.Next;
                }

                currentEntry.Next = new Entry<TK, TV>(key, value);
            }
            else
            {
                var hash = ToHash(key);
                _table[hash] = new Entry<TK, TV>(key, value);
            }
            _size++;
        }

        public Entry<TK, TV> Remove(object key)
        {
            int index = ToHash(key);
            Entry<TK, TV> current = _table[index];
            Entry<TK, TV> prev = null;
            while (current != null)
            {
                if (current.Key.Equals(key))
                {
                    if (prev == null) _table[index] = current.Next;
                    else prev.Next = current.Next;
                    _size--;
                    return current;
                }

                prev = current;
                current = current.Next;
            }
            return null;
        }
    }
}