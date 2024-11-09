using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kasd_labs_console
{
    public class MyStack<T> : MyVector<T>
    {
        public void Push(T item)
        {
            Add(item);
        }
        public T Pop()
        {
            if (IsEmpty())
                throw new InvalidOperationException("Stack is empty");

            T topItem = this[Size - 1];
            Remove(Size - 1);
            return topItem;
        }
        public T Peek()
        {
            if (IsEmpty())
                throw new InvalidOperationException("Stack is empty");

            return this[Size - 1];
        }
        public bool Empty()
        {
            return base.IsEmpty();
        }

        public int Search(T item)
        {
            int index = LastIndexOf(item);
            if (index == -1)
                return -1;
            return Size - index;
        }
    }
}

