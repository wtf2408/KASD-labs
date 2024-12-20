using System;

namespace kasd_labs_console
{
    public class MyLinkedList<T>
    {
        public class MyItr : kasd_labs_console.extended.MyIterator<T>
        {
            private MyLinkedList<T> list;
            private int currentIndex;
            private bool removable;


            public MyItr(MyLinkedList<T> list)
            {
                this.list = list;
                currentIndex = -1;
                removable = false;
            }
            public MyItr(MyLinkedList<T> list, int idex)
            {
                this.list = list;
                currentIndex = idex -1;
                removable = false;
            }

            public bool HasNext()
            {
                return currentIndex + 1 < list.Size();
            }

            public T Next()
            {
                if (!HasNext()) throw new InvalidOperationException("No next element.");
                removable = true; // После вызова Next, можно удалять элемент
                return list.Get(++currentIndex);
            }


            public bool HasPrevious()
            {
                return currentIndex > 0;
            }
            public T Previous()
            {
                if (!HasPrevious()) throw new InvalidOperationException("No previous element.");
                return list.Get(--currentIndex);
            }
            public int NextIndex()
            {
                return currentIndex + 1;
            }
            public int PreviousIndex()
            {
                return currentIndex - 1;
            }
            public void Remove()
            {
                if (!removable) throw new InvalidOperationException("No element to remove.");
                list.Remove(currentIndex);
                currentIndex--;
                removable = false; // После удаления нельзя сразу удалять следующий элемент
            }

            public void Set(T element)
            {
                if (!removable) throw new InvalidOperationException("No element to set.");
                list.Add(currentIndex, element); // Заменяем элемент по индексу
            }

            public void Add(T element)
            {
                list.Add(currentIndex + 1, element);
                currentIndex++;
            }
        }
        class Node<T>
        {
            public T Value;
            public Node<T> Next { get; set; }
            public Node<T> Prev { get; set; }
            public Node(T element)
            {
                Next = null;
                Prev = Next;
                Value = element;
            }
        }
        Node<T> first;
        Node<T> last;
        int size;
        public MyLinkedList()
        {
            first = null;
            last = null;
            size = 0;
        }
        public MyLinkedList(T[] a)
        {
            foreach (T el in a)
            {
                Add(el);
            }
        }
        public MyLinkedList(int capacity)
        {
            first = null;
            last = null;
            size = 0;
        }
        public MyItr ListIterator() => new MyItr(this);
        public MyItr ListIterator(int index) => new MyItr(this, index);

        public void Add(T el)
        {
            Node<T> newNode = new Node<T>(el);
            if (size == 0)
            {
                first = newNode;
                last = newNode;
            }
            else
            {
                if (last != null)
                {
                    last.Next = newNode;
                    newNode.Prev = last;
                }

                last = newNode;
            }
            size++;

        }
        public void AddAll(T[] a)
        {
            foreach (T el in a)
                Add(el);
        }
        public void Clear()
        {
            first = null;
            last = null;
            size = 0;
        }
        public bool Contains(object o)
        {
            Node<T> step = first;
            while (step != null)
            {
                if (step.Value.Equals(o))
                    return true;
                step = step.Next;

            }
            return false;
        }
        public bool ContainsAll(T[] array)
        {
            bool[] check = new bool[array.Length];
            Node<T> step = first;
            while (step != null)
            {
                int i = 0;
                if (step.Equals(array[i])) check[i] = true;
                i++;
                step = step.Next;
            }
            for (int i = 0; i < check.Length; i++)
                if (!check[i]) return false;
            return true;
        }
        public bool Empty() => size == 0;
        public void Remove(T obj)
        {
            if (Contains(obj))
            {
                if (first.Value.Equals((T)obj))
                {
                    first = first.Next;
                    size--;
                    return;
                }
                Node<T> step = first;
                while (step != null)
                {
                    if (step.Next.Value.Equals((T)obj))
                    {
                        step.Next = step.Next.Next; size--; return;
                    }
                    else step = step.Next;
                }
            }
        }
        public void RemoveAll(T[] a)
        {
            foreach (T el in a)
                Remove(el);
        }
        public T Get(int index)
        {
            int curIndex = 0;
            if (index >= size)
                throw new IndexOutOfRangeException();
            if (index == size - 1)
                return last.Value;
            if (index == 0)
                return first.Value;
            Node<T> step = first;
            while (curIndex != index)
            {
                step = step.Next;
                curIndex++;
            }
            return step.Value;
        }
        public void RetainAll(T[] a)
        {
            T[] tmp = new T[a.Length];
            int ind = 0;
            for (int i = 0; i < size; i++)
            {
                int flag = 0;
                for (int j = 0; j < a.Length; j++)
                {
                    if (Get(i).Equals(a[j]))
                    {
                        flag = 0;
                        break;
                    }
                    else flag = 1;
                }
                if (flag == 1)
                    Remove(Get(i));
            }
        }
        public int Size() => size;
        public T[] ToArray()
        {
            T[] newAr = new T[size];
            for (int i = 0; i < size; i++)
                newAr[i] = Get(i);
            return newAr;
        }
        public T[] ToArray(T[] a)
        {
            if (a == null) return ToArray();
            else
            {
                T[] newAr = new T[a.Length + size];
                for (int i = 0; i < a.Length; i++)
                    newAr[i] = a[i];
                for (int i = a.Length; i < newAr.Length; i++)
                    newAr[i] = Get(i);
                return newAr;
            }
        }
        public T Element() => first.Value;
        public T Peek()
        {
            if (first == null)
                return default(T);
            return first.Value;
        }
        public T Poll()
        {
            T obj = first.Value;
            Remove(first.Value);
            return obj;
        }
        public T GetFirst()
        {
            if (first == null)
                throw new IndexOutOfRangeException();
            return first.Value;
        }
        public T GetLast()
        {
            if (last == null)
                throw new IndexOutOfRangeException();
            return last.Value;

        }
        public T PeekFirst()
        {
            if (size == 0)
                return default;
            return first.Value;
        }
        public T PeekLast()
        {
            if (size == 0)
                return default;
            return first.Value;
        }
        public T PollFirst()
        {
            T obj = first.Value;
            Remove(first.Value);
            return obj;
        }
        public T PollLast()
        {
            T obj = last.Value;
            Remove(last.Value);
            return obj;
        }
        public T RemoveFirst()
        {
            T obj = first.Value;
            Remove(first.Value);
            return obj;
        }
        public T RemoveLast()
        {
            T obj = last.Value;
            Remove(last.Value);
            return obj;
        }
        public T Pop()
        {
            T obj = first.Value;
            Remove(first.Value);
            return obj;
        }
        public bool Offer(T obj)
        {
            Add(obj);
            if (Contains(obj)) return true;
            return false;
        }
        public void Add(int index, T obj)
        {
            if (index == 0)
            {
                Node<T> step = new Node<T>(obj);
                step.Next = first;
                first.Prev = step;
                first = step;
                size++;
                return;
            }
            else if (index == size - 1)
            {
                Node<T> step = new Node<T>(obj);
                step.Prev = last;
                last.Next = step;
                last = step;
                size++;
                return;
            }
            else
            {
                int tind = 0;
                Node<T> step = new Node<T>(obj);
                step = first;
                while (tind != index)
                {
                    step = step.Next;
                    tind++;
                }
                if (tind == index)
                {
                    Node<T> el = new Node<T>(obj);
                    el.Next = step;
                    el.Prev = step.Prev;
                    step.Prev.Next = el;
                    step.Prev = el;
                    size = size + 1;
                }
            }
        }
        public void AddAll(int index, T[] a)
        {
            for (int i = a.Length - 1; i >= 0; i--)
                Add(index, a[i]);
        }
        public int IndexOf(T o)
        {
            Node<T> step = new Node<T>(o);
            step = first;
            int i = 0;
            while (step != null)
            {
                if (step.Value.Equals(o))
                    return i;
                i++;
                step = step.Next;
            }
            return -1;
        }
        public int LastIndexOf(T obj)
        {
            Node<T> step = new Node<T>(obj);
            step = first;
            int retInd = -1;
            int ind = 0;
            while (step != null)
            {
                if (step.Value.Equals(obj)) retInd = ind;
                ind++;
                step = step.Next;
            }
            return retInd;
        }
        public T Remove(int index)
        {
            T obj = Get(index);
            Remove(obj);
            return obj;
        }
        public void Set(int index, T obj)
        {
            Node<T> step = new Node<T>(obj);
            step = first;
            int ind = 0;
            while (ind != index)
            {
                ind++;
                step = step.Next;
            }
            step.Value = obj;
        }
        public T[] SubList(int fromIndex, int toIndex)
        {
            T[] a = new T[toIndex - fromIndex + 1];
            Node<T> step = new Node<T>(first.Value);
            step = first;
            int ind1 = 0;
            while (ind1 != fromIndex)
            {
                step = step.Next;
                ind1++;
            }
            int ind2 = 0;
            while (ind1 <= toIndex)
            {
                ind2++;
                ind1++;
                a[ind2] = step.Value;
                step = step.Next;
            }
            return a;

        }
        public void AddFirst(T obj)
        {
            Add(0, obj);
        }
        public void AddLast(T obj)
        {
            Add(size - 1, obj);
        }
        public bool OfferFirst(T obj)
        {
            AddFirst(obj);
            if (Contains(obj)) return true;
            return false;
        }
        public bool OfferLast(T obj)
        {
            AddLast(obj);
            if (Contains(obj)) return true;
            return false;
        }
        public void Push(T obj)
        {
            AddFirst(obj);
        }
        public bool RemoveLastOccurrence(T obj)
        {
            int ind = LastIndexOf(obj);
            if (ind != -1)
            {
                Remove(ind);
                return true;
            }
            return false;
        }
        public bool RemoveFirstOccurrence(T obj)
        {
            int index = IndexOf(obj);
            if (index != -1)
            {
                Remove(index);
                return true;
            }
            return false;
        }
        public void Print()
        {
            Node<T> step = new Node<T>(first.Value);
            step = first;
            while (step != null)
            {
                Console.Write($"{step.Value} ");
                step = step.Next;
            }
        }
    }
}