using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kasd_labs_console
{
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
}
