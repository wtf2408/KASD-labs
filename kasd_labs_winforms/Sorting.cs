using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kasd_labs_winforms
{
    class Sorting
    {
        public static void Swap<T>(ref T a, ref T b)
        {
            T value = a;
            a = b;
            b = value;
        }
        public static void SortBubble<T>(T[] mass, Comparer<T> comparer)
        {
            for (int i = 0; i < mass.Length; i++)
            {
                for (int j = 0; j < mass.Length - 1; j++)
                {
                    if (comparer.Compare(mass[j], mass[j + 1]) > 0) { Swap(ref mass[j], ref mass[j + 1]); }
                }
            }
        }
        public static void SortShaker<T>(T[] mass, Comparer<T> comparer)
        {
            int left_index = 1;
            int right_index = mass.Length - 1;
            while (left_index <= right_index)
            {
                for (int i = right_index; i >= left_index; i--)
                {
                    if (comparer.Compare(mass[i - 1], mass[i]) > 0) { Swap(ref mass[i - 1], ref mass[i]); }
                }
                left_index++;

                for (int i = left_index; i <= right_index; i++)
                {
                    if (comparer.Compare(mass[i - 1], mass[i]) > 0) { Swap(ref mass[i - 1], ref mass[i]); }
                }
                right_index--;
            }
        }
        public static void SortComb<T>(T[] mass, Comparer<T> comparer)
        {
            const double factor = 1.247;
            int iter = mass.Length - 1;

            while (iter >= 1)
            {
                for (int i = 0; i < mass.Length - iter; i++)
                {
                    if (comparer.Compare(mass[i], mass[i + iter]) > 0) { Swap(ref mass[i], ref mass[i + iter]); }
                }
                iter = (int)(iter / factor);
            }

            SortBubble(mass, comparer);
        }

        public static void SortInsertion<T>(T[] mass, Comparer<T> comparer)
        {
            for (int i = 0; i < mass.Length; i++)
            {
                T elem = mass[i];
                int j = i - 1;
                while (j >= 0 && comparer.Compare(mass[j], elem) > 0)
                {
                    mass[j + 1] = mass[j--];
                }
                mass[j + 1] = elem;
            }
        }

        public static void SortShell<T>(T[] mass, Comparer<T> comparer)
        {
            int d = mass.Length / 2;
            while (d > 0)
            {
                for (int i = 0; i < mass.Length - d; i++)
                {
                    int j = i;
                    while (j >= 0 && comparer.Compare(mass[j], mass[j + d]) > 0)
                    {
                        Swap(ref mass[j], ref mass[j + d]);
                        j--;
                    }
                }
                d /= 2;
            }
        }

        private class Tree<T>
        {
            public T value;
            public int height;
            public Tree<T> left, right;
            private Comparer<T> comparer;

            public Tree(T value, Comparer<T> comparer)
            {
                this.value = value;
                this.height = 1;
                this.left = this.right = null;
                this.comparer = comparer;
            }

            public void InsertTree(Tree<T> node)
            {
                if (comparer.Compare(node.value, value) < 0)
                {
                    if (left == null) { left = node; }
                    else { left.InsertTree(node); }
                }
                else
                {
                    if (right == null) { right = node; }
                    else { right.InsertTree(node); }
                }
            }

            public void Transform(ref T[] mass, ref int index)
            {
                if (left != null) { left.Transform(ref mass, ref index); }
                mass[index++] = value;
                if (right != null) { right.Transform(ref mass, ref index); }
            }
        }

        public static void SortTree<T>(T[] mass, Comparer<T> comparer)
        {
            Tree<T> tree = new Tree<T>(mass[0], comparer);
            for (int i = 1; i < mass.Length; i++)
            {
                tree.InsertTree(new Tree<T>(mass[i], comparer));
            }

            int index = 0;
            tree.Transform(ref mass, ref index);
        }
        public static void SortGnome<T>(T[] mass, Comparer<T> comparer)
        {
            int i = 1, j = 2;
            while (i < mass.Length)
            {
                if (comparer.Compare(mass[i - 1], mass[i]) <= 0)
                {
                    i = j++;
                }
                else
                {
                    T t = mass[i];
                    mass[i] = mass[i - 1];
                    mass[--i] = t;
                    if (i == 0)
                    {
                        i = j++;
                    }
                }
            }
        }
        public static void SortSelection<T>(T[] mass, Comparer<T> comparer)
        {
            for (int i = 0; i < mass.Length - 1; i++)
            {
                int min_ind = i;
                for (int j = i + 1; j < mass.Length; j++)
                {
                    if (comparer.Compare(mass[j], mass[min_ind]) < 0)
                    {
                        min_ind = j;
                    }
                }
                if (min_ind != i)
                {
                    Swap(ref mass[i], ref mass[min_ind]);
                }
            }
        }
        private static void Heapify<T>(T[] mass, int n, int i, Comparer<T> comparer)
        {
            int largest = i;
            int ind = 2 * i + 1;
            if (ind < n && comparer.Compare(mass[ind], mass[largest]) > 0) { largest = ind; }
            if (ind + 1 < n && comparer.Compare(mass[ind + 1], mass[largest]) > 0) { largest = ind + 1; }
            if (largest != i)
            {
                Swap(ref mass[i], ref mass[largest]);
                Heapify(mass, n, largest, comparer);
            }
        }

        public static void SortHeap<T>(T[] mass, Comparer<T> comparer)
        {
            int n = mass.Length;
            for (int i = n / 2 - 1; i >= 0; i--)
            {
                Heapify(mass, n, i, comparer);
            }
            for (int i = n - 1; i >= 0; i--)
            {
                Swap(ref mass[0], ref mass[i]);
                Heapify(mass, i, 0, comparer);
            }
        }
        //------done

        public static void SortQuick<T>(T[] mass, int min_ind, int max_ind, Comparer<T> comparer)
        {
            while (min_ind < max_ind)
            {
                if (max_ind - min_ind < 10)
                {
                    SortInsertion(mass, min_ind, max_ind, comparer);
                    break;
                }

                int p = Partition(mass, min_ind, max_ind, comparer);
                if (p - min_ind < max_ind - p)
                {
                    SortQuick(mass, min_ind, p - 1, comparer);
                    min_ind = p + 1;
                }
                else
                {
                    SortQuick(mass, p + 1, max_ind, comparer);
                    max_ind = p - 1;
                }
            }
        }

        public static void SortQuick<T>(T[] mass, Comparer<T> comparer)
        {
            SortQuick(mass, 0, mass.Length - 1, comparer);
        }

        private static int Partition<T>(T[] mass, int min_ind, int max_ind, Comparer<T> comparer)
        {
            int p_ind = MedianOfThree(mass, min_ind, max_ind, comparer);
            Swap(ref mass[p_ind], ref mass[max_ind]);

            int p = min_ind - 1;
            for (int i = min_ind; i <= max_ind; i++)
            {
                if (comparer.Compare(mass[i], mass[max_ind]) < 0 || i == max_ind)
                {
                    Swap(ref mass[++p], ref mass[i]);
                }
            }

            return p;
        }

        private static int MedianOfThree<T>(T[] mass, int min_ind, int max_ind, Comparer<T> comparer)
        {
            int mid_ind = (min_ind + max_ind) / 2;

            if (comparer.Compare(mass[min_ind], mass[mid_ind]) > 0) { Swap(ref mass[min_ind], ref mass[mid_ind]); }
            if (comparer.Compare(mass[min_ind], mass[max_ind]) > 0) { Swap(ref mass[min_ind], ref mass[max_ind]); }
            if (comparer.Compare(mass[mid_ind], mass[max_ind]) > 0) { Swap(ref mass[mid_ind], ref mass[max_ind]); }

            return mid_ind;
        }

        private static void SortInsertion<T>(T[] mass, int min_ind, int max_ind, Comparer<T> comparer)
        {
            for (int i = min_ind + 1; i <= max_ind; i++)
            {
                T key = mass[i];
                int j = i - 1;

                while (j >= min_ind && comparer.Compare(mass[j], key) > 0) { mass[j + 1] = mass[j--]; }
                mass[j + 1] = key;
            }
        }

        private static void Merge<T>(T[] mass, int left, int mid, int right, Comparer<T> comparer)
        {
            int n1 = mid - left + 1;
            int n2 = right - mid;
            T[] left_mass = new T[n1];
            T[] right_mass = new T[n2];
            Array.Copy(mass, left, left_mass, 0, n1);
            Array.Copy(mass, mid + 1, right_mass, 0, n2);

            int i = 0, j = 0, k = left;
            while (i < n1 && j < n2)
            {
                if (comparer.Compare(left_mass[i], right_mass[j]) <= 0) { mass[k++] = left_mass[i++]; }
                else { mass[k++] = right_mass[j++]; }
            }
            while (i < n1) { mass[k++] = left_mass[i++]; }
            while (j < n2) { mass[k++] = right_mass[j++]; }
        }

        public static void SortMerge<T>(T[] mass, int left, int right, Comparer<T> comparer)
        {
            if (left < right)
            {
                int mid = (left + right) / 2;
                SortMerge(mass, left, mid, comparer);
                SortMerge(mass, mid + 1, right, comparer);
                Merge(mass, left, mid, right, comparer);
            }
        }

        public static void SortMerge<T>(T[] mass, Comparer<T> comparer)
        {
            SortMerge(mass, 0, mass.Length - 1, comparer);
        }

        public static void SortCounting<T>(T[] mass, Comparer<T> comparer)
        {
            dynamic min = mass[0];
            dynamic max = mass[0];
            for (int i = 1; i < mass.Length; i++)
            {
                if (comparer.Compare(mass[i], min) < 0) { min = mass[i]; }
                if (comparer.Compare(mass[i], max) > 0) { max = mass[i]; }
            }

            int[] count = new int[max - min + 1];
            for (int i = 0; i < mass.Length; i++)
            {
                count[mass[i] - min]++;
            }

            int k = 0;
            for (int i = 0; i < count.Length; i++)
            {
                while (count[i]-- > 0)
                {
                    mass[k++] = i + min;
                }
            }
        }

        private static void CountingSortByDigit<T>(T[] mass, int exp, Comparer<T> comparer)
        {
            T[] output = new T[mass.Length];
            int[] count = new int[10];
            for (int i = 0; i < mass.Length; i++) { count[((dynamic)mass[i] / exp) % 10]++; }
            for (int i = 1; i < 10; i++) { count[i] += count[i - 1]; }
            for (int i = mass.Length - 1; i >= 0; i--)
            {
                output[count[((dynamic)mass[i] / exp) % 10] - 1] = mass[i];
                count[((dynamic)mass[i] / exp) % 10]--;
            }
            Array.Copy(output, mass, mass.Length);
        }
        public static void SortRadix<T>(T[] mass, Comparer<T> comparer)
        {
            dynamic max = mass[0];
            for (int i = 1; i < mass.Length; i++)
            {
                if (comparer.Compare(mass[i], max) > 0) { max = mass[i]; }
            }

            int exp = 1;
            while (max / exp > 0)
            {
                CountingSortByDigit(mass, exp, comparer);
                exp *= 10;
            }
        }
        public static void MergeBitonic<T>(T[] a, int low, int lenght, bool dir, Comparer<T> comparer)
        {
            if (lenght > 1)
            {
                int k = lenght / 2;
                for (int i = low; i < low + k; i++)
                {
                    bool check = comparer.Compare(a[i], a[i + k]) > 0;
                    if (dir == check) { Swap(ref a[i], ref a[i + k]); }
                }
                MergeBitonic(a, low, k, dir, comparer);
                MergeBitonic(a, low + k, k, dir, comparer);
            }
        }
        public static void SortBitonic<T>(T[] mass, int low, int lenght, bool dir, Comparer<T> comparer)
        {
            if (lenght > 1)
            {
                int k = lenght / 2;
                SortBitonic(mass, low, k, true, comparer);
                SortBitonic(mass, low + k, k, false, comparer);
                MergeBitonic(mass, low, lenght, dir, comparer);
            }
        }
        public static void SortBitonic<T>(T[] mass, Comparer<T> comparer)
        {
            SortBitonic(mass, 0, mass.Length, true, comparer);
        }

    }
}
