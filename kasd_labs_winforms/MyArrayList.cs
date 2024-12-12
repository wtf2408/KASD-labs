using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kasd_labs_winforms
{
    public class MyArrayList<T>
    {
        private T[] elementData;
        private int size; //  количество элементов в динамическом массиве.
        public int Size { get => size; }
        public T this[int i] // метод получения и установки эл. по индексу
        {
            get
            { 
                if (i < 0 || i != 0 && i >= size) throw new IndexOutOfRangeException();
                return elementData[i];
            }
            set
            {
                if (i < 0 || i != 0 && i >= size) throw new IndexOutOfRangeException();
                elementData[i] = value;
            }
        }
        public MyArrayList()
        {
            elementData = new T[10];
            size = 0;
        }
        public MyArrayList(T[] array)
        {
            elementData = new T[array.Length];
            Array.Copy(array, elementData, array.Length);
            size = array.Length;
        }
        public MyArrayList(int capacity)
        {
            if (capacity < 0) throw new ArgumentOutOfRangeException("размер динамического массива не может быть отрицательным");
            elementData = new T[capacity];
            size = 0;
        }
        public void Add(T item)
        {
            EnsureCapacity(size);
            this.elementData[size++] = item;
        }
        public void Add(int index, T item)
        {
            if (index < 0) return;
            if (index >= size) 
            { 
                    Add(item); 
                    return; 
            }
            T oldValue = elementData[index];
            EnsureCapacity(size);
            for (int i = size; i > index; i--)
            {
                elementData[i] = elementData[i - 1];
            }
            elementData[index] = item;
            size++;
        }
        public void AddAll(T[] array)
        {
            foreach (var item in array)
            {
                Add(item);
            }
        }
        public void AddAll(int index, T[] array)
        {
            for (int i = index; i < array.Length; i++)
            {
                Add(i, array[i]);
            }
        }
        public void Clear()
        {
            for (int i = 0; i < elementData.Length; i++) elementData[i] = default(T);
            size = 0;
        }
        public bool Contains(T item) // Метод проверки наличия объекта в массиве
        {
            return IndexOf(item) >= 0;
        }
        public bool ContainsAll(T[] array) // Метод проверки наличия всех объектов из массива в текущем массиве
        {
            foreach (var item in array)
            {
                if (!Contains(item)) return false;
            }
            return true;
        }
        public bool IsEmpty()
        {
            return size == 0;
        }
        public bool Remove(T item)
        {
            int index = IndexOf(item);
            if (index >= 0)
            {
                Remove(index: index);
                return true;
            }
            return false;
        }
        public T Remove(int index) // Метод удаления элемента по индексу
        {
            if (index < 0 || index >= size) throw new IndexOutOfRangeException();
            T oldValue = elementData[index];

            for (int i = index; i < size - 1; i++)
            {
                elementData[i] = elementData[i + 1];
            }

            elementData[--size] = default(T); // Удаляем ссылку на удаляемый объект для сборщика мусора
            return oldValue;
        }
        public void RemoveAll(T[] array) // Метод удаления всех указанных объектов
        {
            foreach (var item in array)
            {
                Remove(item);
            }
        }
        public void RetainAll(T[] array) // Метод оставления только указанных объектов
        {
            for (int i = size - 1; i >= 0; i--)
            {
                if (Array.IndexOf(array, elementData[i]) < 0)
                {
                    Remove(i);
                }
            }
        }
        public T[] ToArray()
        {
            var result = new T[size];
            Array.Copy(elementData, result, size);
            return result;
        }
        public void ToArray(ref T[] array)
        {
            if (array.Length < size) throw new ArgumentOutOfRangeException();
            for (int i = 0; i < elementData.Length; i++)
            {
                array[i] = elementData[i];
            }
            if (array is null) array = ToArray();
        }
        public int IndexOf(T item) // Метод нахождения индекса указанного объекта
        {
            for (int i = 0; i < size; i++)
            {
                if (Equals(item, elementData[i])) return i;
            }
            return -1;
        }
        public int LastIndexOf(T item) // Метод нахождения последнего индекса указанного объекта
        {
            for (int i = size - 1; i >= 0; i--)
            {
                if (Equals(item, elementData[i])) return i;
            }
            return -1;
        }
        public MyArrayList<T> SubList(int fromIndex, int toIndex) // Метод получения подсписка
        {
            if (fromIndex < 0 || toIndex > size || fromIndex > toIndex)
                throw new ArgumentOutOfRangeException();

            var sublist = new MyArrayList<T>(toIndex - fromIndex);

            for (int i = fromIndex; i < toIndex; i++)
            {
                sublist.Add(elementData[i]);
            }

            return sublist;
        }
        private void EnsureCapacity(int count) // Метод для гарантии нужной вместительности внутреннего массива
        {
            if (count >= elementData.Length)
            {
                int newCapacity = (int)Math.Floor(elementData.Length * 1.5);
                T[] newElementData = new T[newCapacity];
                for (int i = 0; i < elementData.Length; i++) newElementData[i] = elementData[i];
                this.elementData = newElementData;
            }
        }
    }

}
