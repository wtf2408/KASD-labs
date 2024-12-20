using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kasd_labs_console
{
    public class MyTreeSet<T>
    {
        private MyTreeMap<T, object> m;
        private readonly object notUsed;
        public MyTreeSet() : this(Comparer<T>.Default) {}
        public MyTreeSet(MyTreeMap<T, object> m)
        {
            this.m = m;
        }
        public MyTreeSet(SortedSet<T> m) : this(Comparer<T>.Default) 
        {
            AddAll(m.ToArray());
        }
        public MyTreeSet(T[] arr)
        {
            AddAll(arr);
        }
        public MyTreeSet(IComparer<T> comparer)
        {
            m = new MyTreeMap<T, object>(comparer);
        }

        public void AddAll(T[] arr)
        {
            foreach (var item in arr) Add(item);
        }
        public void Add(T item) 
        {
            m.Put(item, notUsed);
        }
        public void Clear()
        {
            m.Clear();  // Очистить дерево
        }
        public bool Contains(object o)
        {
            if (o == null) throw new ArgumentNullException(nameof(o));
            return m.ContainsKey((T)o);  // Проверка по ключу в MyTreeMap
        }
        public bool ContainsAll(T[] a)
        {
            foreach (var item in a)
            {
                if (!Contains(item))
                    return false;
            }
            return true;
        }
        public bool IsEmpty()
        {
            return m.IsEmpty();  // Если MyTreeMap пуст, то и множество пусто
        }
        public void Remove(object o)
        {
            if (o == null) throw new ArgumentNullException(nameof(o));
            m.Remove((T)o);  // Удалить элемент из MyTreeMap
        }
        public void RemoveAll(T[] a)
        {
            foreach (var item in a)
            {
                Remove(item);
            }
        }
        public void RetainAll(T[] a)
        {
            foreach (var item in a)
            {
                if (!Contains(item)) Remove(item);
            }
        }
        public int Size()
        {
            return m.Size;  // Размер множества совпадает с размером MyTreeMap
        }
        public T[] ToArray()
        {
            return m.KeySet().ToArray();
        }
        public void ToArray(T[] a)
        {
            if (a is null) a = new T[Size()];
            a = ToArray();
        }
        public T First()
        {
            return m.FirstKey();
        }

        public T Last()
        {
            return m.LastKey();
        }
        public MyTreeSet<T> SubSet(T fromElement, T toElement)
        {
            var subMap = m.SubMap(fromElement, toElement);
            return new MyTreeSet<T>(subMap);
        }

        public MyTreeSet<T> HeadSet(T toElement)
        {
            var headMap = m.HeadMap(toElement);
            return new MyTreeSet<T>(headMap);
        }

        public MyTreeSet<T> TailSet(T fromElement)
        {
            var tailMap = m.TailMap(fromElement);
            return new MyTreeSet<T>(tailMap);
        }
        public T Ceiling(T obj)
        {
            return m.CeilingKey(obj);
        }

        public T Floor(T obj)
        {
            return m.FloorKey(obj);
        }

        public T Higher(T obj)
        {
            return m.HigherKey(obj);
        }

        public T Lower(T obj)
        {
            return m.LowerKey(obj);
        }
        public MyTreeSet<T> HeadSet(T upperBound, bool incl)
        {
            var headSet = m.HeadMap(upperBound);
            return new MyTreeSet<T>(headSet);
        }

        public MyTreeSet<T> SubSet(T lowerBound, bool lowIncl, T upperBound, bool highIncl)
        {
            var subSet = m.SubMap(lowerBound, upperBound);
            return new MyTreeSet<T>(subSet);
        }

        public MyTreeSet<T> TailSet(T fromElement, bool inclusive)
        {
            var tailSet = m.TailMap(fromElement);
            return new MyTreeSet<T>(tailSet);
        }
        public T PollFirst()
        {
            var firstEntry = m.PollFirstEntry();
            return firstEntry.HasValue ? firstEntry.Value.Key : default;
        }

        public T PollLast()
        {
            var lastEntry = m.PollLastEntry();
            return lastEntry.HasValue ? lastEntry.Value.Key : default;
        }

        public IEnumerable<T> DescendingIterator()
        {
            return m.KeySet().Reverse();
        }

        public MyTreeSet<T> DescendingSet()
        {
            var descendingMap = m.KeySet().Reverse().ToArray();
            return new MyTreeSet<T>(descendingMap);
        }
    }
}
